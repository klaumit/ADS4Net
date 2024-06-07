using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Globalization;

namespace Advantage.Data.Provider
{
    public class AdsParameterConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == (object)typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destinationType)
        {
            if (destinationType != (object)typeof(InstanceDescriptor) || !(value is AdsParameter))
                return base.ConvertTo(context, culture, value, destinationType);
            var adsParameter1 = (AdsParameter)value;
            var adsParameter2 = new AdsParameter();
            switch (adsParameter1.Direction != adsParameter2.Direction ||
                    adsParameter1.IsNullable != adsParameter2.IsNullable ||
                    adsParameter1.Precision != adsParameter2.Precision || adsParameter1.Scale != adsParameter2.Scale ||
                    adsParameter1.Value != adsParameter2.Value
                        ? ParamCtorVer.AllInclusive
                        : (adsParameter1.SourceColumn == adsParameter2.SourceColumn
                            ? (ParamCtorVer.NameTypeSize)
                            : (adsParameter1.Size == adsParameter2.Size ||
                               adsParameter1.SourceVersion == adsParameter2.SourceVersion
                                ? (adsParameter1.SourceVersion == adsParameter2.SourceVersion
                                    ? ParamCtorVer.NameTypeSizeSrc
                                    : ParamCtorVer.NameTypeSrcVer)
                                : ParamCtorVer.AllInclusive)))
            {
                case ParamCtorVer.NameTypeSrcVer:
                    return new InstanceDescriptor(typeof(AdsParameter).GetConstructor([
                        typeof(string),
                        typeof(DbType),
                        typeof(int),
                        typeof(DataRowVersion)
                    ]), new object[]
                    {
                        adsParameter1.ParameterName,
                        adsParameter1.DbType,
                        adsParameter1.SourceColumn,
                        adsParameter1.SourceVersion
                    }, true);
                case ParamCtorVer.NameTypeSizeSrc:
                    return new InstanceDescriptor(typeof(AdsParameter).GetConstructor([
                        typeof(string),
                        typeof(DbType),
                        typeof(int),
                        typeof(string)
                    ]), new object[]
                    {
                        adsParameter1.ParameterName,
                        adsParameter1.DbType,
                        adsParameter1.Size,
                        adsParameter1.SourceColumn
                    }, true);
                case ParamCtorVer.NameTypeSize:
                    return new InstanceDescriptor(typeof(AdsParameter).GetConstructor([
                        typeof(string),
                        typeof(DbType),
                        typeof(int)
                    ]), new object[]
                    {
                        adsParameter1.ParameterName,
                        adsParameter1.DbType,
                        adsParameter1.Size
                    }, true);
                case ParamCtorVer.NameType:
                    return new InstanceDescriptor(typeof(AdsParameter).GetConstructor([
                        typeof(string),
                        typeof(DbType)
                    ]), new object[]
                    {
                        adsParameter1.ParameterName,
                        adsParameter1.DbType
                    }, true);
                default:
                    return new InstanceDescriptor(typeof(AdsParameter).GetConstructor([
                        typeof(string),
                        typeof(DbType),
                        typeof(int),
                        typeof(ParameterDirection),
                        typeof(bool),
                        typeof(byte),
                        typeof(byte),
                        typeof(string),
                        typeof(DataRowVersion),
                        typeof(object)
                    ]), new[]
                    {
                        adsParameter1.ParameterName,
                        adsParameter1.DbType,
                        adsParameter1.Size,
                        adsParameter1.Direction,
                        adsParameter1.IsNullable,
                        adsParameter1.Precision,
                        adsParameter1.Scale,
                        adsParameter1.SourceColumn,
                        adsParameter1.SourceVersion,
                        adsParameter1.Value
                    }, true);
            }
        }

        private enum ParamCtorVer
        {
            AllInclusive = 1,
            NameTypeSrcVer = 2,
            NameTypeSizeSrc = 3,
            NameTypeSize = 4,
            NameType = 5,
        }
    }
}