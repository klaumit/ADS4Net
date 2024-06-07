using System;
using System.Runtime.InteropServices;

namespace AdvantageClientEngine
{
    internal class ACEUNPUB
    {
        public const ushort MAX_STRING_CHECK = 5000;
        public const ushort ERROR_FILE_NOT_FOUND = 2;
        public const ushort ADS_FOXGENERAL = 51;
        public const ushort ADS_FOXPICTURE = 52;
        public const ushort ADS_AQE_SERVER = 32768;
        public const ushort ADS_DICTIONARY_SERVER = 16384;
        public const ushort ADS_INTERNAL_CONNECTION = 8192;
        public const short ADS_LOCAL_ASA_CONNECTION = -1;
        public const ushort ADS_READ_NEXT = 1;
        public const ushort ADS_READ_PREV = 2;
        public const ushort ADS_DD_INDEX_FILE_PATH_UNPUB = 6900;
        public const ushort ADS_DD_INDEX_FILE_TYPE_UNPUB = 6901;
        public const ushort ADS_DD_USER_KEYS = 6902;
        public const ushort ADS_DD_INDEX_FILE_RELATIVE_PATH_UNPUB = 6903;
        public const ushort ADS_DD_USER_KEYS_AND_UPDATE = 6904;
        public const ushort ADS_DD_DATABASE_ID = 6905;
        public const ushort ADS_DD_INDEX_FILE_PAGESIZE_UNPUB = 6906;
        public const ushort ADS_DD_DEPLOY_PASSWORD = 6907;
        public const ushort ADS_DD_USER_ENABLE_INTERNET = 6908;
        public const ushort ADS_DD_LINK_AUTHENTICATE = 6909;
        public const ushort ADS_DD_PROC_EXECUTE_PRIV = 6910;
        public const ushort ADS_DD_REFRESH_TABLE_PASSWORD = 6911;
        public const ushort ADS_DD_UPDATE_ADMIN_KEY = 6912;
        public const ushort ADS_DD_TABLE_OPTIONS = 6913;
        public const ushort ADS_DD_FIELD_NAME_UNPUB = 6914;
        public const ushort ADS_DD_USER_PASSWORD_ENCRYPTED = 6931;
        public const ushort ADS_DD_ADMIN_PASSWORD_ENCRYPTED = 6932;
        public const ushort ADS_DD_ENCRYPT_TABLE_PASSWORD_ENCRYPTED = 6933;
        public const ushort ADS_DD_DISABLE_USER_INTERNET = 6934;
        public const ushort ADS_DD_USER_LOGINS_DISABLED = 6935;
        public const ushort ADS_DD_SUBSCR_PASSWORD_ENCRYPTED = 6936;
        public const ushort ADS_DD_READ_OBJECT_NAME = 6937;
        public const ushort ADS_DD_FUNCTION_INPUT_PARAM_COUNT = 6938;
        public const ushort ADS_DD_FUNCTION_INPUT = 6939;
        public const ushort ADS_DD_FUNCTION_OUTPUT = 6940;
        public const ushort ADS_DD_FUNCTION_SCRIPT = 6941;
        public const ushort ADS_DD_FUNCTION_EVAL_PERMISSION = 6942;
        public const ushort ADS_DD_LOCK_OBJECT = 6943;
        public const ushort ADS_DD_UNLOCK_OBJECT = 6944;
        public const ushort ADS_DD_UNLOCK_OBJECT_CANCEL_UPDATE = 6945;
        public const ushort ADS_DD_DATABASE_ID_UPDATE = 6946;
        public const ushort ADS_DD_USER_DB_ROLES = 6947;
        public const ushort ADS_DD_USER_UPDATED_DB_ROLES = 6948;
        public const ushort ADS_DD_VERIFY_DEBUG_PERMISSION = 6949;
        public const ushort ADS_DD_VERIFY_MISSING_PERMISSIONS = 6950;
        public const ushort ADS_DD_VERIFY_ALTDROP_PERMISSIONS = 6951;
        public const ushort ADS_DD_DELETE_INDEX = 6952;
        public const ushort ADS_DD_ADD_INDEX = 6953;
        public const ushort ADS_DD_SET_TABLE_TXN_FREE_UNPUB = 6954;
        public const ushort ADS_DD_FUNCTION_SCRIPT_W = 6955;
        public const uint ADS_DD_OBJ_RIGHT_WRITE = 2;
        public const uint ADS_DD_TABLE_ENCRYPTED = 1;
        public const uint ADS_DD_TABLE_IS_OEM = 2;
        public const uint ADS_DD_TABLE_ALLOW_SEARCH_NRC = 4;
        public const uint ADS_DD_TABLE_STATIC_CURSOR_ONLY = 8;
        public const uint ADS_DD_TABLE_IGNORES_TRANS = 16;
        public const uint ADS_ALTER_OBJECT = 2147483648;
        public const ushort ADS_LOGICAL_AND = 1;
        public const ushort ADS_LOGICAL_OR = 2;
        public const ushort AOF_NORMAL_TYPE = 1;
        public const ushort AOF_CURSOR_TYPE = 2;
        public const char ADS_LINK_TABLE_DELIMITOR = ':';
        public const char ADS_PACKAGE_DELIMITOR = ':';
        public const char ADS_QUALIFIED_NAME_DELIMITOR = ':';
        public const ushort TRIG_BEFORE_UPDATE = 1;
        public const ushort TRIG_AFTER_UPDATE = 2;
        public const ushort TRIG_CONFLICT_UPDATE = 3;
        public const string AQE_STMT_NO_JOIN_ORDER_OPT = "NOJOINORDEROPT";
        public const string AQE_STMT_NO_PUSHDOWN_SORT_OPT = "NOPUSHDOWNSORTOPT";
        public const string AQE_STMT_NO_EXECUTION_OPT = "NOEXECUTIONOPT";
        public const string AQE_STMT_NO_MISC_OPT = "NOMISCOPT";
        public const string AQE_STMT_NO_SUBQUERY_OPT = "NOSUBQUERYOPT";
        public const uint AQE_NO_TOPX = 4294967295;
        public const uint ADS_DD_DISABLE_READ_PROP_CHK = 1;
        public const uint ADS_DD_DISABLE_SET_PROP_CHK = 2;
        public const uint ADS_DD_DISABLE_CREATE_RIGHTS_CHK = 4;
        public const ushort ADS_SET_LARGE_BLOCK_READS = 1;
        public const ushort ADS_SET_BATCH_INSERTS = 2;
        public const ushort ADS_UNIQUE_KEY_ENFORCEMENT = 3;
        public const ushort ADS_RI_ENFORCEMENT = 4;
        public const ushort ADS_AUTO_INCREMENT_ENFORCEMENT = 5;
        public const ushort ADS_MOVE_SERVER_OP_COUNT = 6;
        public const ushort ADS_SET_TRIG_IGNORE_EVENT = 7;
        public const ushort ADS_VERIFY_FTS_INDEXES = 8;
        public const ushort ADS_SET_FORCE_CLOSED = 9;
        public const ushort ADS_SET_NOTRANS_TABLE = 10;
        public const ushort ADS_SET_DDCONN_TRUEUSER = 11;
        public const ushort ADS_SET_OUTPUT_TABLE = 12;
        public const ushort ADS_GET_QUERY_ELEMENTS = 13;
        public const ushort ADS_SKIP_BEGIN_TRIGS = 14;
        public const ushort ADS_SET_DISABLE_PERMISSION = 16;
        public const ushort ADS_ADD_DISABLE_PERMISSION = 17;
        public const ushort ADS_DEBUG_CONNECTION = 18;
        public const ushort ADS_GET_KEY_VALUES = 19;
        public const ushort ADS_FREE_KEY_VALUES = 20;
        public const ushort ADS_SET_DEBUG_MASK = 21;
        public const ushort ADS_VERIFY_ADT = 22;
        public const ushort ADS_SET_NO_RI = 23;
        public const ushort ADS_VERIFY_MEMORY_TABLE = 24;
        public const ushort ADS_ALLOW_SMC_CONNECTIONS = 25;
        public const ushort ADS_SET_CONNINFO_INDEX = 26;
        public const ushort ADS_RETRY_ADS_CONNECTS = 27;
        public const ushort ADS_PUSH_ERROR_STACK = 28;
        public const ushort ADS_POP_ERROR_STACK = 29;
        public const ushort ADS_USE_PTHREAD_FOR_KA = 30;
        public const ushort ADS_SET_REPLICATION_INFO = 31;
        public const ushort ADS_GET_SERVER_USER_ID = 32;
        public const ushort ADS_IGNORE_CS_TESTS = 33;
        public const ushort ADS_ENFORCE_MODTIME_VALUES = 34;
        public const ushort ADS_ENFORCE_ROWVERSION_VALUES = 35;
        public const ushort ADS_DUMP_REPLICATION_CACHE = 36;
        public const ushort ADS_GET_VIEW_STMT = 37;
        public const ushort ADS_IN_PROC_OR_TRIG = 38;
        public const ushort ADS_VERIFY_SCRIPT = 39;
        public const ushort ADS_SET_MAX_CACHE_MEMORY = 40;
        public const ushort ADS_PERSIST_CACHED_TABLE = 41;
        public const ushort ADS_SET_VALUES_TABLE_FLAG = 42;
        public const ushort ADS_GET_VALUES_TABLE_FLAG = 43;
        public const ushort ADS_VERIFY_EXTERNAL_CALL_HANDLE = 44;
        public const ushort ADS_GET_SERVER_RELEASE_BUILD = 45;
        public const ushort ADS_SET_AVAIL_REINDEX_RAM = 46;
        public const ushort ADS_SET_AVAIL_REINDEX_HDD = 47;
        public const ushort ADS_GET_REINDEX_GROUP_NUM = 48;
        public const ushort ADS_SET_REINDEX_SORTBUFFER_LEN = 49;
        public const ushort ADS_SET_PULLING_DEBUG_TRIGGER = 50;
        public const ushort ADS_FLIP_DESCEND_FLAG = 51;
        public const ushort ADS_GET_ACE_ID_STR = 52;
        public const ushort ADS_GET_SERVER_COLLATION_ID = 53;
        public const ushort ADS_SET_REPLICATION_STMT = 54;
        public const ushort ADS_GET_REPLICATION_STMT = 55;
        public const ushort ADS_GET_AOF_PLAN = 56;
        public const ushort ADS_GET_REP_DBID = 57;
        public const ushort ADS_SET_REP_DBID = 58;
        public const ushort ADS_GET_ADD_PATH = 59;
        public const ushort ADS_NONEXCLUSIVE_PROPRIETARY = 60;
        public const ushort ADS_GET_TRIGGER_PULL_INFO = 61;
        public const ushort ADS_SET_LPDBC_SP_TYPE_PTR = 62;
        public const ushort ADS_USE_MEMO_READ_LOCKS = 63;
        public const ushort ADS_USE_MEMO_READ_LOCKS_WA = 64;
        public const ushort ADS_USE_MEMO_READ_LOCKS_TABLE = 65;
        public const ushort ADS_USE_MEMO_READ_LOCKS_USER = 66;
        public const ushort ADS_CANCEL_USER_QUERY_EXACT = 67;
        public const ushort ADS_GET_FILE_SIZE = 68;
        public const ushort ADS_GET_RAWFILE_CACHE_STATS = 69;
        public const ushort ADS_DUMP_DLL_CACHE = 70;
        public const ushort ADS_GET_TRANSACTION_COUNT = 71;
        public const ushort ADS_DUMP_TMP_FILE_POOL = 72;
        public const ushort ADS_SET_EQ_WINDOW_SIZE = 73;
        public const ushort ADS_SET_EQ_MAX_THREADS = 74;
        public const ushort ADS_GET_CODEPAGE = 75;
        public const ushort ADS_GET_ICU_LIB_HANDLE = 76;
        public const ushort ADS_GET_CURSOR_AOF_OPT_LEVEL = 77;
        public const ushort ADS_GET_VIEW_STMT_ENCODING = 78;
        public const ushort ADS_GET_REP_CONN_TYPE = 79;
        public const ushort ADS_SET_ENCRYPTION_TYPE = 80;
        public const ushort ADS_GET_COLLATION_INFO = 81;
        public const ushort ADS_DOES_TABLE_SUPPORT_AES = 82;
        public const ushort ADS_GET_ENCRYPTION_TYPE = 83;
        public const ushort ADS_SET_RANDOM_KEY = 84;
        public const ushort ADS_DUMP_AES_KEY_STORE = 85;
        public const ushort ADS_SET_DEFAULT_FIPS_MODE = 86;
        public const ushort ADS_STRONG_ENCRYPTION_SUPPORT = 87;
        public const ushort ADS_SET_DBCAPI_BUFFER = 88;
        public const ushort ADS_GET_DBCAPI_BUFFER = 89;
        public const ushort ADS_IS_FIRST_FETCH = 90;
        public const ushort ADS_DBCAPI_CANCEL = 91;
        public const ushort ADS_GET_HEADER_LENGTH = 92;
        public const ushort TRIG_NOT_VALUE_TABLE = 0;
        public const ushort TRIG_OLD_TABLE = 1;
        public const ushort TRIG_NEW_TABLE = 2;
        public const ushort TRIG_ERROR_TABLE = 3;
        public const ushort BACKUP_SEVERITY_HIGH = 10;
        public const ushort BACKUP_SEVERITY_MEDHIGH = 7;
        public const ushort BACKUP_SEVERITY_MED = 5;
        public const ushort BACKUP_SEVERITY_LOW = 1;
        public const ushort BACKUP_SEVERITY_NONE = 0;
        public const string BACKUP_FREEPASSWDALL = "__AllFreeTablePassword";
        public const ushort ADS_NORMAL_RA_CACHE_SIZE = 10;
        public const ushort ADS_AGGRESSIVE_RA_CACHE_SIZE = 100;
        public const uint ADS_BEFORE_INSERT_TRIG = 1;
        public const uint ADS_INSTEADOF_INSERT_TRIG = 2;
        public const uint ADS_AFTER_INSERT_TRIG = 4;
        public const uint ADS_BEFORE_UPDATE_TRIG = 8;
        public const uint ADS_INSTEADOF_UPDATE_TRIG = 16;
        public const uint ADS_AFTER_UPDATE_TRIG = 32;
        public const uint ADS_BEFORE_DELETE_TRIG = 64;
        public const uint ADS_INSTEADOF_DELETE_TRIG = 128;
        public const uint ADS_AFTER_DELETE_TRIG = 256;
        public const uint ADS_CONFLICTON_UPDATE_TRIG = 512;
        public const uint ADS_CONFLICTON_DELETE_TRIG = 1024;
        public const uint ADS_DCM_ALL = 4294967295;
        public const uint ADS_DCM_ADT_HEADER = 1;
        public const uint ADS_DCM_ADT_RECYCLE = 2;
        public const uint ADS_DCM_ADM = 4;
        public const uint ADS_DD_NATIVE_ENCODING = 0;
        public const uint ADS_DD_ENCODE_ANSI = 4;
        public const uint ADS_DD_ENCODE_UTF8 = 32;
        public const uint ADS_DD_ENCODE_UTF8_WITHOUT_BOM = 64;
        public const uint ADS_DD_ENCODE_UTF16 = 256;
        public const uint ADS_CREATE_MEMTABLE = 1024;
        public const uint ADS_DONT_ADD_TO_MEMTABLE_LIST = 2048;
        public const uint ADS_RETURN_ID_DROP_TABLE = 32768;
        public const uint ADS_TEMP_CURSOR_TABLE = 524288;
        public const uint ADS_TEMP_TABLE_ANY_NAME = 1048576;
        public const uint ADS_OPEN_TABLE_USING_SQL = 2097152;
        public const uint ADS_NO_TRANSACTION = 4194304;
        public const uint ADS_CREATE_TABLE_OVERWRITE = 8388608;
        public const uint ADS_DONT_ENFORCE_RI = 16777216;
        public const uint ADS_CREATE_ADD_FILE = 33554432;
        public const uint ADS_DONT_OPEN_VIEW = 67108864;
        public const uint ADS_DONT_GO_TOP = 134217728;
        public const uint ADS_DONT_OPEN_INDEXES = 268435456;
        public const uint ADS_LINK_FULLPATH_IN_PARAM = 16777216;
        public const uint ADS_LINK_MODIFY = 33554432;
        public const uint ADS_DISABLE_CONNECTION_CONCURRENCY = 1;
        public const uint ADS_CURRENT_USER = 1;
        public const uint ADS_ALL_USERS = 2;
        public const uint ADS_CACHE_NEW_TABLE = 1;
        public const uint ADS_RETRIEVE_NULL_FLAG = 1;
        public const uint ADS_UPDATE_NULL_FLAG = 2;
        public const uint ADS_UPDATE_FULL_FLAG = 3;
        public const uint ADS_RETRIEVE_FULL_FLAG = 4;
        public const ushort ADS_DONT_ADD_TO_DD = 69;
        public const ushort ADS_RAWFILE_FOR_HOLD = 1;

        [DllImport("ace32.dll", EntryPoint = "AdsBuildKeyFromRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsBuildKeyFromRecord_32(
        IntPtr hTag,
        string mpucRecBuffer,
        uint ulRecordLen,
        [In, Out] char[] pucKey,
        ref ushort pusKeyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsBuildKeyFromRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsBuildKeyFromRecord_64(
        IntPtr hTag,
        string mpucRecBuffer,
        uint ulRecordLen,
        [In, Out] char[] pucKey,
        ref ushort pusKeyLen);

        public static uint AdsBuildKeyFromRecord(
        IntPtr hTag,
        string mpucRecBuffer,
        uint ulRecordLen,
        char[] pucKey,
        ref ushort pusKeyLen)
        {
            return IntPtr.Size == 4 ? AdsBuildKeyFromRecord_32(hTag, mpucRecBuffer, ulRecordLen, pucKey, ref pusKeyLen) : AdsBuildKeyFromRecord_64(hTag, mpucRecBuffer, ulRecordLen, pucKey, ref pusKeyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsClearLastError", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearLastError_32();

        [DllImport("ace64.dll", EntryPoint = "AdsClearLastError", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearLastError_64();

        public static uint AdsClearLastError()
        {
            return IntPtr.Size == 4 ? AdsClearLastError_32() : AdsClearLastError_64();
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDeleteFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteFile_32(IntPtr hConnect, string pucFileName);

        [DllImport("ace64.dll", EntryPoint = "AdsDeleteFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteFile_64(IntPtr hConnect, string pucFileName);

        public static uint AdsDeleteFile(IntPtr hConnect, string pucFileName)
        {
            return IntPtr.Size == 4 ? AdsDeleteFile_32(hConnect, pucFileName) : AdsDeleteFile_64(hConnect, pucFileName);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDeleteTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteTable_32(IntPtr hTable);

        [DllImport("ace64.dll", EntryPoint = "AdsDeleteTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteTable_64(IntPtr hTable);

        public static uint AdsDeleteTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsDeleteTable_32(hTable) : AdsDeleteTable_64(hTable);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsMemCompare", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemCompare_32(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        out short psResult);

        [DllImport("ace64.dll", EntryPoint = "AdsMemCompare", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemCompare_64(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        out short psResult);

        public static uint AdsMemCompare(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        out short psResult)
        {
            return IntPtr.Size == 4 ? AdsMemCompare_32(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, out psResult) : AdsMemCompare_64(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, out psResult);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsMemCompare90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemCompare90_32(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        uint ulCollationID,
        out short psResult);

        [DllImport("ace64.dll", EntryPoint = "AdsMemCompare90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemCompare90_64(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        uint ulCollationID,
        out short psResult);

        public static uint AdsMemCompare90(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        uint ulCollationID,
        out short psResult)
        {
            return IntPtr.Size == 4 ? AdsMemCompare90_32(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, ulCollationID, out psResult) : AdsMemCompare90_64(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, ulCollationID, out psResult);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsMemICompare", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemICompare_32(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        out short psResult);

        [DllImport("ace64.dll", EntryPoint = "AdsMemICompare", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemICompare_64(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        out short psResult);

        public static uint AdsMemICompare(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        out short psResult)
        {
            return IntPtr.Size == 4 ? AdsMemICompare_32(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, out psResult) : AdsMemICompare_64(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, out psResult);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsMemICompare90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemICompare90_32(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        uint ulCollationID,
        out short psResult);

        [DllImport("ace64.dll", EntryPoint = "AdsMemICompare90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemICompare90_64(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        uint ulCollationID,
        out short psResult);

        public static uint AdsMemICompare90(
        IntPtr hConnect,
        string pucStr1,
        uint ulStr1Len,
        string pucStr2,
        uint ulStr2Len,
        ushort usCharSet,
        uint ulCollationID,
        out short psResult)
        {
            return IntPtr.Size == 4 ? AdsMemICompare90_32(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, ulCollationID, out psResult) : AdsMemICompare90_64(hConnect, pucStr1, ulStr1Len, pucStr2, ulStr2Len, usCharSet, ulCollationID, out psResult);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsMemLwr", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemLwr_32(
        IntPtr hConnect,
        string pucStr,
        ushort usStrLen,
        ushort usCharSet);

        [DllImport("ace64.dll", EntryPoint = "AdsMemLwr", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemLwr_64(
        IntPtr hConnect,
        string pucStr,
        ushort usStrLen,
        ushort usCharSet);

        public static uint AdsMemLwr(
        IntPtr hConnect,
        string pucStr,
        ushort usStrLen,
        ushort usCharSet)
        {
            return IntPtr.Size == 4 ? AdsMemLwr_32(hConnect, pucStr, usStrLen, usCharSet) : AdsMemLwr_64(hConnect, pucStr, usStrLen, usCharSet);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsMemLwr90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemLwr90_32(
        IntPtr hConnect,
        string pucStr,
        ushort usStrLen,
        ushort usCharSet,
        uint ulCollationID);

        [DllImport("ace64.dll", EntryPoint = "AdsMemLwr90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMemLwr90_64(
        IntPtr hConnect,
        string pucStr,
        ushort usStrLen,
        ushort usCharSet,
        uint ulCollationID);

        public static uint AdsMemLwr90(
        IntPtr hConnect,
        string pucStr,
        ushort usStrLen,
        ushort usCharSet,
        uint ulCollationID)
        {
            return IntPtr.Size == 4 ? AdsMemLwr90_32(hConnect, pucStr, usStrLen, usCharSet, ulCollationID) : AdsMemLwr90_64(hConnect, pucStr, usStrLen, usCharSet, ulCollationID);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetIndexFlags", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexFlags_32(IntPtr hIndex, out uint pulFlags);

        [DllImport("ace64.dll", EntryPoint = "AdsGetIndexFlags", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexFlags_64(IntPtr hIndex, out uint pulFlags);

        public static uint AdsGetIndexFlags(IntPtr hIndex, out uint pulFlags)
        {
            return IntPtr.Size == 4 ? AdsGetIndexFlags_32(hIndex, out pulFlags) : AdsGetIndexFlags_64(hIndex, out pulFlags);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertKeyToDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertKeyToDouble_32(string pucKey, out double pdValue);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertKeyToDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertKeyToDouble_64(string pucKey, out double pdValue);

        public static uint AdsConvertKeyToDouble(string pucKey, out double pdValue)
        {
            return IntPtr.Size == 4 ? AdsConvertKeyToDouble_32(pucKey, out pdValue) : AdsConvertKeyToDouble_64(pucKey, out pdValue);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetNumSegments", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumSegments_32(IntPtr hTag, out ushort usSegments);

        [DllImport("ace64.dll", EntryPoint = "AdsGetNumSegments", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumSegments_64(IntPtr hTag, out ushort usSegments);

        public static uint AdsGetNumSegments(IntPtr hTag, out ushort usSegments)
        {
            return IntPtr.Size == 4 ? AdsGetNumSegments_32(hTag, out usSegments) : AdsGetNumSegments_64(hTag, out usSegments);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetSegmentFieldname", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSegmentFieldname_32(
        IntPtr hTag,
        ushort usSegmentNum,
        [In, Out] char[] pucFieldname,
        ref ushort pusFldnameLen);

        [DllImport("ace64.dll", EntryPoint = "AdsGetSegmentFieldname", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSegmentFieldname_64(
        IntPtr hTag,
        ushort usSegmentNum,
        [In, Out] char[] pucFieldname,
        ref ushort pusFldnameLen);

        public static uint AdsGetSegmentFieldname(
        IntPtr hTag,
        ushort usSegmentNum,
        char[] pucFieldname,
        ref ushort pusFldnameLen)
        {
            return IntPtr.Size == 4 ? AdsGetSegmentFieldname_32(hTag, usSegmentNum, pucFieldname, ref pusFldnameLen) : AdsGetSegmentFieldname_64(hTag, usSegmentNum, pucFieldname, ref pusFldnameLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetSegmentOffset", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSegmentOffset_32(
        IntPtr hTag,
        ushort usSegmentNum,
        out ushort usOffset);

        [DllImport("ace64.dll", EntryPoint = "AdsGetSegmentOffset", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSegmentOffset_64(
        IntPtr hTag,
        ushort usSegmentNum,
        out ushort usOffset);

        public static uint AdsGetSegmentOffset(IntPtr hTag, ushort usSegmentNum, out ushort usOffset)
        {
            return IntPtr.Size == 4 ? AdsGetSegmentOffset_32(hTag, usSegmentNum, out usOffset) : AdsGetSegmentOffset_64(hTag, usSegmentNum, out usOffset);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsIsSegmentDescending", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsSegmentDescending_32(
        IntPtr hTag,
        ushort usSegmentNum,
        out ushort pbDescending);

        [DllImport("ace64.dll", EntryPoint = "AdsIsSegmentDescending", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsSegmentDescending_64(
        IntPtr hTag,
        ushort usSegmentNum,
        out ushort pbDescending);

        public static uint AdsIsSegmentDescending(
        IntPtr hTag,
        ushort usSegmentNum,
        out ushort pbDescending)
        {
            return IntPtr.Size == 4 ? AdsIsSegmentDescending_32(hTag, usSegmentNum, out pbDescending) : AdsIsSegmentDescending_64(hTag, usSegmentNum, out pbDescending);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetSegmentFieldNumbers", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSegmentFieldNumbers_32(
        IntPtr hTag,
        out ushort pusNumSegments,
        [In, Out] ushort[] pusSegFieldNumbers);

        [DllImport("ace64.dll", EntryPoint = "AdsGetSegmentFieldNumbers", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSegmentFieldNumbers_64(
        IntPtr hTag,
        out ushort pusNumSegments,
        [In, Out] ushort[] pusSegFieldNumbers);

        public static uint AdsGetSegmentFieldNumbers(
        IntPtr hTag,
        out ushort pusNumSegments,
        ushort[] pusSegFieldNumbers)
        {
            return IntPtr.Size == 4 ? AdsGetSegmentFieldNumbers_32(hTag, out pusNumSegments, pusSegFieldNumbers) : AdsGetSegmentFieldNumbers_64(hTag, out pusNumSegments, pusSegFieldNumbers);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFieldRaw_32(
        IntPtr hObj,
        string pucFldName,
        byte[] pucBuf,
        uint ulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsSetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFieldRaw_64(
        IntPtr hObj,
        string pucFldName,
        byte[] pucBuf,
        uint ulLen);

        public static uint AdsSetFieldRaw(IntPtr hObj, string pucFldName, byte[] pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetFieldRaw_32(hObj, pucFldName, pucBuf, ulLen) : AdsSetFieldRaw_64(hObj, pucFldName, pucBuf, ulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFieldRaw_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        byte[] pucBuf,
        uint ulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsSetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFieldRaw_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        byte[] pucBuf,
        uint ulLen);

        public static uint AdsSetFieldRaw(IntPtr hObj, uint lFieldOrdinal, byte[] pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetFieldRaw_32(hObj, lFieldOrdinal, pucBuf, ulLen) : AdsSetFieldRaw_64(hObj, lFieldOrdinal, pucBuf, ulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldRaw_32(
        IntPtr hTbl,
        string pucFldName,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsGetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldRaw_64(
        IntPtr hTbl,
        string pucFldName,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        public static uint AdsGetFieldRaw(
        IntPtr hTbl,
        string pucFldName,
        byte[] pucBuf,
        ref uint pulLen)
        {
            return IntPtr.Size == 4 ? AdsGetFieldRaw_32(hTbl, pucFldName, pucBuf, ref pulLen) : AdsGetFieldRaw_64(hTbl, pucFldName, pucBuf, ref pulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldRaw_32(
        IntPtr hTbl,
        uint lFieldOrdinal,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsGetFieldRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldRaw_64(
        IntPtr hTbl,
        uint lFieldOrdinal,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        public static uint AdsGetFieldRaw(
        IntPtr hTbl,
        uint lFieldOrdinal,
        byte[] pucBuf,
        ref uint pulLen)
        {
            return IntPtr.Size == 4 ? AdsGetFieldRaw_32(hTbl, lFieldOrdinal, pucBuf, ref pulLen) : AdsGetFieldRaw_64(hTbl, lFieldOrdinal, pucBuf, ref pulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetFlushFlag", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFlushFlag_32(IntPtr hConnect, ushort usFlushEveryUpdate);

        [DllImport("ace64.dll", EntryPoint = "AdsSetFlushFlag", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFlushFlag_64(IntPtr hConnect, ushort usFlushEveryUpdate);

        public static uint AdsSetFlushFlag(IntPtr hConnect, ushort usFlushEveryUpdate)
        {
            return IntPtr.Size == 4 ? AdsSetFlushFlag_32(hConnect, usFlushEveryUpdate) : AdsSetFlushFlag_64(hConnect, usFlushEveryUpdate);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetTimeStampRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStampRaw_32(
        IntPtr hObj,
        string pucFldName,
        ref ulong pucBuf,
        uint ulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsSetTimeStampRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStampRaw_64(
        IntPtr hObj,
        string pucFldName,
        ref ulong pucBuf,
        uint ulLen);

        public static uint AdsSetTimeStampRaw(
        IntPtr hObj,
        string pucFldName,
        ref ulong pucBuf,
        uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetTimeStampRaw_32(hObj, pucFldName, ref pucBuf, ulLen) : AdsSetTimeStampRaw_64(hObj, pucFldName, ref pucBuf, ulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetTimeStampRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStampRaw_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        ref ulong pucBuf,
        uint ulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsSetTimeStampRaw", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStampRaw_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        ref ulong pucBuf,
        uint ulLen);

        public static uint AdsSetTimeStampRaw(
        IntPtr hObj,
        uint lFieldOrdinal,
        ref ulong pucBuf,
        uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetTimeStampRaw_32(hObj, lFieldOrdinal, ref pucBuf, ulLen) : AdsSetTimeStampRaw_64(hObj, lFieldOrdinal, ref pucBuf, ulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetInternalError", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetInternalError_32(uint ulErrCode, string pucFile, uint ulLine);

        [DllImport("ace64.dll", EntryPoint = "AdsSetInternalError", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetInternalError_64(uint ulErrCode, string pucFile, uint ulLine);

        public static uint AdsSetInternalError(uint ulErrCode, string pucFile, uint ulLine)
        {
            return IntPtr.Size == 4 ? AdsSetInternalError_32(ulErrCode, pucFile, ulLine) : AdsSetInternalError_64(ulErrCode, pucFile, ulLine);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsValidateThread", CharSet = CharSet.Ansi)]
        private static extern uint AdsValidateThread_32();

        [DllImport("ace64.dll", EntryPoint = "AdsValidateThread", CharSet = CharSet.Ansi)]
        private static extern uint AdsValidateThread_64();

        public static uint AdsValidateThread()
        {
            return IntPtr.Size == 4 ? AdsValidateThread_32() : AdsValidateThread_64();
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetLastError", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLastError_32(uint ulErrCode, string pucDetails);

        [DllImport("ace64.dll", EntryPoint = "AdsSetLastError", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLastError_64(uint ulErrCode, string pucDetails);

        public static uint AdsSetLastError(uint ulErrCode, string pucDetails)
        {
            return IntPtr.Size == 4 ? AdsSetLastError_32(ulErrCode, pucDetails) : AdsSetLastError_64(ulErrCode, pucDetails);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetTableCharType", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTableCharType_32(IntPtr hTbl, ushort usCharType);

        [DllImport("ace64.dll", EntryPoint = "AdsSetTableCharType", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTableCharType_64(IntPtr hTbl, ushort usCharType);

        public static uint AdsSetTableCharType(IntPtr hTbl, ushort usCharType)
        {
            return IntPtr.Size == 4 ? AdsSetTableCharType_32(hTbl, usCharType) : AdsSetTableCharType_64(hTbl, usCharType);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertJulianToString", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertJulianToString_32(
        double dJulian,
        [In, Out] char[] pucJulian,
        ref ushort pusLen);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertJulianToString", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertJulianToString_64(
        double dJulian,
        [In, Out] char[] pucJulian,
        ref ushort pusLen);

        public static uint AdsConvertJulianToString(
        double dJulian,
        char[] pucJulian,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsConvertJulianToString_32(dJulian, pucJulian, ref pusLen) : AdsConvertJulianToString_64(dJulian, pucJulian, ref pusLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertStringToJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertStringToJulian_32(
        string pucJulian,
        ushort usLen,
        out double pdJulian);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertStringToJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertStringToJulian_64(
        string pucJulian,
        ushort usLen,
        out double pdJulian);

        public static uint AdsConvertStringToJulian(
        string pucJulian,
        ushort usLen,
        out double pdJulian)
        {
            return IntPtr.Size == 4 ? AdsConvertStringToJulian_32(pucJulian, usLen, out pdJulian) : AdsConvertStringToJulian_64(pucJulian, usLen, out pdJulian);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertStringToJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertStringToJulian_32(
        [In, Out] char[] pucJulian,
        ushort usLen,
        out double pdJulian);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertStringToJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertStringToJulian_64(
        [In, Out] char[] pucJulian,
        ushort usLen,
        out double pdJulian);

        public static uint AdsConvertStringToJulian(
        char[] pucJulian,
        ushort usLen,
        out double pdJulian)
        {
            return IntPtr.Size == 4 ? AdsConvertStringToJulian_32(pucJulian, usLen, out pdJulian) : AdsConvertStringToJulian_64(pucJulian, usLen, out pdJulian);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertMillisecondsToString", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertMillisecondsToString_32(
        uint ulMSeconds,
        [In, Out] char[] pucTime,
        ref ushort pusLen);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertMillisecondsToString", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertMillisecondsToString_64(
        uint ulMSeconds,
        [In, Out] char[] pucTime,
        ref ushort pusLen);

        public static uint AdsConvertMillisecondsToString(
        uint ulMSeconds,
        char[] pucTime,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsConvertMillisecondsToString_32(ulMSeconds, pucTime, ref pusLen) : AdsConvertMillisecondsToString_64(ulMSeconds, pucTime, ref pusLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertStringToMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertStringToMilliseconds_32(
        string pucTime,
        ushort usLen,
        out uint pulMSeconds);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertStringToMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertStringToMilliseconds_64(
        string pucTime,
        ushort usLen,
        out uint pulMSeconds);

        public static uint AdsConvertStringToMilliseconds(
        string pucTime,
        ushort usLen,
        out uint pulMSeconds)
        {
            return IntPtr.Size == 4 ? AdsConvertStringToMilliseconds_32(pucTime, usLen, out pulMSeconds) : AdsConvertStringToMilliseconds_64(pucTime, usLen, out pulMSeconds);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetCollationSequence", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCollationSequence_32(string pucSequence);

        [DllImport("ace64.dll", EntryPoint = "AdsSetCollationSequence", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCollationSequence_64(string pucSequence);

        public static uint AdsSetCollationSequence(string pucSequence)
        {
            return IntPtr.Size == 4 ? AdsSetCollationSequence_32(pucSequence) : AdsSetCollationSequence_64(pucSequence);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetBOFFlag", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBOFFlag_32(IntPtr hTbl, ushort usBOF);

        [DllImport("ace64.dll", EntryPoint = "AdsSetBOFFlag", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBOFFlag_64(IntPtr hTbl, ushort usBOF);

        public static uint AdsSetBOFFlag(IntPtr hTbl, ushort usBOF)
        {
            return IntPtr.Size == 4 ? AdsSetBOFFlag_32(hTbl, usBOF) : AdsSetBOFFlag_64(hTbl, usBOF);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDBFDateToString", CharSet = CharSet.Ansi)]
        private static extern uint AdsDBFDateToString_32(string pucDBFDate, string pucFormattedDate);

        [DllImport("ace64.dll", EntryPoint = "AdsDBFDateToString", CharSet = CharSet.Ansi)]
        private static extern uint AdsDBFDateToString_64(string pucDBFDate, string pucFormattedDate);

        public static uint AdsDBFDateToString(string pucDBFDate, string pucFormattedDate)
        {
            return IntPtr.Size == 4 ? AdsDBFDateToString_32(pucDBFDate, pucFormattedDate) : AdsDBFDateToString_64(pucDBFDate, pucFormattedDate);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsActivateAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsActivateAOF_32(IntPtr hTable);

        [DllImport("ace64.dll", EntryPoint = "AdsActivateAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsActivateAOF_64(IntPtr hTable);

        public static uint AdsActivateAOF(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsActivateAOF_32(hTable) : AdsActivateAOF_64(hTable);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDeactivateAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeactivateAOF_32(IntPtr hTable);

        [DllImport("ace64.dll", EntryPoint = "AdsDeactivateAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeactivateAOF_64(IntPtr hTable);

        public static uint AdsDeactivateAOF(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsDeactivateAOF_32(hTable) : AdsDeactivateAOF_64(hTable);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsExtractPathPart", CharSet = CharSet.Ansi)]
        private static extern uint AdsExtractPathPart_32(
        ushort usPart,
        string pucFile,
        [In, Out] char[] pucPart,
        ref ushort pusPartLen);

        [DllImport("ace64.dll", EntryPoint = "AdsExtractPathPart", CharSet = CharSet.Ansi)]
        private static extern uint AdsExtractPathPart_64(
        ushort usPart,
        string pucFile,
        [In, Out] char[] pucPart,
        ref ushort pusPartLen);

        public static uint AdsExtractPathPart(
        ushort usPart,
        string pucFile,
        char[] pucPart,
        ref ushort pusPartLen)
        {
            return IntPtr.Size == 4 ? AdsExtractPathPart_32(usPart, pucFile, pucPart, ref pusPartLen) : AdsExtractPathPart_64(usPart, pucFile, pucPart, ref pusPartLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsExpressionLongToShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionLongToShort_32(
        IntPtr hTable,
        string pucLongExpr,
        [In, Out] char[] pucShortExpr,
        ref ushort pusBufferLen);

        [DllImport("ace64.dll", EntryPoint = "AdsExpressionLongToShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionLongToShort_64(
        IntPtr hTable,
        string pucLongExpr,
        [In, Out] char[] pucShortExpr,
        ref ushort pusBufferLen);

        public static uint AdsExpressionLongToShort(
        IntPtr hTable,
        string pucLongExpr,
        char[] pucShortExpr,
        ref ushort pusBufferLen)
        {
            return IntPtr.Size == 4 ? AdsExpressionLongToShort_32(hTable, pucLongExpr, pucShortExpr, ref pusBufferLen) : AdsExpressionLongToShort_64(hTable, pucLongExpr, pucShortExpr, ref pusBufferLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsExpressionShortToLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionShortToLong_32(
        IntPtr hTable,
        string pucShortExpr,
        [In, Out] char[] pucLongExpr,
        ref ushort pusBufferLen);

        [DllImport("ace64.dll", EntryPoint = "AdsExpressionShortToLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionShortToLong_64(
        IntPtr hTable,
        string pucShortExpr,
        [In, Out] char[] pucLongExpr,
        ref ushort pusBufferLen);

        public static uint AdsExpressionShortToLong(
        IntPtr hTable,
        string pucShortExpr,
        char[] pucLongExpr,
        ref ushort pusBufferLen)
        {
            return IntPtr.Size == 4 ? AdsExpressionShortToLong_32(hTable, pucShortExpr, pucLongExpr, ref pusBufferLen) : AdsExpressionShortToLong_64(hTable, pucShortExpr, pucLongExpr, ref pusBufferLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsExpressionLongToShort90", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionLongToShort90_32(
        IntPtr hTable,
        string pucLongExpr,
        [In, Out] char[] pucShortExpr,
        ref uint pulBufferLen);

        [DllImport("ace64.dll", EntryPoint = "AdsExpressionLongToShort90", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionLongToShort90_64(
        IntPtr hTable,
        string pucLongExpr,
        [In, Out] char[] pucShortExpr,
        ref uint pulBufferLen);

        public static uint AdsExpressionLongToShort90(
        IntPtr hTable,
        string pucLongExpr,
        char[] pucShortExpr,
        ref uint pulBufferLen)
        {
            return IntPtr.Size == 4 ? AdsExpressionLongToShort90_32(hTable, pucLongExpr, pucShortExpr, ref pulBufferLen) : AdsExpressionLongToShort90_64(hTable, pucLongExpr, pucShortExpr, ref pulBufferLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsExpressionLongToShort100", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionLongToShort100_32(
        IntPtr hTable,
        string pucLongExpr,
        [In, Out] char[] pucShortExpr,
        ref uint pulBufferLen,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsExpressionLongToShort100", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionLongToShort100_64(
        IntPtr hTable,
        string pucLongExpr,
        [In, Out] char[] pucShortExpr,
        ref uint pulBufferLen,
        uint ulOptions);

        public static uint AdsExpressionLongToShort100(
        IntPtr hTable,
        string pucLongExpr,
        char[] pucShortExpr,
        ref uint pulBufferLen,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsExpressionLongToShort100_32(hTable, pucLongExpr, pucShortExpr, ref pulBufferLen, ulOptions) : AdsExpressionLongToShort100_64(hTable, pucLongExpr, pucShortExpr, ref pulBufferLen, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsExpressionShortToLong90", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionShortToLong90_32(
        IntPtr hTable,
        string pucShortExpr,
        [In, Out] char[] pucLongExpr,
        ref uint pulBufferLen);

        [DllImport("ace64.dll", EntryPoint = "AdsExpressionShortToLong90", CharSet = CharSet.Ansi)]
        private static extern uint AdsExpressionShortToLong90_64(
        IntPtr hTable,
        string pucShortExpr,
        [In, Out] char[] pucLongExpr,
        ref uint pulBufferLen);

        public static uint AdsExpressionShortToLong90(
        IntPtr hTable,
        string pucShortExpr,
        char[] pucLongExpr,
        ref uint pulBufferLen)
        {
            return IntPtr.Size == 4 ? AdsExpressionShortToLong90_32(hTable, pucShortExpr, pucLongExpr, ref pulBufferLen) : AdsExpressionShortToLong90_64(hTable, pucShortExpr, pucLongExpr, ref pulBufferLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSqlPeekStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsSqlPeekStatement_32(IntPtr hCursor, out byte IsLive);

        [DllImport("ace64.dll", EntryPoint = "AdsSqlPeekStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsSqlPeekStatement_64(IntPtr hCursor, out byte IsLive);

        public static uint AdsSqlPeekStatement(IntPtr hCursor, out byte IsLive)
        {
            return IntPtr.Size == 4 ? AdsSqlPeekStatement_32(hCursor, out IsLive) : AdsSqlPeekStatement_64(hCursor, out IsLive);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetCursorAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCursorAOF_32(
        IntPtr hTable,
        string pucFilter,
        ushort usResolve);

        [DllImport("ace64.dll", EntryPoint = "AdsSetCursorAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCursorAOF_64(
        IntPtr hTable,
        string pucFilter,
        ushort usResolve);

        public static uint AdsSetCursorAOF(IntPtr hTable, string pucFilter, ushort usResolve)
        {
            return IntPtr.Size == 4 ? AdsSetCursorAOF_32(hTable, pucFilter, usResolve) : AdsSetCursorAOF_64(hTable, pucFilter, usResolve);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetCursorAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetCursorAOF_32(
        IntPtr hCursor,
        [In, Out] char[] pucFilter,
        ref ushort pusFilterLen);

        [DllImport("ace64.dll", EntryPoint = "AdsGetCursorAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetCursorAOF_64(
        IntPtr hCursor,
        [In, Out] char[] pucFilter,
        ref ushort pusFilterLen);

        public static uint AdsGetCursorAOF(IntPtr hCursor, char[] pucFilter, ref ushort pusFilterLen)
        {
            return IntPtr.Size == 4 ? AdsGetCursorAOF_32(hCursor, pucFilter, ref pusFilterLen) : AdsGetCursorAOF_64(hCursor, pucFilter, ref pusFilterLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsClearCursorAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearCursorAOF_32(IntPtr hTable);

        [DllImport("ace64.dll", EntryPoint = "AdsClearCursorAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearCursorAOF_64(IntPtr hTable);

        public static uint AdsClearCursorAOF(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsClearCursorAOF_32(hTable) : AdsClearCursorAOF_64(hTable);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsInternalCloseCachedTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalCloseCachedTables_32(IntPtr hConnect, ushort usOpen);

        [DllImport("ace64.dll", EntryPoint = "AdsInternalCloseCachedTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalCloseCachedTables_64(IntPtr hConnect, ushort usOpen);

        public static uint AdsInternalCloseCachedTables(IntPtr hConnect, ushort usOpen)
        {
            return IntPtr.Size == 4 ? AdsInternalCloseCachedTables_32(hConnect, usOpen) : AdsInternalCloseCachedTables_64(hConnect, usOpen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsInternalBeginTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalBeginTransaction_32(IntPtr hConnect);

        [DllImport("ace64.dll", EntryPoint = "AdsInternalBeginTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalBeginTransaction_64(IntPtr hConnect);

        public static uint AdsInternalBeginTransaction(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsInternalBeginTransaction_32(hConnect) : AdsInternalBeginTransaction_64(hConnect);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsInternalCommitTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalCommitTransaction_32(IntPtr hConnect);

        [DllImport("ace64.dll", EntryPoint = "AdsInternalCommitTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalCommitTransaction_64(IntPtr hConnect);

        public static uint AdsInternalCommitTransaction(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsInternalCommitTransaction_32(hConnect) : AdsInternalCommitTransaction_64(hConnect);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsInternalRollbackTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalRollbackTransaction_32(IntPtr hConnect);

        [DllImport("ace64.dll", EntryPoint = "AdsInternalRollbackTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalRollbackTransaction_64(IntPtr hConnect);

        public static uint AdsInternalRollbackTransaction(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsInternalRollbackTransaction_32(hConnect) : AdsInternalRollbackTransaction_64(hConnect);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsInternalRollbackTransaction80", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalRollbackTransaction80_32(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsInternalRollbackTransaction80", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalRollbackTransaction80_64(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        public static uint AdsInternalRollbackTransaction80(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsInternalRollbackTransaction80_32(hConnect, pucSavepoint, ulOptions) : AdsInternalRollbackTransaction80_64(hConnect, pucSavepoint, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsInternalCreateSavepoint", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalCreateSavepoint_32(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsInternalCreateSavepoint", CharSet = CharSet.Ansi)]
        private static extern uint AdsInternalCreateSavepoint_64(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        public static uint AdsInternalCreateSavepoint(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsInternalCreateSavepoint_32(hConnect, pucSavepoint, ulOptions) : AdsInternalCreateSavepoint_64(hConnect, pucSavepoint, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsClearRecordBuffer", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearRecordBuffer_32(IntPtr hTbl, string pucBuf, uint ulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsClearRecordBuffer", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearRecordBuffer_64(IntPtr hTbl, string pucBuf, uint ulLen);

        public static uint AdsClearRecordBuffer(IntPtr hTbl, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsClearRecordBuffer_32(hTbl, pucBuf, ulLen) : AdsClearRecordBuffer_64(hTbl, pucBuf, ulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "ObsAdsEncryptBuffer", CharSet = CharSet.Ansi)]
        private static extern uint ObsAdsEncryptBuffer_32(
        string pucPassword,
        string pucBuffer,
        ushort usLen);

        [DllImport("ace64.dll", EntryPoint = "ObsAdsEncryptBuffer", CharSet = CharSet.Ansi)]
        private static extern uint ObsAdsEncryptBuffer_64(
        string pucPassword,
        string pucBuffer,
        ushort usLen);

        public static uint ObsAdsEncryptBuffer(string pucPassword, string pucBuffer, ushort usLen)
        {
            return IntPtr.Size == 4 ? ObsAdsEncryptBuffer_32(pucPassword, pucBuffer, usLen) : ObsAdsEncryptBuffer_64(pucPassword, pucBuffer, usLen);
        }

        [DllImport("ace32.dll", EntryPoint = "ObsAdsDecryptBuffer", CharSet = CharSet.Ansi)]
        private static extern uint ObsAdsDecryptBuffer_32(
        string pucPassword,
        string pucBuffer,
        ushort usLen);

        [DllImport("ace64.dll", EntryPoint = "ObsAdsDecryptBuffer", CharSet = CharSet.Ansi)]
        private static extern uint ObsAdsDecryptBuffer_64(
        string pucPassword,
        string pucBuffer,
        ushort usLen);

        public static uint ObsAdsDecryptBuffer(string pucPassword, string pucBuffer, ushort usLen)
        {
            return IntPtr.Size == 4 ? ObsAdsDecryptBuffer_32(pucPassword, pucBuffer, usLen) : ObsAdsDecryptBuffer_64(pucPassword, pucBuffer, usLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsEvalExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalExpr_32(
        IntPtr hTable,
        string pucPCode,
        [In, Out] char[] pucResult,
        ref ushort pusLen);

        [DllImport("ace64.dll", EntryPoint = "AdsEvalExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalExpr_64(
        IntPtr hTable,
        string pucPCode,
        [In, Out] char[] pucResult,
        ref ushort pusLen);

        public static uint AdsEvalExpr(
        IntPtr hTable,
        string pucPCode,
        char[] pucResult,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsEvalExpr_32(hTable, pucPCode, pucResult, ref pusLen) : AdsEvalExpr_64(hTable, pucPCode, pucResult, ref pusLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsFreeExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsFreeExpr_32(IntPtr hTable, string pucPCode);

        [DllImport("ace64.dll", EntryPoint = "AdsFreeExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsFreeExpr_64(IntPtr hTable, string pucPCode);

        public static uint AdsFreeExpr(IntPtr hTable, string pucPCode)
        {
            return IntPtr.Size == 4 ? AdsFreeExpr_32(hTable, pucPCode) : AdsFreeExpr_64(hTable, pucPCode);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsIsIndexExprValid", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexExprValid_32(
        IntPtr hTbl,
        string pucExpr,
        out ushort pbValid);

        [DllImport("ace64.dll", EntryPoint = "AdsIsIndexExprValid", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexExprValid_64(
        IntPtr hTbl,
        string pucExpr,
        out ushort pbValid);

        public static uint AdsIsIndexExprValid(IntPtr hTbl, string pucExpr, out ushort pbValid)
        {
            return IntPtr.Size == 4 ? AdsIsIndexExprValid_32(hTbl, pucExpr, out pbValid) : AdsIsIndexExprValid_64(hTbl, pucExpr, out pbValid);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsStepIndexKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsStepIndexKey_32(
        IntPtr hIndex,
        string pucKey,
        ushort usLen,
        short sDirection);

        [DllImport("ace64.dll", EntryPoint = "AdsStepIndexKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsStepIndexKey_64(
        IntPtr hIndex,
        string pucKey,
        ushort usLen,
        short sDirection);

        public static uint AdsStepIndexKey(
        IntPtr hIndex,
        string pucKey,
        ushort usLen,
        short sDirection)
        {
            return IntPtr.Size == 4 ? AdsStepIndexKey_32(hIndex, pucKey, usLen, sDirection) : AdsStepIndexKey_64(hIndex, pucKey, usLen, sDirection);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsPrepareSQLNow", CharSet = CharSet.Ansi)]
        private static extern uint AdsPrepareSQLNow_32(
        IntPtr hStatement,
        string pucSQL,
        [In, Out] char[] pucFieldInfo,
        ref ushort pusFieldInfoLen);

        [DllImport("ace64.dll", EntryPoint = "AdsPrepareSQLNow", CharSet = CharSet.Ansi)]
        private static extern uint AdsPrepareSQLNow_64(
        IntPtr hStatement,
        string pucSQL,
        [In, Out] char[] pucFieldInfo,
        ref ushort pusFieldInfoLen);

        public static uint AdsPrepareSQLNow(
        IntPtr hStatement,
        string pucSQL,
        char[] pucFieldInfo,
        ref ushort pusFieldInfoLen)
        {
            return IntPtr.Size == 4 ? AdsPrepareSQLNow_32(hStatement, pucSQL, pucFieldInfo, ref pusFieldInfoLen) : AdsPrepareSQLNow_64(hStatement, pucSQL, pucFieldInfo, ref pusFieldInfoLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsPrepareSQLNowW", CharSet = CharSet.Unicode)]
        private static extern uint AdsPrepareSQLNowW_32(
        IntPtr hStatement,
        string pwcSQL,
        [In, Out] char[] pucFieldInfo,
        ref ushort pusFieldInfoLen);

        [DllImport("ace64.dll", EntryPoint = "AdsPrepareSQLNowW", CharSet = CharSet.Unicode)]
        private static extern uint AdsPrepareSQLNowW_64(
        IntPtr hStatement,
        string pwcSQL,
        [In, Out] char[] pucFieldInfo,
        ref ushort pusFieldInfoLen);

        public static uint AdsPrepareSQLNowW(
        IntPtr hStatement,
        string pwcSQL,
        char[] pucFieldInfo,
        ref ushort pusFieldInfoLen)
        {
            return IntPtr.Size == 4 ? AdsPrepareSQLNowW_32(hStatement, pwcSQL, pucFieldInfo, ref pusFieldInfoLen) : AdsPrepareSQLNowW_64(hStatement, pwcSQL, pucFieldInfo, ref pusFieldInfoLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetPreparedFields", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetPreparedFields_32(
        IntPtr hStatement,
        [In, Out] char[] pucBuffer,
        ref uint pulBufferLen,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsGetPreparedFields", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetPreparedFields_64(
        IntPtr hStatement,
        [In, Out] char[] pucBuffer,
        ref uint pulBufferLen,
        uint ulOptions);

        public static uint AdsGetPreparedFields(
        IntPtr hStatement,
        char[] pucBuffer,
        ref uint pulBufferLen,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsGetPreparedFields_32(hStatement, pucBuffer, ref pulBufferLen, ulOptions) : AdsGetPreparedFields_64(hStatement, pucBuffer, ref pulBufferLen, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsEcho", CharSet = CharSet.Ansi)]
        private static extern uint AdsEcho_32(IntPtr hConnect, string pucData, ushort usLen);

        [DllImport("ace64.dll", EntryPoint = "AdsEcho", CharSet = CharSet.Ansi)]
        private static extern uint AdsEcho_64(IntPtr hConnect, string pucData, ushort usLen);

        public static uint AdsEcho(IntPtr hConnect, string pucData, ushort usLen)
        {
            return IntPtr.Size == 4 ? AdsEcho_32(hConnect, pucData, usLen) : AdsEcho_64(hConnect, pucData, usLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsReadRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsReadRecords_32(IntPtr hObj, uint ulRecordNum, byte cDirection);

        [DllImport("ace64.dll", EntryPoint = "AdsReadRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsReadRecords_64(IntPtr hObj, uint ulRecordNum, byte cDirection);

        public static uint AdsReadRecords(IntPtr hObj, uint ulRecordNum, byte cDirection)
        {
            return IntPtr.Size == 4 ? AdsReadRecords_32(hObj, ulRecordNum, cDirection) : AdsReadRecords_64(hObj, ulRecordNum, cDirection);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsReadRecordNumbers", CharSet = CharSet.Ansi)]
        private static extern uint AdsReadRecordNumbers_32(
        IntPtr hObj,
        uint ulRecordNum,
        byte ucDirection,
        out uint pulRecords,
        ref uint pulArrayLen,
        out ushort pusHitEOF);

        [DllImport("ace64.dll", EntryPoint = "AdsReadRecordNumbers", CharSet = CharSet.Ansi)]
        private static extern uint AdsReadRecordNumbers_64(
        IntPtr hObj,
        uint ulRecordNum,
        byte ucDirection,
        out uint pulRecords,
        ref uint pulArrayLen,
        out ushort pusHitEOF);

        public static uint AdsReadRecordNumbers(
        IntPtr hObj,
        uint ulRecordNum,
        byte ucDirection,
        out uint pulRecords,
        ref uint pulArrayLen,
        out ushort pusHitEOF)
        {
            return IntPtr.Size == 4 ? AdsReadRecordNumbers_32(hObj, ulRecordNum, ucDirection, out pulRecords, ref pulArrayLen, out pusHitEOF) : AdsReadRecordNumbers_64(hObj, ulRecordNum, ucDirection, out pulRecords, ref pulArrayLen, out pusHitEOF);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsMergeAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsMergeAOF_32(IntPtr hTable);

        [DllImport("ace64.dll", EntryPoint = "AdsMergeAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsMergeAOF_64(IntPtr hTable);

        public static uint AdsMergeAOF(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsMergeAOF_32(hTable) : AdsMergeAOF_64(hTable);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetupRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetupRI_32(
        IntPtr hConnection,
        int lTableID,
        byte ucOpen,
        uint ulServerWAN);

        [DllImport("ace64.dll", EntryPoint = "AdsSetupRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetupRI_64(
        IntPtr hConnection,
        int lTableID,
        byte ucOpen,
        uint ulServerWAN);

        public static uint AdsSetupRI(IntPtr hConnection, int lTableID, byte ucOpen, uint ulServerWAN)
        {
            return IntPtr.Size == 4 ? AdsSetupRI_32(hConnection, lTableID, ucOpen, ulServerWAN) : AdsSetupRI_64(hConnection, lTableID, ucOpen, ulServerWAN);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsPerformRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsPerformRI_32(IntPtr hTable, uint ulRecNum, string pucRecBuffer);

        [DllImport("ace64.dll", EntryPoint = "AdsPerformRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsPerformRI_64(IntPtr hTable, uint ulRecNum, string pucRecBuffer);

        public static uint AdsPerformRI(IntPtr hTable, uint ulRecNum, string pucRecBuffer)
        {
            return IntPtr.Size == 4 ? AdsPerformRI_32(hTable, ulRecNum, pucRecBuffer) : AdsPerformRI_64(hTable, ulRecNum, pucRecBuffer);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsLockRecordImplicitly", CharSet = CharSet.Ansi)]
        private static extern uint AdsLockRecordImplicitly_32(IntPtr hTbl, uint ulRec);

        [DllImport("ace64.dll", EntryPoint = "AdsLockRecordImplicitly", CharSet = CharSet.Ansi)]
        private static extern uint AdsLockRecordImplicitly_64(IntPtr hTbl, uint ulRec);

        public static uint AdsLockRecordImplicitly(IntPtr hTbl, uint ulRec)
        {
            return IntPtr.Size == 4 ? AdsLockRecordImplicitly_32(hTbl, ulRec) : AdsLockRecordImplicitly_64(hTbl, ulRec);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetBaseFieldNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBaseFieldNum_32(
        IntPtr hCursor,
        string pucColumnName,
        out ushort pusBaseFieldNum);

        [DllImport("ace64.dll", EntryPoint = "AdsGetBaseFieldNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBaseFieldNum_64(
        IntPtr hCursor,
        string pucColumnName,
        out ushort pusBaseFieldNum);

        public static uint AdsGetBaseFieldNum(
        IntPtr hCursor,
        string pucColumnName,
        out ushort pusBaseFieldNum)
        {
            return IntPtr.Size == 4 ? AdsGetBaseFieldNum_32(hCursor, pucColumnName, out pusBaseFieldNum) : AdsGetBaseFieldNum_64(hCursor, pucColumnName, out pusBaseFieldNum);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetBaseFieldName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBaseFieldName_32(
        IntPtr hTbl,
        ushort usFld,
        [In, Out] char[] pucName,
        ref ushort pusBufLen);

        [DllImport("ace64.dll", EntryPoint = "AdsGetBaseFieldName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBaseFieldName_64(
        IntPtr hTbl,
        ushort usFld,
        [In, Out] char[] pucName,
        ref ushort pusBufLen);

        public static uint AdsGetBaseFieldName(
        IntPtr hTbl,
        ushort usFld,
        char[] pucName,
        ref ushort pusBufLen)
        {
            return IntPtr.Size == 4 ? AdsGetBaseFieldName_32(hTbl, usFld, pucName, ref pusBufLen) : AdsGetBaseFieldName_64(hTbl, usFld, pucName, ref pusBufLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDOpen", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDOpen_32(
        string pucDictionaryPath,
        string pucPassword,
        out IntPtr phDictionary);

        [DllImport("ace64.dll", EntryPoint = "AdsDDOpen", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDOpen_64(
        string pucDictionaryPath,
        string pucPassword,
        out IntPtr phDictionary);

        public static uint AdsDDOpen(
        string pucDictionaryPath,
        string pucPassword,
        out IntPtr phDictionary)
        {
            return IntPtr.Size == 4 ? AdsDDOpen_32(pucDictionaryPath, pucPassword, out phDictionary) : AdsDDOpen_64(pucDictionaryPath, pucPassword, out phDictionary);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDClose", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDClose_32(IntPtr hDictionary);

        [DllImport("ace64.dll", EntryPoint = "AdsDDClose", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDClose_64(IntPtr hDictionary);

        public static uint AdsDDClose(IntPtr hDictionary)
        {
            return IntPtr.Size == 4 ? AdsDDClose_32(hDictionary) : AdsDDClose_64(hDictionary);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsRefreshView", CharSet = CharSet.Ansi)]
        private static extern uint AdsRefreshView_32(out IntPtr phCursor);

        [DllImport("ace64.dll", EntryPoint = "AdsRefreshView", CharSet = CharSet.Ansi)]
        private static extern uint AdsRefreshView_64(out IntPtr phCursor);

        public static uint AdsRefreshView(out IntPtr phCursor)
        {
            return IntPtr.Size == 4 ? AdsRefreshView_32(out phCursor) : AdsRefreshView_64(out phCursor);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDExecuteProcedure", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDExecuteProcedure_32(
        IntPtr hDictionary,
        string pucProcName,
        string pucInput,
        string pucOutput,
        out uint pulRowsAffected,
        uint ulInvokeType);

        [DllImport("ace64.dll", EntryPoint = "AdsDDExecuteProcedure", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDExecuteProcedure_64(
        IntPtr hDictionary,
        string pucProcName,
        string pucInput,
        string pucOutput,
        out uint pulRowsAffected,
        uint ulInvokeType);

        public static uint AdsDDExecuteProcedure(
        IntPtr hDictionary,
        string pucProcName,
        string pucInput,
        string pucOutput,
        out uint pulRowsAffected,
        uint ulInvokeType)
        {
            return IntPtr.Size == 4 ? AdsDDExecuteProcedure_32(hDictionary, pucProcName, pucInput, pucOutput, out pulRowsAffected, ulInvokeType) : AdsDDExecuteProcedure_64(hDictionary, pucProcName, pucInput, pucOutput, out pulRowsAffected, ulInvokeType);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDGetProcInterfaceVersion", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcInterfaceVersion_32(
        IntPtr hDictionary,
        string pucProcName,
        out uint pulInterfaceVersion);

        [DllImport("ace64.dll", EntryPoint = "AdsDDGetProcInterfaceVersion", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcInterfaceVersion_64(
        IntPtr hDictionary,
        string pucProcName,
        out uint pulInterfaceVersion);

        public static uint AdsDDGetProcInterfaceVersion(
        IntPtr hDictionary,
        string pucProcName,
        out uint pulInterfaceVersion)
        {
            return IntPtr.Size == 4 ? AdsDDGetProcInterfaceVersion_32(hDictionary, pucProcName, out pulInterfaceVersion) : AdsDDGetProcInterfaceVersion_64(hDictionary, pucProcName, out pulInterfaceVersion);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetPacketSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetPacketSize_32(IntPtr hConnect, ushort usPacketLength);

        [DllImport("ace64.dll", EntryPoint = "AdsSetPacketSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetPacketSize_64(IntPtr hConnect, ushort usPacketLength);

        public static uint AdsSetPacketSize(IntPtr hConnect, ushort usPacketLength)
        {
            return IntPtr.Size == 4 ? AdsSetPacketSize_32(hConnect, usPacketLength) : AdsSetPacketSize_64(hConnect, usPacketLength);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsVerifyRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsVerifyRI_32(IntPtr hConnect, ushort usExclusive);

        [DllImport("ace64.dll", EntryPoint = "AdsVerifyRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsVerifyRI_64(IntPtr hConnect, ushort usExclusive);

        public static uint AdsVerifyRI(IntPtr hConnect, ushort usExclusive)
        {
            return IntPtr.Size == 4 ? AdsVerifyRI_32(hConnect, usExclusive) : AdsVerifyRI_64(hConnect, usExclusive);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsAddToAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsAddToAOF_32(
        IntPtr hTable,
        string pucFilter,
        ushort usOperation,
        ushort usWhichAOF);

        [DllImport("ace64.dll", EntryPoint = "AdsAddToAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsAddToAOF_64(
        IntPtr hTable,
        string pucFilter,
        ushort usOperation,
        ushort usWhichAOF);

        public static uint AdsAddToAOF(
        IntPtr hTable,
        string pucFilter,
        ushort usOperation,
        ushort usWhichAOF)
        {
            return IntPtr.Size == 4 ? AdsAddToAOF_32(hTable, pucFilter, usOperation, usWhichAOF) : AdsAddToAOF_64(hTable, pucFilter, usOperation, usWhichAOF);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDVerifyUserRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDVerifyUserRights_32(
        IntPtr hObject,
        string pucTableName,
        out uint pulUserRights);

        [DllImport("ace64.dll", EntryPoint = "AdsDDVerifyUserRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDVerifyUserRights_64(
        IntPtr hObject,
        string pucTableName,
        out uint pulUserRights);

        public static uint AdsDDVerifyUserRights(
        IntPtr hObject,
        string pucTableName,
        out uint pulUserRights)
        {
            return IntPtr.Size == 4 ? AdsDDVerifyUserRights_32(hObject, pucTableName, out pulUserRights) : AdsDDVerifyUserRights_64(hObject, pucTableName, out pulUserRights);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetIndexPageSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexPageSize_32(IntPtr hIndex, out uint pulPageSize);

        [DllImport("ace64.dll", EntryPoint = "AdsGetIndexPageSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexPageSize_64(IntPtr hIndex, out uint pulPageSize);

        public static uint AdsGetIndexPageSize(IntPtr hIndex, out uint pulPageSize)
        {
            return IntPtr.Size == 4 ? AdsGetIndexPageSize_32(hIndex, out pulPageSize) : AdsGetIndexPageSize_64(hIndex, out pulPageSize);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDAutoCreateTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAutoCreateTable_32(
        IntPtr hConnect,
        string pucTableName,
        string pucCollation);

        [DllImport("ace64.dll", EntryPoint = "AdsDDAutoCreateTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAutoCreateTable_64(
        IntPtr hConnect,
        string pucTableName,
        string pucCollation);

        public static uint AdsDDAutoCreateTable(
        IntPtr hConnect,
        string pucTableName,
        string pucCollation)
        {
            return IntPtr.Size == 4 ? AdsDDAutoCreateTable_32(hConnect, pucTableName, pucCollation) : AdsDDAutoCreateTable_64(hConnect, pucTableName, pucCollation);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDAutoCreateIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAutoCreateIndex_32(
        IntPtr hConnect,
        string pucTableName,
        string pucIndexName,
        string pucCollation);

        [DllImport("ace64.dll", EntryPoint = "AdsDDAutoCreateIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAutoCreateIndex_64(
        IntPtr hConnect,
        string pucTableName,
        string pucIndexName,
        string pucCollation);

        public static uint AdsDDAutoCreateIndex(
        IntPtr hConnect,
        string pucTableName,
        string pucIndexName,
        string pucCollation)
        {
            return IntPtr.Size == 4 ? AdsDDAutoCreateIndex_32(hConnect, pucTableName, pucIndexName, pucCollation) : AdsDDAutoCreateIndex_64(hConnect, pucTableName, pucIndexName, pucCollation);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetROWIDPrefix", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetROWIDPrefix_32(
        IntPtr hTable,
        string pucRowIDPrefix,
        ushort usBufferLen);

        [DllImport("ace64.dll", EntryPoint = "AdsGetROWIDPrefix", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetROWIDPrefix_64(
        IntPtr hTable,
        string pucRowIDPrefix,
        ushort usBufferLen);

        public static uint AdsGetROWIDPrefix(IntPtr hTable, string pucRowIDPrefix, ushort usBufferLen)
        {
            return IntPtr.Size == 4 ? AdsGetROWIDPrefix_32(hTable, pucRowIDPrefix, usBufferLen) : AdsGetROWIDPrefix_64(hTable, pucRowIDPrefix, usBufferLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetColumnPermissions", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetColumnPermissions_32(
        IntPtr hTable,
        ushort usColumnNum,
        string pucPermissions);

        [DllImport("ace64.dll", EntryPoint = "AdsGetColumnPermissions", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetColumnPermissions_64(
        IntPtr hTable,
        ushort usColumnNum,
        string pucPermissions);

        public static uint AdsGetColumnPermissions(
        IntPtr hTable,
        ushort usColumnNum,
        string pucPermissions)
        {
            return IntPtr.Size == 4 ? AdsGetColumnPermissions_32(hTable, usColumnNum, pucPermissions) : AdsGetColumnPermissions_64(hTable, usColumnNum, pucPermissions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetSQLStmtParams", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSQLStmtParams_32(string pucStatement);

        [DllImport("ace64.dll", EntryPoint = "AdsGetSQLStmtParams", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSQLStmtParams_64(string pucStatement);

        public static uint AdsGetSQLStmtParams(string pucStatement)
        {
            return IntPtr.Size == 4 ? AdsGetSQLStmtParams_32(pucStatement) : AdsGetSQLStmtParams_64(pucStatement);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsRemoveSQLComments", CharSet = CharSet.Ansi)]
        private static extern uint AdsRemoveSQLComments_32(string pucStatement);

        [DllImport("ace64.dll", EntryPoint = "AdsRemoveSQLComments", CharSet = CharSet.Ansi)]
        private static extern uint AdsRemoveSQLComments_64(string pucStatement);

        public static uint AdsRemoveSQLComments(string pucStatement)
        {
            return IntPtr.Size == 4 ? AdsRemoveSQLComments_32(pucStatement) : AdsRemoveSQLComments_64(pucStatement);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetBaseTableAccess", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBaseTableAccess_32(IntPtr hTbl, ushort usAccessBase);

        [DllImport("ace64.dll", EntryPoint = "AdsSetBaseTableAccess", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBaseTableAccess_64(IntPtr hTbl, ushort usAccessBase);

        public static uint AdsSetBaseTableAccess(IntPtr hTbl, ushort usAccessBase)
        {
            return IntPtr.Size == 4 ? AdsSetBaseTableAccess_32(hTbl, usAccessBase) : AdsSetBaseTableAccess_64(hTbl, usAccessBase);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsCopyTableTop", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableTop_32(
        IntPtr hObj,
        IntPtr hDestTbl,
        uint ulNumTopRecords);

        [DllImport("ace64.dll", EntryPoint = "AdsCopyTableTop", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableTop_64(
        IntPtr hObj,
        IntPtr hDestTbl,
        uint ulNumTopRecords);

        public static uint AdsCopyTableTop(IntPtr hObj, IntPtr hDestTbl, uint ulNumTopRecords)
        {
            return IntPtr.Size == 4 ? AdsCopyTableTop_32(hObj, hDestTbl, ulNumTopRecords) : AdsCopyTableTop_64(hObj, hDestTbl, ulNumTopRecords);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsCopyTableTop100", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableTop100_32(
        IntPtr hObj,
        IntPtr hDestTbl,
        uint ulNumTopRecords,
        uint ulOffset);

        [DllImport("ace64.dll", EntryPoint = "AdsCopyTableTop100", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableTop100_64(
        IntPtr hObj,
        IntPtr hDestTbl,
        uint ulNumTopRecords,
        uint ulOffset);

        public static uint AdsCopyTableTop100(
        IntPtr hObj,
        IntPtr hDestTbl,
        uint ulNumTopRecords,
        uint ulOffset)
        {
            return IntPtr.Size == 4 ? AdsCopyTableTop100_32(hObj, hDestTbl, ulNumTopRecords, ulOffset) : AdsCopyTableTop100_64(hObj, hDestTbl, ulNumTopRecords, ulOffset);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetNullRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNullRecord_32(IntPtr hTbl, string pucBuf, uint ulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsGetNullRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNullRecord_64(IntPtr hTbl, string pucBuf, uint ulLen);

        public static uint AdsGetNullRecord(IntPtr hTbl, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsGetNullRecord_32(hTbl, pucBuf, ulLen) : AdsGetNullRecord_64(hTbl, pucBuf, ulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetProperty_32(IntPtr hObj, uint ulOperation, uint ulValue);

        [DllImport("ace64.dll", EntryPoint = "AdsSetProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetProperty_64(IntPtr hObj, uint ulOperation, uint ulValue);

        public static uint AdsSetProperty(IntPtr hObj, uint ulOperation, uint ulValue)
        {
            return IntPtr.Size == 4 ? AdsSetProperty_32(hObj, ulOperation, ulValue) : AdsSetProperty_64(hObj, ulOperation, ulValue);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetProperty90", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetProperty90_32(IntPtr hObj, uint ulOperation, ulong uqValue);

        [DllImport("ace64.dll", EntryPoint = "AdsSetProperty90", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetProperty90_64(IntPtr hObj, uint ulOperation, ulong uqValue);

        public static uint AdsSetProperty90(IntPtr hObj, uint ulOperation, ulong uqValue)
        {
            return IntPtr.Size == 4 ? AdsSetProperty90_32(hObj, ulOperation, uqValue) : AdsSetProperty90_64(hObj, ulOperation, uqValue);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsCloseCachedTrigStatements", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseCachedTrigStatements_32(IntPtr hConnection, int lTableID);

        [DllImport("ace64.dll", EntryPoint = "AdsCloseCachedTrigStatements", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseCachedTrigStatements_64(IntPtr hConnection, int lTableID);

        public static uint AdsCloseCachedTrigStatements(IntPtr hConnection, int lTableID)
        {
            return IntPtr.Size == 4 ? AdsCloseCachedTrigStatements_32(hConnection, lTableID) : AdsCloseCachedTrigStatements_64(hConnection, lTableID);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsAreTriggersEnabled", CharSet = CharSet.Ansi)]
        private static extern uint AdsAreTriggersEnabled_32(
        IntPtr hConnection,
        int lTableID,
        byte ucUpdateType,
        out uint pulEnabled);

        [DllImport("ace64.dll", EntryPoint = "AdsAreTriggersEnabled", CharSet = CharSet.Ansi)]
        private static extern uint AdsAreTriggersEnabled_64(
        IntPtr hConnection,
        int lTableID,
        byte ucUpdateType,
        out uint pulEnabled);

        public static uint AdsAreTriggersEnabled(
        IntPtr hConnection,
        int lTableID,
        byte ucUpdateType,
        out uint pulEnabled)
        {
            return IntPtr.Size == 4 ? AdsAreTriggersEnabled_32(hConnection, lTableID, ucUpdateType, out pulEnabled) : AdsAreTriggersEnabled_64(hConnection, lTableID, ucUpdateType, out pulEnabled);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDCreateASA", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateASA_32(
        IntPtr hConnect,
        string pucDictionaryPath,
        ushort usEncrypt,
        string pucDescription,
        string pucPassword);

        [DllImport("ace64.dll", EntryPoint = "AdsDDCreateASA", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateASA_64(
        IntPtr hConnect,
        string pucDictionaryPath,
        ushort usEncrypt,
        string pucDescription,
        string pucPassword);

        public static uint AdsDDCreateASA(
        IntPtr hConnect,
        string pucDictionaryPath,
        ushort usEncrypt,
        string pucDescription,
        string pucPassword)
        {
            return IntPtr.Size == 4 ? AdsDDCreateASA_32(hConnect, pucDictionaryPath, usEncrypt, pucDescription, pucPassword) : AdsDDCreateASA_64(hConnect, pucDictionaryPath, usEncrypt, pucDescription, pucPassword);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGotoBOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBOF_32(IntPtr hObj);

        [DllImport("ace64.dll", EntryPoint = "AdsGotoBOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBOF_64(IntPtr hObj);

        public static uint AdsGotoBOF(IntPtr hObj)
        {
            return IntPtr.Size == 4 ? AdsGotoBOF_32(hObj) : AdsGotoBOF_64(hObj);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGotoEOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoEOF_32(IntPtr hObj);

        [DllImport("ace64.dll", EntryPoint = "AdsGotoEOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoEOF_64(IntPtr hObj);

        public static uint AdsGotoEOF(IntPtr hObj)
        {
            return IntPtr.Size == 4 ? AdsGotoEOF_32(hObj) : AdsGotoEOF_64(hObj);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsCreateMemTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateMemTable_32(
        IntPtr hConnection,
        string pucName,
        ushort usTableType,
        ushort usCharType,
        string pucFields,
        uint ulSize,
        out IntPtr phTable);

        [DllImport("ace64.dll", EntryPoint = "AdsCreateMemTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateMemTable_64(
        IntPtr hConnection,
        string pucName,
        ushort usTableType,
        ushort usCharType,
        string pucFields,
        uint ulSize,
        out IntPtr phTable);

        public static uint AdsCreateMemTable(
        IntPtr hConnection,
        string pucName,
        ushort usTableType,
        ushort usCharType,
        string pucFields,
        uint ulSize,
        out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsCreateMemTable_32(hConnection, pucName, usTableType, usCharType, pucFields, ulSize, out phTable) : AdsCreateMemTable_64(hConnection, pucName, usTableType, usCharType, pucFields, ulSize, out phTable);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsCreateMemTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateMemTable90_32(
        IntPtr hConnection,
        string pucName,
        ushort usTableType,
        ushort usCharType,
        string pucFields,
        uint ulSize,
        string pucCollation,
        out IntPtr phTable);

        [DllImport("ace64.dll", EntryPoint = "AdsCreateMemTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateMemTable90_64(
        IntPtr hConnection,
        string pucName,
        ushort usTableType,
        ushort usCharType,
        string pucFields,
        uint ulSize,
        string pucCollation,
        out IntPtr phTable);

        public static uint AdsCreateMemTable90(
        IntPtr hConnection,
        string pucName,
        ushort usTableType,
        ushort usCharType,
        string pucFields,
        uint ulSize,
        string pucCollation,
        out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsCreateMemTable90_32(hConnection, pucName, usTableType, usCharType, pucFields, ulSize, pucCollation, out phTable) : AdsCreateMemTable90_64(hConnection, pucName, usTableType, usCharType, pucFields, ulSize, pucCollation, out phTable);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetTableWAN", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableWAN_32(IntPtr hTable, out ushort pusWAN);

        [DllImport("ace64.dll", EntryPoint = "AdsGetTableWAN", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableWAN_64(IntPtr hTable, out ushort pusWAN);

        public static uint AdsGetTableWAN(IntPtr hTable, out ushort pusWAN)
        {
            return IntPtr.Size == 4 ? AdsGetTableWAN_32(hTable, out pusWAN) : AdsGetTableWAN_64(hTable, out pusWAN);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsGetFTSScore", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFTSScore_32(
        IntPtr hIndex,
        uint ulRecord,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out uint pulScore);

        [DllImport("ace64.dll", EntryPoint = "AdsGetFTSScore", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFTSScore_64(
        IntPtr hIndex,
        uint ulRecord,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out uint pulScore);

        public static uint AdsGetFTSScore(
        IntPtr hIndex,
        uint ulRecord,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out uint pulScore)
        {
            return IntPtr.Size == 4 ? AdsGetFTSScore_32(hIndex, ulRecord, pucKey, usKeyLen, usDataType, usSeekType, out pulScore) : AdsGetFTSScore_64(hIndex, ulRecord, pucKey, usKeyLen, usDataType, usSeekType, out pulScore);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertDateToJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertDateToJulian_32(
        IntPtr hConnect,
        string pucDate,
        ushort usLen,
        out double pdJulian);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertDateToJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertDateToJulian_64(
        IntPtr hConnect,
        string pucDate,
        ushort usLen,
        out double pdJulian);

        public static uint AdsConvertDateToJulian(
        IntPtr hConnect,
        string pucDate,
        ushort usLen,
        out double pdJulian)
        {
            return IntPtr.Size == 4 ? AdsConvertDateToJulian_32(hConnect, pucDate, usLen, out pdJulian) : AdsConvertDateToJulian_64(hConnect, pucDate, usLen, out pdJulian);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetActiveDictionary", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetActiveDictionary_32(
        IntPtr hConnect,
        string pucLinkName,
        out IntPtr phDictionary);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetActiveDictionary", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetActiveDictionary_64(
        IntPtr hConnect,
        string pucLinkName,
        out IntPtr phDictionary);

        public static uint AdsDDSetActiveDictionary(
        IntPtr hConnect,
        string pucLinkName,
        out IntPtr phDictionary)
        {
            return IntPtr.Size == 4 ? AdsDDSetActiveDictionary_32(hConnect, pucLinkName, out phDictionary) : AdsDDSetActiveDictionary_64(hConnect, pucLinkName, out phDictionary);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDCreateLinkPre71", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateLinkPre71_32(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsDDCreateLinkPre71", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateLinkPre71_64(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions);

        public static uint AdsDDCreateLinkPre71(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateLinkPre71_32(hDBConn, pucLinkAlias, pucLinkedDDPath, pucUserName, pucPassword, ulOptions) : AdsDDCreateLinkPre71_64(hDBConn, pucLinkAlias, pucLinkedDDPath, pucUserName, pucPassword, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDDropLinkPre71", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropLinkPre71_32(
        IntPtr hDBConn,
        string pucLinkedDD,
        ushort usDropGlobal);

        [DllImport("ace64.dll", EntryPoint = "AdsDDDropLinkPre71", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropLinkPre71_64(
        IntPtr hDBConn,
        string pucLinkedDD,
        ushort usDropGlobal);

        public static uint AdsDDDropLinkPre71(IntPtr hDBConn, string pucLinkedDD, ushort usDropGlobal)
        {
            return IntPtr.Size == 4 ? AdsDDDropLinkPre71_32(hDBConn, pucLinkedDD, usDropGlobal) : AdsDDDropLinkPre71_64(hDBConn, pucLinkedDD, usDropGlobal);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDDisableTriggers", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDisableTriggers_32(
        IntPtr hDictionary,
        string pucObjectName,
        string pucParent,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsDDDisableTriggers", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDisableTriggers_64(
        IntPtr hDictionary,
        string pucObjectName,
        string pucParent,
        uint ulOptions);

        public static uint AdsDDDisableTriggers(
        IntPtr hDictionary,
        string pucObjectName,
        string pucParent,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDDisableTriggers_32(hDictionary, pucObjectName, pucParent, ulOptions) : AdsDDDisableTriggers_64(hDictionary, pucObjectName, pucParent, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDEnableTriggers", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDEnableTriggers_32(
        IntPtr hDictionary,
        string pucObjectName,
        string pucParent,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsDDEnableTriggers", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDEnableTriggers_64(
        IntPtr hDictionary,
        string pucObjectName,
        string pucParent,
        uint ulOptions);

        public static uint AdsDDEnableTriggers(
        IntPtr hDictionary,
        string pucObjectName,
        string pucParent,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDEnableTriggers_32(hDictionary, pucObjectName, pucParent, ulOptions) : AdsDDEnableTriggers_64(hDictionary, pucObjectName, pucParent, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsCreateCriticalSection", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateCriticalSection_32(IntPtr hObj, uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsCreateCriticalSection", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateCriticalSection_64(IntPtr hObj, uint ulOptions);

        public static uint AdsCreateCriticalSection(IntPtr hObj, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsCreateCriticalSection_32(hObj, ulOptions) : AdsCreateCriticalSection_64(hObj, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsWaitForObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsWaitForObject_32(IntPtr hObj, uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsWaitForObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsWaitForObject_64(IntPtr hObj, uint ulOptions);

        public static uint AdsWaitForObject(IntPtr hObj, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsWaitForObject_32(hObj, ulOptions) : AdsWaitForObject_64(hObj, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsReleaseObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsReleaseObject_32(IntPtr hObj);

        [DllImport("ace64.dll", EntryPoint = "AdsReleaseObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsReleaseObject_64(IntPtr hObj);

        public static uint AdsReleaseObject(IntPtr hObj)
        {
            return IntPtr.Size == 4 ? AdsReleaseObject_32(hObj) : AdsReleaseObject_64(hObj);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsBackupDatabase", CharSet = CharSet.Ansi)]
        private static extern uint AdsBackupDatabase_32(
        IntPtr hConnect,
        IntPtr hOutputTable,
        string pucSourcePath,
        string pucSourceMask,
        string pucDestPath,
        string pucOptions,
        string pucFreeTablePasswords,
        ushort usCharType,
        ushort usLockingMode,
        ushort usCheckRights,
        ushort usTableType,
        string pucCollation,
        byte ucDDConn);

        [DllImport("ace64.dll", EntryPoint = "AdsBackupDatabase", CharSet = CharSet.Ansi)]
        private static extern uint AdsBackupDatabase_64(
        IntPtr hConnect,
        IntPtr hOutputTable,
        string pucSourcePath,
        string pucSourceMask,
        string pucDestPath,
        string pucOptions,
        string pucFreeTablePasswords,
        ushort usCharType,
        ushort usLockingMode,
        ushort usCheckRights,
        ushort usTableType,
        string pucCollation,
        byte ucDDConn);

        public static uint AdsBackupDatabase(
        IntPtr hConnect,
        IntPtr hOutputTable,
        string pucSourcePath,
        string pucSourceMask,
        string pucDestPath,
        string pucOptions,
        string pucFreeTablePasswords,
        ushort usCharType,
        ushort usLockingMode,
        ushort usCheckRights,
        ushort usTableType,
        string pucCollation,
        byte ucDDConn)
        {
            return IntPtr.Size == 4 ? AdsBackupDatabase_32(hConnect, hOutputTable, pucSourcePath, pucSourceMask, pucDestPath, pucOptions, pucFreeTablePasswords, usCharType, usLockingMode, usCheckRights, usTableType, pucCollation, ucDDConn) : AdsBackupDatabase_64(hConnect, hOutputTable, pucSourcePath, pucSourceMask, pucDestPath, pucOptions, pucFreeTablePasswords, usCharType, usLockingMode, usCheckRights, usTableType, pucCollation, ucDDConn);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsRestoreDatabase", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestoreDatabase_32(
        IntPtr hConnect,
        IntPtr hOutputTable,
        string pucSourcePath,
        string pucSourcePassword,
        string pucDestPath,
        string pucDestPassword,
        string pucOptions,
        string pucFreeTablePasswords,
        ushort usCharType,
        ushort usLockingMode,
        ushort usCheckRights,
        ushort usTableType,
        string pucCollation,
        byte ucDDConn);

        [DllImport("ace64.dll", EntryPoint = "AdsRestoreDatabase", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestoreDatabase_64(
        IntPtr hConnect,
        IntPtr hOutputTable,
        string pucSourcePath,
        string pucSourcePassword,
        string pucDestPath,
        string pucDestPassword,
        string pucOptions,
        string pucFreeTablePasswords,
        ushort usCharType,
        ushort usLockingMode,
        ushort usCheckRights,
        ushort usTableType,
        string pucCollation,
        byte ucDDConn);

        public static uint AdsRestoreDatabase(
        IntPtr hConnect,
        IntPtr hOutputTable,
        string pucSourcePath,
        string pucSourcePassword,
        string pucDestPath,
        string pucDestPassword,
        string pucOptions,
        string pucFreeTablePasswords,
        ushort usCharType,
        ushort usLockingMode,
        ushort usCheckRights,
        ushort usTableType,
        string pucCollation,
        byte ucDDConn)
        {
            return IntPtr.Size == 4 ? AdsRestoreDatabase_32(hConnect, hOutputTable, pucSourcePath, pucSourcePassword, pucDestPath, pucDestPassword, pucOptions, pucFreeTablePasswords, usCharType, usLockingMode, usCheckRights, usTableType, pucCollation, ucDDConn) : AdsRestoreDatabase_64(hConnect, hOutputTable, pucSourcePath, pucSourcePassword, pucDestPath, pucDestPassword, pucOptions, pucFreeTablePasswords, usCharType, usLockingMode, usCheckRights, usTableType, pucCollation, ucDDConn);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsSetRecordPartial", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRecordPartial_32(IntPtr hObj, string pucRec, uint ulLen);

        [DllImport("ace64.dll", EntryPoint = "AdsSetRecordPartial", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRecordPartial_64(IntPtr hObj, string pucRec, uint ulLen);

        public static uint AdsSetRecordPartial(IntPtr hObj, string pucRec, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetRecordPartial_32(hObj, pucRec, ulLen) : AdsSetRecordPartial_64(hObj, pucRec, ulLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTriggerProperty_32(
        IntPtr hDictionary,
        string pucTriggerName,
        ushort usPropertyID,
        string pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTriggerProperty_64(
        IntPtr hDictionary,
        string pucTriggerName,
        ushort usPropertyID,
        string pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetTriggerProperty(
        IntPtr hDictionary,
        string pucTriggerName,
        ushort usPropertyID,
        string pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetTriggerProperty_32(hDictionary, pucTriggerName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetTriggerProperty_64(hDictionary, pucTriggerName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDCreateFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateFunction_32(
        IntPtr hDictionary,
        string pucName,
        string pucReturnType,
        ushort usInputParamCnt,
        string pucInputParams,
        string pucFuncBody,
        string pucComments);

        [DllImport("ace64.dll", EntryPoint = "AdsDDCreateFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateFunction_64(
        IntPtr hDictionary,
        string pucName,
        string pucReturnType,
        ushort usInputParamCnt,
        string pucInputParams,
        string pucFuncBody,
        string pucComments);

        public static uint AdsDDCreateFunction(
        IntPtr hDictionary,
        string pucName,
        string pucReturnType,
        ushort usInputParamCnt,
        string pucInputParams,
        string pucFuncBody,
        string pucComments)
        {
            return IntPtr.Size == 4 ? AdsDDCreateFunction_32(hDictionary, pucName, pucReturnType, usInputParamCnt, pucInputParams, pucFuncBody, pucComments) : AdsDDCreateFunction_64(hDictionary, pucName, pucReturnType, usInputParamCnt, pucInputParams, pucFuncBody, pucComments);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDCreateFunction100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateFunction100_32(
        IntPtr hDictionary,
        string pucName,
        string pucReturnType,
        ushort usInputParamCnt,
        string pucInputParams,
        string pucFuncBody,
        string pucComments,
        uint ulEncoding,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsDDCreateFunction100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateFunction100_64(
        IntPtr hDictionary,
        string pucName,
        string pucReturnType,
        ushort usInputParamCnt,
        string pucInputParams,
        string pucFuncBody,
        string pucComments,
        uint ulEncoding,
        uint ulOptions);

        public static uint AdsDDCreateFunction100(
        IntPtr hDictionary,
        string pucName,
        string pucReturnType,
        ushort usInputParamCnt,
        string pucInputParams,
        string pucFuncBody,
        string pucComments,
        uint ulEncoding,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateFunction100_32(hDictionary, pucName, pucReturnType, usInputParamCnt, pucInputParams, pucFuncBody, pucComments, ulEncoding, ulOptions) : AdsDDCreateFunction100_64(hDictionary, pucName, pucReturnType, usInputParamCnt, pucInputParams, pucFuncBody, pucComments, ulEncoding, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDDropFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropFunction_32(IntPtr hDictionary, string pucName);

        [DllImport("ace64.dll", EntryPoint = "AdsDDDropFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropFunction_64(IntPtr hDictionary, string pucName);

        public static uint AdsDDDropFunction(IntPtr hDictionary, string pucName)
        {
            return IntPtr.Size == 4 ? AdsDDDropFunction_32(hDictionary, pucName) : AdsDDDropFunction_64(hDictionary, pucName);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDGetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsDDGetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetObjectProperty(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetObjectProperty_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetObjectProperty_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDGetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsDDGetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetObjectProperty(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetObjectProperty_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetObjectProperty_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDGetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsDDGetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetObjectProperty(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetObjectProperty_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetObjectProperty_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDGetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty100_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding);

        [DllImport("ace64.dll", EntryPoint = "AdsDDGetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty100_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding);

        public static uint AdsDDGetObjectProperty100(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding)
        {
            return IntPtr.Size == 4 ? AdsDDGetObjectProperty100_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, ref pusPropertyLen, out pulEncoding) : AdsDDGetObjectProperty100_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, ref pusPropertyLen, out pulEncoding);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDGetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty100_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding);

        [DllImport("ace64.dll", EntryPoint = "AdsDDGetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty100_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding);

        public static uint AdsDDGetObjectProperty100(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding)
        {
            return IntPtr.Size == 4 ? AdsDDGetObjectProperty100_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, ref pusPropertyLen, out pulEncoding) : AdsDDGetObjectProperty100_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, ref pusPropertyLen, out pulEncoding);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDGetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty100_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding);

        [DllImport("ace64.dll", EntryPoint = "AdsDDGetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetObjectProperty100_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding);

        public static uint AdsDDGetObjectProperty100(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen,
        out uint pulEncoding)
        {
            return IntPtr.Size == 4 ? AdsDDGetObjectProperty100_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, ref pusPropertyLen, out pulEncoding) : AdsDDGetObjectProperty100_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, ref pusPropertyLen, out pulEncoding);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetObjectProperty(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetObjectProperty_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetObjectProperty_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetObjectProperty(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetObjectProperty_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetObjectProperty_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetObjectProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetObjectProperty(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetObjectProperty_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetObjectProperty_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty100_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen,
        uint ulEncoding);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty100_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen,
        uint ulEncoding);

        public static uint AdsDDSetObjectProperty100(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen,
        uint ulEncoding)
        {
            return IntPtr.Size == 4 ? AdsDDSetObjectProperty100_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, usPropertyLen, ulEncoding) : AdsDDSetObjectProperty100_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pvProperty, usPropertyLen, ulEncoding);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty100_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen,
        uint ulEncoding);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty100_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen,
        uint ulEncoding);

        public static uint AdsDDSetObjectProperty100(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen,
        uint ulEncoding)
        {
            return IntPtr.Size == 4 ? AdsDDSetObjectProperty100_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, usPropertyLen, ulEncoding) : AdsDDSetObjectProperty100_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, pucProperty, usPropertyLen, ulEncoding);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDSetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty100_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        uint ulEncoding);

        [DllImport("ace64.dll", EntryPoint = "AdsDDSetObjectProperty100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectProperty100_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        uint ulEncoding);

        public static uint AdsDDSetObjectProperty100(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucParent,
        string pucName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        uint ulEncoding)
        {
            return IntPtr.Size == 4 ? AdsDDSetObjectProperty100_32(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, usPropertyLen, ulEncoding) : AdsDDSetObjectProperty100_64(hDictionary, usObjectType, pucParent, pucName, usPropertyID, ref pusProperty, usPropertyLen, ulEncoding);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDCreatePackage", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreatePackage_32(
        IntPtr hDictionary,
        string pucName,
        string pucComments);

        [DllImport("ace64.dll", EntryPoint = "AdsDDCreatePackage", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreatePackage_64(
        IntPtr hDictionary,
        string pucName,
        string pucComments);

        public static uint AdsDDCreatePackage(IntPtr hDictionary, string pucName, string pucComments)
        {
            return IntPtr.Size == 4 ? AdsDDCreatePackage_32(hDictionary, pucName, pucComments) : AdsDDCreatePackage_64(hDictionary, pucName, pucComments);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsDDDropPackage", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropPackage_32(IntPtr hDictionary, string pucName);

        [DllImport("ace64.dll", EntryPoint = "AdsDDDropPackage", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropPackage_64(IntPtr hDictionary, string pucName);

        public static uint AdsDDDropPackage(IntPtr hDictionary, string pucName)
        {
            return IntPtr.Size == 4 ? AdsDDDropPackage_32(hDictionary, pucName) : AdsDDDropPackage_64(hDictionary, pucName);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsCopyTableStructure81", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableStructure81_32(
        IntPtr hTable,
        string pucFile,
        uint ulOptions);

        [DllImport("ace64.dll", EntryPoint = "AdsCopyTableStructure81", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableStructure81_64(
        IntPtr hTable,
        string pucFile,
        uint ulOptions);

        public static uint AdsCopyTableStructure81(IntPtr hTable, string pucFile, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsCopyTableStructure81_32(hTable, pucFile, ulOptions) : AdsCopyTableStructure81_64(hTable, pucFile, ulOptions);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsAccessVfpSystemField", CharSet = CharSet.Ansi)]
        private static extern uint AdsAccessVfpSystemField_32(
        IntPtr hTable,
        string pucFldName,
        string pucBuffer,
        uint ulOptions,
        out ushort puFlag);

        [DllImport("ace64.dll", EntryPoint = "AdsAccessVfpSystemField", CharSet = CharSet.Ansi)]
        private static extern uint AdsAccessVfpSystemField_64(
        IntPtr hTable,
        string pucFldName,
        string pucBuffer,
        uint ulOptions,
        out ushort puFlag);

        public static uint AdsAccessVfpSystemField(
        IntPtr hTable,
        string pucFldName,
        string pucBuffer,
        uint ulOptions,
        out ushort puFlag)
        {
            return IntPtr.Size == 4 ? AdsAccessVfpSystemField_32(hTable, pucFldName, pucBuffer, ulOptions, out puFlag) : AdsAccessVfpSystemField_64(hTable, pucFldName, pucBuffer, ulOptions, out puFlag);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsAccessVfpSystemField", CharSet = CharSet.Ansi)]
        private static extern uint AdsAccessVfpSystemField_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        string pucBuffer,
        uint ulOptions,
        out ushort puFlag);

        [DllImport("ace64.dll", EntryPoint = "AdsAccessVfpSystemField", CharSet = CharSet.Ansi)]
        private static extern uint AdsAccessVfpSystemField_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        string pucBuffer,
        uint ulOptions,
        out ushort puFlag);

        public static uint AdsAccessVfpSystemField(
        IntPtr hTable,
        uint lFieldOrdinal,
        string pucBuffer,
        uint ulOptions,
        out ushort puFlag)
        {
            return IntPtr.Size == 4 ? AdsAccessVfpSystemField_32(hTable, lFieldOrdinal, pucBuffer, ulOptions, out puFlag) : AdsAccessVfpSystemField_64(hTable, lFieldOrdinal, pucBuffer, ulOptions, out puFlag);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertCodePageToUnicode", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertCodePageToUnicode_32(
        IntPtr hConn,
        uint ulCodePage,
        string pucBuffer,
        int lByteLen,
        string pwcBuffer,
        out uint pulCodeUnits);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertCodePageToUnicode", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertCodePageToUnicode_64(
        IntPtr hConn,
        uint ulCodePage,
        string pucBuffer,
        int lByteLen,
        string pwcBuffer,
        out uint pulCodeUnits);

        public static uint AdsConvertCodePageToUnicode(
        IntPtr hConn,
        uint ulCodePage,
        string pucBuffer,
        int lByteLen,
        string pwcBuffer,
        out uint pulCodeUnits)
        {
            return IntPtr.Size == 4 ? AdsConvertCodePageToUnicode_32(hConn, ulCodePage, pucBuffer, lByteLen, pwcBuffer, out pulCodeUnits) : AdsConvertCodePageToUnicode_64(hConn, ulCodePage, pucBuffer, lByteLen, pwcBuffer, out pulCodeUnits);
        }

        [DllImport("ace32.dll", EntryPoint = "AdsConvertUnicodeToCodePage", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertUnicodeToCodePage_32(
        IntPtr hConn,
        uint ulCodePage,
        string pwcBuffer,
        int lCodeUnits,
        [In, Out] char[] pucBuffer,
        ref uint pulByteLen);

        [DllImport("ace64.dll", EntryPoint = "AdsConvertUnicodeToCodePage", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertUnicodeToCodePage_64(
        IntPtr hConn,
        uint ulCodePage,
        string pwcBuffer,
        int lCodeUnits,
        [In, Out] char[] pucBuffer,
        ref uint pulByteLen);

        public static uint AdsConvertUnicodeToCodePage(
        IntPtr hConn,
        uint ulCodePage,
        string pwcBuffer,
        int lCodeUnits,
        char[] pucBuffer,
        ref uint pulByteLen)
        {
            return IntPtr.Size == 4 ? AdsConvertUnicodeToCodePage_32(hConn, ulCodePage, pwcBuffer, lCodeUnits, pucBuffer, ref pulByteLen) : AdsConvertUnicodeToCodePage_64(hConn, ulCodePage, pwcBuffer, lCodeUnits, pucBuffer, ref pulByteLen);
        }

        private enum PathInfo
        {
            PATH_DRIVE = 1,
            PATH_SERVER = 2,
            PATH_VOLUME = 3,
            PATH_PATH = 4,
            PATH_BASENAME = 5,
            PATH_FILENAME = 6,
            PATH_EXTENSION = 7,
        }
    }
}