using AdvantageClientEngine;
using System;
using System.Collections;
using System.Globalization;

namespace Advantage.Data.Provider
{
    internal class AdsConnectionStringHandler
    {
        public const string PropTableType = "TableType";
        public const string PropTableTypeLong = "Advantage Table Type";
        public const string PropCharType = "CharType";
        public const string PropCharTypeLong = "Advantage Character Data Type";
        public const string PropUnicodeCollation = "UnicodeCollation";
        public const string PropLockType = "LockMode";
        public const string PropLockTypeLong = "Advantage Locking Mode";
        public const string PropCheckRights = "SecurityMode";
        public const string PropCheckRightsLong = "Advantage Security Mode";
        public const string PropServerType = "ServerType";
        public const string PropServerTypeLong = "Advantage Server Type";
        public const string PropShowDeleted = "ShowDeleted";
        public const string PropShowDeletedLong = "Show Deleted Records in DBF Tables with Advantage";
        public const string PropIncUserCount = "IncrementUserCount";
        public const string PropIncUserCountLong = "Increment User Count";
        public const string PropStoredProcConn = "StoredProcedureConnection";
        public const string PropStoredProcConnLong = "Stored Procedure Connection";
        public const string PropEncryptionPassword = "EncryptionPassword";
        public const string PropEncryptionPasswordLong = "Advantage Encryption Password";
        public const string PropDbfsUseNulls = "DbfsUseNulls";
        public const string PropDbfsUseNullsLong = "Use NULL values in DBF Tables with Advantage";
        public const string PropFilterOptions = "FilterOptions";
        public const string PropFilterOptionsLong = "Advantage Filter Options";
        public const string PropTrimTrailingSpaces = "TrimTrailingSpaces";
        public const string PropTrimTrailingSpacesLong = "Trim Trailing Spaces";
        public const string PropCompression = "Compression";
        public const string PropCompressionLong = "Advantage Compression";
        public const string PropConnectionHandle = "ConnectionHandle";
        public const string PropTransScopeEnlist = "Enlist";
        public const string PropCommType = "CommType";
        public const string PropEncryptionType = "EncryptionType";
        public const string PropDDPassword = "DDPassword";
        public const string PropFIPSMode = "FIPS";
        public const string PropTLSCiphers = "TLSCiphers";
        public const string PropTLSCertificate = "TLSCertificate";
        public const string PropTLSCommonName = "TLSCommonName";
        public const string PropShared = "Shared";
        public const string PropReadOnly = "ReadOnly";
        public const string PropDataSource = "Data Source";
        public const string PropInitialCatalog = "Initial Catalog";
        public const string PropUserID = "User ID";
        public const string PropPassword = "Password";
        private const string PropBDP = "BDP";
        public const string PropPooling = "Pooling";
        public const string PropMinPoolSize = "Min Pool Size";
        public const string PropMaxPoolSize = "Max Pool Size";
        public const string PropConnectionReset = "Connection Reset";
        public const string PropConnectionLifetime = "Connection Lifetime";
        public const string PropConnectTimeout = "Connect Timeout";
        public const string TT_ADT = "ADT";
        public const string TT_CDX = "CDX";
        public const string TT_NTX = "NTX";
        public const string TT_VFP = "VFP";
        public const string CT_ANSI = "ANSI";
        public const string CT_OEM = "OEM";
        public const string LT_PROPRIETARY = "PROPRIETARY";
        public const string LT_COMPATIBLE = "COMPATIBLE";
        public const string SM_IGNORERIGHTS = "IGNORERIGHTS";
        public const string SM_CHECKRIGHTS = "CHECKRIGHTS";
        public const string ST_REMOTE = "REMOTE";
        public const string ST_LOCAL = "LOCAL";
        public const string ST_AIS = "AIS";
        public const string FO_IGNORE = "IGNORE_WHEN_COUNTING";
        public const string FO_RESPECT = "RESPECT_WHEN_COUNTING";
        public const string COMP_NEVER = "NEVER";
        public const string COMP_ALWAYS = "ALWAYS";
        public const string COMP_INTERNET = "INTERNET";
        public const string CMT_UDP = "UDP_IP";
        public const string CMT_TCP = "TCP_IP";
        public const string CMT_IPX = "IPX";
        public const string CMT_TLS = "TLS";
        public const int POOL_DEFAULT_MIN = 0;
        public const int POOL_DEFAULT_MAX = 100;
        public const int POOL_DEFAULT_LIFE = 0;
        public const string ET_RC4 = "RC4";
        public const string ET_AES128 = "AES128";
        public const string ET_AES256 = "AES256";
        public const string CS_ALL = "AES128-SHA:AES256-SHA:RC4-MD5";
        public const string CS_AES = "AES128-SHA:AES256-SHA";
        public const string CS_AES128 = "AES128-SHA";
        public const string CS_AES256 = "AES256-SHA";
        public const string CS_RC4 = "RC4-MD5";
        private bool mbHaveConnectionHandle;
        private static Hashtable mapLongProps;
        private static bool mbInitialized;
        private Hashtable mapProps;

        public AdsConnectionStringHandler()
        {
            this.mapProps = new Hashtable((IEqualityComparer)StringComparer.InvariantCultureIgnoreCase);
            this.mapProps.Add((object)nameof(TableType), (object)(ushort)3);
            this.mapProps.Add((object)nameof(CharType), (object)"ANSI");
            this.mapProps.Add((object)nameof(UnicodeCollation), (object)"");
            this.mapProps.Add((object)"LockMode", (object)(ushort)1);
            this.mapProps.Add((object)"SecurityMode", (object)(ushort)2);
            this.mapProps.Add((object)nameof(ServerType), (object)null);
            this.mapProps.Add((object)nameof(ShowDeleted), (object)false);
            this.mapProps.Add((object)"IncrementUserCount", (object)false);
            this.mapProps.Add((object)"StoredProcedureConnection", (object)false);
            this.mapProps.Add((object)nameof(EncryptionPassword), (object)null);
            this.mapProps.Add((object)nameof(DbfsUseNulls), (object)false);
            this.mapProps.Add((object)nameof(FilterOptions),
                (object)AdsConnectionStringHandler.FilterOption.IgnoreWhenCounting);
            this.mapProps.Add((object)nameof(TrimTrailingSpaces), (object)false);
            this.mapProps.Add((object)nameof(Compression), (object)null);
            this.mapProps.Add((object)"Data Source", (object)null);
            this.mapProps.Add((object)"Initial Catalog", (object)null);
            this.mapProps.Add((object)"User ID", (object)null);
            this.mapProps.Add((object)nameof(Password), (object)null);
            this.mapProps.Add((object)nameof(Shared), (object)true);
            this.mapProps.Add((object)nameof(ReadOnly), (object)false);
            this.mapProps.Add((object)nameof(Pooling), (object)true);
            this.mapProps.Add((object)"Min Pool Size", (object)0);
            this.mapProps.Add((object)"Max Pool Size", (object)100);
            this.mapProps.Add((object)"Connection Reset", (object)true);
            this.mapProps.Add((object)"Connection Lifetime", (object)0);
            this.mapProps.Add((object)nameof(ConnectionHandle), (object)IntPtr.Zero);
            this.mapProps.Add((object)"Connect Timeout", (object)15);
            this.mapProps.Add((object)nameof(CommType), (object)null);
            this.mapProps.Add((object)"Enlist", (object)true);
            this.mapProps.Add((object)nameof(EncryptionType), (object)null);
            this.mapProps.Add((object)nameof(DDPassword), (object)null);
            this.mapProps.Add((object)"FIPS", (object)false);
            this.mapProps.Add((object)nameof(TLSCiphers), (object)null);
            this.mapProps.Add((object)nameof(TLSCertificate), (object)null);
            this.mapProps.Add((object)nameof(TLSCommonName), (object)null);
        }

        public static void Initialize()
        {
            if (AdsConnectionStringHandler.mbInitialized)
                return;
            Hashtable hashtable = new Hashtable((IEqualityComparer)StringComparer.InvariantCultureIgnoreCase);
            hashtable.Add((object)"Advantage Table Type", (object)"TableType");
            hashtable.Add((object)"Advantage Character Data Type", (object)"CharType");
            hashtable.Add((object)"Advantage Locking Mode", (object)"LockMode");
            hashtable.Add((object)"Advantage Security Mode", (object)"SecurityMode");
            hashtable.Add((object)"Advantage Server Type", (object)"ServerType");
            hashtable.Add((object)"Show Deleted Records in DBF Tables with Advantage", (object)"ShowDeleted");
            hashtable.Add((object)"Increment User Count", (object)"IncrementUserCount");
            hashtable.Add((object)"Stored Procedure Connection", (object)"StoredProcedureConnection");
            hashtable.Add((object)"Advantage Encryption Password", (object)"EncryptionPassword");
            hashtable.Add((object)"Use NULL values in DBF Tables with Advantage", (object)"DbfsUseNulls");
            hashtable.Add((object)"Advantage Filter Options", (object)"FilterOptions");
            hashtable.Add((object)"Trim Trailing Spaces", (object)"TrimTrailingSpaces");
            hashtable.Add((object)"Advantage Compression", (object)"Compression");
            hashtable.Add((object)"TableType", (object)"TableType");
            hashtable.Add((object)"CharType", (object)"CharType");
            hashtable.Add((object)"UnicodeCollation", (object)"UnicodeCollation");
            hashtable.Add((object)"LockMode", (object)"LockMode");
            hashtable.Add((object)"SecurityMode", (object)"SecurityMode");
            hashtable.Add((object)"ServerType", (object)"ServerType");
            hashtable.Add((object)"ShowDeleted", (object)"ShowDeleted");
            hashtable.Add((object)"IncrementUserCount", (object)"IncrementUserCount");
            hashtable.Add((object)"StoredProcedureConnection", (object)"StoredProcedureConnection");
            hashtable.Add((object)"EncryptionPassword", (object)"EncryptionPassword");
            hashtable.Add((object)"DbfsUseNulls", (object)"DbfsUseNulls");
            hashtable.Add((object)"FilterOptions", (object)"FilterOptions");
            hashtable.Add((object)"TrimTrailingSpaces", (object)"TrimTrailingSpaces");
            hashtable.Add((object)"Compression", (object)"Compression");
            hashtable.Add((object)"Shared", (object)"Shared");
            hashtable.Add((object)"ReadOnly", (object)"ReadOnly");
            hashtable.Add((object)"Data Source", (object)"Data Source");
            hashtable.Add((object)"Initial Catalog", (object)"Initial Catalog");
            hashtable.Add((object)"User ID", (object)"User ID");
            hashtable.Add((object)"Password", (object)"Password");
            hashtable.Add((object)"ConnectionHandle", (object)"ConnectionHandle");
            hashtable.Add((object)"Pooling", (object)"Pooling");
            hashtable.Add((object)"Min Pool Size", (object)"Min Pool Size");
            hashtable.Add((object)"Max Pool Size", (object)"Max Pool Size");
            hashtable.Add((object)"Connection Reset", (object)"Connection Reset");
            hashtable.Add((object)"Connection Lifetime", (object)"Connection Lifetime");
            hashtable.Add((object)"Connect Timeout", (object)"Connect Timeout");
            hashtable.Add((object)"CommType", (object)"CommType");
            hashtable.Add((object)"Enlist", (object)"Enlist");
            hashtable.Add((object)"EncryptionType", (object)"EncryptionType");
            hashtable.Add((object)"DDPassword", (object)"DDPassword");
            hashtable.Add((object)"FIPS", (object)"FIPS");
            hashtable.Add((object)"TLSCiphers", (object)"TLSCiphers");
            hashtable.Add((object)"TLSCertificate", (object)"TLSCertificate");
            hashtable.Add((object)"TLSCommonName", (object)"TLSCommonName");
            if (AdsConnectionStringHandler.mbInitialized)
                return;
            AdsConnectionStringHandler.mbInitialized = true;
            AdsConnectionStringHandler.mapLongProps = hashtable;
        }

        public string MapPropertyName(string strProp)
        {
            if (!AdsConnectionStringHandler.mbInitialized)
                AdsConnectionStringHandler.Initialize();
            return (string)AdsConnectionStringHandler.mapLongProps[(object)strProp];
        }

        public void ParseConnectionString(string strConnect)
        {
            bool flag = false;
            if (!AdsConnectionStringHandler.mbInitialized)
                AdsConnectionStringHandler.Initialize();
            strConnect = strConnect.Trim();
            this.mbHaveConnectionHandle = false;
            while (strConnect.Length > 0)
            {
                string strProp;
                string strValue;
                AdsConnectionStringHandler.GetPropPair(ref strConnect, out strProp, out strValue);
                string mapLongProp = (string)AdsConnectionStringHandler.mapLongProps[(object)strProp];
                if (mapLongProp != null)
                    strProp = mapLongProp;
                switch (strProp)
                {
                    case "Data Source":
                    case "EncryptionPassword":
                    case "Initial Catalog":
                    case "User ID":
                    case "Password":
                    case "EncryptionType":
                    case "DDPassword":
                    case "TLSCiphers":
                    case "TLSCertificate":
                    case "TLSCommonName":
                    case "CommType":
                    case "Compression":
                    case "ServerType":
                        this.mapProps[(object)strProp] = (object)strValue;
                        continue;
                    case "TableType":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "ADS_ADT":
                            case "ADT":
                                this.mapProps[(object)strProp] = (object)(ushort)3;
                                continue;
                            case "ADS_CDX":
                            case "CDX":
                                this.mapProps[(object)strProp] = (object)(ushort)2;
                                continue;
                            case "ADS_NTX":
                            case "NTX":
                                this.mapProps[(object)strProp] = (object)(ushort)1;
                                continue;
                            case "ADS_VFP":
                            case "VFP":
                                this.mapProps[(object)strProp] = (object)(ushort)4;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "CharType":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "ANSI":
                                this.mapProps[(object)strProp] = (object)ACE.AdsCharTypes.ADS_ANSI.ToString();
                                continue;
                            case "OEM":
                                this.mapProps[(object)strProp] = (object)ACE.AdsCharTypes.ADS_OEM.ToString();
                                continue;
                            default:
                                try
                                {
                                    Enum.Parse(typeof(ACE.AdsCharTypes),
                                        strValue.ToUpper(CultureInfo.InvariantCulture));
                                    this.mapProps[(object)strProp] = (object)strValue.Trim();
                                    continue;
                                }
                                catch
                                {
                                    throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                                strProp + " in connection string.");
                                }
                        }
                    case "UnicodeCollation":
                        this.mapProps[(object)strProp] = (object)strValue.Trim();
                        continue;
                    case "LockMode":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "ADS_PROPRIETARY_LOCKING":
                            case "PROPRIETARY":
                                this.mapProps[(object)strProp] = (object)(ushort)1;
                                continue;
                            case "ADS_COMPATIBLE_LOCKING":
                            case "COMPATIBLE":
                                this.mapProps[(object)strProp] = (object)(ushort)0;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "SecurityMode":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "ADS_CHECKRIGHTS":
                            case "CHECKRIGHTS":
                                this.mapProps[(object)strProp] = (object)(ushort)1;
                                continue;
                            case "ADS_IGNORERIGHTS":
                            case "IGNORERIGHTS":
                                this.mapProps[(object)strProp] = (object)(ushort)2;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "ShowDeleted":
                    case "IncrementUserCount":
                    case "StoredProcedureConnection":
                    case "DbfsUseNulls":
                    case "TrimTrailingSpaces":
                    case "Shared":
                    case "ReadOnly":
                    case "Pooling":
                    case "Connection Reset":
                    case "Enlist":
                    case "FIPS":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "TRUE":
                                this.mapProps[(object)strProp] = (object)true;
                                continue;
                            case "FALSE":
                                this.mapProps[(object)strProp] = (object)false;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "FilterOptions":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "IGNORE_WHEN_COUNTING":
                                this.mapProps[(object)strProp] =
                                    (object)AdsConnectionStringHandler.FilterOption.IgnoreWhenCounting;
                                continue;
                            case "RESPECT_WHEN_COUNTING":
                                this.mapProps[(object)strProp] =
                                    (object)AdsConnectionStringHandler.FilterOption.RespectWhenCounting;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "ConnectionHandle":
                        try
                        {
                            if (strValue.StartsWith("0x"))
                                this.mapProps[(object)strProp] = (object)(IntPtr)long.Parse(strValue.Substring(2),
                                    NumberStyles.AllowHexSpecifier);
                            else
                                this.mapProps[(object)strProp] = (object)(IntPtr)long.Parse(strValue);
                        }
                        catch
                        {
                            throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                        strProp + " in connection string.");
                        }

                        this.mbHaveConnectionHandle = true;
                        ushort pusType;
                        if (ACE.AdsGetHandleType((IntPtr)this.mapProps[(object)strProp], out pusType) != 0U ||
                            pusType != (ushort)1 && pusType != (ushort)6)
                            throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                        strProp + " in connection string.");
                        continue;
                    case "Min Pool Size":
                    case "Max Pool Size":
                    case "Connection Lifetime":
                    case "Connect Timeout":
                        int int32 = Convert.ToInt32(strValue);
                        this.mapProps[(object)strProp] = int32 >= 0
                            ? (object)int32
                            : throw new ArgumentException("Invalid value for key '" + strProp + "'.");
                        continue;
                    case "BDP":
                        if (strValue.ToUpper(CultureInfo.InvariantCulture) == "TRUE")
                        {
                            flag = true;
                            continue;
                        }

                        continue;
                    default:
                        if (!flag)
                            throw new ArgumentException("Unrecognized property '" + strProp +
                                                        "' in connection string.");
                        continue;
                }
            }
        }

        private static void GetPropPair(ref string strConnect, out string strProp, out string strValue)
        {
            char minValue = char.MinValue;
            int length = strConnect.IndexOf('=');
            if (length == -1)
                throw new ArgumentException("Separator '=' not found while parsing connection string.");
            strProp = strConnect.Substring(0, length).Trim();
            strConnect = strConnect.Remove(0, length + 1).Trim();
            if (strConnect.Length == 0)
            {
                strValue = "";
            }
            else
            {
                if (strConnect[0] == '\'' || strConnect[0] == '"')
                    minValue = strConnect[0];
                int num1;
                if (minValue == char.MinValue)
                {
                    num1 = strConnect.IndexOf(';');
                    if (num1 == -1)
                        num1 = strConnect.Length;
                }
                else
                {
                    int num2 = strConnect.IndexOf(minValue, 1);
                    if (num2 == -1)
                        throw new ArgumentException("Closing quote for property " + strProp +
                                                    " not found in connection string.");
                    num1 = num2 + 1;
                }

                strValue = strConnect.Substring(0, num1).Trim();
                if (minValue != char.MinValue)
                    strValue = strValue.Trim(minValue);
                while (num1 < strConnect.Length && (strConnect[num1] == ';' || char.IsWhiteSpace(strConnect[num1])))
                    ++num1;
                strConnect = strConnect.Remove(0, num1).Trim();
            }
        }

        internal ushort TableType
        {
            get => (ushort)this.mapProps[(object)nameof(TableType)];
            set => this.mapProps[(object)nameof(TableType)] = (object)value;
        }

        internal string CharType
        {
            get => (string)this.mapProps[(object)nameof(CharType)];
            set => this.mapProps[(object)nameof(CharType)] = (object)value;
        }

        internal string UnicodeCollation
        {
            get => (string)this.mapProps[(object)nameof(UnicodeCollation)];
            set => this.mapProps[(object)nameof(UnicodeCollation)] = (object)value;
        }

        internal ushort LockType
        {
            get => (ushort)this.mapProps[(object)"LockMode"];
            set => this.mapProps[(object)"LockMode"] = (object)value;
        }

        internal ushort CheckRights
        {
            get => (ushort)this.mapProps[(object)"SecurityMode"];
            set => this.mapProps[(object)"SecurityMode"] = (object)value;
        }

        internal string ServerType
        {
            get => (string)this.mapProps[(object)nameof(ServerType)];
            set => this.mapProps[(object)nameof(ServerType)] = (object)value;
        }

        internal bool ShowDeleted
        {
            get => (bool)this.mapProps[(object)nameof(ShowDeleted)];
            set => this.mapProps[(object)nameof(ShowDeleted)] = (object)value;
        }

        internal bool DbfsUseNulls
        {
            get => (bool)this.mapProps[(object)nameof(DbfsUseNulls)];
            set => this.mapProps[(object)nameof(DbfsUseNulls)] = (object)value;
        }

        internal string EncryptionPassword
        {
            get => (string)this.mapProps[(object)nameof(EncryptionPassword)];
            set => this.mapProps[(object)nameof(EncryptionPassword)] = (object)value;
        }

        internal bool TrimTrailingSpaces
        {
            get => (bool)this.mapProps[(object)nameof(TrimTrailingSpaces)];
            set => this.mapProps[(object)nameof(TrimTrailingSpaces)] = (object)value;
        }

        internal string Compression
        {
            get => (string)this.mapProps[(object)nameof(Compression)];
            set => this.mapProps[(object)nameof(Compression)] = (object)value;
        }

        internal AdsConnectionStringHandler.FilterOption FilterOptions
        {
            get { return (AdsConnectionStringHandler.FilterOption)this.mapProps[(object)nameof(FilterOptions)]; }
            set => this.mapProps[(object)nameof(FilterOptions)] = (object)value;
        }

        internal bool IncUserCount
        {
            get => (bool)this.mapProps[(object)"IncrementUserCount"];
            set => this.mapProps[(object)"IncrementUserCount"] = (object)value;
        }

        internal bool StoredProcConn
        {
            get => (bool)this.mapProps[(object)"StoredProcedureConnection"];
            set => this.mapProps[(object)"StoredProcedureConnection"] = (object)value;
        }

        internal bool Shared
        {
            get => (bool)this.mapProps[(object)nameof(Shared)];
            set => this.mapProps[(object)nameof(Shared)] = (object)value;
        }

        internal bool ReadOnly
        {
            get => (bool)this.mapProps[(object)nameof(ReadOnly)];
            set => this.mapProps[(object)nameof(ReadOnly)] = (object)value;
        }

        internal string DataSource
        {
            get => (string)this.mapProps[(object)"Data Source"];
            set => this.mapProps[(object)"Data Source"] = (object)value;
        }

        internal string InitialCatalog
        {
            get => (string)this.mapProps[(object)"Initial Catalog"];
            set => this.mapProps[(object)"Initial Catalog"] = (object)value;
        }

        internal string UserID
        {
            get => (string)this.mapProps[(object)"User ID"];
            set => this.mapProps[(object)"User ID"] = (object)value;
        }

        internal string Password
        {
            get => (string)this.mapProps[(object)nameof(Password)];
            set => this.mapProps[(object)nameof(Password)] = (object)value;
        }

        internal bool HaveConnectionHandle => this.mbHaveConnectionHandle;

        internal IntPtr ConnectionHandle => (IntPtr)this.mapProps[(object)nameof(ConnectionHandle)];

        internal int LifeTime
        {
            get => (int)this.mapProps[(object)"Connection Lifetime"];
            set => this.mapProps[(object)"Connection Lifetime"] = (object)value;
        }

        internal int MinPoolSize
        {
            get => (int)this.mapProps[(object)"Min Pool Size"];
            set => this.mapProps[(object)"Min Pool Size"] = (object)value;
        }

        internal int MaxPoolSize
        {
            get => (int)this.mapProps[(object)"Max Pool Size"];
            set => this.mapProps[(object)"Max Pool Size"] = (object)value;
        }

        internal bool ConnectionReset
        {
            get => (bool)this.mapProps[(object)"Connection Reset"];
            set => this.mapProps[(object)"Connection Reset"] = (object)value;
        }

        internal bool Pooling
        {
            get => (bool)this.mapProps[(object)nameof(Pooling)];
            set => this.mapProps[(object)nameof(Pooling)] = (object)value;
        }

        internal int ConnectTimeout
        {
            get => (int)this.mapProps[(object)"Connect Timeout"];
            set => this.mapProps[(object)"Connect Timeout"] = (object)value;
        }

        internal bool TransScopeEnlist
        {
            get => (bool)this.mapProps[(object)"Enlist"];
            set => this.mapProps[(object)"Enlist"] = (object)value;
        }

        internal string CommType
        {
            get => (string)this.mapProps[(object)nameof(CommType)];
            set => this.mapProps[(object)nameof(CommType)] = (object)value;
        }

        internal string EncryptionType
        {
            get => (string)this.mapProps[(object)nameof(EncryptionType)];
            set => this.mapProps[(object)nameof(EncryptionType)] = (object)value;
        }

        internal bool FIPSMode
        {
            get => (bool)this.mapProps[(object)"FIPS"];
            set => this.mapProps[(object)"FIPS"] = (object)value;
        }

        internal string DDPassword
        {
            get => (string)this.mapProps[(object)nameof(DDPassword)];
            set => this.mapProps[(object)nameof(DDPassword)] = (object)value;
        }

        internal string TLSCiphers
        {
            get => (string)this.mapProps[(object)nameof(TLSCiphers)];
            set => this.mapProps[(object)nameof(TLSCiphers)] = (object)value;
        }

        internal string TLSCertificate
        {
            get => (string)this.mapProps[(object)nameof(TLSCertificate)];
            set => this.mapProps[(object)nameof(TLSCertificate)] = (object)value;
        }

        internal string TLSCommonName
        {
            get => (string)this.mapProps[(object)nameof(TLSCommonName)];
            set => this.mapProps[(object)nameof(TLSCommonName)] = (object)value;
        }

        public enum FilterOption
        {
            IgnoreWhenCounting = 1,
            RespectWhenCounting = 2,
        }
    }
}