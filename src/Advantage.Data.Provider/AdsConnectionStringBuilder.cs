using AdvantageClientEngine;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Globalization;

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
            this.mCSHandler = new AdsConnectionStringHandler();
            this.mstrServerType = "REMOTE";
            this.mstrTableType = "ADT";
            this.mstrCharType = "ANSI";
            this.mstrUnicodeCollation = "";
            AdsConnectionStringBuilder.maUnicodeNames = (ArrayList)null;
            this.mstrLockMode = "PROPRIETARY";
            this.mstrSecurityMode = "IGNORERIGHTS";
            this.mstrFilterOptions = "IGNORE_WHEN_COUNTING";
            this.mstrCompression = "";
            this.mstrCommType = "";
        }

        public AdsConnectionStringBuilder()
            : this((string)null)
        {
            this.InitializeBuilder();
        }

        public AdsConnectionStringBuilder(string connStr)
        {
            this.InitializeBuilder();
            this.ConnectionString = connStr;
        }

        public override void Clear()
        {
            this.InitializeBuilder();
            base.Clear();
        }

        [Description("Specifies the connection path with optional data dictionary.")]
        [DisplayName("Data Source")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Data Source")]
        [ParenthesizePropertyName(true)]
        public string DataSource
        {
            get => this.mCSHandler.DataSource;
            set
            {
                base["Data Source"] = (object)value;
                this.mCSHandler.DataSource = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Description(
            "Specifies the data dictionary to use.  This is not required if it is included in the Data Source path.")]
        [DisplayName("Initial Catalog")]
        [Category("Data Source")]
        public string InitialCatalog
        {
            get => this.mCSHandler.InitialCatalog;
            set
            {
                base["Initial Catalog"] = (object)value;
                this.mCSHandler.InitialCatalog = value;
            }
        }

        [DisplayName("User ID")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Data Source")]
        [Description("Specifies the user ID to use when connecting to the data source.")]
        public string UserID
        {
            get => this.mCSHandler.UserID;
            set
            {
                base["User ID"] = (object)value;
                this.mCSHandler.UserID = value;
            }
        }

        [PasswordPropertyText(true)]
        [Category("Data Source")]
        [Description("Specifies the password to use when connecting to the data source.")]
        [DisplayName("Password")]
        [RefreshProperties(RefreshProperties.All)]
        public string Password
        {
            get => this.mCSHandler.Password;
            set
            {
                base[nameof(Password)] = (object)value;
                this.mCSHandler.Password = value;
            }
        }

        [Description("Specifies the table type to use when opening free tables.")]
        [DisplayName("TableType")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [TypeConverter(typeof(AdsConnectionStringBuilder.TableTypeConverter))]
        public string TableType
        {
            get => this.mstrTableType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(TableType));
                    this.mstrTableType = "ADT";
                }
                else
                {
                    this.mCSHandler.ParseConnectionString("TableType=" + value);
                    base[nameof(TableType)] = (object)value;
                    this.mstrTableType = value;
                }
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(AdsConnectionStringBuilder.ServerTypeConverter))]
        [DisplayName("ServerType")]
        [Category("Source")]
        [Description("Specifies the server type to use for the connection.")]
        public string ServerType
        {
            get => this.mstrServerType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(ServerType));
                    this.mstrServerType = "REMOTE";
                }
                else
                {
                    base[nameof(ServerType)] = (object)value;
                    this.mCSHandler.ParseConnectionString("ServerType=" + value);
                    this.mstrServerType = value;
                }
            }
        }

        [TypeConverter(typeof(AdsConnectionStringBuilder.CharTypeConverter))]
        [DisplayName("CharType")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [Description("Specifies the character set to use for collation and comparison of string values.")]
        public string CharType
        {
            get => this.mstrCharType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(CharType));
                    this.mstrCharType = "ANSI";
                }
                else
                {
                    base[nameof(CharType)] = (object)value;
                    this.mCSHandler.ParseConnectionString("CharType=" + value);
                    this.mstrCharType = value;
                }
            }
        }

        [Category("Source")]
        [TypeConverter(typeof(AdsConnectionStringBuilder.UnicodeCollationConverter))]
        [DisplayName("UnicodeCollation")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Specifies the Unicode collation to use for sorting of string values.")]
        public string UnicodeCollation
        {
            get => this.mstrUnicodeCollation;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(UnicodeCollation));
                    this.mstrUnicodeCollation = "";
                }
                else
                {
                    base[nameof(UnicodeCollation)] = (object)value;
                    this.mstrUnicodeCollation = value;
                }
            }
        }

        [Category("Source")]
        [Description("Specifies the locking mode to use with DBF tables.")]
        [DisplayName("LockMode")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(AdsConnectionStringBuilder.LockModeTypeConverter))]
        public string LockMode
        {
            get => this.mstrLockMode;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(LockMode));
                    this.mstrLockMode = "PROPRIETARY";
                }
                else
                {
                    base[nameof(LockMode)] = (object)value;
                    this.mCSHandler.ParseConnectionString("LockMode=" + value);
                    this.mstrLockMode = value;
                }
            }
        }

        [Category("Source")]
        [TypeConverter(typeof(AdsConnectionStringBuilder.SecurityModeTypeConverter))]
        [DisplayName("SecurityMode")]
        [Description("Specifies if the client should perform rights checking for free connections.")]
        [RefreshProperties(RefreshProperties.All)]
        public string SecurityMode
        {
            get => this.mstrSecurityMode;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(SecurityMode));
                    this.mstrSecurityMode = "IGNORERIGHTS";
                }
                else
                {
                    base[nameof(SecurityMode)] = (object)value;
                    this.mCSHandler.ParseConnectionString("SecurityMode=" + value);
                    this.mstrSecurityMode = value;
                }
            }
        }

        [Category("Source")]
        [Description("Specifies the communication protocol used to connect to the Advantage Database Server.")]
        [DisplayName("CommType")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(AdsConnectionStringBuilder.CommTypeConverter))]
        public string CommType
        {
            get => this.mstrCommType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(CommType));
                    this.mstrCommType = "";
                }
                else
                {
                    base[nameof(CommType)] = (object)value;
                    this.mCSHandler.ParseConnectionString("CommType=" + value);
                    this.mstrCommType = value;
                }
            }
        }

        [DisplayName("ShowDeleted")]
        [Description("Specifies whether deleted records in DBF tables are visible.")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        public bool ShowDeleted
        {
            get => this.mCSHandler.ShowDeleted;
            set
            {
                base[nameof(ShowDeleted)] = (object)value;
                this.mCSHandler.ShowDeleted = value;
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
            get => this.mCSHandler.EncryptionPassword;
            set
            {
                base[nameof(EncryptionPassword)] = (object)value;
                this.mCSHandler.EncryptionPassword = value;
            }
        }

        [Category("Source")]
        [DisplayName("DbfsUseNulls")]
        [RefreshProperties(RefreshProperties.All)]
        [Description(
            "Specifies whether DBF tables are to return NULL for column data that is ordinarily considered as \"empty\" in Xbase terminology.")]
        public bool DbfsUseNulls
        {
            get => this.mCSHandler.DbfsUseNulls;
            set
            {
                base[nameof(DbfsUseNulls)] = (object)value;
                this.mCSHandler.DbfsUseNulls = value;
            }
        }

        [TypeConverter(typeof(AdsConnectionStringBuilder.FilterOptionsTypeConverter))]
        [DisplayName("FilterOptions")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [Description(
            "Specifies whether to respect the filtering applied to the rowset when determining record count and logical positioning information.")]
        public string FilterOptions
        {
            get => this.mstrFilterOptions;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(FilterOptions));
                    this.mstrFilterOptions = "IGNORE_WHEN_COUNTING";
                }
                else
                {
                    base[nameof(FilterOptions)] = (object)value;
                    this.mCSHandler.ParseConnectionString("FilterOptions=" + value);
                    this.mstrFilterOptions = value;
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
            get => this.mCSHandler.TrimTrailingSpaces;
            set
            {
                base[nameof(TrimTrailingSpaces)] = (object)value;
                this.mCSHandler.TrimTrailingSpaces = value;
            }
        }

        [Description(
            "Specifies whether the connection is automatically enlisted in the thread's current transaction context.")]
        [DisplayName("Enlist")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        public bool Enlist
        {
            get => this.mCSHandler.TransScopeEnlist;
            set
            {
                base[nameof(Enlist)] = (object)value;
                this.mCSHandler.TransScopeEnlist = value;
            }
        }

        [TypeConverter(typeof(AdsConnectionStringBuilder.CompressionTypeConverter))]
        [Category("Source")]
        [Description("Specifies the option for communications compression between client and server.")]
        [DisplayName("Compression")]
        [RefreshProperties(RefreshProperties.All)]
        public string Compression
        {
            get => this.mstrCompression;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(Compression));
                    this.mstrCompression = "";
                }
                else
                {
                    base[nameof(Compression)] = (object)value;
                    this.mCSHandler.ParseConnectionString("Compression=" + value);
                    this.mstrCompression = value;
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
            get => this.mCSHandler.Shared;
            set
            {
                base[nameof(Shared)] = (object)value;
                this.mCSHandler.Shared = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("ReadOnly")]
        [Category("Source")]
        [Description("Specifies whether tables are opened read-only or read-write.")]
        public bool ReadOnly
        {
            get => this.mCSHandler.ReadOnly;
            set
            {
                base[nameof(ReadOnly)] = (object)value;
                this.mCSHandler.ReadOnly = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Description("Specifies whether connection pooling is used.")]
        [DisplayName("Pooling")]
        [Category("Pooling")]
        public bool Pooling
        {
            get => this.mCSHandler.Pooling;
            set
            {
                base[nameof(Pooling)] = (object)value;
                this.mCSHandler.Pooling = value;
            }
        }

        [Description("Specifies the minimum number of connections to keep in an individual connection pool.")]
        [Category("Pooling")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("Min Pool Size")]
        public int MinPoolSize
        {
            get => this.mCSHandler.MinPoolSize;
            set
            {
                base["Min Pool Size"] = (object)value;
                this.mCSHandler.MinPoolSize = value;
            }
        }

        [Description("Specifies the maximum number of connections allowed in an individual connection pool.")]
        [Category("Pooling")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("Max Pool Size")]
        public int MaxPoolSize
        {
            get => this.mCSHandler.MaxPoolSize;
            set
            {
                base["Max Pool Size"] = (object)value;
                this.mCSHandler.MaxPoolSize = value;
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Category("Pooling")]
        [Description(
            "Specifies the time (in seconds) that a connection will remain open once it has been returned to the pool.")]
        [DisplayName("Connection Lifetime")]
        public int ConnectionLifetime
        {
            get => this.mCSHandler.LifeTime;
            set
            {
                base["Connection Lifetime"] = (object)value;
                this.mCSHandler.LifeTime = value;
            }
        }

        [Description(
            "Specifies whether a FIPS mode connection should be attempted. The client FIPS setting must match the server setting.")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("FIPS")]
        [Category("Source")]
        public bool FIPS
        {
            get => this.mCSHandler.FIPSMode;
            set
            {
                base[nameof(FIPS)] = (object)value;
                this.mCSHandler.FIPSMode = value;
            }
        }

        [Description(
            "Specifies the encryption type to use for newly encrypted free tables or new dictionaries created on this connection.")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(AdsConnectionStringBuilder.EncryptionTypeConverter))]
        [DisplayName("EncryptionType")]
        [Category("Source")]
        public string EncryptionType
        {
            get => this.mCSHandler.EncryptionType;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(EncryptionType));
                }
                else
                {
                    base[nameof(EncryptionType)] = (object)value;
                    this.mCSHandler.EncryptionType = value;
                }
            }
        }

        [RefreshProperties(RefreshProperties.All)]
        [Category("Source")]
        [Description("Specifies the allowed ciphers for Transport Layer Security communications.")]
        [DisplayName("TLSCiphers")]
        [TypeConverter(typeof(AdsConnectionStringBuilder.TLSCiphersConverter))]
        public string TLSCiphers
        {
            get => this.mCSHandler.TLSCiphers;
            set
            {
                if (value == null)
                {
                    base.Remove(nameof(TLSCiphers));
                }
                else
                {
                    base[nameof(TLSCiphers)] = (object)value;
                    this.mCSHandler.TLSCiphers = value;
                }
            }
        }

        [Category("Source")]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("TLSCertificate")]
        [Description("Specifies the public certificate file for Transport Layer Security communications.")]
        public string TLSCertificate
        {
            get => this.mCSHandler.TLSCertificate;
            set
            {
                base[nameof(TLSCertificate)] = (object)value;
                this.mCSHandler.TLSCertificate = value;
            }
        }

        [DisplayName("TLSCommonName")]
        [Description("Specifies the expected common name from the server for Transport Layer Security communications.")]
        [Category("Source")]
        [RefreshProperties(RefreshProperties.All)]
        public string TLSCommonName
        {
            get => this.mCSHandler.TLSCommonName;
            set
            {
                base[nameof(TLSCommonName)] = (object)value;
                this.mCSHandler.TLSCommonName = value;
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
            get => this.mCSHandler.DDPassword;
            set
            {
                base[nameof(DDPassword)] = (object)value;
                this.mCSHandler.DDPassword = value;
            }
        }

        public override object this[string strKey]
        {
            get => base[strKey];
            set
            {
                switch (this.mCSHandler.MapPropertyName(strKey))
                {
                    case "TableType":
                        this.TableType = (string)value;
                        break;
                    case "CharType":
                        this.CharType = (string)value;
                        break;
                    case "UnicodeCollation":
                        this.UnicodeCollation = (string)value;
                        break;
                    case "LockMode":
                        this.LockMode = (string)value;
                        break;
                    case "SecurityMode":
                        this.SecurityMode = (string)value;
                        break;
                    case "ServerType":
                        this.ServerType = (string)value;
                        break;
                    case "ShowDeleted":
                        this.ShowDeleted =
                            ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "EncryptionPassword":
                        this.EncryptionPassword = (string)value;
                        break;
                    case "DbfsUseNulls":
                        this.DbfsUseNulls =
                            ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "FilterOptions":
                        this.FilterOptions = (string)value;
                        break;
                    case "TrimTrailingSpaces":
                        this.TrimTrailingSpaces =
                            ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "Enlist":
                        this.Enlist = ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "Compression":
                        this.Compression = (string)value;
                        break;
                    case "Shared":
                        this.Shared = ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "ReadOnly":
                        this.ReadOnly = ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "Data Source":
                        this.DataSource = (string)value;
                        break;
                    case "Initial Catalog":
                        this.InitialCatalog = (string)value;
                        break;
                    case "User ID":
                        this.UserID = (string)value;
                        break;
                    case "Password":
                        this.Password = (string)value;
                        break;
                    case "Pooling":
                        this.Pooling = ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "Min Pool Size":
                        this.MinPoolSize = ((IConvertible)value).ToInt32((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "Max Pool Size":
                        this.MaxPoolSize = ((IConvertible)value).ToInt32((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "Connection Lifetime":
                        this.ConnectionLifetime =
                            ((IConvertible)value).ToInt32((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "CommType":
                        this.CommType = (string)value;
                        break;
                    case "FIPS":
                        this.FIPS = ((IConvertible)value).ToBoolean((IFormatProvider)CultureInfo.InvariantCulture);
                        break;
                    case "EncryptionType":
                        this.EncryptionType = (string)value;
                        break;
                    case "DDPassword":
                        this.DDPassword = (string)value;
                        break;
                    case "TLSCiphers":
                        this.TLSCiphers = (string)value;
                        break;
                    case "TLSCertificate":
                        this.TLSCertificate = (string)value;
                        break;
                    case "TLSCommonName":
                        this.TLSCommonName = (string)value;
                        break;
                    default:
                        base[strKey] = value;
                        break;
                }
            }
        }

        public override bool Remove(string strKey)
        {
            bool flag = base.Remove(strKey);
            switch (this.mCSHandler.MapPropertyName(strKey))
            {
                case "TableType":
                    this.TableType = (string)null;
                    break;
                case "CharType":
                    this.CharType = (string)null;
                    break;
                case "UnicodeCollation":
                    this.UnicodeCollation = (string)null;
                    break;
                case "LockMode":
                    this.LockMode = (string)null;
                    break;
                case "SecurityMode":
                    this.SecurityMode = (string)null;
                    break;
                case "ServerType":
                    this.ServerType = (string)null;
                    break;
                case "ShowDeleted":
                    this.mCSHandler.ShowDeleted = false;
                    break;
                case "EncryptionPassword":
                    this.mCSHandler.EncryptionPassword = (string)null;
                    break;
                case "DbfsUseNulls":
                    this.mCSHandler.DbfsUseNulls = false;
                    break;
                case "FilterOptions":
                    this.FilterOptions = (string)null;
                    break;
                case "TrimTrailingSpaces":
                    this.mCSHandler.TrimTrailingSpaces = false;
                    break;
                case "Enlist":
                    this.mCSHandler.TransScopeEnlist = true;
                    break;
                case "Compression":
                    this.Compression = (string)null;
                    break;
                case "Shared":
                    this.mCSHandler.Shared = true;
                    break;
                case "ReadOnly":
                    this.mCSHandler.ReadOnly = false;
                    break;
                case "Data Source":
                    this.mCSHandler.DataSource = (string)null;
                    break;
                case "Initial Catalog":
                    this.mCSHandler.InitialCatalog = (string)null;
                    break;
                case "User ID":
                    this.mCSHandler.UserID = (string)null;
                    break;
                case "Password":
                    this.mCSHandler.Password = (string)null;
                    break;
                case "Pooling":
                    this.mCSHandler.Pooling = true;
                    break;
                case "Min Pool Size":
                    this.mCSHandler.MinPoolSize = 0;
                    break;
                case "Max Pool Size":
                    this.mCSHandler.MaxPoolSize = 100;
                    break;
                case "Connection Lifetime":
                    this.mCSHandler.LifeTime = 0;
                    break;
                case "CommType":
                    this.CommType = (string)null;
                    break;
                case "FIPS":
                    this.FIPS = false;
                    break;
                case "EncryptionType":
                    this.EncryptionType = (string)null;
                    break;
                case "DDPassword":
                    this.DDPassword = (string)null;
                    break;
                case "TLSCiphers":
                    this.TLSCiphers = (string)null;
                    break;
                case "TLSCertificate":
                    this.TLSCertificate = (string)null;
                    break;
                case "TLSCommonName":
                    this.TLSCommonName = (string)null;
                    break;
            }

            return flag;
        }

        private sealed class TableTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[4]
                    {
                        "ADT",
                        "VFP",
                        "CDX",
                        "NTX"
                    });
                return this.mcValues;
            }
        }

        private sealed class ServerTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[4]
                    {
                        "",
                        "REMOTE",
                        "LOCAL",
                        "AIS"
                    });
                return this.mcValues;
            }
        }

        private sealed class CharTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues =
                        new TypeConverter.StandardValuesCollection(
                            (ICollection)Enum.GetNames(typeof(ACE.AdsCharTypes)));
                return this.mcValues;
            }
        }

        private sealed class UnicodeCollationConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                {
                    try
                    {
                        if (AdsConnectionStringBuilder.maUnicodeNames == null)
                        {
                            AdsConnection adsConnection = new AdsConnection("data source=.;servertype=local;");
                            adsConnection.Open();
                            AdsCommand command = adsConnection.CreateCommand();
                            command.CommandText =
                                "select x.Name from ( execute procedure sp_getcollations(null) )x where x.UnicodeLocale is null;";
                            command.CommandType = CommandType.Text;
                            AdsDataReader adsDataReader = command.ExecuteReader();
                            AdsConnectionStringBuilder.maUnicodeNames = new ArrayList();
                            while (adsDataReader.Read())
                            {
                                AdsConnectionStringBuilder.maUnicodeNames.Add((object)adsDataReader.GetString(0)
                                    .Trim());
                                AdsConnectionStringBuilder.maUnicodeNames.Add(
                                    (object)(adsDataReader.GetString(0).Trim() + "_ads_ci"));
                            }

                            adsDataReader.Close();
                            adsConnection.Close();
                            adsConnection.Dispose();
                        }

                        this.mcValues =
                            new TypeConverter.StandardValuesCollection(
                                (ICollection)AdsConnectionStringBuilder.maUnicodeNames);
                    }
                    catch
                    {
                        this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new object[0]);
                    }
                }

                return this.mcValues;
            }
        }

        private sealed class LockModeTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[2]
                    {
                        "PROPRIETARY",
                        "COMPATIBLE"
                    });
                return this.mcValues;
            }
        }

        private sealed class SecurityModeTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[2]
                    {
                        "IGNORERIGHTS",
                        "CHECKRIGHTS"
                    });
                return this.mcValues;
            }
        }

        private sealed class CommTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[5]
                    {
                        "",
                        "UDP_IP",
                        "TCP_IP",
                        "IPX",
                        "TLS"
                    });
                return this.mcValues;
            }
        }

        private sealed class FilterOptionsTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[2]
                    {
                        "IGNORE_WHEN_COUNTING",
                        "RESPECT_WHEN_COUNTING"
                    });
                return this.mcValues;
            }
        }

        private sealed class CompressionTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[4]
                    {
                        "",
                        "INTERNET",
                        "ALWAYS",
                        "NEVER"
                    });
                return this.mcValues;
            }
        }

        private sealed class EncryptionTypeConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[4]
                    {
                        "",
                        "RC4",
                        "AES128",
                        "AES256"
                    });
                return this.mcValues;
            }
        }

        private sealed class TLSCiphersConverter : TypeConverter
        {
            private TypeConverter.StandardValuesCollection mcValues;

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

            public override TypeConverter.StandardValuesCollection GetStandardValues(
                ITypeDescriptorContext context)
            {
                if (this.mcValues == null)
                    this.mcValues = new TypeConverter.StandardValuesCollection((ICollection)new string[6]
                    {
                        "",
                        "AES128-SHA:AES256-SHA:RC4-MD5",
                        "AES128-SHA:AES256-SHA",
                        "AES128-SHA",
                        "AES256-SHA",
                        "RC4-MD5"
                    });
                return this.mcValues;
            }
        }
    }
}