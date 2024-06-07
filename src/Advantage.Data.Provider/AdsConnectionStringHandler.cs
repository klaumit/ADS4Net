using System;
using System.Collections;
using System.Globalization;
using AdvantageClientEngine;

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
            mapProps = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            mapProps.Add(nameof(TableType), (ushort)3);
            mapProps.Add(nameof(CharType), "ANSI");
            mapProps.Add(nameof(UnicodeCollation), "");
            mapProps.Add("LockMode", (ushort)1);
            mapProps.Add("SecurityMode", (ushort)2);
            mapProps.Add(nameof(ServerType), null);
            mapProps.Add(nameof(ShowDeleted), false);
            mapProps.Add("IncrementUserCount", false);
            mapProps.Add("StoredProcedureConnection", false);
            mapProps.Add(nameof(EncryptionPassword), null);
            mapProps.Add(nameof(DbfsUseNulls), false);
            mapProps.Add(nameof(FilterOptions),
                FilterOption.IgnoreWhenCounting);
            mapProps.Add(nameof(TrimTrailingSpaces), false);
            mapProps.Add(nameof(Compression), null);
            mapProps.Add("Data Source", null);
            mapProps.Add("Initial Catalog", null);
            mapProps.Add("User ID", null);
            mapProps.Add(nameof(Password), null);
            mapProps.Add(nameof(Shared), true);
            mapProps.Add(nameof(ReadOnly), false);
            mapProps.Add(nameof(Pooling), true);
            mapProps.Add("Min Pool Size", 0);
            mapProps.Add("Max Pool Size", 100);
            mapProps.Add("Connection Reset", true);
            mapProps.Add("Connection Lifetime", 0);
            mapProps.Add(nameof(ConnectionHandle), IntPtr.Zero);
            mapProps.Add("Connect Timeout", 15);
            mapProps.Add(nameof(CommType), null);
            mapProps.Add("Enlist", true);
            mapProps.Add(nameof(EncryptionType), null);
            mapProps.Add(nameof(DDPassword), null);
            mapProps.Add("FIPS", false);
            mapProps.Add(nameof(TLSCiphers), null);
            mapProps.Add(nameof(TLSCertificate), null);
            mapProps.Add(nameof(TLSCommonName), null);
        }

        public static void Initialize()
        {
            if (mbInitialized)
                return;
            var hashtable = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
            hashtable.Add("Advantage Table Type", "TableType");
            hashtable.Add("Advantage Character Data Type", "CharType");
            hashtable.Add("Advantage Locking Mode", "LockMode");
            hashtable.Add("Advantage Security Mode", "SecurityMode");
            hashtable.Add("Advantage Server Type", "ServerType");
            hashtable.Add("Show Deleted Records in DBF Tables with Advantage", "ShowDeleted");
            hashtable.Add("Increment User Count", "IncrementUserCount");
            hashtable.Add("Stored Procedure Connection", "StoredProcedureConnection");
            hashtable.Add("Advantage Encryption Password", "EncryptionPassword");
            hashtable.Add("Use NULL values in DBF Tables with Advantage", "DbfsUseNulls");
            hashtable.Add("Advantage Filter Options", "FilterOptions");
            hashtable.Add("Trim Trailing Spaces", "TrimTrailingSpaces");
            hashtable.Add("Advantage Compression", "Compression");
            hashtable.Add("TableType", "TableType");
            hashtable.Add("CharType", "CharType");
            hashtable.Add("UnicodeCollation", "UnicodeCollation");
            hashtable.Add("LockMode", "LockMode");
            hashtable.Add("SecurityMode", "SecurityMode");
            hashtable.Add("ServerType", "ServerType");
            hashtable.Add("ShowDeleted", "ShowDeleted");
            hashtable.Add("IncrementUserCount", "IncrementUserCount");
            hashtable.Add("StoredProcedureConnection", "StoredProcedureConnection");
            hashtable.Add("EncryptionPassword", "EncryptionPassword");
            hashtable.Add("DbfsUseNulls", "DbfsUseNulls");
            hashtable.Add("FilterOptions", "FilterOptions");
            hashtable.Add("TrimTrailingSpaces", "TrimTrailingSpaces");
            hashtable.Add("Compression", "Compression");
            hashtable.Add("Shared", "Shared");
            hashtable.Add("ReadOnly", "ReadOnly");
            hashtable.Add("Data Source", "Data Source");
            hashtable.Add("Initial Catalog", "Initial Catalog");
            hashtable.Add("User ID", "User ID");
            hashtable.Add("Password", "Password");
            hashtable.Add("ConnectionHandle", "ConnectionHandle");
            hashtable.Add("Pooling", "Pooling");
            hashtable.Add("Min Pool Size", "Min Pool Size");
            hashtable.Add("Max Pool Size", "Max Pool Size");
            hashtable.Add("Connection Reset", "Connection Reset");
            hashtable.Add("Connection Lifetime", "Connection Lifetime");
            hashtable.Add("Connect Timeout", "Connect Timeout");
            hashtable.Add("CommType", "CommType");
            hashtable.Add("Enlist", "Enlist");
            hashtable.Add("EncryptionType", "EncryptionType");
            hashtable.Add("DDPassword", "DDPassword");
            hashtable.Add("FIPS", "FIPS");
            hashtable.Add("TLSCiphers", "TLSCiphers");
            hashtable.Add("TLSCertificate", "TLSCertificate");
            hashtable.Add("TLSCommonName", "TLSCommonName");
            if (mbInitialized)
                return;
            mbInitialized = true;
            mapLongProps = hashtable;
        }

        public string MapPropertyName(string strProp)
        {
            if (!mbInitialized)
                Initialize();
            return (string)mapLongProps[strProp];
        }

        public void ParseConnectionString(string strConnect)
        {
            var flag = false;
            if (!mbInitialized)
                Initialize();
            strConnect = strConnect.Trim();
            mbHaveConnectionHandle = false;
            while (strConnect.Length > 0)
            {
                string strProp;
                string strValue;
                GetPropPair(ref strConnect, out strProp, out strValue);
                var mapLongProp = (string)mapLongProps[strProp];
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
                        mapProps[strProp] = strValue;
                        continue;
                    case "TableType":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "ADS_ADT":
                            case "ADT":
                                mapProps[strProp] = (ushort)3;
                                continue;
                            case "ADS_CDX":
                            case "CDX":
                                mapProps[strProp] = (ushort)2;
                                continue;
                            case "ADS_NTX":
                            case "NTX":
                                mapProps[strProp] = (ushort)1;
                                continue;
                            case "ADS_VFP":
                            case "VFP":
                                mapProps[strProp] = (ushort)4;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "CharType":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "ANSI":
                                mapProps[strProp] = ACE.AdsCharTypes.ADS_ANSI.ToString();
                                continue;
                            case "OEM":
                                mapProps[strProp] = ACE.AdsCharTypes.ADS_OEM.ToString();
                                continue;
                            default:
                                try
                                {
                                    Enum.Parse(typeof(ACE.AdsCharTypes),
                                        strValue.ToUpper(CultureInfo.InvariantCulture));
                                    mapProps[strProp] = strValue.Trim();
                                    continue;
                                }
                                catch
                                {
                                    throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                                strProp + " in connection string.");
                                }
                        }
                    case "UnicodeCollation":
                        mapProps[strProp] = strValue.Trim();
                        continue;
                    case "LockMode":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "ADS_PROPRIETARY_LOCKING":
                            case "PROPRIETARY":
                                mapProps[strProp] = (ushort)1;
                                continue;
                            case "ADS_COMPATIBLE_LOCKING":
                            case "COMPATIBLE":
                                mapProps[strProp] = (ushort)0;
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
                                mapProps[strProp] = (ushort)1;
                                continue;
                            case "ADS_IGNORERIGHTS":
                            case "IGNORERIGHTS":
                                mapProps[strProp] = (ushort)2;
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
                                mapProps[strProp] = true;
                                continue;
                            case "FALSE":
                                mapProps[strProp] = false;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "FilterOptions":
                        switch (strValue.ToUpper(CultureInfo.InvariantCulture))
                        {
                            case "IGNORE_WHEN_COUNTING":
                                mapProps[strProp] =
                                    FilterOption.IgnoreWhenCounting;
                                continue;
                            case "RESPECT_WHEN_COUNTING":
                                mapProps[strProp] =
                                    FilterOption.RespectWhenCounting;
                                continue;
                            default:
                                throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                            strProp + " in connection string.");
                        }
                    case "ConnectionHandle":
                        try
                        {
                            if (strValue.StartsWith("0x"))
                                mapProps[strProp] = (IntPtr)long.Parse(strValue.Substring(2),
                                    NumberStyles.AllowHexSpecifier);
                            else
                                mapProps[strProp] = (IntPtr)long.Parse(strValue);
                        }
                        catch
                        {
                            throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                        strProp + " in connection string.");
                        }

                        mbHaveConnectionHandle = true;
                        ushort pusType;
                        if (ACE.AdsGetHandleType((IntPtr)mapProps[strProp], out pusType) != 0U ||
                            pusType != 1 && pusType != 6)
                            throw new ArgumentException("Unrecognized value '" + strValue + "' for property " +
                                                        strProp + " in connection string.");
                        continue;
                    case "Min Pool Size":
                    case "Max Pool Size":
                    case "Connection Lifetime":
                    case "Connect Timeout":
                        var int32 = Convert.ToInt32(strValue);
                        mapProps[strProp] = int32 >= 0
                            ? (object)int32
                            : throw new ArgumentException("Invalid value for key '" + strProp + "'.");
                        continue;
                    case "BDP":
                        if (strValue.ToUpper(CultureInfo.InvariantCulture) == "TRUE")
                        {
                            flag = true;
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
            var minValue = char.MinValue;
            var length = strConnect.IndexOf('=');
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
                    var num2 = strConnect.IndexOf(minValue, 1);
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
            get => (ushort)mapProps[nameof(TableType)];
            set => mapProps[nameof(TableType)] = value;
        }

        internal string CharType
        {
            get => (string)mapProps[nameof(CharType)];
            set => mapProps[nameof(CharType)] = value;
        }

        internal string UnicodeCollation
        {
            get => (string)mapProps[nameof(UnicodeCollation)];
            set => mapProps[nameof(UnicodeCollation)] = value;
        }

        internal ushort LockType
        {
            get => (ushort)mapProps["LockMode"];
            set => mapProps["LockMode"] = value;
        }

        internal ushort CheckRights
        {
            get => (ushort)mapProps["SecurityMode"];
            set => mapProps["SecurityMode"] = value;
        }

        internal string ServerType
        {
            get => (string)mapProps[nameof(ServerType)];
            set => mapProps[nameof(ServerType)] = value;
        }

        internal bool ShowDeleted
        {
            get => (bool)mapProps[nameof(ShowDeleted)];
            set => mapProps[nameof(ShowDeleted)] = value;
        }

        internal bool DbfsUseNulls
        {
            get => (bool)mapProps[nameof(DbfsUseNulls)];
            set => mapProps[nameof(DbfsUseNulls)] = value;
        }

        internal string EncryptionPassword
        {
            get => (string)mapProps[nameof(EncryptionPassword)];
            set => mapProps[nameof(EncryptionPassword)] = value;
        }

        internal bool TrimTrailingSpaces
        {
            get => (bool)mapProps[nameof(TrimTrailingSpaces)];
            set => mapProps[nameof(TrimTrailingSpaces)] = value;
        }

        internal string Compression
        {
            get => (string)mapProps[nameof(Compression)];
            set => mapProps[nameof(Compression)] = value;
        }

        internal FilterOption FilterOptions
        {
            get { return (FilterOption)mapProps[nameof(FilterOptions)]; }
            set => mapProps[nameof(FilterOptions)] = value;
        }

        internal bool IncUserCount
        {
            get => (bool)mapProps["IncrementUserCount"];
            set => mapProps["IncrementUserCount"] = value;
        }

        internal bool StoredProcConn
        {
            get => (bool)mapProps["StoredProcedureConnection"];
            set => mapProps["StoredProcedureConnection"] = value;
        }

        internal bool Shared
        {
            get => (bool)mapProps[nameof(Shared)];
            set => mapProps[nameof(Shared)] = value;
        }

        internal bool ReadOnly
        {
            get => (bool)mapProps[nameof(ReadOnly)];
            set => mapProps[nameof(ReadOnly)] = value;
        }

        internal string DataSource
        {
            get => (string)mapProps["Data Source"];
            set => mapProps["Data Source"] = value;
        }

        internal string InitialCatalog
        {
            get => (string)mapProps["Initial Catalog"];
            set => mapProps["Initial Catalog"] = value;
        }

        internal string UserID
        {
            get => (string)mapProps["User ID"];
            set => mapProps["User ID"] = value;
        }

        internal string Password
        {
            get => (string)mapProps[nameof(Password)];
            set => mapProps[nameof(Password)] = value;
        }

        internal bool HaveConnectionHandle => mbHaveConnectionHandle;

        internal IntPtr ConnectionHandle => (IntPtr)mapProps[nameof(ConnectionHandle)];

        internal int LifeTime
        {
            get => (int)mapProps["Connection Lifetime"];
            set => mapProps["Connection Lifetime"] = value;
        }

        internal int MinPoolSize
        {
            get => (int)mapProps["Min Pool Size"];
            set => mapProps["Min Pool Size"] = value;
        }

        internal int MaxPoolSize
        {
            get => (int)mapProps["Max Pool Size"];
            set => mapProps["Max Pool Size"] = value;
        }

        internal bool ConnectionReset
        {
            get => (bool)mapProps["Connection Reset"];
            set => mapProps["Connection Reset"] = value;
        }

        internal bool Pooling
        {
            get => (bool)mapProps[nameof(Pooling)];
            set => mapProps[nameof(Pooling)] = value;
        }

        internal int ConnectTimeout
        {
            get => (int)mapProps["Connect Timeout"];
            set => mapProps["Connect Timeout"] = value;
        }

        internal bool TransScopeEnlist
        {
            get => (bool)mapProps["Enlist"];
            set => mapProps["Enlist"] = value;
        }

        internal string CommType
        {
            get => (string)mapProps[nameof(CommType)];
            set => mapProps[nameof(CommType)] = value;
        }

        internal string EncryptionType
        {
            get => (string)mapProps[nameof(EncryptionType)];
            set => mapProps[nameof(EncryptionType)] = value;
        }

        internal bool FIPSMode
        {
            get => (bool)mapProps["FIPS"];
            set => mapProps["FIPS"] = value;
        }

        internal string DDPassword
        {
            get => (string)mapProps[nameof(DDPassword)];
            set => mapProps[nameof(DDPassword)] = value;
        }

        internal string TLSCiphers
        {
            get => (string)mapProps[nameof(TLSCiphers)];
            set => mapProps[nameof(TLSCiphers)] = value;
        }

        internal string TLSCertificate
        {
            get => (string)mapProps[nameof(TLSCertificate)];
            set => mapProps[nameof(TLSCertificate)] = value;
        }

        internal string TLSCommonName
        {
            get => (string)mapProps[nameof(TLSCommonName)];
            set => mapProps[nameof(TLSCommonName)] = value;
        }

        public enum FilterOption
        {
            IgnoreWhenCounting = 1,
            RespectWhenCounting = 2,
        }
    }
}