using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Advantage.Data.Provider
{
    [TypeConverter("Advantage.Data.Provider.AdsParameterConverter")]
    public sealed class AdsParameter : DbParameter, ICloneable, IDbDataParameter, IDataParameter
    {
        private DbType mdbType = DbType.String;
        private bool mbUserSetType;
        private ParameterDirection mParamDirection = ParameterDirection.Input;
        private bool mbNullable = true;
        private string mstrParamName = "";
        private string mstrSourceColumn = "";
        private DataRowVersion mSourceVersion = DataRowVersion.Current;
        private object mValue;
        private byte mucPrecision;
        private byte mucScale;
        private int miSize = -1;
        private int miIndex = 1;
        private bool mbIsNull;
        private bool mbSourceColumnNullMapping;

        public AdsParameter()
        {
        }

        public AdsParameter(string parameterName, DbType type)
        {
            ParameterName = parameterName;
            mdbType = type;
            mbUserSetType = true;
        }

        public AdsParameter(string parameterName, DbType type, int iSize)
        {
            ParameterName = parameterName;
            mdbType = type;
            miSize = iSize;
            mbUserSetType = true;
        }

        public AdsParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }

        public AdsParameter(int iIndex, object value)
        {
            miIndex = iIndex;
            Value = value;
        }

        public AdsParameter(string parameterName, DbType dbType, int iSize, string sourceColumn)
        {
            ParameterName = parameterName;
            mdbType = dbType;
            miSize = iSize;
            mstrSourceColumn = sourceColumn;
            mbUserSetType = true;
        }

        public AdsParameter(
            string parameterName,
            DbType dbType,
            string srcColumn,
            DataRowVersion srcVersion)
        {
            ParameterName = parameterName;
            mdbType = dbType;
            mstrSourceColumn = srcColumn;
            mSourceVersion = srcVersion;
            mbUserSetType = true;
        }

        public AdsParameter(
            string parameterName,
            DbType dbType,
            int iSize,
            ParameterDirection direction,
            bool isNullable,
            byte precision,
            byte scale,
            string srcColumn,
            DataRowVersion srcVersion,
            object value)
        {
            ParameterName = parameterName;
            mdbType = dbType;
            miSize = iSize;
            mParamDirection = direction;
            mbNullable = isNullable;
            mucPrecision = precision;
            mucScale = scale;
            mstrSourceColumn = srcColumn;
            mSourceVersion = srcVersion;
            mbUserSetType = true;
            Value = value;
        }

        public void Dispose()
        {
        }

        [Category("Data")]
        [DefaultValue(0)]
        public override int Size
        {
            get => miSize;
            set => miSize = value;
        }

        [DefaultValue(0)]
        [Category("Data")]
        public new byte Precision
        {
            get => mucPrecision;
            set => mucPrecision = value;
        }

        [Category("Data")]
        [DefaultValue(0)]
        public new byte Scale
        {
            get => mucScale;
            set => mucScale = value;
        }

        [DefaultValue(DbType.String)]
        [Category("Data")]
        public override DbType DbType
        {
            get => mdbType;
            set
            {
                mdbType = value;
                mbUserSetType = true;
            }
        }

        [Category("Data")]
        [DefaultValue(ParameterDirection.Input)]
        public override ParameterDirection Direction
        {
            get => mParamDirection;
            set => mParamDirection = value;
        }

        [DefaultValue(true)]
        [Category("Data")]
        public override bool IsNullable
        {
            get => mbNullable;
            set => mbNullable = value;
        }

        public override bool SourceColumnNullMapping
        {
            get => mbSourceColumnNullMapping;
            set => mbSourceColumnNullMapping = value;
        }

        [Category("Misc")]
        [DefaultValue("")]
        public override string ParameterName
        {
            get => mstrParamName;
            set
            {
                mstrParamName = value;
                if (mstrParamName == null ||
                    !mstrParamName.StartsWith("@") && !mstrParamName.StartsWith(":"))
                    return;
                mstrParamName = mstrParamName.Substring(1);
            }
        }

        [DefaultValue("")]
        [Category("Data")]
        public override string SourceColumn
        {
            get => mstrSourceColumn;
            set => mstrSourceColumn = value;
        }

        [DefaultValue(DataRowVersion.Current)]
        [Category("Data")]
        public override DataRowVersion SourceVersion
        {
            get => mSourceVersion;
            set => mSourceVersion = value;
        }

        [Browsable(false)]
        public int Index
        {
            get => miIndex;
            set => miIndex = value;
        }

        private void SetParamType()
        {
            mbIsNull = mValue == null || Type.GetTypeCode(mValue.GetType()) == TypeCode.DBNull;
            if (mbUserSetType && mdbType != DbType.Object)
                return;
            if (mValue == null)
                mdbType = DbType.String;
            else
                mdbType = InferType(mValue);
        }

        [Category("Data")]
        [DefaultValue(null)]
        [TypeConverter(typeof(StringConverter))]
        public override object Value
        {
            get => mValue;
            set
            {
                mValue = value;
                SetParamType();
                if (miSize != 0)
                    return;
                miSize = -1;
            }
        }

        [Browsable(false)] public bool IsNull => mbIsNull;

        public override void ResetDbType()
        {
            mbUserSetType = false;
            SetParamType();
        }

        object ICloneable.Clone()
        {
            return new AdsParameter(ParameterName, DbType, Size, Direction, IsNullable,
                Precision, Scale, SourceColumn, SourceVersion, Value);
        }

        private DbType InferType(object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Empty:
                    throw new SystemException("Invalid data type.");
                case TypeCode.Object:
                    return value.GetType() == (object)Type.GetType("System.TimeSpan")
                        ? DbType.Time
                        : DbType.Object;
                case TypeCode.DBNull:
                    return DbType.String;
                case TypeCode.Boolean:
                    return DbType.Boolean;
                case TypeCode.Char:
                    throw new ArgumentException("Char parameter data type not supported.");
                case TypeCode.SByte:
                    return DbType.SByte;
                case TypeCode.Byte:
                    return DbType.Byte;
                case TypeCode.Int16:
                    return DbType.Int16;
                case TypeCode.UInt16:
                    return DbType.UInt16;
                case TypeCode.Int32:
                    return DbType.Int32;
                case TypeCode.UInt32:
                    return DbType.UInt32;
                case TypeCode.Int64:
                    return DbType.Int64;
                case TypeCode.UInt64:
                    return DbType.UInt64;
                case TypeCode.Single:
                    return DbType.Single;
                case TypeCode.Double:
                    return DbType.Double;
                case TypeCode.Decimal:
                    return DbType.Decimal;
                case TypeCode.DateTime:
                    return DbType.DateTime;
                case TypeCode.String:
                    return DbType.String;
                default:
                    throw new SystemException("Value is of unknown data type");
            }
        }
    }
}