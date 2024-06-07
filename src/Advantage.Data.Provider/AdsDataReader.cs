using AdvantageClientEngine;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Advantage.Data.Provider
{
    public class AdsDataReader : DbDataReader, IDisposable, IEnumerable
    {
        internal const int ADS_MAX_VARCHAR_LEN = 64000;
        internal const int ADS_MAX_MEMO_LEN = 2147483640;
        internal const int ADS_MAX_NMEMO_LEN = 1073741820;
        internal const int MAX_BLOB_SIZE = 2147483647;
        internal const int AXS_MAX_NUMERIC_LEN = 32;
        internal const int AXS_JULIAN_ADJUSTMENT = 1721425;
        internal const int ADS_CODEUNIT_SIZE = 2;
        protected bool mbDisposed;
        protected bool mbOpen;
        protected bool mbBOF = true;
        protected bool mbEOF;
        protected IntPtr mhCursor = IntPtr.Zero;
        protected bool mbIsCursor;
        protected bool mbIsStatic;
        protected int miOptions;
        protected int miRecordsAffected = -1;
        protected int miFieldCount = -1;
        protected AdsConnection mConnection;
        protected AdsCommand mCommand;
        protected CommandBehavior meBehavior;
        protected short msCacheSize;
        protected SortedList mFieldNames;
        protected byte[] mabRecord;
        protected bool mbRecordValid;
        protected bool mbHasRows;
        protected int[] maiFieldOffset;
        protected int[] maiFieldLength;
        protected short[] masACETypes;
        protected short[] masVFPNullable;
        protected short[] masVFPNoCPTrans;
        protected bool mbIsOEM;
        protected short msTableType = 3;
        private Encoding mEncoding;

        protected static byte[] mabDblNull = new byte[8]
        {
            (byte)32,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)128
        };

        protected static byte[] mabInt32Null = new byte[4]
        {
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)128
        };

        protected static byte[] mabInt16Null = new byte[2]
        {
            (byte)0,
            (byte)128
        };

        protected static byte[] mabInt64Null = new byte[8]
        {
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)128
        };

        protected static byte[] mabTimeNull = new byte[4]
        {
            byte.MaxValue,
            byte.MaxValue,
            byte.MaxValue,
            byte.MaxValue
        };

        internal AdsDataReader(
            IntPtr hCursor,
            int iRecsAffected,
            AdsCommand adsCmd,
            AdsConnection adsConn,
            CommandBehavior eBehavior)
        {
            this.mbOpen = true;
            this.mhCursor = hCursor;
            this.mConnection = adsConn;
            this.mCommand = adsCmd;
            this.meBehavior = eBehavior;
            this.miRecordsAffected = iRecsAffected;
            if (!(this.mhCursor != IntPtr.Zero))
                return;
            ushort pusCount;
            AdsException.CheckACE(ACE.AdsGetNumFields(this.mhCursor, out pusCount));
            this.miFieldCount = (int)pusCount;
            if ((this.meBehavior & CommandBehavior.SingleRow) != CommandBehavior.SingleRow)
                this.RecordCache = (short)100;
            ushort pusType1;
            AdsException.CheckACE(ACE.AdsGetHandleType(this.mhCursor, out pusType1));
            this.mbIsCursor = pusType1 == (ushort)5;
            if (!this.mbIsCursor)
            {
                this.mbIsStatic = false;
            }
            else
            {
                byte IsLive;
                AdsException.CheckACE(ACEUNPUB.AdsSqlPeekStatement(this.mhCursor, out IsLive));
                this.mbIsStatic = IsLive == (byte)0;
            }

            uint pulOptions;
            AdsException.CheckACE(ACE.AdsGetTableOpenOptions(this.mhCursor, out pulOptions));
            this.miOptions = (int)pulOptions;
            ushort pusType2;
            AdsException.CheckACE(ACE.AdsGetTableType(this.mhCursor, out pusType2));
            this.msTableType = (short)pusType2;
            if (this.msTableType != (short)3)
            {
                ushort pusCharType;
                AdsException.CheckACE(ACE.AdsGetTableCharType(this.mhCursor, out pusCharType));
                if (pusCharType == (ushort)2)
                    this.mbIsOEM = true;
            }

            ushort pbEof;
            if (ACE.AdsAtEOF(this.mhCursor, out pbEof) != 0U || pbEof != (ushort)0)
                return;
            this.mbHasRows = true;
        }

        ~AdsDataReader() => this.Dispose(false);

        protected override void Dispose(bool bDisposing)
        {
            if (this.mbDisposed)
                return;
            lock (this)
            {
                if (this.mbDisposed)
                    return;
                if (!bDisposing)
                    this.mConnection = (AdsConnection)null;
                this.Close();
                this.mbDisposed = true;
            }
        }

        internal void SetBOF()
        {
            this.mbBOF = true;
            this.mbEOF = false;
        }

        public override void Close()
        {
            if (this.mhCursor != IntPtr.Zero)
            {
                if (this.mConnection != null && this.mConnection.State == ConnectionState.Open)
                {
                    int num = (int)ACE.AdsCloseTable(this.mhCursor);
                }

                this.mhCursor = IntPtr.Zero;
                this.mbOpen = false;
                this.mbBOF = this.mbEOF = true;
                this.miFieldCount = 0;
            }

            if (this.mConnection == null)
                return;
            this.mConnection.Busy = false;
            if ((this.meBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
                this.mConnection.Close();
            this.mConnection = (AdsConnection)null;
        }

        public override int Depth => 0;

        public override bool IsClosed => !this.mbOpen;

        public override int RecordsAffected => this.miRecordsAffected;

        public override bool NextResult() => false;

        public override bool Read() => this.Read(this.mhCursor);

        protected bool Read(IntPtr hCursor)
        {
            if (hCursor == IntPtr.Zero)
                return false;
            this.InvalidateRecord();
            if ((this.meBehavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly || !this.mbOpen ||
                this.mbEOF)
                return false;
            if (this.mbBOF)
            {
                AdsException.CheckACE(ACE.AdsGotoTop(hCursor));
            }
            else
            {
                if ((this.meBehavior & CommandBehavior.SingleRow) == CommandBehavior.SingleRow)
                    return false;
                AdsException.CheckACE(ACE.AdsSkip(hCursor, 1));
            }

            ushort pbEof;
            AdsException.CheckACE(ACE.AdsAtEOF(this.mhCursor, out pbEof));
            if (pbEof != (ushort)0)
                this.mbEOF = true;
            else
                this.mbBOF = false;
            return !this.mbEOF;
        }

        private void ReadRecord()
        {
            if (this.mbRecordValid)
                return;
            if (this.mabRecord == null)
            {
                uint pulLength = 0;
                AdsException.CheckACE(ACE.AdsGetRecordLength(this.mhCursor, out pulLength));
                this.mabRecord = new byte[pulLength];
            }

            if (this.maiFieldOffset == null)
            {
                this.maiFieldOffset = new int[this.miFieldCount];
                this.maiFieldLength = new int[this.miFieldCount];
                if (this.msTableType == (short)4)
                {
                    this.masVFPNullable = new short[this.miFieldCount];
                    this.masVFPNoCPTrans = new short[this.miFieldCount];
                }

                for (int iCol = 0; iCol < this.miFieldCount; ++iCol)
                {
                    uint pulOffset;
                    AdsException.CheckACE(ACE.AdsGetFieldOffset(this.mhCursor, (uint)(iCol + 1), out pulOffset));
                    int aceFieldType = (int)this.GetACEFieldType(iCol);
                    uint pulLength;
                    AdsException.CheckACE(ACE.AdsGetFieldLength(this.mhCursor, (uint)(iCol + 1), out pulLength));
                    this.maiFieldOffset[iCol] = (int)pulOffset;
                    this.maiFieldLength[iCol] = (int)pulLength;
                    if (this.msTableType == (short)4)
                    {
                        ushort num;
                        AdsException.CheckACE(ACE.AdsIsNullable(this.mhCursor, (uint)(iCol + 1), out num));
                        this.masVFPNullable[iCol] = (short)num;
                        AdsException.CheckACE(ACE.AdsIsFieldBinary(this.mhCursor, (uint)(iCol + 1), out num));
                        this.masVFPNoCPTrans[iCol] = (short)num;
                    }
                }
            }

            uint length = (uint)this.mabRecord.Length;
            AdsException.CheckACE(ACE.AdsGetRecord(this.mhCursor, this.mabRecord, ref length));
            this.mbRecordValid = true;
        }

        protected void InvalidateRecord() => this.mbRecordValid = false;

        protected ArrayList GetKeyColumns()
        {
            char[] pucKeyColumn = new char[1024];
            ushort length = (ushort)pucKeyColumn.Length;
            uint keyColumn = ACE.AdsGetKeyColumn(this.mhCursor, pucKeyColumn, ref length);
            if (keyColumn == 5005U)
            {
                pucKeyColumn = new char[(int)length];
                keyColumn = ACE.AdsGetKeyColumn(this.mhCursor, pucKeyColumn, ref length);
            }

            if (keyColumn == 5041U)
                return (ArrayList)null;
            AdsException.CheckACE(keyColumn);
            ArrayList keyColumns =
                new ArrayList((ICollection)new string(pucKeyColumn, 0, (int)length).ToUpper().Split(';'));
            if (keyColumns.Count > 0 && string.Compare((string)keyColumns[keyColumns.Count - 1], string.Empty) == 0)
                keyColumns.RemoveAt(keyColumns.Count - 1);
            return keyColumns;
        }

        public override DataTable GetSchemaTable()
        {
            char[] pucName1 = new char[129];
            char[] pucName2 = new char[2 * (int)Math.Max((ushort)260, (ushort)65534) + 1];
            DataTable schemaTable = new DataTable("SchemaTable");
            this.CheckOpen();
            if (this.mhCursor == IntPtr.Zero)
                return (DataTable)null;
            DataColumn dataColumn1 = schemaTable.Columns.Add("ColumnName", Type.GetType("System.String"));
            DataColumn dataColumn2 = schemaTable.Columns.Add("ColumnOrdinal", Type.GetType("System.Int32"));
            DataColumn dataColumn3 = schemaTable.Columns.Add("ColumnSize", Type.GetType("System.Int32"));
            DataColumn dataColumn4 = schemaTable.Columns.Add("NumericPrecision", Type.GetType("System.Int16"));
            DataColumn dataColumn5 = schemaTable.Columns.Add("NumericScale", Type.GetType("System.Int16"));
            DataColumn dataColumn6 = schemaTable.Columns.Add("DataType", Type.GetType("System.Type"));
            DataColumn dataColumn7 = schemaTable.Columns.Add("ProviderType", Type.GetType("System.Int32"));
            DataColumn dataColumn8 = schemaTable.Columns.Add("IsLong", Type.GetType("System.Boolean"));
            DataColumn dataColumn9 = schemaTable.Columns.Add("AllowDBNull", Type.GetType("System.Boolean"));
            DataColumn dataColumn10 = schemaTable.Columns.Add("IsReadOnly", Type.GetType("System.Boolean"));
            DataColumn dataColumn11 = schemaTable.Columns.Add("IsRowVersion", Type.GetType("System.Boolean"));
            DataColumn dataColumn12 = schemaTable.Columns.Add("IsUnique", Type.GetType("System.Boolean"));
            DataColumn dataColumn13 = schemaTable.Columns.Add("IsKey", Type.GetType("System.Boolean"));
            DataColumn dataColumn14 = schemaTable.Columns.Add("IsAutoIncrement", Type.GetType("System.Boolean"));
            DataColumn dataColumn15 = schemaTable.Columns.Add("BaseSchemaName", Type.GetType("System.String"));
            DataColumn dataColumn16 = schemaTable.Columns.Add("BaseCatalogName", Type.GetType("System.String"));
            DataColumn dataColumn17 = schemaTable.Columns.Add("BaseTableName", Type.GetType("System.String"));
            DataColumn dataColumn18 = schemaTable.Columns.Add("BaseColumnName", Type.GetType("System.String"));
            DataColumn dataColumn19 = schemaTable.Columns.Add("IsAliased", Type.GetType("System.Boolean"));
            DataColumn dataColumn20 = schemaTable.Columns.Add("IsKeyColumn", Type.GetType("System.Boolean"));
            DataColumn dataColumn21 = schemaTable.Columns.Add("CaseSensitive", Type.GetType("System.Boolean"));
            DataColumn dataColumn22 = schemaTable.Columns.Add("ProviderTypeName", Type.GetType("System.String"));
            ArrayList keyColumns = this.GetKeyColumns();
            string pucTableName;
            if (this.mbIsCursor && this.mbIsStatic)
            {
                pucTableName = (string)null;
            }
            else
            {
                ushort length = (ushort)pucName2.Length;
                AdsException.CheckACE(ACE.AdsGetTableFilename(this.mhCursor, (ushort)1, pucName2, ref length));
                pucTableName = new string(pucName2, 0, (int)length);
            }

            string str1;
            if (this.mbIsCursor && this.mbIsStatic)
            {
                str1 = (string)null;
            }
            else
            {
                ushort length = (ushort)pucName2.Length;
                if (ACE.AdsGetTableFilename(this.mhCursor, (ushort)4, pucName2, ref length) != 0U)
                {
                    str1 = (string)null;
                }
                else
                {
                    Match match = new Regex("^:.+::").Match(new string(pucName2, 0, (int)length));
                    str1 = !match.Success ? (string)null : match.Value.Substring(1, match.Length - 3);
                }
            }

            for (int iCol = 0; iCol < this.miFieldCount; ++iCol)
            {
                DataRow row = schemaTable.NewRow();
                ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
                ushort length = (ushort)pucName1.Length;
                AdsException.CheckACE(ACE.AdsGetFieldName(this.mhCursor, (ushort)(iCol + 1), pucName1, ref length));
                string str2 = new string(pucName1, 0, (int)length);
                string strB = str2;
                ushort pusBaseFieldNum;
                if (ACEUNPUB.AdsGetBaseFieldNum(this.mhCursor, str2, out pusBaseFieldNum) == 0U)
                {
                    AdsException.CheckACE(ACEUNPUB.AdsSetBaseTableAccess(this.mhCursor, (ushort)1));
                    length = (ushort)pucName1.Length;
                    uint fieldName = ACE.AdsGetFieldName(this.mhCursor, pusBaseFieldNum, pucName1, ref length);
                    if (fieldName != 0U)
                    {
                        AdsException adsException = new AdsException(fieldName);
                        ACEUNPUB.AdsSetBaseTableAccess(this.mhCursor, (ushort)0);
                        throw adsException;
                    }

                    strB = new string(pucName1, 0, (int)length);
                    AdsException.CheckACE(ACEUNPUB.AdsSetBaseTableAccess(this.mhCursor, (ushort)0));
                }

                row[dataColumn1.Ordinal] = (object)str2;
                row[dataColumn2.Ordinal] = (object)iCol;
                int aceFieldLength = this.GetACEFieldLength(iCol);
                row[dataColumn3.Ordinal] = (object)aceFieldLength;
                row[dataColumn4.Ordinal] = (object)DBNull.Value;
                row[dataColumn5.Ordinal] = (object)DBNull.Value;
                row[dataColumn6.Ordinal] = (object)AdsDataReader.ConvertAceToSystemType(aceFieldType);
                row[dataColumn7.Ordinal] = (object)(int)aceFieldType;
                row[dataColumn22.Ordinal] = (object)AdsDataReader.ConvertACETypeToName(aceFieldType);
                row[dataColumn8.Ordinal] = (object)false;
                row[dataColumn9.Ordinal] = (object)true;
                if (this.mConnection.IsDictionaryConn)
                {
                    ushort pusPropertyLen = 2;
                    ushort pusProperty = 0;
                    if (ACE.AdsDDGetFieldProperty(this.mConnection.Handle, pucTableName, str2, (ushort)301,
                            ref pusProperty,
                            ref pusPropertyLen) == 0U && pusProperty == (ushort)0)
                        row[dataColumn9.Ordinal] = (object)false;
                }

                row[dataColumn10.Ordinal] = (object)false;
                row[dataColumn11.Ordinal] = (object)false;
                row[dataColumn12.Ordinal] =
                    keyColumns == null || keyColumns.Count != 1 ||
                    string.Compare((string)keyColumns[0], str2, true) != 0
                        ? (object)false
                        : (object)true;
                row[dataColumn13.Ordinal] = keyColumns == null || !keyColumns.Contains((object)str2.ToUpper())
                    ? (object)false
                    : (object)true;
                row[dataColumn20.Ordinal] = row[dataColumn13.Ordinal];
                row[dataColumn14.Ordinal] = (object)false;
                row[dataColumn15.Ordinal] = (object)DBNull.Value;
                row[dataColumn16.Ordinal] = (object)str1;
                row[dataColumn17.Ordinal] = (object)pucTableName;
                row[dataColumn18.Ordinal] = !this.mbIsCursor || !this.mbIsStatic ? (object)strB : (object)DBNull.Value;
                row[dataColumn19.Ordinal] = string.Compare(str2, strB, true) == 0 ? (object)false : (object)true;
                row[dataColumn21.Ordinal] = (object)false;
                switch (aceFieldType)
                {
                    case 1:
                    case 16:
                    case 20:
                    case 24:
                    case 29:
                        schemaTable.Rows.Add(row);
                        continue;
                    case 2:
                        ushort pusDecimals;
                        AdsException.CheckACE(ACE.AdsGetFieldDecimals(this.mhCursor, (uint)(ushort)(iCol + 1),
                            out pusDecimals));
                        row[dataColumn4.Ordinal] = (object)(short)aceFieldLength;
                        row[dataColumn5.Ordinal] = (object)(short)pusDecimals;
                        goto case 1;
                    case 3:
                    case 9:
                        row[dataColumn4.Ordinal] = (object)(short)10;
                        row[dataColumn5.Ordinal] = (object)(short)0;
                        goto case 1;
                    case 4:
                    case 23:
                    case 26:
                    case 27:
                        row[dataColumn21.Ordinal] = (object)true;
                        goto case 1;
                    case 5:
                        row[dataColumn3.Ordinal] = (object)2147483640;
                        row[dataColumn8.Ordinal] = (object)true;
                        row[dataColumn21.Ordinal] = (object)true;
                        goto case 1;
                    case 6:
                    case 7:
                        row[dataColumn3.Ordinal] = (object)int.MaxValue;
                        row[dataColumn8.Ordinal] = (object)true;
                        goto case 1;
                    case 8:
                        row[dataColumn3.Ordinal] = (object)64000;
                        row[dataColumn21.Ordinal] = (object)true;
                        goto case 1;
                    case 10:
                    case 17:
                        row[dataColumn4.Ordinal] = (object)(short)15;
                        row[dataColumn5.Ordinal] = (object)(short)0;
                        goto case 1;
                    case 11:
                        row[dataColumn4.Ordinal] = (object)(short)10;
                        row[dataColumn5.Ordinal] = (object)(short)0;
                        goto case 1;
                    case 12:
                        row[dataColumn4.Ordinal] = (object)(short)5;
                        row[dataColumn5.Ordinal] = (object)(short)0;
                        goto case 1;
                    case 13:
                        row[dataColumn4.Ordinal] = (object)(short)12;
                        row[dataColumn5.Ordinal] = (object)(short)3;
                        goto case 1;
                    case 14:
                        row[dataColumn4.Ordinal] = (object)(short)23;
                        row[dataColumn5.Ordinal] = (object)(short)3;
                        goto case 1;
                    case 15:
                        row[dataColumn4.Ordinal] = (object)(short)10;
                        row[dataColumn5.Ordinal] = (object)(short)0;
                        row[dataColumn12.Ordinal] = (object)true;
                        row[dataColumn14.Ordinal] = (object)true;
                        row[dataColumn9.Ordinal] = (object)false;
                        row[dataColumn10.Ordinal] = (object)true;
                        goto case 1;
                    case 18:
                        row[dataColumn4.Ordinal] = (object)(short)19;
                        row[dataColumn5.Ordinal] = (object)(short)4;
                        goto case 1;
                    case 19:
                        row[dataColumn4.Ordinal] = (object)(short)19;
                        row[dataColumn5.Ordinal] = (object)(short)0;
                        goto case 1;
                    case 21:
                        row[dataColumn12.Ordinal] = (object)true;
                        row[dataColumn11.Ordinal] = (object)true;
                        row[dataColumn4.Ordinal] = (object)(short)19;
                        row[dataColumn5.Ordinal] = (object)(short)0;
                        row[dataColumn14.Ordinal] = (object)true;
                        row[dataColumn9.Ordinal] = (object)false;
                        row[dataColumn10.Ordinal] = (object)true;
                        goto case 1;
                    case 22:
                        row[dataColumn14.Ordinal] = (object)true;
                        row[dataColumn4.Ordinal] = (object)(short)23;
                        row[dataColumn5.Ordinal] = (object)(short)3;
                        row[dataColumn9.Ordinal] = (object)false;
                        row[dataColumn10.Ordinal] = (object)true;
                        goto case 1;
                    case 28:
                        row[dataColumn3.Ordinal] = (object)1073741820;
                        row[dataColumn8.Ordinal] = (object)true;
                        row[dataColumn21.Ordinal] = (object)true;
                        goto case 1;
                    default:
                        throw new NotSupportedException("Unexpected type " + (object)aceFieldType + ".");
                }
            }

            schemaTable.AcceptChanges();
            return schemaTable;
        }

        public override int FieldCount => this.miFieldCount;

        public override string GetName(int iCol)
        {
            char[] pucName = new char[129];
            this.CheckOpen();
            this.CheckColumnIndex(iCol);
            ushort length = (ushort)pucName.Length;
            AdsException.CheckACE(ACE.AdsGetFieldName(this.mhCursor, (ushort)(iCol + 1), pucName, ref length));
            return new string(pucName, 0, (int)length);
        }

        public override string GetDataTypeName(int iCol)
        {
            this.CheckOpen();
            this.CheckColumnIndex(iCol);
            return AdsDataReader.ConvertACETypeToName((ushort)this.GetACEFieldType(iCol));
        }

        protected short GetACEFieldType(int iCol)
        {
            if (this.masACETypes == null)
            {
                this.masACETypes = new short[this.miFieldCount];
                for (int index = 0; index < this.miFieldCount; ++index)
                    this.masACETypes[index] = (short)0;
            }

            if (this.masACETypes[iCol] != (short)0)
                return this.masACETypes[iCol];
            ushort pusType;
            AdsException.CheckACE(ACE.AdsGetFieldType(this.mhCursor, (uint)(iCol + 1), out pusType));
            this.masACETypes[iCol] = (short)pusType;
            return (short)pusType;
        }

        public override Type GetFieldType(int iCol)
        {
            this.CheckOpen();
            this.CheckColumnIndex(iCol);
            return AdsDataReader.ConvertAceToSystemType((ushort)this.GetACEFieldType(iCol));
        }

        protected void CheckColumnIndex(int iCol)
        {
            if (iCol < 0 || iCol >= this.miFieldCount)
                throw new IndexOutOfRangeException("The column index is not valid.");
        }

        protected void CheckForNull(int iCol)
        {
            if (this.FieldIsEmpty(iCol))
                throw new AdsException("The value is Null.  This method or property cannot be called for Null values.");
        }

        protected virtual void CheckOpen()
        {
            if (!this.mbOpen)
                throw new InvalidOperationException("The reader must be open for this operation.");
        }

        protected virtual void CheckPositioned()
        {
            if (this.mbBOF || this.mbEOF)
                throw new InvalidOperationException("There is no current record.");
        }

        protected void CheckForGet(int iCol)
        {
            this.CheckOpen();
            this.CheckPositioned();
            this.CheckColumnIndex(iCol);
            this.ReadRecord();
            this.CheckForNull(iCol);
        }

        internal static string ConvertACETypeToName(ushort usAceType)
        {
            switch (usAceType)
            {
                case 1:
                    return "logical";
                case 2:
                    return "numeric";
                case 3:
                case 9:
                    return "date";
                case 4:
                    return "char";
                case 5:
                    return "memo";
                case 6:
                case 7:
                    return "blob";
                case 8:
                case 23:
                    return "varchar";
                case 10:
                    return "double";
                case 11:
                    return "integer";
                case 12:
                    return "short";
                case 13:
                    return "time";
                case 14:
                    return "timestamp";
                case 15:
                    return "autoinc";
                case 16:
                    return "raw";
                case 17:
                    return "curdouble";
                case 18:
                    return "money";
                case 19:
                    return "longint";
                case 20:
                    return "cichar";
                case 21:
                    return "rowversion";
                case 22:
                    return "modtime";
                case 24:
                    return "varbinary";
                case 26:
                    return "nchar";
                case 27:
                    return "nvarchar";
                case 28:
                    return "nmemo";
                case 29:
                    return "guid";
                default:
                    throw new AdsException("Unsupported type found.");
            }
        }

        internal static ushort ConvertACETypeNameToType(string strType)
        {
            switch (strType.ToLower().Trim())
            {
                case "logical":
                    return 1;
                case "char":
                case "character":
                    return 4;
                case "nchar":
                    return 26;
                case "memo":
                    return 5;
                case "nmemo":
                    return 28;
                case "varchar":
                case "varcharfox":
                    return 23;
                case "nvarchar":
                    return 27;
                case "cichar":
                case "cicharacter":
                    return 20;
                case "numeric":
                    return 2;
                case "date":
                    return 3;
                case "timestamp":
                    return 14;
                case "modtime":
                    return 22;
                case "binary":
                    return 6;
                case "raw":
                    return 16;
                case "varbinary":
                    return 24;
                case "double":
                    return 10;
                case "integer":
                    return 11;
                case "longint":
                    return 19;
                case "short":
                case "shortint":
                    return 12;
                case "time":
                    return 13;
                case "autoinc":
                    return 15;
                case "curdouble":
                    return 17;
                case "money":
                    return 18;
                case "rowversion":
                    return 21;
                case "guid":
                    return 29;
                default:
                    throw new ArgumentException("Unrecognized ACE type '" + strType + "'.");
            }
        }

        internal static Type ConvertAceToSystemType(ushort usAceType)
        {
            switch (usAceType)
            {
                case 1:
                    return Type.GetType("System.Boolean");
                case 2:
                    return Type.GetType("System.Decimal");
                case 3:
                case 9:
                    return Type.GetType("System.DateTime");
                case 4:
                case 8:
                case 20:
                case 23:
                case 26:
                case 27:
                    return Type.GetType("System.String");
                case 5:
                case 28:
                    return Type.GetType("System.String");
                case 6:
                case 7:
                    return Type.GetType("System.Byte[]");
                case 10:
                    return Type.GetType("System.Double");
                case 11:
                    return Type.GetType("System.Int32");
                case 12:
                    return Type.GetType("System.Int16");
                case 13:
                    return Type.GetType("System.TimeSpan");
                case 14:
                case 22:
                    return Type.GetType("System.DateTime");
                case 15:
                    return Type.GetType("System.Int32");
                case 16:
                case 24:
                    return Type.GetType("System.Byte[]");
                case 17:
                    return Type.GetType("System.Double");
                case 18:
                    return Type.GetType("System.Decimal");
                case 19:
                case 21:
                    return Type.GetType("System.Int64");
                case 29:
                    return Type.GetType("System.Guid");
                default:
                    throw new AdsException("AdsDataReader.GetType : Unsupported type found.");
            }
        }

        public override object GetValue(int iCol)
        {
            this.CheckOpen();
            this.CheckPositioned();
            this.ReadRecord();
            this.CheckColumnIndex(iCol);
            if (this.FieldIsEmpty(iCol))
                return (object)DBNull.Value;
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 1:
                    return (object)this.GetBoolean(iCol);
                case 2:
                    return (object)this.GetDecimal(iCol);
                case 3:
                case 9:
                case 14:
                case 22:
                    return (object)this.GetDateTime(iCol);
                case 4:
                case 5:
                case 8:
                case 20:
                case 23:
                case 26:
                case 27:
                case 28:
                    return (object)this.GetString(iCol);
                case 6:
                case 7:
                case 16:
                case 24:
                    return (object)this.GetBytes(iCol);
                case 10:
                    return (object)this.GetDouble(iCol);
                case 11:
                    return (object)this.GetInt32(iCol);
                case 12:
                    return (object)this.GetInt16(iCol);
                case 13:
                    return (object)this.GetTimeSpan(iCol);
                case 15:
                    return (object)this.GetInt32(iCol);
                case 17:
                    return (object)this.GetDouble(iCol);
                case 18:
                    return (object)this.GetDecimal(iCol);
                case 19:
                case 21:
                    return (object)this.GetInt64(iCol);
                case 29:
                    return (object)this.GetGuid(iCol);
                default:
                    throw new Exception("AdsDataReader.GetValue : Unsupported type.");
            }
        }

        public override int GetValues(object[] values)
        {
            int ordinal;
            for (ordinal = 0; ordinal < values.Length && ordinal < this.miFieldCount; ++ordinal)
                values[ordinal] = this.GetValue(ordinal);
            return ordinal;
        }

        public override int GetOrdinal(string strName)
        {
            if (strName == null)
                throw new ArgumentNullException("Value cannot be null.");
            this.CheckOpen();
            if (this.mFieldNames == null)
            {
                char[] pucName = new char[129];
                this.mFieldNames = CollectionsUtil.CreateCaseInsensitiveSortedList();
                this.mFieldNames.Capacity = this.miFieldCount;
                for (int index = 0; index < this.miFieldCount; ++index)
                {
                    ushort length = (ushort)pucName.Length;
                    AdsException.CheckACE(ACE.AdsGetFieldName(this.mhCursor, (ushort)(index + 1), pucName, ref length));
                    this.mFieldNames.Add((object)new string(pucName, 0, (int)length), (object)index);
                }
            }

            try
            {
                return (int)this.mFieldNames[(object)strName];
            }
            catch (Exception ex)
            {
                throw new IndexOutOfRangeException("Column " + strName + " not found.");
            }
        }

        public override object this[int i] => this.GetValue(i);

        public override object this[string strName] => this[this.GetOrdinal(strName)];

        public override bool GetBoolean(int iCol)
        {
            this.CheckForGet(iCol);
            if (this.GetACEFieldType(iCol) != (short)1)
                throw new InvalidCastException("Cannot convert field to Boolean.");
            bool boolean;
            switch (this.mabRecord[this.maiFieldOffset[iCol]])
            {
                case 1:
                case 84:
                case 89:
                case 116:
                case 121:
                    boolean = true;
                    break;
                default:
                    boolean = false;
                    break;
            }

            return boolean;
        }

        public override byte GetByte(int iCol)
        {
            int int32;
            try
            {
                int32 = this.GetInt32(iCol);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Cannot convert field to Byte.");
            }

            return int32 >= 0 && int32 <= (int)byte.MaxValue
                ? (byte)int32
                : throw new InvalidCastException("Cannot convert field to Byte.");
        }

        public override long GetBytes(
            int iCol,
            long lFieldOffset,
            byte[] buffer,
            int iBufferOffset,
            int iLength)
        {
            this.CheckForGet(iCol);
            if (lFieldOffset < 0L)
                return 0;
            if (iBufferOffset < 0)
                throw new ArgumentOutOfRangeException("The buffer offset (" + (object)iBufferOffset +
                                                      ") cannot be negative.");
            if (iLength < 0)
                throw new IndexOutOfRangeException("The number of bytes to read (" + (object)iLength +
                                                   ") cannot be negative.");
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 6:
                case 7:
                    uint aceDataLength = (uint)this.GetACEDataLength(iCol);
                    if (buffer == null)
                        return (long)aceDataLength;
                    if (iBufferOffset >= buffer.Length)
                        throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                    if (lFieldOffset >= (long)(int)aceDataLength)
                        return 0;
                    int length1 = (int)aceDataLength - (int)lFieldOffset;
                    if (length1 > iLength)
                        length1 = iLength;
                    if (length1 + iBufferOffset > buffer.Length)
                        throw new IndexOutOfRangeException(
                            "The buffer offset plus the amount to read does not fit in the output buffer.");
                    uint pulLen = (uint)length1;
                    if (iBufferOffset == 0)
                    {
                        AdsException.CheckACE(ACE.AdsGetBinary(this.mhCursor, (uint)(iCol + 1), (uint)lFieldOffset,
                            buffer,
                            ref pulLen));
                    }
                    else
                    {
                        byte[] numArray = new byte[length1];
                        AdsException.CheckACE(ACE.AdsGetBinary(this.mhCursor, (uint)(iCol + 1), (uint)lFieldOffset,
                            numArray,
                            ref pulLen));
                        Array.Copy((Array)numArray, 0, (Array)buffer, iBufferOffset, length1);
                    }

                    return (long)pulLen;
                case 16:
                case 24:
                    if (buffer == null)
                        return (long)this.GetACEDataLength(iCol);
                    if (iBufferOffset >= buffer.Length)
                        throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                    byte[] bytes = this.GetBytes(iCol);
                    if (lFieldOffset >= (long)bytes.Length)
                        return 0;
                    int length2 = bytes.Length - (int)lFieldOffset;
                    if (length2 > iLength)
                        length2 = iLength;
                    if (length2 + iBufferOffset > buffer.Length)
                        throw new IndexOutOfRangeException(
                            "The buffer offset plus the amount to read does not fit in the output buffer.");
                    Array.Copy((Array)bytes, (int)lFieldOffset, (Array)buffer, iBufferOffset, length2);
                    return (long)length2;
                default:
                    throw new InvalidCastException("Cannot convert field to byte[] array.");
            }
        }

        public override char GetChar(int i)
        {
            throw new NotImplementedException("AdsDataReader.GetChar");
        }

        public override long GetChars(
            int iCol,
            long lFieldOffset,
            char[] acBuffer,
            int iBufferOffset,
            int iLength)
        {
            this.CheckForGet(iCol);
            if (lFieldOffset < 0L)
                return 0;
            if (iBufferOffset < 0)
                throw new ArgumentOutOfRangeException("The buffer offset (" + (object)iBufferOffset +
                                                      ") cannot be negative.");
            if (iLength < 0)
                throw new IndexOutOfRangeException("The number of chars to read (" + (object)iLength +
                                                   ") cannot be negative.");
            ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
            switch (aceFieldType)
            {
                case 4:
                case 5:
                case 8:
                case 20:
                case 23:
                case 26:
                case 27:
                case 28:
                    uint aceDataLength = (uint)this.GetACEDataLength(iCol);
                    if (acBuffer == null)
                        return (long)aceDataLength;
                    if (iBufferOffset >= acBuffer.Length)
                        throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                    if (lFieldOffset >= (long)(int)aceDataLength)
                        return 0;
                    int chars = (int)aceDataLength - (int)lFieldOffset;
                    if (chars > iLength)
                        chars = iLength;
                    if (chars + iBufferOffset > acBuffer.Length)
                        throw new IndexOutOfRangeException(
                            "The buffer offset plus the amount to read does not fit in the output buffer.");
                    switch (aceFieldType)
                    {
                        case 4:
                        case 20:
                        case 23:
                            this.AdsEncoding.GetChars(this.mabRecord, (int)lFieldOffset + this.maiFieldOffset[iCol],
                                chars, acBuffer,
                                iBufferOffset);
                            break;
                        case 26:
                        case 27:
                            Encoding.Unicode.GetChars(this.mabRecord, (int)lFieldOffset * 2 + this.maiFieldOffset[iCol],
                                chars * 2,
                                acBuffer, iBufferOffset);
                            break;
                        case 28:
                            if (lFieldOffset == 0L && iBufferOffset == 0)
                            {
                                uint pulLen = (uint)chars;
                                uint stringW = ACE.AdsGetStringW(this.mhCursor, (uint)(iCol + 1), acBuffer, ref pulLen,
                                    (ushort)0);
                                switch (stringW)
                                {
                                    case 0:
                                    case 5005:
                                        break;
                                    default:
                                        AdsException.CheckACE(stringW);
                                        break;
                                }
                            }
                            else
                                goto default;

                            break;
                        default:
                            uint pulLen1 = aceDataLength + 1U;
                            char[] chArray = new char[pulLen1];
                            AdsException.CheckACE(ACE.AdsGetFieldW(this.mhCursor, (uint)(iCol + 1), chArray,
                                ref pulLen1, (ushort)0));
                            Array.Copy((Array)chArray, lFieldOffset, (Array)acBuffer, (long)iBufferOffset, (long)chars);
                            break;
                    }

                    return (long)chars;
                default:
                    throw new InvalidCastException("Cannot convert field to char[] array.");
            }
        }

        public override Guid GetGuid(int i)
        {
            byte[] bytes;
            try
            {
                bytes = this.GetBytes(i);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Cannot convert field to GUID.");
            }

            return bytes.Length == 16
                ? new Guid(bytes)
                : throw new InvalidCastException("Cannot convert field to GUID.");
        }

        public override short GetInt16(int iCol)
        {
            int int32;
            try
            {
                int32 = this.GetInt32(iCol);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Cannot convert field to Int16.");
            }

            return int32 >= (int)short.MinValue && int32 <= (int)short.MaxValue
                ? (short)int32
                : throw new InvalidCastException("Cannot convert field to Int16.");
        }

        public override int GetInt32(int iCol)
        {
            this.CheckForGet(iCol);
            int plValue;
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 11:
                case 15:
                    plValue = BitConverter.ToInt32(this.mabRecord, this.maiFieldOffset[iCol]);
                    break;
                case 12:
                    plValue = (int)BitConverter.ToInt16(this.mabRecord, this.maiFieldOffset[iCol]);
                    break;
                default:
                    uint ulRet = ACE.AdsGetLong(this.mhCursor, (uint)(iCol + 1), out plValue);
                    switch (ulRet)
                    {
                        case 5003:
                        case 5066:
                        case 5179:
                            throw new InvalidCastException("Cannot convert field to Int32.");
                        default:
                            AdsException.CheckACE(ulRet);
                            break;
                    }

                    break;
            }

            return plValue;
        }

        public override long GetInt64(int iCol)
        {
            long pqValue;
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 11:
                case 12:
                    pqValue = (long)this.GetInt32(iCol);
                    break;
                case 15:
                    pqValue = (long)(uint)this.GetInt32(iCol);
                    break;
                case 19:
                case 21:
                    this.CheckForGet(iCol);
                    pqValue = BitConverter.ToInt64(this.mabRecord, this.maiFieldOffset[iCol]);
                    break;
                default:
                    this.CheckForGet(iCol);
                    uint longLong = ACE.AdsGetLongLong(this.mhCursor, (uint)(iCol + 1), out pqValue);
                    if (longLong == 5066U)
                        throw new InvalidCastException("Cannot convert field to Int64.");
                    AdsException.CheckACE(longLong);
                    break;
            }

            return pqValue;
        }

        public override float GetFloat(int iCol)
        {
            double num;
            try
            {
                num = this.GetDouble(iCol);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Cannot convert field to Float.");
            }

            return num <= 3.4028234663852886E+38 && num >= -3.4028234663852886E+38
                ? (float)num
                : throw new InvalidCastException("Cannot convert field to Float.");
        }

        public override double GetDouble(int iCol)
        {
            this.CheckForGet(iCol);
            double pdValue;
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 10:
                case 17:
                    pdValue = BitConverter.ToDouble(this.mabRecord, this.maiFieldOffset[iCol]);
                    break;
                default:
                    uint ulRet = ACE.AdsGetDouble(this.mhCursor, (uint)(iCol + 1), out pdValue);
                    switch (ulRet)
                    {
                        case 5066:
                        case 5179:
                            throw new InvalidCastException("Cannot convert field to Double.");
                        default:
                            AdsException.CheckACE(ulRet);
                            break;
                    }

                    break;
            }

            return pdValue;
        }

        public override string GetString(int iCol)
        {
            bool trimTrailingSpaces = this.mConnection.TrimTrailingSpaces;
            return this.GetString(iCol, trimTrailingSpaces);
        }

        private Encoding AdsEncoding
        {
            get
            {
                if (this.mEncoding == null)
                {
                    uint pulProperty;
                    AdsException.CheckACE(ACE.AdsGetIntProperty(this.mhCursor, 1U, out pulProperty));
                    this.mEncoding = Encoding.GetEncoding((int)pulProperty);
                }

                return this.mEncoding;
            }
        }

        public string GetString(int iCol, bool bTrim)
        {
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 4:
                case 20:
                case 23:
                    this.CheckForGet(iCol);
                    int aceDataLength1 = this.GetACEDataLength(iCol);
                    if (bTrim)
                    {
                        while (aceDataLength1 > 0 &&
                               this.mabRecord[aceDataLength1 - 1 + this.maiFieldOffset[iCol]] == (byte)32)
                            --aceDataLength1;
                    }

                    return this.AdsEncoding.GetString(this.mabRecord, this.maiFieldOffset[iCol], aceDataLength1);
                case 26:
                case 27:
                    this.CheckForGet(iCol);
                    int aceDataLength2 = this.GetACEDataLength(iCol);
                    string str =
                        Encoding.Unicode.GetString(this.mabRecord, this.maiFieldOffset[iCol], aceDataLength2 * 2);
                    return bTrim ? str.TrimEnd((char[])null) : str;
                default:
                    ushort usOption = 0;
                    long chars;
                    try
                    {
                        chars = this.GetChars(iCol, 0L, (char[])null, 0, 0);
                    }
                    catch (InvalidCastException ex)
                    {
                        throw new InvalidCastException("Cannot convert field to String.");
                    }

                    uint pulLen = (uint)chars + 1U;
                    char[] pwcBuf = new char[pulLen];
                    if (bTrim)
                        usOption = (ushort)2;
                    uint fieldW = ACE.AdsGetFieldW(this.mhCursor, (uint)(iCol + 1), pwcBuf, ref pulLen, usOption);
                    if (fieldW == 5066U)
                        throw new InvalidCastException("Cannot convert field to String.");
                    AdsException.CheckACE(fieldW);
                    return new string(pwcBuf, 0, (int)pulLen);
            }
        }

        public override Decimal GetDecimal(int iCol)
        {
            char[] chArray1 = new char[33];
            this.CheckForGet(iCol);
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 2:
                    char[] chArray2 = new char[this.maiFieldLength[iCol]];
                    for (int index = 0; index < this.maiFieldLength[iCol]; ++index)
                        chArray2[index] = (char)this.mabRecord[index + this.maiFieldOffset[iCol]];
                    string str = new string(chArray2);
                    if (str.Trim() == "")
                        return 0M;
                    try
                    {
                        return Convert.ToDecimal(str, (IFormatProvider)CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch (FormatException ex)
                    {
                        throw new InvalidCastException(
                            "Cannot convert field to Decimal. The numeric field contains invalid data.");
                    }
                case 11:
                case 12:
                case 15:
                    int int32;
                    try
                    {
                        int32 = this.GetInt32(iCol);
                    }
                    catch (InvalidCastException ex)
                    {
                        throw new InvalidCastException("Cannot convert field to Decimal.");
                    }

                    return Convert.ToDecimal(int32);
                case 18:
                    return Decimal.FromOACurrency(BitConverter.ToInt64(this.mabRecord, this.maiFieldOffset[iCol]));
                case 19:
                case 21:
                    long int64;
                    try
                    {
                        int64 = this.GetInt64(iCol);
                    }
                    catch (InvalidCastException ex)
                    {
                        throw new InvalidCastException("Cannot convert field to Decimal.");
                    }

                    return Convert.ToDecimal(int64);
                default:
                    throw new InvalidCastException("Cannot convert field to Decimal.");
            }
        }

        protected int AxYearToDays(int iYear)
        {
            --iYear;
            return iYear * 365 + iYear / 4 - iYear / 100 + iYear / 400;
        }

        protected void AxMonthDay(int iYear, int iDays, out int iMonth, out int iDay)
        {
            short[] numArray = new short[13]
            {
                (short)0,
                (short)31,
                (short)59,
                (short)90,
                (short)120,
                (short)151,
                (short)181,
                (short)212,
                (short)243,
                (short)273,
                (short)304,
                (short)334,
                (short)365
            };
            short num = iYear % 4 == 0 && iYear % 100 != 0 || iYear % 400 == 0 ? (short)1 : (short)0;
            if (iDays <= 59)
                num = (short)0;
            for (short index = 1; index <= (short)12; ++index)
            {
                if (iDays <= (int)numArray[(int)index] + (int)num)
                {
                    iMonth = (int)index;
                    if (index <= (short)2)
                        num = (short)0;
                    iDay = iDays - (int)numArray[(int)index - 1] - (int)num;
                    return;
                }
            }

            iMonth = iDay = 0;
            throw new InvalidCastException("Cannot convert Julian day value to date.");
        }

        protected void ConvertJulianDay(int iJulian, out int iYear, out int iMonth, out int iDay)
        {
            iYear = iMonth = iDay = 0;
            if (iJulian == 0)
                return;
            int num1 = iJulian - 1721425;
            iYear = (int)((double)num1 / 365.2425 + 1.0);
            int iDays = num1 - this.AxYearToDays(iYear);
            if (iDays <= 0)
            {
                --iYear;
                iDays = num1 - this.AxYearToDays(iYear);
            }

            int num2 = iYear % 4 == 0 && iYear % 100 != 0 || iYear % 400 == 0 ? 366 : 365;
            if (iDays > num2)
            {
                ++iYear;
                iDays -= num2;
            }

            this.AxMonthDay(iYear, iDays, out iMonth, out iDay);
        }

        public override DateTime GetDateTime(int iCol)
        {
            int iYear = 0;
            int iMonth = 0;
            int iDay = 0;
            this.CheckForGet(iCol);
            ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
            switch (aceFieldType)
            {
                case 3:
                case 14:
                case 22:
                    if (this.msTableType == (short)3 || aceFieldType == (ushort)14)
                    {
                        this.ConvertJulianDay(BitConverter.ToInt32(this.mabRecord, this.maiFieldOffset[iCol]),
                            out iYear,
                            out iMonth, out iDay);
                    }
                    else
                    {
                        char[] chArray = new char[this.maiFieldLength[iCol]];
                        for (int index = 0; index < this.maiFieldLength[iCol]; ++index)
                            chArray[index] = (char)this.mabRecord[index + this.maiFieldOffset[iCol]];
                        string str = new string(chArray, 0, this.maiFieldLength[iCol]);
                        try
                        {
                            iYear = Convert.ToInt32(str.Substring(0, 4));
                            iMonth = Convert.ToInt32(str.Substring(4, 2));
                            iDay = Convert.ToInt32(str.Substring(6, 2));
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidCastException(
                                "Cannot convert field to DateTime.  The date value (" + str + ") is not valid.", ex);
                        }
                    }

                    DateTime dateTime;
                    try
                    {
                        dateTime = new DateTime(iYear, iMonth, iDay);
                    }
                    catch (Exception ex)
                    {
                        throw new AdsException(
                            "Unable to convert a date field or the date portion of a timestamp field to a DateTime object.",
                            ex);
                    }

                    if (aceFieldType == (ushort)14 || aceFieldType == (ushort)22)
                    {
                        int num = BitConverter.ToInt32(this.mabRecord, this.maiFieldOffset[iCol] + 4);
                        if (this.msTableType == (short)4)
                            num = 1000 * ((num + 500) / 1000);
                        dateTime = dateTime.AddMilliseconds((double)num);
                    }

                    return dateTime;
                default:
                    throw new InvalidCastException("Cannot convert field to DateTime.");
            }
        }

        protected bool FieldIsEmpty(int iCol)
        {
            ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
            if (!this.mConnection.DbfsUseNulls && this.msTableType != (short)3 && this.msTableType != (short)4 &&
                aceFieldType != (ushort)3)
                return false;
            ushort pbNull;
            if (this.msTableType == (short)3)
            {
                switch (aceFieldType)
                {
                    case 1:
                        return this.mabRecord[this.maiFieldOffset[iCol]] == (byte)32;
                    case 2:
                    case 3:
                    case 4:
                    case 9:
                    case 16:
                    case 20:
                    case 26:
                    case 29:
                        int num1 = 1;
                        if (aceFieldType == (ushort)26)
                            num1 = 2;
                        for (int index = 0; index < this.maiFieldLength[iCol] * num1; ++index)
                        {
                            if (this.mabRecord[index + this.maiFieldOffset[iCol]] != (byte)0)
                                return false;
                        }

                        return true;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 23:
                    case 24:
                    case 27:
                    case 28:
                        break;
                    case 10:
                    case 17:
                        for (int index = 0; index < AdsDataReader.mabDblNull.Length; ++index)
                        {
                            if ((int)AdsDataReader.mabDblNull[index] !=
                                (int)this.mabRecord[index + this.maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 11:
                        for (int index = 0; index < AdsDataReader.mabInt32Null.Length; ++index)
                        {
                            if ((int)AdsDataReader.mabInt32Null[index] !=
                                (int)this.mabRecord[index + this.maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 12:
                        for (int index = 0; index < AdsDataReader.mabInt16Null.Length; ++index)
                        {
                            if ((int)AdsDataReader.mabInt16Null[index] !=
                                (int)this.mabRecord[index + this.maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 13:
                        for (int index = 0; index < AdsDataReader.mabTimeNull.Length; ++index)
                        {
                            if ((int)AdsDataReader.mabTimeNull[index] !=
                                (int)this.mabRecord[index + this.maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 14:
                    case 22:
                        for (int index = 0; index < 4; ++index)
                        {
                            if (this.mabRecord[index + this.maiFieldOffset[iCol]] != (byte)0)
                                return false;
                        }

                        return true;
                    case 15:
                        return false;
                    case 18:
                    case 19:
                    case 21:
                        for (int index = 0; index < AdsDataReader.mabInt64Null.Length; ++index)
                        {
                            if ((int)AdsDataReader.mabInt64Null[index] !=
                                (int)this.mabRecord[index + this.maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    default:
                        throw new Exception("AdsDataReader.FieldIsEmpty : Unsupported ADT type.");
                }
            }
            else if (this.msTableType == (short)4)
            {
                if (aceFieldType == (ushort)3 || aceFieldType == (ushort)14)
                {
                    byte num2 = aceFieldType != (ushort)3 ? (byte)0 : (byte)32;
                    pbNull = (ushort)1;
                    for (int index = 0; index < this.maiFieldLength[iCol]; ++index)
                    {
                        if ((int)this.mabRecord[index + this.maiFieldOffset[iCol]] != (int)num2)
                        {
                            pbNull = (ushort)0;
                            break;
                        }
                    }

                    if (pbNull == (ushort)1)
                        return true;
                }

                if (this.masVFPNullable[iCol] == (short)0)
                    return false;
            }
            else
            {
                switch (aceFieldType)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        for (int index = 0; index < this.maiFieldLength[iCol]; ++index)
                        {
                            if (this.mabRecord[index + this.maiFieldOffset[iCol]] != (byte)32)
                                return false;
                        }

                        return true;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 26:
                    case 27:
                    case 28:
                    case 51:
                    case 52:
                        ushort pbEmpty;
                        AdsException.CheckACE(ACE.AdsIsEmpty(this.mhCursor, (uint)(iCol + 1), out pbEmpty));
                        return pbEmpty == (ushort)1;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 16:
                    case 17:
                    case 29:
                        for (int index = 0; index < this.maiFieldLength[iCol]; ++index)
                        {
                            if (this.mabRecord[index + this.maiFieldOffset[iCol]] != (byte)0)
                                return false;
                        }

                        return true;
                    default:
                        throw new Exception("AdsDataReader.FieldIsEmpty : Unsupported DBF type.");
                }
            }

            AdsException.CheckACE(ACE.AdsIsNull(this.mhCursor, (uint)(iCol + 1), out pbNull));
            return pbNull == (ushort)1;
        }

        public override bool IsDBNull(int iCol)
        {
            this.CheckOpen();
            this.CheckPositioned();
            this.CheckColumnIndex(iCol);
            this.ReadRecord();
            return this.FieldIsEmpty(iCol);
        }

        public byte[] GetBytes(int iCol)
        {
            this.CheckForGet(iCol);
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 6:
                case 7:
                    uint pulLength;
                    AdsException.CheckACE(ACE.AdsGetBinaryLength(this.mhCursor, (uint)(iCol + 1), out pulLength));
                    byte[] pucBuf = new byte[pulLength];
                    uint pulLen = pulLength;
                    AdsException.CheckACE(ACE.AdsGetBinary(this.mhCursor, (uint)(iCol + 1), 0U, pucBuf, ref pulLen));
                    return pucBuf;
                case 16:
                case 24:
                case 29:
                    byte[] bytes = new byte[this.GetACEDataLength(iCol)];
                    for (int index = 0; index < bytes.Length; ++index)
                        bytes[index] = this.mabRecord[index + this.maiFieldOffset[iCol]];
                    return bytes;
                default:
                    throw new InvalidCastException("Cannot convert field to byte[] array.");
            }
        }

        public TimeSpan GetTimeSpan(int iCol)
        {
            this.CheckForGet(iCol);
            if (this.GetACEFieldType(iCol) != (short)13)
                throw new InvalidCastException("Cannot convert field to TimeSpan.");
            return new TimeSpan(0, 0, 0, 0, BitConverter.ToInt32(this.mabRecord, this.maiFieldOffset[iCol]));
        }

        public short RecordCache
        {
            get => this.msCacheSize;
            set
            {
                this.CheckOpen();
                if (!this.mbOpen || !(this.mhCursor != IntPtr.Zero) || (int)value == (int)this.msCacheSize)
                    return;
                AdsException.CheckACE(ACE.AdsCacheRecords(this.mhCursor, (ushort)value));
                this.msCacheSize = value;
                if (this.msCacheSize != (short)0)
                    return;
                this.msCacheSize = (short)1;
            }
        }

        public bool IsStatic => this.mbIsStatic;

        public bool IsDate(int iCol)
        {
            this.CheckOpen();
            this.CheckColumnIndex(iCol);
            ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
            return aceFieldType == (ushort)3 || aceFieldType == (ushort)9;
        }

        public bool IsDateTime(int iCol)
        {
            this.CheckOpen();
            this.CheckColumnIndex(iCol);
            ushort aceFieldType = (ushort)this.GetACEFieldType(iCol);
            return aceFieldType == (ushort)14 || aceFieldType == (ushort)22;
        }

        protected int GetACEFieldLength(int iCol)
        {
            if (this.maiFieldLength != null)
                return this.maiFieldLength[iCol];
            int aceFieldType = (int)this.GetACEFieldType(iCol);
            uint pulLength;
            AdsException.CheckACE(ACE.AdsGetFieldLength(this.mhCursor, (uint)(iCol + 1), out pulLength));
            return (int)pulLength;
        }

        protected int GetACEDataLength(int iCol)
        {
            switch ((ushort)this.GetACEFieldType(iCol))
            {
                case 5:
                case 6:
                case 7:
                case 8:
                case 23:
                case 24:
                case 27:
                case 28:
                    uint pulLength;
                    AdsException.CheckACE(ACE.AdsGetDataLength(this.mhCursor, (uint)(iCol + 1), 0U, out pulLength));
                    return (int)pulLength;
                default:
                    return this.GetACEFieldLength(iCol);
            }
        }

        public int GetDataLength(int iCol)
        {
            this.CheckOpen();
            this.CheckPositioned();
            this.CheckColumnIndex(iCol);
            return this.GetACEDataLength(iCol);
        }

        public override IEnumerator GetEnumerator()
        {
            return (IEnumerator)new DbEnumerator((IDataReader)this);
        }

        public override bool HasRows
        {
            get
            {
                this.CheckOpen();
                return this.mbHasRows;
            }
        }
    }
}