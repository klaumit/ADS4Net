using System;
using System.Runtime.InteropServices;

namespace AdvantageClientEngine
{
    public class ACE
    {
        public const ushort ADS_DEFAULT_SQL_TIMEOUT = 0;
        public const ushort ADS_FALSE = 0;
        public const ushort ADS_TRUE = 1;
        public const ushort ADS_DEFAULT = 0;
        public const ushort ADS_ANSI = 1;
        public const ushort ADS_OEM = 2;
        public const ushort ADS_MAX_CHAR_SETS = 118;
        public const ushort ADS_CHECKRIGHTS = 1;
        public const ushort ADS_IGNORERIGHTS = 2;
        public const uint ADS_RESPECT_RIGHTS_CHECKING = 1;
        public const uint ADS_IGNORE_RIGHTS_CHECKING = 2;
        public const uint ADS_INC_USERCOUNT = 1;
        public const uint ADS_STORED_PROC_CONN = 2;
        public const uint ADS_COMPRESS_ALWAYS = 4;
        public const uint ADS_COMPRESS_NEVER = 8;
        public const uint ADS_COMPRESS_INTERNET = 12;
        public const uint ADS_REPLICATION_CONNECTION = 16;
        public const uint ADS_UDP_IP_CONNECTION = 32;
        public const uint ADS_IPX_CONNECTION = 64;
        public const uint ADS_TCP_IP_CONNECTION = 128;
        public const uint ADS_TCP_IP_V6_CONNECTION = 256;
        public const uint ADS_NOTIFICATION_CONNECTION = 512;
        public const uint ADS_TLS_CONNECTION = 4096;
        public const uint ADS_CHECK_FREE_TABLE_ACCESS = 8192;
        public const uint ADS_EXCLUSIVE = 1;
        public const uint ADS_READONLY = 2;
        public const uint ADS_SHARED = 4;
        public const uint ADS_CLIPPER_MEMOS = 8;
        public const uint ADS_TABLE_PERM_READ = 16;
        public const uint ADS_TABLE_PERM_UPDATE = 32;
        public const uint ADS_TABLE_PERM_INSERT = 64;
        public const uint ADS_TABLE_PERM_DELETE = 128;
        public const uint ADS_REINDEX_ON_COLLATION_MISMATCH = 256;
        public const uint ADS_IGNORE_COLLATION_MISMATCH = 512;
        public const uint ADS_FREE_TABLE = 4096;
        public const uint ADS_TEMP_TABLE = 8192;
        public const uint ADS_DICTIONARY_BOUND_TABLE = 16384;
        public const uint ADS_CACHE_READS = 536870912;
        public const uint ADS_CACHE_WRITES = 1073741824;
        public const uint ADS_ASCENDING = 0;
        public const uint ADS_UNIQUE = 1;
        public const uint ADS_COMPOUND = 2;
        public const uint ADS_CUSTOM = 4;
        public const uint ADS_DESCENDING = 8;
        public const uint ADS_USER_DEFINED = 16;
        public const uint ADS_FTS_INDEX = 32;
        public const uint ADS_FTS_FIXED = 64;
        public const uint ADS_FTS_CASE_SENSITIVE = 128;
        public const uint ADS_FTS_KEEP_SCORE = 256;
        public const uint ADS_FTS_PROTECT_NUMBERS = 512;
        public const uint ADS_NOT_AUTO_OPEN = 1024;
        public const uint ADS_CANDIDATE = 2048;
        public const uint ADS_BINARY_INDEX = 4096;
        public const uint ADS_FTS_ENCODE_UTF8 = 8192;
        public const uint ADS_FTS_ENCODE_UTF16 = 16384;
        public const uint ADS_ONLINE = 2097152;
        public const uint ADS_ALLOW_MULTIPLE_COLLATION = 268435456;
        public const uint ADS_UCHAR_KEY_SHORT = 536870912;
        public const uint ADS_UCHAR_KEY_LONG = 1073741824;
        public const uint ADS_UCHAR_KEY_XLONG = 1610612736;
        public const uint ADS_INDEX_OPTIONS_MASK = 1881178111;
        public const ushort ADS_NONE = 0;
        public const ushort ADS_LTRIM = 1;
        public const ushort ADS_RTRIM = 2;
        public const ushort ADS_TRIM = 3;
        public const ushort ADS_GET_UTF8 = 4;
        public const ushort ADS_DONT_CHECK_CONV_ERR = 8;
        public const ushort ADS_GET_FORMAT_ANSI = 16;
        public const ushort ADS_GET_FORMAT_WEB = 48;
        public const ushort ADS_GET_GUID_MIME = 256;
        public const ushort ADS_GET_GUID_FILE = 512;
        public const ushort ADS_GET_GUID_NUMBERS = 1024;
        public const ushort ADS_GET_GUID_REGISTRY = 2048;
        public const ushort ADS_COMPATIBLE_LOCKING = 0;
        public const ushort ADS_PROPRIETARY_LOCKING = 1;
        public const ushort ADS_SOFTSEEK = 1;
        public const ushort ADS_HARDSEEK = 2;
        public const ushort ADS_SEEKGT = 4;
        public const ushort ADS_RAWKEY = 1;
        public const ushort ADS_STRINGKEY = 2;
        public const ushort ADS_DOUBLEKEY = 4;
        public const ushort ADS_WSTRINGKEY = 8;
        public const uint ADS_GET_DEFAULT_KEY_LENGTH = 0;
        public const uint ADS_GET_PARTIAL_FULL_KEY_LENGTH = 1;
        public const uint ADS_GET_FULL_KEY_LENGTH = 2;
        public const uint ADS_GET_PRIMARY_WEIGHT_LENGTH = 4;
        public const ushort ADS_TOP = 1;
        public const ushort ADS_BOTTOM = 2;
        public const ushort ADS_RESPECTFILTERS = 1;
        public const ushort ADS_IGNOREFILTERS = 2;
        public const ushort ADS_RESPECTSCOPES = 3;
        public const ushort ADS_REFRESHCOUNT = 4;
        public const ushort ADS_LOCAL_SERVER = 1;
        public const ushort ADS_REMOTE_SERVER = 2;
        public const ushort ADS_AIS_SERVER = 4;
        public const ushort ADS_CONNECTION = 1;
        public const ushort ADS_TABLE = 2;
        public const ushort ADS_INDEX_ORDER = 3;
        public const ushort ADS_STATEMENT = 4;
        public const ushort ADS_CURSOR = 5;
        public const ushort ADS_DATABASE_CONNECTION = 6;
        public const ushort ADS_FTS_INDEX_ORDER = 8;
        public const ushort ADS_CURSOR_READONLY = 1;
        public const ushort ADS_CURSOR_READWRITE = 2;
        public const ushort ADS_CONSTRAIN = 1;
        public const ushort ADS_NO_CONSTRAIN = 2;
        public const ushort ADS_READ_ALL_COLUMNS = 1;
        public const ushort ADS_READ_SELECT_COLUMNS = 2;
        public const ushort ADS_NO_VALIDATE = 0;
        public const ushort ADS_VALIDATE_NO_SAVE = 1;
        public const ushort ADS_VALIDATE_WRITE_FAIL = 2;
        public const ushort ADS_VALIDATE_APPEND_FAIL = 3;
        public const ushort ADS_VALIDATE_RETURN_ERROR = 4;
        public const int ADS_CMP_LESS = -1;
        public const int ADS_CMP_EQUAL = 0;
        public const int ADS_CMP_GREATER = 1;
        public const ushort ADS_CONNECTIONPROP_USERNAME = 0;
        public const ushort ADS_CONNECTIONPROP_PASSWORD = 1;
        public const ushort ADS_CONNECTIONPROP_PROTOCOL = 2;
        public const ushort ADS_CONNECTIONPROP_ENCRYPTION_TYPE = 3;
        public const ushort ADS_CONNECTIONPROP_FIPS_MODE = 4;
        public const ushort ADS_CONNECTIONPROP_CERTIFICATE_FILE = 5;
        public const ushort ADS_CONNECTIONPROP_CIPHER_SUITE = 6;
        public const ushort ADS_CONNECTIONPROP_COMMON_NAME = 7;
        public const ushort ADS_CONNECTIONPROP_USING_TCP_IP = 1;
        public const ushort ADS_CONNECTIONPROP_USING_TLC = 5;
        public const ushort ADS_CRC_LOCALLY = 1;
        public const ushort ADS_CRC_IGNOREMEMOPAGES = 2;
        public const ushort ADS_EVENT_ASYNC = 1;
        public const ushort ADS_EVENT_WITH_DATA = 2;
        public const ushort ADS_PRESERVE_ERR = 1;
        public const ushort ADS_CODE_PAGE = 1;
        public const ushort AE_SUCCESS = 0;
        public const ushort AE_ALLOCATION_FAILED = 5001;
        public const ushort AE_COMM_MISMATCH = 5002;
        public const ushort AE_DATA_TOO_LONG = 5003;
        public const ushort AE_FILE_NOT_FOUND = 5004;
        public const ushort AE_INSUFFICIENT_BUFFER = 5005;
        public const ushort AE_INVALID_BOOKMARK = 5006;
        public const ushort AE_INVALID_CALLBACK = 5007;
        public const ushort AE_INVALID_CENTURY = 5008;
        public const ushort AE_INVALID_DATEFORMAT = 5009;
        public const ushort AE_INVALID_DECIMALS = 5010;
        public const ushort AE_INVALID_EXPRESSION = 5011;
        public const ushort AE_INVALID_FIELDDEF = 5012;
        public const ushort AE_INVALID_FILTER_OPTION = 5013;
        public const ushort AE_INVALID_INDEX_HANDLE = 5014;
        public const ushort AE_INVALID_INDEX_NAME = 5015;
        public const ushort AE_INVALID_INDEX_ORDER_NAME = 5016;
        public const ushort AE_INVALID_INDEX_TYPE = 5017;
        public const ushort AE_INVALID_HANDLE = 5018;
        public const ushort AE_INVALID_OPTION = 5019;
        public const ushort AE_INVALID_PATH = 5020;
        public const ushort AE_INVALID_POINTER = 5021;
        public const ushort AE_INVALID_RECORD_NUMBER = 5022;
        public const ushort AE_INVALID_TABLE_HANDLE = 5023;
        public const ushort AE_INVALID_CONNECTION_HANDLE = 5024;
        public const ushort AE_INVALID_TABLETYPE = 5025;
        public const ushort AE_INVALID_WORKAREA = 5026;
        public const ushort AE_INVALID_CHARSETTYPE = 5027;
        public const ushort AE_INVALID_LOCKTYPE = 5028;
        public const ushort AE_INVALID_RIGHTSOPTION = 5029;
        public const ushort AE_INVALID_FIELDNUMBER = 5030;
        public const ushort AE_INVALID_KEY_LENGTH = 5031;
        public const ushort AE_INVALID_FIELDNAME = 5032;
        public const ushort AE_NO_DRIVE_CONNECTION = 5033;
        public const ushort AE_FILE_NOT_ON_SERVER = 5034;
        public const ushort AE_LOCK_FAILED = 5035;
        public const ushort AE_NO_CONNECTION = 5036;
        public const ushort AE_NO_FILTER = 5037;
        public const ushort AE_NO_SCOPE = 5038;
        public const ushort AE_NO_TABLE = 5039;
        public const ushort AE_NO_WORKAREA = 5040;
        public const ushort AE_NOT_FOUND = 5041;
        public const ushort AE_NOT_IMPLEMENTED = 5042;
        public const ushort AE_MAX_THREADS_EXCEEDED = 5043;
        public const ushort AE_START_THREAD_FAIL = 5044;
        public const ushort AE_TOO_MANY_INDEXES = 5045;
        public const ushort AE_TOO_MANY_TAGS = 5046;
        public const ushort AE_TRANS_OUT_OF_SEQUENCE = 5047;
        public const ushort AE_UNKNOWN_ERRCODE = 5048;
        public const ushort AE_UNSUPPORTED_COLLATION = 5049;
        public const ushort AE_NAME_TOO_LONG = 5050;
        public const ushort AE_DUPLICATE_ALIAS = 5051;
        public const ushort AE_TABLE_CLOSED_IN_TRANSACTION = 5053;
        public const ushort AE_PERMISSION_DENIED = 5054;
        public const ushort AE_STRING_NOT_FOUND = 5055;
        public const ushort AE_UNKNOWN_CHAR_SET = 5056;
        public const ushort AE_INVALID_OEM_CHAR_FILE = 5057;
        public const ushort AE_INVALID_MEMO_BLOCK_SIZE = 5058;
        public const ushort AE_NO_FILE_FOUND = 5059;
        public const ushort AE_NO_INF_LOCK = 5060;
        public const ushort AE_INF_FILE_ERROR = 5061;
        public const ushort AE_RECORD_NOT_LOCKED = 5062;
        public const ushort AE_ILLEGAL_COMMAND_DURING_TRANS = 5063;
        public const ushort AE_TABLE_NOT_SHARED = 5064;
        public const ushort AE_INDEX_ALREADY_OPEN = 5065;
        public const ushort AE_INVALID_FIELD_TYPE = 5066;
        public const ushort AE_TABLE_NOT_EXCLUSIVE = 5067;
        public const ushort AE_NO_CURRENT_RECORD = 5068;
        public const ushort AE_PRECISION_LOST = 5069;
        public const ushort AE_INVALID_DATA_TYPE = 5070;
        public const ushort AE_DATA_TRUNCATED = 5071;
        public const ushort AE_TABLE_READONLY = 5072;
        public const ushort AE_INVALID_RECORD_LENGTH = 5073;
        public const ushort AE_NO_ERROR_MESSAGE = 5074;
        public const ushort AE_INDEX_SHARED = 5075;
        public const ushort AE_INDEX_EXISTS = 5076;
        public const ushort AE_CYCLIC_RELATION = 5077;
        public const ushort AE_INVALID_RELATION = 5078;
        public const ushort AE_INVALID_DAY = 5079;
        public const ushort AE_INVALID_MONTH = 5080;
        public const ushort AE_CORRUPT_TABLE = 5081;
        public const ushort AE_INVALID_BINARY_OFFSET = 5082;
        public const ushort AE_BINARY_FILE_ERROR = 5083;
        public const ushort AE_INVALID_DELETED_BYTE_VALUE = 5084;
        public const ushort AE_NO_PENDING_UPDATE = 5085;
        public const ushort AE_PENDING_UPDATE = 5086;
        public const ushort AE_TABLE_NOT_LOCKED = 5087;
        public const ushort AE_CORRUPT_INDEX = 5088;
        public const ushort AE_AUTOOPEN_INDEX = 5089;
        public const ushort AE_SAME_TABLE = 5090;
        public const ushort AE_INVALID_IMAGE = 5091;
        public const ushort AE_COLLATION_SEQUENCE_MISMATCH = 5092;
        public const ushort AE_INVALID_INDEX_ORDER = 5093;
        public const ushort AE_TABLE_CACHED = 5094;
        public const ushort AE_INVALID_DATE = 5095;
        public const ushort AE_ENCRYPTION_NOT_ENABLED = 5096;
        public const ushort AE_INVALID_PASSWORD = 5097;
        public const ushort AE_TABLE_ENCRYPTED = 5098;
        public const ushort AE_SERVER_MISMATCH = 5099;
        public const ushort AE_INVALID_USERNAME = 5100;
        public const ushort AE_INVALID_VALUE = 5101;
        public const ushort AE_INVALID_CONTINUE = 5102;
        public const ushort AE_UNRECOGNIZED_VERSION = 5103;
        public const ushort AE_RECORD_ENCRYPTED = 5104;
        public const ushort AE_UNRECOGNIZED_ENCRYPTION = 5105;
        public const ushort AE_INVALID_SQLSTATEMENT_HANDLE = 5106;
        public const ushort AE_INVALID_SQLCURSOR_HANDLE = 5107;
        public const ushort AE_NOT_PREPARED = 5108;
        public const ushort AE_CURSOR_NOT_CLOSED = 5109;
        public const ushort AE_INVALID_SQL_PARAM_NUMBER = 5110;
        public const ushort AE_INVALID_SQL_PARAM_NAME = 5111;
        public const ushort AE_INVALID_COLUMN_NUMBER = 5112;
        public const ushort AE_INVALID_COLUMN_NAME = 5113;
        public const ushort AE_INVALID_READONLY_OPTION = 5114;
        public const ushort AE_IS_CURSOR_HANDLE = 5115;
        public const ushort AE_INDEX_EXPR_NOT_FOUND = 5116;
        public const ushort AE_NOT_DML = 5117;
        public const ushort AE_INVALID_CONSTRAIN_TYPE = 5118;
        public const ushort AE_INVALID_CURSORHANDLE = 5119;
        public const ushort AE_OBSOLETE_FUNCTION = 5120;
        public const ushort AE_TADSDATASET_GENERAL = 5121;
        public const ushort AE_UDF_OVERWROTE_BUFFER = 5122;
        public const ushort AE_INDEX_UDF_NOT_SET = 5123;
        public const ushort AE_CONCURRENT_PROBLEM = 5124;
        public const ushort AE_INVALID_DICTIONARY_HANDLE = 5125;
        public const ushort AE_INVALID_PROPERTY_ID = 5126;
        public const ushort AE_INVALID_PROPERTY = 5127;
        public const ushort AE_DICTIONARY_ALREADY_EXISTS = 5128;
        public const ushort AE_INVALID_FIND_HANDLE = 5129;
        public const ushort AE_DD_REQUEST_NOT_COMPLETED = 5130;
        public const ushort AE_INVALID_OBJECT_ID = 5131;
        public const ushort AE_INVALID_OBJECT_NAME = 5132;
        public const ushort AE_INVALID_PROPERTY_LENGTH = 5133;
        public const ushort AE_INVALID_KEY_OPTIONS = 5134;
        public const ushort AE_CONSTRAINT_VALIDATION_ERROR = 5135;
        public const ushort AE_INVALID_OBJECT_TYPE = 5136;
        public const ushort AE_NO_OBJECT_FOUND = 5137;
        public const ushort AE_PROPERTY_NOT_SET = 5138;
        public const ushort AE_NO_PRIMARY_KEY_EXISTS = 5139;
        public const ushort AE_LOCAL_CONN_DISABLED = 5140;
        public const ushort AE_RI_RESTRICT = 5141;
        public const ushort AE_RI_CASCADE = 5142;
        public const ushort AE_RI_FAILED = 5143;
        public const ushort AE_RI_CORRUPTED = 5144;
        public const ushort AE_RI_UNDO_FAILED = 5145;
        public const ushort AE_RI_RULE_EXISTS = 5146;
        public const ushort AE_COLUMN_CANNOT_BE_NULL = 5147;
        public const ushort AE_MIN_CONSTRAINT_VIOLATION = 5148;
        public const ushort AE_MAX_CONSTRAINT_VIOLATION = 5149;
        public const ushort AE_RECORD_CONSTRAINT_VIOLATION = 5150;
        public const ushort AE_CANNOT_DELETE_TEMP_INDEX = 5151;
        public const ushort AE_RESTRUCTURE_FAILED = 5152;
        public const ushort AE_INVALID_STATEMENT = 5153;
        public const ushort AE_STORED_PROCEDURE_FAILED = 5154;
        public const ushort AE_INVALID_DICTIONARY_FILE = 5155;
        public const ushort AE_NOT_MEMBER_OF_GROUP = 5156;
        public const ushort AE_ALREADY_MEMBER_OF_GROUP = 5157;
        public const ushort AE_INVALID_OBJECT_RIGHT = 5158;
        public const ushort AE_INVALID_OBJECT_PERMISSION = 5158;
        public const ushort AE_CANNOT_OPEN_DATABASE_TABLE = 5159;
        public const ushort AE_INVALID_CONSTRAINT = 5160;
        public const ushort AE_NOT_ADMINISTRATOR = 5161;
        public const ushort AE_NO_TABLE_ENCRYPTION_PASSWORD = 5162;
        public const ushort AE_TABLE_NOT_ENCRYPTED = 5163;
        public const ushort AE_INVALID_ENCRYPTION_VERSION = 5164;
        public const ushort AE_NO_STORED_PROC_EXEC_RIGHTS = 5165;
        public const ushort AE_DD_UNSUPPORTED_DEPLOYMENT = 5166;
        public const ushort AE_INFO_AUTO_CREATION_OCCURRED = 5168;
        public const ushort AE_INFO_COPY_MADE_BY_CLIENT = 5169;
        public const ushort AE_DATABASE_REQUIRES_NEW_SERVER = 5170;
        public const ushort AE_COLUMN_PERMISSION_DENIED = 5171;
        public const ushort AE_DATABASE_REQUIRES_NEW_CLIENT = 5172;
        public const ushort AE_INVALID_LINK_NUMBER = 5173;
        public const ushort AE_LINK_ACTIVATION_FAILED = 5174;
        public const ushort AE_INDEX_COLLATION_MISMATCH = 5175;
        public const ushort AE_ILLEGAL_USER_OPERATION = 5176;
        public const ushort AE_TRIGGER_FAILED = 5177;
        public const ushort AE_NO_ASA_FUNCTION_FOUND = 5178;
        public const ushort AE_VALUE_OVERFLOW = 5179;
        public const ushort AE_UNRECOGNIZED_FTS_VERSION = 5180;
        public const ushort AE_TRIG_CREATION_FAILED = 5181;
        public const ushort AE_MEMTABLE_SIZE_EXCEEDED = 5182;
        public const ushort AE_OUTDATED_CLIENT_VERSION = 5183;
        public const ushort AE_FREE_TABLE = 5184;
        public const ushort AE_LOCAL_CONN_RESTRICTED = 5185;
        public const ushort AE_OLD_RECORD = 5186;
        public const ushort AE_QUERY_NOT_ACTIVE = 5187;
        public const ushort AE_KEY_EXCEEDS_PAGE_SIZE = 5188;
        public const ushort AE_TABLE_FOUND = 5189;
        public const ushort AE_TABLE_NOT_FOUND = 5190;
        public const ushort AE_LOCK_OBJECT = 5191;
        public const ushort AE_INVALID_REPLICATION_IDENT = 5192;
        public const ushort AE_ILLEGAL_COMMAND_DURING_BACKUP = 5193;
        public const ushort AE_NO_MEMO_FILE = 5194;
        public const ushort AE_SUBSCRIPTION_QUEUE_NOT_EMPTY = 5195;
        public const ushort AE_UNABLE_TO_DISABLE_TRIGGERS = 5196;
        public const ushort AE_UNABLE_TO_ENABLE_TRIGGERS = 5197;
        public const ushort AE_BACKUP = 5198;
        public const ushort AE_FREETABLEFAILED = 5199;
        public const ushort AE_BLURRY_SNAPSHOT = 5200;
        public const ushort AE_INVALID_VERTICAL_FILTER = 5201;
        public const ushort AE_INVALID_USE_OF_HANDLE_IN_AEP = 5202;
        public const ushort AE_COLLATION_NOT_RECOGNIZED = 5203;
        public const ushort AE_INVALID_COLLATION = 5204;
        public const ushort AE_NOT_VFP_NULLABLE_FIELD = 5205;
        public const ushort AE_NOT_VFP_VARIABLE_FIELD = 5206;
        public const ushort AE_ILLEGAL_EVENT_COMMAND = 5207;
        public const ushort AE_KEY_CANNOT_BE_NULL = 5208;
        public const ushort AE_COLLATIONS_DO_NOT_MATCH = 5209;
        public const ushort AE_INVALID_APPID = 5210;
        public const ushort AE_UNICODE_CONVERSION = 5211;
        public const ushort AE_UNICODE_COLLATION = 5212;
        public const ushort AE_SERVER_ENUMERATION_ERROR = 5213;
        public const ushort AE_UNABLE_TO_LOAD_SSL = 5214;
        public const ushort AE_UNABLE_TO_VERIFY_SIGNATURE = 5215;
        public const ushort AE_UNABLE_TO_LOAD_SSL_ENTRYPOINT = 5216;
        public const ushort AE_CRYPTO_ERROR = 5217;
        public const ushort AE_UNRECOGNIZED_CIPHER = 5218;
        public const ushort AE_FIPS_MODE_ENCRYPTION = 5219;
        public const ushort AE_FIPS_REQUIRED = 5220;
        public const ushort AE_FIPS_NOT_ALLOWED = 5221;
        public const ushort AE_FIPS_MODE_FAILED = 5222;
        public const ushort AE_PASSWORD_REQUIRED = 5223;
        public const ushort AE_CONNECTION_TIMED_OUT = 5224;
        public const ushort AE_DELTA_SUPPORT_NOT_POSSIBLE = 5225;
        public const ushort AE_QUERY_LOGGING_ERROR = 5226;
        public const ushort AE_COMPRESSION_FAILED = 5227;
        public const ushort AE_INVALID_DATA = 5228;
        public const ushort AE_ROWVERSION_REQUIRED = 5229;
        public const ushort ADS_DATABASE_TABLE = 0;
        public const ushort ADS_NTX = 1;
        public const ushort ADS_CDX = 2;
        public const ushort ADS_ADT = 3;
        public const ushort ADS_VFP = 4;
        public const ushort ADS_BASENAME = 1;
        public const ushort ADS_BASENAMEANDEXT = 2;
        public const ushort ADS_FULLPATHNAME = 3;
        public const ushort ADS_DATADICTIONARY_NAME = 4;
        public const ushort ADS_TABLE_OPEN_NAME = 5;
        public const ushort ADS_OPTIMIZED_FULL = 1;
        public const ushort ADS_OPTIMIZED_PART = 2;
        public const ushort ADS_OPTIMIZED_NONE = 3;
        public const uint ADS_DYNAMIC_AOF = 0;
        public const uint ADS_RESOLVE_IMMEDIATE = 1;
        public const uint ADS_RESOLVE_DYNAMIC = 2;
        public const uint ADS_KEYSET_AOF = 4;
        public const uint ADS_FIXED_AOF = 8;
        public const uint ADS_KEEP_AOF_PLAN = 16;
        public const uint ADS_ENCODE_UTF16 = 8192;
        public const uint ADS_ENCODE_UTF8 = 16384;
        public const ushort ADS_AOF_ADD_RECORD = 1;
        public const ushort ADS_AOF_REMOVE_RECORD = 2;
        public const ushort ADS_AOF_TOGGLE_RECORD = 3;
        public const uint ADS_STORED_PROC = 1;
        public const uint ADS_COMSTORED_PROC = 2;
        public const uint ADS_SCRIPT_PROC = 4;
        public const uint ADS_PROC_VARYING_OUTPUT = 4096;
        public const ushort ADS_ENCRYPTION_RC4 = 3;
        public const ushort ADS_ENCRYPTION_AES128 = 5;
        public const ushort ADS_ENCRYPTION_AES256 = 6;
        public const ushort ADS_MAX_DATEMASK = 12;
        public const ushort ADS_MAX_ERROR_LEN = 600;
        public const ushort ADS_MAX_INDEX_EXPR_LEN = 510;
        public const ushort ADS_MAX_KEY_LENGTH = 4082;
        public const ushort ADS_MAX_FIELD_NAME = 128;
        public const ushort ADS_MAX_DBF_FIELD_NAME = 10;
        public const ushort ADS_MAX_INDEXES = 15;
        public const ushort ADS_MAX_PATH = 260;
        public const ushort ADS_MAX_TABLE_NAME = 255;
        public const ushort ADS_MAX_TAG_NAME = 128;
        public const ushort ADS_MAX_TAGS = 256;
        public const ushort ADS_MAX_OBJECT_NAME = 200;
        public const ushort ADS_MAX_TABLE_AND_PATH = 515;
        public const ushort ADS_MIN_ADI_PAGESIZE = 512;
        public const ushort ADS_MAX_ADI_PAGESIZE = 8192;
        public const ushort ADS_TYPE_UNKNOWN = 0;
        public const ushort ADS_LOGICAL = 1;
        public const ushort ADS_NUMERIC = 2;
        public const ushort ADS_DATE = 3;
        public const ushort ADS_STRING = 4;
        public const ushort ADS_MEMO = 5;
        public const ushort ADS_BINARY = 6;
        public const ushort ADS_IMAGE = 7;
        public const ushort ADS_VARCHAR = 8;
        public const ushort ADS_COMPACTDATE = 9;
        public const ushort ADS_DOUBLE = 10;
        public const ushort ADS_INTEGER = 11;
        public const ushort ADS_SHORTINT = 12;
        public const ushort ADS_TIME = 13;
        public const ushort ADS_TIMESTAMP = 14;
        public const ushort ADS_AUTOINC = 15;
        public const ushort ADS_RAW = 16;
        public const ushort ADS_CURDOUBLE = 17;
        public const ushort ADS_MONEY = 18;
        public const ushort ADS_LONGINT = 19;
        public const ushort ADS_LONGLONG = 19;
        public const ushort ADS_CISTRING = 20;
        public const ushort ADS_ROWVERSION = 21;
        public const ushort ADS_MODTIME = 22;
        public const ushort ADS_VARCHAR_FOX = 23;
        public const ushort ADS_VARBINARY_FOX = 24;
        public const ushort ADS_SYSTEM_FIELD = 25;
        public const ushort ADS_NCHAR = 26;
        public const ushort ADS_NVARCHAR = 27;
        public const ushort ADS_NMEMO = 28;
        public const ushort ADS_GUID = 29;
        public const ushort ADS_INDEX_UDF = 1;
        public const ushort ADS_MAX_CFG_PATH = 256;
        public const ushort ADS_MGMT_NETWARE_SERVER = 1;
        public const ushort ADS_MGMT_NETWARE4_OR_OLDER_SERVER = 1;
        public const ushort ADS_MGMT_NT_SERVER = 2;
        public const ushort ADS_MGMT_LOCAL_SERVER = 3;
        public const ushort ADS_MGMT_WIN9X_SERVER = 4;
        public const ushort ADS_MGMT_NETWARE5_OR_NEWER_SERVER = 5;
        public const ushort ADS_MGMT_LINUX_SERVER = 6;
        public const ushort ADS_MGMT_NT_SERVER_64_BIT = 7;
        public const ushort ADS_MGMT_LINUX_SERVER_64_BIT = 8;
        public const ushort ADS_MGMT_NO_LOCK = 1;
        public const ushort ADS_MGMT_RECORD_LOCK = 2;
        public const ushort ADS_MGMT_FILE_LOCK = 3;
        public const ushort ADS_REG_OWNER_LEN = 36;
        public const ushort ADS_REVISION_LEN = 16;
        public const ushort ADS_INST_DATE_LEN = 16;
        public const ushort ADS_OEM_CHAR_NAME_LEN = 16;
        public const ushort ADS_ANSI_CHAR_NAME_LEN = 16;
        public const ushort ADS_SERIAL_NUM_LEN = 16;
        public const ushort ADS_MGMT_MAX_PATH = 260;
        public const ushort ADS_MGMT_PROPRIETARY_LOCKING = 1;
        public const ushort ADS_MGMT_CDX_LOCKING = 2;
        public const ushort ADS_MGMT_NTX_LOCKING = 3;
        public const ushort ADS_MGMT_ADT_LOCKING = 4;
        public const ushort ADS_MGMT_COMIX_LOCKING = 5;
        public const ushort ADS_MAX_USER_NAME = 50;
        public const ushort ADS_MAX_ADDRESS_SIZE = 30;
        public const ushort ADS_MAX_MGMT_APPID_SIZE = 70;
        public const ushort ADS_DD_PROPERTY_NOT_AVAIL = 65535;
        public const ushort ADS_DD_MAX_PROPERTY_LEN = 65534;
        public const ushort ADS_DD_MAX_OBJECT_NAME_LEN = 200;
        public const ushort ADS_DD_UNKNOWN_OBJECT = 0;
        public const ushort ADS_DD_TABLE_OBJECT = 1;
        public const ushort ADS_DD_RELATION_OBJECT = 2;
        public const ushort ADS_DD_INDEX_FILE_OBJECT = 3;
        public const ushort ADS_DD_FIELD_OBJECT = 4;
        public const ushort ADS_DD_COLUMN_OBJECT = 4;
        public const ushort ADS_DD_INDEX_OBJECT = 5;
        public const ushort ADS_DD_VIEW_OBJECT = 6;
        public const ushort ADS_DD_VIEW_OR_TABLE_OBJECT = 7;
        public const ushort ADS_DD_USER_OBJECT = 8;
        public const ushort ADS_DD_USER_GROUP_OBJECT = 9;
        public const ushort ADS_DD_PROCEDURE_OBJECT = 10;
        public const ushort ADS_DD_DATABASE_OBJECT = 11;
        public const ushort ADS_DD_LINK_OBJECT = 12;
        public const ushort ADS_DD_TABLE_VIEW_OR_LINK_OBJECT = 13;
        public const ushort ADS_DD_TRIGGER_OBJECT = 14;
        public const ushort ADS_DD_PUBLICATION_OBJECT = 15;
        public const ushort ADS_DD_ARTICLE_OBJECT = 16;
        public const ushort ADS_DD_SUBSCRIPTION_OBJECT = 17;
        public const ushort ADS_DD_FUNCTION_OBJECT = 18;
        public const ushort ADS_DD_PACKAGE_OBJECT = 19;
        public const ushort ADS_DD_QUALIFIED_TRIGGER_OBJ = 20;
        public const ushort ADS_DD_PERMISSION_OBJECT = 21;
        public const ushort ADS_DD_COMMENT = 1;
        public const ushort ADS_DD_VERSION = 2;
        public const ushort ADS_DD_USER_DEFINED_PROP = 3;
        public const ushort ADS_DD_OBJECT_NAME = 4;
        public const ushort ADS_DD_TRIGGERS_DISABLED = 5;
        public const ushort ADS_DD_OBJECT_ID = 6;
        public const ushort ADS_DD_OPTIONS = 7;
        public const uint ADS_DD_QVR_OPT_QUERY = 1;
        public const uint ADS_DD_QVR_OPT_PROCEDURE = 2;
        public const ushort ADS_DD_DEFAULT_TABLE_PATH = 100;
        public const ushort ADS_DD_ADMIN_PASSWORD = 101;
        public const ushort ADS_DD_TEMP_TABLE_PATH = 102;
        public const ushort ADS_DD_LOG_IN_REQUIRED = 103;
        public const ushort ADS_DD_VERIFY_ACCESS_RIGHTS = 104;
        public const ushort ADS_DD_ENCRYPT_TABLE_PASSWORD = 105;
        public const ushort ADS_DD_ENCRYPT_NEW_TABLE = 106;
        public const ushort ADS_DD_ENABLE_INTERNET = 107;
        public const ushort ADS_DD_INTERNET_SECURITY_LEVEL = 108;
        public const ushort ADS_DD_MAX_FAILED_ATTEMPTS = 109;
        public const ushort ADS_DD_ALLOW_ADSSYS_NET_ACCESS = 110;
        public const ushort ADS_DD_VERSION_MAJOR = 111;
        public const ushort ADS_DD_VERSION_MINOR = 112;
        public const ushort ADS_DD_LOGINS_DISABLED = 113;
        public const ushort ADS_DD_LOGINS_DISABLED_ERRSTR = 114;
        public const ushort ADS_DD_FTS_DELIMITERS = 115;
        public const ushort ADS_DD_FTS_NOISE = 116;
        public const ushort ADS_DD_FTS_DROP_CHARS = 117;
        public const ushort ADS_DD_FTS_CONDITIONAL_CHARS = 118;
        public const ushort ADS_DD_ENCRYPTED = 119;
        public const ushort ADS_DD_ENCRYPT_INDEXES = 120;
        public const ushort ADS_DD_QUERY_LOG_TABLE = 121;
        public const ushort ADS_DD_ENCRYPT_COMMUNICATION = 122;
        public const ushort ADS_DD_DEFAULT_TABLE_RELATIVE_PATH = 123;
        public const ushort ADS_DD_TEMP_TABLE_RELATIVE_PATH = 124;
        public const ushort ADS_DD_DISABLE_DLL_CACHING = 125;
        public const ushort ADS_DD_DATA_ENCRYPTION_TYPE = 126;
        public const ushort ADS_DD_FTS_DELIMITERS_W = 127;
        public const ushort ADS_DD_FTS_NOISE_W = 128;
        public const ushort ADS_DD_FTS_DROP_CHARS_W = 129;
        public const ushort ADS_DD_FTS_CONDITIONAL_CHARS_W = 130;
        public const ushort ADS_DD_QUERY_VIA_ROOT = 131;
        public const ushort ADS_DD_ENFORCE_MAX_FAILED_LOGINS = 132;
        public const ushort ADS_DD_TABLE_VALIDATION_EXPR = 200;
        public const ushort ADS_DD_TABLE_VALIDATION_MSG = 201;
        public const ushort ADS_DD_TABLE_PRIMARY_KEY = 202;
        public const ushort ADS_DD_TABLE_AUTO_CREATE = 203;
        public const ushort ADS_DD_TABLE_TYPE = 204;
        public const ushort ADS_DD_TABLE_PATH = 205;
        public const ushort ADS_DD_TABLE_FIELD_COUNT = 206;
        public const ushort ADS_DD_TABLE_RI_GRAPH = 207;
        public const ushort ADS_DD_TABLE_OBJ_ID = 208;
        public const ushort ADS_DD_TABLE_RI_XY = 209;
        public const ushort ADS_DD_TABLE_IS_RI_PARENT = 210;
        public const ushort ADS_DD_TABLE_RELATIVE_PATH = 211;
        public const ushort ADS_DD_TABLE_CHAR_TYPE = 212;
        public const ushort ADS_DD_TABLE_DEFAULT_INDEX = 213;
        public const ushort ADS_DD_TABLE_ENCRYPTION = 214;
        public const ushort ADS_DD_TABLE_MEMO_BLOCK_SIZE = 215;
        public const ushort ADS_DD_TABLE_PERMISSION_LEVEL = 216;
        public const ushort ADS_DD_TABLE_TRIGGER_TYPES = 217;
        public const ushort ADS_DD_TABLE_TRIGGER_OPTIONS = 218;
        public const ushort ADS_DD_TABLE_CACHING = 219;
        public const ushort ADS_DD_TABLE_TXN_FREE = 220;
        public const ushort ADS_DD_TABLE_VALIDATION_EXPR_W = 221;
        public const ushort ADS_DD_TABLE_WEB_DELTA = 222;
        public const ushort ADS_DD_TABLE_CONCURRENCY_ENABLED = 223;
        public const uint ADS_DD_FIELD_OPT_VFP_BINARY = 1;
        public const uint ADS_DD_FIELD_OPT_VFP_NULLABLE = 2;
        public const uint ADS_DD_FIELD_OPT_COMPRESSED = 65536;
        public const ushort ADS_DD_FIELD_DEFAULT_VALUE = 300;
        public const ushort ADS_DD_FIELD_CAN_NULL = 301;
        public const ushort ADS_DD_FIELD_MIN_VALUE = 302;
        public const ushort ADS_DD_FIELD_MAX_VALUE = 303;
        public const ushort ADS_DD_FIELD_VALIDATION_MSG = 304;
        public const ushort ADS_DD_FIELD_DEFINITION = 305;
        public const ushort ADS_DD_FIELD_TYPE = 306;
        public const ushort ADS_DD_FIELD_LENGTH = 307;
        public const ushort ADS_DD_FIELD_DECIMAL = 308;
        public const ushort ADS_DD_FIELD_NUM = 309;
        public const ushort ADS_DD_FIELD_OPTIONS = 310;
        public const ushort ADS_DD_FIELD_DEFAULT_VALUE_W = 311;
        public const ushort ADS_DD_FIELD_MIN_VALUE_W = 312;
        public const ushort ADS_DD_FIELD_MAX_VALUE_W = 313;
        public const ushort ADS_DD_INDEX_FILE_NAME = 400;
        public const ushort ADS_DD_INDEX_EXPRESSION = 401;
        public const ushort ADS_DD_INDEX_CONDITION = 402;
        public const ushort ADS_DD_INDEX_OPTIONS = 403;
        public const ushort ADS_DD_INDEX_KEY_LENGTH = 404;
        public const ushort ADS_DD_INDEX_KEY_TYPE = 405;
        public const ushort ADS_DD_INDEX_FTS_MIN_LENGTH = 406;
        public const ushort ADS_DD_INDEX_FTS_DELIMITERS = 407;
        public const ushort ADS_DD_INDEX_FTS_NOISE = 408;
        public const ushort ADS_DD_INDEX_FTS_DROP_CHARS = 409;
        public const ushort ADS_DD_INDEX_FTS_CONDITIONAL_CHARS = 410;
        public const ushort ADS_DD_INDEX_COLLATION = 411;
        public const ushort ADS_DD_INDEX_FTS_DELIMITERS_W = 412;
        public const ushort ADS_DD_INDEX_FTS_NOISE_W = 413;
        public const ushort ADS_DD_INDEX_FTS_DROP_CHARS_W = 414;
        public const ushort ADS_DD_INDEX_FTS_CONDITIONAL_CHARS_W = 415;
        public const ushort ADS_DD_RI_PARENT_GRAPH = 500;
        public const ushort ADS_DD_RI_PRIMARY_TABLE = 501;
        public const ushort ADS_DD_RI_PRIMARY_INDEX = 502;
        public const ushort ADS_DD_RI_FOREIGN_TABLE = 503;
        public const ushort ADS_DD_RI_FOREIGN_INDEX = 504;
        public const ushort ADS_DD_RI_UPDATERULE = 505;
        public const ushort ADS_DD_RI_DELETERULE = 506;
        public const ushort ADS_DD_RI_NO_PKEY_ERROR = 507;
        public const ushort ADS_DD_RI_CASCADE_ERROR = 508;
        public const ushort ADS_DD_USER_GROUP_NAME = 600;
        public const ushort ADS_DD_VIEW_STMT = 700;
        public const ushort ADS_DD_VIEW_STMT_LEN = 701;
        public const ushort ADS_DD_VIEW_TRIGGER_TYPES = 702;
        public const ushort ADS_DD_VIEW_TRIGGER_OPTIONS = 703;
        public const ushort ADS_DD_VIEW_STMT_W = 704;
        public const ushort ADS_DD_PROC_INPUT = 800;
        public const ushort ADS_DD_PROC_OUTPUT = 801;
        public const ushort ADS_DD_PROC_DLL_NAME = 802;
        public const ushort ADS_DD_PROC_DLL_FUNCTION_NAME = 803;
        public const ushort ADS_DD_PROC_INVOKE_OPTION = 804;
        public const ushort ADS_DD_PROC_SCRIPT = 805;
        public const ushort ADS_DD_PROC_SCRIPT_W = 806;
        public const ushort ADS_DD_INDEX_FILE_PATH = 900;
        public const ushort ADS_DD_INDEX_FILE_PAGESIZE = 901;
        public const ushort ADS_DD_INDEX_FILE_RELATIVE_PATH = 902;
        public const ushort ADS_DD_INDEX_FILE_TYPE = 903;
        public const ushort ADS_DD_TABLES_RIGHTS = 1001;
        public const ushort ADS_DD_VIEWS_RIGHTS = 1002;
        public const ushort ADS_DD_PROCS_RIGHTS = 1003;
        public const ushort ADS_DD_OBJECTS_RIGHTS = 1004;
        public const ushort ADS_DD_FREE_TABLES_RIGHTS = 1005;
        public const ushort ADS_DD_USER_PASSWORD = 1101;
        public const ushort ADS_DD_USER_GROUP_MEMBERSHIP = 1102;
        public const ushort ADS_DD_USER_BAD_LOGINS = 1103;
        public const ushort ADS_DD_CURRENT_USER_PASSWORD = 1104;
        public const ushort ADS_DD_REQUIRE_OLD_PASSWORD = 1105;
        public const ushort ADS_DD_LINK_PATH = 1300;
        public const ushort ADS_DD_LINK_OPTIONS = 1301;
        public const ushort ADS_DD_LINK_USERNAME = 1302;
        public const ushort ADS_DD_LINK_RELATIVE_PATH = 1303;
        public const ushort ADS_DD_TRIG_TABLEID = 1400;
        public const ushort ADS_DD_TRIG_EVENT_TYPE = 1401;
        public const ushort ADS_DD_TRIG_TRIGGER_TYPE = 1402;
        public const ushort ADS_DD_TRIG_CONTAINER_TYPE = 1403;
        public const ushort ADS_DD_TRIG_CONTAINER = 1404;
        public const ushort ADS_DD_TRIG_FUNCTION_NAME = 1405;
        public const ushort ADS_DD_TRIG_PRIORITY = 1406;
        public const ushort ADS_DD_TRIG_OPTIONS = 1407;
        public const ushort ADS_DD_TRIG_TABLENAME = 1408;
        public const ushort ADS_DD_TRIG_CONTAINER_W = 1409;
        public const ushort ADS_DD_PUBLICATION_OPTIONS = 1500;
        public const ushort ADS_DD_ARTICLE_FILTER = 1600;
        public const ushort ADS_DD_ARTICLE_ID_COLUMNS = 1601;
        public const ushort ADS_DD_ARTICLE_ID_COLUMN_NUMBERS = 1602;
        public const ushort ADS_DD_ARTICLE_FILTER_SHORT = 1603;
        public const ushort ADS_DD_ARTICLE_INCLUDE_COLUMNS = 1604;
        public const ushort ADS_DD_ARTICLE_EXCLUDE_COLUMNS = 1605;
        public const ushort ADS_DD_ARTICLE_INC_COLUMN_NUMBERS = 1606;
        public const ushort ADS_DD_ARTICLE_INSERT_MERGE = 1607;
        public const ushort ADS_DD_ARTICLE_UPDATE_MERGE = 1608;
        public const ushort ADS_DD_ARTICLE_FILTER_W = 1609;
        public const ushort ADS_DD_SUBSCR_PUBLICATION_NAME = 1700;
        public const ushort ADS_DD_SUBSCR_TARGET = 1701;
        public const ushort ADS_DD_SUBSCR_USERNAME = 1702;
        public const ushort ADS_DD_SUBSCR_PASSWORD = 1703;
        public const ushort ADS_DD_SUBSCR_FORWARD = 1704;
        public const ushort ADS_DD_SUBSCR_ENABLED = 1705;
        public const ushort ADS_DD_SUBSCR_QUEUE_NAME = 1706;
        public const ushort ADS_DD_SUBSCR_OPTIONS = 1707;
        public const ushort ADS_DD_SUBSCR_QUEUE_NAME_RELATIVE = 1708;
        public const ushort ADS_DD_SUBSCR_PAUSED = 1709;
        public const ushort ADS_DD_SUBSCR_COMM_TCP_IP = 1710;
        public const ushort ADS_DD_SUBSCR_COMM_TCP_IP_V6 = 1711;
        public const ushort ADS_DD_SUBSCR_COMM_UDP_IP = 1712;
        public const ushort ADS_DD_SUBSCR_COMM_IPX = 1713;
        public const ushort ADS_DD_SUBSCR_OPTIONS_INTERNAL = 1714;
        public const ushort ADS_DD_SUBSCR_COMM_TLS = 1715;
        public const ushort ADS_DD_SUBSCR_CONNECTION_STR = 1716;
        public const ushort ADS_PROPERTY_UNSPECIFIED = 0;
        public const ushort ADS_DONT_KILL_APPID = 1;
        public const ushort ADS_RESTRICT_KILL = 2;
        public const ushort ADS_DD_LEVEL_0 = 0;
        public const ushort ADS_DD_LEVEL_1 = 1;
        public const ushort ADS_DD_LEVEL_2 = 2;
        public const ushort ADS_DD_RI_CASCADE = 1;
        public const ushort ADS_DD_RI_RESTRICT = 2;
        public const ushort ADS_DD_RI_SETNULL = 3;
        public const ushort ADS_DD_RI_SETDEFAULT = 4;
        public const ushort ADS_DD_DFV_UNKNOWN = 1;
        public const ushort ADS_DD_DFV_NONE = 2;
        public const ushort ADS_DD_DFV_VALUES_STORED = 3;
        public const uint ADS_PERMISSION_NONE = 0;
        public const uint ADS_PERMISSION_READ = 1;
        public const uint ADS_PERMISSION_UPDATE = 2;
        public const uint ADS_PERMISSION_EXECUTE = 4;
        public const uint ADS_PERMISSION_INHERIT = 8;
        public const uint ADS_PERMISSION_INSERT = 16;
        public const uint ADS_PERMISSION_DELETE = 32;
        public const uint ADS_PERMISSION_LINK_ACCESS = 64;
        public const uint ADS_PERMISSION_CREATE = 128;
        public const uint ADS_PERMISSION_ALTER = 256;
        public const uint ADS_PERMISSION_DROP = 512;
        public const uint ADS_PERMISSION_WITH_GRANT = 2147483648;
        public const uint ADS_PERMISSION_ALL_WITH_GRANT = 2415919103;
        public const uint ADS_PERMISSION_ALL = 4294967295;
        public const uint ADS_GET_PERMISSIONS_WITH_GRANT = 2147549183;
        public const uint ADS_GET_PERMISSIONS_CREATE = 4294901888;
        public const uint ADS_GET_PERMISSIONS_CREATE_WITH_GRANT = 2415918991;
        public const uint ADS_LINK_GLOBAL = 1;
        public const uint ADS_LINK_AUTH_ACTIVE_USER = 2;
        public const uint ADS_LINK_PATH_IS_STATIC = 4;
        public const ushort ADS_TRIGEVENT_INSERT = 1;
        public const ushort ADS_TRIGEVENT_UPDATE = 2;
        public const ushort ADS_TRIGEVENT_DELETE = 3;
        public const uint ADS_TRIGTYPE_BEFORE = 1;
        public const uint ADS_TRIGTYPE_INSTEADOF = 2;
        public const uint ADS_TRIGTYPE_AFTER = 4;
        public const uint ADS_TRIGTYPE_CONFLICTON = 8;
        public const ushort ADS_TRIG_WIN32DLL = 1;
        public const ushort ADS_TRIG_COM = 2;
        public const ushort ADS_TRIG_SCRIPT = 3;
        public const uint ADS_TRIGOPTIONS_NO_VALUES = 0;
        public const uint ADS_TRIGOPTIONS_WANT_VALUES = 1;
        public const uint ADS_TRIGOPTIONS_WANT_MEMOS_AND_BLOBS = 2;
        public const uint ADS_TRIGOPTIONS_DEFAULT = 3;
        public const uint ADS_TRIGOPTIONS_NO_TRANSACTION = 4;
        public const ushort ADS_DD_TABLE_PERMISSION_LEVEL_1 = 1;
        public const ushort ADS_DD_TABLE_PERMISSION_LEVEL_2 = 2;
        public const ushort ADS_DD_TABLE_PERMISSION_LEVEL_3 = 3;
        public const uint ADS_KEEP_TABLE_FILE_NAME = 1;
        public const uint ADS_IDENTIFY_BY_PRIMARY = 1;
        public const uint ADS_IDENTIFY_BY_ALL = 2;
        public const uint ADS_SUBSCR_QUEUE_IS_STATIC = 1;
        public const uint ADS_SUBSCR_AIS_TARGET = 2;
        public const uint ADS_SUBSCR_IGNORE_FAILED_REP = 4;
        public const uint ADS_SUBSCR_LOG_FAILED_REP_DATA = 8;
        public const ushort ADS_CODEUNIT_LENGTH = 0;
        public const uint ADS_BYTE_LENGTH = 1;
        public const uint ADS_BYTE_LENGTH_IN_BUFFER = 2;
        public const uint ADS_FS_MULTICAST_ONLY = 1;
        public const uint ADS_FS_CONNECT_ALL = 2;
        public const ushort ADS_TABLE_CACHE_NONE = 0;
        public const ushort ADS_TABLE_CACHE_READS = 1;
        public const ushort ADS_TABLE_CACHE_WRITES = 2;
        public const string ADS_ENCRYPT_STRING_RC4 = "RC4";
        public const string ADS_ENCRYPT_STRING_AES128 = "AES128";
        public const string ADS_ENCRYPT_STRING_AES256 = "AES256";
        public const string ADS_CIPHER_SUITE_STRING_RC4 = "RC4-MD5";
        public const string ADS_CIPHER_SUITE_STRING_AES128 = "AES128-SHA";
        public const string ADS_CIPHER_SUITE_STRING_AES256 = "AES256-SHA";
        public const string ADS_ROOT_DD_ALIAS = "__rootdd";
        public const uint ADS_FILTER_FORMAT_ODATA = 1;
        public const uint ADS_FILTER_ENCODE_UTF8 = 2;

        [DllImport("ace32", EntryPoint = "AdsAddCustomKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsAddCustomKey_32(IntPtr hIndex);

        [DllImport("ace64", EntryPoint = "AdsAddCustomKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsAddCustomKey_64(IntPtr hIndex);

        public static uint AdsAddCustomKey(IntPtr hIndex)
        {
            return IntPtr.Size == 4 ? AdsAddCustomKey_32(hIndex) : AdsAddCustomKey_64(hIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsAppendRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsAppendRecord_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsAppendRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsAppendRecord_64(IntPtr hTable);

        public static uint AdsAppendRecord(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsAppendRecord_32(hTable) : AdsAppendRecord_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsApplicationExit", CharSet = CharSet.Ansi)]
        private static extern uint AdsApplicationExit_32();

        [DllImport("ace64", EntryPoint = "AdsApplicationExit", CharSet = CharSet.Ansi)]
        private static extern uint AdsApplicationExit_64();

        public static uint AdsApplicationExit()
        {
            return IntPtr.Size == 4 ? AdsApplicationExit_32() : AdsApplicationExit_64();
        }

        [DllImport("ace32", EntryPoint = "AdsAtBOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsAtBOF_32(IntPtr hTable, out ushort pbBof);

        [DllImport("ace64", EntryPoint = "AdsAtBOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsAtBOF_64(IntPtr hTable, out ushort pbBof);

        public static uint AdsAtBOF(IntPtr hTable, out ushort pbBof)
        {
            return IntPtr.Size == 4 ? AdsAtBOF_32(hTable, out pbBof) : AdsAtBOF_64(hTable, out pbBof);
        }

        [DllImport("ace32", EntryPoint = "AdsAtEOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsAtEOF_32(IntPtr hTable, out ushort pbEof);

        [DllImport("ace64", EntryPoint = "AdsAtEOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsAtEOF_64(IntPtr hTable, out ushort pbEof);

        public static uint AdsAtEOF(IntPtr hTable, out ushort pbEof)
        {
            return IntPtr.Size == 4 ? AdsAtEOF_32(hTable, out pbEof) : AdsAtEOF_64(hTable, out pbEof);
        }

        [DllImport("ace32", EntryPoint = "AdsBeginTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsBeginTransaction_32(IntPtr hConnect);

        [DllImport("ace64", EntryPoint = "AdsBeginTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsBeginTransaction_64(IntPtr hConnect);

        public static uint AdsBeginTransaction(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsBeginTransaction_32(hConnect) : AdsBeginTransaction_64(hConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsBinaryToFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsBinaryToFile_32(
        IntPtr hTable,
        string pucFldName,
        string pucFileName);

        [DllImport("ace64", EntryPoint = "AdsBinaryToFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsBinaryToFile_64(
        IntPtr hTable,
        string pucFldName,
        string pucFileName);

        public static uint AdsBinaryToFile(IntPtr hTable, string pucFldName, string pucFileName)
        {
            return IntPtr.Size == 4 ? AdsBinaryToFile_32(hTable, pucFldName, pucFileName) : AdsBinaryToFile_64(hTable, pucFldName, pucFileName);
        }

        [DllImport("ace32", EntryPoint = "AdsBinaryToFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsBinaryToFile_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        string pucFileName);

        [DllImport("ace64", EntryPoint = "AdsBinaryToFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsBinaryToFile_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        string pucFileName);

        public static uint AdsBinaryToFile(IntPtr hTable, uint lFieldOrdinal, string pucFileName)
        {
            return IntPtr.Size == 4 ? AdsBinaryToFile_32(hTable, lFieldOrdinal, pucFileName) : AdsBinaryToFile_64(hTable, lFieldOrdinal, pucFileName);
        }

        [DllImport("ace32", EntryPoint = "AdsCacheOpenCursors", CharSet = CharSet.Ansi)]
        private static extern uint AdsCacheOpenCursors_32(ushort usOpen);

        [DllImport("ace64", EntryPoint = "AdsCacheOpenCursors", CharSet = CharSet.Ansi)]
        private static extern uint AdsCacheOpenCursors_64(ushort usOpen);

        public static uint AdsCacheOpenCursors(ushort usOpen)
        {
            return IntPtr.Size == 4 ? AdsCacheOpenCursors_32(usOpen) : AdsCacheOpenCursors_64(usOpen);
        }

        [DllImport("ace32", EntryPoint = "AdsCacheOpenTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsCacheOpenTables_32(ushort usOpen);

        [DllImport("ace64", EntryPoint = "AdsCacheOpenTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsCacheOpenTables_64(ushort usOpen);

        public static uint AdsCacheOpenTables(ushort usOpen)
        {
            return IntPtr.Size == 4 ? AdsCacheOpenTables_32(usOpen) : AdsCacheOpenTables_64(usOpen);
        }

        [DllImport("ace32", EntryPoint = "AdsCacheRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsCacheRecords_32(IntPtr hTable, ushort usNumRecords);

        [DllImport("ace64", EntryPoint = "AdsCacheRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsCacheRecords_64(IntPtr hTable, ushort usNumRecords);

        public static uint AdsCacheRecords(IntPtr hTable, ushort usNumRecords)
        {
            return IntPtr.Size == 4 ? AdsCacheRecords_32(hTable, usNumRecords) : AdsCacheRecords_64(hTable, usNumRecords);
        }

        [DllImport("ace32", EntryPoint = "AdsCancelUpdate", CharSet = CharSet.Ansi)]
        private static extern uint AdsCancelUpdate_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsCancelUpdate", CharSet = CharSet.Ansi)]
        private static extern uint AdsCancelUpdate_64(IntPtr hTable);

        public static uint AdsCancelUpdate(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsCancelUpdate_32(hTable) : AdsCancelUpdate_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsCancelUpdate90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCancelUpdate90_32(IntPtr hTable, uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsCancelUpdate90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCancelUpdate90_64(IntPtr hTable, uint ulOptions);

        public static uint AdsCancelUpdate90(IntPtr hTable, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsCancelUpdate90_32(hTable, ulOptions) : AdsCancelUpdate90_64(hTable, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsCheckExistence", CharSet = CharSet.Ansi)]
        private static extern uint AdsCheckExistence_32(
        IntPtr hConnect,
        string pucFileName,
        out ushort pusOnDisk);

        [DllImport("ace64", EntryPoint = "AdsCheckExistence", CharSet = CharSet.Ansi)]
        private static extern uint AdsCheckExistence_64(
        IntPtr hConnect,
        string pucFileName,
        out ushort pusOnDisk);

        public static uint AdsCheckExistence(IntPtr hConnect, string pucFileName, out ushort pusOnDisk)
        {
            return IntPtr.Size == 4 ? AdsCheckExistence_32(hConnect, pucFileName, out pusOnDisk) : AdsCheckExistence_64(hConnect, pucFileName, out pusOnDisk);
        }

        [DllImport("ace32", EntryPoint = "AdsClearAllScopes", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearAllScopes_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsClearAllScopes", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearAllScopes_64(IntPtr hTable);

        public static uint AdsClearAllScopes(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsClearAllScopes_32(hTable) : AdsClearAllScopes_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsClearDefault", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearDefault_32();

        [DllImport("ace64", EntryPoint = "AdsClearDefault", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearDefault_64();

        public static uint AdsClearDefault()
        {
            return IntPtr.Size == 4 ? AdsClearDefault_32() : AdsClearDefault_64();
        }

        [DllImport("ace32", EntryPoint = "AdsClearFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearFilter_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsClearFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearFilter_64(IntPtr hTable);

        public static uint AdsClearFilter(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsClearFilter_32(hTable) : AdsClearFilter_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsClearRelation", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearRelation_32(IntPtr hTableParent);

        [DllImport("ace64", EntryPoint = "AdsClearRelation", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearRelation_64(IntPtr hTableParent);

        public static uint AdsClearRelation(IntPtr hTableParent)
        {
            return IntPtr.Size == 4 ? AdsClearRelation_32(hTableParent) : AdsClearRelation_64(hTableParent);
        }

        [DllImport("ace32", EntryPoint = "AdsClearScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearScope_32(IntPtr hIndex, ushort usScopeOption);

        [DllImport("ace64", EntryPoint = "AdsClearScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearScope_64(IntPtr hIndex, ushort usScopeOption);

        public static uint AdsClearScope(IntPtr hIndex, ushort usScopeOption)
        {
            return IntPtr.Size == 4 ? AdsClearScope_32(hIndex, usScopeOption) : AdsClearScope_64(hIndex, usScopeOption);
        }

        [DllImport("ace32", EntryPoint = "AdsCloneTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloneTable_32(IntPtr hTable, out IntPtr phClone);

        [DllImport("ace64", EntryPoint = "AdsCloneTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloneTable_64(IntPtr hTable, out IntPtr phClone);

        public static uint AdsCloneTable(IntPtr hTable, out IntPtr phClone)
        {
            return IntPtr.Size == 4 ? AdsCloneTable_32(hTable, out phClone) : AdsCloneTable_64(hTable, out phClone);
        }

        [DllImport("ace32", EntryPoint = "AdsCloseAllIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseAllIndexes_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsCloseAllIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseAllIndexes_64(IntPtr hTable);

        public static uint AdsCloseAllIndexes(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsCloseAllIndexes_32(hTable) : AdsCloseAllIndexes_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsCloseAllTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseAllTables_32();

        [DllImport("ace64", EntryPoint = "AdsCloseAllTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseAllTables_64();

        public static uint AdsCloseAllTables()
        {
            return IntPtr.Size == 4 ? AdsCloseAllTables_32() : AdsCloseAllTables_64();
        }

        [DllImport("ace32", EntryPoint = "AdsCloseIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseIndex_32(IntPtr hIndex);

        [DllImport("ace64", EntryPoint = "AdsCloseIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseIndex_64(IntPtr hIndex);

        public static uint AdsCloseIndex(IntPtr hIndex)
        {
            return IntPtr.Size == 4 ? AdsCloseIndex_32(hIndex) : AdsCloseIndex_64(hIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsCloseTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseTable_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsCloseTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseTable_64(IntPtr hTable);

        public static uint AdsCloseTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsCloseTable_32(hTable) : AdsCloseTable_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsCloseCachedTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseCachedTables_32(IntPtr hConnection);

        [DllImport("ace64", EntryPoint = "AdsCloseCachedTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseCachedTables_64(IntPtr hConnection);

        public static uint AdsCloseCachedTables(IntPtr hConnection)
        {
            return IntPtr.Size == 4 ? AdsCloseCachedTables_32(hConnection) : AdsCloseCachedTables_64(hConnection);
        }

        [DllImport("ace32", EntryPoint = "AdsCommitTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsCommitTransaction_32(IntPtr hConnect);

        [DllImport("ace64", EntryPoint = "AdsCommitTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsCommitTransaction_64(IntPtr hConnect);

        public static uint AdsCommitTransaction(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsCommitTransaction_32(hConnect) : AdsCommitTransaction_64(hConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsConnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect_32(string pucServerName, out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsConnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect_64(string pucServerName, out IntPtr phConnect);

        public static uint AdsConnect(string pucServerName, out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsConnect_32(pucServerName, out phConnect) : AdsConnect_64(pucServerName, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsConnect26", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect26_32(
        string pucServerName,
        ushort usServerTypes,
        out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsConnect26", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect26_64(
        string pucServerName,
        ushort usServerTypes,
        out IntPtr phConnect);

        public static uint AdsConnect26(
        string pucServerName,
        ushort usServerTypes,
        out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsConnect26_32(pucServerName, usServerTypes, out phConnect) : AdsConnect26_64(pucServerName, usServerTypes, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsConnect60", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect60_32(
        string pucServerPath,
        ushort usServerTypes,
        string pucUserName,
        string pucPassword,
        uint ulOptions,
        out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsConnect60", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect60_64(
        string pucServerPath,
        ushort usServerTypes,
        string pucUserName,
        string pucPassword,
        uint ulOptions,
        out IntPtr phConnect);

        public static uint AdsConnect60(
        string pucServerPath,
        ushort usServerTypes,
        string pucUserName,
        string pucPassword,
        uint ulOptions,
        out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsConnect60_32(pucServerPath, usServerTypes, pucUserName, pucPassword, ulOptions, out phConnect) : AdsConnect60_64(pucServerPath, usServerTypes, pucUserName, pucPassword, ulOptions, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsConnect101", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect101_32(
        string pucConnectString,
        out IntPtr phConnectionOptions,
        out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsConnect101", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect101_64(
        string pucConnectString,
        out IntPtr phConnectionOptions,
        out IntPtr phConnect);

        public static uint AdsConnect101(
        string pucConnectString,
        out IntPtr phConnectionOptions,
        out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsConnect101_32(pucConnectString, out phConnectionOptions, out phConnect) : AdsConnect101_64(pucConnectString, out phConnectionOptions, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsConnect101", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect101_32(
        string pucConnectString,
        IntPtr phNullOptions,
        out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsConnect101", CharSet = CharSet.Ansi)]
        private static extern uint AdsConnect101_64(
        string pucConnectString,
        IntPtr phNullOptions,
        out IntPtr phConnect);

        public static uint AdsConnect101(
        string pucConnectString,
        IntPtr phNullOptions,
        out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsConnect101_32(pucConnectString, phNullOptions, out phConnect) : AdsConnect101_64(pucConnectString, phNullOptions, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsClearCachePool", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearCachePool_32(string pucConnectString);

        [DllImport("ace64", EntryPoint = "AdsClearCachePool", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearCachePool_64(string pucConnectString);

        public static uint AdsClearCachePool(string pucConnectString)
        {
            return IntPtr.Size == 4 ? AdsClearCachePool_32(pucConnectString) : AdsClearCachePool_64(pucConnectString);
        }

        [DllImport("ace32", EntryPoint = "AdsReapUnusedConnections", CharSet = CharSet.Ansi)]
        private static extern uint AdsReapUnusedConnections_32();

        [DllImport("ace64", EntryPoint = "AdsReapUnusedConnections", CharSet = CharSet.Ansi)]
        private static extern uint AdsReapUnusedConnections_64();

        public static uint AdsReapUnusedConnections()
        {
            return IntPtr.Size == 4 ? AdsReapUnusedConnections_32() : AdsReapUnusedConnections_64();
        }

        [DllImport("ace32", EntryPoint = "AdsIsConnectionAlive", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsConnectionAlive_32(
        IntPtr hConnect,
        out ushort pbConnectionIsAlive);

        [DllImport("ace64", EntryPoint = "AdsIsConnectionAlive", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsConnectionAlive_64(
        IntPtr hConnect,
        out ushort pbConnectionIsAlive);

        public static uint AdsIsConnectionAlive(IntPtr hConnect, out ushort pbConnectionIsAlive)
        {
            return IntPtr.Size == 4 ? AdsIsConnectionAlive_32(hConnect, out pbConnectionIsAlive) : AdsIsConnectionAlive_64(hConnect, out pbConnectionIsAlive);
        }

        [DllImport("ace32", EntryPoint = "AdsContinue", CharSet = CharSet.Ansi)]
        private static extern uint AdsContinue_32(IntPtr hTable, out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsContinue", CharSet = CharSet.Ansi)]
        private static extern uint AdsContinue_64(IntPtr hTable, out ushort pbFound);

        public static uint AdsContinue(IntPtr hTable, out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsContinue_32(hTable, out pbFound) : AdsContinue_64(hTable, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsConvertTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertTable_32(
        IntPtr hObj,
        ushort usFilterOption,
        string pucFile,
        ushort usTableType);

        [DllImport("ace64", EntryPoint = "AdsConvertTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsConvertTable_64(
        IntPtr hObj,
        ushort usFilterOption,
        string pucFile,
        ushort usTableType);

        public static uint AdsConvertTable(
        IntPtr hObj,
        ushort usFilterOption,
        string pucFile,
        ushort usTableType)
        {
            return IntPtr.Size == 4 ? AdsConvertTable_32(hObj, usFilterOption, pucFile, usTableType) : AdsConvertTable_64(hObj, usFilterOption, pucFile, usTableType);
        }

        [DllImport("ace32", EntryPoint = "AdsCopyTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTable_32(IntPtr hObj, ushort usFilterOption, string pucFile);

        [DllImport("ace64", EntryPoint = "AdsCopyTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTable_64(IntPtr hObj, ushort usFilterOption, string pucFile);

        public static uint AdsCopyTable(IntPtr hObj, ushort usFilterOption, string pucFile)
        {
            return IntPtr.Size == 4 ? AdsCopyTable_32(hObj, usFilterOption, pucFile) : AdsCopyTable_64(hObj, usFilterOption, pucFile);
        }

        [DllImport("ace32", EntryPoint = "AdsCopyTableContents", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableContents_32(
        IntPtr hObjFrom,
        IntPtr hTableTo,
        ushort usFilterOption);

        [DllImport("ace64", EntryPoint = "AdsCopyTableContents", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableContents_64(
        IntPtr hObjFrom,
        IntPtr hTableTo,
        ushort usFilterOption);

        public static uint AdsCopyTableContents(
        IntPtr hObjFrom,
        IntPtr hTableTo,
        ushort usFilterOption)
        {
            return IntPtr.Size == 4 ? AdsCopyTableContents_32(hObjFrom, hTableTo, usFilterOption) : AdsCopyTableContents_64(hObjFrom, hTableTo, usFilterOption);
        }

        [DllImport("ace32", EntryPoint = "AdsCopyTableStructure", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableStructure_32(IntPtr hTable, string pucFile);

        [DllImport("ace64", EntryPoint = "AdsCopyTableStructure", CharSet = CharSet.Ansi)]
        private static extern uint AdsCopyTableStructure_64(IntPtr hTable, string pucFile);

        public static uint AdsCopyTableStructure(IntPtr hTable, string pucFile)
        {
            return IntPtr.Size == 4 ? AdsCopyTableStructure_32(hTable, pucFile) : AdsCopyTableStructure_64(hTable, pucFile);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateIndex_32(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        out IntPtr phIndex);

        [DllImport("ace64", EntryPoint = "AdsCreateIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateIndex_64(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        out IntPtr phIndex);

        public static uint AdsCreateIndex(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        out IntPtr phIndex)
        {
            return IntPtr.Size == 4 ? AdsCreateIndex_32(hObj, pucFileName, pucTag, pucExpr, pucCondition, pucWhile, ulOptions, out phIndex) : AdsCreateIndex_64(hObj, pucFileName, pucTag, pucExpr, pucCondition, pucWhile, ulOptions, out phIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateIndex61", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateIndex61_32(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        uint ulPageSize,
        out IntPtr phIndex);

        [DllImport("ace64", EntryPoint = "AdsCreateIndex61", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateIndex61_64(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        uint ulPageSize,
        out IntPtr phIndex);

        public static uint AdsCreateIndex61(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        uint ulPageSize,
        out IntPtr phIndex)
        {
            return IntPtr.Size == 4 ? AdsCreateIndex61_32(hObj, pucFileName, pucTag, pucExpr, pucCondition, pucWhile, ulOptions, ulPageSize, out phIndex) : AdsCreateIndex61_64(hObj, pucFileName, pucTag, pucExpr, pucCondition, pucWhile, ulOptions, ulPageSize, out phIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateIndex90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateIndex90_32(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        uint ulPageSize,
        string pucCollation,
        out IntPtr phIndex);

        [DllImport("ace64", EntryPoint = "AdsCreateIndex90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateIndex90_64(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        uint ulPageSize,
        string pucCollation,
        out IntPtr phIndex);

        public static uint AdsCreateIndex90(
        IntPtr hObj,
        string pucFileName,
        string pucTag,
        string pucExpr,
        string pucCondition,
        string pucWhile,
        uint ulOptions,
        uint ulPageSize,
        string pucCollation,
        out IntPtr phIndex)
        {
            return IntPtr.Size == 4 ? AdsCreateIndex90_32(hObj, pucFileName, pucTag, pucExpr, pucCondition, pucWhile, ulOptions, ulPageSize, pucCollation, out phIndex) : AdsCreateIndex90_64(hObj, pucFileName, pucTag, pucExpr, pucCondition, pucWhile, ulOptions, ulPageSize, pucCollation, out phIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateFTSIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateFTSIndex_32(
        IntPtr hTable,
        string pucFileName,
        string pucTag,
        string pucField,
        uint ulPageSize,
        uint ulMinWordLen,
        uint ulMaxWordLen,
        ushort usUseDefaultDelim,
        [In, Out] byte[] pvDelimiters,
        ushort usUseDefaultNoise,
        [In, Out] byte[] pvNoiseWords,
        ushort usUseDefaultDrop,
        [In, Out] byte[] pvDropChars,
        ushort usUseDefaultConditionals,
        [In, Out] byte[] pvConditionalChars,
        string pucCollation,
        string pucReserved1,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsCreateFTSIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateFTSIndex_64(
        IntPtr hTable,
        string pucFileName,
        string pucTag,
        string pucField,
        uint ulPageSize,
        uint ulMinWordLen,
        uint ulMaxWordLen,
        ushort usUseDefaultDelim,
        [In, Out] byte[] pvDelimiters,
        ushort usUseDefaultNoise,
        [In, Out] byte[] pvNoiseWords,
        ushort usUseDefaultDrop,
        [In, Out] byte[] pvDropChars,
        ushort usUseDefaultConditionals,
        [In, Out] byte[] pvConditionalChars,
        string pucCollation,
        string pucReserved1,
        uint ulOptions);

        public static uint AdsCreateFTSIndex(
        IntPtr hTable,
        string pucFileName,
        string pucTag,
        string pucField,
        uint ulPageSize,
        uint ulMinWordLen,
        uint ulMaxWordLen,
        ushort usUseDefaultDelim,
        byte[] pvDelimiters,
        ushort usUseDefaultNoise,
        byte[] pvNoiseWords,
        ushort usUseDefaultDrop,
        byte[] pvDropChars,
        ushort usUseDefaultConditionals,
        byte[] pvConditionalChars,
        string pucCollation,
        string pucReserved1,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsCreateFTSIndex_32(hTable, pucFileName, pucTag, pucField, ulPageSize, ulMinWordLen, ulMaxWordLen, usUseDefaultDelim, pvDelimiters, usUseDefaultNoise, pvNoiseWords, usUseDefaultDrop, pvDropChars, usUseDefaultConditionals, pvConditionalChars, pucCollation, pucReserved1, ulOptions) : AdsCreateFTSIndex_64(hTable, pucFileName, pucTag, pucField, ulPageSize, ulMinWordLen, ulMaxWordLen, usUseDefaultDelim, pvDelimiters, usUseDefaultNoise, pvNoiseWords, usUseDefaultDrop, pvDropChars, usUseDefaultConditionals, pvConditionalChars, pucCollation, pucReserved1, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateTable_32(
        IntPtr hConnection,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsCreateTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateTable_64(
        IntPtr hConnection,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        out IntPtr phTable);

        public static uint AdsCreateTable(
        IntPtr hConnection,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsCreateTable_32(hConnection, pucName, pucAlias, usTableType, usCharType, usLockType, usCheckRights, usMemoSize, pucFields, out phTable) : AdsCreateTable_64(hConnection, pucName, pucAlias, usTableType, usCharType, usLockType, usCheckRights, usMemoSize, pucFields, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateTable71", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateTable71_32(
        IntPtr hConnection,
        string pucName,
        string pucDBObjName,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        uint ulOptions,
        out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsCreateTable71", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateTable71_64(
        IntPtr hConnection,
        string pucName,
        string pucDBObjName,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        uint ulOptions,
        out IntPtr phTable);

        public static uint AdsCreateTable71(
        IntPtr hConnection,
        string pucName,
        string pucDBObjName,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        uint ulOptions,
        out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsCreateTable71_32(hConnection, pucName, pucDBObjName, usTableType, usCharType, usLockType, usCheckRights, usMemoSize, pucFields, ulOptions, out phTable) : AdsCreateTable71_64(hConnection, pucName, pucDBObjName, usTableType, usCharType, usLockType, usCheckRights, usMemoSize, pucFields, ulOptions, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateTable90_32(
        IntPtr hConnection,
        string pucName,
        string pucDBObjName,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        uint ulOptions,
        string pucCollation,
        out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsCreateTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateTable90_64(
        IntPtr hConnection,
        string pucName,
        string pucDBObjName,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        uint ulOptions,
        string pucCollation,
        out IntPtr phTable);

        public static uint AdsCreateTable90(
        IntPtr hConnection,
        string pucName,
        string pucDBObjName,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        ushort usMemoSize,
        string pucFields,
        uint ulOptions,
        string pucCollation,
        out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsCreateTable90_32(hConnection, pucName, pucDBObjName, usTableType, usCharType, usLockType, usCheckRights, usMemoSize, pucFields, ulOptions, pucCollation, out phTable) : AdsCreateTable90_64(hConnection, pucName, pucDBObjName, usTableType, usCharType, usLockType, usCheckRights, usMemoSize, pucFields, ulOptions, pucCollation, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreate", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreate_32(
        string pucDictionaryPath,
        ushort usEncrypt,
        string pucDescription,
        out IntPtr phDictionary);

        [DllImport("ace64", EntryPoint = "AdsDDCreate", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreate_64(
        string pucDictionaryPath,
        ushort usEncrypt,
        string pucDescription,
        out IntPtr phDictionary);

        public static uint AdsDDCreate(
        string pucDictionaryPath,
        ushort usEncrypt,
        string pucDescription,
        out IntPtr phDictionary)
        {
            return IntPtr.Size == 4 ? AdsDDCreate_32(pucDictionaryPath, usEncrypt, pucDescription, out phDictionary) : AdsDDCreate_64(pucDictionaryPath, usEncrypt, pucDescription, out phDictionary);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreate101", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreate101_32(
        string pucConnectString,
        out IntPtr phConnectOptions,
        out IntPtr phDictionary);

        [DllImport("ace64", EntryPoint = "AdsDDCreate101", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreate101_64(
        string pucConnectString,
        out IntPtr phConnectOptions,
        out IntPtr phDictionary);

        public static uint AdsDDCreate101(
        string pucConnectString,
        out IntPtr phConnectOptions,
        out IntPtr phDictionary)
        {
            return IntPtr.Size == 4 ? AdsDDCreate101_32(pucConnectString, out phConnectOptions, out phDictionary) : AdsDDCreate101_64(pucConnectString, out phConnectOptions, out phDictionary);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateRefIntegrity", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateRefIntegrity_32(
        IntPtr hDictionary,
        string pucRIName,
        string pucFailTable,
        string pucParentTableName,
        string pucParentTagName,
        string pucChildTableName,
        string pucChildTagName,
        ushort usUpdateRule,
        ushort usDeleteRule);

        [DllImport("ace64", EntryPoint = "AdsDDCreateRefIntegrity", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateRefIntegrity_64(
        IntPtr hDictionary,
        string pucRIName,
        string pucFailTable,
        string pucParentTableName,
        string pucParentTagName,
        string pucChildTableName,
        string pucChildTagName,
        ushort usUpdateRule,
        ushort usDeleteRule);

        public static uint AdsDDCreateRefIntegrity(
        IntPtr hDictionary,
        string pucRIName,
        string pucFailTable,
        string pucParentTableName,
        string pucParentTagName,
        string pucChildTableName,
        string pucChildTagName,
        ushort usUpdateRule,
        ushort usDeleteRule)
        {
            return IntPtr.Size == 4 ? AdsDDCreateRefIntegrity_32(hDictionary, pucRIName, pucFailTable, pucParentTableName, pucParentTagName, pucChildTableName, pucChildTagName, usUpdateRule, usDeleteRule) : AdsDDCreateRefIntegrity_64(hDictionary, pucRIName, pucFailTable, pucParentTableName, pucParentTagName, pucChildTableName, pucChildTagName, usUpdateRule, usDeleteRule);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateRefIntegrity62", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateRefIntegrity62_32(
        IntPtr hDictionary,
        string pucRIName,
        string pucFailTable,
        string pucParentTableName,
        string pucParentTagName,
        string pucChildTableName,
        string pucChildTagName,
        ushort usUpdateRule,
        ushort usDeleteRule,
        string pucNoPrimaryError,
        string pucCascadeError);

        [DllImport("ace64", EntryPoint = "AdsDDCreateRefIntegrity62", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateRefIntegrity62_64(
        IntPtr hDictionary,
        string pucRIName,
        string pucFailTable,
        string pucParentTableName,
        string pucParentTagName,
        string pucChildTableName,
        string pucChildTagName,
        ushort usUpdateRule,
        ushort usDeleteRule,
        string pucNoPrimaryError,
        string pucCascadeError);

        public static uint AdsDDCreateRefIntegrity62(
        IntPtr hDictionary,
        string pucRIName,
        string pucFailTable,
        string pucParentTableName,
        string pucParentTagName,
        string pucChildTableName,
        string pucChildTagName,
        ushort usUpdateRule,
        ushort usDeleteRule,
        string pucNoPrimaryError,
        string pucCascadeError)
        {
            return IntPtr.Size == 4 ? AdsDDCreateRefIntegrity62_32(hDictionary, pucRIName, pucFailTable, pucParentTableName, pucParentTagName, pucChildTableName, pucChildTagName, usUpdateRule, usDeleteRule, pucNoPrimaryError, pucCascadeError) : AdsDDCreateRefIntegrity62_64(hDictionary, pucRIName, pucFailTable, pucParentTableName, pucParentTagName, pucChildTableName, pucChildTagName, usUpdateRule, usDeleteRule, pucNoPrimaryError, pucCascadeError);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRemoveRefIntegrity", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveRefIntegrity_32(IntPtr hDictionary, string pucRIName);

        [DllImport("ace64", EntryPoint = "AdsDDRemoveRefIntegrity", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveRefIntegrity_64(IntPtr hDictionary, string pucRIName);

        public static uint AdsDDRemoveRefIntegrity(IntPtr hDictionary, string pucRIName)
        {
            return IntPtr.Size == 4 ? AdsDDRemoveRefIntegrity_32(hDictionary, pucRIName) : AdsDDRemoveRefIntegrity_64(hDictionary, pucRIName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetDatabaseProperty_32(
        IntPtr hObject,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetDatabaseProperty_64(
        IntPtr hObject,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetDatabaseProperty(
        IntPtr hObject,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetDatabaseProperty_32(hObject, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetDatabaseProperty_64(hObject, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetDatabaseProperty_32(
        IntPtr hObject,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetDatabaseProperty_64(
        IntPtr hObject,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetDatabaseProperty(
        IntPtr hObject,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetDatabaseProperty_32(hObject, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetDatabaseProperty_64(hObject, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetDatabaseProperty_32(
        IntPtr hObject,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetDatabaseProperty_64(
        IntPtr hObject,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetDatabaseProperty(
        IntPtr hObject,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetDatabaseProperty_32(hObject, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetDatabaseProperty_64(hObject, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetFieldProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetFieldProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetFieldProperty(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetFieldProperty_32(hObject, pucTableName, pucFieldName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetFieldProperty_64(hObject, pucTableName, pucFieldName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetFieldProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetFieldProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetFieldProperty(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetFieldProperty_32(hObject, pucTableName, pucFieldName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetFieldProperty_64(hObject, pucTableName, pucFieldName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetFieldProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetFieldProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetFieldProperty(
        IntPtr hObject,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetFieldProperty_32(hObject, pucTableName, pucFieldName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetFieldProperty_64(hObject, pucTableName, pucFieldName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetIndexFileProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexFileProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetIndexFileProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexFileProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetIndexFileProperty(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetIndexFileProperty_32(hObject, pucTableName, pucIndexFileName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetIndexFileProperty_64(hObject, pucTableName, pucIndexFileName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetIndexFileProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexFileProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetIndexFileProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexFileProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetIndexFileProperty(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetIndexFileProperty_32(hObject, pucTableName, pucIndexFileName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetIndexFileProperty_64(hObject, pucTableName, pucIndexFileName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetIndexFileProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexFileProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetIndexFileProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexFileProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetIndexFileProperty(
        IntPtr hObject,
        string pucTableName,
        string pucIndexFileName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetIndexFileProperty_32(hObject, pucTableName, pucIndexFileName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetIndexFileProperty_64(hObject, pucTableName, pucIndexFileName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetIndexProperty(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetIndexProperty_32(hObject, pucTableName, pucIndexName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetIndexProperty_64(hObject, pucTableName, pucIndexName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetIndexProperty(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetIndexProperty_32(hObject, pucTableName, pucIndexName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetIndexProperty_64(hObject, pucTableName, pucIndexName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexProperty_32(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetIndexProperty_64(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetIndexProperty(
        IntPtr hObject,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetIndexProperty_32(hObject, pucTableName, pucIndexName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetIndexProperty_64(hObject, pucTableName, pucIndexName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetLinkProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetLinkProperty_32(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetLinkProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetLinkProperty_64(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetLinkProperty(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetLinkProperty_32(hConnect, pucLinkName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetLinkProperty_64(hConnect, pucLinkName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetLinkProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetLinkProperty_32(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetLinkProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetLinkProperty_64(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetLinkProperty(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetLinkProperty_32(hConnect, pucLinkName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetLinkProperty_64(hConnect, pucLinkName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetLinkProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetLinkProperty_32(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetLinkProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetLinkProperty_64(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetLinkProperty(
        IntPtr hConnect,
        string pucLinkName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetLinkProperty_32(hConnect, pucLinkName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetLinkProperty_64(hConnect, pucLinkName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTableProperty_32(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTableProperty_64(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetTableProperty(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetTableProperty_32(hObject, pucTableName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetTableProperty_64(hObject, pucTableName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTableProperty_32(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTableProperty_64(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetTableProperty(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetTableProperty_32(hObject, pucTableName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetTableProperty_64(hObject, pucTableName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTableProperty_32(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTableProperty_64(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetTableProperty(
        IntPtr hObject,
        string pucTableName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetTableProperty_32(hObject, pucTableName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetTableProperty_64(hObject, pucTableName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserGroupProperty_32(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserGroupProperty_64(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetUserGroupProperty(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetUserGroupProperty_32(hObject, pucUserGroupName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetUserGroupProperty_64(hObject, pucUserGroupName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserGroupProperty_32(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserGroupProperty_64(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetUserGroupProperty(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetUserGroupProperty_32(hObject, pucUserGroupName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetUserGroupProperty_64(hObject, pucUserGroupName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserGroupProperty_32(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserGroupProperty_64(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetUserGroupProperty(
        IntPtr hObject,
        string pucUserGroupName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetUserGroupProperty_32(hObject, pucUserGroupName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetUserGroupProperty_64(hObject, pucUserGroupName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserProperty_32(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserProperty_64(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetUserProperty(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetUserProperty_32(hObject, pucUserName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetUserProperty_64(hObject, pucUserName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserProperty_32(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserProperty_64(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetUserProperty(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetUserProperty_32(hObject, pucUserName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetUserProperty_64(hObject, pucUserName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserProperty_32(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetUserProperty_64(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetUserProperty(
        IntPtr hObject,
        string pucUserName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetUserProperty_32(hObject, pucUserName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetUserProperty_64(hObject, pucUserName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetViewProperty_32(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetViewProperty_64(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetViewProperty(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetViewProperty_32(hObject, pucViewName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetViewProperty_64(hObject, pucViewName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetViewProperty_32(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetViewProperty_64(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetViewProperty(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetViewProperty_32(hObject, pucViewName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetViewProperty_64(hObject, pucViewName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetViewProperty_32(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetViewProperty_64(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetViewProperty(
        IntPtr hObject,
        string pucViewName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetViewProperty_32(hObject, pucViewName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetViewProperty_64(hObject, pucViewName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTriggerProperty_32(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTriggerProperty_64(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetTriggerProperty(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetTriggerProperty_32(hObject, pucTriggerName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetTriggerProperty_64(hObject, pucTriggerName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTriggerProperty_32(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTriggerProperty_64(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetTriggerProperty(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetTriggerProperty_32(hObject, pucTriggerName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetTriggerProperty_64(hObject, pucTriggerName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTriggerProperty_32(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetTriggerProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetTriggerProperty_64(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetTriggerProperty(
        IntPtr hObject,
        string pucTriggerName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetTriggerProperty_32(hObject, pucTriggerName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetTriggerProperty_64(hObject, pucTriggerName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcedureProperty_32(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcedureProperty_64(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetProcedureProperty(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetProcedureProperty_32(hObject, pucProcName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetProcedureProperty_64(hObject, pucProcName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcedureProperty_32(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcedureProperty_64(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetProcedureProperty(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetProcedureProperty_32(hObject, pucProcName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetProcedureProperty_64(hObject, pucProcName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcedureProperty_32(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetProcedureProperty_64(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetProcedureProperty(
        IntPtr hObject,
        string pucProcName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetProcedureProperty_32(hObject, pucProcName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetProcedureProperty_64(hObject, pucProcName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetRefIntegrityProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetRefIntegrityProperty_32(
        IntPtr hObject,
        string pucRIName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetRefIntegrityProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetRefIntegrityProperty_64(
        IntPtr hObject,
        string pucRIName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetRefIntegrityProperty(
        IntPtr hObject,
        string pucRIName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetRefIntegrityProperty_32(hObject, pucRIName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetRefIntegrityProperty_64(hObject, pucRIName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetPermissions", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPermissions_32(
        IntPtr hDBConn,
        string pucGrantee,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        ushort usGetInherited,
        out uint pulPermissions);

        [DllImport("ace64", EntryPoint = "AdsDDGetPermissions", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPermissions_64(
        IntPtr hDBConn,
        string pucGrantee,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        ushort usGetInherited,
        out uint pulPermissions);

        public static uint AdsDDGetPermissions(
        IntPtr hDBConn,
        string pucGrantee,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        ushort usGetInherited,
        out uint pulPermissions)
        {
            return IntPtr.Size == 4 ? AdsDDGetPermissions_32(hDBConn, pucGrantee, usObjectType, pucObjectName, pucParentName, usGetInherited, out pulPermissions) : AdsDDGetPermissions_64(hDBConn, pucGrantee, usObjectType, pucObjectName, pucParentName, usGetInherited, out pulPermissions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGrantPermission", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGrantPermission_32(
        IntPtr hAdminConn,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        string pucGrantee,
        uint ulPermissions);

        [DllImport("ace64", EntryPoint = "AdsDDGrantPermission", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGrantPermission_64(
        IntPtr hAdminConn,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        string pucGrantee,
        uint ulPermissions);

        public static uint AdsDDGrantPermission(
        IntPtr hAdminConn,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        string pucGrantee,
        uint ulPermissions)
        {
            return IntPtr.Size == 4 ? AdsDDGrantPermission_32(hAdminConn, usObjectType, pucObjectName, pucParentName, pucGrantee, ulPermissions) : AdsDDGrantPermission_64(hAdminConn, usObjectType, pucObjectName, pucParentName, pucGrantee, ulPermissions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRevokePermission", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRevokePermission_32(
        IntPtr hAdminConn,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        string pucGrantee,
        uint ulPermissions);

        [DllImport("ace64", EntryPoint = "AdsDDRevokePermission", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRevokePermission_64(
        IntPtr hAdminConn,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        string pucGrantee,
        uint ulPermissions);

        public static uint AdsDDRevokePermission(
        IntPtr hAdminConn,
        ushort usObjectType,
        string pucObjectName,
        string pucParentName,
        string pucGrantee,
        uint ulPermissions)
        {
            return IntPtr.Size == 4 ? AdsDDRevokePermission_32(hAdminConn, usObjectType, pucObjectName, pucParentName, pucGrantee, ulPermissions) : AdsDDRevokePermission_64(hAdminConn, usObjectType, pucObjectName, pucParentName, pucGrantee, ulPermissions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetDatabaseProperty_32(
        IntPtr hDictionary,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetDatabaseProperty_64(
        IntPtr hDictionary,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetDatabaseProperty(
        IntPtr hDictionary,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetDatabaseProperty_32(hDictionary, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetDatabaseProperty_64(hDictionary, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetDatabaseProperty_32(
        IntPtr hDictionary,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetDatabaseProperty_64(
        IntPtr hDictionary,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetDatabaseProperty(
        IntPtr hDictionary,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetDatabaseProperty_32(hDictionary, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetDatabaseProperty_64(hDictionary, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetDatabaseProperty_32(
        IntPtr hDictionary,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetDatabaseProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetDatabaseProperty_64(
        IntPtr hDictionary,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetDatabaseProperty(
        IntPtr hDictionary,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetDatabaseProperty_32(hDictionary, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetDatabaseProperty_64(hDictionary, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetFieldProperty_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        [DllImport("ace64", EntryPoint = "AdsDDSetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetFieldProperty_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        public static uint AdsDDSetFieldProperty(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable)
        {
            return IntPtr.Size == 4 ? AdsDDSetFieldProperty_32(hDictionary, pucTableName, pucFieldName, usPropertyID, pvProperty, usPropertyLen, usValidateOption, pucFailTable) : AdsDDSetFieldProperty_64(hDictionary, pucTableName, pucFieldName, usPropertyID, pvProperty, usPropertyLen, usValidateOption, pucFailTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetFieldProperty_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        [DllImport("ace64", EntryPoint = "AdsDDSetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetFieldProperty_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        public static uint AdsDDSetFieldProperty(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable)
        {
            return IntPtr.Size == 4 ? AdsDDSetFieldProperty_32(hDictionary, pucTableName, pucFieldName, usPropertyID, pucProperty, usPropertyLen, usValidateOption, pucFailTable) : AdsDDSetFieldProperty_64(hDictionary, pucTableName, pucFieldName, usPropertyID, pucProperty, usPropertyLen, usValidateOption, pucFailTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetFieldProperty_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        [DllImport("ace64", EntryPoint = "AdsDDSetFieldProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetFieldProperty_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        public static uint AdsDDSetFieldProperty(
        IntPtr hDictionary,
        string pucTableName,
        string pucFieldName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable)
        {
            return IntPtr.Size == 4 ? AdsDDSetFieldProperty_32(hDictionary, pucTableName, pucFieldName, usPropertyID, ref pusProperty, usPropertyLen, usValidateOption, pucFailTable) : AdsDDSetFieldProperty_64(hDictionary, pucTableName, pucFieldName, usPropertyID, ref pusProperty, usPropertyLen, usValidateOption, pucFailTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetProcedureProperty_32(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetProcedureProperty_64(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetProcedureProperty(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetProcedureProperty_32(hDictionary, pucProcedureName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetProcedureProperty_64(hDictionary, pucProcedureName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetProcedureProperty_32(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetProcedureProperty_64(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetProcedureProperty(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetProcedureProperty_32(hDictionary, pucProcedureName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetProcedureProperty_64(hDictionary, pucProcedureName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetProcedureProperty_32(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetProcedureProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetProcedureProperty_64(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetProcedureProperty(
        IntPtr hDictionary,
        string pucProcedureName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetProcedureProperty_32(hDictionary, pucProcedureName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetProcedureProperty_64(hDictionary, pucProcedureName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTableProperty_32(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        [DllImport("ace64", EntryPoint = "AdsDDSetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTableProperty_64(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        public static uint AdsDDSetTableProperty(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable)
        {
            return IntPtr.Size == 4 ? AdsDDSetTableProperty_32(hDictionary, pucTableName, usPropertyID, pvProperty, usPropertyLen, usValidateOption, pucFailTable) : AdsDDSetTableProperty_64(hDictionary, pucTableName, usPropertyID, pvProperty, usPropertyLen, usValidateOption, pucFailTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTableProperty_32(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        [DllImport("ace64", EntryPoint = "AdsDDSetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTableProperty_64(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        public static uint AdsDDSetTableProperty(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable)
        {
            return IntPtr.Size == 4 ? AdsDDSetTableProperty_32(hDictionary, pucTableName, usPropertyID, pucProperty, usPropertyLen, usValidateOption, pucFailTable) : AdsDDSetTableProperty_64(hDictionary, pucTableName, usPropertyID, pucProperty, usPropertyLen, usValidateOption, pucFailTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTableProperty_32(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        [DllImport("ace64", EntryPoint = "AdsDDSetTableProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetTableProperty_64(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable);

        public static uint AdsDDSetTableProperty(
        IntPtr hDictionary,
        string pucTableName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen,
        ushort usValidateOption,
        string pucFailTable)
        {
            return IntPtr.Size == 4 ? AdsDDSetTableProperty_32(hDictionary, pucTableName, usPropertyID, ref pusProperty, usPropertyLen, usValidateOption, pucFailTable) : AdsDDSetTableProperty_64(hDictionary, pucTableName, usPropertyID, ref pusProperty, usPropertyLen, usValidateOption, pucFailTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserGroupProperty_32(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserGroupProperty_64(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetUserGroupProperty(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetUserGroupProperty_32(hDictionary, pucUserGroupName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetUserGroupProperty_64(hDictionary, pucUserGroupName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserGroupProperty_32(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserGroupProperty_64(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetUserGroupProperty(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetUserGroupProperty_32(hDictionary, pucUserGroupName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetUserGroupProperty_64(hDictionary, pucUserGroupName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserGroupProperty_32(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetUserGroupProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserGroupProperty_64(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetUserGroupProperty(
        IntPtr hDictionary,
        string pucUserGroupName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetUserGroupProperty_32(hDictionary, pucUserGroupName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetUserGroupProperty_64(hDictionary, pucUserGroupName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserProperty_32(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserProperty_64(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetUserProperty(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetUserProperty_32(hDictionary, pucUserName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetUserProperty_64(hDictionary, pucUserName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserProperty_32(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserProperty_64(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetUserProperty(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetUserProperty_32(hDictionary, pucUserName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetUserProperty_64(hDictionary, pucUserName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserProperty_32(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetUserProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetUserProperty_64(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetUserProperty(
        IntPtr hDictionary,
        string pucUserName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetUserProperty_32(hDictionary, pucUserName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetUserProperty_64(hDictionary, pucUserName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetViewProperty_32(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetViewProperty_64(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetViewProperty(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetViewProperty_32(hDictionary, pucViewName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetViewProperty_64(hDictionary, pucViewName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetViewProperty_32(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetViewProperty_64(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetViewProperty(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetViewProperty_32(hDictionary, pucViewName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetViewProperty_64(hDictionary, pucViewName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetViewProperty_32(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetViewProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetViewProperty_64(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetViewProperty(
        IntPtr hDictionary,
        string pucViewName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetViewProperty_32(hDictionary, pucViewName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetViewProperty_64(hDictionary, pucViewName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetObjectAccessRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectAccessRights_32(
        IntPtr hDictionary,
        string pucObjectName,
        string pucAccessorName,
        string pucAllowedAccess);

        [DllImport("ace64", EntryPoint = "AdsDDSetObjectAccessRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetObjectAccessRights_64(
        IntPtr hDictionary,
        string pucObjectName,
        string pucAccessorName,
        string pucAllowedAccess);

        public static uint AdsDDSetObjectAccessRights(
        IntPtr hDictionary,
        string pucObjectName,
        string pucAccessorName,
        string pucAllowedAccess)
        {
            return IntPtr.Size == 4 ? AdsDDSetObjectAccessRights_32(hDictionary, pucObjectName, pucAccessorName, pucAllowedAccess) : AdsDDSetObjectAccessRights_64(hDictionary, pucObjectName, pucAccessorName, pucAllowedAccess);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddProcedure", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddProcedure_32(
        IntPtr hDictionary,
        string pucName,
        string pucContainer,
        string pucProcName,
        uint ulInvokeOption,
        string pucInParams,
        string pucOutParams,
        string pucComments);

        [DllImport("ace64", EntryPoint = "AdsDDAddProcedure", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddProcedure_64(
        IntPtr hDictionary,
        string pucName,
        string pucContainer,
        string pucProcName,
        uint ulInvokeOption,
        string pucInParams,
        string pucOutParams,
        string pucComments);

        public static uint AdsDDAddProcedure(
        IntPtr hDictionary,
        string pucName,
        string pucContainer,
        string pucProcName,
        uint ulInvokeOption,
        string pucInParams,
        string pucOutParams,
        string pucComments)
        {
            return IntPtr.Size == 4 ? AdsDDAddProcedure_32(hDictionary, pucName, pucContainer, pucProcName, ulInvokeOption, pucInParams, pucOutParams, pucComments) : AdsDDAddProcedure_64(hDictionary, pucName, pucContainer, pucProcName, ulInvokeOption, pucInParams, pucOutParams, pucComments);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddProcedure100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddProcedure100_32(
        IntPtr hDictionary,
        string pucName,
        string pwcContainer,
        string pucProcName,
        uint ulInvokeOption,
        string pucInParams,
        string pucOutParams,
        string pucComments);

        [DllImport("ace64", EntryPoint = "AdsDDAddProcedure100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddProcedure100_64(
        IntPtr hDictionary,
        string pucName,
        string pwcContainer,
        string pucProcName,
        uint ulInvokeOption,
        string pucInParams,
        string pucOutParams,
        string pucComments);

        public static uint AdsDDAddProcedure100(
        IntPtr hDictionary,
        string pucName,
        string pwcContainer,
        string pucProcName,
        uint ulInvokeOption,
        string pucInParams,
        string pucOutParams,
        string pucComments)
        {
            return IntPtr.Size == 4 ? AdsDDAddProcedure100_32(hDictionary, pucName, pwcContainer, pucProcName, ulInvokeOption, pucInParams, pucOutParams, pucComments) : AdsDDAddProcedure100_64(hDictionary, pucName, pwcContainer, pucProcName, ulInvokeOption, pucInParams, pucOutParams, pucComments);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddTable_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucTablePath,
        ushort usTableType,
        ushort usCharType,
        string pucIndexFiles,
        string pucComments);

        [DllImport("ace64", EntryPoint = "AdsDDAddTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddTable_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucTablePath,
        ushort usTableType,
        ushort usCharType,
        string pucIndexFiles,
        string pucComments);

        public static uint AdsDDAddTable(
        IntPtr hDictionary,
        string pucTableName,
        string pucTablePath,
        ushort usTableType,
        ushort usCharType,
        string pucIndexFiles,
        string pucComments)
        {
            return IntPtr.Size == 4 ? AdsDDAddTable_32(hDictionary, pucTableName, pucTablePath, usTableType, usCharType, pucIndexFiles, pucComments) : AdsDDAddTable_64(hDictionary, pucTableName, pucTablePath, usTableType, usCharType, pucIndexFiles, pucComments);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddTable90_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucTablePath,
        ushort usTableType,
        ushort usCharType,
        string pucIndexFiles,
        string pucComments,
        string pucCollation);

        [DllImport("ace64", EntryPoint = "AdsDDAddTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddTable90_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucTablePath,
        ushort usTableType,
        ushort usCharType,
        string pucIndexFiles,
        string pucComments,
        string pucCollation);

        public static uint AdsDDAddTable90(
        IntPtr hDictionary,
        string pucTableName,
        string pucTablePath,
        ushort usTableType,
        ushort usCharType,
        string pucIndexFiles,
        string pucComments,
        string pucCollation)
        {
            return IntPtr.Size == 4 ? AdsDDAddTable90_32(hDictionary, pucTableName, pucTablePath, usTableType, usCharType, pucIndexFiles, pucComments, pucCollation) : AdsDDAddTable90_64(hDictionary, pucTableName, pucTablePath, usTableType, usCharType, pucIndexFiles, pucComments, pucCollation);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddView", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddView_32(
        IntPtr hDictionary,
        string pucName,
        string pucComments,
        string pucSQL);

        [DllImport("ace64", EntryPoint = "AdsDDAddView", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddView_64(
        IntPtr hDictionary,
        string pucName,
        string pucComments,
        string pucSQL);

        public static uint AdsDDAddView(
        IntPtr hDictionary,
        string pucName,
        string pucComments,
        string pucSQL)
        {
            return IntPtr.Size == 4 ? AdsDDAddView_32(hDictionary, pucName, pucComments, pucSQL) : AdsDDAddView_64(hDictionary, pucName, pucComments, pucSQL);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddView100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddView100_32(
        IntPtr hDictionary,
        string pucName,
        string pucComments,
        string pwcSQL);

        [DllImport("ace64", EntryPoint = "AdsDDAddView100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddView100_64(
        IntPtr hDictionary,
        string pucName,
        string pucComments,
        string pwcSQL);

        public static uint AdsDDAddView100(
        IntPtr hDictionary,
        string pucName,
        string pucComments,
        string pwcSQL)
        {
            return IntPtr.Size == 4 ? AdsDDAddView100_32(hDictionary, pucName, pucComments, pwcSQL) : AdsDDAddView100_64(hDictionary, pucName, pucComments, pwcSQL);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateTrigger", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateTrigger_32(
        IntPtr hDictionary,
        string pucName,
        string pucTableName,
        uint ulTriggerType,
        uint ulEventTypes,
        uint ulContainerType,
        string pucContainer,
        string pucFunctionName,
        uint ulPriority,
        string pucComments,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDCreateTrigger", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateTrigger_64(
        IntPtr hDictionary,
        string pucName,
        string pucTableName,
        uint ulTriggerType,
        uint ulEventTypes,
        uint ulContainerType,
        string pucContainer,
        string pucFunctionName,
        uint ulPriority,
        string pucComments,
        uint ulOptions);

        public static uint AdsDDCreateTrigger(
        IntPtr hDictionary,
        string pucName,
        string pucTableName,
        uint ulTriggerType,
        uint ulEventTypes,
        uint ulContainerType,
        string pucContainer,
        string pucFunctionName,
        uint ulPriority,
        string pucComments,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateTrigger_32(hDictionary, pucName, pucTableName, ulTriggerType, ulEventTypes, ulContainerType, pucContainer, pucFunctionName, ulPriority, pucComments, ulOptions) : AdsDDCreateTrigger_64(hDictionary, pucName, pucTableName, ulTriggerType, ulEventTypes, ulContainerType, pucContainer, pucFunctionName, ulPriority, pucComments, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateTrigger100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateTrigger100_32(
        IntPtr hDictionary,
        string pucName,
        string pucTableName,
        uint ulTriggerType,
        uint ulEventTypes,
        uint ulContainerType,
        string pwcContainer,
        string pucFunctionName,
        uint ulPriority,
        string pucComments,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDCreateTrigger100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateTrigger100_64(
        IntPtr hDictionary,
        string pucName,
        string pucTableName,
        uint ulTriggerType,
        uint ulEventTypes,
        uint ulContainerType,
        string pwcContainer,
        string pucFunctionName,
        uint ulPriority,
        string pucComments,
        uint ulOptions);

        public static uint AdsDDCreateTrigger100(
        IntPtr hDictionary,
        string pucName,
        string pucTableName,
        uint ulTriggerType,
        uint ulEventTypes,
        uint ulContainerType,
        string pwcContainer,
        string pucFunctionName,
        uint ulPriority,
        string pucComments,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateTrigger100_32(hDictionary, pucName, pucTableName, ulTriggerType, ulEventTypes, ulContainerType, pwcContainer, pucFunctionName, ulPriority, pucComments, ulOptions) : AdsDDCreateTrigger100_64(hDictionary, pucName, pucTableName, ulTriggerType, ulEventTypes, ulContainerType, pwcContainer, pucFunctionName, ulPriority, pucComments, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRemoveTrigger", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveTrigger_32(IntPtr hDictionary, string pucName);

        [DllImport("ace64", EntryPoint = "AdsDDRemoveTrigger", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveTrigger_64(IntPtr hDictionary, string pucName);

        public static uint AdsDDRemoveTrigger(IntPtr hDictionary, string pucName)
        {
            return IntPtr.Size == 4 ? AdsDDRemoveTrigger_32(hDictionary, pucName) : AdsDDRemoveTrigger_64(hDictionary, pucName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddIndexFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddIndexFile_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexFilePath,
        string pucComment);

        [DllImport("ace64", EntryPoint = "AdsDDAddIndexFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddIndexFile_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexFilePath,
        string pucComment);

        public static uint AdsDDAddIndexFile(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexFilePath,
        string pucComment)
        {
            return IntPtr.Size == 4 ? AdsDDAddIndexFile_32(hDictionary, pucTableName, pucIndexFilePath, pucComment) : AdsDDAddIndexFile_64(hDictionary, pucTableName, pucIndexFilePath, pucComment);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateUser", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateUser_32(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName,
        string pucPassword,
        string pucDescription);

        [DllImport("ace64", EntryPoint = "AdsDDCreateUser", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateUser_64(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName,
        string pucPassword,
        string pucDescription);

        public static uint AdsDDCreateUser(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName,
        string pucPassword,
        string pucDescription)
        {
            return IntPtr.Size == 4 ? AdsDDCreateUser_32(hDictionary, pucGroupName, pucUserName, pucPassword, pucDescription) : AdsDDCreateUser_64(hDictionary, pucGroupName, pucUserName, pucPassword, pucDescription);
        }

        [DllImport("ace32", EntryPoint = "AdsDDAddUserToGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddUserToGroup_32(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName);

        [DllImport("ace64", EntryPoint = "AdsDDAddUserToGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDAddUserToGroup_64(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName);

        public static uint AdsDDAddUserToGroup(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName)
        {
            return IntPtr.Size == 4 ? AdsDDAddUserToGroup_32(hDictionary, pucGroupName, pucUserName) : AdsDDAddUserToGroup_64(hDictionary, pucGroupName, pucUserName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRemoveUserFromGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveUserFromGroup_32(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName);

        [DllImport("ace64", EntryPoint = "AdsDDRemoveUserFromGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveUserFromGroup_64(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName);

        public static uint AdsDDRemoveUserFromGroup(
        IntPtr hDictionary,
        string pucGroupName,
        string pucUserName)
        {
            return IntPtr.Size == 4 ? AdsDDRemoveUserFromGroup_32(hDictionary, pucGroupName, pucUserName) : AdsDDRemoveUserFromGroup_64(hDictionary, pucGroupName, pucUserName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDeleteUser", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteUser_32(IntPtr hDictionary, string pucUserName);

        [DllImport("ace64", EntryPoint = "AdsDDDeleteUser", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteUser_64(IntPtr hDictionary, string pucUserName);

        public static uint AdsDDDeleteUser(IntPtr hDictionary, string pucUserName)
        {
            return IntPtr.Size == 4 ? AdsDDDeleteUser_32(hDictionary, pucUserName) : AdsDDDeleteUser_64(hDictionary, pucUserName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateUserGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateUserGroup_32(
        IntPtr hDictionary,
        string pucGroupName,
        string pucDescription);

        [DllImport("ace64", EntryPoint = "AdsDDCreateUserGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateUserGroup_64(
        IntPtr hDictionary,
        string pucGroupName,
        string pucDescription);

        public static uint AdsDDCreateUserGroup(
        IntPtr hDictionary,
        string pucGroupName,
        string pucDescription)
        {
            return IntPtr.Size == 4 ? AdsDDCreateUserGroup_32(hDictionary, pucGroupName, pucDescription) : AdsDDCreateUserGroup_64(hDictionary, pucGroupName, pucDescription);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDeleteUserGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteUserGroup_32(IntPtr hDictionary, string pucGroupName);

        [DllImport("ace64", EntryPoint = "AdsDDDeleteUserGroup", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteUserGroup_64(IntPtr hDictionary, string pucGroupName);

        public static uint AdsDDDeleteUserGroup(IntPtr hDictionary, string pucGroupName)
        {
            return IntPtr.Size == 4 ? AdsDDDeleteUserGroup_32(hDictionary, pucGroupName) : AdsDDDeleteUserGroup_64(hDictionary, pucGroupName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDeleteIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteIndex_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexName);

        [DllImport("ace64", EntryPoint = "AdsDDDeleteIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteIndex_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexName);

        public static uint AdsDDDeleteIndex(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexName)
        {
            return IntPtr.Size == 4 ? AdsDDDeleteIndex_32(hDictionary, pucTableName, pucIndexName) : AdsDDDeleteIndex_64(hDictionary, pucTableName, pucIndexName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRemoveIndexFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveIndexFile_32(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexFileName,
        ushort usDeleteFile);

        [DllImport("ace64", EntryPoint = "AdsDDRemoveIndexFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveIndexFile_64(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexFileName,
        ushort usDeleteFile);

        public static uint AdsDDRemoveIndexFile(
        IntPtr hDictionary,
        string pucTableName,
        string pucIndexFileName,
        ushort usDeleteFile)
        {
            return IntPtr.Size == 4 ? AdsDDRemoveIndexFile_32(hDictionary, pucTableName, pucIndexFileName, usDeleteFile) : AdsDDRemoveIndexFile_64(hDictionary, pucTableName, pucIndexFileName, usDeleteFile);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRemoveProcedure", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveProcedure_32(IntPtr hDictionary, string pucName);

        [DllImport("ace64", EntryPoint = "AdsDDRemoveProcedure", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveProcedure_64(IntPtr hDictionary, string pucName);

        public static uint AdsDDRemoveProcedure(IntPtr hDictionary, string pucName)
        {
            return IntPtr.Size == 4 ? AdsDDRemoveProcedure_32(hDictionary, pucName) : AdsDDRemoveProcedure_64(hDictionary, pucName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRemoveTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveTable_32(
        IntPtr hObject,
        string pucTableName,
        ushort usDeleteFiles);

        [DllImport("ace64", EntryPoint = "AdsDDRemoveTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveTable_64(
        IntPtr hObject,
        string pucTableName,
        ushort usDeleteFiles);

        public static uint AdsDDRemoveTable(IntPtr hObject, string pucTableName, ushort usDeleteFiles)
        {
            return IntPtr.Size == 4 ? AdsDDRemoveTable_32(hObject, pucTableName, usDeleteFiles) : AdsDDRemoveTable_64(hObject, pucTableName, usDeleteFiles);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRemoveView", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveView_32(IntPtr hDictionary, string pucName);

        [DllImport("ace64", EntryPoint = "AdsDDRemoveView", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRemoveView_64(IntPtr hDictionary, string pucName);

        public static uint AdsDDRemoveView(IntPtr hDictionary, string pucName)
        {
            return IntPtr.Size == 4 ? AdsDDRemoveView_32(hDictionary, pucName) : AdsDDRemoveView_64(hDictionary, pucName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDRenameObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRenameObject_32(
        IntPtr hDictionary,
        string pucObjectName,
        string pucNewObjectName,
        ushort usObjectType,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDRenameObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDRenameObject_64(
        IntPtr hDictionary,
        string pucObjectName,
        string pucNewObjectName,
        ushort usObjectType,
        uint ulOptions);

        public static uint AdsDDRenameObject(
        IntPtr hDictionary,
        string pucObjectName,
        string pucNewObjectName,
        ushort usObjectType,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDRenameObject_32(hDictionary, pucObjectName, pucNewObjectName, usObjectType, ulOptions) : AdsDDRenameObject_64(hDictionary, pucObjectName, pucNewObjectName, usObjectType, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDMoveObjectFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDMoveObjectFile_32(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucObjectName,
        string pucNewPath,
        string pucIndexFiles,
        string pucParent,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDMoveObjectFile", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDMoveObjectFile_64(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucObjectName,
        string pucNewPath,
        string pucIndexFiles,
        string pucParent,
        uint ulOptions);

        public static uint AdsDDMoveObjectFile(
        IntPtr hDictionary,
        ushort usObjectType,
        string pucObjectName,
        string pucNewPath,
        string pucIndexFiles,
        string pucParent,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDMoveObjectFile_32(hDictionary, usObjectType, pucObjectName, pucNewPath, pucIndexFiles, pucParent, ulOptions) : AdsDDMoveObjectFile_64(hDictionary, usObjectType, pucObjectName, pucNewPath, pucIndexFiles, pucParent, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDFindFirstObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFindFirstObject_32(
        IntPtr hObject,
        ushort usFindObjectType,
        string pucParentName,
        [In, Out] char[] pucObjectName,
        ref ushort pusObjectNameLen,
        out IntPtr phFindHandle);

        [DllImport("ace64", EntryPoint = "AdsDDFindFirstObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFindFirstObject_64(
        IntPtr hObject,
        ushort usFindObjectType,
        string pucParentName,
        [In, Out] char[] pucObjectName,
        ref ushort pusObjectNameLen,
        out IntPtr phFindHandle);

        public static uint AdsDDFindFirstObject(
        IntPtr hObject,
        ushort usFindObjectType,
        string pucParentName,
        char[] pucObjectName,
        ref ushort pusObjectNameLen,
        out IntPtr phFindHandle)
        {
            return IntPtr.Size == 4 ? AdsDDFindFirstObject_32(hObject, usFindObjectType, pucParentName, pucObjectName, ref pusObjectNameLen, out phFindHandle) : AdsDDFindFirstObject_64(hObject, usFindObjectType, pucParentName, pucObjectName, ref pusObjectNameLen, out phFindHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsDDFindNextObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFindNextObject_32(
        IntPtr hObject,
        IntPtr hFindHandle,
        [In, Out] char[] pucObjectName,
        ref ushort pusObjectNameLen);

        [DllImport("ace64", EntryPoint = "AdsDDFindNextObject", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFindNextObject_64(
        IntPtr hObject,
        IntPtr hFindHandle,
        [In, Out] char[] pucObjectName,
        ref ushort pusObjectNameLen);

        public static uint AdsDDFindNextObject(
        IntPtr hObject,
        IntPtr hFindHandle,
        char[] pucObjectName,
        ref ushort pusObjectNameLen)
        {
            return IntPtr.Size == 4 ? AdsDDFindNextObject_32(hObject, hFindHandle, pucObjectName, ref pusObjectNameLen) : AdsDDFindNextObject_64(hObject, hFindHandle, pucObjectName, ref pusObjectNameLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDFindClose", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFindClose_32(IntPtr hObject, IntPtr hFindHandle);

        [DllImport("ace64", EntryPoint = "AdsDDFindClose", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFindClose_64(IntPtr hObject, IntPtr hFindHandle);

        public static uint AdsDDFindClose(IntPtr hObject, IntPtr hFindHandle)
        {
            return IntPtr.Size == 4 ? AdsDDFindClose_32(hObject, hFindHandle) : AdsDDFindClose_64(hObject, hFindHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateLink", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateLink_32(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDCreateLink", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateLink_64(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions);

        public static uint AdsDDCreateLink(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateLink_32(hDBConn, pucLinkAlias, pucLinkedDDPath, pucUserName, pucPassword, ulOptions) : AdsDDCreateLink_64(hDBConn, pucLinkAlias, pucLinkedDDPath, pucUserName, pucPassword, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDModifyLink", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDModifyLink_32(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDModifyLink", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDModifyLink_64(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions);

        public static uint AdsDDModifyLink(
        IntPtr hDBConn,
        string pucLinkAlias,
        string pucLinkedDDPath,
        string pucUserName,
        string pucPassword,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDModifyLink_32(hDBConn, pucLinkAlias, pucLinkedDDPath, pucUserName, pucPassword, ulOptions) : AdsDDModifyLink_64(hDBConn, pucLinkAlias, pucLinkedDDPath, pucUserName, pucPassword, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDropLink", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropLink_32(
        IntPtr hDBConn,
        string pucLinkedDD,
        ushort usDropGlobal);

        [DllImport("ace64", EntryPoint = "AdsDDDropLink", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDropLink_64(
        IntPtr hDBConn,
        string pucLinkedDD,
        ushort usDropGlobal);

        public static uint AdsDDDropLink(IntPtr hDBConn, string pucLinkedDD, ushort usDropGlobal)
        {
            return IntPtr.Size == 4 ? AdsDDDropLink_32(hDBConn, pucLinkedDD, usDropGlobal) : AdsDDDropLink_64(hDBConn, pucLinkedDD, usDropGlobal);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreatePublication", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreatePublication_32(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucComments,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDCreatePublication", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreatePublication_64(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucComments,
        uint ulOptions);

        public static uint AdsDDCreatePublication(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucComments,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreatePublication_32(hDictionary, pucPublicationName, pucComments, ulOptions) : AdsDDCreatePublication_64(hDictionary, pucPublicationName, pucComments, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPublicationProperty_32(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPublicationProperty_64(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetPublicationProperty(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetPublicationProperty_32(hObject, pucPublicationName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetPublicationProperty_64(hObject, pucPublicationName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPublicationProperty_32(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPublicationProperty_64(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetPublicationProperty(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetPublicationProperty_32(hObject, pucPublicationName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetPublicationProperty_64(hObject, pucPublicationName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPublicationProperty_32(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetPublicationProperty_64(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetPublicationProperty(
        IntPtr hObject,
        string pucPublicationName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetPublicationProperty_32(hObject, pucPublicationName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetPublicationProperty_64(hObject, pucPublicationName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetPublicationProperty_32(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetPublicationProperty_64(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetPublicationProperty(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetPublicationProperty_32(hDictionary, pucPublicationName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetPublicationProperty_64(hDictionary, pucPublicationName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetPublicationProperty_32(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetPublicationProperty_64(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetPublicationProperty(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetPublicationProperty_32(hDictionary, pucPublicationName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetPublicationProperty_64(hDictionary, pucPublicationName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetPublicationProperty_32(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetPublicationProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetPublicationProperty_64(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetPublicationProperty(
        IntPtr hDictionary,
        string pucPublicationName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetPublicationProperty_32(hDictionary, pucPublicationName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetPublicationProperty_64(hDictionary, pucPublicationName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDeletePublication", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeletePublication_32(
        IntPtr hDictionary,
        string pucPublicationName);

        [DllImport("ace64", EntryPoint = "AdsDDDeletePublication", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeletePublication_64(
        IntPtr hDictionary,
        string pucPublicationName);

        public static uint AdsDDDeletePublication(IntPtr hDictionary, string pucPublicationName)
        {
            return IntPtr.Size == 4 ? AdsDDDeletePublication_32(hDictionary, pucPublicationName) : AdsDDDeletePublication_64(hDictionary, pucPublicationName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateArticle", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateArticle_32(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        string pucRowIdentColumns,
        string pucFilter,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDCreateArticle", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateArticle_64(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        string pucRowIdentColumns,
        string pucFilter,
        uint ulOptions);

        public static uint AdsDDCreateArticle(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        string pucRowIdentColumns,
        string pucFilter,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateArticle_32(hDictionary, pucPublicationName, pucObjectName, pucRowIdentColumns, pucFilter, ulOptions) : AdsDDCreateArticle_64(hDictionary, pucPublicationName, pucObjectName, pucRowIdentColumns, pucFilter, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateArticle100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateArticle100_32(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        string pucRowIdentColumns,
        string pwcFilter,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDCreateArticle100", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateArticle100_64(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        string pucRowIdentColumns,
        string pwcFilter,
        uint ulOptions);

        public static uint AdsDDCreateArticle100(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        string pucRowIdentColumns,
        string pwcFilter,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateArticle100_32(hDictionary, pucPublicationName, pucObjectName, pucRowIdentColumns, pwcFilter, ulOptions) : AdsDDCreateArticle100_64(hDictionary, pucPublicationName, pucObjectName, pucRowIdentColumns, pwcFilter, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetArticleProperty_32(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetArticleProperty_64(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetArticleProperty(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetArticleProperty_32(hObject, pucPublicationName, pucObjectName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetArticleProperty_64(hObject, pucPublicationName, pucObjectName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetArticleProperty_32(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetArticleProperty_64(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetArticleProperty(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetArticleProperty_32(hObject, pucPublicationName, pucObjectName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetArticleProperty_64(hObject, pucPublicationName, pucObjectName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetArticleProperty_32(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetArticleProperty_64(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetArticleProperty(
        IntPtr hObject,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetArticleProperty_32(hObject, pucPublicationName, pucObjectName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetArticleProperty_64(hObject, pucPublicationName, pucObjectName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetArticleProperty_32(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetArticleProperty_64(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetArticleProperty(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetArticleProperty_32(hDictionary, pucPublicationName, pucObjectName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetArticleProperty_64(hDictionary, pucPublicationName, pucObjectName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetArticleProperty_32(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetArticleProperty_64(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetArticleProperty(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetArticleProperty_32(hDictionary, pucPublicationName, pucObjectName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetArticleProperty_64(hDictionary, pucPublicationName, pucObjectName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetArticleProperty_32(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetArticleProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetArticleProperty_64(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetArticleProperty(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetArticleProperty_32(hDictionary, pucPublicationName, pucObjectName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetArticleProperty_64(hDictionary, pucPublicationName, pucObjectName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDeleteArticle", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteArticle_32(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName);

        [DllImport("ace64", EntryPoint = "AdsDDDeleteArticle", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteArticle_64(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName);

        public static uint AdsDDDeleteArticle(
        IntPtr hDictionary,
        string pucPublicationName,
        string pucObjectName)
        {
            return IntPtr.Size == 4 ? AdsDDDeleteArticle_32(hDictionary, pucPublicationName, pucObjectName) : AdsDDDeleteArticle_64(hDictionary, pucPublicationName, pucObjectName);
        }

        [DllImport("ace32", EntryPoint = "AdsDDCreateSubscription", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateSubscription_32(
        IntPtr hDictionary,
        string pucSubscriptionName,
        string pucPublicationName,
        string pucTarget,
        string pucUser,
        string pucPassword,
        string pucReplicationQueue,
        ushort usForward,
        string pucComments,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDCreateSubscription", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDCreateSubscription_64(
        IntPtr hDictionary,
        string pucSubscriptionName,
        string pucPublicationName,
        string pucTarget,
        string pucUser,
        string pucPassword,
        string pucReplicationQueue,
        ushort usForward,
        string pucComments,
        uint ulOptions);

        public static uint AdsDDCreateSubscription(
        IntPtr hDictionary,
        string pucSubscriptionName,
        string pucPublicationName,
        string pucTarget,
        string pucUser,
        string pucPassword,
        string pucReplicationQueue,
        ushort usForward,
        string pucComments,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDCreateSubscription_32(hDictionary, pucSubscriptionName, pucPublicationName, pucTarget, pucUser, pucPassword, pucReplicationQueue, usForward, pucComments, ulOptions) : AdsDDCreateSubscription_64(hDictionary, pucSubscriptionName, pucPublicationName, pucTarget, pucUser, pucPassword, pucReplicationQueue, usForward, pucComments, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetSubscriptionProperty_32(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetSubscriptionProperty_64(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetSubscriptionProperty(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        byte[] pvProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetSubscriptionProperty_32(hObject, pucSubscriptionName, usPropertyID, pvProperty, ref pusPropertyLen) : AdsDDGetSubscriptionProperty_64(hObject, pucSubscriptionName, usPropertyID, pvProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetSubscriptionProperty_32(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetSubscriptionProperty_64(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetSubscriptionProperty(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        char[] pucProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetSubscriptionProperty_32(hObject, pucSubscriptionName, usPropertyID, pucProperty, ref pusPropertyLen) : AdsDDGetSubscriptionProperty_64(hObject, pucSubscriptionName, usPropertyID, pucProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDGetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetSubscriptionProperty_32(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDGetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDGetSubscriptionProperty_64(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen);

        public static uint AdsDDGetSubscriptionProperty(
        IntPtr hObject,
        string pucSubscriptionName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ref ushort pusPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDGetSubscriptionProperty_32(hObject, pucSubscriptionName, usPropertyID, ref pusProperty, ref pusPropertyLen) : AdsDDGetSubscriptionProperty_64(hObject, pucSubscriptionName, usPropertyID, ref pusProperty, ref pusPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetSubscriptionProperty_32(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetSubscriptionProperty_64(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetSubscriptionProperty(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetSubscriptionProperty_32(hDictionary, pucSubscriptionName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetSubscriptionProperty_64(hDictionary, pucSubscriptionName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetSubscriptionProperty_32(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetSubscriptionProperty_64(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetSubscriptionProperty(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetSubscriptionProperty_32(hDictionary, pucSubscriptionName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetSubscriptionProperty_64(hDictionary, pucSubscriptionName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetSubscriptionProperty_32(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetSubscriptionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetSubscriptionProperty_64(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetSubscriptionProperty(
        IntPtr hDictionary,
        string pucSubscriptionName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetSubscriptionProperty_32(hDictionary, pucSubscriptionName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetSubscriptionProperty_64(hDictionary, pucSubscriptionName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDeleteSubscription", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteSubscription_32(
        IntPtr hDictionary,
        string pucSubscriptionName);

        [DllImport("ace64", EntryPoint = "AdsDDDeleteSubscription", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeleteSubscription_64(
        IntPtr hDictionary,
        string pucSubscriptionName);

        public static uint AdsDDDeleteSubscription(IntPtr hDictionary, string pucSubscriptionName)
        {
            return IntPtr.Size == 4 ? AdsDDDeleteSubscription_32(hDictionary, pucSubscriptionName) : AdsDDDeleteSubscription_64(hDictionary, pucSubscriptionName);
        }

        [DllImport("ace32", EntryPoint = "AdsDecryptRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsDecryptRecord_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsDecryptRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsDecryptRecord_64(IntPtr hTable);

        public static uint AdsDecryptRecord(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsDecryptRecord_32(hTable) : AdsDecryptRecord_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDecryptTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDecryptTable_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsDecryptTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDecryptTable_64(IntPtr hTable);

        public static uint AdsDecryptTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsDecryptTable_32(hTable) : AdsDecryptTable_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDeleteCustomKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteCustomKey_32(IntPtr hIndex);

        [DllImport("ace64", EntryPoint = "AdsDeleteCustomKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteCustomKey_64(IntPtr hIndex);

        public static uint AdsDeleteCustomKey(IntPtr hIndex)
        {
            return IntPtr.Size == 4 ? AdsDeleteCustomKey_32(hIndex) : AdsDeleteCustomKey_64(hIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsDeleteIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteIndex_32(IntPtr hIndex);

        [DllImport("ace64", EntryPoint = "AdsDeleteIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteIndex_64(IntPtr hIndex);

        public static uint AdsDeleteIndex(IntPtr hIndex)
        {
            return IntPtr.Size == 4 ? AdsDeleteIndex_32(hIndex) : AdsDeleteIndex_64(hIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsDeleteRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteRecord_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsDeleteRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsDeleteRecord_64(IntPtr hTable);

        public static uint AdsDeleteRecord(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsDeleteRecord_32(hTable) : AdsDeleteRecord_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsGetKeyColumn", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyColumn_32(
        IntPtr hCursor,
        [In, Out] char[] pucKeyColumn,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetKeyColumn", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyColumn_64(
        IntPtr hCursor,
        [In, Out] char[] pucKeyColumn,
        ref ushort pusLen);

        public static uint AdsGetKeyColumn(IntPtr hCursor, char[] pucKeyColumn, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetKeyColumn_32(hCursor, pucKeyColumn, ref pusLen) : AdsGetKeyColumn_64(hCursor, pucKeyColumn, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetKeyFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyFilter_32(
        IntPtr hTable,
        string pucValuesTable,
        uint ulOptions,
        [In, Out] char[] pucFilter,
        ref uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetKeyFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyFilter_64(
        IntPtr hTable,
        string pucValuesTable,
        uint ulOptions,
        [In, Out] char[] pucFilter,
        ref uint pulLength);

        public static uint AdsGetKeyFilter(
        IntPtr hTable,
        string pucValuesTable,
        uint ulOptions,
        char[] pucFilter,
        ref uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetKeyFilter_32(hTable, pucValuesTable, ulOptions, pucFilter, ref pulLength) : AdsGetKeyFilter_64(hTable, pucValuesTable, ulOptions, pucFilter, ref pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsDisableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableEncryption_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsDisableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableEncryption_64(IntPtr hTable);

        public static uint AdsDisableEncryption(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsDisableEncryption_32(hTable) : AdsDisableEncryption_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDisableLocalConnections", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableLocalConnections_32();

        [DllImport("ace64", EntryPoint = "AdsDisableLocalConnections", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableLocalConnections_64();

        public static uint AdsDisableLocalConnections()
        {
            return IntPtr.Size == 4 ? AdsDisableLocalConnections_32() : AdsDisableLocalConnections_64();
        }

        [DllImport("ace32", EntryPoint = "AdsDisconnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisconnect_32(IntPtr hConnect);

        [DllImport("ace64", EntryPoint = "AdsDisconnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisconnect_64(IntPtr hConnect);

        public static uint AdsDisconnect(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsDisconnect_32(hConnect) : AdsDisconnect_64(hConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsEnableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableEncryption_32(IntPtr hTable, string pucPassword);

        [DllImport("ace64", EntryPoint = "AdsEnableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableEncryption_64(IntPtr hTable, string pucPassword);

        public static uint AdsEnableEncryption(IntPtr hTable, string pucPassword)
        {
            return IntPtr.Size == 4 ? AdsEnableEncryption_32(hTable, pucPassword) : AdsEnableEncryption_64(hTable, pucPassword);
        }

        [DllImport("ace32", EntryPoint = "AdsEncryptRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsEncryptRecord_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsEncryptRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsEncryptRecord_64(IntPtr hTable);

        public static uint AdsEncryptRecord(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsEncryptRecord_32(hTable) : AdsEncryptRecord_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsEncryptTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsEncryptTable_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsEncryptTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsEncryptTable_64(IntPtr hTable);

        public static uint AdsEncryptTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsEncryptTable_32(hTable) : AdsEncryptTable_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsEvalLogicalExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalLogicalExpr_32(
        IntPtr hTable,
        string pucExpr,
        out ushort pbResult);

        [DllImport("ace64", EntryPoint = "AdsEvalLogicalExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalLogicalExpr_64(
        IntPtr hTable,
        string pucExpr,
        out ushort pbResult);

        public static uint AdsEvalLogicalExpr(IntPtr hTable, string pucExpr, out ushort pbResult)
        {
            return IntPtr.Size == 4 ? AdsEvalLogicalExpr_32(hTable, pucExpr, out pbResult) : AdsEvalLogicalExpr_64(hTable, pucExpr, out pbResult);
        }

        [DllImport("ace32", EntryPoint = "AdsEvalLogicalExprW", CharSet = CharSet.Unicode)]
        private static extern uint AdsEvalLogicalExprW_32(
        IntPtr hTable,
        string pwcExpr,
        out ushort pbResult);

        [DllImport("ace64", EntryPoint = "AdsEvalLogicalExprW", CharSet = CharSet.Unicode)]
        private static extern uint AdsEvalLogicalExprW_64(
        IntPtr hTable,
        string pwcExpr,
        out ushort pbResult);

        public static uint AdsEvalLogicalExprW(IntPtr hTable, string pwcExpr, out ushort pbResult)
        {
            return IntPtr.Size == 4 ? AdsEvalLogicalExprW_32(hTable, pwcExpr, out pbResult) : AdsEvalLogicalExprW_64(hTable, pwcExpr, out pbResult);
        }

        [DllImport("ace32", EntryPoint = "AdsEvalNumericExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalNumericExpr_32(
        IntPtr hTable,
        string pucExpr,
        out double pdResult);

        [DllImport("ace64", EntryPoint = "AdsEvalNumericExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalNumericExpr_64(
        IntPtr hTable,
        string pucExpr,
        out double pdResult);

        public static uint AdsEvalNumericExpr(IntPtr hTable, string pucExpr, out double pdResult)
        {
            return IntPtr.Size == 4 ? AdsEvalNumericExpr_32(hTable, pucExpr, out pdResult) : AdsEvalNumericExpr_64(hTable, pucExpr, out pdResult);
        }

        [DllImport("ace32", EntryPoint = "AdsEvalStringExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalStringExpr_32(
        IntPtr hTable,
        string pucExpr,
        [In, Out] char[] pucResult,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsEvalStringExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalStringExpr_64(
        IntPtr hTable,
        string pucExpr,
        [In, Out] char[] pucResult,
        ref ushort pusLen);

        public static uint AdsEvalStringExpr(
        IntPtr hTable,
        string pucExpr,
        char[] pucResult,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsEvalStringExpr_32(hTable, pucExpr, pucResult, ref pusLen) : AdsEvalStringExpr_64(hTable, pucExpr, pucResult, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsEvalTestExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalTestExpr_32(
        IntPtr hTable,
        string pucExpr,
        out ushort pusType);

        [DllImport("ace64", EntryPoint = "AdsEvalTestExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalTestExpr_64(
        IntPtr hTable,
        string pucExpr,
        out ushort pusType);

        public static uint AdsEvalTestExpr(IntPtr hTable, string pucExpr, out ushort pusType)
        {
            return IntPtr.Size == 4 ? AdsEvalTestExpr_32(hTable, pucExpr, out pusType) : AdsEvalTestExpr_64(hTable, pucExpr, out pusType);
        }

        [DllImport("ace32", EntryPoint = "AdsExtractKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsExtractKey_32(IntPtr hIndex, [In, Out] char[] pucKey, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsExtractKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsExtractKey_64(IntPtr hIndex, [In, Out] char[] pucKey, ref ushort pusLen);

        public static uint AdsExtractKey(IntPtr hIndex, char[] pucKey, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsExtractKey_32(hIndex, pucKey, ref pusLen) : AdsExtractKey_64(hIndex, pucKey, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsFailedTransactionRecovery", CharSet = CharSet.Ansi)]
        private static extern uint AdsFailedTransactionRecovery_32(string pucServer);

        [DllImport("ace64", EntryPoint = "AdsFailedTransactionRecovery", CharSet = CharSet.Ansi)]
        private static extern uint AdsFailedTransactionRecovery_64(string pucServer);

        public static uint AdsFailedTransactionRecovery(string pucServer)
        {
            return IntPtr.Size == 4 ? AdsFailedTransactionRecovery_32(pucServer) : AdsFailedTransactionRecovery_64(pucServer);
        }

        [DllImport("ace32", EntryPoint = "AdsFileToBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsFileToBinary_32(
        IntPtr hTable,
        string pucFldName,
        ushort usBinaryType,
        string pucFileName);

        [DllImport("ace64", EntryPoint = "AdsFileToBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsFileToBinary_64(
        IntPtr hTable,
        string pucFldName,
        ushort usBinaryType,
        string pucFileName);

        public static uint AdsFileToBinary(
        IntPtr hTable,
        string pucFldName,
        ushort usBinaryType,
        string pucFileName)
        {
            return IntPtr.Size == 4 ? AdsFileToBinary_32(hTable, pucFldName, usBinaryType, pucFileName) : AdsFileToBinary_64(hTable, pucFldName, usBinaryType, pucFileName);
        }

        [DllImport("ace32", EntryPoint = "AdsFileToBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsFileToBinary_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        string pucFileName);

        [DllImport("ace64", EntryPoint = "AdsFileToBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsFileToBinary_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        string pucFileName);

        public static uint AdsFileToBinary(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        string pucFileName)
        {
            return IntPtr.Size == 4 ? AdsFileToBinary_32(hTable, lFieldOrdinal, usBinaryType, pucFileName) : AdsFileToBinary_64(hTable, lFieldOrdinal, usBinaryType, pucFileName);
        }

        [DllImport("ace32", EntryPoint = "AdsFindConnection", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindConnection_32(string pucServerName, out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsFindConnection", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindConnection_64(string pucServerName, out IntPtr phConnect);

        public static uint AdsFindConnection(string pucServerName, out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsFindConnection_32(pucServerName, out phConnect) : AdsFindConnection_64(pucServerName, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsFindConnection25", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindConnection25_32(string pucFullPath, out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsFindConnection25", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindConnection25_64(string pucFullPath, out IntPtr phConnect);

        public static uint AdsFindConnection25(string pucFullPath, out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsFindConnection25_32(pucFullPath, out phConnect) : AdsFindConnection25_64(pucFullPath, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsFindClose", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindClose_32(IntPtr hConnect, IntPtr lHandle);

        [DllImport("ace64", EntryPoint = "AdsFindClose", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindClose_64(IntPtr hConnect, IntPtr lHandle);

        public static uint AdsFindClose(IntPtr hConnect, IntPtr lHandle)
        {
            return IntPtr.Size == 4 ? AdsFindClose_32(hConnect, lHandle) : AdsFindClose_64(hConnect, lHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsFindFirstTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindFirstTable_32(
        IntPtr hConnect,
        string pucFileMask,
        [In, Out] char[] pucFirstFile,
        ref ushort pusFileLen,
        out IntPtr plHandle);

        [DllImport("ace64", EntryPoint = "AdsFindFirstTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindFirstTable_64(
        IntPtr hConnect,
        string pucFileMask,
        [In, Out] char[] pucFirstFile,
        ref ushort pusFileLen,
        out IntPtr plHandle);

        public static uint AdsFindFirstTable(
        IntPtr hConnect,
        string pucFileMask,
        char[] pucFirstFile,
        ref ushort pusFileLen,
        out IntPtr plHandle)
        {
            return IntPtr.Size == 4 ? AdsFindFirstTable_32(hConnect, pucFileMask, pucFirstFile, ref pusFileLen, out plHandle) : AdsFindFirstTable_64(hConnect, pucFileMask, pucFirstFile, ref pusFileLen, out plHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsFindNextTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindNextTable_32(
        IntPtr hConnect,
        IntPtr lHandle,
        [In, Out] char[] pucFileName,
        ref ushort pusFileLen);

        [DllImport("ace64", EntryPoint = "AdsFindNextTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindNextTable_64(
        IntPtr hConnect,
        IntPtr lHandle,
        [In, Out] char[] pucFileName,
        ref ushort pusFileLen);

        public static uint AdsFindNextTable(
        IntPtr hConnect,
        IntPtr lHandle,
        char[] pucFileName,
        ref ushort pusFileLen)
        {
            return IntPtr.Size == 4 ? AdsFindNextTable_32(hConnect, lHandle, pucFileName, ref pusFileLen) : AdsFindNextTable_64(hConnect, lHandle, pucFileName, ref pusFileLen);
        }

        [DllImport("ace32", EntryPoint = "AdsFindFirstTable62", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindFirstTable62_32(
        IntPtr hConnect,
        string pucFileMask,
        [In, Out] char[] pucFirstDD,
        ref ushort pusDDLen,
        [In, Out] char[] pucFirstFile,
        ref ushort pusFileLen,
        out IntPtr plHandle);

        [DllImport("ace64", EntryPoint = "AdsFindFirstTable62", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindFirstTable62_64(
        IntPtr hConnect,
        string pucFileMask,
        [In, Out] char[] pucFirstDD,
        ref ushort pusDDLen,
        [In, Out] char[] pucFirstFile,
        ref ushort pusFileLen,
        out IntPtr plHandle);

        public static uint AdsFindFirstTable62(
        IntPtr hConnect,
        string pucFileMask,
        char[] pucFirstDD,
        ref ushort pusDDLen,
        char[] pucFirstFile,
        ref ushort pusFileLen,
        out IntPtr plHandle)
        {
            return IntPtr.Size == 4 ? AdsFindFirstTable62_32(hConnect, pucFileMask, pucFirstDD, ref pusDDLen, pucFirstFile, ref pusFileLen, out plHandle) : AdsFindFirstTable62_64(hConnect, pucFileMask, pucFirstDD, ref pusDDLen, pucFirstFile, ref pusFileLen, out plHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsFindNextTable62", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindNextTable62_32(
        IntPtr hConnect,
        IntPtr lHandle,
        [In, Out] char[] pucDDName,
        ref ushort pusDDLen,
        [In, Out] char[] pucFileName,
        ref ushort pusFileLen);

        [DllImport("ace64", EntryPoint = "AdsFindNextTable62", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindNextTable62_64(
        IntPtr hConnect,
        IntPtr lHandle,
        [In, Out] char[] pucDDName,
        ref ushort pusDDLen,
        [In, Out] char[] pucFileName,
        ref ushort pusFileLen);

        public static uint AdsFindNextTable62(
        IntPtr hConnect,
        IntPtr lHandle,
        char[] pucDDName,
        ref ushort pusDDLen,
        char[] pucFileName,
        ref ushort pusFileLen)
        {
            return IntPtr.Size == 4 ? AdsFindNextTable62_32(hConnect, lHandle, pucDDName, ref pusDDLen, pucFileName, ref pusFileLen) : AdsFindNextTable62_64(hConnect, lHandle, pucDDName, ref pusDDLen, pucFileName, ref pusFileLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetAllIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAllIndexes_32(
        IntPtr hTable,
        [In, Out] IntPtr[] ahIndex,
        ref ushort pusArrayLen);

        [DllImport("ace64", EntryPoint = "AdsGetAllIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAllIndexes_64(
        IntPtr hTable,
        [In, Out] IntPtr[] ahIndex,
        ref ushort pusArrayLen);

        public static uint AdsGetAllIndexes(IntPtr hTable, IntPtr[] ahIndex, ref ushort pusArrayLen)
        {
            return IntPtr.Size == 4 ? AdsGetAllIndexes_32(hTable, ahIndex, ref pusArrayLen) : AdsGetAllIndexes_64(hTable, ahIndex, ref pusArrayLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFTSIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFTSIndexes_32(
        IntPtr hTable,
        [In, Out] IntPtr[] ahIndex,
        ref ushort pusArrayLen);

        [DllImport("ace64", EntryPoint = "AdsGetFTSIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFTSIndexes_64(
        IntPtr hTable,
        [In, Out] IntPtr[] ahIndex,
        ref ushort pusArrayLen);

        public static uint AdsGetFTSIndexes(IntPtr hTable, IntPtr[] ahIndex, ref ushort pusArrayLen)
        {
            return IntPtr.Size == 4 ? AdsGetFTSIndexes_32(hTable, ahIndex, ref pusArrayLen) : AdsGetFTSIndexes_64(hTable, ahIndex, ref pusArrayLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetAllLocks", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAllLocks_32(
        IntPtr hTable,
        [In, Out] uint[] aulLocks,
        ref ushort pusArrayLen);

        [DllImport("ace64", EntryPoint = "AdsGetAllLocks", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAllLocks_64(
        IntPtr hTable,
        [In, Out] uint[] aulLocks,
        ref ushort pusArrayLen);

        public static uint AdsGetAllLocks(IntPtr hTable, uint[] aulLocks, ref ushort pusArrayLen)
        {
            return IntPtr.Size == 4 ? AdsGetAllLocks_32(hTable, aulLocks, ref pusArrayLen) : AdsGetAllLocks_64(hTable, aulLocks, ref pusArrayLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetAllTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAllTables_32([In, Out] IntPtr[] ahTable, ref ushort pusArrayLen);

        [DllImport("ace64", EntryPoint = "AdsGetAllTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAllTables_64([In, Out] IntPtr[] ahTable, ref ushort pusArrayLen);

        public static uint AdsGetAllTables(IntPtr[] ahTable, ref ushort pusArrayLen)
        {
            return IntPtr.Size == 4 ? AdsGetAllTables_32(ahTable, ref pusArrayLen) : AdsGetAllTables_64(ahTable, ref pusArrayLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinary_32(
        IntPtr hTable,
        string pucFldName,
        uint ulOffset,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        [DllImport("ace64", EntryPoint = "AdsGetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinary_64(
        IntPtr hTable,
        string pucFldName,
        uint ulOffset,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        public static uint AdsGetBinary(
        IntPtr hTable,
        string pucFldName,
        uint ulOffset,
        byte[] pucBuf,
        ref uint pulLen)
        {
            return IntPtr.Size == 4 ? AdsGetBinary_32(hTable, pucFldName, ulOffset, pucBuf, ref pulLen) : AdsGetBinary_64(hTable, pucFldName, ulOffset, pucBuf, ref pulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinary_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOffset,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        [DllImport("ace64", EntryPoint = "AdsGetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinary_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOffset,
        [In, Out] byte[] pucBuf,
        ref uint pulLen);

        public static uint AdsGetBinary(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOffset,
        byte[] pucBuf,
        ref uint pulLen)
        {
            return IntPtr.Size == 4 ? AdsGetBinary_32(hTable, lFieldOrdinal, ulOffset, pucBuf, ref pulLen) : AdsGetBinary_64(hTable, lFieldOrdinal, ulOffset, pucBuf, ref pulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetBinaryLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinaryLength_32(
        IntPtr hTable,
        string pucFldName,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetBinaryLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinaryLength_64(
        IntPtr hTable,
        string pucFldName,
        out uint pulLength);

        public static uint AdsGetBinaryLength(IntPtr hTable, string pucFldName, out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetBinaryLength_32(hTable, pucFldName, out pulLength) : AdsGetBinaryLength_64(hTable, pucFldName, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetBinaryLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinaryLength_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetBinaryLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBinaryLength_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulLength);

        public static uint AdsGetBinaryLength(IntPtr hTable, uint lFieldOrdinal, out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetBinaryLength_32(hTable, lFieldOrdinal, out pulLength) : AdsGetBinaryLength_64(hTable, lFieldOrdinal, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetBookmark", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBookmark_32(IntPtr hTable, out IntPtr phBookmark);

        [DllImport("ace64", EntryPoint = "AdsGetBookmark", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBookmark_64(IntPtr hTable, out IntPtr phBookmark);

        public static uint AdsGetBookmark(IntPtr hTable, out IntPtr phBookmark)
        {
            return IntPtr.Size == 4 ? AdsGetBookmark_32(hTable, out phBookmark) : AdsGetBookmark_64(hTable, out phBookmark);
        }

        [DllImport("ace32", EntryPoint = "AdsGetBookmark60", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBookmark60_32(
        IntPtr hObj,
        [In, Out] char[] pucBookmark,
        ref uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetBookmark60", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBookmark60_64(
        IntPtr hObj,
        [In, Out] char[] pucBookmark,
        ref uint pulLength);

        public static uint AdsGetBookmark60(IntPtr hObj, char[] pucBookmark, ref uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetBookmark60_32(hObj, pucBookmark, ref pulLength) : AdsGetBookmark60_64(hObj, pucBookmark, ref pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetBookmarkLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBookmarkLength_32(IntPtr hObj, ref uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetBookmarkLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetBookmarkLength_64(IntPtr hObj, ref uint pulLength);

        public static uint AdsGetBookmarkLength(IntPtr hObj, ref uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetBookmarkLength_32(hObj, ref pulLength) : AdsGetBookmarkLength_64(hObj, ref pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsCompareBookmarks", CharSet = CharSet.Ansi)]
        private static extern uint AdsCompareBookmarks_32(
        string pucBookmark1,
        string pucBookmark2,
        out int plResult);

        [DllImport("ace64", EntryPoint = "AdsCompareBookmarks", CharSet = CharSet.Ansi)]
        private static extern uint AdsCompareBookmarks_64(
        string pucBookmark1,
        string pucBookmark2,
        out int plResult);

        public static uint AdsCompareBookmarks(
        string pucBookmark1,
        string pucBookmark2,
        out int plResult)
        {
            return IntPtr.Size == 4 ? AdsCompareBookmarks_32(pucBookmark1, pucBookmark2, out plResult) : AdsCompareBookmarks_64(pucBookmark1, pucBookmark2, out plResult);
        }

        [DllImport("ace32", EntryPoint = "AdsGetCollationLang", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetCollationLang_32([In, Out] char[] pucLang, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetCollationLang", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetCollationLang_64([In, Out] char[] pucLang, ref ushort pusLen);

        public static uint AdsGetCollationLang(char[] pucLang, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetCollationLang_32(pucLang, ref pusLen) : AdsGetCollationLang_64(pucLang, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetCollation_32(
        IntPtr hConnect,
        [In, Out] char[] pucCollation,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetCollation_64(
        IntPtr hConnect,
        [In, Out] char[] pucCollation,
        ref ushort pusLen);

        public static uint AdsGetCollation(IntPtr hConnect, char[] pucCollation, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetCollation_32(hConnect, pucCollation, ref pusLen) : AdsGetCollation_64(hConnect, pucCollation, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIntProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIntProperty_32(
        IntPtr hObj,
        uint ulPropertyID,
        out uint pulProperty);

        [DllImport("ace64", EntryPoint = "AdsGetIntProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIntProperty_64(
        IntPtr hObj,
        uint ulPropertyID,
        out uint pulProperty);

        public static uint AdsGetIntProperty(IntPtr hObj, uint ulPropertyID, out uint pulProperty)
        {
            return IntPtr.Size == 4 ? AdsGetIntProperty_32(hObj, ulPropertyID, out pulProperty) : AdsGetIntProperty_64(hObj, ulPropertyID, out pulProperty);
        }

        [DllImport("ace32", EntryPoint = "AdsGetConnectionType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetConnectionType_32(IntPtr hConnect, out ushort pusConnectType);

        [DllImport("ace64", EntryPoint = "AdsGetConnectionType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetConnectionType_64(IntPtr hConnect, out ushort pusConnectType);

        public static uint AdsGetConnectionType(IntPtr hConnect, out ushort pusConnectType)
        {
            return IntPtr.Size == 4 ? AdsGetConnectionType_32(hConnect, out pusConnectType) : AdsGetConnectionType_64(hConnect, out pusConnectType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTransactionCount", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTransactionCount_32(
        IntPtr hConnect,
        out uint pulTransactionCount);

        [DllImport("ace64", EntryPoint = "AdsGetTransactionCount", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTransactionCount_64(
        IntPtr hConnect,
        out uint pulTransactionCount);

        public static uint AdsGetTransactionCount(IntPtr hConnect, out uint pulTransactionCount)
        {
            return IntPtr.Size == 4 ? AdsGetTransactionCount_32(hConnect, out pulTransactionCount) : AdsGetTransactionCount_64(hConnect, out pulTransactionCount);
        }

        [DllImport("ace32", EntryPoint = "AdsGetConnectionPath", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetConnectionPath_32(
        IntPtr hConnect,
        [In, Out] char[] pucConnectionPath,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetConnectionPath", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetConnectionPath_64(
        IntPtr hConnect,
        [In, Out] char[] pucConnectionPath,
        ref ushort pusLen);

        public static uint AdsGetConnectionPath(
        IntPtr hConnect,
        char[] pucConnectionPath,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetConnectionPath_32(hConnect, pucConnectionPath, ref pusLen) : AdsGetConnectionPath_64(hConnect, pucConnectionPath, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetConnectionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetConnectionProperty_32(
        IntPtr hConnect,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref uint pulPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsGetConnectionProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetConnectionProperty_64(
        IntPtr hConnect,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ref uint pulPropertyLen);

        public static uint AdsGetConnectionProperty(
        IntPtr hConnect,
        ushort usPropertyID,
        byte[] pvProperty,
        ref uint pulPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsGetConnectionProperty_32(hConnect, usPropertyID, pvProperty, ref pulPropertyLen) : AdsGetConnectionProperty_64(hConnect, usPropertyID, pvProperty, ref pulPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDate_32(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDate_64(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        public static uint AdsGetDate(
        IntPtr hTable,
        string pucFldName,
        char[] pucBuf,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetDate_32(hTable, pucFldName, pucBuf, ref pusLen) : AdsGetDate_64(hTable, pucFldName, pucBuf, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDate_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDate_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        public static uint AdsGetDate(
        IntPtr hTable,
        uint lFieldOrdinal,
        char[] pucBuf,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetDate_32(hTable, lFieldOrdinal, pucBuf, ref pusLen) : AdsGetDate_64(hTable, lFieldOrdinal, pucBuf, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDateFormat", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDateFormat_32([In, Out] char[] pucFormat, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetDateFormat", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDateFormat_64([In, Out] char[] pucFormat, ref ushort pusLen);

        public static uint AdsGetDateFormat(char[] pucFormat, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetDateFormat_32(pucFormat, ref pusLen) : AdsGetDateFormat_64(pucFormat, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDateFormat60", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDateFormat60_32(
        IntPtr hConnect,
        [In, Out] char[] pucFormat,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetDateFormat60", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDateFormat60_64(
        IntPtr hConnect,
        [In, Out] char[] pucFormat,
        ref ushort pusLen);

        public static uint AdsGetDateFormat60(IntPtr hConnect, char[] pucFormat, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetDateFormat60_32(hConnect, pucFormat, ref pusLen) : AdsGetDateFormat60_64(hConnect, pucFormat, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDecimals_32(out ushort pusDecimals);

        [DllImport("ace64", EntryPoint = "AdsGetDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDecimals_64(out ushort pusDecimals);

        public static uint AdsGetDecimals(out ushort pusDecimals)
        {
            return IntPtr.Size == 4 ? AdsGetDecimals_32(out pusDecimals) : AdsGetDecimals_64(out pusDecimals);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDefault", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDefault_32([In, Out] char[] pucDefault, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetDefault", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDefault_64([In, Out] char[] pucDefault, ref ushort pusLen);

        public static uint AdsGetDefault(char[] pucDefault, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetDefault_32(pucDefault, ref pusLen) : AdsGetDefault_64(pucDefault, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDeleted", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDeleted_32(out ushort pbUseDeleted);

        [DllImport("ace64", EntryPoint = "AdsGetDeleted", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDeleted_64(out ushort pbUseDeleted);

        public static uint AdsGetDeleted(out ushort pbUseDeleted)
        {
            return IntPtr.Size == 4 ? AdsGetDeleted_32(out pbUseDeleted) : AdsGetDeleted_64(out pbUseDeleted);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDouble_32(
        IntPtr hTable,
        string pucFldName,
        out double pdValue);

        [DllImport("ace64", EntryPoint = "AdsGetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDouble_64(
        IntPtr hTable,
        string pucFldName,
        out double pdValue);

        public static uint AdsGetDouble(IntPtr hTable, string pucFldName, out double pdValue)
        {
            return IntPtr.Size == 4 ? AdsGetDouble_32(hTable, pucFldName, out pdValue) : AdsGetDouble_64(hTable, pucFldName, out pdValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDouble_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out double pdValue);

        [DllImport("ace64", EntryPoint = "AdsGetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDouble_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out double pdValue);

        public static uint AdsGetDouble(IntPtr hTable, uint lFieldOrdinal, out double pdValue)
        {
            return IntPtr.Size == 4 ? AdsGetDouble_32(hTable, lFieldOrdinal, out pdValue) : AdsGetDouble_64(hTable, lFieldOrdinal, out pdValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetEpoch", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetEpoch_32(out ushort pusCentury);

        [DllImport("ace64", EntryPoint = "AdsGetEpoch", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetEpoch_64(out ushort pusCentury);

        public static uint AdsGetEpoch(out ushort pusCentury)
        {
            return IntPtr.Size == 4 ? AdsGetEpoch_32(out pusCentury) : AdsGetEpoch_64(out pusCentury);
        }

        [DllImport("ace32", EntryPoint = "AdsGetErrorString", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetErrorString_32(
        uint ulErrCode,
        [In, Out] char[] pucBuf,
        ref ushort pusBufLen);

        [DllImport("ace64", EntryPoint = "AdsGetErrorString", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetErrorString_64(
        uint ulErrCode,
        [In, Out] char[] pucBuf,
        ref ushort pusBufLen);

        public static uint AdsGetErrorString(uint ulErrCode, char[] pucBuf, ref ushort pusBufLen)
        {
            return IntPtr.Size == 4 ? AdsGetErrorString_32(ulErrCode, pucBuf, ref pusBufLen) : AdsGetErrorString_64(ulErrCode, pucBuf, ref pusBufLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetExact", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetExact_32(out ushort pbExact);

        [DllImport("ace64", EntryPoint = "AdsGetExact", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetExact_64(out ushort pbExact);

        public static uint AdsGetExact(out ushort pbExact)
        {
            return IntPtr.Size == 4 ? AdsGetExact_32(out pbExact) : AdsGetExact_64(out pbExact);
        }

        [DllImport("ace32", EntryPoint = "AdsGetExact22", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetExact22_32(IntPtr hObj, out ushort pbExact);

        [DllImport("ace64", EntryPoint = "AdsGetExact22", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetExact22_64(IntPtr hObj, out ushort pbExact);

        public static uint AdsGetExact22(IntPtr hObj, out ushort pbExact)
        {
            return IntPtr.Size == 4 ? AdsGetExact22_32(hObj, out pbExact) : AdsGetExact22_64(hObj, out pbExact);
        }

        [DllImport("ace32", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_32(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_64(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetField(
        IntPtr hTable,
        string pucFldName,
        char[] pucBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetField_32(hTable, pucFldName, pucBuf, ref pulLen, usOption) : AdsGetField_64(hTable, pucFldName, pucBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetField(
        IntPtr hTable,
        uint lFieldOrdinal,
        char[] pucBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetField_32(hTable, lFieldOrdinal, pucBuf, ref pulLen, usOption) : AdsGetField_64(hTable, lFieldOrdinal, pucBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_32(
        IntPtr hTable,
        string pucFldName,
        [In, Out] byte[] abBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_64(
        IntPtr hTable,
        string pucFldName,
        [In, Out] byte[] abBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetField(
        IntPtr hTable,
        string pucFldName,
        byte[] abBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetField_32(hTable, pucFldName, abBuf, ref pulLen, usOption) : AdsGetField_64(hTable, pucFldName, abBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] byte[] abBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetField_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] byte[] abBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetField(
        IntPtr hTable,
        uint lFieldOrdinal,
        byte[] abBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetField_32(hTable, lFieldOrdinal, abBuf, ref pulLen, usOption) : AdsGetField_64(hTable, lFieldOrdinal, abBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetFieldW_32(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetFieldW_64(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetFieldW(
        IntPtr hObj,
        string pucFldName,
        char[] pwcBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetFieldW_32(hObj, pucFldName, pwcBuf, ref pulLen, usOption) : AdsGetFieldW_64(hObj, pucFldName, pwcBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetFieldW_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetFieldW_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetFieldW(
        IntPtr hObj,
        uint lFieldOrdinal,
        char[] pwcBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetFieldW_32(hObj, lFieldOrdinal, pwcBuf, ref pulLen, usOption) : AdsGetFieldW_64(hObj, lFieldOrdinal, pwcBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldDecimals_32(
        IntPtr hTable,
        string pucFldName,
        out ushort pusDecimals);

        [DllImport("ace64", EntryPoint = "AdsGetFieldDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldDecimals_64(
        IntPtr hTable,
        string pucFldName,
        out ushort pusDecimals);

        public static uint AdsGetFieldDecimals(
        IntPtr hTable,
        string pucFldName,
        out ushort pusDecimals)
        {
            return IntPtr.Size == 4 ? AdsGetFieldDecimals_32(hTable, pucFldName, out pusDecimals) : AdsGetFieldDecimals_64(hTable, pucFldName, out pusDecimals);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldDecimals_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusDecimals);

        [DllImport("ace64", EntryPoint = "AdsGetFieldDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldDecimals_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusDecimals);

        public static uint AdsGetFieldDecimals(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusDecimals)
        {
            return IntPtr.Size == 4 ? AdsGetFieldDecimals_32(hTable, lFieldOrdinal, out pusDecimals) : AdsGetFieldDecimals_64(hTable, lFieldOrdinal, out pusDecimals);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength_32(
        IntPtr hTable,
        string pucFldName,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetFieldLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength_64(
        IntPtr hTable,
        string pucFldName,
        out uint pulLength);

        public static uint AdsGetFieldLength(IntPtr hTable, string pucFldName, out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetFieldLength_32(hTable, pucFldName, out pulLength) : AdsGetFieldLength_64(hTable, pucFldName, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetFieldLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulLength);

        public static uint AdsGetFieldLength(IntPtr hTable, uint lFieldOrdinal, out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetFieldLength_32(hTable, lFieldOrdinal, out pulLength) : AdsGetFieldLength_64(hTable, lFieldOrdinal, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldName_32(
        IntPtr hTable,
        ushort usFld,
        [In, Out] char[] pucName,
        ref ushort pusBufLen);

        [DllImport("ace64", EntryPoint = "AdsGetFieldName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldName_64(
        IntPtr hTable,
        ushort usFld,
        [In, Out] char[] pucName,
        ref ushort pusBufLen);

        public static uint AdsGetFieldName(
        IntPtr hTable,
        ushort usFld,
        char[] pucName,
        ref ushort pusBufLen)
        {
            return IntPtr.Size == 4 ? AdsGetFieldName_32(hTable, usFld, pucName, ref pusBufLen) : AdsGetFieldName_64(hTable, usFld, pucName, ref pusBufLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldNum_32(
        IntPtr hTable,
        string pucFldName,
        out ushort pusNum);

        [DllImport("ace64", EntryPoint = "AdsGetFieldNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldNum_64(
        IntPtr hTable,
        string pucFldName,
        out ushort pusNum);

        public static uint AdsGetFieldNum(IntPtr hTable, string pucFldName, out ushort pusNum)
        {
            return IntPtr.Size == 4 ? AdsGetFieldNum_32(hTable, pucFldName, out pusNum) : AdsGetFieldNum_64(hTable, pucFldName, out pusNum);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldNum_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusNum);

        [DllImport("ace64", EntryPoint = "AdsGetFieldNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldNum_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusNum);

        public static uint AdsGetFieldNum(IntPtr hTable, uint lFieldOrdinal, out ushort pusNum)
        {
            return IntPtr.Size == 4 ? AdsGetFieldNum_32(hTable, lFieldOrdinal, out pusNum) : AdsGetFieldNum_64(hTable, lFieldOrdinal, out pusNum);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldOffset", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldOffset_32(
        IntPtr hTable,
        string pucFldName,
        out uint pulOffset);

        [DllImport("ace64", EntryPoint = "AdsGetFieldOffset", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldOffset_64(
        IntPtr hTable,
        string pucFldName,
        out uint pulOffset);

        public static uint AdsGetFieldOffset(IntPtr hTable, string pucFldName, out uint pulOffset)
        {
            return IntPtr.Size == 4 ? AdsGetFieldOffset_32(hTable, pucFldName, out pulOffset) : AdsGetFieldOffset_64(hTable, pucFldName, out pulOffset);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldOffset", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldOffset_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulOffset);

        [DllImport("ace64", EntryPoint = "AdsGetFieldOffset", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldOffset_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulOffset);

        public static uint AdsGetFieldOffset(IntPtr hTable, uint lFieldOrdinal, out uint pulOffset)
        {
            return IntPtr.Size == 4 ? AdsGetFieldOffset_32(hTable, lFieldOrdinal, out pulOffset) : AdsGetFieldOffset_64(hTable, lFieldOrdinal, out pulOffset);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldType_32(
        IntPtr hTable,
        string pucFldName,
        out ushort pusType);

        [DllImport("ace64", EntryPoint = "AdsGetFieldType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldType_64(
        IntPtr hTable,
        string pucFldName,
        out ushort pusType);

        public static uint AdsGetFieldType(IntPtr hTable, string pucFldName, out ushort pusType)
        {
            return IntPtr.Size == 4 ? AdsGetFieldType_32(hTable, pucFldName, out pusType) : AdsGetFieldType_64(hTable, pucFldName, out pusType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldType_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusType);

        [DllImport("ace64", EntryPoint = "AdsGetFieldType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldType_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusType);

        public static uint AdsGetFieldType(IntPtr hTable, uint lFieldOrdinal, out ushort pusType)
        {
            return IntPtr.Size == 4 ? AdsGetFieldType_32(hTable, lFieldOrdinal, out pusType) : AdsGetFieldType_64(hTable, lFieldOrdinal, out pusType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFilter_32(IntPtr hTable, [In, Out] char[] pucFilter, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFilter_64(IntPtr hTable, [In, Out] char[] pucFilter, ref ushort pusLen);

        public static uint AdsGetFilter(IntPtr hTable, char[] pucFilter, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetFilter_32(hTable, pucFilter, ref pusLen) : AdsGetFilter_64(hTable, pucFilter, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetHandleLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetHandleLong_32(IntPtr hObj, out uint pulVal);

        [DllImport("ace64", EntryPoint = "AdsGetHandleLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetHandleLong_64(IntPtr hObj, out uint pulVal);

        public static uint AdsGetHandleLong(IntPtr hObj, out uint pulVal)
        {
            return IntPtr.Size == 4 ? AdsGetHandleLong_32(hObj, out pulVal) : AdsGetHandleLong_64(hObj, out pulVal);
        }

        [DllImport("ace32", EntryPoint = "AdsGetHandleType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetHandleType_32(IntPtr hObj, out ushort pusType);

        [DllImport("ace64", EntryPoint = "AdsGetHandleType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetHandleType_64(IntPtr hObj, out ushort pusType);

        public static uint AdsGetHandleType(IntPtr hObj, out ushort pusType)
        {
            return IntPtr.Size == 4 ? AdsGetHandleType_32(hObj, out pusType) : AdsGetHandleType_64(hObj, out pusType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexCondition", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexCondition_32(
        IntPtr hIndex,
        [In, Out] char[] pucExpr,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetIndexCondition", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexCondition_64(
        IntPtr hIndex,
        [In, Out] char[] pucExpr,
        ref ushort pusLen);

        public static uint AdsGetIndexCondition(IntPtr hIndex, char[] pucExpr, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetIndexCondition_32(hIndex, pucExpr, ref pusLen) : AdsGetIndexCondition_64(hIndex, pucExpr, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexExpr_32(IntPtr hIndex, [In, Out] char[] pucExpr, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetIndexExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexExpr_64(IntPtr hIndex, [In, Out] char[] pucExpr, ref ushort pusLen);

        public static uint AdsGetIndexExpr(IntPtr hIndex, char[] pucExpr, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetIndexExpr_32(hIndex, pucExpr, ref pusLen) : AdsGetIndexExpr_64(hIndex, pucExpr, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexFilename", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexFilename_32(
        IntPtr hIndex,
        ushort usOption,
        [In, Out] char[] pucName,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetIndexFilename", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexFilename_64(
        IntPtr hIndex,
        ushort usOption,
        [In, Out] char[] pucName,
        ref ushort pusLen);

        public static uint AdsGetIndexFilename(
        IntPtr hIndex,
        ushort usOption,
        char[] pucName,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetIndexFilename_32(hIndex, usOption, pucName, ref pusLen) : AdsGetIndexFilename_64(hIndex, usOption, pucName, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexHandle_32(
        IntPtr hTable,
        string pucIndexOrder,
        out IntPtr phIndex);

        [DllImport("ace64", EntryPoint = "AdsGetIndexHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexHandle_64(
        IntPtr hTable,
        string pucIndexOrder,
        out IntPtr phIndex);

        public static uint AdsGetIndexHandle(IntPtr hTable, string pucIndexOrder, out IntPtr phIndex)
        {
            return IntPtr.Size == 4 ? AdsGetIndexHandle_32(hTable, pucIndexOrder, out phIndex) : AdsGetIndexHandle_64(hTable, pucIndexOrder, out phIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexHandleByOrder", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexHandleByOrder_32(
        IntPtr hTable,
        ushort usOrderNum,
        out IntPtr phIndex);

        [DllImport("ace64", EntryPoint = "AdsGetIndexHandleByOrder", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexHandleByOrder_64(
        IntPtr hTable,
        ushort usOrderNum,
        out IntPtr phIndex);

        public static uint AdsGetIndexHandleByOrder(
        IntPtr hTable,
        ushort usOrderNum,
        out IntPtr phIndex)
        {
            return IntPtr.Size == 4 ? AdsGetIndexHandleByOrder_32(hTable, usOrderNum, out phIndex) : AdsGetIndexHandleByOrder_64(hTable, usOrderNum, out phIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexHandleByExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexHandleByExpr_32(
        IntPtr hTable,
        string pucExpr,
        uint ulDescending,
        out IntPtr phIndex);

        [DllImport("ace64", EntryPoint = "AdsGetIndexHandleByExpr", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexHandleByExpr_64(
        IntPtr hTable,
        string pucExpr,
        uint ulDescending,
        out IntPtr phIndex);

        public static uint AdsGetIndexHandleByExpr(
        IntPtr hTable,
        string pucExpr,
        uint ulDescending,
        out IntPtr phIndex)
        {
            return IntPtr.Size == 4 ? AdsGetIndexHandleByExpr_32(hTable, pucExpr, ulDescending, out phIndex) : AdsGetIndexHandleByExpr_64(hTable, pucExpr, ulDescending, out phIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexName_32(IntPtr hIndex, [In, Out] char[] pucName, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetIndexName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexName_64(IntPtr hIndex, [In, Out] char[] pucName, ref ushort pusLen);

        public static uint AdsGetIndexName(IntPtr hIndex, char[] pucName, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetIndexName_32(hIndex, pucName, ref pusLen) : AdsGetIndexName_64(hIndex, pucName, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexOrderByHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexOrderByHandle_32(IntPtr hIndex, out ushort pusIndexOrder);

        [DllImport("ace64", EntryPoint = "AdsGetIndexOrderByHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexOrderByHandle_64(IntPtr hIndex, out ushort pusIndexOrder);

        public static uint AdsGetIndexOrderByHandle(IntPtr hIndex, out ushort pusIndexOrder)
        {
            return IntPtr.Size == 4 ? AdsGetIndexOrderByHandle_32(hIndex, out pusIndexOrder) : AdsGetIndexOrderByHandle_64(hIndex, out pusIndexOrder);
        }

        [DllImport("ace32", EntryPoint = "AdsGetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetJulian_32(IntPtr hTable, string pucFldName, out int plDate);

        [DllImport("ace64", EntryPoint = "AdsGetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetJulian_64(IntPtr hTable, string pucFldName, out int plDate);

        public static uint AdsGetJulian(IntPtr hTable, string pucFldName, out int plDate)
        {
            return IntPtr.Size == 4 ? AdsGetJulian_32(hTable, pucFldName, out plDate) : AdsGetJulian_64(hTable, pucFldName, out plDate);
        }

        [DllImport("ace32", EntryPoint = "AdsGetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetJulian_32(IntPtr hTable, uint lFieldOrdinal, out int plDate);

        [DllImport("ace64", EntryPoint = "AdsGetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetJulian_64(IntPtr hTable, uint lFieldOrdinal, out int plDate);

        public static uint AdsGetJulian(IntPtr hTable, uint lFieldOrdinal, out int plDate)
        {
            return IntPtr.Size == 4 ? AdsGetJulian_32(hTable, lFieldOrdinal, out plDate) : AdsGetJulian_64(hTable, lFieldOrdinal, out plDate);
        }

        [DllImport("ace32", EntryPoint = "AdsGetKeyCount", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyCount_32(
        IntPtr hIndex,
        ushort usFilterOption,
        out uint pulCount);

        [DllImport("ace64", EntryPoint = "AdsGetKeyCount", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyCount_64(
        IntPtr hIndex,
        ushort usFilterOption,
        out uint pulCount);

        public static uint AdsGetKeyCount(IntPtr hIndex, ushort usFilterOption, out uint pulCount)
        {
            return IntPtr.Size == 4 ? AdsGetKeyCount_32(hIndex, usFilterOption, out pulCount) : AdsGetKeyCount_64(hIndex, usFilterOption, out pulCount);
        }

        [DllImport("ace32", EntryPoint = "AdsGetKeyNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyNum_32(
        IntPtr hIndex,
        ushort usFilterOption,
        out uint pulKey);

        [DllImport("ace64", EntryPoint = "AdsGetKeyNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyNum_64(
        IntPtr hIndex,
        ushort usFilterOption,
        out uint pulKey);

        public static uint AdsGetKeyNum(IntPtr hIndex, ushort usFilterOption, out uint pulKey)
        {
            return IntPtr.Size == 4 ? AdsGetKeyNum_32(hIndex, usFilterOption, out pulKey) : AdsGetKeyNum_64(hIndex, usFilterOption, out pulKey);
        }

        [DllImport("ace32", EntryPoint = "AdsGetKeyLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyLength_32(IntPtr hIndex, out ushort pusKeyLength);

        [DllImport("ace64", EntryPoint = "AdsGetKeyLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyLength_64(IntPtr hIndex, out ushort pusKeyLength);

        public static uint AdsGetKeyLength(IntPtr hIndex, out ushort pusKeyLength)
        {
            return IntPtr.Size == 4 ? AdsGetKeyLength_32(hIndex, out pusKeyLength) : AdsGetKeyLength_64(hIndex, out pusKeyLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetKeyType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyType_32(IntPtr hIndex, out ushort usKeyType);

        [DllImport("ace64", EntryPoint = "AdsGetKeyType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetKeyType_64(IntPtr hIndex, out ushort usKeyType);

        public static uint AdsGetKeyType(IntPtr hIndex, out ushort usKeyType)
        {
            return IntPtr.Size == 4 ? AdsGetKeyType_32(hIndex, out usKeyType) : AdsGetKeyType_64(hIndex, out usKeyType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLastError", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLastError_32(
        out uint pulErrCode,
        [In, Out] char[] pucBuf,
        ref ushort pusBufLen);

        [DllImport("ace64", EntryPoint = "AdsGetLastError", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLastError_64(
        out uint pulErrCode,
        [In, Out] char[] pucBuf,
        ref ushort pusBufLen);

        public static uint AdsGetLastError(out uint pulErrCode, char[] pucBuf, ref ushort pusBufLen)
        {
            return IntPtr.Size == 4 ? AdsGetLastError_32(out pulErrCode, pucBuf, ref pusBufLen) : AdsGetLastError_64(out pulErrCode, pucBuf, ref pusBufLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLastTableUpdate", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLastTableUpdate_32(
        IntPtr hTable,
        [In, Out] char[] pucDate,
        ref ushort pusDateLen);

        [DllImport("ace64", EntryPoint = "AdsGetLastTableUpdate", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLastTableUpdate_64(
        IntPtr hTable,
        [In, Out] char[] pucDate,
        ref ushort pusDateLen);

        public static uint AdsGetLastTableUpdate(IntPtr hTable, char[] pucDate, ref ushort pusDateLen)
        {
            return IntPtr.Size == 4 ? AdsGetLastTableUpdate_32(hTable, pucDate, ref pusDateLen) : AdsGetLastTableUpdate_64(hTable, pucDate, ref pusDateLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLogical_32(
        IntPtr hTable,
        string pucFldName,
        out ushort pbValue);

        [DllImport("ace64", EntryPoint = "AdsGetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLogical_64(
        IntPtr hTable,
        string pucFldName,
        out ushort pbValue);

        public static uint AdsGetLogical(IntPtr hTable, string pucFldName, out ushort pbValue)
        {
            return IntPtr.Size == 4 ? AdsGetLogical_32(hTable, pucFldName, out pbValue) : AdsGetLogical_64(hTable, pucFldName, out pbValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLogical_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pbValue);

        [DllImport("ace64", EntryPoint = "AdsGetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLogical_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pbValue);

        public static uint AdsGetLogical(IntPtr hTable, uint lFieldOrdinal, out ushort pbValue)
        {
            return IntPtr.Size == 4 ? AdsGetLogical_32(hTable, lFieldOrdinal, out pbValue) : AdsGetLogical_64(hTable, lFieldOrdinal, out pbValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLong_32(IntPtr hTable, string pucFldName, out int plValue);

        [DllImport("ace64", EntryPoint = "AdsGetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLong_64(IntPtr hTable, string pucFldName, out int plValue);

        public static uint AdsGetLong(IntPtr hTable, string pucFldName, out int plValue)
        {
            return IntPtr.Size == 4 ? AdsGetLong_32(hTable, pucFldName, out plValue) : AdsGetLong_64(hTable, pucFldName, out plValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLong_32(IntPtr hTable, uint lFieldOrdinal, out int plValue);

        [DllImport("ace64", EntryPoint = "AdsGetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLong_64(IntPtr hTable, uint lFieldOrdinal, out int plValue);

        public static uint AdsGetLong(IntPtr hTable, uint lFieldOrdinal, out int plValue)
        {
            return IntPtr.Size == 4 ? AdsGetLong_32(hTable, lFieldOrdinal, out plValue) : AdsGetLong_64(hTable, lFieldOrdinal, out plValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLongLong_32(
        IntPtr hTable,
        string pucFldName,
        out long pqValue);

        [DllImport("ace64", EntryPoint = "AdsGetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLongLong_64(
        IntPtr hTable,
        string pucFldName,
        out long pqValue);

        public static uint AdsGetLongLong(IntPtr hTable, string pucFldName, out long pqValue)
        {
            return IntPtr.Size == 4 ? AdsGetLongLong_32(hTable, pucFldName, out pqValue) : AdsGetLongLong_64(hTable, pucFldName, out pqValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLongLong_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out long pqValue);

        [DllImport("ace64", EntryPoint = "AdsGetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLongLong_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out long pqValue);

        public static uint AdsGetLongLong(IntPtr hTable, uint lFieldOrdinal, out long pqValue)
        {
            return IntPtr.Size == 4 ? AdsGetLongLong_32(hTable, lFieldOrdinal, out pqValue) : AdsGetLongLong_64(hTable, lFieldOrdinal, out pqValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMemoBlockSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoBlockSize_32(IntPtr hTable, out ushort pusBlockSize);

        [DllImport("ace64", EntryPoint = "AdsGetMemoBlockSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoBlockSize_64(IntPtr hTable, out ushort pusBlockSize);

        public static uint AdsGetMemoBlockSize(IntPtr hTable, out ushort pusBlockSize)
        {
            return IntPtr.Size == 4 ? AdsGetMemoBlockSize_32(hTable, out pusBlockSize) : AdsGetMemoBlockSize_64(hTable, out pusBlockSize);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMemoLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoLength_32(
        IntPtr hTable,
        string pucFldName,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetMemoLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoLength_64(
        IntPtr hTable,
        string pucFldName,
        out uint pulLength);

        public static uint AdsGetMemoLength(IntPtr hTable, string pucFldName, out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetMemoLength_32(hTable, pucFldName, out pulLength) : AdsGetMemoLength_64(hTable, pucFldName, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMemoLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoLength_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetMemoLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoLength_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out uint pulLength);

        public static uint AdsGetMemoLength(IntPtr hTable, uint lFieldOrdinal, out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetMemoLength_32(hTable, lFieldOrdinal, out pulLength) : AdsGetMemoLength_64(hTable, lFieldOrdinal, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMemoDataType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoDataType_32(
        IntPtr hTable,
        string pucFldName,
        out ushort pusType);

        [DllImport("ace64", EntryPoint = "AdsGetMemoDataType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoDataType_64(
        IntPtr hTable,
        string pucFldName,
        out ushort pusType);

        public static uint AdsGetMemoDataType(IntPtr hTable, string pucFldName, out ushort pusType)
        {
            return IntPtr.Size == 4 ? AdsGetMemoDataType_32(hTable, pucFldName, out pusType) : AdsGetMemoDataType_64(hTable, pucFldName, out pusType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMemoDataType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoDataType_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusType);

        [DllImport("ace64", EntryPoint = "AdsGetMemoDataType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMemoDataType_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pusType);

        public static uint AdsGetMemoDataType(IntPtr hTable, uint lFieldOrdinal, out ushort pusType)
        {
            return IntPtr.Size == 4 ? AdsGetMemoDataType_32(hTable, lFieldOrdinal, out pusType) : AdsGetMemoDataType_64(hTable, lFieldOrdinal, out pusType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMilliseconds_32(
        IntPtr hTable,
        string pucFldName,
        out int plTime);

        [DllImport("ace64", EntryPoint = "AdsGetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMilliseconds_64(
        IntPtr hTable,
        string pucFldName,
        out int plTime);

        public static uint AdsGetMilliseconds(IntPtr hTable, string pucFldName, out int plTime)
        {
            return IntPtr.Size == 4 ? AdsGetMilliseconds_32(hTable, pucFldName, out plTime) : AdsGetMilliseconds_64(hTable, pucFldName, out plTime);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMilliseconds_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out int plTime);

        [DllImport("ace64", EntryPoint = "AdsGetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMilliseconds_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out int plTime);

        public static uint AdsGetMilliseconds(IntPtr hTable, uint lFieldOrdinal, out int plTime)
        {
            return IntPtr.Size == 4 ? AdsGetMilliseconds_32(hTable, lFieldOrdinal, out plTime) : AdsGetMilliseconds_64(hTable, lFieldOrdinal, out plTime);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMoney_32(IntPtr hTbl, string pucFldName, out long pqValue);

        [DllImport("ace64", EntryPoint = "AdsGetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMoney_64(IntPtr hTbl, string pucFldName, out long pqValue);

        public static uint AdsGetMoney(IntPtr hTbl, string pucFldName, out long pqValue)
        {
            return IntPtr.Size == 4 ? AdsGetMoney_32(hTbl, pucFldName, out pqValue) : AdsGetMoney_64(hTbl, pucFldName, out pqValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMoney_32(IntPtr hTbl, uint lFieldOrdinal, out long pqValue);

        [DllImport("ace64", EntryPoint = "AdsGetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetMoney_64(IntPtr hTbl, uint lFieldOrdinal, out long pqValue);

        public static uint AdsGetMoney(IntPtr hTbl, uint lFieldOrdinal, out long pqValue)
        {
            return IntPtr.Size == 4 ? AdsGetMoney_32(hTbl, lFieldOrdinal, out pqValue) : AdsGetMoney_64(hTbl, lFieldOrdinal, out pqValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetActiveLinkInfo", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetActiveLinkInfo_32(
        IntPtr hDBConn,
        ushort usLinkNum,
        [In, Out] char[] pucLinkInfo,
        ref ushort pusBufferLen);

        [DllImport("ace64", EntryPoint = "AdsGetActiveLinkInfo", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetActiveLinkInfo_64(
        IntPtr hDBConn,
        ushort usLinkNum,
        [In, Out] char[] pucLinkInfo,
        ref ushort pusBufferLen);

        public static uint AdsGetActiveLinkInfo(
        IntPtr hDBConn,
        ushort usLinkNum,
        char[] pucLinkInfo,
        ref ushort pusBufferLen)
        {
            return IntPtr.Size == 4 ? AdsGetActiveLinkInfo_32(hDBConn, usLinkNum, pucLinkInfo, ref pusBufferLen) : AdsGetActiveLinkInfo_64(hDBConn, usLinkNum, pucLinkInfo, ref pusBufferLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetNumActiveLinks", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumActiveLinks_32(IntPtr hDBConn, out ushort pusNumLinks);

        [DllImport("ace64", EntryPoint = "AdsGetNumActiveLinks", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumActiveLinks_64(IntPtr hDBConn, out ushort pusNumLinks);

        public static uint AdsGetNumActiveLinks(IntPtr hDBConn, out ushort pusNumLinks)
        {
            return IntPtr.Size == 4 ? AdsGetNumActiveLinks_32(hDBConn, out pusNumLinks) : AdsGetNumActiveLinks_64(hDBConn, out pusNumLinks);
        }

        [DllImport("ace32", EntryPoint = "AdsGetNumFields", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumFields_32(IntPtr hTable, out ushort pusCount);

        [DllImport("ace64", EntryPoint = "AdsGetNumFields", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumFields_64(IntPtr hTable, out ushort pusCount);

        public static uint AdsGetNumFields(IntPtr hTable, out ushort pusCount)
        {
            return IntPtr.Size == 4 ? AdsGetNumFields_32(hTable, out pusCount) : AdsGetNumFields_64(hTable, out pusCount);
        }

        [DllImport("ace32", EntryPoint = "AdsGetNumIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumIndexes_32(IntPtr hTable, out ushort pusNum);

        [DllImport("ace64", EntryPoint = "AdsGetNumIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumIndexes_64(IntPtr hTable, out ushort pusNum);

        public static uint AdsGetNumIndexes(IntPtr hTable, out ushort pusNum)
        {
            return IntPtr.Size == 4 ? AdsGetNumIndexes_32(hTable, out pusNum) : AdsGetNumIndexes_64(hTable, out pusNum);
        }

        [DllImport("ace32", EntryPoint = "AdsGetNumFTSIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumFTSIndexes_32(IntPtr hTable, out ushort pusNum);

        [DllImport("ace64", EntryPoint = "AdsGetNumFTSIndexes", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumFTSIndexes_64(IntPtr hTable, out ushort pusNum);

        public static uint AdsGetNumFTSIndexes(IntPtr hTable, out ushort pusNum)
        {
            return IntPtr.Size == 4 ? AdsGetNumFTSIndexes_32(hTable, out pusNum) : AdsGetNumFTSIndexes_64(hTable, out pusNum);
        }

        [DllImport("ace32", EntryPoint = "AdsGetNumLocks", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumLocks_32(IntPtr hTable, out ushort pusNum);

        [DllImport("ace64", EntryPoint = "AdsGetNumLocks", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumLocks_64(IntPtr hTable, out ushort pusNum);

        public static uint AdsGetNumLocks(IntPtr hTable, out ushort pusNum)
        {
            return IntPtr.Size == 4 ? AdsGetNumLocks_32(hTable, out pusNum) : AdsGetNumLocks_64(hTable, out pusNum);
        }

        [DllImport("ace32", EntryPoint = "AdsGetNumOpenTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumOpenTables_32(out ushort pusNum);

        [DllImport("ace64", EntryPoint = "AdsGetNumOpenTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumOpenTables_64(out ushort pusNum);

        public static uint AdsGetNumOpenTables(out ushort pusNum)
        {
            return IntPtr.Size == 4 ? AdsGetNumOpenTables_32(out pusNum) : AdsGetNumOpenTables_64(out pusNum);
        }

        [DllImport("ace32", EntryPoint = "AdsGetRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecord_32(IntPtr hTable, [In, Out] byte[] pucRec, ref uint pulLen);

        [DllImport("ace64", EntryPoint = "AdsGetRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecord_64(IntPtr hTable, [In, Out] byte[] pucRec, ref uint pulLen);

        public static uint AdsGetRecord(IntPtr hTable, byte[] pucRec, ref uint pulLen)
        {
            return IntPtr.Size == 4 ? AdsGetRecord_32(hTable, pucRec, ref pulLen) : AdsGetRecord_64(hTable, pucRec, ref pulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetRecordCount", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordCount_32(
        IntPtr hTable,
        ushort usFilterOption,
        out uint pulCount);

        [DllImport("ace64", EntryPoint = "AdsGetRecordCount", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordCount_64(
        IntPtr hTable,
        ushort usFilterOption,
        out uint pulCount);

        public static uint AdsGetRecordCount(IntPtr hTable, ushort usFilterOption, out uint pulCount)
        {
            return IntPtr.Size == 4 ? AdsGetRecordCount_32(hTable, usFilterOption, out pulCount) : AdsGetRecordCount_64(hTable, usFilterOption, out pulCount);
        }

        [DllImport("ace32", EntryPoint = "AdsGetRecordNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordNum_32(
        IntPtr hTable,
        ushort usFilterOption,
        out uint pulRec);

        [DllImport("ace64", EntryPoint = "AdsGetRecordNum", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordNum_64(
        IntPtr hTable,
        ushort usFilterOption,
        out uint pulRec);

        public static uint AdsGetRecordNum(IntPtr hTable, ushort usFilterOption, out uint pulRec)
        {
            return IntPtr.Size == 4 ? AdsGetRecordNum_32(hTable, usFilterOption, out pulRec) : AdsGetRecordNum_64(hTable, usFilterOption, out pulRec);
        }

        [DllImport("ace32", EntryPoint = "AdsGetRecordLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordLength_32(IntPtr hTable, out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetRecordLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordLength_64(IntPtr hTable, out uint pulLength);

        public static uint AdsGetRecordLength(IntPtr hTable, out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetRecordLength_32(hTable, out pulLength) : AdsGetRecordLength_64(hTable, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetRecordCRC", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordCRC_32(IntPtr hTable, out uint pulCRC, uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsGetRecordCRC", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRecordCRC_64(IntPtr hTable, out uint pulCRC, uint ulOptions);

        public static uint AdsGetRecordCRC(IntPtr hTable, out uint pulCRC, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsGetRecordCRC_32(hTable, out pulCRC, ulOptions) : AdsGetRecordCRC_64(hTable, out pulCRC, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsGetRelKeyPos", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRelKeyPos_32(IntPtr hIndex, out double pdPos);

        [DllImport("ace64", EntryPoint = "AdsGetRelKeyPos", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetRelKeyPos_64(IntPtr hIndex, out double pdPos);

        public static uint AdsGetRelKeyPos(IntPtr hIndex, out double pdPos)
        {
            return IntPtr.Size == 4 ? AdsGetRelKeyPos_32(hIndex, out pdPos) : AdsGetRelKeyPos_64(hIndex, out pdPos);
        }

        [DllImport("ace32", EntryPoint = "AdsGetScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetScope_32(
        IntPtr hIndex,
        ushort usScopeOption,
        [In, Out] char[] pucScope,
        ref ushort pusBufLen);

        [DllImport("ace64", EntryPoint = "AdsGetScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetScope_64(
        IntPtr hIndex,
        ushort usScopeOption,
        [In, Out] char[] pucScope,
        ref ushort pusBufLen);

        public static uint AdsGetScope(
        IntPtr hIndex,
        ushort usScopeOption,
        char[] pucScope,
        ref ushort pusBufLen)
        {
            return IntPtr.Size == 4 ? AdsGetScope_32(hIndex, usScopeOption, pucScope, ref pusBufLen) : AdsGetScope_64(hIndex, usScopeOption, pucScope, ref pusBufLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetSearchPath", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSearchPath_32([In, Out] char[] pucPath, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetSearchPath", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSearchPath_64([In, Out] char[] pucPath, ref ushort pusLen);

        public static uint AdsGetSearchPath(char[] pucPath, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetSearchPath_32(pucPath, ref pusLen) : AdsGetSearchPath_64(pucPath, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetServerName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetServerName_32(
        IntPtr hConnect,
        [In, Out] char[] pucName,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetServerName", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetServerName_64(
        IntPtr hConnect,
        [In, Out] char[] pucName,
        ref ushort pusLen);

        public static uint AdsGetServerName(IntPtr hConnect, char[] pucName, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetServerName_32(hConnect, pucName, ref pusLen) : AdsGetServerName_64(hConnect, pucName, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetServerTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetServerTime_32(
        IntPtr hConnect,
        [In, Out] char[] pucDateBuf,
        ref ushort pusDateBufLen,
        out int plTime,
        [In, Out] char[] pucTimeBuf,
        ref ushort pusTimeBufLen);

        [DllImport("ace64", EntryPoint = "AdsGetServerTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetServerTime_64(
        IntPtr hConnect,
        [In, Out] char[] pucDateBuf,
        ref ushort pusDateBufLen,
        out int plTime,
        [In, Out] char[] pucTimeBuf,
        ref ushort pusTimeBufLen);

        public static uint AdsGetServerTime(
        IntPtr hConnect,
        char[] pucDateBuf,
        ref ushort pusDateBufLen,
        out int plTime,
        char[] pucTimeBuf,
        ref ushort pusTimeBufLen)
        {
            return IntPtr.Size == 4 ? AdsGetServerTime_32(hConnect, pucDateBuf, ref pusDateBufLen, out plTime, pucTimeBuf, ref pusTimeBufLen) : AdsGetServerTime_64(hConnect, pucDateBuf, ref pusDateBufLen, out plTime, pucTimeBuf, ref pusTimeBufLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetShort_32(IntPtr hTable, string pucFldName, out short psValue);

        [DllImport("ace64", EntryPoint = "AdsGetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetShort_64(IntPtr hTable, string pucFldName, out short psValue);

        public static uint AdsGetShort(IntPtr hTable, string pucFldName, out short psValue)
        {
            return IntPtr.Size == 4 ? AdsGetShort_32(hTable, pucFldName, out psValue) : AdsGetShort_64(hTable, pucFldName, out psValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetShort_32(IntPtr hTable, uint lFieldOrdinal, out short psValue);

        [DllImport("ace64", EntryPoint = "AdsGetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetShort_64(IntPtr hTable, uint lFieldOrdinal, out short psValue);

        public static uint AdsGetShort(IntPtr hTable, uint lFieldOrdinal, out short psValue)
        {
            return IntPtr.Size == 4 ? AdsGetShort_32(hTable, lFieldOrdinal, out psValue) : AdsGetShort_64(hTable, lFieldOrdinal, out psValue);
        }

        [DllImport("ace32", EntryPoint = "AdsGetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetString_32(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetString_64(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetString(
        IntPtr hTable,
        string pucFldName,
        char[] pucBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetString_32(hTable, pucFldName, pucBuf, ref pulLen, usOption) : AdsGetString_64(hTable, pucFldName, pucBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetString_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetString_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetString(
        IntPtr hTable,
        uint lFieldOrdinal,
        char[] pucBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetString_32(hTable, lFieldOrdinal, pucBuf, ref pulLen, usOption) : AdsGetString_64(hTable, lFieldOrdinal, pucBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetStringW_32(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetStringW_64(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetStringW(
        IntPtr hObj,
        string pucFldName,
        char[] pwcBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetStringW_32(hObj, pucFldName, pwcBuf, ref pulLen, usOption) : AdsGetStringW_64(hObj, pucFldName, pwcBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetStringW_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsGetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetStringW_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        [In, Out] char[] pwcBuf,
        ref uint pulLen,
        ushort usOption);

        public static uint AdsGetStringW(
        IntPtr hObj,
        uint lFieldOrdinal,
        char[] pwcBuf,
        ref uint pulLen,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsGetStringW_32(hObj, lFieldOrdinal, pwcBuf, ref pulLen, usOption) : AdsGetStringW_64(hObj, lFieldOrdinal, pwcBuf, ref pulLen, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableAlias", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableAlias_32(
        IntPtr hTable,
        [In, Out] char[] pucAlias,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetTableAlias", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableAlias_64(
        IntPtr hTable,
        [In, Out] char[] pucAlias,
        ref ushort pusLen);

        public static uint AdsGetTableAlias(IntPtr hTable, char[] pucAlias, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetTableAlias_32(hTable, pucAlias, ref pusLen) : AdsGetTableAlias_64(hTable, pucAlias, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableCharType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableCharType_32(IntPtr hTable, out ushort pusCharType);

        [DllImport("ace64", EntryPoint = "AdsGetTableCharType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableCharType_64(IntPtr hTable, out ushort pusCharType);

        public static uint AdsGetTableCharType(IntPtr hTable, out ushort pusCharType)
        {
            return IntPtr.Size == 4 ? AdsGetTableCharType_32(hTable, out pusCharType) : AdsGetTableCharType_64(hTable, out pusCharType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableConnection", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableConnection_32(IntPtr hTable, out IntPtr phConnect);

        [DllImport("ace64", EntryPoint = "AdsGetTableConnection", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableConnection_64(IntPtr hTable, out IntPtr phConnect);

        public static uint AdsGetTableConnection(IntPtr hTable, out IntPtr phConnect)
        {
            return IntPtr.Size == 4 ? AdsGetTableConnection_32(hTable, out phConnect) : AdsGetTableConnection_64(hTable, out phConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableFilename", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableFilename_32(
        IntPtr hTable,
        ushort usOption,
        [In, Out] char[] pucName,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetTableFilename", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableFilename_64(
        IntPtr hTable,
        ushort usOption,
        [In, Out] char[] pucName,
        ref ushort pusLen);

        public static uint AdsGetTableFilename(
        IntPtr hTable,
        ushort usOption,
        char[] pucName,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetTableFilename_32(hTable, usOption, pucName, ref pusLen) : AdsGetTableFilename_64(hTable, usOption, pucName, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableHandle_32(string pucName, out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsGetTableHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableHandle_64(string pucName, out IntPtr phTable);

        public static uint AdsGetTableHandle(string pucName, out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsGetTableHandle_32(pucName, out phTable) : AdsGetTableHandle_64(pucName, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableHandle25", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableHandle25_32(
        IntPtr hConnect,
        string pucName,
        out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsGetTableHandle25", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableHandle25_64(
        IntPtr hConnect,
        string pucName,
        out IntPtr phTable);

        public static uint AdsGetTableHandle25(IntPtr hConnect, string pucName, out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsGetTableHandle25_32(hConnect, pucName, out phTable) : AdsGetTableHandle25_64(hConnect, pucName, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableLockType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableLockType_32(IntPtr hTable, out ushort pusLockType);

        [DllImport("ace64", EntryPoint = "AdsGetTableLockType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableLockType_64(IntPtr hTable, out ushort pusLockType);

        public static uint AdsGetTableLockType(IntPtr hTable, out ushort pusLockType)
        {
            return IntPtr.Size == 4 ? AdsGetTableLockType_32(hTable, out pusLockType) : AdsGetTableLockType_64(hTable, out pusLockType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableMemoSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableMemoSize_32(IntPtr hTable, out ushort pusMemoSize);

        [DllImport("ace64", EntryPoint = "AdsGetTableMemoSize", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableMemoSize_64(IntPtr hTable, out ushort pusMemoSize);

        public static uint AdsGetTableMemoSize(IntPtr hTable, out ushort pusMemoSize)
        {
            return IntPtr.Size == 4 ? AdsGetTableMemoSize_32(hTable, out pusMemoSize) : AdsGetTableMemoSize_64(hTable, out pusMemoSize);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableOpenOptions", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableOpenOptions_32(IntPtr hTable, out uint pulOptions);

        [DllImport("ace64", EntryPoint = "AdsGetTableOpenOptions", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableOpenOptions_64(IntPtr hTable, out uint pulOptions);

        public static uint AdsGetTableOpenOptions(IntPtr hTable, out uint pulOptions)
        {
            return IntPtr.Size == 4 ? AdsGetTableOpenOptions_32(hTable, out pulOptions) : AdsGetTableOpenOptions_64(hTable, out pulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableRights_32(IntPtr hTable, out ushort pusRights);

        [DllImport("ace64", EntryPoint = "AdsGetTableRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableRights_64(IntPtr hTable, out ushort pusRights);

        public static uint AdsGetTableRights(IntPtr hTable, out ushort pusRights)
        {
            return IntPtr.Size == 4 ? AdsGetTableRights_32(hTable, out pusRights) : AdsGetTableRights_64(hTable, out pusRights);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableType_32(IntPtr hTable, out ushort pusType);

        [DllImport("ace64", EntryPoint = "AdsGetTableType", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableType_64(IntPtr hTable, out ushort pusType);

        public static uint AdsGetTableType(IntPtr hTable, out ushort pusType)
        {
            return IntPtr.Size == 4 ? AdsGetTableType_32(hTable, out pusType) : AdsGetTableType_64(hTable, out pusType);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTime_32(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTime_64(
        IntPtr hTable,
        string pucFldName,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        public static uint AdsGetTime(
        IntPtr hTable,
        string pucFldName,
        char[] pucBuf,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetTime_32(hTable, pucFldName, pucBuf, ref pusLen) : AdsGetTime_64(hTable, pucFldName, pucBuf, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTime_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTime_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        [In, Out] char[] pucBuf,
        ref ushort pusLen);

        public static uint AdsGetTime(
        IntPtr hTable,
        uint lFieldOrdinal,
        char[] pucBuf,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetTime_32(hTable, lFieldOrdinal, pucBuf, ref pusLen) : AdsGetTime_64(hTable, lFieldOrdinal, pucBuf, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetVersion", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetVersion_32(
        out uint pulMajor,
        out uint pulMinor,
        string pucLetter,
        [In, Out] char[] pucDesc,
        ref ushort pusDescLen);

        [DllImport("ace64", EntryPoint = "AdsGetVersion", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetVersion_64(
        out uint pulMajor,
        out uint pulMinor,
        string pucLetter,
        [In, Out] char[] pucDesc,
        ref ushort pusDescLen);

        public static uint AdsGetVersion(
        out uint pulMajor,
        out uint pulMinor,
        string pucLetter,
        char[] pucDesc,
        ref ushort pusDescLen)
        {
            return IntPtr.Size == 4 ? AdsGetVersion_32(out pulMajor, out pulMinor, pucLetter, pucDesc, ref pusDescLen) : AdsGetVersion_64(out pulMajor, out pulMinor, pucLetter, pucDesc, ref pusDescLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGotoBookmark", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBookmark_32(IntPtr hTable, IntPtr hBookmark);

        [DllImport("ace64", EntryPoint = "AdsGotoBookmark", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBookmark_64(IntPtr hTable, IntPtr hBookmark);

        public static uint AdsGotoBookmark(IntPtr hTable, IntPtr hBookmark)
        {
            return IntPtr.Size == 4 ? AdsGotoBookmark_32(hTable, hBookmark) : AdsGotoBookmark_64(hTable, hBookmark);
        }

        [DllImport("ace32", EntryPoint = "AdsGotoBookmark60", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBookmark60_32(IntPtr hObj, string pucBookmark);

        [DllImport("ace64", EntryPoint = "AdsGotoBookmark60", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBookmark60_64(IntPtr hObj, string pucBookmark);

        public static uint AdsGotoBookmark60(IntPtr hObj, string pucBookmark)
        {
            return IntPtr.Size == 4 ? AdsGotoBookmark60_32(hObj, pucBookmark) : AdsGotoBookmark60_64(hObj, pucBookmark);
        }

        [DllImport("ace32", EntryPoint = "AdsGotoBottom", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBottom_32(IntPtr hObj);

        [DllImport("ace64", EntryPoint = "AdsGotoBottom", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoBottom_64(IntPtr hObj);

        public static uint AdsGotoBottom(IntPtr hObj)
        {
            return IntPtr.Size == 4 ? AdsGotoBottom_32(hObj) : AdsGotoBottom_64(hObj);
        }

        [DllImport("ace32", EntryPoint = "AdsGotoRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoRecord_32(IntPtr hTable, uint ulRec);

        [DllImport("ace64", EntryPoint = "AdsGotoRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoRecord_64(IntPtr hTable, uint ulRec);

        public static uint AdsGotoRecord(IntPtr hTable, uint ulRec)
        {
            return IntPtr.Size == 4 ? AdsGotoRecord_32(hTable, ulRec) : AdsGotoRecord_64(hTable, ulRec);
        }

        [DllImport("ace32", EntryPoint = "AdsGotoTop", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoTop_32(IntPtr hObj);

        [DllImport("ace64", EntryPoint = "AdsGotoTop", CharSet = CharSet.Ansi)]
        private static extern uint AdsGotoTop_64(IntPtr hObj);

        public static uint AdsGotoTop(IntPtr hObj)
        {
            return IntPtr.Size == 4 ? AdsGotoTop_32(hObj) : AdsGotoTop_64(hObj);
        }

        [DllImport("ace32", EntryPoint = "AdsImageToClipboard", CharSet = CharSet.Ansi)]
        private static extern uint AdsImageToClipboard_32(IntPtr hTable, string pucFldName);

        [DllImport("ace64", EntryPoint = "AdsImageToClipboard", CharSet = CharSet.Ansi)]
        private static extern uint AdsImageToClipboard_64(IntPtr hTable, string pucFldName);

        public static uint AdsImageToClipboard(IntPtr hTable, string pucFldName)
        {
            return IntPtr.Size == 4 ? AdsImageToClipboard_32(hTable, pucFldName) : AdsImageToClipboard_64(hTable, pucFldName);
        }

        [DllImport("ace32", EntryPoint = "AdsInTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInTransaction_32(IntPtr hConnect, out ushort pbInTrans);

        [DllImport("ace64", EntryPoint = "AdsInTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsInTransaction_64(IntPtr hConnect, out ushort pbInTrans);

        public static uint AdsInTransaction(IntPtr hConnect, out ushort pbInTrans)
        {
            return IntPtr.Size == 4 ? AdsInTransaction_32(hConnect, out pbInTrans) : AdsInTransaction_64(hConnect, out pbInTrans);
        }

        [DllImport("ace32", EntryPoint = "AdsIsEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsEmpty_32(IntPtr hTable, string pucFldName, out ushort pbEmpty);

        [DllImport("ace64", EntryPoint = "AdsIsEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsEmpty_64(IntPtr hTable, string pucFldName, out ushort pbEmpty);

        public static uint AdsIsEmpty(IntPtr hTable, string pucFldName, out ushort pbEmpty)
        {
            return IntPtr.Size == 4 ? AdsIsEmpty_32(hTable, pucFldName, out pbEmpty) : AdsIsEmpty_64(hTable, pucFldName, out pbEmpty);
        }

        [DllImport("ace32", EntryPoint = "AdsIsEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsEmpty_32(IntPtr hTable, uint lFieldOrdinal, out ushort pbEmpty);

        [DllImport("ace64", EntryPoint = "AdsIsEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsEmpty_64(IntPtr hTable, uint lFieldOrdinal, out ushort pbEmpty);

        public static uint AdsIsEmpty(IntPtr hTable, uint lFieldOrdinal, out ushort pbEmpty)
        {
            return IntPtr.Size == 4 ? AdsIsEmpty_32(hTable, lFieldOrdinal, out pbEmpty) : AdsIsEmpty_64(hTable, lFieldOrdinal, out pbEmpty);
        }

        [DllImport("ace32", EntryPoint = "AdsIsExprValid", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsExprValid_32(IntPtr hTable, string pucExpr, out ushort pbValid);

        [DllImport("ace64", EntryPoint = "AdsIsExprValid", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsExprValid_64(IntPtr hTable, string pucExpr, out ushort pbValid);

        public static uint AdsIsExprValid(IntPtr hTable, string pucExpr, out ushort pbValid)
        {
            return IntPtr.Size == 4 ? AdsIsExprValid_32(hTable, pucExpr, out pbValid) : AdsIsExprValid_64(hTable, pucExpr, out pbValid);
        }

        [DllImport("ace32", EntryPoint = "AdsIsFound", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsFound_32(IntPtr hObj, out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsIsFound", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsFound_64(IntPtr hObj, out ushort pbFound);

        public static uint AdsIsFound(IntPtr hObj, out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsIsFound_32(hObj, out pbFound) : AdsIsFound_64(hObj, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexCompound", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexCompound_32(IntPtr hIndex, out ushort pbCompound);

        [DllImport("ace64", EntryPoint = "AdsIsIndexCompound", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexCompound_64(IntPtr hIndex, out ushort pbCompound);

        public static uint AdsIsIndexCompound(IntPtr hIndex, out ushort pbCompound)
        {
            return IntPtr.Size == 4 ? AdsIsIndexCompound_32(hIndex, out pbCompound) : AdsIsIndexCompound_64(hIndex, out pbCompound);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexCandidate", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexCandidate_32(IntPtr hIndex, out ushort pbCandidate);

        [DllImport("ace64", EntryPoint = "AdsIsIndexCandidate", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexCandidate_64(IntPtr hIndex, out ushort pbCandidate);

        public static uint AdsIsIndexCandidate(IntPtr hIndex, out ushort pbCandidate)
        {
            return IntPtr.Size == 4 ? AdsIsIndexCandidate_32(hIndex, out pbCandidate) : AdsIsIndexCandidate_64(hIndex, out pbCandidate);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexNullable", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexNullable_32(IntPtr hIndex, out ushort pbNullable);

        [DllImport("ace64", EntryPoint = "AdsIsIndexNullable", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexNullable_64(IntPtr hIndex, out ushort pbNullable);

        public static uint AdsIsIndexNullable(IntPtr hIndex, out ushort pbNullable)
        {
            return IntPtr.Size == 4 ? AdsIsIndexNullable_32(hIndex, out pbNullable) : AdsIsIndexNullable_64(hIndex, out pbNullable);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexCustom", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexCustom_32(IntPtr hIndex, out ushort pbCustom);

        [DllImport("ace64", EntryPoint = "AdsIsIndexCustom", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexCustom_64(IntPtr hIndex, out ushort pbCustom);

        public static uint AdsIsIndexCustom(IntPtr hIndex, out ushort pbCustom)
        {
            return IntPtr.Size == 4 ? AdsIsIndexCustom_32(hIndex, out pbCustom) : AdsIsIndexCustom_64(hIndex, out pbCustom);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexDescending", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexDescending_32(IntPtr hIndex, out ushort pbDescending);

        [DllImport("ace64", EntryPoint = "AdsIsIndexDescending", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexDescending_64(IntPtr hIndex, out ushort pbDescending);

        public static uint AdsIsIndexDescending(IntPtr hIndex, out ushort pbDescending)
        {
            return IntPtr.Size == 4 ? AdsIsIndexDescending_32(hIndex, out pbDescending) : AdsIsIndexDescending_64(hIndex, out pbDescending);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexPrimaryKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexPrimaryKey_32(IntPtr hIndex, out ushort pbPrimaryKey);

        [DllImport("ace64", EntryPoint = "AdsIsIndexPrimaryKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexPrimaryKey_64(IntPtr hIndex, out ushort pbPrimaryKey);

        public static uint AdsIsIndexPrimaryKey(IntPtr hIndex, out ushort pbPrimaryKey)
        {
            return IntPtr.Size == 4 ? AdsIsIndexPrimaryKey_32(hIndex, out pbPrimaryKey) : AdsIsIndexPrimaryKey_64(hIndex, out pbPrimaryKey);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexFTS", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexFTS_32(IntPtr hIndex, out ushort pbFTS);

        [DllImport("ace64", EntryPoint = "AdsIsIndexFTS", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexFTS_64(IntPtr hIndex, out ushort pbFTS);

        public static uint AdsIsIndexFTS(IntPtr hIndex, out ushort pbFTS)
        {
            return IntPtr.Size == 4 ? AdsIsIndexFTS_32(hIndex, out pbFTS) : AdsIsIndexFTS_64(hIndex, out pbFTS);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexUnique", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexUnique_32(IntPtr hIndex, out ushort pbUnique);

        [DllImport("ace64", EntryPoint = "AdsIsIndexUnique", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexUnique_64(IntPtr hIndex, out ushort pbUnique);

        public static uint AdsIsIndexUnique(IntPtr hIndex, out ushort pbUnique)
        {
            return IntPtr.Size == 4 ? AdsIsIndexUnique_32(hIndex, out pbUnique) : AdsIsIndexUnique_64(hIndex, out pbUnique);
        }

        [DllImport("ace32", EntryPoint = "AdsIsRecordDeleted", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordDeleted_32(IntPtr hTable, out ushort pbDeleted);

        [DllImport("ace64", EntryPoint = "AdsIsRecordDeleted", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordDeleted_64(IntPtr hTable, out ushort pbDeleted);

        public static uint AdsIsRecordDeleted(IntPtr hTable, out ushort pbDeleted)
        {
            return IntPtr.Size == 4 ? AdsIsRecordDeleted_32(hTable, out pbDeleted) : AdsIsRecordDeleted_64(hTable, out pbDeleted);
        }

        [DllImport("ace32", EntryPoint = "AdsIsRecordEncrypted", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordEncrypted_32(IntPtr hTable, out ushort pbEncrypted);

        [DllImport("ace64", EntryPoint = "AdsIsRecordEncrypted", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordEncrypted_64(IntPtr hTable, out ushort pbEncrypted);

        public static uint AdsIsRecordEncrypted(IntPtr hTable, out ushort pbEncrypted)
        {
            return IntPtr.Size == 4 ? AdsIsRecordEncrypted_32(hTable, out pbEncrypted) : AdsIsRecordEncrypted_64(hTable, out pbEncrypted);
        }

        [DllImport("ace32", EntryPoint = "AdsIsRecordLocked", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordLocked_32(IntPtr hTable, uint ulRec, out ushort pbLocked);

        [DllImport("ace64", EntryPoint = "AdsIsRecordLocked", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordLocked_64(IntPtr hTable, uint ulRec, out ushort pbLocked);

        public static uint AdsIsRecordLocked(IntPtr hTable, uint ulRec, out ushort pbLocked)
        {
            return IntPtr.Size == 4 ? AdsIsRecordLocked_32(hTable, ulRec, out pbLocked) : AdsIsRecordLocked_64(hTable, ulRec, out pbLocked);
        }

        [DllImport("ace32", EntryPoint = "AdsIsRecordVisible", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordVisible_32(IntPtr hObj, out ushort pbVisible);

        [DllImport("ace64", EntryPoint = "AdsIsRecordVisible", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordVisible_64(IntPtr hObj, out ushort pbVisible);

        public static uint AdsIsRecordVisible(IntPtr hObj, out ushort pbVisible)
        {
            return IntPtr.Size == 4 ? AdsIsRecordVisible_32(hObj, out pbVisible) : AdsIsRecordVisible_64(hObj, out pbVisible);
        }

        [DllImport("ace32", EntryPoint = "AdsIsServerLoaded", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsServerLoaded_32(string pucServer, out ushort pbLoaded);

        [DllImport("ace64", EntryPoint = "AdsIsServerLoaded", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsServerLoaded_64(string pucServer, out ushort pbLoaded);

        public static uint AdsIsServerLoaded(string pucServer, out ushort pbLoaded)
        {
            return IntPtr.Size == 4 ? AdsIsServerLoaded_32(pucServer, out pbLoaded) : AdsIsServerLoaded_64(pucServer, out pbLoaded);
        }

        [DllImport("ace32", EntryPoint = "AdsIsTableEncrypted", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsTableEncrypted_32(IntPtr hTable, out ushort pbEncrypted);

        [DllImport("ace64", EntryPoint = "AdsIsTableEncrypted", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsTableEncrypted_64(IntPtr hTable, out ushort pbEncrypted);

        public static uint AdsIsTableEncrypted(IntPtr hTable, out ushort pbEncrypted)
        {
            return IntPtr.Size == 4 ? AdsIsTableEncrypted_32(hTable, out pbEncrypted) : AdsIsTableEncrypted_64(hTable, out pbEncrypted);
        }

        [DllImport("ace32", EntryPoint = "AdsIsTableLocked", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsTableLocked_32(IntPtr hTable, out ushort pbLocked);

        [DllImport("ace64", EntryPoint = "AdsIsTableLocked", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsTableLocked_64(IntPtr hTable, out ushort pbLocked);

        public static uint AdsIsTableLocked(IntPtr hTable, out ushort pbLocked)
        {
            return IntPtr.Size == 4 ? AdsIsTableLocked_32(hTable, out pbLocked) : AdsIsTableLocked_64(hTable, out pbLocked);
        }

        [DllImport("ace32", EntryPoint = "AdsLocate", CharSet = CharSet.Ansi)]
        private static extern uint AdsLocate_32(
        IntPtr hTable,
        string pucExpr,
        ushort bForward,
        out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsLocate", CharSet = CharSet.Ansi)]
        private static extern uint AdsLocate_64(
        IntPtr hTable,
        string pucExpr,
        ushort bForward,
        out ushort pbFound);

        public static uint AdsLocate(
        IntPtr hTable,
        string pucExpr,
        ushort bForward,
        out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsLocate_32(hTable, pucExpr, bForward, out pbFound) : AdsLocate_64(hTable, pucExpr, bForward, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsLockRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsLockRecord_32(IntPtr hTable, uint ulRec);

        [DllImport("ace64", EntryPoint = "AdsLockRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsLockRecord_64(IntPtr hTable, uint ulRec);

        public static uint AdsLockRecord(IntPtr hTable, uint ulRec)
        {
            return IntPtr.Size == 4 ? AdsLockRecord_32(hTable, ulRec) : AdsLockRecord_64(hTable, ulRec);
        }

        [DllImport("ace32", EntryPoint = "AdsLockTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsLockTable_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsLockTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsLockTable_64(IntPtr hTable);

        public static uint AdsLockTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsLockTable_32(hTable) : AdsLockTable_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsLookupKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsLookupKey_32(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsLookupKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsLookupKey_64(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound);

        public static uint AdsLookupKey(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsLookupKey_32(hIndex, pucKey, usKeyLen, usDataType, out pbFound) : AdsLookupKey_64(hIndex, pucKey, usKeyLen, usDataType, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsMgConnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgConnect_32(
        string pucServerName,
        string pucUserName,
        string pucPassword,
        out IntPtr phMgmtHandle);

        [DllImport("ace64", EntryPoint = "AdsMgConnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgConnect_64(
        string pucServerName,
        string pucUserName,
        string pucPassword,
        out IntPtr phMgmtHandle);

        public static uint AdsMgConnect(
        string pucServerName,
        string pucUserName,
        string pucPassword,
        out IntPtr phMgmtHandle)
        {
            return IntPtr.Size == 4 ? AdsMgConnect_32(pucServerName, pucUserName, pucPassword, out phMgmtHandle) : AdsMgConnect_64(pucServerName, pucUserName, pucPassword, out phMgmtHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsMgDisconnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgDisconnect_32(IntPtr hMgmtHandle);

        [DllImport("ace64", EntryPoint = "AdsMgDisconnect", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgDisconnect_64(IntPtr hMgmtHandle);

        public static uint AdsMgDisconnect(IntPtr hMgmtHandle)
        {
            return IntPtr.Size == 4 ? AdsMgDisconnect_32(hMgmtHandle) : AdsMgDisconnect_64(hMgmtHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsMgResetCommStats", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgResetCommStats_32(IntPtr hMgmtHandle);

        [DllImport("ace64", EntryPoint = "AdsMgResetCommStats", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgResetCommStats_64(IntPtr hMgmtHandle);

        public static uint AdsMgResetCommStats(IntPtr hMgmtHandle)
        {
            return IntPtr.Size == 4 ? AdsMgResetCommStats_32(hMgmtHandle) : AdsMgResetCommStats_64(hMgmtHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsMgDumpInternalTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgDumpInternalTables_32(IntPtr hMgmtHandle);

        [DllImport("ace64", EntryPoint = "AdsMgDumpInternalTables", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgDumpInternalTables_64(IntPtr hMgmtHandle);

        public static uint AdsMgDumpInternalTables(IntPtr hMgmtHandle)
        {
            return IntPtr.Size == 4 ? AdsMgDumpInternalTables_32(hMgmtHandle) : AdsMgDumpInternalTables_64(hMgmtHandle);
        }

        [DllImport("ace32", EntryPoint = "AdsMgGetServerType", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgGetServerType_32(IntPtr hMgmtHandle, out ushort pusServerType);

        [DllImport("ace64", EntryPoint = "AdsMgGetServerType", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgGetServerType_64(IntPtr hMgmtHandle, out ushort pusServerType);

        public static uint AdsMgGetServerType(IntPtr hMgmtHandle, out ushort pusServerType)
        {
            return IntPtr.Size == 4 ? AdsMgGetServerType_32(hMgmtHandle, out pusServerType) : AdsMgGetServerType_64(hMgmtHandle, out pusServerType);
        }

        [DllImport("ace32", EntryPoint = "AdsMgKillUser", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgKillUser_32(
        IntPtr hMgmtHandle,
        string pucUserName,
        ushort usConnNumber);

        [DllImport("ace64", EntryPoint = "AdsMgKillUser", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgKillUser_64(
        IntPtr hMgmtHandle,
        string pucUserName,
        ushort usConnNumber);

        public static uint AdsMgKillUser(IntPtr hMgmtHandle, string pucUserName, ushort usConnNumber)
        {
            return IntPtr.Size == 4 ? AdsMgKillUser_32(hMgmtHandle, pucUserName, usConnNumber) : AdsMgKillUser_64(hMgmtHandle, pucUserName, usConnNumber);
        }

        [DllImport("ace32", EntryPoint = "AdsNullTerminateStrings", CharSet = CharSet.Ansi)]
        private static extern uint AdsNullTerminateStrings_32(ushort bNullTerminate);

        [DllImport("ace64", EntryPoint = "AdsNullTerminateStrings", CharSet = CharSet.Ansi)]
        private static extern uint AdsNullTerminateStrings_64(ushort bNullTerminate);

        public static uint AdsNullTerminateStrings(ushort bNullTerminate)
        {
            return IntPtr.Size == 4 ? AdsNullTerminateStrings_32(bNullTerminate) : AdsNullTerminateStrings_64(bNullTerminate);
        }

        [DllImport("ace32", EntryPoint = "AdsOpenIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenIndex_32(
        IntPtr hTable,
        string pucName,
        [In, Out] IntPtr[] ahIndex,
        ref ushort pusArrayLen);

        [DllImport("ace64", EntryPoint = "AdsOpenIndex", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenIndex_64(
        IntPtr hTable,
        string pucName,
        [In, Out] IntPtr[] ahIndex,
        ref ushort pusArrayLen);

        public static uint AdsOpenIndex(
        IntPtr hTable,
        string pucName,
        IntPtr[] ahIndex,
        ref ushort pusArrayLen)
        {
            return IntPtr.Size == 4 ? AdsOpenIndex_32(hTable, pucName, ahIndex, ref pusArrayLen) : AdsOpenIndex_64(hTable, pucName, ahIndex, ref pusArrayLen);
        }

        [DllImport("ace32", EntryPoint = "AdsOpenTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenTable_32(
        IntPtr hConnect,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        uint ulOptions,
        out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsOpenTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenTable_64(
        IntPtr hConnect,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        uint ulOptions,
        out IntPtr phTable);

        public static uint AdsOpenTable(
        IntPtr hConnect,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        uint ulOptions,
        out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsOpenTable_32(hConnect, pucName, pucAlias, usTableType, usCharType, usLockType, usCheckRights, ulOptions, out phTable) : AdsOpenTable_64(hConnect, pucName, pucAlias, usTableType, usCharType, usLockType, usCheckRights, ulOptions, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsOpenTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenTable90_32(
        IntPtr hConnect,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        uint ulOptions,
        string pucCollation,
        out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsOpenTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenTable90_64(
        IntPtr hConnect,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        uint ulOptions,
        string pucCollation,
        out IntPtr phTable);

        public static uint AdsOpenTable90(
        IntPtr hConnect,
        string pucName,
        string pucAlias,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        uint ulOptions,
        string pucCollation,
        out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsOpenTable90_32(hConnect, pucName, pucAlias, usTableType, usCharType, usLockType, usCheckRights, ulOptions, pucCollation, out phTable) : AdsOpenTable90_64(hConnect, pucName, pucAlias, usTableType, usCharType, usLockType, usCheckRights, ulOptions, pucCollation, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsOpenTable101", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenTable101_32(
        IntPtr hConnect,
        string pucName,
        out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsOpenTable101", CharSet = CharSet.Ansi)]
        private static extern uint AdsOpenTable101_64(
        IntPtr hConnect,
        string pucName,
        out IntPtr phTable);

        public static uint AdsOpenTable101(IntPtr hConnect, string pucName, out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsOpenTable101_32(hConnect, pucName, out phTable) : AdsOpenTable101_64(hConnect, pucName, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsPackTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsPackTable_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsPackTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsPackTable_64(IntPtr hTable);

        public static uint AdsPackTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsPackTable_32(hTable) : AdsPackTable_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsPackTable120", CharSet = CharSet.Ansi)]
        private static extern uint AdsPackTable120_32(
        IntPtr hTable,
        uint ulMemoBlockSize,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsPackTable120", CharSet = CharSet.Ansi)]
        private static extern uint AdsPackTable120_64(
        IntPtr hTable,
        uint ulMemoBlockSize,
        uint ulOptions);

        public static uint AdsPackTable120(IntPtr hTable, uint ulMemoBlockSize, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsPackTable120_32(hTable, ulMemoBlockSize, ulOptions) : AdsPackTable120_64(hTable, ulMemoBlockSize, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsRecallRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsRecallRecord_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsRecallRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsRecallRecord_64(IntPtr hTable);

        public static uint AdsRecallRecord(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsRecallRecord_32(hTable) : AdsRecallRecord_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsRecallAllRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsRecallAllRecords_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsRecallAllRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsRecallAllRecords_64(IntPtr hTable);

        public static uint AdsRecallAllRecords(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsRecallAllRecords_32(hTable) : AdsRecallAllRecords_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsRefreshRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsRefreshRecord_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsRefreshRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsRefreshRecord_64(IntPtr hTable);

        public static uint AdsRefreshRecord(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsRefreshRecord_32(hTable) : AdsRefreshRecord_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsClearProgressCallback", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearProgressCallback_32();

        [DllImport("ace64", EntryPoint = "AdsClearProgressCallback", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearProgressCallback_64();

        public static uint AdsClearProgressCallback()
        {
            return IntPtr.Size == 4 ? AdsClearProgressCallback_32() : AdsClearProgressCallback_64();
        }

        [DllImport("ace32", EntryPoint = "AdsRegisterCallbackFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsRegisterCallbackFunction_32(CallbackFn pfn, uint ulCallBackID);

        [DllImport("ace64", EntryPoint = "AdsRegisterCallbackFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsRegisterCallbackFunction_64(CallbackFn pfn, uint ulCallBackID);

        public static uint AdsRegisterCallbackFunction(CallbackFn pfn, uint ulCallBackID)
        {
            return IntPtr.Size == 4 ? AdsRegisterCallbackFunction_32(pfn, ulCallBackID) : AdsRegisterCallbackFunction_64(pfn, ulCallBackID);
        }

        [DllImport("ace32", EntryPoint = "AdsRegisterCallbackFunction101", CharSet = CharSet.Ansi)]
        private static extern uint AdsRegisterCallbackFunction101_32(
        CallbackFn101 pfn,
        long qCallBackID);

        [DllImport("ace64", EntryPoint = "AdsRegisterCallbackFunction101", CharSet = CharSet.Ansi)]
        private static extern uint AdsRegisterCallbackFunction101_64(
        CallbackFn101 pfn,
        long qCallBackID);

        public static uint AdsRegisterCallbackFunction101(CallbackFn101 pfn, long qCallBackID)
        {
            return IntPtr.Size == 4 ? AdsRegisterCallbackFunction101_32(pfn, qCallBackID) : AdsRegisterCallbackFunction101_64(pfn, qCallBackID);
        }

        [DllImport("ace32", EntryPoint = "AdsClearCallbackFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearCallbackFunction_32();

        [DllImport("ace64", EntryPoint = "AdsClearCallbackFunction", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearCallbackFunction_64();

        public static uint AdsClearCallbackFunction()
        {
            return IntPtr.Size == 4 ? AdsClearCallbackFunction_32() : AdsClearCallbackFunction_64();
        }

        [DllImport("ace32", EntryPoint = "AdsSetSQLTimeout", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetSQLTimeout_32(IntPtr hObj, uint ulTimeout);

        [DllImport("ace64", EntryPoint = "AdsSetSQLTimeout", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetSQLTimeout_64(IntPtr hObj, uint ulTimeout);

        public static uint AdsSetSQLTimeout(IntPtr hObj, uint ulTimeout)
        {
            return IntPtr.Size == 4 ? AdsSetSQLTimeout_32(hObj, ulTimeout) : AdsSetSQLTimeout_64(hObj, ulTimeout);
        }

        [DllImport("ace32", EntryPoint = "AdsReindex", CharSet = CharSet.Ansi)]
        private static extern uint AdsReindex_32(IntPtr hObject);

        [DllImport("ace64", EntryPoint = "AdsReindex", CharSet = CharSet.Ansi)]
        private static extern uint AdsReindex_64(IntPtr hObject);

        public static uint AdsReindex(IntPtr hObject)
        {
            return IntPtr.Size == 4 ? AdsReindex_32(hObject) : AdsReindex_64(hObject);
        }

        [DllImport("ace32", EntryPoint = "AdsReindex61", CharSet = CharSet.Ansi)]
        private static extern uint AdsReindex61_32(IntPtr hObject, uint ulPageSize);

        [DllImport("ace64", EntryPoint = "AdsReindex61", CharSet = CharSet.Ansi)]
        private static extern uint AdsReindex61_64(IntPtr hObject, uint ulPageSize);

        public static uint AdsReindex61(IntPtr hObject, uint ulPageSize)
        {
            return IntPtr.Size == 4 ? AdsReindex61_32(hObject, ulPageSize) : AdsReindex61_64(hObject, ulPageSize);
        }

        [DllImport("ace32", EntryPoint = "AdsReindexFTS", CharSet = CharSet.Ansi)]
        private static extern uint AdsReindexFTS_32(IntPtr hObject, uint ulPageSize);

        [DllImport("ace64", EntryPoint = "AdsReindexFTS", CharSet = CharSet.Ansi)]
        private static extern uint AdsReindexFTS_64(IntPtr hObject, uint ulPageSize);

        public static uint AdsReindexFTS(IntPtr hObject, uint ulPageSize)
        {
            return IntPtr.Size == 4 ? AdsReindexFTS_32(hObject, ulPageSize) : AdsReindexFTS_64(hObject, ulPageSize);
        }

        [DllImport("ace32", EntryPoint = "AdsResetConnection", CharSet = CharSet.Ansi)]
        private static extern uint AdsResetConnection_32(IntPtr hConnect);

        [DllImport("ace64", EntryPoint = "AdsResetConnection", CharSet = CharSet.Ansi)]
        private static extern uint AdsResetConnection_64(IntPtr hConnect);

        public static uint AdsResetConnection(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsResetConnection_32(hConnect) : AdsResetConnection_64(hConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsRollbackTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsRollbackTransaction_32(IntPtr hConnect);

        [DllImport("ace64", EntryPoint = "AdsRollbackTransaction", CharSet = CharSet.Ansi)]
        private static extern uint AdsRollbackTransaction_64(IntPtr hConnect);

        public static uint AdsRollbackTransaction(IntPtr hConnect)
        {
            return IntPtr.Size == 4 ? AdsRollbackTransaction_32(hConnect) : AdsRollbackTransaction_64(hConnect);
        }

        [DllImport("ace32", EntryPoint = "AdsSeek", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeek_32(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsSeek", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeek_64(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out ushort pbFound);

        public static uint AdsSeek(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsSeek_32(hIndex, pucKey, usKeyLen, usDataType, usSeekType, out pbFound) : AdsSeek_64(hIndex, pucKey, usKeyLen, usDataType, usSeekType, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsSeek", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeek_32(
        IntPtr hIndex,
        byte[] abKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsSeek", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeek_64(
        IntPtr hIndex,
        byte[] abKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out ushort pbFound);

        public static uint AdsSeek(
        IntPtr hIndex,
        byte[] abKey,
        ushort usKeyLen,
        ushort usDataType,
        ushort usSeekType,
        out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsSeek_32(hIndex, abKey, usKeyLen, usDataType, usSeekType, out pbFound) : AdsSeek_64(hIndex, abKey, usKeyLen, usDataType, usSeekType, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsSeekLast", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeekLast_32(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsSeekLast", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeekLast_64(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound);

        public static uint AdsSeekLast(
        IntPtr hIndex,
        string pucKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsSeekLast_32(hIndex, pucKey, usKeyLen, usDataType, out pbFound) : AdsSeekLast_64(hIndex, pucKey, usKeyLen, usDataType, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsSeekLast", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeekLast_32(
        IntPtr hIndex,
        byte[] abKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound);

        [DllImport("ace64", EntryPoint = "AdsSeekLast", CharSet = CharSet.Ansi)]
        private static extern uint AdsSeekLast_64(
        IntPtr hIndex,
        byte[] abKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound);

        public static uint AdsSeekLast(
        IntPtr hIndex,
        byte[] abKey,
        ushort usKeyLen,
        ushort usDataType,
        out ushort pbFound)
        {
            return IntPtr.Size == 4 ? AdsSeekLast_32(hIndex, abKey, usKeyLen, usDataType, out pbFound) : AdsSeekLast_64(hIndex, abKey, usKeyLen, usDataType, out pbFound);
        }

        [DllImport("ace32", EntryPoint = "AdsSetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBinary_32(
        IntPtr hTable,
        string pucFldName,
        ushort usBinaryType,
        uint ulTotalLength,
        uint ulOffset,
        byte[] pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBinary_64(
        IntPtr hTable,
        string pucFldName,
        ushort usBinaryType,
        uint ulTotalLength,
        uint ulOffset,
        byte[] pucBuf,
        uint ulLen);

        public static uint AdsSetBinary(
        IntPtr hTable,
        string pucFldName,
        ushort usBinaryType,
        uint ulTotalLength,
        uint ulOffset,
        byte[] pucBuf,
        uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetBinary_32(hTable, pucFldName, usBinaryType, ulTotalLength, ulOffset, pucBuf, ulLen) : AdsSetBinary_64(hTable, pucFldName, usBinaryType, ulTotalLength, ulOffset, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBinary_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        uint ulTotalLength,
        uint ulOffset,
        byte[] pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetBinary_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        uint ulTotalLength,
        uint ulOffset,
        byte[] pucBuf,
        uint ulLen);

        public static uint AdsSetBinary(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        uint ulTotalLength,
        uint ulOffset,
        byte[] pucBuf,
        uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetBinary_32(hTable, lFieldOrdinal, usBinaryType, ulTotalLength, ulOffset, pucBuf, ulLen) : AdsSetBinary_64(hTable, lFieldOrdinal, usBinaryType, ulTotalLength, ulOffset, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetCollationLang", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCollationLang_32(string pucLang);

        [DllImport("ace64", EntryPoint = "AdsSetCollationLang", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCollationLang_64(string pucLang);

        public static uint AdsSetCollationLang(string pucLang)
        {
            return IntPtr.Size == 4 ? AdsSetCollationLang_32(pucLang) : AdsSetCollationLang_64(pucLang);
        }

        [DllImport("ace32", EntryPoint = "AdsSetCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCollation_32(IntPtr hConnect, string pucCollation);

        [DllImport("ace64", EntryPoint = "AdsSetCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetCollation_64(IntPtr hConnect, string pucCollation);

        public static uint AdsSetCollation(IntPtr hConnect, string pucCollation)
        {
            return IntPtr.Size == 4 ? AdsSetCollation_32(hConnect, pucCollation) : AdsSetCollation_64(hConnect, pucCollation);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDate_32(
        IntPtr hObj,
        string pucFldName,
        string pucValue,
        ushort usLen);

        [DllImport("ace64", EntryPoint = "AdsSetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDate_64(
        IntPtr hObj,
        string pucFldName,
        string pucValue,
        ushort usLen);

        public static uint AdsSetDate(IntPtr hObj, string pucFldName, string pucValue, ushort usLen)
        {
            return IntPtr.Size == 4 ? AdsSetDate_32(hObj, pucFldName, pucValue, usLen) : AdsSetDate_64(hObj, pucFldName, pucValue, usLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDate_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucValue,
        ushort usLen);

        [DllImport("ace64", EntryPoint = "AdsSetDate", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDate_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucValue,
        ushort usLen);

        public static uint AdsSetDate(IntPtr hObj, uint lFieldOrdinal, string pucValue, ushort usLen)
        {
            return IntPtr.Size == 4 ? AdsSetDate_32(hObj, lFieldOrdinal, pucValue, usLen) : AdsSetDate_64(hObj, lFieldOrdinal, pucValue, usLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDateFormat", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDateFormat_32(string pucFormat);

        [DllImport("ace64", EntryPoint = "AdsSetDateFormat", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDateFormat_64(string pucFormat);

        public static uint AdsSetDateFormat(string pucFormat)
        {
            return IntPtr.Size == 4 ? AdsSetDateFormat_32(pucFormat) : AdsSetDateFormat_64(pucFormat);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDateFormat60", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDateFormat60_32(IntPtr hConnect, string pucFormat);

        [DllImport("ace64", EntryPoint = "AdsSetDateFormat60", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDateFormat60_64(IntPtr hConnect, string pucFormat);

        public static uint AdsSetDateFormat60(IntPtr hConnect, string pucFormat)
        {
            return IntPtr.Size == 4 ? AdsSetDateFormat60_32(hConnect, pucFormat) : AdsSetDateFormat60_64(hConnect, pucFormat);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDecimals_32(ushort usDecimals);

        [DllImport("ace64", EntryPoint = "AdsSetDecimals", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDecimals_64(ushort usDecimals);

        public static uint AdsSetDecimals(ushort usDecimals)
        {
            return IntPtr.Size == 4 ? AdsSetDecimals_32(usDecimals) : AdsSetDecimals_64(usDecimals);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDefault", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDefault_32(string pucDefault);

        [DllImport("ace64", EntryPoint = "AdsSetDefault", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDefault_64(string pucDefault);

        public static uint AdsSetDefault(string pucDefault)
        {
            return IntPtr.Size == 4 ? AdsSetDefault_32(pucDefault) : AdsSetDefault_64(pucDefault);
        }

        [DllImport("ace32", EntryPoint = "AdsShowDeleted", CharSet = CharSet.Ansi)]
        private static extern uint AdsShowDeleted_32(ushort bShowDeleted);

        [DllImport("ace64", EntryPoint = "AdsShowDeleted", CharSet = CharSet.Ansi)]
        private static extern uint AdsShowDeleted_64(ushort bShowDeleted);

        public static uint AdsShowDeleted(ushort bShowDeleted)
        {
            return IntPtr.Size == 4 ? AdsShowDeleted_32(bShowDeleted) : AdsShowDeleted_64(bShowDeleted);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDouble_32(IntPtr hObj, string pucFldName, double dValue);

        [DllImport("ace64", EntryPoint = "AdsSetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDouble_64(IntPtr hObj, string pucFldName, double dValue);

        public static uint AdsSetDouble(IntPtr hObj, string pucFldName, double dValue)
        {
            return IntPtr.Size == 4 ? AdsSetDouble_32(hObj, pucFldName, dValue) : AdsSetDouble_64(hObj, pucFldName, dValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDouble_32(IntPtr hObj, uint lFieldOrdinal, double dValue);

        [DllImport("ace64", EntryPoint = "AdsSetDouble", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetDouble_64(IntPtr hObj, uint lFieldOrdinal, double dValue);

        public static uint AdsSetDouble(IntPtr hObj, uint lFieldOrdinal, double dValue)
        {
            return IntPtr.Size == 4 ? AdsSetDouble_32(hObj, lFieldOrdinal, dValue) : AdsSetDouble_64(hObj, lFieldOrdinal, dValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetEmpty_32(IntPtr hObj, string pucFldName);

        [DllImport("ace64", EntryPoint = "AdsSetEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetEmpty_64(IntPtr hObj, string pucFldName);

        public static uint AdsSetEmpty(IntPtr hObj, string pucFldName)
        {
            return IntPtr.Size == 4 ? AdsSetEmpty_32(hObj, pucFldName) : AdsSetEmpty_64(hObj, pucFldName);
        }

        [DllImport("ace32", EntryPoint = "AdsSetEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetEmpty_32(IntPtr hObj, uint lFieldOrdinal);

        [DllImport("ace64", EntryPoint = "AdsSetEmpty", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetEmpty_64(IntPtr hObj, uint lFieldOrdinal);

        public static uint AdsSetEmpty(IntPtr hObj, uint lFieldOrdinal)
        {
            return IntPtr.Size == 4 ? AdsSetEmpty_32(hObj, lFieldOrdinal) : AdsSetEmpty_64(hObj, lFieldOrdinal);
        }

        [DllImport("ace32", EntryPoint = "AdsSetEpoch", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetEpoch_32(ushort usCentury);

        [DllImport("ace64", EntryPoint = "AdsSetEpoch", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetEpoch_64(ushort usCentury);

        public static uint AdsSetEpoch(ushort usCentury)
        {
            return IntPtr.Size == 4 ? AdsSetEpoch_32(usCentury) : AdsSetEpoch_64(usCentury);
        }

        [DllImport("ace32", EntryPoint = "AdsSetExact", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetExact_32(ushort bExact);

        [DllImport("ace64", EntryPoint = "AdsSetExact", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetExact_64(ushort bExact);

        public static uint AdsSetExact(ushort bExact)
        {
            return IntPtr.Size == 4 ? AdsSetExact_32(bExact) : AdsSetExact_64(bExact);
        }

        [DllImport("ace32", EntryPoint = "AdsSetExact22", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetExact22_32(IntPtr hObj, ushort bExact);

        [DllImport("ace64", EntryPoint = "AdsSetExact22", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetExact22_64(IntPtr hObj, ushort bExact);

        public static uint AdsSetExact22(IntPtr hObj, ushort bExact)
        {
            return IntPtr.Size == 4 ? AdsSetExact22_32(hObj, bExact) : AdsSetExact22_64(hObj, bExact);
        }

        [DllImport("ace32", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_32(
        IntPtr hObj,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_64(
        IntPtr hObj,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetField(IntPtr hObj, string pucFldName, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetField_32(hObj, pucFldName, pucBuf, ulLen) : AdsSetField_64(hObj, pucFldName, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetField(IntPtr hObj, uint lFieldOrdinal, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetField_32(hObj, lFieldOrdinal, pucBuf, ulLen) : AdsSetField_64(hObj, lFieldOrdinal, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_32(
        IntPtr hObj,
        string pucFldName,
        byte[] abBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_64(
        IntPtr hObj,
        string pucFldName,
        byte[] abBuf,
        uint ulLen);

        public static uint AdsSetField(IntPtr hObj, string pucFldName, byte[] abBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetField_32(hObj, pucFldName, abBuf, ulLen) : AdsSetField_64(hObj, pucFldName, abBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        byte[] abBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetField", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetField_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        byte[] abBuf,
        uint ulLen);

        public static uint AdsSetField(IntPtr hObj, uint lFieldOrdinal, byte[] abBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetField_32(hObj, lFieldOrdinal, abBuf, ulLen) : AdsSetField_64(hObj, lFieldOrdinal, abBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetFieldW_32(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        string pwcBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetFieldW_64(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        string pwcBuf,
        uint ulLen);

        public static uint AdsSetFieldW(IntPtr hObj, string pucFldName, string pwcBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetFieldW_32(hObj, pucFldName, pwcBuf, ulLen) : AdsSetFieldW_64(hObj, pucFldName, pwcBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetFieldW_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pwcBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetFieldW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetFieldW_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pwcBuf,
        uint ulLen);

        public static uint AdsSetFieldW(IntPtr hObj, uint lFieldOrdinal, string pwcBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetFieldW_32(hObj, lFieldOrdinal, pwcBuf, ulLen) : AdsSetFieldW_64(hObj, lFieldOrdinal, pwcBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFilter_32(IntPtr hTable, string pucFilter);

        [DllImport("ace64", EntryPoint = "AdsSetFilter", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetFilter_64(IntPtr hTable, string pucFilter);

        public static uint AdsSetFilter(IntPtr hTable, string pucFilter)
        {
            return IntPtr.Size == 4 ? AdsSetFilter_32(hTable, pucFilter) : AdsSetFilter_64(hTable, pucFilter);
        }

        [DllImport("ace32", EntryPoint = "AdsSetFilter100", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetFilter100_32(IntPtr hTable, string pvFilter, uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsSetFilter100", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetFilter100_64(IntPtr hTable, string pvFilter, uint ulOptions);

        public static uint AdsSetFilter100(IntPtr hTable, string pvFilter, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsSetFilter100_32(hTable, pvFilter, ulOptions) : AdsSetFilter100_64(hTable, pvFilter, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsSetHandleLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetHandleLong_32(IntPtr hObj, uint ulVal);

        [DllImport("ace64", EntryPoint = "AdsSetHandleLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetHandleLong_64(IntPtr hObj, uint ulVal);

        public static uint AdsSetHandleLong(IntPtr hObj, uint ulVal)
        {
            return IntPtr.Size == 4 ? AdsSetHandleLong_32(hObj, ulVal) : AdsSetHandleLong_64(hObj, ulVal);
        }

        [DllImport("ace32", EntryPoint = "AdsSetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetJulian_32(IntPtr hObj, string pucFldName, int lDate);

        [DllImport("ace64", EntryPoint = "AdsSetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetJulian_64(IntPtr hObj, string pucFldName, int lDate);

        public static uint AdsSetJulian(IntPtr hObj, string pucFldName, int lDate)
        {
            return IntPtr.Size == 4 ? AdsSetJulian_32(hObj, pucFldName, lDate) : AdsSetJulian_64(hObj, pucFldName, lDate);
        }

        [DllImport("ace32", EntryPoint = "AdsSetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetJulian_32(IntPtr hObj, uint lFieldOrdinal, int lDate);

        [DllImport("ace64", EntryPoint = "AdsSetJulian", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetJulian_64(IntPtr hObj, uint lFieldOrdinal, int lDate);

        public static uint AdsSetJulian(IntPtr hObj, uint lFieldOrdinal, int lDate)
        {
            return IntPtr.Size == 4 ? AdsSetJulian_32(hObj, lFieldOrdinal, lDate) : AdsSetJulian_64(hObj, lFieldOrdinal, lDate);
        }

        [DllImport("ace32", EntryPoint = "AdsSetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLogical_32(IntPtr hObj, string pucFldName, ushort bValue);

        [DllImport("ace64", EntryPoint = "AdsSetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLogical_64(IntPtr hObj, string pucFldName, ushort bValue);

        public static uint AdsSetLogical(IntPtr hObj, string pucFldName, ushort bValue)
        {
            return IntPtr.Size == 4 ? AdsSetLogical_32(hObj, pucFldName, bValue) : AdsSetLogical_64(hObj, pucFldName, bValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLogical_32(IntPtr hObj, uint lFieldOrdinal, ushort bValue);

        [DllImport("ace64", EntryPoint = "AdsSetLogical", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLogical_64(IntPtr hObj, uint lFieldOrdinal, ushort bValue);

        public static uint AdsSetLogical(IntPtr hObj, uint lFieldOrdinal, ushort bValue)
        {
            return IntPtr.Size == 4 ? AdsSetLogical_32(hObj, lFieldOrdinal, bValue) : AdsSetLogical_64(hObj, lFieldOrdinal, bValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLong_32(IntPtr hObj, string pucFldName, int lValue);

        [DllImport("ace64", EntryPoint = "AdsSetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLong_64(IntPtr hObj, string pucFldName, int lValue);

        public static uint AdsSetLong(IntPtr hObj, string pucFldName, int lValue)
        {
            return IntPtr.Size == 4 ? AdsSetLong_32(hObj, pucFldName, lValue) : AdsSetLong_64(hObj, pucFldName, lValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLong_32(IntPtr hObj, uint lFieldOrdinal, int lValue);

        [DllImport("ace64", EntryPoint = "AdsSetLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLong_64(IntPtr hObj, uint lFieldOrdinal, int lValue);

        public static uint AdsSetLong(IntPtr hObj, uint lFieldOrdinal, int lValue)
        {
            return IntPtr.Size == 4 ? AdsSetLong_32(hObj, lFieldOrdinal, lValue) : AdsSetLong_64(hObj, lFieldOrdinal, lValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLongLong_32(IntPtr hObj, string pucFldName, long qValue);

        [DllImport("ace64", EntryPoint = "AdsSetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLongLong_64(IntPtr hObj, string pucFldName, long qValue);

        public static uint AdsSetLongLong(IntPtr hObj, string pucFldName, long qValue)
        {
            return IntPtr.Size == 4 ? AdsSetLongLong_32(hObj, pucFldName, qValue) : AdsSetLongLong_64(hObj, pucFldName, qValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLongLong_32(IntPtr hObj, uint lFieldOrdinal, long qValue);

        [DllImport("ace64", EntryPoint = "AdsSetLongLong", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetLongLong_64(IntPtr hObj, uint lFieldOrdinal, long qValue);

        public static uint AdsSetLongLong(IntPtr hObj, uint lFieldOrdinal, long qValue)
        {
            return IntPtr.Size == 4 ? AdsSetLongLong_32(hObj, lFieldOrdinal, qValue) : AdsSetLongLong_64(hObj, lFieldOrdinal, qValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMilliseconds_32(IntPtr hObj, string pucFldName, int lTime);

        [DllImport("ace64", EntryPoint = "AdsSetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMilliseconds_64(IntPtr hObj, string pucFldName, int lTime);

        public static uint AdsSetMilliseconds(IntPtr hObj, string pucFldName, int lTime)
        {
            return IntPtr.Size == 4 ? AdsSetMilliseconds_32(hObj, pucFldName, lTime) : AdsSetMilliseconds_64(hObj, pucFldName, lTime);
        }

        [DllImport("ace32", EntryPoint = "AdsSetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMilliseconds_32(IntPtr hObj, uint lFieldOrdinal, int lTime);

        [DllImport("ace64", EntryPoint = "AdsSetMilliseconds", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMilliseconds_64(IntPtr hObj, uint lFieldOrdinal, int lTime);

        public static uint AdsSetMilliseconds(IntPtr hObj, uint lFieldOrdinal, int lTime)
        {
            return IntPtr.Size == 4 ? AdsSetMilliseconds_32(hObj, lFieldOrdinal, lTime) : AdsSetMilliseconds_64(hObj, lFieldOrdinal, lTime);
        }

        [DllImport("ace32", EntryPoint = "AdsSetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMoney_32(IntPtr hObj, string pucFldName, long qValue);

        [DllImport("ace64", EntryPoint = "AdsSetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMoney_64(IntPtr hObj, string pucFldName, long qValue);

        public static uint AdsSetMoney(IntPtr hObj, string pucFldName, long qValue)
        {
            return IntPtr.Size == 4 ? AdsSetMoney_32(hObj, pucFldName, qValue) : AdsSetMoney_64(hObj, pucFldName, qValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMoney_32(IntPtr hObj, uint lFieldOrdinal, long qValue);

        [DllImport("ace64", EntryPoint = "AdsSetMoney", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetMoney_64(IntPtr hObj, uint lFieldOrdinal, long qValue);

        public static uint AdsSetMoney(IntPtr hObj, uint lFieldOrdinal, long qValue)
        {
            return IntPtr.Size == 4 ? AdsSetMoney_32(hObj, lFieldOrdinal, qValue) : AdsSetMoney_64(hObj, lFieldOrdinal, qValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRecord_32(IntPtr hObj, byte[] pucRec, uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRecord_64(IntPtr hObj, byte[] pucRec, uint ulLen);

        public static uint AdsSetRecord(IntPtr hObj, byte[] pucRec, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetRecord_32(hObj, pucRec, ulLen) : AdsSetRecord_64(hObj, pucRec, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetRelation", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRelation_32(
        IntPtr hTableParent,
        IntPtr hIndexChild,
        string pucExpr);

        [DllImport("ace64", EntryPoint = "AdsSetRelation", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRelation_64(
        IntPtr hTableParent,
        IntPtr hIndexChild,
        string pucExpr);

        public static uint AdsSetRelation(IntPtr hTableParent, IntPtr hIndexChild, string pucExpr)
        {
            return IntPtr.Size == 4 ? AdsSetRelation_32(hTableParent, hIndexChild, pucExpr) : AdsSetRelation_64(hTableParent, hIndexChild, pucExpr);
        }

        [DllImport("ace32", EntryPoint = "AdsSetRelKeyPos", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRelKeyPos_32(IntPtr hIndex, double dPos);

        [DllImport("ace64", EntryPoint = "AdsSetRelKeyPos", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRelKeyPos_64(IntPtr hIndex, double dPos);

        public static uint AdsSetRelKeyPos(IntPtr hIndex, double dPos)
        {
            return IntPtr.Size == 4 ? AdsSetRelKeyPos_32(hIndex, dPos) : AdsSetRelKeyPos_64(hIndex, dPos);
        }

        [DllImport("ace32", EntryPoint = "AdsSetScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetScope_32(
        IntPtr hIndex,
        ushort usScopeOption,
        string pucScope,
        ushort usScopeLen,
        ushort usDataType);

        [DllImport("ace64", EntryPoint = "AdsSetScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetScope_64(
        IntPtr hIndex,
        ushort usScopeOption,
        string pucScope,
        ushort usScopeLen,
        ushort usDataType);

        public static uint AdsSetScope(
        IntPtr hIndex,
        ushort usScopeOption,
        string pucScope,
        ushort usScopeLen,
        ushort usDataType)
        {
            return IntPtr.Size == 4 ? AdsSetScope_32(hIndex, usScopeOption, pucScope, usScopeLen, usDataType) : AdsSetScope_64(hIndex, usScopeOption, pucScope, usScopeLen, usDataType);
        }

        [DllImport("ace32", EntryPoint = "AdsSetScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetScope_32(
        IntPtr hIndex,
        ushort usScopeOption,
        byte[] abScope,
        ushort usScopeLen,
        ushort usDataType);

        [DllImport("ace64", EntryPoint = "AdsSetScope", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetScope_64(
        IntPtr hIndex,
        ushort usScopeOption,
        byte[] abScope,
        ushort usScopeLen,
        ushort usDataType);

        public static uint AdsSetScope(
        IntPtr hIndex,
        ushort usScopeOption,
        byte[] abScope,
        ushort usScopeLen,
        ushort usDataType)
        {
            return IntPtr.Size == 4 ? AdsSetScope_32(hIndex, usScopeOption, abScope, usScopeLen, usDataType) : AdsSetScope_64(hIndex, usScopeOption, abScope, usScopeLen, usDataType);
        }

        [DllImport("ace32", EntryPoint = "AdsSetScopedRelation", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetScopedRelation_32(
        IntPtr hTableParent,
        IntPtr hIndexChild,
        string pucExpr);

        [DllImport("ace64", EntryPoint = "AdsSetScopedRelation", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetScopedRelation_64(
        IntPtr hTableParent,
        IntPtr hIndexChild,
        string pucExpr);

        public static uint AdsSetScopedRelation(
        IntPtr hTableParent,
        IntPtr hIndexChild,
        string pucExpr)
        {
            return IntPtr.Size == 4 ? AdsSetScopedRelation_32(hTableParent, hIndexChild, pucExpr) : AdsSetScopedRelation_64(hTableParent, hIndexChild, pucExpr);
        }

        [DllImport("ace32", EntryPoint = "AdsSetSearchPath", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetSearchPath_32(string pucPath);

        [DllImport("ace64", EntryPoint = "AdsSetSearchPath", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetSearchPath_64(string pucPath);

        public static uint AdsSetSearchPath(string pucPath)
        {
            return IntPtr.Size == 4 ? AdsSetSearchPath_32(pucPath) : AdsSetSearchPath_64(pucPath);
        }

        [DllImport("ace32", EntryPoint = "AdsSetServerType", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetServerType_32(ushort usServerOptions);

        [DllImport("ace64", EntryPoint = "AdsSetServerType", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetServerType_64(ushort usServerOptions);

        public static uint AdsSetServerType(ushort usServerOptions)
        {
            return IntPtr.Size == 4 ? AdsSetServerType_32(usServerOptions) : AdsSetServerType_64(usServerOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsSetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetShort_32(IntPtr hObj, string pucFldName, short sValue);

        [DllImport("ace64", EntryPoint = "AdsSetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetShort_64(IntPtr hObj, string pucFldName, short sValue);

        public static uint AdsSetShort(IntPtr hObj, string pucFldName, short sValue)
        {
            return IntPtr.Size == 4 ? AdsSetShort_32(hObj, pucFldName, sValue) : AdsSetShort_64(hObj, pucFldName, sValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetShort_32(IntPtr hObj, uint lFieldOrdinal, short sValue);

        [DllImport("ace64", EntryPoint = "AdsSetShort", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetShort_64(IntPtr hObj, uint lFieldOrdinal, short sValue);

        public static uint AdsSetShort(IntPtr hObj, uint lFieldOrdinal, short sValue)
        {
            return IntPtr.Size == 4 ? AdsSetShort_32(hObj, lFieldOrdinal, sValue) : AdsSetShort_64(hObj, lFieldOrdinal, sValue);
        }

        [DllImport("ace32", EntryPoint = "AdsSetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetString_32(
        IntPtr hObj,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetString_64(
        IntPtr hObj,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetString(IntPtr hObj, string pucFldName, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetString_32(hObj, pucFldName, pucBuf, ulLen) : AdsSetString_64(hObj, pucFldName, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetString_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetString", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetString_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetString(IntPtr hObj, uint lFieldOrdinal, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetString_32(hObj, lFieldOrdinal, pucBuf, ulLen) : AdsSetString_64(hObj, lFieldOrdinal, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetStringW_32(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        string pwcBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetStringW_64(
        IntPtr hObj,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        string pwcBuf,
        uint ulLen);

        public static uint AdsSetStringW(IntPtr hObj, string pucFldName, string pwcBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetStringW_32(hObj, pucFldName, pwcBuf, ulLen) : AdsSetStringW_64(hObj, pucFldName, pwcBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetStringW_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pwcBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetStringW", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetStringW_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pwcBuf,
        uint ulLen);

        public static uint AdsSetStringW(IntPtr hObj, uint lFieldOrdinal, string pwcBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetStringW_32(hObj, lFieldOrdinal, pwcBuf, ulLen) : AdsSetStringW_64(hObj, lFieldOrdinal, pwcBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetStringFromCodePage", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetStringFromCodePage_32(
        IntPtr hObj,
        uint ulCodePage,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetStringFromCodePage", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetStringFromCodePage_64(
        IntPtr hObj,
        uint ulCodePage,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetStringFromCodePage(
        IntPtr hObj,
        uint ulCodePage,
        string pucFldName,
        string pucBuf,
        uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetStringFromCodePage_32(hObj, ulCodePage, pucFldName, pucBuf, ulLen) : AdsSetStringFromCodePage_64(hObj, ulCodePage, pucFldName, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetStringFromCodePage", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetStringFromCodePage_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetStringFromCodePage", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetStringFromCodePage_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetStringFromCodePage(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetStringFromCodePage_32(hObj, lFieldOrdinal, pucBuf, ulLen) : AdsSetStringFromCodePage_64(hObj, lFieldOrdinal, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTime_32(
        IntPtr hObj,
        string pucFldName,
        string pucValue,
        ushort usLen);

        [DllImport("ace64", EntryPoint = "AdsSetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTime_64(
        IntPtr hObj,
        string pucFldName,
        string pucValue,
        ushort usLen);

        public static uint AdsSetTime(IntPtr hObj, string pucFldName, string pucValue, ushort usLen)
        {
            return IntPtr.Size == 4 ? AdsSetTime_32(hObj, pucFldName, pucValue, usLen) : AdsSetTime_64(hObj, pucFldName, pucValue, usLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTime_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucValue,
        ushort usLen);

        [DllImport("ace64", EntryPoint = "AdsSetTime", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTime_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucValue,
        ushort usLen);

        public static uint AdsSetTime(IntPtr hObj, uint lFieldOrdinal, string pucValue, ushort usLen)
        {
            return IntPtr.Size == 4 ? AdsSetTime_32(hObj, lFieldOrdinal, pucValue, usLen) : AdsSetTime_64(hObj, lFieldOrdinal, pucValue, usLen);
        }

        [DllImport("ace32", EntryPoint = "AdsShowError", CharSet = CharSet.Ansi)]
        private static extern uint AdsShowError_32(string pucTitle);

        [DllImport("ace64", EntryPoint = "AdsShowError", CharSet = CharSet.Ansi)]
        private static extern uint AdsShowError_64(string pucTitle);

        public static uint AdsShowError(string pucTitle)
        {
            return IntPtr.Size == 4 ? AdsShowError_32(pucTitle) : AdsShowError_64(pucTitle);
        }

        [DllImport("ace32", EntryPoint = "AdsSkip", CharSet = CharSet.Ansi)]
        private static extern uint AdsSkip_32(IntPtr hObj, int lRecs);

        [DllImport("ace64", EntryPoint = "AdsSkip", CharSet = CharSet.Ansi)]
        private static extern uint AdsSkip_64(IntPtr hObj, int lRecs);

        public static uint AdsSkip(IntPtr hObj, int lRecs)
        {
            return IntPtr.Size == 4 ? AdsSkip_32(hObj, lRecs) : AdsSkip_64(hObj, lRecs);
        }

        [DllImport("ace32", EntryPoint = "AdsSkipUnique", CharSet = CharSet.Ansi)]
        private static extern uint AdsSkipUnique_32(IntPtr hIndex, int lRecs);

        [DllImport("ace64", EntryPoint = "AdsSkipUnique", CharSet = CharSet.Ansi)]
        private static extern uint AdsSkipUnique_64(IntPtr hIndex, int lRecs);

        public static uint AdsSkipUnique(IntPtr hIndex, int lRecs)
        {
            return IntPtr.Size == 4 ? AdsSkipUnique_32(hIndex, lRecs) : AdsSkipUnique_64(hIndex, lRecs);
        }

        [DllImport("ace32", EntryPoint = "AdsThreadExit", CharSet = CharSet.Ansi)]
        private static extern uint AdsThreadExit_32();

        [DllImport("ace64", EntryPoint = "AdsThreadExit", CharSet = CharSet.Ansi)]
        private static extern uint AdsThreadExit_64();

        public static uint AdsThreadExit()
        {
            return IntPtr.Size == 4 ? AdsThreadExit_32() : AdsThreadExit_64();
        }

        [DllImport("ace32", EntryPoint = "AdsUnlockRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsUnlockRecord_32(IntPtr hTable, uint ulRec);

        [DllImport("ace64", EntryPoint = "AdsUnlockRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsUnlockRecord_64(IntPtr hTable, uint ulRec);

        public static uint AdsUnlockRecord(IntPtr hTable, uint ulRec)
        {
            return IntPtr.Size == 4 ? AdsUnlockRecord_32(hTable, ulRec) : AdsUnlockRecord_64(hTable, ulRec);
        }

        [DllImport("ace32", EntryPoint = "AdsUnlockTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsUnlockTable_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsUnlockTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsUnlockTable_64(IntPtr hTable);

        public static uint AdsUnlockTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsUnlockTable_32(hTable) : AdsUnlockTable_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsVerifyPassword", CharSet = CharSet.Ansi)]
        private static extern uint AdsVerifyPassword_32(IntPtr hTable, out ushort pusEnabled);

        [DllImport("ace64", EntryPoint = "AdsVerifyPassword", CharSet = CharSet.Ansi)]
        private static extern uint AdsVerifyPassword_64(IntPtr hTable, out ushort pusEnabled);

        public static uint AdsVerifyPassword(IntPtr hTable, out ushort pusEnabled)
        {
            return IntPtr.Size == 4 ? AdsVerifyPassword_32(hTable, out pusEnabled) : AdsVerifyPassword_64(hTable, out pusEnabled);
        }

        [DllImport("ace32", EntryPoint = "AdsIsEncryptionEnabled", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsEncryptionEnabled_32(IntPtr hTable, out ushort pusEnabled);

        [DllImport("ace64", EntryPoint = "AdsIsEncryptionEnabled", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsEncryptionEnabled_64(IntPtr hTable, out ushort pusEnabled);

        public static uint AdsIsEncryptionEnabled(IntPtr hTable, out ushort pusEnabled)
        {
            return IntPtr.Size == 4 ? AdsIsEncryptionEnabled_32(hTable, out pusEnabled) : AdsIsEncryptionEnabled_64(hTable, out pusEnabled);
        }

        [DllImport("ace32", EntryPoint = "AdsWriteAllRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsWriteAllRecords_32();

        [DllImport("ace64", EntryPoint = "AdsWriteAllRecords", CharSet = CharSet.Ansi)]
        private static extern uint AdsWriteAllRecords_64();

        public static uint AdsWriteAllRecords()
        {
            return IntPtr.Size == 4 ? AdsWriteAllRecords_32() : AdsWriteAllRecords_64();
        }

        [DllImport("ace32", EntryPoint = "AdsWriteRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsWriteRecord_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsWriteRecord", CharSet = CharSet.Ansi)]
        private static extern uint AdsWriteRecord_64(IntPtr hTable);

        public static uint AdsWriteRecord(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsWriteRecord_32(hTable) : AdsWriteRecord_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsZapTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsZapTable_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsZapTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsZapTable_64(IntPtr hTable);

        public static uint AdsZapTable(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsZapTable_32(hTable) : AdsZapTable_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsSetAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetAOF_32(IntPtr hTable, string pucFilter, ushort usOptions);

        [DllImport("ace64", EntryPoint = "AdsSetAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetAOF_64(IntPtr hTable, string pucFilter, ushort usOptions);

        public static uint AdsSetAOF(IntPtr hTable, string pucFilter, ushort usOptions)
        {
            return IntPtr.Size == 4 ? AdsSetAOF_32(hTable, pucFilter, usOptions) : AdsSetAOF_64(hTable, pucFilter, usOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsSetAOF100", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetAOF100_32(IntPtr hTable, string pvFilter, uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsSetAOF100", CharSet = CharSet.Unicode)]
        private static extern uint AdsSetAOF100_64(IntPtr hTable, string pvFilter, uint ulOptions);

        public static uint AdsSetAOF100(IntPtr hTable, string pvFilter, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsSetAOF100_32(hTable, pvFilter, ulOptions) : AdsSetAOF100_64(hTable, pvFilter, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsEvalAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalAOF_32(
        IntPtr hTable,
        string pucFilter,
        out ushort pusOptLevel);

        [DllImport("ace64", EntryPoint = "AdsEvalAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsEvalAOF_64(
        IntPtr hTable,
        string pucFilter,
        out ushort pusOptLevel);

        public static uint AdsEvalAOF(IntPtr hTable, string pucFilter, out ushort pusOptLevel)
        {
            return IntPtr.Size == 4 ? AdsEvalAOF_32(hTable, pucFilter, out pusOptLevel) : AdsEvalAOF_64(hTable, pucFilter, out pusOptLevel);
        }

        [DllImport("ace32", EntryPoint = "AdsEvalAOF100", CharSet = CharSet.Unicode)]
        private static extern uint AdsEvalAOF100_32(
        IntPtr hTable,
        string pvFilter,
        uint ulOptions,
        out ushort pusOptLevel);

        [DllImport("ace64", EntryPoint = "AdsEvalAOF100", CharSet = CharSet.Unicode)]
        private static extern uint AdsEvalAOF100_64(
        IntPtr hTable,
        string pvFilter,
        uint ulOptions,
        out ushort pusOptLevel);

        public static uint AdsEvalAOF100(
        IntPtr hTable,
        string pvFilter,
        uint ulOptions,
        out ushort pusOptLevel)
        {
            return IntPtr.Size == 4 ? AdsEvalAOF100_32(hTable, pvFilter, ulOptions, out pusOptLevel) : AdsEvalAOF100_64(hTable, pvFilter, ulOptions, out pusOptLevel);
        }

        [DllImport("ace32", EntryPoint = "AdsClearAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearAOF_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsClearAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearAOF_64(IntPtr hTable);

        public static uint AdsClearAOF(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsClearAOF_32(hTable) : AdsClearAOF_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsRefreshAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsRefreshAOF_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsRefreshAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsRefreshAOF_64(IntPtr hTable);

        public static uint AdsRefreshAOF(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsRefreshAOF_32(hTable) : AdsRefreshAOF_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsGetAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAOF_32(IntPtr hTable, [In, Out] char[] pucFilter, ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAOF_64(IntPtr hTable, [In, Out] char[] pucFilter, ref ushort pusLen);

        public static uint AdsGetAOF(IntPtr hTable, char[] pucFilter, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetAOF_32(hTable, pucFilter, ref pusLen) : AdsGetAOF_64(hTable, pucFilter, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetAOF100", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetAOF100_32(
        IntPtr hTable,
        uint ulOptions,
        [In, Out] char[] pvFilter,
        ref uint pulLen);

        [DllImport("ace64", EntryPoint = "AdsGetAOF100", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetAOF100_64(
        IntPtr hTable,
        uint ulOptions,
        [In, Out] char[] pvFilter,
        ref uint pulLen);

        public static uint AdsGetAOF100(
        IntPtr hTable,
        uint ulOptions,
        char[] pvFilter,
        ref uint pulLen)
        {
            return IntPtr.Size == 4 ? AdsGetAOF100_32(hTable, ulOptions, pvFilter, ref pulLen) : AdsGetAOF100_64(hTable, ulOptions, pvFilter, ref pulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetAOFOptLevel", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAOFOptLevel_32(
        IntPtr hTable,
        out ushort pusOptLevel,
        [In, Out] char[] pucNonOpt,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetAOFOptLevel", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetAOFOptLevel_64(
        IntPtr hTable,
        out ushort pusOptLevel,
        [In, Out] char[] pucNonOpt,
        ref ushort pusLen);

        public static uint AdsGetAOFOptLevel(
        IntPtr hTable,
        out ushort pusOptLevel,
        char[] pucNonOpt,
        ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetAOFOptLevel_32(hTable, out pusOptLevel, pucNonOpt, ref pusLen) : AdsGetAOFOptLevel_64(hTable, out pusOptLevel, pucNonOpt, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetAOFOptLevel100", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetAOFOptLevel100_32(
        IntPtr hTable,
        out ushort pusOptLevel,
        [In, Out] char[] pvNonOpt,
        ref uint pulExprLen,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsGetAOFOptLevel100", CharSet = CharSet.Unicode)]
        private static extern uint AdsGetAOFOptLevel100_64(
        IntPtr hTable,
        out ushort pusOptLevel,
        [In, Out] char[] pvNonOpt,
        ref uint pulExprLen,
        uint ulOptions);

        public static uint AdsGetAOFOptLevel100(
        IntPtr hTable,
        out ushort pusOptLevel,
        char[] pvNonOpt,
        ref uint pulExprLen,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsGetAOFOptLevel100_32(hTable, out pusOptLevel, pvNonOpt, ref pulExprLen, ulOptions) : AdsGetAOFOptLevel100_64(hTable, out pusOptLevel, pvNonOpt, ref pulExprLen, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsIsRecordInAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordInAOF_32(
        IntPtr hTable,
        uint ulRecordNum,
        out ushort pusIsInAOF);

        [DllImport("ace64", EntryPoint = "AdsIsRecordInAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsRecordInAOF_64(
        IntPtr hTable,
        uint ulRecordNum,
        out ushort pusIsInAOF);

        public static uint AdsIsRecordInAOF(IntPtr hTable, uint ulRecordNum, out ushort pusIsInAOF)
        {
            return IntPtr.Size == 4 ? AdsIsRecordInAOF_32(hTable, ulRecordNum, out pusIsInAOF) : AdsIsRecordInAOF_64(hTable, ulRecordNum, out pusIsInAOF);
        }

        [DllImport("ace32", EntryPoint = "AdsCustomizeAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsCustomizeAOF_32(
        IntPtr hTable,
        uint ulNumRecords,
        out uint pulRecords,
        ushort usOption);

        [DllImport("ace64", EntryPoint = "AdsCustomizeAOF", CharSet = CharSet.Ansi)]
        private static extern uint AdsCustomizeAOF_64(
        IntPtr hTable,
        uint ulNumRecords,
        out uint pulRecords,
        ushort usOption);

        public static uint AdsCustomizeAOF(
        IntPtr hTable,
        uint ulNumRecords,
        out uint pulRecords,
        ushort usOption)
        {
            return IntPtr.Size == 4 ? AdsCustomizeAOF_32(hTable, ulNumRecords, out pulRecords, usOption) : AdsCustomizeAOF_64(hTable, ulNumRecords, out pulRecords, usOption);
        }

        [DllImport("ace32", EntryPoint = "AdsInitRawKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsInitRawKey_32(IntPtr hIndex);

        [DllImport("ace64", EntryPoint = "AdsInitRawKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsInitRawKey_64(IntPtr hIndex);

        public static uint AdsInitRawKey(IntPtr hIndex)
        {
            return IntPtr.Size == 4 ? AdsInitRawKey_32(hIndex) : AdsInitRawKey_64(hIndex);
        }

        [DllImport("ace32", EntryPoint = "AdsBuildRawKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsBuildRawKey_32(
        IntPtr hIndex,
        [In, Out] byte[] pucKey,
        ref ushort pusKeyLen);

        [DllImport("ace64", EntryPoint = "AdsBuildRawKey", CharSet = CharSet.Ansi)]
        private static extern uint AdsBuildRawKey_64(
        IntPtr hIndex,
        [In, Out] byte[] pucKey,
        ref ushort pusKeyLen);

        public static uint AdsBuildRawKey(IntPtr hIndex, byte[] pucKey, ref ushort pusKeyLen)
        {
            return IntPtr.Size == 4 ? AdsBuildRawKey_32(hIndex, pucKey, ref pusKeyLen) : AdsBuildRawKey_64(hIndex, pucKey, ref pusKeyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsBuildRawKey100", CharSet = CharSet.Ansi)]
        private static extern uint AdsBuildRawKey100_32(
        IntPtr hIndex,
        [In, Out] byte[] pucKey,
        ref ushort pusKeyLen,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsBuildRawKey100", CharSet = CharSet.Ansi)]
        private static extern uint AdsBuildRawKey100_64(
        IntPtr hIndex,
        [In, Out] byte[] pucKey,
        ref ushort pusKeyLen,
        uint ulOptions);

        public static uint AdsBuildRawKey100(
        IntPtr hIndex,
        byte[] pucKey,
        ref ushort pusKeyLen,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsBuildRawKey100_32(hIndex, pucKey, ref pusKeyLen, ulOptions) : AdsBuildRawKey100_64(hIndex, pucKey, ref pusKeyLen, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateSQLStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateSQLStatement_32(IntPtr hConnect, out IntPtr phStatement);

        [DllImport("ace64", EntryPoint = "AdsCreateSQLStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateSQLStatement_64(IntPtr hConnect, out IntPtr phStatement);

        public static uint AdsCreateSQLStatement(IntPtr hConnect, out IntPtr phStatement)
        {
            return IntPtr.Size == 4 ? AdsCreateSQLStatement_32(hConnect, out phStatement) : AdsCreateSQLStatement_64(hConnect, out phStatement);
        }

        [DllImport("ace32", EntryPoint = "AdsPrepareSQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsPrepareSQL_32(IntPtr hStatement, string pucSQL);

        [DllImport("ace64", EntryPoint = "AdsPrepareSQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsPrepareSQL_64(IntPtr hStatement, string pucSQL);

        public static uint AdsPrepareSQL(IntPtr hStatement, string pucSQL)
        {
            return IntPtr.Size == 4 ? AdsPrepareSQL_32(hStatement, pucSQL) : AdsPrepareSQL_64(hStatement, pucSQL);
        }

        [DllImport("ace32", EntryPoint = "AdsPrepareSQLW", CharSet = CharSet.Unicode)]
        private static extern uint AdsPrepareSQLW_32(IntPtr hStatement, string pwcSQL);

        [DllImport("ace64", EntryPoint = "AdsPrepareSQLW", CharSet = CharSet.Unicode)]
        private static extern uint AdsPrepareSQLW_64(IntPtr hStatement, string pwcSQL);

        public static uint AdsPrepareSQLW(IntPtr hStatement, string pwcSQL)
        {
            return IntPtr.Size == 4 ? AdsPrepareSQLW_32(hStatement, pwcSQL) : AdsPrepareSQLW_64(hStatement, pwcSQL);
        }

        [DllImport("ace32", EntryPoint = "AdsCachePrepareSQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsCachePrepareSQL_32(
        IntPtr hConnect,
        string pucSQL,
        out IntPtr phStatement);

        [DllImport("ace64", EntryPoint = "AdsCachePrepareSQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsCachePrepareSQL_64(
        IntPtr hConnect,
        string pucSQL,
        out IntPtr phStatement);

        public static uint AdsCachePrepareSQL(IntPtr hConnect, string pucSQL, out IntPtr phStatement)
        {
            return IntPtr.Size == 4 ? AdsCachePrepareSQL_32(hConnect, pucSQL, out phStatement) : AdsCachePrepareSQL_64(hConnect, pucSQL, out phStatement);
        }

        [DllImport("ace32", EntryPoint = "AdsCachePrepareSQLW", CharSet = CharSet.Unicode)]
        private static extern uint AdsCachePrepareSQLW_32(
        IntPtr hConnect,
        string pwcSQL,
        out IntPtr phStatement);

        [DllImport("ace64", EntryPoint = "AdsCachePrepareSQLW", CharSet = CharSet.Unicode)]
        private static extern uint AdsCachePrepareSQLW_64(
        IntPtr hConnect,
        string pwcSQL,
        out IntPtr phStatement);

        public static uint AdsCachePrepareSQLW(IntPtr hConnect, string pwcSQL, out IntPtr phStatement)
        {
            return IntPtr.Size == 4 ? AdsCachePrepareSQLW_32(hConnect, pwcSQL, out phStatement) : AdsCachePrepareSQLW_64(hConnect, pwcSQL, out phStatement);
        }

        [DllImport("ace32", EntryPoint = "AdsExecuteSQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsExecuteSQL_32(IntPtr hStatement, out IntPtr phCursor);

        [DllImport("ace64", EntryPoint = "AdsExecuteSQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsExecuteSQL_64(IntPtr hStatement, out IntPtr phCursor);

        public static uint AdsExecuteSQL(IntPtr hStatement, out IntPtr phCursor)
        {
            return IntPtr.Size == 4 ? AdsExecuteSQL_32(hStatement, out phCursor) : AdsExecuteSQL_64(hStatement, out phCursor);
        }

        [DllImport("ace32", EntryPoint = "AdsExecuteSQLDirect", CharSet = CharSet.Ansi)]
        private static extern uint AdsExecuteSQLDirect_32(
        IntPtr hStatement,
        string pucSQL,
        out IntPtr phCursor);

        [DllImport("ace64", EntryPoint = "AdsExecuteSQLDirect", CharSet = CharSet.Ansi)]
        private static extern uint AdsExecuteSQLDirect_64(
        IntPtr hStatement,
        string pucSQL,
        out IntPtr phCursor);

        public static uint AdsExecuteSQLDirect(IntPtr hStatement, string pucSQL, out IntPtr phCursor)
        {
            return IntPtr.Size == 4 ? AdsExecuteSQLDirect_32(hStatement, pucSQL, out phCursor) : AdsExecuteSQLDirect_64(hStatement, pucSQL, out phCursor);
        }

        [DllImport("ace32", EntryPoint = "AdsExecuteSQLDirectW", CharSet = CharSet.Unicode)]
        private static extern uint AdsExecuteSQLDirectW_32(
        IntPtr hStatement,
        string pwcSQL,
        out IntPtr phCursor);

        [DllImport("ace64", EntryPoint = "AdsExecuteSQLDirectW", CharSet = CharSet.Unicode)]
        private static extern uint AdsExecuteSQLDirectW_64(
        IntPtr hStatement,
        string pwcSQL,
        out IntPtr phCursor);

        public static uint AdsExecuteSQLDirectW(IntPtr hStatement, string pwcSQL, out IntPtr phCursor)
        {
            return IntPtr.Size == 4 ? AdsExecuteSQLDirectW_32(hStatement, pwcSQL, out phCursor) : AdsExecuteSQLDirectW_64(hStatement, pwcSQL, out phCursor);
        }

        [DllImport("ace32", EntryPoint = "AdsCloseSQLStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseSQLStatement_32(IntPtr hStatement);

        [DllImport("ace64", EntryPoint = "AdsCloseSQLStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsCloseSQLStatement_64(IntPtr hStatement);

        public static uint AdsCloseSQLStatement(IntPtr hStatement)
        {
            return IntPtr.Size == 4 ? AdsCloseSQLStatement_32(hStatement) : AdsCloseSQLStatement_64(hStatement);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtSetTableRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableRights_32(IntPtr hStatement, ushort usCheckRights);

        [DllImport("ace64", EntryPoint = "AdsStmtSetTableRights", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableRights_64(IntPtr hStatement, ushort usCheckRights);

        public static uint AdsStmtSetTableRights(IntPtr hStatement, ushort usCheckRights)
        {
            return IntPtr.Size == 4 ? AdsStmtSetTableRights_32(hStatement, usCheckRights) : AdsStmtSetTableRights_64(hStatement, usCheckRights);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtSetTableReadOnly", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableReadOnly_32(IntPtr hStatement, ushort usReadOnly);

        [DllImport("ace64", EntryPoint = "AdsStmtSetTableReadOnly", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableReadOnly_64(IntPtr hStatement, ushort usReadOnly);

        public static uint AdsStmtSetTableReadOnly(IntPtr hStatement, ushort usReadOnly)
        {
            return IntPtr.Size == 4 ? AdsStmtSetTableReadOnly_32(hStatement, usReadOnly) : AdsStmtSetTableReadOnly_64(hStatement, usReadOnly);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtSetTableLockType", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableLockType_32(IntPtr hStatement, ushort usLockType);

        [DllImport("ace64", EntryPoint = "AdsStmtSetTableLockType", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableLockType_64(IntPtr hStatement, ushort usLockType);

        public static uint AdsStmtSetTableLockType(IntPtr hStatement, ushort usLockType)
        {
            return IntPtr.Size == 4 ? AdsStmtSetTableLockType_32(hStatement, usLockType) : AdsStmtSetTableLockType_64(hStatement, usLockType);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtSetTableCharType", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableCharType_32(IntPtr hStatement, ushort usCharType);

        [DllImport("ace64", EntryPoint = "AdsStmtSetTableCharType", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableCharType_64(IntPtr hStatement, ushort usCharType);

        public static uint AdsStmtSetTableCharType(IntPtr hStatement, ushort usCharType)
        {
            return IntPtr.Size == 4 ? AdsStmtSetTableCharType_32(hStatement, usCharType) : AdsStmtSetTableCharType_64(hStatement, usCharType);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtSetTableType", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableType_32(IntPtr hStatement, ushort usTableType);

        [DllImport("ace64", EntryPoint = "AdsStmtSetTableType", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableType_64(IntPtr hStatement, ushort usTableType);

        public static uint AdsStmtSetTableType(IntPtr hStatement, ushort usTableType)
        {
            return IntPtr.Size == 4 ? AdsStmtSetTableType_32(hStatement, usTableType) : AdsStmtSetTableType_64(hStatement, usTableType);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtSetTableCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableCollation_32(IntPtr hStatement, string pucCollation);

        [DllImport("ace64", EntryPoint = "AdsStmtSetTableCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTableCollation_64(IntPtr hStatement, string pucCollation);

        public static uint AdsStmtSetTableCollation(IntPtr hStatement, string pucCollation)
        {
            return IntPtr.Size == 4 ? AdsStmtSetTableCollation_32(hStatement, pucCollation) : AdsStmtSetTableCollation_64(hStatement, pucCollation);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtConstrainUpdates", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtConstrainUpdates_32(IntPtr hStatement, ushort usConstrain);

        [DllImport("ace64", EntryPoint = "AdsStmtConstrainUpdates", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtConstrainUpdates_64(IntPtr hStatement, ushort usConstrain);

        public static uint AdsStmtConstrainUpdates(IntPtr hStatement, ushort usConstrain)
        {
            return IntPtr.Size == 4 ? AdsStmtConstrainUpdates_32(hStatement, usConstrain) : AdsStmtConstrainUpdates_64(hStatement, usConstrain);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtEnableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtEnableEncryption_32(IntPtr hStatement, string pucPassword);

        [DllImport("ace64", EntryPoint = "AdsStmtEnableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtEnableEncryption_64(IntPtr hStatement, string pucPassword);

        public static uint AdsStmtEnableEncryption(IntPtr hStatement, string pucPassword)
        {
            return IntPtr.Size == 4 ? AdsStmtEnableEncryption_32(hStatement, pucPassword) : AdsStmtEnableEncryption_64(hStatement, pucPassword);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtDisableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtDisableEncryption_32(IntPtr hStatement);

        [DllImport("ace64", EntryPoint = "AdsStmtDisableEncryption", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtDisableEncryption_64(IntPtr hStatement);

        public static uint AdsStmtDisableEncryption(IntPtr hStatement)
        {
            return IntPtr.Size == 4 ? AdsStmtDisableEncryption_32(hStatement) : AdsStmtDisableEncryption_64(hStatement);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtSetTablePassword", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTablePassword_32(
        IntPtr hStatement,
        string pucTableName,
        string pucPassword);

        [DllImport("ace64", EntryPoint = "AdsStmtSetTablePassword", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtSetTablePassword_64(
        IntPtr hStatement,
        string pucTableName,
        string pucPassword);

        public static uint AdsStmtSetTablePassword(
        IntPtr hStatement,
        string pucTableName,
        string pucPassword)
        {
            return IntPtr.Size == 4 ? AdsStmtSetTablePassword_32(hStatement, pucTableName, pucPassword) : AdsStmtSetTablePassword_64(hStatement, pucTableName, pucPassword);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtClearTablePasswords", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtClearTablePasswords_32(IntPtr hStatement);

        [DllImport("ace64", EntryPoint = "AdsStmtClearTablePasswords", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtClearTablePasswords_64(IntPtr hStatement);

        public static uint AdsStmtClearTablePasswords(IntPtr hStatement)
        {
            return IntPtr.Size == 4 ? AdsStmtClearTablePasswords_32(hStatement) : AdsStmtClearTablePasswords_64(hStatement);
        }

        [DllImport("ace32", EntryPoint = "AdsStmtReadAllColumns", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtReadAllColumns_32(IntPtr hStatement, ushort usReadColumns);

        [DllImport("ace64", EntryPoint = "AdsStmtReadAllColumns", CharSet = CharSet.Ansi)]
        private static extern uint AdsStmtReadAllColumns_64(IntPtr hStatement, ushort usReadColumns);

        public static uint AdsStmtReadAllColumns(IntPtr hStatement, ushort usReadColumns)
        {
            return IntPtr.Size == 4 ? AdsStmtReadAllColumns_32(hStatement, usReadColumns) : AdsStmtReadAllColumns_64(hStatement, usReadColumns);
        }

        [DllImport("ace32", EntryPoint = "AdsClearSQLParams", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearSQLParams_32(IntPtr hStatement);

        [DllImport("ace64", EntryPoint = "AdsClearSQLParams", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearSQLParams_64(IntPtr hStatement);

        public static uint AdsClearSQLParams(IntPtr hStatement)
        {
            return IntPtr.Size == 4 ? AdsClearSQLParams_32(hStatement) : AdsClearSQLParams_64(hStatement);
        }

        [DllImport("ace32", EntryPoint = "AdsSetTimeStamp", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStamp_32(
        IntPtr hObj,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetTimeStamp", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStamp_64(
        IntPtr hObj,
        string pucFldName,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetTimeStamp(IntPtr hObj, string pucFldName, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetTimeStamp_32(hObj, pucFldName, pucBuf, ulLen) : AdsSetTimeStamp_64(hObj, pucFldName, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsSetTimeStamp", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStamp_32(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        [DllImport("ace64", EntryPoint = "AdsSetTimeStamp", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTimeStamp_64(
        IntPtr hObj,
        uint lFieldOrdinal,
        string pucBuf,
        uint ulLen);

        public static uint AdsSetTimeStamp(IntPtr hObj, uint lFieldOrdinal, string pucBuf, uint ulLen)
        {
            return IntPtr.Size == 4 ? AdsSetTimeStamp_32(hObj, lFieldOrdinal, pucBuf, ulLen) : AdsSetTimeStamp_64(hObj, lFieldOrdinal, pucBuf, ulLen);
        }

        [DllImport("ace32", EntryPoint = "AdsClearSQLAbortFunc", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearSQLAbortFunc_32();

        [DllImport("ace64", EntryPoint = "AdsClearSQLAbortFunc", CharSet = CharSet.Ansi)]
        private static extern uint AdsClearSQLAbortFunc_64();

        public static uint AdsClearSQLAbortFunc()
        {
            return IntPtr.Size == 4 ? AdsClearSQLAbortFunc_32() : AdsClearSQLAbortFunc_64();
        }

        [DllImport("ace32", EntryPoint = "AdsGetNumParams", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumParams_32(IntPtr hStatement, out ushort pusNumParams);

        [DllImport("ace64", EntryPoint = "AdsGetNumParams", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetNumParams_64(IntPtr hStatement, out ushort pusNumParams);

        public static uint AdsGetNumParams(IntPtr hStatement, out ushort pusNumParams)
        {
            return IntPtr.Size == 4 ? AdsGetNumParams_32(hStatement, out pusNumParams) : AdsGetNumParams_64(hStatement, out pusNumParams);
        }

        [DllImport("ace32", EntryPoint = "AdsGetLastAutoinc", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLastAutoinc_32(IntPtr hObj, out uint pulAutoIncVal);

        [DllImport("ace64", EntryPoint = "AdsGetLastAutoinc", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetLastAutoinc_64(IntPtr hObj, out uint pulAutoIncVal);

        public static uint AdsGetLastAutoinc(IntPtr hObj, out uint pulAutoIncVal)
        {
            return IntPtr.Size == 4 ? AdsGetLastAutoinc_32(hObj, out pulAutoIncVal) : AdsGetLastAutoinc_64(hObj, out pulAutoIncVal);
        }

        [DllImport("ace32", EntryPoint = "AdsIsIndexUserDefined", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexUserDefined_32(IntPtr hIndex, out ushort pbUserDefined);

        [DllImport("ace64", EntryPoint = "AdsIsIndexUserDefined", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsIndexUserDefined_64(IntPtr hIndex, out ushort pbUserDefined);

        public static uint AdsIsIndexUserDefined(IntPtr hIndex, out ushort pbUserDefined)
        {
            return IntPtr.Size == 4 ? AdsIsIndexUserDefined_32(hIndex, out pbUserDefined) : AdsIsIndexUserDefined_64(hIndex, out pbUserDefined);
        }

        [DllImport("ace32", EntryPoint = "AdsRestructureTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestructureTable_32(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields);

        [DllImport("ace64", EntryPoint = "AdsRestructureTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestructureTable_64(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields);

        public static uint AdsRestructureTable(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields)
        {
            return IntPtr.Size == 4 ? AdsRestructureTable_32(hObj, pucName, pucPassword, usTableType, usCharType, usLockType, usCheckRights, pucAddFields, pucDeleteFields, pucChangeFields) : AdsRestructureTable_64(hObj, pucName, pucPassword, usTableType, usCharType, usLockType, usCheckRights, pucAddFields, pucDeleteFields, pucChangeFields);
        }

        [DllImport("ace32", EntryPoint = "AdsRestructureTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestructureTable90_32(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields,
        string pucCollation);

        [DllImport("ace64", EntryPoint = "AdsRestructureTable90", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestructureTable90_64(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields,
        string pucCollation);

        public static uint AdsRestructureTable90(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields,
        string pucCollation)
        {
            return IntPtr.Size == 4 ? AdsRestructureTable90_32(hObj, pucName, pucPassword, usTableType, usCharType, usLockType, usCheckRights, pucAddFields, pucDeleteFields, pucChangeFields, pucCollation) : AdsRestructureTable90_64(hObj, pucName, pucPassword, usTableType, usCharType, usLockType, usCheckRights, pucAddFields, pucDeleteFields, pucChangeFields, pucCollation);
        }

        [DllImport("ace32", EntryPoint = "AdsRestructureTable120", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestructureTable120_32(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields,
        string pucCollation,
        uint ulMemoBlockSize,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsRestructureTable120", CharSet = CharSet.Ansi)]
        private static extern uint AdsRestructureTable120_64(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields,
        string pucCollation,
        uint ulMemoBlockSize,
        uint ulOptions);

        public static uint AdsRestructureTable120(
        IntPtr hObj,
        string pucName,
        string pucPassword,
        ushort usTableType,
        ushort usCharType,
        ushort usLockType,
        ushort usCheckRights,
        string pucAddFields,
        string pucDeleteFields,
        string pucChangeFields,
        string pucCollation,
        uint ulMemoBlockSize,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsRestructureTable120_32(hObj, pucName, pucPassword, usTableType, usCharType, usLockType, usCheckRights, pucAddFields, pucDeleteFields, pucChangeFields, pucCollation, ulMemoBlockSize, ulOptions) : AdsRestructureTable120_64(hObj, pucName, pucPassword, usTableType, usCharType, usLockType, usCheckRights, pucAddFields, pucDeleteFields, pucChangeFields, pucCollation, ulMemoBlockSize, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsGetSQLStatementHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSQLStatementHandle_32(IntPtr hCursor, out IntPtr phStmt);

        [DllImport("ace64", EntryPoint = "AdsGetSQLStatementHandle", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSQLStatementHandle_64(IntPtr hCursor, out IntPtr phStmt);

        public static uint AdsGetSQLStatementHandle(IntPtr hCursor, out IntPtr phStmt)
        {
            return IntPtr.Size == 4 ? AdsGetSQLStatementHandle_32(hCursor, out phStmt) : AdsGetSQLStatementHandle_64(hCursor, out phStmt);
        }

        [DllImport("ace32", EntryPoint = "AdsGetSQLStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSQLStatement_32(
        IntPtr hStmt,
        [In, Out] char[] pucSQL,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetSQLStatement", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetSQLStatement_64(
        IntPtr hStmt,
        [In, Out] char[] pucSQL,
        ref ushort pusLen);

        public static uint AdsGetSQLStatement(IntPtr hStmt, char[] pucSQL, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetSQLStatement_32(hStmt, pucSQL, ref pusLen) : AdsGetSQLStatement_64(hStmt, pucSQL, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsFlushFileBuffers", CharSet = CharSet.Ansi)]
        private static extern uint AdsFlushFileBuffers_32(IntPtr hTable);

        [DllImport("ace64", EntryPoint = "AdsFlushFileBuffers", CharSet = CharSet.Ansi)]
        private static extern uint AdsFlushFileBuffers_64(IntPtr hTable);

        public static uint AdsFlushFileBuffers(IntPtr hTable)
        {
            return IntPtr.Size == 4 ? AdsFlushFileBuffers_32(hTable) : AdsFlushFileBuffers_64(hTable);
        }

        [DllImport("ace32", EntryPoint = "AdsDDDeployDatabase", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeployDatabase_32(
        string pucDestination,
        string pucDestinationPassword,
        string pucSource,
        string pucSourcePassword,
        ushort usServerTypes,
        ushort usValidateOption,
        ushort usBackupFiles,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsDDDeployDatabase", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDDeployDatabase_64(
        string pucDestination,
        string pucDestinationPassword,
        string pucSource,
        string pucSourcePassword,
        ushort usServerTypes,
        ushort usValidateOption,
        ushort usBackupFiles,
        uint ulOptions);

        public static uint AdsDDDeployDatabase(
        string pucDestination,
        string pucDestinationPassword,
        string pucSource,
        string pucSourcePassword,
        ushort usServerTypes,
        ushort usValidateOption,
        ushort usBackupFiles,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsDDDeployDatabase_32(pucDestination, pucDestinationPassword, pucSource, pucSourcePassword, usServerTypes, usValidateOption, usBackupFiles, ulOptions) : AdsDDDeployDatabase_64(pucDestination, pucDestinationPassword, pucSource, pucSourcePassword, usServerTypes, usValidateOption, usBackupFiles, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsVerifySQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsVerifySQL_32(IntPtr hStatement, string pucSQL);

        [DllImport("ace64", EntryPoint = "AdsVerifySQL", CharSet = CharSet.Ansi)]
        private static extern uint AdsVerifySQL_64(IntPtr hStatement, string pucSQL);

        public static uint AdsVerifySQL(IntPtr hStatement, string pucSQL)
        {
            return IntPtr.Size == 4 ? AdsVerifySQL_32(hStatement, pucSQL) : AdsVerifySQL_64(hStatement, pucSQL);
        }

        [DllImport("ace32", EntryPoint = "AdsVerifySQLW", CharSet = CharSet.Unicode)]
        private static extern uint AdsVerifySQLW_32(IntPtr hStatement, string pwcSQL);

        [DllImport("ace64", EntryPoint = "AdsVerifySQLW", CharSet = CharSet.Unicode)]
        private static extern uint AdsVerifySQLW_64(IntPtr hStatement, string pwcSQL);

        public static uint AdsVerifySQLW(IntPtr hStatement, string pwcSQL)
        {
            return IntPtr.Size == 4 ? AdsVerifySQLW_32(hStatement, pwcSQL) : AdsVerifySQLW_64(hStatement, pwcSQL);
        }

        [DllImport("ace32", EntryPoint = "AdsDisableUniqueEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableUniqueEnforcement_32(IntPtr hConnection);

        [DllImport("ace64", EntryPoint = "AdsDisableUniqueEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableUniqueEnforcement_64(IntPtr hConnection);

        public static uint AdsDisableUniqueEnforcement(IntPtr hConnection)
        {
            return IntPtr.Size == 4 ? AdsDisableUniqueEnforcement_32(hConnection) : AdsDisableUniqueEnforcement_64(hConnection);
        }

        [DllImport("ace32", EntryPoint = "AdsEnableUniqueEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableUniqueEnforcement_32(IntPtr hConnection);

        [DllImport("ace64", EntryPoint = "AdsEnableUniqueEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableUniqueEnforcement_64(IntPtr hConnection);

        public static uint AdsEnableUniqueEnforcement(IntPtr hConnection)
        {
            return IntPtr.Size == 4 ? AdsEnableUniqueEnforcement_32(hConnection) : AdsEnableUniqueEnforcement_64(hConnection);
        }

        [DllImport("ace32", EntryPoint = "AdsDisableRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableRI_32(IntPtr hConnection);

        [DllImport("ace64", EntryPoint = "AdsDisableRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableRI_64(IntPtr hConnection);

        public static uint AdsDisableRI(IntPtr hConnection)
        {
            return IntPtr.Size == 4 ? AdsDisableRI_32(hConnection) : AdsDisableRI_64(hConnection);
        }

        [DllImport("ace32", EntryPoint = "AdsEnableRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableRI_32(IntPtr hConnection);

        [DllImport("ace64", EntryPoint = "AdsEnableRI", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableRI_64(IntPtr hConnection);

        public static uint AdsEnableRI(IntPtr hConnection)
        {
            return IntPtr.Size == 4 ? AdsEnableRI_32(hConnection) : AdsEnableRI_64(hConnection);
        }

        [DllImport("ace32", EntryPoint = "AdsDisableAutoIncEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableAutoIncEnforcement_32(IntPtr hConnection);

        [DllImport("ace64", EntryPoint = "AdsDisableAutoIncEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsDisableAutoIncEnforcement_64(IntPtr hConnection);

        public static uint AdsDisableAutoIncEnforcement(IntPtr hConnection)
        {
            return IntPtr.Size == 4 ? AdsDisableAutoIncEnforcement_32(hConnection) : AdsDisableAutoIncEnforcement_64(hConnection);
        }

        [DllImport("ace32", EntryPoint = "AdsEnableAutoIncEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableAutoIncEnforcement_32(IntPtr hConnection);

        [DllImport("ace64", EntryPoint = "AdsEnableAutoIncEnforcement", CharSet = CharSet.Ansi)]
        private static extern uint AdsEnableAutoIncEnforcement_64(IntPtr hConnection);

        public static uint AdsEnableAutoIncEnforcement(IntPtr hConnection)
        {
            return IntPtr.Size == 4 ? AdsEnableAutoIncEnforcement_32(hConnection) : AdsEnableAutoIncEnforcement_64(hConnection);
        }

        [DllImport("ace32", EntryPoint = "AdsRollbackTransaction80", CharSet = CharSet.Ansi)]
        private static extern uint AdsRollbackTransaction80_32(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsRollbackTransaction80", CharSet = CharSet.Ansi)]
        private static extern uint AdsRollbackTransaction80_64(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        public static uint AdsRollbackTransaction80(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsRollbackTransaction80_32(hConnect, pucSavepoint, ulOptions) : AdsRollbackTransaction80_64(hConnect, pucSavepoint, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsCreateSavepoint", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateSavepoint_32(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsCreateSavepoint", CharSet = CharSet.Ansi)]
        private static extern uint AdsCreateSavepoint_64(
        IntPtr hConnect,
        string pucSavepoint,
        uint ulOptions);

        public static uint AdsCreateSavepoint(IntPtr hConnect, string pucSavepoint, uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsCreateSavepoint_32(hConnect, pucSavepoint, ulOptions) : AdsCreateSavepoint_64(hConnect, pucSavepoint, ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsDDFreeTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFreeTable_32(string pucTableName, string pucPassword);

        [DllImport("ace64", EntryPoint = "AdsDDFreeTable", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDFreeTable_64(string pucTableName, string pucPassword);

        public static uint AdsDDFreeTable(string pucTableName, string pucPassword)
        {
            return IntPtr.Size == 4 ? AdsDDFreeTable_32(pucTableName, pucPassword) : AdsDDFreeTable_64(pucTableName, pucPassword);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetIndexProperty_32(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetIndexProperty_64(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetIndexProperty(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetIndexProperty_32(hAdminConn, pucTableName, pucIndexName, usPropertyID, pvProperty, usPropertyLen) : AdsDDSetIndexProperty_64(hAdminConn, pucTableName, pucIndexName, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetIndexProperty_32(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetIndexProperty_64(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        [In, Out] char[] pucProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetIndexProperty(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        char[] pucProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetIndexProperty_32(hAdminConn, pucTableName, pucIndexName, usPropertyID, pucProperty, usPropertyLen) : AdsDDSetIndexProperty_64(hAdminConn, pucTableName, pucIndexName, usPropertyID, pucProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsDDSetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetIndexProperty_32(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsDDSetIndexProperty", CharSet = CharSet.Ansi)]
        private static extern uint AdsDDSetIndexProperty_64(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen);

        public static uint AdsDDSetIndexProperty(
        IntPtr hAdminConn,
        string pucTableName,
        string pucIndexName,
        ushort usPropertyID,
        ref ushort pusProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsDDSetIndexProperty_32(hAdminConn, pucTableName, pucIndexName, usPropertyID, ref pusProperty, usPropertyLen) : AdsDDSetIndexProperty_64(hAdminConn, pucTableName, pucIndexName, usPropertyID, ref pusProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsIsFieldBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsFieldBinary_32(
        IntPtr hTable,
        string pucFldName,
        out ushort pbBinary);

        [DllImport("ace64", EntryPoint = "AdsIsFieldBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsFieldBinary_64(
        IntPtr hTable,
        string pucFldName,
        out ushort pbBinary);

        public static uint AdsIsFieldBinary(IntPtr hTable, string pucFldName, out ushort pbBinary)
        {
            return IntPtr.Size == 4 ? AdsIsFieldBinary_32(hTable, pucFldName, out pbBinary) : AdsIsFieldBinary_64(hTable, pucFldName, out pbBinary);
        }

        [DllImport("ace32", EntryPoint = "AdsIsFieldBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsFieldBinary_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pbBinary);

        [DllImport("ace64", EntryPoint = "AdsIsFieldBinary", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsFieldBinary_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pbBinary);

        public static uint AdsIsFieldBinary(IntPtr hTable, uint lFieldOrdinal, out ushort pbBinary)
        {
            return IntPtr.Size == 4 ? AdsIsFieldBinary_32(hTable, lFieldOrdinal, out pbBinary) : AdsIsFieldBinary_64(hTable, lFieldOrdinal, out pbBinary);
        }

        [DllImport("ace32", EntryPoint = "AdsIsNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNull_32(IntPtr hTable, string pucFldName, out ushort pbNull);

        [DllImport("ace64", EntryPoint = "AdsIsNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNull_64(IntPtr hTable, string pucFldName, out ushort pbNull);

        public static uint AdsIsNull(IntPtr hTable, string pucFldName, out ushort pbNull)
        {
            return IntPtr.Size == 4 ? AdsIsNull_32(hTable, pucFldName, out pbNull) : AdsIsNull_64(hTable, pucFldName, out pbNull);
        }

        [DllImport("ace32", EntryPoint = "AdsIsNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNull_32(IntPtr hTable, uint lFieldOrdinal, out ushort pbNull);

        [DllImport("ace64", EntryPoint = "AdsIsNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNull_64(IntPtr hTable, uint lFieldOrdinal, out ushort pbNull);

        public static uint AdsIsNull(IntPtr hTable, uint lFieldOrdinal, out ushort pbNull)
        {
            return IntPtr.Size == 4 ? AdsIsNull_32(hTable, lFieldOrdinal, out pbNull) : AdsIsNull_64(hTable, lFieldOrdinal, out pbNull);
        }

        [DllImport("ace32", EntryPoint = "AdsIsNullable", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNullable_32(
        IntPtr hTable,
        string pucFldName,
        out ushort pbNullable);

        [DllImport("ace64", EntryPoint = "AdsIsNullable", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNullable_64(
        IntPtr hTable,
        string pucFldName,
        out ushort pbNullable);

        public static uint AdsIsNullable(IntPtr hTable, string pucFldName, out ushort pbNullable)
        {
            return IntPtr.Size == 4 ? AdsIsNullable_32(hTable, pucFldName, out pbNullable) : AdsIsNullable_64(hTable, pucFldName, out pbNullable);
        }

        [DllImport("ace32", EntryPoint = "AdsIsNullable", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNullable_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pbNullable);

        [DllImport("ace64", EntryPoint = "AdsIsNullable", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsNullable_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        out ushort pbNullable);

        public static uint AdsIsNullable(IntPtr hTable, uint lFieldOrdinal, out ushort pbNullable)
        {
            return IntPtr.Size == 4 ? AdsIsNullable_32(hTable, lFieldOrdinal, out pbNullable) : AdsIsNullable_64(hTable, lFieldOrdinal, out pbNullable);
        }

        [DllImport("ace32", EntryPoint = "AdsSetNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetNull_32(IntPtr hTable, string pucFldName);

        [DllImport("ace64", EntryPoint = "AdsSetNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetNull_64(IntPtr hTable, string pucFldName);

        public static uint AdsSetNull(IntPtr hTable, string pucFldName)
        {
            return IntPtr.Size == 4 ? AdsSetNull_32(hTable, pucFldName) : AdsSetNull_64(hTable, pucFldName);
        }

        [DllImport("ace32", EntryPoint = "AdsSetNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetNull_32(IntPtr hTable, uint lFieldOrdinal);

        [DllImport("ace64", EntryPoint = "AdsSetNull", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetNull_64(IntPtr hTable, uint lFieldOrdinal);

        public static uint AdsSetNull(IntPtr hTable, uint lFieldOrdinal)
        {
            return IntPtr.Size == 4 ? AdsSetNull_32(hTable, lFieldOrdinal) : AdsSetNull_64(hTable, lFieldOrdinal);
        }

        [DllImport("ace32", EntryPoint = "AdsGetTableCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableCollation_32(
        IntPtr hTbl,
        [In, Out] char[] pucCollation,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetTableCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetTableCollation_64(
        IntPtr hTbl,
        [In, Out] char[] pucCollation,
        ref ushort pusLen);

        public static uint AdsGetTableCollation(IntPtr hTbl, char[] pucCollation, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetTableCollation_32(hTbl, pucCollation, ref pusLen) : AdsGetTableCollation_64(hTbl, pucCollation, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetIndexCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexCollation_32(
        IntPtr hIndex,
        [In, Out] char[] pucCollation,
        ref ushort pusLen);

        [DllImport("ace64", EntryPoint = "AdsGetIndexCollation", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetIndexCollation_64(
        IntPtr hIndex,
        [In, Out] char[] pucCollation,
        ref ushort pusLen);

        public static uint AdsGetIndexCollation(IntPtr hIndex, char[] pucCollation, ref ushort pusLen)
        {
            return IntPtr.Size == 4 ? AdsGetIndexCollation_32(hIndex, pucCollation, ref pusLen) : AdsGetIndexCollation_64(hIndex, pucCollation, ref pusLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDataLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDataLength_32(
        IntPtr hTable,
        string pucFldName,
        uint ulOptions,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetDataLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDataLength_64(
        IntPtr hTable,
        string pucFldName,
        uint ulOptions,
        out uint pulLength);

        public static uint AdsGetDataLength(
        IntPtr hTable,
        string pucFldName,
        uint ulOptions,
        out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetDataLength_32(hTable, pucFldName, ulOptions, out pulLength) : AdsGetDataLength_64(hTable, pucFldName, ulOptions, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetDataLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDataLength_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOptions,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetDataLength", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetDataLength_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOptions,
        out uint pulLength);

        public static uint AdsGetDataLength(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOptions,
        out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetDataLength_32(hTable, lFieldOrdinal, ulOptions, out pulLength) : AdsGetDataLength_64(hTable, lFieldOrdinal, ulOptions, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsSetIndexDirection", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetIndexDirection_32(IntPtr hIndex, ushort usReverseDirection);

        [DllImport("ace64", EntryPoint = "AdsSetIndexDirection", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetIndexDirection_64(IntPtr hIndex, ushort usReverseDirection);

        public static uint AdsSetIndexDirection(IntPtr hIndex, ushort usReverseDirection)
        {
            return IntPtr.Size == 4 ? AdsSetIndexDirection_32(hIndex, usReverseDirection) : AdsSetIndexDirection_64(hIndex, usReverseDirection);
        }

        [DllImport("ace32", EntryPoint = "AdsMgKillUser90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgKillUser90_32(
        IntPtr hMgmtHandle,
        string pucUserName,
        ushort usConnNumber,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        [DllImport("ace64", EntryPoint = "AdsMgKillUser90", CharSet = CharSet.Ansi)]
        private static extern uint AdsMgKillUser90_64(
        IntPtr hMgmtHandle,
        string pucUserName,
        ushort usConnNumber,
        ushort usPropertyID,
        [In, Out] byte[] pvProperty,
        ushort usPropertyLen);

        public static uint AdsMgKillUser90(
        IntPtr hMgmtHandle,
        string pucUserName,
        ushort usConnNumber,
        ushort usPropertyID,
        byte[] pvProperty,
        ushort usPropertyLen)
        {
            return IntPtr.Size == 4 ? AdsMgKillUser90_32(hMgmtHandle, pucUserName, usConnNumber, usPropertyID, pvProperty, usPropertyLen) : AdsMgKillUser90_64(hMgmtHandle, pucUserName, usConnNumber, usPropertyID, pvProperty, usPropertyLen);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldLength100", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength100_32(
        IntPtr hTable,
        string pucFldName,
        uint ulOptions,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetFieldLength100", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength100_64(
        IntPtr hTable,
        string pucFldName,
        uint ulOptions,
        out uint pulLength);

        public static uint AdsGetFieldLength100(
        IntPtr hTable,
        string pucFldName,
        uint ulOptions,
        out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetFieldLength100_32(hTable, pucFldName, ulOptions, out pulLength) : AdsGetFieldLength100_64(hTable, pucFldName, ulOptions, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsGetFieldLength100", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength100_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOptions,
        out uint pulLength);

        [DllImport("ace64", EntryPoint = "AdsGetFieldLength100", CharSet = CharSet.Ansi)]
        private static extern uint AdsGetFieldLength100_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOptions,
        out uint pulLength);

        public static uint AdsGetFieldLength100(
        IntPtr hTable,
        uint lFieldOrdinal,
        uint ulOptions,
        out uint pulLength)
        {
            return IntPtr.Size == 4 ? AdsGetFieldLength100_32(hTable, lFieldOrdinal, ulOptions, out pulLength) : AdsGetFieldLength100_64(hTable, lFieldOrdinal, ulOptions, out pulLength);
        }

        [DllImport("ace32", EntryPoint = "AdsSetRightsChecking", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRightsChecking_32(uint ulOptions);

        [DllImport("ace64", EntryPoint = "AdsSetRightsChecking", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetRightsChecking_64(uint ulOptions);

        public static uint AdsSetRightsChecking(uint ulOptions)
        {
            return IntPtr.Size == 4 ? AdsSetRightsChecking_32(ulOptions) : AdsSetRightsChecking_64(ulOptions);
        }

        [DllImport("ace32", EntryPoint = "AdsSetTableTransactionFree", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTableTransactionFree_32(IntPtr hTable, ushort usTransFree);

        [DllImport("ace64", EntryPoint = "AdsSetTableTransactionFree", CharSet = CharSet.Ansi)]
        private static extern uint AdsSetTableTransactionFree_64(IntPtr hTable, ushort usTransFree);

        public static uint AdsSetTableTransactionFree(IntPtr hTable, ushort usTransFree)
        {
            return IntPtr.Size == 4 ? AdsSetTableTransactionFree_32(hTable, usTransFree) : AdsSetTableTransactionFree_64(hTable, usTransFree);
        }

        [DllImport("ace32", EntryPoint = "AdsIsTableTransactionFree", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsTableTransactionFree_32(IntPtr hTable, out ushort pusTransFree);

        [DllImport("ace64", EntryPoint = "AdsIsTableTransactionFree", CharSet = CharSet.Ansi)]
        private static extern uint AdsIsTableTransactionFree_64(IntPtr hTable, out ushort pusTransFree);

        public static uint AdsIsTableTransactionFree(IntPtr hTable, out ushort pusTransFree)
        {
            return IntPtr.Size == 4 ? AdsIsTableTransactionFree_32(hTable, out pusTransFree) : AdsIsTableTransactionFree_64(hTable, out pusTransFree);
        }

        [DllImport("ace32", EntryPoint = "AdsFindServers", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindServers_32(uint ulOptions, out IntPtr phTable);

        [DllImport("ace64", EntryPoint = "AdsFindServers", CharSet = CharSet.Ansi)]
        private static extern uint AdsFindServers_64(uint ulOptions, out IntPtr phTable);

        public static uint AdsFindServers(uint ulOptions, out IntPtr phTable)
        {
            return IntPtr.Size == 4 ? AdsFindServers_32(ulOptions, out phTable) : AdsFindServers_64(ulOptions, out phTable);
        }

        [DllImport("ace32", EntryPoint = "AdsBinaryToFileW", CharSet = CharSet.Unicode)]
        private static extern uint AdsBinaryToFileW_32(
        IntPtr hTable,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        string pwcFileName);

        [DllImport("ace64", EntryPoint = "AdsBinaryToFileW", CharSet = CharSet.Unicode)]
        private static extern uint AdsBinaryToFileW_64(
        IntPtr hTable,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        string pwcFileName);

        public static uint AdsBinaryToFileW(IntPtr hTable, string pucFldName, string pwcFileName)
        {
            return IntPtr.Size == 4 ? AdsBinaryToFileW_32(hTable, pucFldName, pwcFileName) : AdsBinaryToFileW_64(hTable, pucFldName, pwcFileName);
        }

        [DllImport("ace32", EntryPoint = "AdsBinaryToFileW", CharSet = CharSet.Unicode)]
        private static extern uint AdsBinaryToFileW_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        string pwcFileName);

        [DllImport("ace64", EntryPoint = "AdsBinaryToFileW", CharSet = CharSet.Unicode)]
        private static extern uint AdsBinaryToFileW_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        string pwcFileName);

        public static uint AdsBinaryToFileW(IntPtr hTable, uint lFieldOrdinal, string pwcFileName)
        {
            return IntPtr.Size == 4 ? AdsBinaryToFileW_32(hTable, lFieldOrdinal, pwcFileName) : AdsBinaryToFileW_64(hTable, lFieldOrdinal, pwcFileName);
        }

        [DllImport("ace32", EntryPoint = "AdsFileToBinaryW", CharSet = CharSet.Unicode)]
        private static extern uint AdsFileToBinaryW_32(
        IntPtr hTable,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        ushort usBinaryType,
        string pwcFileName);

        [DllImport("ace64", EntryPoint = "AdsFileToBinaryW", CharSet = CharSet.Unicode)]
        private static extern uint AdsFileToBinaryW_64(
        IntPtr hTable,
        [MarshalAs(UnmanagedType.LPStr)] string pucFldName,
        ushort usBinaryType,
        string pwcFileName);

        public static uint AdsFileToBinaryW(
        IntPtr hTable,
        string pucFldName,
        ushort usBinaryType,
        string pwcFileName)
        {
            return IntPtr.Size == 4 ? AdsFileToBinaryW_32(hTable, pucFldName, usBinaryType, pwcFileName) : AdsFileToBinaryW_64(hTable, pucFldName, usBinaryType, pwcFileName);
        }

        [DllImport("ace32", EntryPoint = "AdsFileToBinaryW", CharSet = CharSet.Unicode)]
        private static extern uint AdsFileToBinaryW_32(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        string pwcFileName);

        [DllImport("ace64", EntryPoint = "AdsFileToBinaryW", CharSet = CharSet.Unicode)]
        private static extern uint AdsFileToBinaryW_64(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        string pwcFileName);

        public static uint AdsFileToBinaryW(
        IntPtr hTable,
        uint lFieldOrdinal,
        ushort usBinaryType,
        string pwcFileName)
        {
            return IntPtr.Size == 4 ? AdsFileToBinaryW_32(hTable, lFieldOrdinal, usBinaryType, pwcFileName) : AdsFileToBinaryW_64(hTable, lFieldOrdinal, usBinaryType, pwcFileName);
        }

        public enum AdsCharTypes : ushort
        {
            ADS_ANSI = 1,
            ADS_OEM = 2,
            CZECH_VFP_CI_AS_1250 = 3,
            GENERAL_VFP_CI_AS_1250 = 4,
            HUNGARY_VFP_CI_AS_1250 = 5,
            MACHINE_VFP_BIN_1250 = 6,
            POLISH_VFP_CI_AS_1250 = 7,
            SLOVAK_VFP_CI_AS_1250 = 8,
            MACHINE_VFP_BIN_1251 = 9,
            RUSSIAN_VFP_CI_AS_1251 = 10, // 0x000A
            DUTCH_VFP_CI_AS_1252 = 11, // 0x000B
            GENERAL_VFP_CI_AS_1252 = 12, // 0x000C
            GERMAN_VFP_CI_AS_1252 = 13, // 0x000D
            ICELAND_VFP_CI_AS_1252 = 14, // 0x000E
            MACHINE_VFP_BIN_1252 = 15, // 0x000F
            NORDAN_VFP_CI_AS_1252 = 16, // 0x0010
            SPANISH_VFP_CI_AS_1252 = 17, // 0x0011
            SWEFIN_VFP_CI_AS_1252 = 18, // 0x0012
            UNIQWT_VFP_CS_AS_1252 = 19, // 0x0013
            GREEK_VFP_CI_AS_1253 = 20, // 0x0014
            MACHINE_VFP_BIN_1253 = 21, // 0x0015
            GENERAL_VFP_CI_AS_1254 = 22, // 0x0016
            MACHINE_VFP_BIN_1254 = 23, // 0x0017
            TURKISH_VFP_CI_AS_1254 = 24, // 0x0018
            DUTCH_VFP_CI_AS_437 = 25, // 0x0019
            GENERAL_VFP_CI_AS_437 = 26, // 0x001A
            GERMAN_VFP_CI_AS_437 = 27, // 0x001B
            ICELAND_VFP_CI_AS_437 = 28, // 0x001C
            MACHINE_VFP_BIN_437 = 29, // 0x001D
            NORDAN_VFP_CI_AS_437 = 30, // 0x001E
            SPANISH_VFP_CI_AS_437 = 31, // 0x001F
            SWEFIN_VFP_CI_AS_437 = 32, // 0x0020
            UNIQWT_VFP_CS_AS_437 = 33, // 0x0021
            GENERAL_VFP_CI_AS_620 = 34, // 0x0022
            MACHINE_VFP_BIN_620 = 35, // 0x0023
            POLISH_VFP_CI_AS_620 = 36, // 0x0024
            GREEK_VFP_CI_AS_737 = 37, // 0x0025
            MACHINE_VFP_BIN_737 = 38, // 0x0026
            DUTCH_VFP_CI_AS_850 = 39, // 0x0027
            GENERAL_VFP_CI_AS_850 = 40, // 0x0028
            ICELAND_VFP_CI_AS_850 = 41, // 0x0029
            MACHINE_VFP_BIN_850 = 42, // 0x002A
            NORDAN_VFP_CI_AS_850 = 43, // 0x002B
            SPANISH_VFP_CI_AS_850 = 44, // 0x002C
            SWEFIN_VFP_CI_AS_850 = 45, // 0x002D
            UNIQWT_VFP_CS_AS_850 = 46, // 0x002E
            CZECH_VFP_CI_AS_852 = 47, // 0x002F
            GENERAL_VFP_CI_AS_852 = 48, // 0x0030
            HUNGARY_VFP_CI_AS_852 = 49, // 0x0031
            MACHINE_VFP_BIN_852 = 50, // 0x0032
            POLISH_VFP_CI_AS_852 = 51, // 0x0033
            SLOVAK_VFP_CI_AS_852 = 52, // 0x0034
            GENERAL_VFP_CI_AS_857 = 53, // 0x0035
            MACHINE_VFP_BIN_857 = 54, // 0x0036
            TURKISH_VFP_CI_AS_857 = 55, // 0x0037
            GENERAL_VFP_CI_AS_861 = 56, // 0x0038
            ICELAND_VFP_CI_AS_861 = 57, // 0x0039
            MACHINE_VFP_BIN_861 = 58, // 0x003A
            GENERAL_VFP_CI_AS_865 = 59, // 0x003B
            MACHINE_VFP_BIN_865 = 60, // 0x003C
            NORDAN_VFP_CI_AS_865 = 61, // 0x003D
            SWEFIN_VFP_CI_AS_865 = 62, // 0x003E
            MACHINE_VFP_BIN_866 = 63, // 0x003F
            RUSSIAN_VFP_CI_AS_866 = 64, // 0x0040
            CZECH_VFP_CI_AS_895 = 65, // 0x0041
            GENERAL_VFP_CI_AS_895 = 66, // 0x0042
            MACHINE_VFP_BIN_895 = 67, // 0x0043
            SLOVAK_VFP_CI_AS_895 = 68, // 0x0044
            DANISH_ADS_CS_AS_1252 = 69, // 0x0045
            DUTCH_ADS_CS_AS_1252 = 70, // 0x0046
            ENGL_AMER_ADS_CS_AS_1252 = 71, // 0x0047
            ENGL_CAN_ADS_CS_AS_1252 = 72, // 0x0048
            ENGL_UK_ADS_CS_AS_1252 = 73, // 0x0049
            FINNISH_ADS_CS_AS_1252 = 74, // 0x004A
            FRENCH_ADS_CS_AS_1252 = 75, // 0x004B
            FRENCH_CAN_ADS_CS_AS_1252 = 76, // 0x004C
            GERMAN_ADS_CS_AS_1252 = 77, // 0x004D
            ICELANDIC_ADS_CS_AS_1252 = 78, // 0x004E
            ITALIAN_ADS_CS_AS_1252 = 79, // 0x004F
            NORWEGIAN_ADS_CS_AS_1252 = 80, // 0x0050
            PORTUGUESE_ADS_CS_AS_1252 = 81, // 0x0051
            SPANISH_ADS_CS_AS_1252 = 82, // 0x0052
            SPAN_MOD_ADS_CS_AS_1252 = 83, // 0x0053
            SWEDISH_ADS_CS_AS_1252 = 84, // 0x0054
            RUSSIAN_ADS_CS_AS_1251 = 85, // 0x0055
            ASCII_ADS_CS_AS_1252 = 86, // 0x0056
            TURKISH_ADS_CS_AS_1254 = 87, // 0x0057
            POLISH_ADS_CS_AS_1250 = 88, // 0x0058
            BALTIC_ADS_CS_AS_1257 = 89, // 0x0059
            UKRAINIAN_ADS_CS_AS_1251 = 90, // 0x005A
            DUDEN_DE_ADS_CS_AS_1252 = 91, // 0x005B
            USA_ADS_CS_AS_437 = 92, // 0x005C
            DANISH_ADS_CS_AS_865 = 93, // 0x005D
            DUTCH_ADS_CS_AS_850 = 94, // 0x005E
            FINNISH_ADS_CS_AS_865 = 95, // 0x005F
            FRENCH_ADS_CS_AS_863 = 96, // 0x0060
            GERMAN_ADS_CS_AS_850 = 97, // 0x0061
            GREEK437_ADS_CS_AS_437 = 98, // 0x0062
            GREEK851_ADS_CS_AS_851 = 99, // 0x0063
            ICELD850_ADS_CS_AS_850 = 100, // 0x0064
            ICELD861_ADS_CS_AS_861 = 101, // 0x0065
            ITALIAN_ADS_CS_AS_850 = 102, // 0x0066
            NORWEGN_ADS_CS_AS_865 = 103, // 0x0067
            PORTUGUE_ADS_CS_AS_860 = 104, // 0x0068
            SPANISH_ADS_CS_AS_852 = 105, // 0x0069
            SWEDISH_ADS_CS_AS_865 = 106, // 0x006A
            MAZOVIA_ADS_CS_AS_852 = 107, // 0x006B
            PC_LATIN_ADS_CS_AS_852 = 108, // 0x006C
            ISOLATIN_ADS_CS_AS_850 = 109, // 0x006D
            RUSSIAN_ADS_CS_AS_866 = 110, // 0x006E
            NTXCZ852_ADS_CS_AS_852 = 111, // 0x006F
            NTXCZ895_ADS_CS_AS_895 = 112, // 0x0070
            NTXSL852_ADS_CS_AS_852 = 113, // 0x0071
            NTXSL895_ADS_CS_AS_895 = 114, // 0x0072
            NTXHU852_ADS_CS_AS_852 = 115, // 0x0073
            NTXPL852_ADS_CS_AS_852 = 116, // 0x0074
            TURKISH_ADS_CS_AS_857 = 117, // 0x0075
            BOSNIAN_ADS_CS_AS_775 = 118, // 0x0076
        }

        public delegate uint CallbackFn(ushort usPercentDone, uint ulCallbackID);

        public delegate uint CallbackFn101(ushort usPercentDone, long qCallbackID);
    }
}