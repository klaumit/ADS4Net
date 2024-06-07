using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;
using AdvantageClientEngine;

namespace Advantage.Data.Provider
{
    public sealed class AdsConnectionStringBuilder : DbConnectionStringBuilder
    {
        private AdsConnectionStringHandler mCSHandler;
        private string mstrServerType;
        private string mstrTableType;
        private string mstrCharType;
        private string mstrUnicodeCollation;
        private string mstrLockMode;
        private string mstrSecurityMode;
        private string mstrFilterOptions;
        private string mstrCompression;
        private string mstrCommType;
        private static ArrayList maUnicodeNames;

        private void InitializeBuilder()
        {
            mCSHandler = new AdsConnectionStringHandler();
            mstrServerType = "REMOTE";
            mstrTableType = "ADT";
            mstrCharType = "ANSI";
            mstrUnicodeCollation = "";
            maUnicodeNames = null;
            mstrLockMode = "PROPRIETARY";
            mstrSecurityMode = "IGNORERIGHTS";
            mstrFilterOptions = "IGNORE_WHEN_COUNTING";
            mstrCompression = "";
            mstrCommType = "";
        }

        public AdsConnectionStringBuilder()
            : this(null)
        {
            InitializeBuilder();
        }

        public AdsConnectionStringBuilder(string connStr)
        {
            InitializeBuilder();
            ConnectionString = connStr;
        }

        public override void Clear()
        {
            InitializeBuilder();
            base.Clear();
        }

        [Description("Specifies the connection path with optional data dictionary.")]
        [DisplayName("Data Source")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Data Source")]
        [ParenthesizePropertyName(true)]
        public string DataSource
        {
            get => mCSHandler.DataSource;
            set
            {
                base["Data Source"] = value;
                mCSHandler.DataSource = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Description(
            "Specifies the data dictionary to use.  This is not required if it is included in the Data Source path.")]
        [DisplayName("Initial Catalog")]
        [Category("Data Source")]
        public string InitialCatalog
        {
            get => mCSHandler.InitialCatalog;
            set
            {
                base["Initial Catalog"] = value;
                mCSHandler.InitialCatalog = value;
            }
        }

        [DisplayName("User ID")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Data Source")]
        [Description("Specifies the user ID to use when connecting to the data source.")]
        public string UserID
        {
            get => mCSHandler.UserID;
            set
            {
                base["User ID"] = value;
                mCSHandler.UserID = value;
            }
        }

        [PasswordPropertyText(true)]
        [Category("Data Source")]
        [Description("Specifies the password to use when connecting to the data source.")]
        [DisplayName("Password")]
        [RefreshProperties(RefreshProperties.All)]
        public string Password
        {
            get => mCSHandler.Password;
            set
            {
                base[nameof(Password)] = value;
                mCSHandler.Password = value;
            }
        }

        [Description("Specifies the table type to use when opening free tables.")]
        [DisplayName("TableType")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [TypeConverter(typeof(TableTypeConverter))]
        public string TableType
        {
            get => mstrTableType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(TableType));
                    mstrTableType = "ADT";
                }
                else
                {
                    mCSHandler.ParseConnectionString("TableType=" + value);
                    base[nameof(TableType)] = value;
                    mstrTableType = value;
                }
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(ServerTypeConverter))]
        [DisplayName("ServerType")]
        [Category("Source")]
        [Description("Specifies the server type to use for the connection.")]
        public string ServerType
        {
            get => mstrServerType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(ServerType));
                    mstrServerType = "REMOTE";
                }
                else
                {
                    base[nameof(ServerType)] = value;
                    mCSHandler.ParseConnectionString("ServerType=" + value);
                    mstrServerType = value;
                }
            }
        }

        [TypeConverter(typeof(CharTypeConverter))]
        [DisplayName("CharType")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [Description("Specifies the character set to use for collation and comparison of string values.")]
        public string CharType
        {
            get => mstrCharType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(CharType));
                    mstrCharType = "ANSI";
                }
                else
                {
                    base[nameof(CharType)] = value;
                    mCSHandler.ParseConnectionString("CharType=" + value);
                    mstrCharType = value;
                }
            }
        }

        [Category("Source")]
        [TypeConverter(typeof(UnicodeCollationConverter))]
        [DisplayName("UnicodeCollation")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Specifies the Unicode collation to use for sorting of string values.")]
        public string UnicodeCollation
        {
            get => mstrUnicodeCollation;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(UnicodeCollation));
                    mstrUnicodeCollation = "";
                }
                else
                {
                    base[nameof(UnicodeCollation)] = value;
                    mstrUnicodeCollation = value;
                }
            }
        }

        [Category("Source")]
        [Description("Specifies the locking mode to use with DBF tables.")]
        [DisplayName("LockMode")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(LockModeTypeConverter))]
        public string LockMode
        {
            get => mstrLockMode;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(LockMode));
                    mstrLockMode = "PROPRIETARY";
                }
                else
                {
                    base[nameof(LockMode)] = value;
                    mCSHandler.ParseConnectionString("LockMode=" + value);
                    mstrLockMode = value;
                }
            }
        }

        [Category("Source")]
        [TypeConverter(typeof(SecurityModeTypeConverter))]
        [DisplayName("SecurityMode")]
        [Description("Specifies if the client should perform rights checking for free connections.")]
        [RefreshProperties(RefreshProperties.All)]
        public string SecurityMode
        {
            get => mstrSecurityMode;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(SecurityMode));
                    mstrSecurityMode = "IGNORERIGHTS";
                }
                else
                {
                    base[nameof(SecurityMode)] = value;
                    mCSHandler.ParseConnectionString("SecurityMode=" + value);
                    mstrSecurityMode = value;
                }
            }
        }

        [Category("Source")]
        [Description("Specifies the communication protocol used to connect to the Advantage Database Server.")]
        [DisplayName("CommType")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(CommTypeConverter))]
        public string CommType
        {
            get => mstrCommType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(CommType));
                    mstrCommType = "";
                }
                else
                {
                    base[nameof(CommType)] = value;
                    mCSHandler.ParseConnectionString("CommType=" + value);
                    mstrCommType = value;
                }
            }
        }

        [DisplayName("ShowDeleted")]
        [Description("Specifies whether deleted records in DBF tables are visible.")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        public bool ShowDeleted
        {
            get => mCSHandler.ShowDeleted;
            set
            {
                base[nameof(ShowDeleted)] = value;
                mCSHandler.ShowDeleted = value;
            }
        }

        [Description(
            "Specifies the encryption password to use for free tables that are opened with CommandType.TableDirect.")]
        [DisplayName("EncryptionPassword")]
        [Category("Source")]
        [RefreshProperties(RefreshProperties.All)]
        [PasswordPropertyText(true)]
        public string EncryptionPassword
        {
            get => mCSHandler.EncryptionPassword;
            set
            {
                base[nameof(EncryptionPassword)] = value;
                mCSHandler.EncryptionPassword = value;
            }
        }

        [Category("Source")]
        [DisplayName("DbfsUseNulls")]
        [RefreshProperties(RefreshProperties.All)]
        [Description(
            "Specifies whether DBF tables are to return NULL for column data that is ordinarily considered as \"empty\" in Xbase terminology.")]
        public bool DbfsUseNulls
        {
            get => mCSHandler.DbfsUseNulls;
            set
            {
                base[nameof(DbfsUseNulls)] = value;
                mCSHandler.DbfsUseNulls = value;
            }
        }

        [TypeConverter(typeof(FilterOptionsTypeConverter))]
        [DisplayName("FilterOptions")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [Description(
            "Specifies whether to respect the filtering applied to the rowset when determining record count and logical positioning information.")]
        public string FilterOptions
        {
            get => mstrFilterOptions;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(FilterOptions));
                    mstrFilterOptions = "IGNORE_WHEN_COUNTING";
                }
                else
                {
                    base[nameof(FilterOptions)] = value;
                    mCSHandler.ParseConnectionString("FilterOptions=" + value);
                    mstrFilterOptions = value;
                }
            }
        }

        [Category("Source")]
        [DisplayName("TrimTrailingSpaces")]
        [RefreshProperties(RefreshProperties.All)]
        [Description(
            "Specifies whether trailing white space is removed from string fields when the data is retrieved.")]
        public bool TrimTrailingSpaces
        {
            get => mCSHandler.TrimTrailingSpaces;
            set
            {
                base[nameof(TrimTrailingSpaces)] = value;
                mCSHandler.TrimTrailingSpaces = value;
            }
        }

        [Description(
            "Specifies whether the connection is automatically enlisted in the thread's current transaction context.")]
        [DisplayName("Enlist")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        public bool Enlist
        {
            get => mCSHandler.TransScopeEnlist;
            set
            {
                base[nameof(Enlist)] = value;
                mCSHandler.TransScopeEnlist = value;
            }
        }

        [TypeConverter(typeof(CompressionTypeConverter))]
        [Category("Source")]
        [Description("Specifies the option for communications compression between client and server.")]
        [DisplayName("Compression")]
        [RefreshProperties(RefreshProperties.All)]
        public string Compression
        {
            get => mstrCompression;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(Compression));
                    mstrCompression = "";
                }
                else
                {
                    base[nameof(Compression)] = value;
                    mCSHandler.ParseConnectionString("Compression=" + value);
                    mstrCompression = value;
                }
            }
        }

        [DisplayName("Shared")]
        [Description(
            "Specifies whether tables are opened with CommandType.TableDirect are opened shared or exclusive.")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        public bool Shared
        {
            get => mCSHandler.Shared;
            set
            {
                base[nameof(Shared)] = value;
                mCSHandler.Shared = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("ReadOnly")]
        [Category("Source")]
        [Description("Specifies whether tables are opened read-only or read-write.")]
        public bool ReadOnly
        {
            get => mCSHandler.ReadOnly;
            set
            {
                base[nameof(ReadOnly)] = value;
                mCSHandler.ReadOnly = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Description("Specifies whether connection pooling is used.")]
        [DisplayName("Pooling")]
        [Category("Pooling")]
        public bool Pooling
        {
            get => mCSHandler.Pooling;
            set
            {
                base[nameof(Pooling)] = value;
                mCSHandler.Pooling = value;
            }
        }

        [Description("Specifies the minimum number of connections to keep in an individual connection pool.")]
        [Category("Pooling")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("Min Pool Size")]
        public int MinPoolSize
        {
            get => mCSHandler.MinPoolSize;
            set
            {
                base["Min Pool Size"] = value;
                mCSHandler.MinPoolSize = value;
            }
        }

        [Description("Specifies the maximum number of connections allowed in an individual connection pool.")]
        [Category("Pooling")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("Max Pool Size")]
        public int MaxPoolSize
        {
            get => mCSHandler.MaxPoolSize;
            set
            {
                base["Max Pool Size"] = value;
                mCSHandler.MaxPoolSize = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Category("Pooling")]
        [Description(
            "Specifies the time (in seconds) that a connection will remain open once it has been returned to the pool.")]
        [DisplayName("Connection Lifetime")]
        public int ConnectionLifetime
        {
            get => mCSHandler.LifeTime;
            set
            {
                base["Connection Lifetime"] = value;
                mCSHandler.LifeTime = value;
            }
        }

        [Description(
            "Specifies whether a FIPS mode connection should be attempted. The client FIPS setting must match the server setting.")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("FIPS")]
        [Category("Source")]
        public bool FIPS
        {
            get => mCSHandler.FIPSMode;
            set
            {
                base[nameof(FIPS)] = value;
                mCSHandler.FIPSMode = value;
            }
        }

        [Description(
            "Specifies the encryption type to use for newly encrypted free tables or new dictionaries created on this connection.")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(EncryptionTypeConverter))]
        [DisplayName("EncryptionType")]
        [Category("Source")]
        public string EncryptionType
        {
            get => mCSHandler.EncryptionType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(EncryptionType));
                }
                else
                {
                    base[nameof(EncryptionType)] = value;
                    mCSHandler.EncryptionType = value;
                }
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [Description("Specifies the allowed ciphers for Transport Layer Security communications.")]
        [DisplayName("TLSCiphers")]
        [TypeConverter(typeof(TLSCiphersConverter))]
        public string TLSCiphers
        {
            get => mCSHandler.TLSCiphers;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(TLSCiphers));
                }
                else
                {
                    base[nameof(TLSCiphers)] = value;
                    mCSHandler.TLSCiphers = value;
                }
            }
        }

        [Category("Source")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("TLSCertificate")]
        [Description("Specifies the public certificate file for Transport Layer Security communications.")]
        public string TLSCertificate
        {
            get => mCSHandler.TLSCertificate;
            set
            {
                base[nameof(TLSCertificate)] = value;
                mCSHandler.TLSCertificate = value;
            }
        }

        [DisplayName("TLSCommonName")]
        [Description("Specifies the expected common name from the server for Transport Layer Security communications.")]
        [Category("Source")]
        [RefreshProperties(RefreshProperties.All)]
        public string TLSCommonName
        {
            get => mCSHandler.TLSCommonName;
            set
            {
                base[nameof(TLSCommonName)] = value;
                mCSHandler.TLSCommonName = value;
            }
        }

        [DisplayName("DDPassword")]
        [Category("Source")]
        [PasswordPropertyText(true)]
        [Description(
            "Specifies the data dictionary password for AES encryption. This is not required if the password has been set at the server with the SE_PASSWORDS configuration parameter.")]
        [RefreshProperties(RefreshProperties.All)]
        public string DDPassword
        {
            get => mCSHandler.DDPassword;
            set
            {
                base[nameof(DDPassword)] = value;
                mCSHandler.DDPassword = value;
            }
        }

        public override object this[string strKey]
        {
            get => base[strKey];
            set
            {
                switch (mCSHandler.MapPropertyName(strKey))
                {
                    case "TableType":
                        TableType = (string)value;
                        break;
                    case "CharType":
                        CharType = (string)value;
                        break;
                    case "UnicodeCollation":
                        UnicodeCollation = (string)value;
                        break;
                    case "LockMode":
                        LockMode = (string)value;
                        break;
                    case "SecurityMode":
                        SecurityMode = (string)value;
                        break;
                    case "ServerType":
                        ServerType = (string)value;
                        break;
                    case "ShowDeleted":
                        ShowDeleted =
                            ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "EncryptionPassword":
                        EncryptionPassword = (string)value;
                        break;
                    case "DbfsUseNulls":
                        DbfsUseNulls =
                            ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "FilterOptions":
                        FilterOptions = (string)value;
                        break;
                    case "TrimTrailingSpaces":
                        TrimTrailingSpaces =
                            ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "Enlist":
                        Enlist = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "Compression":
                        Compression = (string)value;
                        break;
                    case "Shared":
                        Shared = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "ReadOnly":
                        ReadOnly = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "Data Source":
                        DataSource = (string)value;
                        break;
                    case "Initial Catalog":
                        InitialCatalog = (string)value;
                        break;
                    case "User ID":
                        UserID = (string)value;
                        break;
                    case "Password":
                        Password = (string)value;
                        break;
                    case "Pooling":
                        Pooling = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "Min Pool Size":
                        MinPoolSize = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
                        break;
                    case "Max Pool Size":
                        MaxPoolSize = ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
                        break;
                    case "Connection Lifetime":
                        ConnectionLifetime =
                            ((IConvertible)value).ToInt32(CultureInfo.InvariantCulture);
                        break;
                    case "CommType":
                        CommType = (string)value;
                        break;
                    case "FIPS":
                        FIPS = ((IConvertible)value).ToBoolean(CultureInfo.InvariantCulture);
                        break;
                    case "EncryptionType":
                        EncryptionType = (string)value;
                        break;
                    case "DDPassword":
                        DDPassword = (string)value;
                        break;
                    case "TLSCiphers":
                        TLSCiphers = (string)value;
                        break;
                    case "TLSCertificate":
                        TLSCertificate = (string)value;
                        break;
                    case "TLSCommonName":
                        TLSCommonName = (string)value;
                        break;
                    default:
                        base[strKey] = value;
                        break;
                }
            }
        }

        public override bool Remove(string strKey)
        {
            var flag = base.Remove(strKey);
            switch (mCSHandler.MapPropertyName(strKey))
            {
                case "TableType":
                    TableType = null;
                    break;
                case "CharType":
                    CharType = null;
                    break;
                case "UnicodeCollation":
                    UnicodeCollation = null;
                    break;
                case "LockMode":
                    LockMode = null;
                    break;
                case "SecurityMode":
                    SecurityMode = null;
                    break;
                case "ServerType":
                    ServerType = null;
                    break;
                case "ShowDeleted":
                    mCSHandler.ShowDeleted = false;
                    break;
                case "EncryptionPassword":
                    mCSHandler.EncryptionPassword = null;
                    break;
                case "DbfsUseNulls":
                    mCSHandler.DbfsUseNulls = false;
                    break;
                case "FilterOptions":
                    FilterOptions = null;
                    break;
                case "TrimTrailingSpaces":
                    mCSHandler.TrimTrailingSpaces = false;
                    break;
                case "Enlist":
                    mCSHandler.TransScopeEnlist = true;
                    break;
                case "Compression":
                    Compression = null;
                    break;
                case "Shared":
                    mCSHandler.Shared = true;
                    break;
                case "ReadOnly":
                    mCSHandler.ReadOnly = false;
                    break;
                case "Data Source":
                    mCSHandler.DataSource = null;
                    break;
                case "Initial Catalog":
                    mCSHandler.InitialCatalog = null;
                    break;
                case "User ID":
                    mCSHandler.UserID = null;
                    break;
                case "Password":
                    mCSHandler.Password = null;
                    break;
                case "Pooling":
                    mCSHandler.Pooling = true;
                    break;
                case "Min Pool Size":
                    mCSHandler.MinPoolSize = 0;
                    break;
                case "Max Pool Size":
                    mCSHandler.MaxPoolSize = 100;
                    break;
                case "Connection Lifetime":
                    mCSHandler.LifeTime = 0;
                    break;
                case "CommType":
                    CommType = null;
                    break;
                case "FIPS":
                    FIPS = false;
                    break;
                case "EncryptionType":
                    EncryptionType = null;
                    break;
                case "DDPassword":
                    DDPassword = null;
                    break;
                case "TLSCiphers":
                    TLSCiphers = null;
                    break;
                case "TLSCertificate":
                    TLSCertificate = null;
                    break;
                case "TLSCommonName":
                    TLSCommonName = null;
                    break;
            }

            return flag;
        }

        private sealed class TableTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[4]
                    {
                        "ADT",
                        "VFP",
                        "CDX",
                        "NTX"
                    });
                return mcValues;
            }
        }

        private sealed class ServerTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[4]
                    {
                        "",
                        "REMOTE",
                        "LOCAL",
                        "AIS"
                    });
                return mcValues;
            }
        }

        private sealed class CharTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues =
                        new StandardValuesCollection(
                            Enum.GetNames(typeof(ACE.AdsCharTypes)));
                return mcValues;
            }
        }

        private sealed class UnicodeCollationConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                {
                    try
                    {
                        if (maUnicodeNames == null)
                        {
                            var adsConnection = new AdsConnection("data source=.;servertype=local;");
                            adsConnection.Open();
                            var command = adsConnection.CreateCommand();
                            command.CommandText =
                                "select x.Name from ( execute procedure sp_getcollations(null) )x where x.UnicodeLocale is null;";
                            command.CommandType = CommandType.Text;
                            var adsDataReader = command.ExecuteReader();
                            maUnicodeNames = new ArrayList();
                            while (adsDataReader.Read())
                            {
                                maUnicodeNames.Add(adsDataReader.GetString(0)
                                    .Trim());
                                maUnicodeNames.Add(
                                    adsDataReader.GetString(0).Trim() + "_ads_ci");
                            }

                            adsDataReader.Close();
                            adsConnection.Close();
                            adsConnection.Dispose();
                        }

                        mcValues =
                            new StandardValuesCollection(
                                maUnicodeNames);
                    }
                    catch
                    {
                        mcValues = new StandardValuesCollection(new object[0]);
                    }
                }

                return mcValues;
            }
        }

        private sealed class LockModeTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[2]
                    {
                        "PROPRIETARY",
                        "COMPATIBLE"
                    });
                return mcValues;
            }
        }

        private sealed class SecurityModeTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[2]
                    {
                        "IGNORERIGHTS",
                        "CHECKRIGHTS"
                    });
                return mcValues;
            }
        }

        private sealed class CommTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[5]
                    {
                        "",
                        "UDP_IP",
                        "TCP_IP",
                        "IPX",
                        "TLS"
                    });
                return mcValues;
            }
        }

        private sealed class FilterOptionsTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[2]
                    {
                        "IGNORE_WHEN_COUNTING",
                        "RESPECT_WHEN_COUNTING"
                    });
                return mcValues;
            }
        }

        private sealed class CompressionTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[4]
                    {
                        "",
                        "INTERNET",
                        "ALWAYS",
                        "NEVER"
                    });
                return mcValues;
            }
        }

        private sealed class EncryptionTypeConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[4]
                    {
                        "",
                        "RC4",
                        "AES128",
                        "AES256"
                    });
                return mcValues;
            }
        }

        private sealed class TLSCiphersConverter : TypeConverter
        {
            private StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (mcValues == null)
                    mcValues = new StandardValuesCollection(new string[6]
                    {
                        "",
                        "AES128-SHA:AES256-SHA:RC4-MD5",
                        "AES128-SHA:AES256-SHA",
                        "AES128-SHA",
                        "AES256-SHA",
                        "RC4-MD5"
                    });
                return mcValues;
            }
        }
    }
}