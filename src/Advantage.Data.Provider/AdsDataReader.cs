using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using AdvantageClientEngine;

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
            32,
            0,
            0,
            0,
            0,
            0,
            0,
            128
        };

        protected static byte[] mabInt32Null = new byte[4]
        {
            0,
            0,
            0,
            128
        };

        protected static byte[] mabInt16Null = new byte[2]
        {
            0,
            128
        };

        protected static byte[] mabInt64Null = new byte[8]
        {
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            128
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
            mbOpen = true;
            mhCursor = hCursor;
            mConnection = adsConn;
            mCommand = adsCmd;
            meBehavior = eBehavior;
            miRecordsAffected = iRecsAffected;
            if (!(mhCursor != IntPtr.Zero))
                return;
            ushort pusCount;
            AdsException.CheckACE(ACE.AdsGetNumFields(mhCursor, out pusCount));
            miFieldCount = pusCount;
            if ((meBehavior & CommandBehavior.SingleRow) != CommandBehavior.SingleRow)
                RecordCache = 100;
            ushort pusType1;
            AdsException.CheckACE(ACE.AdsGetHandleType(mhCursor, out pusType1));
            mbIsCursor = pusType1 == 5;
            if (!mbIsCursor)
            {
                mbIsStatic = false;
            }
            else
            {
                byte IsLive;
                AdsException.CheckACE(ACEUNPUB.AdsSqlPeekStatement(mhCursor, out IsLive));
                mbIsStatic = IsLive == 0;
            }

            uint pulOptions;
            AdsException.CheckACE(ACE.AdsGetTableOpenOptions(mhCursor, out pulOptions));
            miOptions = (int)pulOptions;
            ushort pusType2;
            AdsException.CheckACE(ACE.AdsGetTableType(mhCursor, out pusType2));
            msTableType = (short)pusType2;
            if (msTableType != 3)
            {
                ushort pusCharType;
                AdsException.CheckACE(ACE.AdsGetTableCharType(mhCursor, out pusCharType));
                if (pusCharType == 2)
                    mbIsOEM = true;
            }

            ushort pbEof;
            if (ACE.AdsAtEOF(mhCursor, out pbEof) != 0U || pbEof != 0)
                return;
            mbHasRows = true;
        }

        ~AdsDataReader() => Dispose(false);

        protected override void Dispose(bool bDisposing)
        {
            if (mbDisposed)
                return;
            lock (this)
            {
                if (mbDisposed)
                    return;
                if (!bDisposing)
                    mConnection = null;
                Close();
                mbDisposed = true;
            }
        }

        internal void SetBOF()
        {
            mbBOF = true;
            mbEOF = false;
        }

        public override void Close()
        {
            if (mhCursor != IntPtr.Zero)
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    var num = (int)ACE.AdsCloseTable(mhCursor);
                }

                mhCursor = IntPtr.Zero;
                mbOpen = false;
                mbBOF = mbEOF = true;
                miFieldCount = 0;
            }

            if (mConnection == null)
                return;
            mConnection.Busy = false;
            if ((meBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
                mConnection.Close();
            mConnection = null;
        }

        public override int Depth => 0;

        public override bool IsClosed => !mbOpen;

        public override int RecordsAffected => miRecordsAffected;

        public override bool NextResult() => false;

        public override bool Read() => Read(mhCursor);

        protected bool Read(IntPtr hCursor)
        {
            if (hCursor == IntPtr.Zero)
                return false;
            InvalidateRecord();
            if ((meBehavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly || !mbOpen ||
                mbEOF)
                return false;
            if (mbBOF)
            {
                AdsException.CheckACE(ACE.AdsGotoTop(hCursor));
            }
            else
            {
                if ((meBehavior & CommandBehavior.SingleRow) == CommandBehavior.SingleRow)
                    return false;
                AdsException.CheckACE(ACE.AdsSkip(hCursor, 1));
            }

            ushort pbEof;
            AdsException.CheckACE(ACE.AdsAtEOF(mhCursor, out pbEof));
            if (pbEof != 0)
                mbEOF = true;
            else
                mbBOF = false;
            return !mbEOF;
        }

        private void ReadRecord()
        {
            if (mbRecordValid)
                return;
            if (mabRecord == null)
            {
                uint pulLength = 0;
                AdsException.CheckACE(ACE.AdsGetRecordLength(mhCursor, out pulLength));
                mabRecord = new byte[pulLength];
            }

            if (maiFieldOffset == null)
            {
                maiFieldOffset = new int[miFieldCount];
                maiFieldLength = new int[miFieldCount];
                if (msTableType == 4)
                {
                    masVFPNullable = new short[miFieldCount];
                    masVFPNoCPTrans = new short[miFieldCount];
                }

                for (var iCol = 0; iCol < miFieldCount; ++iCol)
                {
                    uint pulOffset;
                    AdsException.CheckACE(ACE.AdsGetFieldOffset(mhCursor, (uint)(iCol + 1), out pulOffset));
                    int aceFieldType = GetACEFieldType(iCol);
                    uint pulLength;
                    AdsException.CheckACE(ACE.AdsGetFieldLength(mhCursor, (uint)(iCol + 1), out pulLength));
                    maiFieldOffset[iCol] = (int)pulOffset;
                    maiFieldLength[iCol] = (int)pulLength;
                    if (msTableType == 4)
                    {
                        ushort num;
                        AdsException.CheckACE(ACE.AdsIsNullable(mhCursor, (uint)(iCol + 1), out num));
                        masVFPNullable[iCol] = (short)num;
                        AdsException.CheckACE(ACE.AdsIsFieldBinary(mhCursor, (uint)(iCol + 1), out num));
                        masVFPNoCPTrans[iCol] = (short)num;
                    }
                }
            }

            var length = (uint)mabRecord.Length;
            AdsException.CheckACE(ACE.AdsGetRecord(mhCursor, mabRecord, ref length));
            mbRecordValid = true;
        }

        protected void InvalidateRecord() => mbRecordValid = false;

        protected ArrayList GetKeyColumns()
        {
            var pucKeyColumn = new char[1024];
            var length = (ushort)pucKeyColumn.Length;
            var keyColumn = ACE.AdsGetKeyColumn(mhCursor, pucKeyColumn, ref length);
            if (keyColumn == 5005U)
            {
                pucKeyColumn = new char[length];
                keyColumn = ACE.AdsGetKeyColumn(mhCursor, pucKeyColumn, ref length);
            }

            if (keyColumn == 5041U)
                return null;
            AdsException.CheckACE(keyColumn);
            var keyColumns =
                new ArrayList(new string(pucKeyColumn, 0, length).ToUpper().Split(';'));
            if (keyColumns.Count > 0 && string.Compare((string)keyColumns[keyColumns.Count - 1], string.Empty) == 0)
                keyColumns.RemoveAt(keyColumns.Count - 1);
            return keyColumns;
        }

        public override DataTable GetSchemaTable()
        {
            var pucName1 = new char[129];
            var pucName2 = new char[2 * Math.Max((ushort)260, (ushort)65534) + 1];
            var schemaTable = new DataTable("SchemaTable");
            CheckOpen();
            if (mhCursor == IntPtr.Zero)
                return null;
            var dataColumn1 = schemaTable.Columns.Add("ColumnName", Type.GetType("System.String"));
            var dataColumn2 = schemaTable.Columns.Add("ColumnOrdinal", Type.GetType("System.Int32"));
            var dataColumn3 = schemaTable.Columns.Add("ColumnSize", Type.GetType("System.Int32"));
            var dataColumn4 = schemaTable.Columns.Add("NumericPrecision", Type.GetType("System.Int16"));
            var dataColumn5 = schemaTable.Columns.Add("NumericScale", Type.GetType("System.Int16"));
            var dataColumn6 = schemaTable.Columns.Add("DataType", Type.GetType("System.Type"));
            var dataColumn7 = schemaTable.Columns.Add("ProviderType", Type.GetType("System.Int32"));
            var dataColumn8 = schemaTable.Columns.Add("IsLong", Type.GetType("System.Boolean"));
            var dataColumn9 = schemaTable.Columns.Add("AllowDBNull", Type.GetType("System.Boolean"));
            var dataColumn10 = schemaTable.Columns.Add("IsReadOnly", Type.GetType("System.Boolean"));
            var dataColumn11 = schemaTable.Columns.Add("IsRowVersion", Type.GetType("System.Boolean"));
            var dataColumn12 = schemaTable.Columns.Add("IsUnique", Type.GetType("System.Boolean"));
            var dataColumn13 = schemaTable.Columns.Add("IsKey", Type.GetType("System.Boolean"));
            var dataColumn14 = schemaTable.Columns.Add("IsAutoIncrement", Type.GetType("System.Boolean"));
            var dataColumn15 = schemaTable.Columns.Add("BaseSchemaName", Type.GetType("System.String"));
            var dataColumn16 = schemaTable.Columns.Add("BaseCatalogName", Type.GetType("System.String"));
            var dataColumn17 = schemaTable.Columns.Add("BaseTableName", Type.GetType("System.String"));
            var dataColumn18 = schemaTable.Columns.Add("BaseColumnName", Type.GetType("System.String"));
            var dataColumn19 = schemaTable.Columns.Add("IsAliased", Type.GetType("System.Boolean"));
            var dataColumn20 = schemaTable.Columns.Add("IsKeyColumn", Type.GetType("System.Boolean"));
            var dataColumn21 = schemaTable.Columns.Add("CaseSensitive", Type.GetType("System.Boolean"));
            var dataColumn22 = schemaTable.Columns.Add("ProviderTypeName", Type.GetType("System.String"));
            var keyColumns = GetKeyColumns();
            string pucTableName;
            if (mbIsCursor && mbIsStatic)
            {
                pucTableName = null;
            }
            else
            {
                var length = (ushort)pucName2.Length;
                AdsException.CheckACE(ACE.AdsGetTableFilename(mhCursor, 1, pucName2, ref length));
                pucTableName = new string(pucName2, 0, length);
            }

            string str1;
            if (mbIsCursor && mbIsStatic)
            {
                str1 = null;
            }
            else
            {
                var length = (ushort)pucName2.Length;
                if (ACE.AdsGetTableFilename(mhCursor, 4, pucName2, ref length) != 0U)
                {
                    str1 = null;
                }
                else
                {
                    var match = new Regex("^:.+::").Match(new string(pucName2, 0, length));
                    str1 = !match.Success ? null : match.Value.Substring(1, match.Length - 3);
                }
            }

            for (var iCol = 0; iCol < miFieldCount; ++iCol)
            {
                var row = schemaTable.NewRow();
                var aceFieldType = (ushort)GetACEFieldType(iCol);
                var length = (ushort)pucName1.Length;
                AdsException.CheckACE(ACE.AdsGetFieldName(mhCursor, (ushort)(iCol + 1), pucName1, ref length));
                var str2 = new string(pucName1, 0, length);
                var strB = str2;
                ushort pusBaseFieldNum;
                if (ACEUNPUB.AdsGetBaseFieldNum(mhCursor, str2, out pusBaseFieldNum) == 0U)
                {
                    AdsException.CheckACE(ACEUNPUB.AdsSetBaseTableAccess(mhCursor, 1));
                    length = (ushort)pucName1.Length;
                    var fieldName = ACE.AdsGetFieldName(mhCursor, pusBaseFieldNum, pucName1, ref length);
                    if (fieldName != 0U)
                    {
                        var adsException = new AdsException(fieldName);
                        ACEUNPUB.AdsSetBaseTableAccess(mhCursor, 0);
                        throw adsException;
                    }

                    strB = new string(pucName1, 0, length);
                    AdsException.CheckACE(ACEUNPUB.AdsSetBaseTableAccess(mhCursor, 0));
                }

                row[dataColumn1.Ordinal] = str2;
                row[dataColumn2.Ordinal] = iCol;
                var aceFieldLength = GetACEFieldLength(iCol);
                row[dataColumn3.Ordinal] = aceFieldLength;
                row[dataColumn4.Ordinal] = DBNull.Value;
                row[dataColumn5.Ordinal] = DBNull.Value;
                row[dataColumn6.Ordinal] = ConvertAceToSystemType(aceFieldType);
                row[dataColumn7.Ordinal] = (int)aceFieldType;
                row[dataColumn22.Ordinal] = ConvertACETypeToName(aceFieldType);
                row[dataColumn8.Ordinal] = false;
                row[dataColumn9.Ordinal] = true;
                if (mConnection.IsDictionaryConn)
                {
                    ushort pusPropertyLen = 2;
                    ushort pusProperty = 0;
                    if (ACE.AdsDDGetFieldProperty(mConnection.Handle, pucTableName, str2, 301,
                            ref pusProperty,
                            ref pusPropertyLen) == 0U && pusProperty == 0)
                        row[dataColumn9.Ordinal] = false;
                }

                row[dataColumn10.Ordinal] = false;
                row[dataColumn11.Ordinal] = false;
                row[dataColumn12.Ordinal] =
                    keyColumns == null || keyColumns.Count != 1 ||
                    string.Compare((string)keyColumns[0], str2, true) != 0
                        ? false
                        : (object)true;
                row[dataColumn13.Ordinal] = keyColumns == null || !keyColumns.Contains(str2.ToUpper())
                    ? false
                    : (object)true;
                row[dataColumn20.Ordinal] = row[dataColumn13.Ordinal];
                row[dataColumn14.Ordinal] = false;
                row[dataColumn15.Ordinal] = DBNull.Value;
                row[dataColumn16.Ordinal] = str1;
                row[dataColumn17.Ordinal] = pucTableName;
                row[dataColumn18.Ordinal] = !mbIsCursor || !mbIsStatic ? strB : DBNull.Value;
                row[dataColumn19.Ordinal] = string.Compare(str2, strB, true) == 0 ? false : (object)true;
                row[dataColumn21.Ordinal] = false;
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
                        AdsException.CheckACE(ACE.AdsGetFieldDecimals(mhCursor, (ushort)(iCol + 1),
                            out pusDecimals));
                        row[dataColumn4.Ordinal] = (short)aceFieldLength;
                        row[dataColumn5.Ordinal] = (short)pusDecimals;
                        goto case 1;
                    case 3:
                    case 9:
                        row[dataColumn4.Ordinal] = (short)10;
                        row[dataColumn5.Ordinal] = (short)0;
                        goto case 1;
                    case 4:
                    case 23:
                    case 26:
                    case 27:
                        row[dataColumn21.Ordinal] = true;
                        goto case 1;
                    case 5:
                        row[dataColumn3.Ordinal] = 2147483640;
                        row[dataColumn8.Ordinal] = true;
                        row[dataColumn21.Ordinal] = true;
                        goto case 1;
                    case 6:
                    case 7:
                        row[dataColumn3.Ordinal] = int.MaxValue;
                        row[dataColumn8.Ordinal] = true;
                        goto case 1;
                    case 8:
                        row[dataColumn3.Ordinal] = 64000;
                        row[dataColumn21.Ordinal] = true;
                        goto case 1;
                    case 10:
                    case 17:
                        row[dataColumn4.Ordinal] = (short)15;
                        row[dataColumn5.Ordinal] = (short)0;
                        goto case 1;
                    case 11:
                        row[dataColumn4.Ordinal] = (short)10;
                        row[dataColumn5.Ordinal] = (short)0;
                        goto case 1;
                    case 12:
                        row[dataColumn4.Ordinal] = (short)5;
                        row[dataColumn5.Ordinal] = (short)0;
                        goto case 1;
                    case 13:
                        row[dataColumn4.Ordinal] = (short)12;
                        row[dataColumn5.Ordinal] = (short)3;
                        goto case 1;
                    case 14:
                        row[dataColumn4.Ordinal] = (short)23;
                        row[dataColumn5.Ordinal] = (short)3;
                        goto case 1;
                    case 15:
                        row[dataColumn4.Ordinal] = (short)10;
                        row[dataColumn5.Ordinal] = (short)0;
                        row[dataColumn12.Ordinal] = true;
                        row[dataColumn14.Ordinal] = true;
                        row[dataColumn9.Ordinal] = false;
                        row[dataColumn10.Ordinal] = true;
                        goto case 1;
                    case 18:
                        row[dataColumn4.Ordinal] = (short)19;
                        row[dataColumn5.Ordinal] = (short)4;
                        goto case 1;
                    case 19:
                        row[dataColumn4.Ordinal] = (short)19;
                        row[dataColumn5.Ordinal] = (short)0;
                        goto case 1;
                    case 21:
                        row[dataColumn12.Ordinal] = true;
                        row[dataColumn11.Ordinal] = true;
                        row[dataColumn4.Ordinal] = (short)19;
                        row[dataColumn5.Ordinal] = (short)0;
                        row[dataColumn14.Ordinal] = true;
                        row[dataColumn9.Ordinal] = false;
                        row[dataColumn10.Ordinal] = true;
                        goto case 1;
                    case 22:
                        row[dataColumn14.Ordinal] = true;
                        row[dataColumn4.Ordinal] = (short)23;
                        row[dataColumn5.Ordinal] = (short)3;
                        row[dataColumn9.Ordinal] = false;
                        row[dataColumn10.Ordinal] = true;
                        goto case 1;
                    case 28:
                        row[dataColumn3.Ordinal] = 1073741820;
                        row[dataColumn8.Ordinal] = true;
                        row[dataColumn21.Ordinal] = true;
                        goto case 1;
                    default:
                        throw new NotSupportedException("Unexpected type " + aceFieldType + ".");
                }
            }

            schemaTable.AcceptChanges();
            return schemaTable;
        }

        public override int FieldCount => miFieldCount;

        public override string GetName(int iCol)
        {
            var pucName = new char[129];
            CheckOpen();
            CheckColumnIndex(iCol);
            var length = (ushort)pucName.Length;
            AdsException.CheckACE(ACE.AdsGetFieldName(mhCursor, (ushort)(iCol + 1), pucName, ref length));
            return new string(pucName, 0, length);
        }

        public override string GetDataTypeName(int iCol)
        {
            CheckOpen();
            CheckColumnIndex(iCol);
            return ConvertACETypeToName((ushort)GetACEFieldType(iCol));
        }

        protected short GetACEFieldType(int iCol)
        {
            if (masACETypes == null)
            {
                masACETypes = new short[miFieldCount];
                for (var index = 0; index < miFieldCount; ++index)
                    masACETypes[index] = 0;
            }

            if (masACETypes[iCol] != 0)
                return masACETypes[iCol];
            ushort pusType;
            AdsException.CheckACE(ACE.AdsGetFieldType(mhCursor, (uint)(iCol + 1), out pusType));
            masACETypes[iCol] = (short)pusType;
            return (short)pusType;
        }

        public override Type GetFieldType(int iCol)
        {
            CheckOpen();
            CheckColumnIndex(iCol);
            return ConvertAceToSystemType((ushort)GetACEFieldType(iCol));
        }

        protected void CheckColumnIndex(int iCol)
        {
            if (iCol < 0 || iCol >= miFieldCount)
                throw new IndexOutOfRangeException("The column index is not valid.");
        }

        protected void CheckForNull(int iCol)
        {
            if (FieldIsEmpty(iCol))
                throw new AdsException("The value is Null.  This method or property cannot be called for Null values.");
        }

        protected virtual void CheckOpen()
        {
            if (!mbOpen)
                throw new InvalidOperationException("The reader must be open for this operation.");
        }

        protected virtual void CheckPositioned()
        {
            if (mbBOF || mbEOF)
                throw new InvalidOperationException("There is no current record.");
        }

        protected void CheckForGet(int iCol)
        {
            CheckOpen();
            CheckPositioned();
            CheckColumnIndex(iCol);
            ReadRecord();
            CheckForNull(iCol);
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
            CheckOpen();
            CheckPositioned();
            ReadRecord();
            CheckColumnIndex(iCol);
            if (FieldIsEmpty(iCol))
                return DBNull.Value;
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 1:
                    return GetBoolean(iCol);
                case 2:
                    return GetDecimal(iCol);
                case 3:
                case 9:
                case 14:
                case 22:
                    return GetDateTime(iCol);
                case 4:
                case 5:
                case 8:
                case 20:
                case 23:
                case 26:
                case 27:
                case 28:
                    return GetString(iCol);
                case 6:
                case 7:
                case 16:
                case 24:
                    return GetBytes(iCol);
                case 10:
                    return GetDouble(iCol);
                case 11:
                    return GetInt32(iCol);
                case 12:
                    return GetInt16(iCol);
                case 13:
                    return GetTimeSpan(iCol);
                case 15:
                    return GetInt32(iCol);
                case 17:
                    return GetDouble(iCol);
                case 18:
                    return GetDecimal(iCol);
                case 19:
                case 21:
                    return GetInt64(iCol);
                case 29:
                    return GetGuid(iCol);
                default:
                    throw new Exception("AdsDataReader.GetValue : Unsupported type.");
            }
        }

        public override int GetValues(object[] values)
        {
            int ordinal;
            for (ordinal = 0; ordinal < values.Length && ordinal < miFieldCount; ++ordinal)
                values[ordinal] = GetValue(ordinal);
            return ordinal;
        }

        public override int GetOrdinal(string strName)
        {
            if (strName == null)
                throw new ArgumentNullException("Value cannot be null.");
            CheckOpen();
            if (mFieldNames == null)
            {
                var pucName = new char[129];
                mFieldNames = CollectionsUtil.CreateCaseInsensitiveSortedList();
                mFieldNames.Capacity = miFieldCount;
                for (var index = 0; index < miFieldCount; ++index)
                {
                    var length = (ushort)pucName.Length;
                    AdsException.CheckACE(ACE.AdsGetFieldName(mhCursor, (ushort)(index + 1), pucName, ref length));
                    mFieldNames.Add(new string(pucName, 0, length), index);
                }
            }

            try
            {
                return (int)mFieldNames[strName];
            }
            catch (Exception ex)
            {
                throw new IndexOutOfRangeException("Column " + strName + " not found.");
            }
        }

        public override object this[int i] => GetValue(i);

        public override object this[string strName] => this[GetOrdinal(strName)];

        public override bool GetBoolean(int iCol)
        {
            CheckForGet(iCol);
            if (GetACEFieldType(iCol) != 1)
                throw new InvalidCastException("Cannot convert field to Boolean.");
            bool boolean;
            switch (mabRecord[maiFieldOffset[iCol]])
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
                int32 = GetInt32(iCol);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Cannot convert field to Byte.");
            }

            return int32 >= 0 && int32 <= byte.MaxValue
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
            CheckForGet(iCol);
            if (lFieldOffset < 0L)
                return 0;
            if (iBufferOffset < 0)
                throw new ArgumentOutOfRangeException("The buffer offset (" + iBufferOffset +
                                                      ") cannot be negative.");
            if (iLength < 0)
                throw new IndexOutOfRangeException("The number of bytes to read (" + iLength +
                                                   ") cannot be negative.");
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 6:
                case 7:
                    var aceDataLength = (uint)GetACEDataLength(iCol);
                    if (buffer == null)
                        return aceDataLength;
                    if (iBufferOffset >= buffer.Length)
                        throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                    if (lFieldOffset >= (int)aceDataLength)
                        return 0;
                    var length1 = (int)aceDataLength - (int)lFieldOffset;
                    if (length1 > iLength)
                        length1 = iLength;
                    if (length1 + iBufferOffset > buffer.Length)
                        throw new IndexOutOfRangeException(
                            "The buffer offset plus the amount to read does not fit in the output buffer.");
                    var pulLen = (uint)length1;
                    if (iBufferOffset == 0)
                    {
                        AdsException.CheckACE(ACE.AdsGetBinary(mhCursor, (uint)(iCol + 1), (uint)lFieldOffset,
                            buffer,
                            ref pulLen));
                    }
                    else
                    {
                        var numArray = new byte[length1];
                        AdsException.CheckACE(ACE.AdsGetBinary(mhCursor, (uint)(iCol + 1), (uint)lFieldOffset,
                            numArray,
                            ref pulLen));
                        Array.Copy(numArray, 0, buffer, iBufferOffset, length1);
                    }

                    return pulLen;
                case 16:
                case 24:
                    if (buffer == null)
                        return GetACEDataLength(iCol);
                    if (iBufferOffset >= buffer.Length)
                        throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                    var bytes = GetBytes(iCol);
                    if (lFieldOffset >= bytes.Length)
                        return 0;
                    var length2 = bytes.Length - (int)lFieldOffset;
                    if (length2 > iLength)
                        length2 = iLength;
                    if (length2 + iBufferOffset > buffer.Length)
                        throw new IndexOutOfRangeException(
                            "The buffer offset plus the amount to read does not fit in the output buffer.");
                    Array.Copy(bytes, (int)lFieldOffset, buffer, iBufferOffset, length2);
                    return length2;
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
            CheckForGet(iCol);
            if (lFieldOffset < 0L)
                return 0;
            if (iBufferOffset < 0)
                throw new ArgumentOutOfRangeException("The buffer offset (" + iBufferOffset +
                                                      ") cannot be negative.");
            if (iLength < 0)
                throw new IndexOutOfRangeException("The number of chars to read (" + iLength +
                                                   ") cannot be negative.");
            var aceFieldType = (ushort)GetACEFieldType(iCol);
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
                    var aceDataLength = (uint)GetACEDataLength(iCol);
                    if (acBuffer == null)
                        return aceDataLength;
                    if (iBufferOffset >= acBuffer.Length)
                        throw new ArgumentOutOfRangeException("The buffer offset is not valid.");
                    if (lFieldOffset >= (int)aceDataLength)
                        return 0;
                    var chars = (int)aceDataLength - (int)lFieldOffset;
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
                            AdsEncoding.GetChars(mabRecord, (int)lFieldOffset + maiFieldOffset[iCol],
                                chars, acBuffer,
                                iBufferOffset);
                            break;
                        case 26:
                        case 27:
                            Encoding.Unicode.GetChars(mabRecord, (int)lFieldOffset * 2 + maiFieldOffset[iCol],
                                chars * 2,
                                acBuffer, iBufferOffset);
                            break;
                        case 28:
                            if (lFieldOffset == 0L && iBufferOffset == 0)
                            {
                                var pulLen = (uint)chars;
                                var stringW = ACE.AdsGetStringW(mhCursor, (uint)(iCol + 1), acBuffer, ref pulLen,
                                    0);
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
                            var pulLen1 = aceDataLength + 1U;
                            var chArray = new char[pulLen1];
                            AdsException.CheckACE(ACE.AdsGetFieldW(mhCursor, (uint)(iCol + 1), chArray,
                                ref pulLen1, 0));
                            Array.Copy(chArray, lFieldOffset, acBuffer, iBufferOffset, chars);
                            break;
                    }

                    return chars;
                default:
                    throw new InvalidCastException("Cannot convert field to char[] array.");
            }
        }

        public override Guid GetGuid(int i)
        {
            byte[] bytes;
            try
            {
                bytes = GetBytes(i);
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
                int32 = GetInt32(iCol);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Cannot convert field to Int16.");
            }

            return int32 >= short.MinValue && int32 <= short.MaxValue
                ? (short)int32
                : throw new InvalidCastException("Cannot convert field to Int16.");
        }

        public override int GetInt32(int iCol)
        {
            CheckForGet(iCol);
            int plValue;
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 11:
                case 15:
                    plValue = BitConverter.ToInt32(mabRecord, maiFieldOffset[iCol]);
                    break;
                case 12:
                    plValue = BitConverter.ToInt16(mabRecord, maiFieldOffset[iCol]);
                    break;
                default:
                    var ulRet = ACE.AdsGetLong(mhCursor, (uint)(iCol + 1), out plValue);
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
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 11:
                case 12:
                    pqValue = GetInt32(iCol);
                    break;
                case 15:
                    pqValue = (uint)GetInt32(iCol);
                    break;
                case 19:
                case 21:
                    CheckForGet(iCol);
                    pqValue = BitConverter.ToInt64(mabRecord, maiFieldOffset[iCol]);
                    break;
                default:
                    CheckForGet(iCol);
                    var longLong = ACE.AdsGetLongLong(mhCursor, (uint)(iCol + 1), out pqValue);
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
                num = GetDouble(iCol);
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
            CheckForGet(iCol);
            double pdValue;
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 10:
                case 17:
                    pdValue = BitConverter.ToDouble(mabRecord, maiFieldOffset[iCol]);
                    break;
                default:
                    var ulRet = ACE.AdsGetDouble(mhCursor, (uint)(iCol + 1), out pdValue);
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
            var trimTrailingSpaces = mConnection.TrimTrailingSpaces;
            return GetString(iCol, trimTrailingSpaces);
        }

        private Encoding AdsEncoding
        {
            get
            {
                if (mEncoding == null)
                {
                    uint pulProperty;
                    AdsException.CheckACE(ACE.AdsGetIntProperty(mhCursor, 1U, out pulProperty));
                    mEncoding = Encoding.GetEncoding((int)pulProperty);
                }

                return mEncoding;
            }
        }

        public string GetString(int iCol, bool bTrim)
        {
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 4:
                case 20:
                case 23:
                    CheckForGet(iCol);
                    var aceDataLength1 = GetACEDataLength(iCol);
                    if (bTrim)
                    {
                        while (aceDataLength1 > 0 &&
                               mabRecord[aceDataLength1 - 1 + maiFieldOffset[iCol]] == 32)
                            --aceDataLength1;
                    }

                    return AdsEncoding.GetString(mabRecord, maiFieldOffset[iCol], aceDataLength1);
                case 26:
                case 27:
                    CheckForGet(iCol);
                    var aceDataLength2 = GetACEDataLength(iCol);
                    var str =
                        Encoding.Unicode.GetString(mabRecord, maiFieldOffset[iCol], aceDataLength2 * 2);
                    return bTrim ? str.TrimEnd(null) : str;
                default:
                    ushort usOption = 0;
                    long chars;
                    try
                    {
                        chars = GetChars(iCol, 0L, null, 0, 0);
                    }
                    catch (InvalidCastException ex)
                    {
                        throw new InvalidCastException("Cannot convert field to String.");
                    }

                    var pulLen = (uint)chars + 1U;
                    var pwcBuf = new char[pulLen];
                    if (bTrim)
                        usOption = 2;
                    var fieldW = ACE.AdsGetFieldW(mhCursor, (uint)(iCol + 1), pwcBuf, ref pulLen, usOption);
                    if (fieldW == 5066U)
                        throw new InvalidCastException("Cannot convert field to String.");
                    AdsException.CheckACE(fieldW);
                    return new string(pwcBuf, 0, (int)pulLen);
            }
        }

        public override Decimal GetDecimal(int iCol)
        {
            var chArray1 = new char[33];
            CheckForGet(iCol);
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 2:
                    var chArray2 = new char[maiFieldLength[iCol]];
                    for (var index = 0; index < maiFieldLength[iCol]; ++index)
                        chArray2[index] = (char)mabRecord[index + maiFieldOffset[iCol]];
                    var str = new string(chArray2);
                    if (str.Trim() == "")
                        return 0M;
                    try
                    {
                        return Convert.ToDecimal(str, CultureInfo.InvariantCulture.NumberFormat);
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
                        int32 = GetInt32(iCol);
                    }
                    catch (InvalidCastException ex)
                    {
                        throw new InvalidCastException("Cannot convert field to Decimal.");
                    }

                    return Convert.ToDecimal(int32);
                case 18:
                    return Decimal.FromOACurrency(BitConverter.ToInt64(mabRecord, maiFieldOffset[iCol]));
                case 19:
                case 21:
                    long int64;
                    try
                    {
                        int64 = GetInt64(iCol);
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
            var numArray = new short[13]
            {
                0,
                31,
                59,
                90,
                120,
                151,
                181,
                212,
                243,
                273,
                304,
                334,
                365
            };
            var num = iYear % 4 == 0 && iYear % 100 != 0 || iYear % 400 == 0 ? (short)1 : (short)0;
            if (iDays <= 59)
                num = 0;
            for (short index = 1; index <= 12; ++index)
            {
                if (iDays <= numArray[index] + num)
                {
                    iMonth = index;
                    if (index <= 2)
                        num = 0;
                    iDay = iDays - numArray[index - 1] - num;
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
            var num1 = iJulian - 1721425;
            iYear = (int)(num1 / 365.2425 + 1.0);
            var iDays = num1 - AxYearToDays(iYear);
            if (iDays <= 0)
            {
                --iYear;
                iDays = num1 - AxYearToDays(iYear);
            }

            var num2 = iYear % 4 == 0 && iYear % 100 != 0 || iYear % 400 == 0 ? 366 : 365;
            if (iDays > num2)
            {
                ++iYear;
                iDays -= num2;
            }

            AxMonthDay(iYear, iDays, out iMonth, out iDay);
        }

        public override DateTime GetDateTime(int iCol)
        {
            var iYear = 0;
            var iMonth = 0;
            var iDay = 0;
            CheckForGet(iCol);
            var aceFieldType = (ushort)GetACEFieldType(iCol);
            switch (aceFieldType)
            {
                case 3:
                case 14:
                case 22:
                    if (msTableType == 3 || aceFieldType == 14)
                    {
                        ConvertJulianDay(BitConverter.ToInt32(mabRecord, maiFieldOffset[iCol]),
                            out iYear,
                            out iMonth, out iDay);
                    }
                    else
                    {
                        var chArray = new char[maiFieldLength[iCol]];
                        for (var index = 0; index < maiFieldLength[iCol]; ++index)
                            chArray[index] = (char)mabRecord[index + maiFieldOffset[iCol]];
                        var str = new string(chArray, 0, maiFieldLength[iCol]);
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

                    if (aceFieldType == 14 || aceFieldType == 22)
                    {
                        var num = BitConverter.ToInt32(mabRecord, maiFieldOffset[iCol] + 4);
                        if (msTableType == 4)
                            num = 1000 * ((num + 500) / 1000);
                        dateTime = dateTime.AddMilliseconds(num);
                    }

                    return dateTime;
                default:
                    throw new InvalidCastException("Cannot convert field to DateTime.");
            }
        }

        protected bool FieldIsEmpty(int iCol)
        {
            var aceFieldType = (ushort)GetACEFieldType(iCol);
            if (!mConnection.DbfsUseNulls && msTableType != 3 && msTableType != 4 &&
                aceFieldType != 3)
                return false;
            ushort pbNull;
            if (msTableType == 3)
            {
                switch (aceFieldType)
                {
                    case 1:
                        return mabRecord[maiFieldOffset[iCol]] == 32;
                    case 2:
                    case 3:
                    case 4:
                    case 9:
                    case 16:
                    case 20:
                    case 26:
                    case 29:
                        var num1 = 1;
                        if (aceFieldType == 26)
                            num1 = 2;
                        for (var index = 0; index < maiFieldLength[iCol] * num1; ++index)
                        {
                            if (mabRecord[index + maiFieldOffset[iCol]] != 0)
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
                        for (var index = 0; index < mabDblNull.Length; ++index)
                        {
                            if (mabDblNull[index] !=
                                mabRecord[index + maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 11:
                        for (var index = 0; index < mabInt32Null.Length; ++index)
                        {
                            if (mabInt32Null[index] !=
                                mabRecord[index + maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 12:
                        for (var index = 0; index < mabInt16Null.Length; ++index)
                        {
                            if (mabInt16Null[index] !=
                                mabRecord[index + maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 13:
                        for (var index = 0; index < mabTimeNull.Length; ++index)
                        {
                            if (mabTimeNull[index] !=
                                mabRecord[index + maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    case 14:
                    case 22:
                        for (var index = 0; index < 4; ++index)
                        {
                            if (mabRecord[index + maiFieldOffset[iCol]] != 0)
                                return false;
                        }

                        return true;
                    case 15:
                        return false;
                    case 18:
                    case 19:
                    case 21:
                        for (var index = 0; index < mabInt64Null.Length; ++index)
                        {
                            if (mabInt64Null[index] !=
                                mabRecord[index + maiFieldOffset[iCol]])
                                return false;
                        }

                        return true;
                    default:
                        throw new Exception("AdsDataReader.FieldIsEmpty : Unsupported ADT type.");
                }
            }
            else if (msTableType == 4)
            {
                if (aceFieldType == 3 || aceFieldType == 14)
                {
                    var num2 = aceFieldType != 3 ? (byte)0 : (byte)32;
                    pbNull = 1;
                    for (var index = 0; index < maiFieldLength[iCol]; ++index)
                    {
                        if (mabRecord[index + maiFieldOffset[iCol]] != num2)
                        {
                            pbNull = 0;
                            break;
                        }
                    }

                    if (pbNull == 1)
                        return true;
                }

                if (masVFPNullable[iCol] == 0)
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
                        for (var index = 0; index < maiFieldLength[iCol]; ++index)
                        {
                            if (mabRecord[index + maiFieldOffset[iCol]] != 32)
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
                        AdsException.CheckACE(ACE.AdsIsEmpty(mhCursor, (uint)(iCol + 1), out pbEmpty));
                        return pbEmpty == 1;
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 16:
                    case 17:
                    case 29:
                        for (var index = 0; index < maiFieldLength[iCol]; ++index)
                        {
                            if (mabRecord[index + maiFieldOffset[iCol]] != 0)
                                return false;
                        }

                        return true;
                    default:
                        throw new Exception("AdsDataReader.FieldIsEmpty : Unsupported DBF type.");
                }
            }

            AdsException.CheckACE(ACE.AdsIsNull(mhCursor, (uint)(iCol + 1), out pbNull));
            return pbNull == 1;
        }

        public override bool IsDBNull(int iCol)
        {
            CheckOpen();
            CheckPositioned();
            CheckColumnIndex(iCol);
            ReadRecord();
            return FieldIsEmpty(iCol);
        }

        public byte[] GetBytes(int iCol)
        {
            CheckForGet(iCol);
            switch ((ushort)GetACEFieldType(iCol))
            {
                case 6:
                case 7:
                    uint pulLength;
                    AdsException.CheckACE(ACE.AdsGetBinaryLength(mhCursor, (uint)(iCol + 1), out pulLength));
                    var pucBuf = new byte[pulLength];
                    var pulLen = pulLength;
                    AdsException.CheckACE(ACE.AdsGetBinary(mhCursor, (uint)(iCol + 1), 0U, pucBuf, ref pulLen));
                    return pucBuf;
                case 16:
                case 24:
                case 29:
                    var bytes = new byte[GetACEDataLength(iCol)];
                    for (var index = 0; index < bytes.Length; ++index)
                        bytes[index] = mabRecord[index + maiFieldOffset[iCol]];
                    return bytes;
                default:
                    throw new InvalidCastException("Cannot convert field to byte[] array.");
            }
        }

        public TimeSpan GetTimeSpan(int iCol)
        {
            CheckForGet(iCol);
            if (GetACEFieldType(iCol) != 13)
                throw new InvalidCastException("Cannot convert field to TimeSpan.");
            return new TimeSpan(0, 0, 0, 0, BitConverter.ToInt32(mabRecord, maiFieldOffset[iCol]));
        }

        public short RecordCache
        {
            get => msCacheSize;
            set
            {
                CheckOpen();
                if (!mbOpen || !(mhCursor != IntPtr.Zero) || value == msCacheSize)
                    return;
                AdsException.CheckACE(ACE.AdsCacheRecords(mhCursor, (ushort)value));
                msCacheSize = value;
                if (msCacheSize != 0)
                    return;
                msCacheSize = 1;
            }
        }

        public bool IsStatic => mbIsStatic;

        public bool IsDate(int iCol)
        {
            CheckOpen();
            CheckColumnIndex(iCol);
            var aceFieldType = (ushort)GetACEFieldType(iCol);
            return aceFieldType == 3 || aceFieldType == 9;
        }

        public bool IsDateTime(int iCol)
        {
            CheckOpen();
            CheckColumnIndex(iCol);
            var aceFieldType = (ushort)GetACEFieldType(iCol);
            return aceFieldType == 14 || aceFieldType == 22;
        }

        protected int GetACEFieldLength(int iCol)
        {
            if (maiFieldLength != null)
                return maiFieldLength[iCol];
            int aceFieldType = GetACEFieldType(iCol);
            uint pulLength;
            AdsException.CheckACE(ACE.AdsGetFieldLength(mhCursor, (uint)(iCol + 1), out pulLength));
            return (int)pulLength;
        }

        protected int GetACEDataLength(int iCol)
        {
            switch ((ushort)GetACEFieldType(iCol))
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
                    AdsException.CheckACE(ACE.AdsGetDataLength(mhCursor, (uint)(iCol + 1), 0U, out pulLength));
                    return (int)pulLength;
                default:
                    return GetACEFieldLength(iCol);
            }
        }

        public int GetDataLength(int iCol)
        {
            CheckOpen();
            CheckPositioned();
            CheckColumnIndex(iCol);
            return GetACEDataLength(iCol);
        }

        public override IEnumerator GetEnumerator()
        {
            return new DbEnumerator((IDataReader)this);
        }

        public override bool HasRows
        {
            get
            {
                CheckOpen();
                return mbHasRows;
            }
        }
    }
}