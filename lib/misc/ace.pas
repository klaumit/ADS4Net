// (c) 2014 SAP AG or an SAP affiliate company. All rights reserved.
//
// No part of this publication may be reproduced or transmitted in any form or
// for any purpose without the express permission of SAP AG or an SAP affiliate
// company.
//
// The information contained herein may be changed without prior notice.  Some
// software products marketed by SAP AG and its distributors contain
// proprietary software components of other software vendors. National product
// specifications may vary.
//
// These materials are provided by SAP AG or an SAP affiliate company for
// informational purposes only, without representation or warranty of any kind,
// and SAP or its affiliated companies shall not be liable for errors or
// omissions with respect to the materials. The only warranties for SAP or SAP
// affiliate company products and services are those that are set forth in the
// express warranty statements accompanying such products and services, if any.
// Nothing herein should be construed as constituting an additional warranty.
//
// SAP and other SAP products and services mentioned herein as well as their
// respective logos are trademarks or registered trademarks of SAP AG (or an
// SAP affiliate company) in Germany and other countries. All other product and
// service names mentioned are the trademarks of their respective companies.
// Please see http://global.sap.com/corporate-en/legal/copyright/index.epx for
// additional trademark information and notices.
// LIBACE_VERSION 12.00
{*******************************************************************************
* Source File  : ace.pas
* Description  : This is the main header file for the Advantage Client
*                Engine.  It contains the type definitions, constants,
*                and prototypes for the APIs
*******************************************************************************}

unit ace;

{$IFDEF FPC}
   {$MODE delphi}{$H+}
{$ENDIF}

{$INCLUDE versions.inc}

{$IFDEF ADSDELPHI7}

      {$WARN UNSAFE_TYPE OFF}
      {$WARN UNSAFE_CODE OFF}
      {$WARN UNSAFE_CAST OFF}

{$ENDIF}

interface

      {$ifdef WIN32}
      const ACE_LIB = 'ACE32.DLL';
      const ADS_PATH_DELIMITER = '\';
      {$endif}

      {$ifdef WIN64}
      const ACE_LIB = 'ACE64.DLL';
      const ADS_PATH_DELIMITER = '\';
      {$endif}

      {$ifdef LINUX}
      const ACE_LIB = 'libace.so.12.00';
      const ADS_PATH_DELIMITER = '/';
      {$endif}

            {*
            * long on Linux has a variable size depending on the underlying
            * architecture.
            *}

               // Long is 64 bits in GCC.

      {* SQL Timeout value *}
      const ADS_DEFAULT_SQL_TIMEOUT = $0000; // Default client SQL timeout.

      {* Logical constants *}
      const ADS_FALSE = 0;
      const ADS_TRUE = 1;

      {* This is for parameters to routines that accept a default setting *}
      const ADS_DEFAULT = 0;

      {* character set types *}
      const ADS_ANSI = 1;
      const ADS_OEM = 2;
      const CZECH_VFP_CI_AS_1250 = 3;
      const GENERAL_VFP_CI_AS_1250 = 4;
      const HUNGARY_VFP_CI_AS_1250 = 5;
      const MACHINE_VFP_BIN_1250 = 6;
      const POLISH_VFP_CI_AS_1250 = 7;
      const SLOVAK_VFP_CI_AS_1250 = 8;
      const MACHINE_VFP_BIN_1251 = 9;
      const RUSSIAN_VFP_CI_AS_1251 = 10;
      const DUTCH_VFP_CI_AS_1252 = 11;
      const GENERAL_VFP_CI_AS_1252 = 12;
      const GERMAN_VFP_CI_AS_1252 = 13;
      const ICELAND_VFP_CI_AS_1252 = 14;
      const MACHINE_VFP_BIN_1252 = 15;
      const NORDAN_VFP_CI_AS_1252 = 16;
      const SPANISH_VFP_CI_AS_1252 = 17;
      const SWEFIN_VFP_CI_AS_1252 = 18;
      const UNIQWT_VFP_CS_AS_1252 = 19;
      const GREEK_VFP_CI_AS_1253 = 20;
      const MACHINE_VFP_BIN_1253 = 21;
      const GENERAL_VFP_CI_AS_1254 = 22;
      const MACHINE_VFP_BIN_1254 = 23;
      const TURKISH_VFP_CI_AS_1254 = 24;
      const DUTCH_VFP_CI_AS_437 = 25;
      const GENERAL_VFP_CI_AS_437 = 26;
      const GERMAN_VFP_CI_AS_437 = 27;
      const ICELAND_VFP_CI_AS_437 = 28;
      const MACHINE_VFP_BIN_437 = 29;
      const NORDAN_VFP_CI_AS_437 = 30;
      const SPANISH_VFP_CI_AS_437 = 31;
      const SWEFIN_VFP_CI_AS_437 = 32;
      const UNIQWT_VFP_CS_AS_437 = 33;
      const GENERAL_VFP_CI_AS_620 = 34;
      const MACHINE_VFP_BIN_620 = 35;
      const POLISH_VFP_CI_AS_620 = 36;
      const GREEK_VFP_CI_AS_737 = 37;
      const MACHINE_VFP_BIN_737 = 38;
      const DUTCH_VFP_CI_AS_850 = 39;
      const GENERAL_VFP_CI_AS_850 = 40;
      const ICELAND_VFP_CI_AS_850 = 41;
      const MACHINE_VFP_BIN_850 = 42;
      const NORDAN_VFP_CI_AS_850 = 43;
      const SPANISH_VFP_CI_AS_850 = 44;
      const SWEFIN_VFP_CI_AS_850 = 45;
      const UNIQWT_VFP_CS_AS_850 = 46;
      const CZECH_VFP_CI_AS_852 = 47;
      const GENERAL_VFP_CI_AS_852 = 48;
      const HUNGARY_VFP_CI_AS_852 = 49;
      const MACHINE_VFP_BIN_852 = 50;
      const POLISH_VFP_CI_AS_852 = 51;
      const SLOVAK_VFP_CI_AS_852 = 52;
      const GENERAL_VFP_CI_AS_857 = 53;
      const MACHINE_VFP_BIN_857 = 54;
      const TURKISH_VFP_CI_AS_857 = 55;
      const GENERAL_VFP_CI_AS_861 = 56;
      const ICELAND_VFP_CI_AS_861 = 57;
      const MACHINE_VFP_BIN_861 = 58;
      const GENERAL_VFP_CI_AS_865 = 59;
      const MACHINE_VFP_BIN_865 = 60;
      const NORDAN_VFP_CI_AS_865 = 61;
      const SWEFIN_VFP_CI_AS_865 = 62;
      const MACHINE_VFP_BIN_866 = 63;
      const RUSSIAN_VFP_CI_AS_866 = 64;
      const CZECH_VFP_CI_AS_895 = 65;
      const GENERAL_VFP_CI_AS_895 = 66;
      const MACHINE_VFP_BIN_895 = 67;
      const SLOVAK_VFP_CI_AS_895 = 68;
      const DANISH_ADS_CS_AS_1252 = 69;
      const DUTCH_ADS_CS_AS_1252 = 70;
      const ENGL_AMER_ADS_CS_AS_1252 = 71;
      const ENGL_CAN_ADS_CS_AS_1252 = 72;
      const ENGL_UK_ADS_CS_AS_1252 = 73;
      const FINNISH_ADS_CS_AS_1252 = 74;
      const FRENCH_ADS_CS_AS_1252 = 75;
      const FRENCH_CAN_ADS_CS_AS_1252 = 76;
      const GERMAN_ADS_CS_AS_1252 = 77;
      const ICELANDIC_ADS_CS_AS_1252 = 78;
      const ITALIAN_ADS_CS_AS_1252 = 79;
      const NORWEGIAN_ADS_CS_AS_1252 = 80;
      const PORTUGUESE_ADS_CS_AS_1252 = 81;
      const SPANISH_ADS_CS_AS_1252 = 82;
      const SPAN_MOD_ADS_CS_AS_1252 = 83;
      const SWEDISH_ADS_CS_AS_1252 = 84;
      const RUSSIAN_ADS_CS_AS_1251 = 85;
      const ASCII_ADS_CS_AS_1252 = 86;
      const TURKISH_ADS_CS_AS_1254 = 87;
      const POLISH_ADS_CS_AS_1250 = 88;
      const BALTIC_ADS_CS_AS_1257 = 89;
      const UKRAINIAN_ADS_CS_AS_1251 = 90;
      const DUDEN_DE_ADS_CS_AS_1252 = 91;
      const USA_ADS_CS_AS_437 = 92;
      const DANISH_ADS_CS_AS_865 = 93;
      const DUTCH_ADS_CS_AS_850 = 94;
      const FINNISH_ADS_CS_AS_865 = 95;
      const FRENCH_ADS_CS_AS_863 = 96;
      const GERMAN_ADS_CS_AS_850 = 97;
      const GREEK437_ADS_CS_AS_437 = 98;
      const GREEK851_ADS_CS_AS_851 = 99;
      const ICELD850_ADS_CS_AS_850 = 100;
      const ICELD861_ADS_CS_AS_861 = 101;
      const ITALIAN_ADS_CS_AS_850 = 102;
      const NORWEGN_ADS_CS_AS_865 = 103;
      const PORTUGUE_ADS_CS_AS_860 = 104;
      const SPANISH_ADS_CS_AS_852 = 105;
      const SWEDISH_ADS_CS_AS_865 = 106;
      const MAZOVIA_ADS_CS_AS_852 = 107;
      const PC_LATIN_ADS_CS_AS_852 = 108;
      const ISOLATIN_ADS_CS_AS_850 = 109;
      const RUSSIAN_ADS_CS_AS_866 = 110;
      const NTXCZ852_ADS_CS_AS_852 = 111;
      const NTXCZ895_ADS_CS_AS_895 = 112;
      const NTXSL852_ADS_CS_AS_852 = 113;
      const NTXSL895_ADS_CS_AS_895 = 114;
      const NTXHU852_ADS_CS_AS_852 = 115;
      const NTXPL852_ADS_CS_AS_852 = 116;
      const TURKISH_ADS_CS_AS_857 = 117;
      const BOSNIAN_ADS_CS_AS_775 = 118;

      const ADS_MAX_CHAR_SETS = 118;

      {* rights checking options *}
      const ADS_CHECKRIGHTS = 1;
      const ADS_IGNORERIGHTS = 2;

      {*
      * In version 10, the default behavior is changed to never do the rights
      * checking for performance reasons.  The client side existence checks are very
      * expensive and are unnecessary in almost all situations.  If an application
      * requires the old behavior, it can be restored with AdsSetRightsChecking and
      * passing it the appropriate flag below.
      *}
      const ADS_RESPECT_RIGHTS_CHECKING = $00000001; // pre v10 behavior
      const ADS_IGNORE_RIGHTS_CHECKING = $00000002; // post v10 behavior

      {* options for connecting to Advantage servers - can be ORed together *}
      const ADS_INC_USERCOUNT = $00000001;
      const ADS_STORED_PROC_CONN = $00000002;
      const ADS_COMPRESS_ALWAYS = $00000004;
      const ADS_COMPRESS_NEVER = $00000008;
      const ADS_COMPRESS_INTERNET = $0000000C;
      const ADS_REPLICATION_CONNECTION = $00000010;
      const ADS_UDP_IP_CONNECTION = $00000020;
      const ADS_IPX_CONNECTION = $00000040;
      const ADS_TCP_IP_CONNECTION = $00000080;
      const ADS_TCP_IP_V6_CONNECTION = $00000100;
      const ADS_NOTIFICATION_CONNECTION = $00000200;
      // Reserved                         0x00000400
      // Reserved                         0x00000800
      const ADS_TLS_CONNECTION = $00001000;
      const ADS_CHECK_FREE_TABLE_ACCESS = $00002000;

      {* options for opening/create tables - can be ORed together *}
      const ADS_EXCLUSIVE = $00000001;
      const ADS_READONLY = $00000002;
      const ADS_SHARED = $00000004;
      const ADS_CLIPPER_MEMOS = $00000008;
      const ADS_TABLE_PERM_READ = $00000010;
      const ADS_TABLE_PERM_UPDATE = $00000020;
      const ADS_TABLE_PERM_INSERT = $00000040;
      const ADS_TABLE_PERM_DELETE = $00000080;
      const ADS_REINDEX_ON_COLLATION_MISMATCH = $00000100;
      const ADS_IGNORE_COLLATION_MISMATCH = $00000200;
      const ADS_FREE_TABLE = $00001000; // Mutually exclusive with ADS_DICTIONARY_BOUND_TABLE
      const ADS_TEMP_TABLE = $00002000; // Mutually exclusive with ADS_DICTIONARY_BOUND_TABLE
      const ADS_DICTIONARY_BOUND_TABLE = $00004000; // Mutually exclusive with ADS_FREE_TABLE or ADS_TEMP_TABLE
      const ADS_CACHE_READS = $20000000; // Enable caching of reads on the table
      const ADS_CACHE_WRITES = $40000000; // Enable caching of reads & writes on the table

      {* When adding entry in here, make sure the corresponding
      * entry is added in aceunpub.h and ensure that there is no
      * conflict.
      *}

      {*
      * Options for creating indexes - can be ORed together. Be sure to update
      * the ADS_INDEX_OPTIONS_MASK when adding bits to the options
      *}
      const ADS_ASCENDING = $00000000;
      const ADS_UNIQUE = $00000001;
      const ADS_COMPOUND = $00000002;
      const ADS_CUSTOM = $00000004;
      const ADS_DESCENDING = $00000008;
      const ADS_USER_DEFINED = $00000010;
      {* Options specifically for FTS indexes 0020 - 0200 *}
      const ADS_FTS_INDEX = $00000020; // This is implied for AdsCreateFTSIndex
      const ADS_FTS_FIXED = $00000040; // Do not maintain the index with record updates
      const ADS_FTS_CASE_SENSITIVE = $00000080; // Make the index case sensitive
      const ADS_FTS_KEEP_SCORE = $00000100; // Track word counts in the index for faster SCORE()
      const ADS_FTS_PROTECT_NUMBERS = $00000200; // Don't break numbers on commas and periods

      const ADS_NOT_AUTO_OPEN = $00000400; // Don't make this an auto open index in data dictionary
      const ADS_CANDIDATE = $00000800; // true unique CDX index (equivalent to ADS_UNIQUE for ADIs)
      const ADS_BINARY_INDEX = $00001000; // logical index with a bitmap for data

      {* Options concerning the parameters supplied to the AdsCreateFTSIndex 00002000 - 00004000 *}
      const ADS_FTS_ENCODE_UTF8 = $00002000;
      const ADS_FTS_ENCODE_UTF16 = $00004000;

      const ADS_ONLINE = $00200000; // Perform ONLINE create with table open shared

      {* Option to force index version *}
      const ADS_ALLOW_MULTIPLE_COLLATION = $10000000;

      {* Options concerning Unicode string in the indexes 20000000 - 40000000 *}
      const ADS_UCHAR_KEY_SHORT = $20000000;
      const ADS_UCHAR_KEY_LONG = $40000000;
      const ADS_UCHAR_KEY_XLONG = $60000000;

      const ADS_INDEX_OPTIONS_MASK = $70207FFF; // All valid index options

      {* Options for returning string values *}
      const ADS_NONE = $0000;
      const ADS_LTRIM = $0001;
      const ADS_RTRIM = $0002;
      const ADS_TRIM = $0003;
      const ADS_GET_UTF8 = $0004;
      const ADS_DONT_CHECK_CONV_ERR = $0008;
      const ADS_GET_FORMAT_ANSI = $0010;
      const ADS_GET_FORMAT_WEB = $0030;
      const ADS_GET_GUID_MIME = $0100; // MIME ENCODED
      const ADS_GET_GUID_FILE = $0200; // FILE ENCODED
      const ADS_GET_GUID_NUMBERS = $0400; // ONLY NUMBER
      const ADS_GET_GUID_REGISTRY = $0800; // REGISTRY Format - default

      {* locking compatibility *}
      const ADS_COMPATIBLE_LOCKING = 0;
      const ADS_PROPRIETARY_LOCKING = 1;

      {* settings for seeks *}
      const ADS_SOFTSEEK = $0001;
      const ADS_HARDSEEK = $0002;
      const ADS_SEEKGT = $0004;

      {* data types for seeks (and scopes) *}
      const ADS_RAWKEY = 1; {* no conversion performed on given data *}
      const ADS_STRINGKEY = 2; {* data given as a string *}
      const ADS_DOUBLEKEY = 4; {* data is a pointer to 8 byte double *}
      const ADS_WSTRINGKEY = 8; {* data given as a UTF16 string *}

      {* Option for AdsBuildRawKey100 *}
      const ADS_GET_DEFAULT_KEY_LENGTH = $00000000;
      const ADS_GET_PARTIAL_FULL_KEY_LENGTH = $00000001;
      const ADS_GET_FULL_KEY_LENGTH = $00000002;
      const ADS_GET_PRIMARY_WEIGHT_LENGTH = $00000004;

      {* For retrieving scope settings *}
      const ADS_TOP = 1;
      const ADS_BOTTOM = 2;

      {* for calls that can optionally use filters *}
      const ADS_RESPECTFILTERS = $0001;
      const ADS_IGNOREFILTERS = $0002;
      const ADS_RESPECTSCOPES = $0003;
      {*
      * This value is only used with GetRecordCount:  It can be ORed in with the
      * ignore filter value to force a read from the table header to get the most
      * current record count.
      *}
      const ADS_REFRESHCOUNT = $0004;

      {* Server type constants *}
      const ADS_LOCAL_SERVER = $0001;
      const ADS_REMOTE_SERVER = $0002;
      const ADS_AIS_SERVER = $0004;

      {* ACE Handle types *}
      const ADS_CONNECTION = 1;
      const ADS_TABLE = 2;
      const ADS_INDEX_ORDER = 3;
      const ADS_STATEMENT = 4;
      const ADS_CURSOR = 5;
      const ADS_DATABASE_CONNECTION = 6;
      {* #define ADS_SYS_ADMIN_CONNECTION  7   obsolete *}
      const ADS_FTS_INDEX_ORDER = 8;

      {* ACE Cursor ReadOnly settings *}
      const ADS_CURSOR_READONLY = 1;
      const ADS_CURSOR_READWRITE = 2;

      {* ACE Cursor Constrain settings *}
      const ADS_CONSTRAIN = 1;
      const ADS_NO_CONSTRAIN = 2;

      {* Select Field Read settings *}
      const ADS_READ_ALL_COLUMNS = 1;
      const ADS_READ_SELECT_COLUMNS = 2;

      {* Data dictionary new contraint property validation options *}
      const ADS_NO_VALIDATE = 0; {* Do not validate records against the new constraint *}
      const ADS_VALIDATE_NO_SAVE = 1; {* Delete record not meeting the constraint from the table, no save *}
      {* Validate the records against the new constraint and overwrite
       * the fail table with records not meeting the constraint. *}
      const ADS_VALIDATE_WRITE_FAIL = 2;
      {* Validate the records against the new constraint and append
       * the failed records into the fail table *}
      const ADS_VALIDATE_APPEND_FAIL = 3;
      {* Validate the records against the new constraint and return
       * error if there is any record not meeting the constraint *}
      const ADS_VALIDATE_RETURN_ERROR = 4;

      {* Possible result values from AdsCompareBookmarks. *}
      const ADS_CMP_LESS = -1;
      const ADS_CMP_EQUAL = 0;
      const ADS_CMP_GREATER = 1;

      {* Property values for the AdsGetConnectionProperty API *}
      const ADS_CONNECTIONPROP_USERNAME = 0;
      const ADS_CONNECTIONPROP_PASSWORD = 1;
      const ADS_CONNECTIONPROP_PROTOCOL = 2;
      const ADS_CONNECTIONPROP_ENCRYPTION_TYPE = 3;
      const ADS_CONNECTIONPROP_FIPS_MODE = 4;
      const ADS_CONNECTIONPROP_CERTIFICATE_FILE = 5;
      const ADS_CONNECTIONPROP_CIPHER_SUITE = 6;
      const ADS_CONNECTIONPROP_COMMON_NAME = 7;

      const ADS_CONNECTIONPROP_USING_TCP_IP = 1;
      const ADS_CONNECTIONPROP_USING_TLC = 5;

      {* Options for the AdsGetRecordCRC API *}
      const ADS_CRC_LOCALLY = 1;
      const ADS_CRC_IGNOREMEMOPAGES = 2;

      {* Options for notification events *}
      const ADS_EVENT_ASYNC = 1;
      const ADS_EVENT_WITH_DATA = 2; // Allow data to be passed with this event

      {* Options for the AdsCancelUpdate90 API *}
      const ADS_PRESERVE_ERR = $0001;

      {* property for AdsGetIntProperty API *}
      const ADS_CODE_PAGE = 1;

      {* Success return code *}
      const AE_SUCCESS = 0;

      {* Error codes *}
      const AE_ALLOCATION_FAILED = 5001;
      const AE_COMM_MISMATCH = 5002;
      const AE_DATA_TOO_LONG = 5003;
      const AE_FILE_NOT_FOUND = 5004;
      const AE_INSUFFICIENT_BUFFER = 5005;
      const AE_INVALID_BOOKMARK = 5006;
      const AE_INVALID_CALLBACK = 5007;
      const AE_INVALID_CENTURY = 5008;
      const AE_INVALID_DATEFORMAT = 5009;
      const AE_INVALID_DECIMALS = 5010;
      const AE_INVALID_EXPRESSION = 5011;
      const AE_INVALID_FIELDDEF = 5012;
      const AE_INVALID_FILTER_OPTION = 5013;
      const AE_INVALID_INDEX_HANDLE = 5014;
      const AE_INVALID_INDEX_NAME = 5015;
      const AE_INVALID_INDEX_ORDER_NAME = 5016;
      const AE_INVALID_INDEX_TYPE = 5017;
      const AE_INVALID_HANDLE = 5018;
      const AE_INVALID_OPTION = 5019;
      const AE_INVALID_PATH = 5020;
      const AE_INVALID_POINTER = 5021;
      const AE_INVALID_RECORD_NUMBER = 5022;
      const AE_INVALID_TABLE_HANDLE = 5023;
      const AE_INVALID_CONNECTION_HANDLE = 5024;
      const AE_INVALID_TABLETYPE = 5025;
      const AE_INVALID_WORKAREA = 5026;
      const AE_INVALID_CHARSETTYPE = 5027;
      const AE_INVALID_LOCKTYPE = 5028;
      const AE_INVALID_RIGHTSOPTION = 5029;
      const AE_INVALID_FIELDNUMBER = 5030;
      const AE_INVALID_KEY_LENGTH = 5031;
      const AE_INVALID_FIELDNAME = 5032;
      const AE_NO_DRIVE_CONNECTION = 5033;
      const AE_FILE_NOT_ON_SERVER = 5034;
      const AE_LOCK_FAILED = 5035;
      const AE_NO_CONNECTION = 5036;
      const AE_NO_FILTER = 5037;
      const AE_NO_SCOPE = 5038;
      const AE_NO_TABLE = 5039;
      const AE_NO_WORKAREA = 5040;
      const AE_NOT_FOUND = 5041;
      const AE_NOT_IMPLEMENTED = 5042;
      const AE_MAX_THREADS_EXCEEDED = 5043;
      const AE_START_THREAD_FAIL = 5044;
      const AE_TOO_MANY_INDEXES = 5045;
      const AE_TOO_MANY_TAGS = 5046;
      const AE_TRANS_OUT_OF_SEQUENCE = 5047;
      const AE_UNKNOWN_ERRCODE = 5048;
      const AE_UNSUPPORTED_COLLATION = 5049;
      const AE_NAME_TOO_LONG = 5050;
      const AE_DUPLICATE_ALIAS = 5051;
      const AE_TABLE_CLOSED_IN_TRANSACTION = 5053;
      const AE_PERMISSION_DENIED = 5054;
      const AE_STRING_NOT_FOUND = 5055;
      const AE_UNKNOWN_CHAR_SET = 5056;
      const AE_INVALID_OEM_CHAR_FILE = 5057;
      const AE_INVALID_MEMO_BLOCK_SIZE = 5058;
      const AE_NO_FILE_FOUND = 5059;
      const AE_NO_INF_LOCK = 5060;
      const AE_INF_FILE_ERROR = 5061;
      const AE_RECORD_NOT_LOCKED = 5062;
      const AE_ILLEGAL_COMMAND_DURING_TRANS = 5063;
      const AE_TABLE_NOT_SHARED = 5064;
      const AE_INDEX_ALREADY_OPEN = 5065;
      const AE_INVALID_FIELD_TYPE = 5066;
      const AE_TABLE_NOT_EXCLUSIVE = 5067;
      const AE_NO_CURRENT_RECORD = 5068;
      const AE_PRECISION_LOST = 5069;
      const AE_INVALID_DATA_TYPE = 5070;
      const AE_DATA_TRUNCATED = 5071;
      const AE_TABLE_READONLY = 5072;
      const AE_INVALID_RECORD_LENGTH = 5073;
      const AE_NO_ERROR_MESSAGE = 5074;
      const AE_INDEX_SHARED = 5075;
      const AE_INDEX_EXISTS = 5076;
      const AE_CYCLIC_RELATION = 5077;
      const AE_INVALID_RELATION = 5078;
      const AE_INVALID_DAY = 5079;
      const AE_INVALID_MONTH = 5080;
      const AE_CORRUPT_TABLE = 5081;
      const AE_INVALID_BINARY_OFFSET = 5082;
      const AE_BINARY_FILE_ERROR = 5083;
      const AE_INVALID_DELETED_BYTE_VALUE = 5084;
      const AE_NO_PENDING_UPDATE = 5085;
      const AE_PENDING_UPDATE = 5086;
      const AE_TABLE_NOT_LOCKED = 5087;
      const AE_CORRUPT_INDEX = 5088;
      const AE_AUTOOPEN_INDEX = 5089;
      const AE_SAME_TABLE = 5090;
      const AE_INVALID_IMAGE = 5091;
      const AE_COLLATION_SEQUENCE_MISMATCH = 5092;
      const AE_INVALID_INDEX_ORDER = 5093;
      const AE_TABLE_CACHED = 5094;
      const AE_INVALID_DATE = 5095;
      const AE_ENCRYPTION_NOT_ENABLED = 5096;
      const AE_INVALID_PASSWORD = 5097;
      const AE_TABLE_ENCRYPTED = 5098;
      const AE_SERVER_MISMATCH = 5099;
      const AE_INVALID_USERNAME = 5100;
      const AE_INVALID_VALUE = 5101;
      const AE_INVALID_CONTINUE = 5102;
      const AE_UNRECOGNIZED_VERSION = 5103;
      const AE_RECORD_ENCRYPTED = 5104;
      const AE_UNRECOGNIZED_ENCRYPTION = 5105;
      const AE_INVALID_SQLSTATEMENT_HANDLE = 5106;
      const AE_INVALID_SQLCURSOR_HANDLE = 5107;
      const AE_NOT_PREPARED = 5108;
      const AE_CURSOR_NOT_CLOSED = 5109;
      const AE_INVALID_SQL_PARAM_NUMBER = 5110;
      const AE_INVALID_SQL_PARAM_NAME = 5111;
      const AE_INVALID_COLUMN_NUMBER = 5112;
      const AE_INVALID_COLUMN_NAME = 5113;
      const AE_INVALID_READONLY_OPTION = 5114;
      const AE_IS_CURSOR_HANDLE = 5115;
      const AE_INDEX_EXPR_NOT_FOUND = 5116;
      const AE_NOT_DML = 5117;
      const AE_INVALID_CONSTRAIN_TYPE = 5118;
      const AE_INVALID_CURSORHANDLE = 5119;
      const AE_OBSOLETE_FUNCTION = 5120;
      const AE_TADSDATASET_GENERAL = 5121;
      const AE_UDF_OVERWROTE_BUFFER = 5122;
      const AE_INDEX_UDF_NOT_SET = 5123;
      const AE_CONCURRENT_PROBLEM = 5124;
      const AE_INVALID_DICTIONARY_HANDLE = 5125;
      const AE_INVALID_PROPERTY_ID = 5126;
      const AE_INVALID_PROPERTY = 5127;
      const AE_DICTIONARY_ALREADY_EXISTS = 5128;
      const AE_INVALID_FIND_HANDLE = 5129;
      const AE_DD_REQUEST_NOT_COMPLETED = 5130;
      const AE_INVALID_OBJECT_ID = 5131;
      const AE_INVALID_OBJECT_NAME = 5132;
      const AE_INVALID_PROPERTY_LENGTH = 5133;
      const AE_INVALID_KEY_OPTIONS = 5134;
      const AE_CONSTRAINT_VALIDATION_ERROR = 5135;
      const AE_INVALID_OBJECT_TYPE = 5136;
      const AE_NO_OBJECT_FOUND = 5137;
      const AE_PROPERTY_NOT_SET = 5138;
      const AE_NO_PRIMARY_KEY_EXISTS = 5139;
      const AE_LOCAL_CONN_DISABLED = 5140;
      const AE_RI_RESTRICT = 5141;
      const AE_RI_CASCADE = 5142;
      const AE_RI_FAILED = 5143;
      const AE_RI_CORRUPTED = 5144;
      const AE_RI_UNDO_FAILED = 5145;
      const AE_RI_RULE_EXISTS = 5146;
      const AE_COLUMN_CANNOT_BE_NULL = 5147;
      const AE_MIN_CONSTRAINT_VIOLATION = 5148;
      const AE_MAX_CONSTRAINT_VIOLATION = 5149;
      const AE_RECORD_CONSTRAINT_VIOLATION = 5150;
      const AE_CANNOT_DELETE_TEMP_INDEX = 5151;
      const AE_RESTRUCTURE_FAILED = 5152;
      const AE_INVALID_STATEMENT = 5153;
      const AE_STORED_PROCEDURE_FAILED = 5154;
      const AE_INVALID_DICTIONARY_FILE = 5155;
      const AE_NOT_MEMBER_OF_GROUP = 5156;
      const AE_ALREADY_MEMBER_OF_GROUP = 5157;
      const AE_INVALID_OBJECT_RIGHT = 5158;
      {* Note that this is same as above. The word
       * permission is more commonly used.
       *}
      const AE_INVALID_OBJECT_PERMISSION = 5158;
      const AE_CANNOT_OPEN_DATABASE_TABLE = 5159;
      const AE_INVALID_CONSTRAINT = 5160;
      const AE_NOT_ADMINISTRATOR = 5161;
      const AE_NO_TABLE_ENCRYPTION_PASSWORD = 5162;
      const AE_TABLE_NOT_ENCRYPTED = 5163;
      const AE_INVALID_ENCRYPTION_VERSION = 5164;
      const AE_NO_STORED_PROC_EXEC_RIGHTS = 5165;
      const AE_DD_UNSUPPORTED_DEPLOYMENT = 5166;
      const AE_INFO_AUTO_CREATION_OCCURRED = 5168;
      const AE_INFO_COPY_MADE_BY_CLIENT = 5169;
      const AE_DATABASE_REQUIRES_NEW_SERVER = 5170;
      const AE_COLUMN_PERMISSION_DENIED = 5171;
      const AE_DATABASE_REQUIRES_NEW_CLIENT = 5172;
      const AE_INVALID_LINK_NUMBER = 5173;
      const AE_LINK_ACTIVATION_FAILED = 5174;
      const AE_INDEX_COLLATION_MISMATCH = 5175;
      const AE_ILLEGAL_USER_OPERATION = 5176;
      const AE_TRIGGER_FAILED = 5177;
      const AE_NO_ASA_FUNCTION_FOUND = 5178;
      const AE_VALUE_OVERFLOW = 5179;
      const AE_UNRECOGNIZED_FTS_VERSION = 5180;
      const AE_TRIG_CREATION_FAILED = 5181;
      const AE_MEMTABLE_SIZE_EXCEEDED = 5182;
      const AE_OUTDATED_CLIENT_VERSION = 5183;
      const AE_FREE_TABLE = 5184;
      const AE_LOCAL_CONN_RESTRICTED = 5185;
      const AE_OLD_RECORD = 5186;
      const AE_QUERY_NOT_ACTIVE = 5187;
      const AE_KEY_EXCEEDS_PAGE_SIZE = 5188;
      const AE_TABLE_FOUND = 5189;
      const AE_TABLE_NOT_FOUND = 5190;
      const AE_LOCK_OBJECT = 5191;
      const AE_INVALID_REPLICATION_IDENT = 5192;
      const AE_ILLEGAL_COMMAND_DURING_BACKUP = 5193;
      const AE_NO_MEMO_FILE = 5194;
      const AE_SUBSCRIPTION_QUEUE_NOT_EMPTY = 5195;
      const AE_UNABLE_TO_DISABLE_TRIGGERS = 5196;
      const AE_UNABLE_TO_ENABLE_TRIGGERS = 5197;
      const AE_BACKUP = 5198;
      const AE_FREETABLEFAILED = 5199;
      const AE_BLURRY_SNAPSHOT = 5200;
      const AE_INVALID_VERTICAL_FILTER = 5201;
      const AE_INVALID_USE_OF_HANDLE_IN_AEP = 5202;
      const AE_COLLATION_NOT_RECOGNIZED = 5203;
      const AE_INVALID_COLLATION = 5204;
      const AE_NOT_VFP_NULLABLE_FIELD = 5205;
      const AE_NOT_VFP_VARIABLE_FIELD = 5206;
      const AE_ILLEGAL_EVENT_COMMAND = 5207;
      const AE_KEY_CANNOT_BE_NULL = 5208;
      const AE_COLLATIONS_DO_NOT_MATCH = 5209;
      const AE_INVALID_APPID = 5210;
      const AE_UNICODE_CONVERSION = 5211;
      const AE_UNICODE_COLLATION = 5212;
      const AE_SERVER_ENUMERATION_ERROR = 5213;
      const AE_UNABLE_TO_LOAD_SSL = 5214;
      const AE_UNABLE_TO_VERIFY_SIGNATURE = 5215;
      const AE_UNABLE_TO_LOAD_SSL_ENTRYPOINT = 5216;
      const AE_CRYPTO_ERROR = 5217;
      const AE_UNRECOGNIZED_CIPHER = 5218;
      const AE_FIPS_MODE_ENCRYPTION = 5219; {* FIPS mode encryption violation *}
      const AE_FIPS_REQUIRED = 5220;
      const AE_FIPS_NOT_ALLOWED = 5221;
      const AE_FIPS_MODE_FAILED = 5222;
      const AE_PASSWORD_REQUIRED = 5223; {* the additional error info should include details on type of password *}
      const AE_CONNECTION_TIMED_OUT = 5224;
      const AE_DELTA_SUPPORT_NOT_POSSIBLE = 5225; {* Cannot enable web delta functionality *}
      const AE_QUERY_LOGGING_ERROR = 5226; {* the additional error info should include specifics *}
      const AE_COMPRESSION_FAILED = 5227;
      const AE_INVALID_DATA = 5228;
      const AE_ROWVERSION_REQUIRED = 5229;

      {* Supported file types *}
      const ADS_DATABASE_TABLE = ADS_DEFAULT;
      const ADS_NTX = 1;
      const ADS_CDX = 2;
      const ADS_ADT = 3;
      const ADS_VFP = 4;

      {* for retrieving file names of tables *}
      const ADS_BASENAME = 1;
      const ADS_BASENAMEANDEXT = 2;
      const ADS_FULLPATHNAME = 3;
      const ADS_DATADICTIONARY_NAME = 4;
      const ADS_TABLE_OPEN_NAME = 5;

      {* Advantage Optimized Filter (AOF) optimization levels *}
      const ADS_OPTIMIZED_FULL = 1;
      const ADS_OPTIMIZED_PART = 2;
      const ADS_OPTIMIZED_NONE = 3;

      {* Advantage Optimized Filter (AOF) options *}
      const ADS_DYNAMIC_AOF = $00000000; {* default *}
      const ADS_RESOLVE_IMMEDIATE = $00000001;
      const ADS_RESOLVE_DYNAMIC = $00000002;
      const ADS_KEYSET_AOF = $00000004;
      const ADS_FIXED_AOF = $00000008;
      const ADS_KEEP_AOF_PLAN = $00000010;
      const ADS_ENCODE_UTF16 = $00002000; // Used in AdsSetFilter100 options as well
      const ADS_ENCODE_UTF8 = $00004000; // Used in AdsSetFitler100 options as well

      {* Advantage Optimized Filter (AOF) customization options *}
      const ADS_AOF_ADD_RECORD = 1;
      const ADS_AOF_REMOVE_RECORD = 2;
      const ADS_AOF_TOGGLE_RECORD = 3;

      {* Stored procedure or trigger type *}
      const ADS_STORED_PROC = $00000001;
      {* means we know for sure this is a com
       * aep. Before 7.1 we couldn't distinguish. *}
      const ADS_COMSTORED_PROC = $00000002;
      const ADS_SCRIPT_PROC = $00000004; {* Stored procedure written in SQL script *}

      {*
      * Bit mask used by AdsDDAddProcedure to specify that the procedure returns an varying
      * output cursor. Used in the ulInvokeType param.
      *}
      const ADS_PROC_VARYING_OUTPUT = $00001000; {* Stored procedure returns varying output *}

      {* Table (and related file) encryption types when using v10 *}
      const ADS_ENCRYPTION_RC4 = 3; {* RC4 Encryption *}
      const ADS_ENCRYPTION_AES128 = 5; {* 128-bit AES in CTR mode, PBKDF2 key derivation *}
      const ADS_ENCRYPTION_AES256 = 6; {* 256-bit AES in CTR mode, PBKDF2 key derivation *}

      {* some maximum values used by the client *}
      {* NOTE:  constants meant for string length exclude space for null terminator *}
      const ADS_MAX_DATEMASK = 12;
      const ADS_MAX_ERROR_LEN = 600;
      const ADS_MAX_INDEX_EXPR_LEN = 510; {* this is only accurate for index expressions *}
      {* maximum key value length.  This is the max key length
       * of ADI indexes.  Max CDX key length is 240.  Max
       * NTX key length is 256 *}
      const ADS_MAX_KEY_LENGTH = 4082;
      const ADS_MAX_FIELD_NAME = 128;
      const ADS_MAX_DBF_FIELD_NAME = 10; {* maximum length of field name in a DBF *}
      const ADS_MAX_INDEXES = 15; {* physical index files, NOT index orders *}
      const ADS_MAX_PATH = 260;
      const ADS_MAX_TABLE_NAME = 255; {* long file name *}
      const ADS_MAX_TAG_NAME = 128;
      const ADS_MAX_TAGS = 256; {* maximum for CDX/ADI file *}
      const ADS_MAX_OBJECT_NAME = 200; {* maximum length of DD object name *}
      const ADS_MAX_TABLE_AND_PATH = ADS_MAX_TABLE_NAME + ADS_MAX_PATH;

      {*
      * Valid range of page sizes for ADI indexes.  The default page size is 512
      * bytes.  Before using another page size, please read the section titled
      * "Index Page Size" in the Advantage Client Engine help file (ace.hlp)
      *}
      const ADS_MIN_ADI_PAGESIZE = 512;
      const ADS_MAX_ADI_PAGESIZE = 8192;

      {* data types *}
      const ADS_TYPE_UNKNOWN = 0;
      const ADS_LOGICAL = 1; {* 1 byte logical value *}
      const ADS_NUMERIC = 2; {* DBF character style numeric *}
      {* Date field.  With ADS_NTX, ADS_CDX, and
       * ADS_VFP< this is an 8 byte field of the form
       * CCYYMMDD.  With ADS_ADT, it is a 4 byte
       * Julian date. *}
      const ADS_DATE = 3;
      const ADS_STRING = 4; {* Character data *}
      const ADS_MEMO = 5; {* Variable length character data *}

      {* the following are extended data types *}
      const ADS_BINARY = 6; {* BLOB - any data *}
      const ADS_IMAGE = 7; {* BLOB - bitmap *}
      const ADS_VARCHAR = 8; {* variable length character field *}
      const ADS_COMPACTDATE = 9; {* DBF date represented with 3 bytes *}
      const ADS_DOUBLE = 10; {* IEEE 8 byte floating point *}
      const ADS_INTEGER = 11; {* IEEE 4 byte signed long integer *}

      {* the following are supported with the ADT format *}
      const ADS_SHORTINT = 12; {* IEEE 2 byte signed short integer *}
      {* 4 byte long integer representing
       * milliseconds since midnight *}
      const ADS_TIME = 13;
      {* 8 bytes.  High order 4 bytes are a
       * long integer representing Julian date.
       * Low order 4 bytes are a long integer
       * representing milliseconds since
       * midnight *}
      const ADS_TIMESTAMP = 14;
      const ADS_AUTOINC = 15; {* 4 byte auto-increment value *}
      const ADS_RAW = 16; {* Untranslated data *}
      const ADS_CURDOUBLE = 17; {* IEEE 8 byte floating point currency *}
      const ADS_MONEY = 18; {* 8 byte, 4 implied decimal Currency Field *}
      const ADS_LONGINT = 19; {* 8 byte integer *}
      const ADS_LONGLONG = 19; {* 8 byte integer. Deprecated. Use ADS_LONGINT instead. *}
      const ADS_CISTRING = 20; {* CaSe INSensiTIVE character data *}
      const ADS_ROWVERSION = 21; {* 8 byte integer, incremented for every update, unique to entire table *}
      const ADS_MODTIME = 22; {* 8 byte timestamp, updated when record is updated *}
      const ADS_VARCHAR_FOX = 23; {* Visual FoxPro varchar field *}
      const ADS_VARBINARY_FOX = 24; {* Visual FoxPro varbinary field *}
      const ADS_SYSTEM_FIELD = 25; {* For internal usage *}
      const ADS_NCHAR = 26; {* Unicode Character data *}
      const ADS_NVARCHAR = 27; {* Unpadded Unicode Character data *}
      const ADS_NMEMO = 28; {* Variable Length Unicode Data *}
      const ADS_GUID = 29; {* 16-byte binary data *}

      {*
      * supported User Defined Function types to be used with AdsRegisterUDF
      *}
      const ADS_INDEX_UDF = 1;

      {*
      * Constant for AdsMgGetConfigInfo
      *}
      const ADS_MAX_CFG_PATH = 256;

      {*
      * Constants for AdsMgGetServerType
      * Note ADS_MGMT_NETWARE_SERVER remains for backwards compatibility only
      *}
      const ADS_MGMT_NETWARE_SERVER = 1;
      const ADS_MGMT_NETWARE4_OR_OLDER_SERVER = 1;
      const ADS_MGMT_NT_SERVER = 2;
      const ADS_MGMT_LOCAL_SERVER = 3;
      const ADS_MGMT_WIN9X_SERVER = 4;
      const ADS_MGMT_NETWARE5_OR_NEWER_SERVER = 5;
      const ADS_MGMT_LINUX_SERVER = 6;
      const ADS_MGMT_NT_SERVER_64_BIT = 7;
      const ADS_MGMT_LINUX_SERVER_64_BIT = 8;

      {*
      * Constants for AdsMgGetLockOwner
      *}
      const ADS_MGMT_NO_LOCK = 1;
      const ADS_MGMT_RECORD_LOCK = 2;
      const ADS_MGMT_FILE_LOCK = 3;

      {*
      * Constants for MgGetInstallInfo
      *}
      const ADS_REG_OWNER_LEN = 36;
      const ADS_REVISION_LEN = 16;
      const ADS_INST_DATE_LEN = 16;
      const ADS_OEM_CHAR_NAME_LEN = 16;
      const ADS_ANSI_CHAR_NAME_LEN = 16;
      const ADS_SERIAL_NUM_LEN = 16;

      {*
      * Constants for MgGetOpenTables
      *}
      const ADS_MGMT_MAX_PATH = 260;
      const ADS_MGMT_PROPRIETARY_LOCKING = 1;
      const ADS_MGMT_CDX_LOCKING = 2;
      const ADS_MGMT_NTX_LOCKING = 3;
      const ADS_MGMT_ADT_LOCKING = 4;
      const ADS_MGMT_COMIX_LOCKING = 5;

      const ADS_MAX_USER_NAME = 50;

      const ADS_MAX_ADDRESS_SIZE = 30;
      const ADS_MAX_MGMT_APPID_SIZE = 70;

      {*
      * Data dictionary properties related constants and structure
      *}

      const ADS_DD_PROPERTY_NOT_AVAIL = $FFFF;
      const ADS_DD_MAX_PROPERTY_LEN = $FFFE;
      const ADS_DD_MAX_OBJECT_NAME_LEN = 200;

      const ADS_DD_UNKNOWN_OBJECT = 0;
      const ADS_DD_TABLE_OBJECT = 1;
      const ADS_DD_RELATION_OBJECT = 2;
      const ADS_DD_INDEX_FILE_OBJECT = 3;
      const ADS_DD_FIELD_OBJECT = 4;
      const ADS_DD_COLUMN_OBJECT = 4;
      const ADS_DD_INDEX_OBJECT = 5;
      const ADS_DD_VIEW_OBJECT = 6;
      const ADS_DD_VIEW_OR_TABLE_OBJECT = 7; {* Used in AdsFindFirst/NextTable *}
      const ADS_DD_USER_OBJECT = 8;
      const ADS_DD_USER_GROUP_OBJECT = 9;
      const ADS_DD_PROCEDURE_OBJECT = 10;
      const ADS_DD_DATABASE_OBJECT = 11;
      const ADS_DD_LINK_OBJECT = 12;
      const ADS_DD_TABLE_VIEW_OR_LINK_OBJECT = 13; {* Used in v6.2 AdsFindFirst/NextTable *}
      const ADS_DD_TRIGGER_OBJECT = 14;
      const ADS_DD_PUBLICATION_OBJECT = 15;
      const ADS_DD_ARTICLE_OBJECT = 16; {* the things (tables) that get published *}
      const ADS_DD_SUBSCRIPTION_OBJECT = 17; {* indicates where a publication goes *}
      const ADS_DD_FUNCTION_OBJECT = 18; {* User defined function *}
      const ADS_DD_PACKAGE_OBJECT = 19; {* function and stored procedure packages *}
      const ADS_DD_QUALIFIED_TRIGGER_OBJ = 20; {* Used in AdsDDFindFirst/NextObject *}
      const ADS_DD_PERMISSION_OBJECT = 21;
      const ADS_DD_DATABASE_TRIGGER_OBJ = 22; {* Used in AdsDDFindFirst/NextObject *}

      {* Common properties numbers < 100 *}
      const ADS_DD_COMMENT = 1;
      const ADS_DD_VERSION = 2;
      const ADS_DD_USER_DEFINED_PROP = 3;
      const ADS_DD_OBJECT_NAME = 4;
      const ADS_DD_TRIGGERS_DISABLED = 5;
      const ADS_DD_OBJECT_ID = 6;
      const ADS_DD_OPTIONS = 7;

      {* bit options for ADS_DD_QUERY_VIA_ROOT *}
      const ADS_DD_QVR_OPT_QUERY = $00000001;
      const ADS_DD_QVR_OPT_PROCEDURE = $00000002;

      {* Database properties between 100 and 199 *}
      const ADS_DD_DEFAULT_TABLE_PATH = 100;
      const ADS_DD_ADMIN_PASSWORD = 101;
      const ADS_DD_TEMP_TABLE_PATH = 102;
      const ADS_DD_LOG_IN_REQUIRED = 103;
      const ADS_DD_VERIFY_ACCESS_RIGHTS = 104;
      const ADS_DD_ENCRYPT_TABLE_PASSWORD = 105;
      const ADS_DD_ENCRYPT_NEW_TABLE = 106;
      const ADS_DD_ENABLE_INTERNET = 107;
      const ADS_DD_INTERNET_SECURITY_LEVEL = 108;
      const ADS_DD_MAX_FAILED_ATTEMPTS = 109;
      const ADS_DD_ALLOW_ADSSYS_NET_ACCESS = 110;
      const ADS_DD_VERSION_MAJOR = 111; {* properties for customer dd version *}
      const ADS_DD_VERSION_MINOR = 112;
      const ADS_DD_LOGINS_DISABLED = 113;
      const ADS_DD_LOGINS_DISABLED_ERRSTR = 114;
      const ADS_DD_FTS_DELIMITERS = 115;
      const ADS_DD_FTS_NOISE = 116;
      const ADS_DD_FTS_DROP_CHARS = 117;
      const ADS_DD_FTS_CONDITIONAL_CHARS = 118;
      const ADS_DD_ENCRYPTED = 119;
      const ADS_DD_ENCRYPT_INDEXES = 120;
      const ADS_DD_QUERY_LOG_TABLE = 121;
      const ADS_DD_ENCRYPT_COMMUNICATION = 122;
      const ADS_DD_DEFAULT_TABLE_RELATIVE_PATH = 123;
      const ADS_DD_TEMP_TABLE_RELATIVE_PATH = 124;
      const ADS_DD_DISABLE_DLL_CACHING = 125;
      const ADS_DD_DATA_ENCRYPTION_TYPE = 126;
      const ADS_DD_FTS_DELIMITERS_W = 127;
      const ADS_DD_FTS_NOISE_W = 128;
      const ADS_DD_FTS_DROP_CHARS_W = 129;
      const ADS_DD_FTS_CONDITIONAL_CHARS_W = 130;
      const ADS_DD_QUERY_VIA_ROOT = 131;
      const ADS_DD_ENFORCE_MAX_FAILED_LOGINS = 132;
      const ADS_DD_DATABASE_TRIGGER_TYPES = 133; {* for internal use *}

      {* Table properties between 200 and 299 *}
      const ADS_DD_TABLE_VALIDATION_EXPR = 200;
      const ADS_DD_TABLE_VALIDATION_MSG = 201;
      const ADS_DD_TABLE_PRIMARY_KEY = 202;
      const ADS_DD_TABLE_AUTO_CREATE = 203;
      const ADS_DD_TABLE_TYPE = 204;
      const ADS_DD_TABLE_PATH = 205;
      const ADS_DD_TABLE_FIELD_COUNT = 206;
      const ADS_DD_TABLE_RI_GRAPH = 207;
      const ADS_DD_TABLE_OBJ_ID = 208;
      const ADS_DD_TABLE_RI_XY = 209;
      const ADS_DD_TABLE_IS_RI_PARENT = 210;
      const ADS_DD_TABLE_RELATIVE_PATH = 211;
      const ADS_DD_TABLE_CHAR_TYPE = 212;
      const ADS_DD_TABLE_DEFAULT_INDEX = 213;
      const ADS_DD_TABLE_ENCRYPTION = 214;
      const ADS_DD_TABLE_MEMO_BLOCK_SIZE = 215;
      const ADS_DD_TABLE_PERMISSION_LEVEL = 216;
      const ADS_DD_TABLE_TRIGGER_TYPES = 217;
      const ADS_DD_TABLE_TRIGGER_OPTIONS = 218;
      const ADS_DD_TABLE_CACHING = 219;
      const ADS_DD_TABLE_TXN_FREE = 220;
      const ADS_DD_TABLE_VALIDATION_EXPR_W = 221;
      const ADS_DD_TABLE_WEB_DELTA = 222;
      const ADS_DD_TABLE_CONCURRENCY_ENABLED = 223; // for OData concurrency control

      {* Bit values for the ADS_DD_FIELD_OPTIONS property *}
      const ADS_DD_FIELD_OPT_VFP_BINARY = $00000001; {* field has NOCPTRANS option *}
      const ADS_DD_FIELD_OPT_VFP_NULLABLE = $00000002; {* field can be physicall set to NULL *}
      const ADS_DD_FIELD_OPT_COMPRESSED = $00010000; {* Field may be compressed, ADT memo, nmemo and blob *}

      {* Field properties between 300 - 399 *}
      const ADS_DD_FIELD_DEFAULT_VALUE = 300;
      const ADS_DD_FIELD_CAN_NULL = 301;
      const ADS_DD_FIELD_MIN_VALUE = 302;
      const ADS_DD_FIELD_MAX_VALUE = 303;
      const ADS_DD_FIELD_VALIDATION_MSG = 304;
      const ADS_DD_FIELD_DEFINITION = 305;
      const ADS_DD_FIELD_TYPE = 306;
      const ADS_DD_FIELD_LENGTH = 307;
      const ADS_DD_FIELD_DECIMAL = 308;
      const ADS_DD_FIELD_NUM = 309;
      const ADS_DD_FIELD_OPTIONS = 310;
      const ADS_DD_FIELD_DEFAULT_VALUE_W = 311;
      const ADS_DD_FIELD_MIN_VALUE_W = 312;
      const ADS_DD_FIELD_MAX_VALUE_W = 313;

      {* Index tag properties between 400 - 499 *}
      const ADS_DD_INDEX_FILE_NAME = 400;
      const ADS_DD_INDEX_EXPRESSION = 401;
      const ADS_DD_INDEX_CONDITION = 402;
      const ADS_DD_INDEX_OPTIONS = 403;
      const ADS_DD_INDEX_KEY_LENGTH = 404;
      const ADS_DD_INDEX_KEY_TYPE = 405;
      const ADS_DD_INDEX_FTS_MIN_LENGTH = 406;
      const ADS_DD_INDEX_FTS_DELIMITERS = 407;
      const ADS_DD_INDEX_FTS_NOISE = 408;
      const ADS_DD_INDEX_FTS_DROP_CHARS = 409;
      const ADS_DD_INDEX_FTS_CONDITIONAL_CHARS = 410;
      const ADS_DD_INDEX_COLLATION = 411;
      const ADS_DD_INDEX_FTS_DELIMITERS_W = 412;
      const ADS_DD_INDEX_FTS_NOISE_W = 413;
      const ADS_DD_INDEX_FTS_DROP_CHARS_W = 414;
      const ADS_DD_INDEX_FTS_CONDITIONAL_CHARS_W = 415;

      {* RI properties between 500-599 *}
      const ADS_DD_RI_PARENT_GRAPH = 500;
      const ADS_DD_RI_PRIMARY_TABLE = 501;
      const ADS_DD_RI_PRIMARY_INDEX = 502;
      const ADS_DD_RI_FOREIGN_TABLE = 503;
      const ADS_DD_RI_FOREIGN_INDEX = 504;
      const ADS_DD_RI_UPDATERULE = 505;
      const ADS_DD_RI_DELETERULE = 506;
      const ADS_DD_RI_NO_PKEY_ERROR = 507;
      const ADS_DD_RI_CASCADE_ERROR = 508;

      {* User properties between 600-699 *}
      const ADS_DD_USER_GROUP_NAME = 600;

      {* View properties between 700-749 *}
      const ADS_DD_VIEW_STMT = 700;
      const ADS_DD_VIEW_STMT_LEN = 701;
      const ADS_DD_VIEW_TRIGGER_TYPES = 702;
      const ADS_DD_VIEW_TRIGGER_OPTIONS = 703;
      const ADS_DD_VIEW_STMT_W = 704;

      {* Stored procedure properties 800-899 *}
      const ADS_DD_PROC_INPUT = 800;
      const ADS_DD_PROC_OUTPUT = 801;
      const ADS_DD_PROC_DLL_NAME = 802;
      const ADS_DD_PROC_DLL_FUNCTION_NAME = 803;
      const ADS_DD_PROC_INVOKE_OPTION = 804;
      const ADS_DD_PROC_SCRIPT = 805;
      const ADS_DD_PROC_SCRIPT_W = 806;

      {* Index file properties 900-999 *}
      const ADS_DD_INDEX_FILE_PATH = 900;
      const ADS_DD_INDEX_FILE_PAGESIZE = 901;
      const ADS_DD_INDEX_FILE_RELATIVE_PATH = 902;
      const ADS_DD_INDEX_FILE_TYPE = 903;

      {*
      * Object rights properties 1001 - 1099 .  They can be used
      * with either user or user group objects.
      *}
      const ADS_DD_TABLES_RIGHTS = 1001;
      const ADS_DD_VIEWS_RIGHTS = 1002;
      const ADS_DD_PROCS_RIGHTS = 1003;
      const ADS_DD_OBJECTS_RIGHTS = 1004;
      const ADS_DD_FREE_TABLES_RIGHTS = 1005;

      {* User Properties 1101 - 1199 *}
      const ADS_DD_USER_PASSWORD = 1101;
      const ADS_DD_USER_GROUP_MEMBERSHIP = 1102;
      const ADS_DD_USER_BAD_LOGINS = 1103;
      const ADS_DD_CURRENT_USER_PASSWORD = 1104;
      const ADS_DD_REQUIRE_OLD_PASSWORD = 1105;

      {* User group Properties 1201 - 1299 *}
      {* None at this moment. *}

      {* Link properties 1301 - 1399 *}
      const ADS_DD_LINK_PATH = 1300;
      const ADS_DD_LINK_OPTIONS = 1301;
      const ADS_DD_LINK_USERNAME = 1302;
      const ADS_DD_LINK_RELATIVE_PATH = 1303;

      {* Trigger properties 1400 - 1499 *}
      const ADS_DD_TRIG_TABLEID = 1400;
      const ADS_DD_TRIG_EVENT_TYPE = 1401;
      const ADS_DD_TRIG_TRIGGER_TYPE = 1402;
      const ADS_DD_TRIG_CONTAINER_TYPE = 1403;
      const ADS_DD_TRIG_CONTAINER = 1404;
      const ADS_DD_TRIG_FUNCTION_NAME = 1405;
      const ADS_DD_TRIG_PRIORITY = 1406;
      const ADS_DD_TRIG_OPTIONS = 1407;
      const ADS_DD_TRIG_TABLENAME = 1408;
      const ADS_DD_TRIG_CONTAINER_W = 1409;

      {* Publication properties 1500 - 1599 *}
      const ADS_DD_PUBLICATION_OPTIONS = 1500;

      {* Publication article properties 1600 - 1699 *}
      const ADS_DD_ARTICLE_FILTER = 1600; // horizontal filter (optional)
      const ADS_DD_ARTICLE_ID_COLUMNS = 1601; // columns that identify the target row
      const ADS_DD_ARTICLE_ID_COLUMN_NUMBERS = 1602; // array of the field numbers
      const ADS_DD_ARTICLE_FILTER_SHORT = 1603; // short version of the expression
      const ADS_DD_ARTICLE_INCLUDE_COLUMNS = 1604; // Vertical filter (inclusion list)
      const ADS_DD_ARTICLE_EXCLUDE_COLUMNS = 1605; // Vertical filter (exclusion list)
      const ADS_DD_ARTICLE_INC_COLUMN_NUMBERS = 1606; // Retrieve column nums to replicate
      const ADS_DD_ARTICLE_INSERT_MERGE = 1607; // Use SQL MERGE with INSERTs
      const ADS_DD_ARTICLE_UPDATE_MERGE = 1608; // Use SQL MERGE with UPDATEs
      const ADS_DD_ARTICLE_FILTER_W = 1609; // horizontal filter (optional)

      {* Subscription article properties 1700 - 1799 *}
      const ADS_DD_SUBSCR_PUBLICATION_NAME = 1700; // Name of the publication (for reading)
      const ADS_DD_SUBSCR_TARGET = 1701; // full path of target database
      const ADS_DD_SUBSCR_USERNAME = 1702; // user name to use to connect to target
      const ADS_DD_SUBSCR_PASSWORD = 1703; // password for connecting
      const ADS_DD_SUBSCR_FORWARD = 1704; // boolean flag:  forward updates that came from a replication?
      const ADS_DD_SUBSCR_ENABLED = 1705; // boolean flag:  Replication enabled on this subscription?
      const ADS_DD_SUBSCR_QUEUE_NAME = 1706; // replication queue
      const ADS_DD_SUBSCR_OPTIONS = 1707; // for future use
      const ADS_DD_SUBSCR_QUEUE_NAME_RELATIVE = 1708; // replication queue relative to the DD
      const ADS_DD_SUBSCR_PAUSED = 1709; // boolean flag:  Replication paused on this subscription?
      const ADS_DD_SUBSCR_COMM_TCP_IP = 1710; // boolean flag:  TRUE for TCP/IP communications
      const ADS_DD_SUBSCR_COMM_TCP_IP_V6 = 1711; // boolean flag:  TRUE for TCP/IP V6 communications
      const ADS_DD_SUBSCR_COMM_UDP_IP = 1712; // boolean flag:  TRUE for UDP/IP communications
      const ADS_DD_SUBSCR_COMM_IPX = 1713; // boolean flag:  TRUE for IPX communications
      const ADS_DD_SUBSCR_OPTIONS_INTERNAL = 1714; // internal ID to get ALL options incl COMM types
      const ADS_DD_SUBSCR_COMM_TLS = 1715; // boolean flag:  TRUE for TLS communications
      const ADS_DD_SUBSCR_CONNECTION_STR = 1716; // Free form connection string for new AdsConnect101 API

      {* AdsMgKillUser90 Constants *}
      const ADS_PROPERTY_UNSPECIFIED = $0000;
      const ADS_DONT_KILL_APPID = $0001;
      const ADS_RESTRICT_KILL = $0002;

      const ADS_DD_LEVEL_0 = 0;
      const ADS_DD_LEVEL_1 = 1;
      const ADS_DD_LEVEL_2 = 2;

      {* Referential Integrity (RI) update and delete rules *}
      const ADS_DD_RI_CASCADE = 1;
      const ADS_DD_RI_RESTRICT = 2;
      const ADS_DD_RI_SETNULL = 3;
      const ADS_DD_RI_SETDEFAULT = 4;

      {* Default Field Value Options *}
      const ADS_DD_DFV_UNKNOWN = 1;
      const ADS_DD_DFV_NONE = 2;
      const ADS_DD_DFV_VALUES_STORED = 3;

      {* Supported permissions in the data dictionary *}
      const ADS_PERMISSION_NONE = $00000000;
      const ADS_PERMISSION_READ = $00000001;
      const ADS_PERMISSION_UPDATE = $00000002;
      const ADS_PERMISSION_EXECUTE = $00000004;
      const ADS_PERMISSION_INHERIT = $00000008;
      const ADS_PERMISSION_INSERT = $00000010;
      const ADS_PERMISSION_DELETE = $00000020;
      const ADS_PERMISSION_LINK_ACCESS = $00000040;
      const ADS_PERMISSION_CREATE = $00000080;
      const ADS_PERMISSION_ALTER = $00000100;
      const ADS_PERMISSION_DROP = $00000200;
      const ADS_PERMISSION_WITH_GRANT = $80000000;
      const ADS_PERMISSION_ALL_WITH_GRANT = $8FFFFFFF;
      const ADS_PERMISSION_ALL = $FFFFFFFF;

      {*
      * special code that can be used as the input to specify
      * which special permission to retrieve.
      *}
      const ADS_GET_PERMISSIONS_WITH_GRANT = $8000FFFF;
      const ADS_GET_PERMISSIONS_CREATE = $FFFF0080;
      const ADS_GET_PERMISSIONS_CREATE_WITH_GRANT = $8FFFFF8F;

      {* Link DD options *}
      const ADS_LINK_GLOBAL = $00000001;
      const ADS_LINK_AUTH_ACTIVE_USER = $00000002;
      const ADS_LINK_PATH_IS_STATIC = $00000004;

      {* Trigger event types *}
      const ADS_TRIGEVENT_INSERT = 1;
      const ADS_TRIGEVENT_UPDATE = 2;
      const ADS_TRIGEVENT_DELETE = 3;

      {* Dictionary (system) trigger event types *}
      const ADS_TRIGEVENT_OPEN_TABLE = 4;
      const ADS_TRIGEVENT_CLOSE_TABLE = 5;
      const ADS_TRIGEVENT_CONNECT = 6;
      const ADS_TRIGEVENT_DISCONNECT = 7;

      {* Trigger types *}
      const ADS_TRIGTYPE_BEFORE = $00000001;
      const ADS_TRIGTYPE_INSTEADOF = $00000002;
      const ADS_TRIGTYPE_AFTER = $00000004;
      const ADS_TRIGTYPE_CONFLICTON = $00000008;

      {* Trigger container types *}
      const ADS_TRIG_WIN32DLL = 1;
      const ADS_TRIG_COM = 2;
      const ADS_TRIG_SCRIPT = 3;

      {*
      * Trigger options, if changed or adding more please inspect code
      * in RemoveTriggerFromDictionary
      *}
      const ADS_TRIGOPTIONS_NO_VALUES = $00000000;
      const ADS_TRIGOPTIONS_WANT_VALUES = $00000001;
      const ADS_TRIGOPTIONS_WANT_MEMOS_AND_BLOBS = $00000002;
      const ADS_TRIGOPTIONS_DEFAULT = $00000003; {* default is to include vals and memos *}
      const ADS_TRIGOPTIONS_NO_TRANSACTION = $00000004; {* don't use implicit transactions *}

      {*
      * Table permission verification levels.
      * level 1 is all columns searchable, even those without permission.
      * level 2 is default. Permission to the column is required to search or filter on a column.
      * level 3 is most restricted. Only static SQL cursor is allowed.
      *}
      const ADS_DD_TABLE_PERMISSION_LEVEL_1 = 1;
      const ADS_DD_TABLE_PERMISSION_LEVEL_2 = 2;
      const ADS_DD_TABLE_PERMISSION_LEVEL_3 = 3;

      {* AdsDDRenameObject options *}
      const ADS_KEEP_TABLE_FILE_NAME = $00000001;

      {* AdsDDCreateArticle options *}
      const ADS_IDENTIFY_BY_PRIMARY = $00000001;
      const ADS_IDENTIFY_BY_ALL = $00000002;

      {* AdsDDCreateSubscription options *}
      const ADS_SUBSCR_QUEUE_IS_STATIC = $00000001;
      const ADS_SUBSCR_AIS_TARGET = $00000002; // use AIS to connect to target
      const ADS_SUBSCR_IGNORE_FAILED_REP = $00000004; // Delete failed replication updates from the queue
      const ADS_SUBSCR_LOG_FAILED_REP_DATA = $00000008; // if set, show data of failed replication updates in
                                                           // the error log.
      //#define ADS_UDP_IP_CONNECTION         0x00000020   // These connection type constants are stored in the
      //#define ADS_IPX_CONNECTION            0x00000040   // options of the subscription, so don't use these values
      //#define ADS_TCP_IP_CONNECTION         0x00000080   // for other subscription properties.
      //#define ADS_TCP_IP_V6_CONNECTION      0x00000100

      {* AdsGetFieldLength10 options *}
      const ADS_CODEUNIT_LENGTH = ADS_DEFAULT; // length in code units (characters)
      const ADS_BYTE_LENGTH = $00000001; // length in bytes
      const ADS_BYTE_LENGTH_IN_BUFFER = $00000002; // physical length of data in bytes in the record buffer

      {* Options for AdsFindServers *}
      const ADS_FS_MULTICAST_ONLY = $00000001; // Only perform multicast step
      const ADS_FS_CONNECT_ALL = $00000002; // Attempt to connect to every address returned
                                                   // in order to gather server name

      {* Table caching property modes, used with AdsDDSetTableProperty etc. *}
      const ADS_TABLE_CACHE_NONE = $0000;
      const ADS_TABLE_CACHE_READS = $0001;
      const ADS_TABLE_CACHE_WRITES = $0002;

      {* Connection string encryption options as strings. *}
      const ADS_ENCRYPT_STRING_RC4 = 'RC4';
      const ADS_ENCRYPT_STRING_AES128 = 'AES128';
      const ADS_ENCRYPT_STRING_AES256 = 'AES256';

      {* Connection string cipher suite options as strings. *}
      const ADS_CIPHER_SUITE_STRING_RC4 = 'RC4-MD5';
      const ADS_CIPHER_SUITE_STRING_AES128 = 'AES128-SHA';
      const ADS_CIPHER_SUITE_STRING_AES256 = 'AES256-SHA';

      {*
      * System alias which always resolves to the root dictionary
      * defined by ADS_ROOT_DICTIONARY server configuration setting
      *}
      const ADS_ROOT_DD_ALIAS = '__rootdd';

      {* Options for the AdsGetKeyFilter *}
      const ADS_FILTER_FORMAT_ODATA = $00000001;
      const ADS_FILTER_ENCODE_UTF8 = $00000002;
      { The following Management API values can be freely changed by you to fit
        your application's needs.}
      const ADS_LOCK_ARRAY_SIZE = 400;
      const ADS_THREAD_ARRAY_SIZE = 50;
      const ADS_TABLE_ARRAY_SIZE = 200;
      const ADS_INDEX_ARRAY_SIZE = 200;
      const ADS_USER_ARRAY_SIZE = 200;
      const ADS_DD_MAX_LINK_INFO_SIZE = 2 * ADS_DD_MAX_OBJECT_NAME_LEN + ADS_MAX_PATH + 3;


      type
      PAceChar = PAnsiChar;
      AceChar = AnsiChar;

      {$IFDEF ADSDELPHI2008_OR_NEWER}
      const ADS_MAX_FIELD_TYPE = 30;
      type PAceBinary = PByte;
      {$ELSE}
      const ADS_MAX_FIELD_TYPE = 25;
      type PAceBinary = PChar;
      {$ENDIF}

      { This data type defines what type of function to pass to
        AdsRegisterCallbackFunction(). }
      TCallbackFunction = function( usPercent: Word;
                                    ulCallbackID: Longint ): Longint; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

{$IFDEF ADSDELPHI4_OR_NEWER}
      TCallbackFunction101 = function( usPercent: Word;
                                       qCallbackID: Int64 ): Longint; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
{$ENDIF}

      { This data type defines what type of function to pass to
        AdsRegisterProgressCallback(). }
      TProgressCallback = function( usPercent: Word ): Longint; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      { This data type defines what type of function to pass to
        AdsRegisterSQLAbortFunc(). }
      TSQLAbortFunc = function: Longint; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      { This data type defines the type of function that AdsRegisterUDF() takes
        as a parameter.  This should be used to cast the real function, which has
        different parameters.  See the documentation for AdsRegisterUDF for more
        information }
      TUDFFunc = function: Longint; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      { This type allows a numeric field value to be passed into functions
        that expect field names.  If the user prefers to use column number,
        then calls like this can be made:
        ulRet = AdsGetDouble( hTable, ADSFIELD( 1 ), &dVal );
        Where the first column is a numeric value to retrieve. }
      ADSFIELD = PAceChar;

      {$IFNDEF FPC}
         {$IFDEF ADSDELPHIXE2_OR_NEWER}
            AdsNativeInt = NativeInt;    // Architecture-matching (32/64-bit) Signed Int
            AdsNativeUInt = NativeUInt;  // Architecutre-matching (32/64-bit) Unsigned Int
         {$ELSE}
            {*
             * The IDE is pre-Delpih XE2.  No x64, so these will just be 32-bit signed/unsigned integers
             *}
            AdsNativeInt = LongInt;      // 32-bit signed integer
            {$IFDEF ADSDELPHI4_OR_NEWER}
               AdsNativeUInt = LongWord;    // Same as definition for UNSIGNED32 below
            {$ELSE}
               AdsNativeUInt = LongInt;     // Same as definition for UNSIGNED32 below
            {$ENDIF}

         {$ENDIF}
      {$ELSE}
         {*
          * FreePascal already has architecture-matching types.  We'll alias those for consistency.
          *}
         AdsNativeInt = PtrInt;
         AdsNativeUInt = PtrUInt;
      {$ENDIF}

      PAdsNativeInt  = ^AdsNativeInt;
      PAdsNativeUInt = ^AdsNativeUInt;

      { data type definitions }
      ADSHANDLE      = AdsNativeUInt;

      {*      * With data dicitonaries it is possible for a table to have more than
      * fifty index tags.  If you need more than fifty tags you will
      * need to declare a new array with a larger size.
      *}
      ADSIndexArray  = array[0..ADS_MAX_TAGS - 1] of ADSHANDLE;
      PADSIndexArray = ^ADSIndexArray;
      PADSHANDLE     = ^ADSHANDLE;
{$IFNDEF FPC}
      PDOUBLE        = ^Double;
      PWord          = ^Word;
{$ENDIF}
      SIGNED16       = SmallInt;
      PSIGNED16      = ^SmallInt;
      UNSIGNED16     = Word;
      PUNSIGNED16    = ^Word;
      SIGNED32       = Longint;
      PSIGNED32      = ^SIGNED32;
      {$IFDEF ADSDELPHI4_OR_NEWER }
      SIGNED64       = Int64;
      PSIGNED64      = ^SIGNED64;
      {$ENDIF}
      {$IFDEF ADSDELPHI2007_OR_NEWER }
      UNSIGNED64     = UInt64;
      {$ELSE}
      UNSIGNED64     = Int64;
      {$ENDIF}
      PUNSIGNED64    = ^UNSIGNED64;
      {$IFDEF ADSDELPHI4_OR_NEWER }
      UNSIGNED32     = LongWord;
      {$ELSE}
      UNSIGNED32     = Longint;
      {$ENDIF}
      PUNSIGNED32    = ^UNSIGNED32;
      SIGNED8        = BYTE;
      UNSIGNED8      = BYTE;
{$IFNDEF FPC}
      pPChar         = ^PAceChar;
      pPWideChar     = ^PWideChar;
{$ENDIF}

      {*
      * Management API structures
      *}
      ADS_MGMT_COMM_STATS = packed record
         dPercentCheckSums:   double;   { 0f pkts with checksum failures }
         ulTotalPackets:   UNSIGNED32;   { Total packets received }
         ulRcvPktOutOfSeq:   UNSIGNED32;   { Receive packets out of sequence }
         ulNotLoggedIn:   UNSIGNED32;   { Packet owner not logged in }
         ulRcvReqOutOfSeq:   UNSIGNED32;   { Receive requests out of sequence }
         ulCheckSumFailures:   UNSIGNED32;   { Checksum failures }
         ulDisconnectedUsers:   UNSIGNED32;   { Server initiated disconnects }
         ulPartialConnects:   UNSIGNED32;   { Removed partial connections }
         ulInvalidPackets:   UNSIGNED32;   { Rcvd invalid packets (NT only) }
         ulRecvFromErrors:   UNSIGNED32;   { RecvFrom failed (NT only) }
         ulSendToErrors:   UNSIGNED32;   { SendTo failed (NT only) }
      end;

      pADS_MGMT_COMM_STATS = ^ADS_MGMT_COMM_STATS;

      ADS_MGMT_CONFIG_PARAMS = packed record
         ulNumConnections:   UNSIGNED32;   { number connections }
         ulNumWorkAreas:   UNSIGNED32;   { number work areas }
         ulNumTables:   UNSIGNED32;   { number tables }
         ulNumIndexes:   UNSIGNED32;   { number indexes }
         ulNumLocks:   UNSIGNED32;   { number locks }
         ulUserBufferSize:   UNSIGNED32;   { user buffer }
         ulStatDumpInterval:   UNSIGNED32;   { statistics dump interval }
         ulErrorLogMax:   UNSIGNED32;   { max size of error log }
         ulNumTPSHeaderElems:   UNSIGNED32;   { number TPS header elems }
         ulNumTPSVisibilityElems:   UNSIGNED32;   { number TPS vis elems }
         ulNumTPSMemoTransElems:   UNSIGNED32;   { number TPS memo elems }
         usNumRcvECBs: UNSIGNED16;   { number rcv ECBs (NLM only) }
         usNumSendECBs:   UNSIGNED16;   { number send ECBs (NLM only) }
         usNumBurstPackets:   UNSIGNED16;   { number packets per burst }
         usNumWorkerThreads:   UNSIGNED16;   { number worker threads }
         ulSortBuffSize:   UNSIGNED32;   { index sort buffer size }
         aucErrorLog:    array[0..ADS_MAX_CFG_PATH - 1] of AceChar;   { error log path }
         aucSemaphore:    array[0..ADS_MAX_CFG_PATH - 1] of AceChar;   { semaphore file path }
         aucTransaction:    array[0..ADS_MAX_CFG_PATH - 1] of AceChar;   { TPS log file path }

         ucReserved3:   UNSIGNED8;   { reserved }
         ucReserved4:   UNSIGNED8;   { reserved }
         usSendIPPort:   UNSIGNED16;   { NT Service IP send port # }
         usReceiveIPPort:   UNSIGNED16;   { NT Service IP rcv port # }
         ucUseIPProtocol:   UNSIGNED8;   { Win9x only. Which protocol to use }
         ucFlushEveryUpdate:   UNSIGNED8;   { Win9x specific }

         ulGhostTimeout:   UNSIGNED32;   { Diconnection time for partial connections }
         ulFlushFrequency:   UNSIGNED32;   { For local server only }

         ulKeepAliveTimeOut:   UNSIGNED32;   { When not using semaophore files. In milliseconds }
         ucDisplayNWLoginNames:   UNSIGNED8;   { Display connections using user names. }
         ucUseSemaphoreFiles:   UNSIGNED8;   { Whether or not to use semaphore files }
         ucUseDynamicAOFs:   UNSIGNED8;
         ucUseInternet:   UNSIGNED8;   { 0 if an Internet port is not specified. }

         usInternetPort:   UNSIGNED16;   { Internet Port }
         usMaxConnFailures:   UNSIGNED16;   { Maximum Internet connection failures allowed. }
         ulInternetKeepAlive:   UNSIGNED32;   { In Milliseconds }

         usCompressionLevel:   UNSIGNED16;   {  Compression option at server.  ADS_COMPRESS_NEVER, }
                                          { ADS_COMPRESS_INTERNET, or ADS_COMPRESS_ALWAYS }
         ulNumQueries:   UNSIGNED32;   { number of queries }
         usReceiveSSLPort:   UNSIGNED16;   { Port number used for SSL }

      end;

      pADS_MGMT_CONFIG_PARAMS = ^ADS_MGMT_CONFIG_PARAMS;

      ADS_MGMT_CONFIG_MEMORY = packed record
         ulTotalConfigMem:   UNSIGNED32;   { Total mem taken by cfg params }
         ulConnectionMem:   UNSIGNED32;   { memory taken by connections }
         ulWorkAreaMem:   UNSIGNED32;   { memory taken by work areas }
         ulTableMem:   UNSIGNED32;   { memory taken by tables }
         ulIndexMem:   UNSIGNED32;   { memory taken by indexes }
         ulLockMem:   UNSIGNED32;   { memory taken by locks }
         ulUserBufferMem:   UNSIGNED32;   { memory taken by user buffer }
         ulTPSHeaderElemMem:   UNSIGNED32;   { memory taken by TPS hdr elems }
         ulTPSVisibilityElemMem:   UNSIGNED32;   { memory taken by TPS vis elems }
         ulTPSMemoTransElemMem:   UNSIGNED32;   { mem taken by TPS memo elems }
         ulRcvEcbMem: UNSIGNED32;   { mem taken by rcv ECBs (NLM) }
         ulSendEcbMem:   UNSIGNED32;   { mem taken by send ECBs (NLM) }
         ulWorkerThreadMem:   UNSIGNED32;   { mem taken by worker threads }
         ulQueryMem:   UNSIGNED32;   { mem taken by queries }
      end;

      pADS_MGMT_CONFIG_MEMORY = ^ADS_MGMT_CONFIG_MEMORY;

      ADS_MGMT_INSTALL_INFO = packed record
         ulUserOption:   UNSIGNED32;   { User option purchased }
         aucRegisteredOwner:    array[0..ADS_REG_OWNER_LEN - 1] of AceChar;   { Registered owner }
         aucVersionStr:    array[0..ADS_REVISION_LEN - 1] of AceChar;   { Advantage version }
         aucInstallDate:    array[0..ADS_INST_DATE_LEN - 1] of AceChar;   { Install date string }
         aucOemCharName:    array[0..ADS_OEM_CHAR_NAME_LEN - 1] of AceChar;   { OEM char language }
         aucAnsiCharName:    array[0..ADS_ANSI_CHAR_NAME_LEN - 1] of AceChar;   { ANSI char language }
         aucEvalExpireDate:    array[0..ADS_INST_DATE_LEN - 1] of AceChar;   { Eval expiration date }
         aucSerialNumber:    array[0..ADS_SERIAL_NUM_LEN - 1] of AceChar;   { Serial number string }
         ulMaxStatefulUsers:   UNSIGNED32;   { How many stateful connections allowed }
         ulMaxStatelessUsers:   UNSIGNED32;   { How many stateless connections allowed }
      end;

      pADS_MGMT_INSTALL_INFO = ^ADS_MGMT_INSTALL_INFO;

      ADS_MGMT_UPTIME_INFO = packed record
         usDays:   UNSIGNED16;   { Number of days server has been up }
         usHours:   UNSIGNED16;   { Number of hours server has been up }
         usMinutes:   UNSIGNED16;   { Number of minutes server has been up }
         usSeconds:   UNSIGNED16;   { Number of seconds server has been up }
      end;

      pADS_MGMT_UPTIME_INFO = ^ADS_MGMT_UPTIME_INFO;

      ADS_MGMT_USAGE_INFO = packed record
         ulInUse:   UNSIGNED32;   { Number of items in use }
         ulMaxUsed:   UNSIGNED32;   { Max number of items ever used }
         ulRejected:   UNSIGNED32;   { Number of items rejected }
      end;

      pADS_MGMT_USAGE_INFO = ^ADS_MGMT_USAGE_INFO;

      ADS_MGMT_ACTIVITY_INFO = packed record
         ulOperations:   UNSIGNED32;   { Number operations since started }
         ulLoggedErrors:   UNSIGNED32;   { Number logged errors }
         stUpTime:   ADS_MGMT_UPTIME_INFO;   { Length of time ADS has been up }
         stUsers:   ADS_MGMT_USAGE_INFO;   { Users in use, max, rejected }
         stConnections:   ADS_MGMT_USAGE_INFO;   { Conns in use, max, rejected }
         stWorkAreas:   ADS_MGMT_USAGE_INFO;   { WAs in use, max, rejected }
         stTables:   ADS_MGMT_USAGE_INFO;   { Tables in use, max, rejected }
         stIndexes:   ADS_MGMT_USAGE_INFO;   { Indexes in use, max, rejected }
         stLocks:   ADS_MGMT_USAGE_INFO;   { Locks in use, max, rejected }
         stTpsHeaderElems:   ADS_MGMT_USAGE_INFO;   { TPS header elems in use, max }
         stTpsVisElems:   ADS_MGMT_USAGE_INFO;   { TPS vis elems in use, max }
         stTpsMemoElems:   ADS_MGMT_USAGE_INFO;   { TPS memo elems in use, max }
         stWorkerThreads:   ADS_MGMT_USAGE_INFO;   { Worker threads in use, max }
         stQueries:   ADS_MGMT_USAGE_INFO;   { Queries in use, max, rejected }
         stStatefulUsers:   ADS_MGMT_USAGE_INFO;   { Stateful users in use }
         stStatelessUsers:   ADS_MGMT_USAGE_INFO;   { Stateless users in use }
      end;

      pADS_MGMT_ACTIVITY_INFO = ^ADS_MGMT_ACTIVITY_INFO;

      ADS_MGMT_USER_INFO = packed record
         aucUserName:    array[0..ADS_MAX_USER_NAME - 1] of AceChar;   { Name of connected user }
         usConnNumber:   UNSIGNED16;   { NetWare conn # (NLM only) }
         aucDictionaryName:    array[0..ADS_MAX_USER_NAME - 1] of AceChar;   { Dictionary user name }
         aucAddress:    array[0..ADS_MAX_ADDRESS_SIZE - 1] of AceChar;   { Network address of user }
         aucOSUserLoginName:    array[0..ADS_MAX_USER_NAME - 1] of AceChar;   { OS user login name }
         aucTSAddress:    array[0..ADS_MAX_ADDRESS_SIZE - 1] of AceChar;   { Terminal Services client IP Address }
         aucApplicationID:    array[0..ADS_MAX_MGMT_APPID_SIZE - 1] of AceChar;   { application id }
         ulAveRequestCost:   UNSIGNED32;   { estimated average cost of each server request }
         usReserved1:   UNSIGNED16;   { reserved to maintain byte alignment (ace.pas structs are not packed) }
      end;

      pADS_MGMT_USER_INFO = ^ADS_MGMT_USER_INFO;
      ADSMgUserArray = array[0..ADS_USER_ARRAY_SIZE - 1] of ADS_MGMT_USER_INFO;
      pADSMgUserArray = ^ADSMgUserArray;

      ADS_MGMT_TABLE_INFO = packed record
         aucTableName:    array[0..ADS_MGMT_MAX_PATH - 1] of AceChar;   { Fully qualified table name }
         usLockType:   UNSIGNED16;   { Advantage locking mode }
      end;

      pADS_MGMT_TABLE_INFO = ^ADS_MGMT_TABLE_INFO;
      ADSMgTableArray = array[0..ADS_TABLE_ARRAY_SIZE - 1] of ADS_MGMT_TABLE_INFO;
      PADSMgTableArray = ^ADSMgTableArray;

      ADS_MGMT_INDEX_INFO = packed record
         aucIndexName:    array[0..ADS_MGMT_MAX_PATH - 1] of AceChar;   { Fully qualified table name }
      end;

      pADS_MGMT_INDEX_INFO = ^ADS_MGMT_INDEX_INFO;
      ADSMgIndexArray = array[0..ADS_INDEX_ARRAY_SIZE - 1] of ADS_MGMT_INDEX_INFO;
      PADSMgIndexArray = ^ADSMgIndexArray;

      ADS_MGMT_RECORD_INFO = packed record
         ulRecordNumber:   UNSIGNED32;   { Record number that is locked }
      end;

      pADS_MGMT_RECORD_INFO = ^ADS_MGMT_RECORD_INFO;
      ADSMgLocksArray = array[0..ADS_LOCK_ARRAY_SIZE - 1] of ADS_MGMT_RECORD_INFO;
      PADSMgLocksArray = ^ADSMgLocksArray;

      ADS_MGMT_THREAD_ACTIVITY = packed record
         ulThreadNumber:   UNSIGNED32;   { Thread Number }
         usOpCode:   UNSIGNED16;   { Operation in progress }
         aucUserName:    array[0..ADS_MAX_USER_NAME - 1] of AceChar;   { Name of user }
         usConnNumber:   UNSIGNED16;   { NetWare conn num (NLM only) }
         usReserved1:   UNSIGNED16;   { Reserved }
         aucOSUserLoginName:    array[0..ADS_MAX_USER_NAME - 1] of AceChar;   { OS user login name }
      end;

      pADS_MGMT_THREAD_ACTIVITY = ^ADS_MGMT_THREAD_ACTIVITY;
      ADSMgThreadsArray = array[0..ADS_THREAD_ARRAY_SIZE - 1] of ADS_MGMT_THREAD_ACTIVITY;
      PADSMgThreadsArray = ^ADSMgThreadsArray;

      {* GUID structure *}
      ADS_GUID_DATA = packed record
         Data1:   UNSIGNED32;
         Data2:   UNSIGNED16;
         Data3:   UNSIGNED16;
         Data4:   UNSIGNED64;
      end;

      pADS_GUID_DATA = ^ADS_GUID_DATA;

      ADD_FIELD_DESC = packed record
         usFieldType:   UNSIGNED16;
         usFieldLength:   UNSIGNED16;
         usFieldDecimal:   UNSIGNED16;
      end;

      pADD_FIELD_DESC = ^ADD_FIELD_DESC;


      function AdsAddCustomKey( hIndex: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsAppendRecord( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsApplicationExit:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsAtBOF( hTable: ADSHANDLE;
                         pbBof: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsAtEOF( hTable: ADSHANDLE;
                         pbEof: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsBeginTransaction( hConnect: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsBinaryToFile( hTable: ADSHANDLE;
                                pucFldName: PAceChar;
                                pucFileName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCacheOpenCursors( usOpen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCacheOpenTables( usOpen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCacheRecords( hTable: ADSHANDLE;
                                usNumRecords: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCancelUpdate( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCancelUpdate90( hTable: ADSHANDLE;
                                  ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCheckExistence( hConnect: ADSHANDLE;
                                  pucFileName: PAceChar;
                                  pusOnDisk: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearAllScopes( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearDefault:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearFilter( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearRelation( hTableParent: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearScope( hIndex: ADSHANDLE;
                              usScopeOption: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCloneTable( hTable: ADSHANDLE;
                              phClone: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCloseAllIndexes( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCloseAllTables:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCloseIndex( hIndex: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCloseTable( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCloseCachedTables( hConnection: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCommitTransaction( hConnect: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsConnect( pucServerName: PAceChar;
                           phConnect: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsConnect26( pucServerName: PAceChar;
                             usServerTypes: UNSIGNED16;
                             phConnect: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsConnect60( pucServerPath: PAceChar;
                             usServerTypes: UNSIGNED16;
                             pucUserName: PAceChar;
                             pucPassword: PAceChar;
                             ulOptions: UNSIGNED32;
                             phConnect: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsConnect101( pucConnectString: PAceChar;
                              phConnectionOptions: pADSHANDLE;
                              phConnect: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearCachePool( pucConnectString: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsReapUnusedConnections:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsConnectionAlive( hConnect: ADSHANDLE;
                                     pbConnectionIsAlive: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsContinue( hTable: ADSHANDLE;
                            pbFound: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsConvertTable( hObj: ADSHANDLE;
                                usFilterOption: UNSIGNED16;
                                pucFile: PAceChar;
                                usTableType: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCopyTable( hObj: ADSHANDLE;
                             usFilterOption: UNSIGNED16;
                             pucFile: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCopyTableContents( hObjFrom: ADSHANDLE;
                                     hTableTo: ADSHANDLE;
                                     usFilterOption: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCopyTableStructure( hTable: ADSHANDLE;
                                      pucFile: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateIndex( hObj: ADSHANDLE;
                               pucFileName: PAceChar;
                               pucTag: PAceChar;
                               pucExpr: PAceChar;
                               pucCondition: PAceChar;
                               pucWhile: PAceChar;
                               ulOptions: UNSIGNED32;
                               phIndex: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateIndex61( hObj: ADSHANDLE;
                                 pucFileName: PAceChar;
                                 pucTag: PAceChar;
                                 pucExpr: PAceChar;
                                 pucCondition: PAceChar;
                                 pucWhile: PAceChar;
                                 ulOptions: UNSIGNED32;
                                 ulPageSize: UNSIGNED32;
                                 phIndex: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateIndex90( hObj: ADSHANDLE;
                                 pucFileName: PAceChar;
                                 pucTag: PAceChar;
                                 pucExpr: PAceChar;
                                 pucCondition: PAceChar;
                                 pucWhile: PAceChar;
                                 ulOptions: UNSIGNED32;
                                 ulPageSize: UNSIGNED32;
                                 pucCollation: PAceChar;
                                 phIndex: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateFTSIndex( hTable: ADSHANDLE;
                                  pucFileName: PAceChar;
                                  pucTag: PAceChar;
                                  pucField: PAceChar;
                                  ulPageSize: UNSIGNED32;
                                  ulMinWordLen: UNSIGNED32;
                                  ulMaxWordLen: UNSIGNED32;
                                  usUseDefaultDelim: UNSIGNED16;
                                  pvDelimiters: pointer;
                                  usUseDefaultNoise: UNSIGNED16;
                                  pvNoiseWords: pointer;
                                  usUseDefaultDrop: UNSIGNED16;
                                  pvDropChars: pointer;
                                  usUseDefaultConditionals: UNSIGNED16;
                                  pvConditionalChars: pointer;
                                  pucCollation: PAceChar;
                                  pucReserved1: PAceChar;
                                  ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateTable( hConnection: ADSHANDLE;
                               pucName: PAceChar;
                               pucAlias: PAceChar;
                               usTableType: UNSIGNED16;
                               usCharType: UNSIGNED16;
                               usLockType: UNSIGNED16;
                               usCheckRights: UNSIGNED16;
                               usMemoSize: UNSIGNED16;
                               pucFields: PAceChar;
                               phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateTable71( hConnection: ADSHANDLE;
                                 pucName: PAceChar;
                                 pucDBObjName: PAceChar;
                                 usTableType: UNSIGNED16;
                                 usCharType: UNSIGNED16;
                                 usLockType: UNSIGNED16;
                                 usCheckRights: UNSIGNED16;
                                 usMemoSize: UNSIGNED16;
                                 pucFields: PAceChar;
                                 ulOptions: UNSIGNED32;
                                 phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateTable90( hConnection: ADSHANDLE;
                                 pucName: PAceChar;
                                 pucDBObjName: PAceChar;
                                 usTableType: UNSIGNED16;
                                 usCharType: UNSIGNED16;
                                 usLockType: UNSIGNED16;
                                 usCheckRights: UNSIGNED16;
                                 usMemoSize: UNSIGNED16;
                                 pucFields: PAceChar;
                                 ulOptions: UNSIGNED32;
                                 pucCollation: PAceChar;
                                 phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreate( pucDictionaryPath: PAceChar;
                            usEncrypt: UNSIGNED16;
                            pucDescription: PAceChar;
                            phDictionary: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreate101( pucConnectString: PAceChar;
                               phConnectOptions: pADSHANDLE;
                               phDictionary: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateRefIntegrity( hDictionary: ADSHANDLE;
                                        pucRIName: PAceChar;
                                        pucFailTable: PAceChar;
                                        pucParentTableName: PAceChar;
                                        pucParentTagName: PAceChar;
                                        pucChildTableName: PAceChar;
                                        pucChildTagName: PAceChar;
                                        usUpdateRule: UNSIGNED16;
                                        usDeleteRule: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateRefIntegrity62( hDictionary: ADSHANDLE;
                                          pucRIName: PAceChar;
                                          pucFailTable: PAceChar;
                                          pucParentTableName: PAceChar;
                                          pucParentTagName: PAceChar;
                                          pucChildTableName: PAceChar;
                                          pucChildTagName: PAceChar;
                                          usUpdateRule: UNSIGNED16;
                                          usDeleteRule: UNSIGNED16;
                                          pucNoPrimaryError: PAceChar;
                                          pucCascadeError: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRemoveRefIntegrity( hDictionary: ADSHANDLE;
                                        pucRIName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetDatabaseProperty( hObject: ADSHANDLE;
                                         usPropertyID: UNSIGNED16;
                                         pvProperty: pointer;
                                         pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetFieldProperty( hObject: ADSHANDLE;
                                      pucTableName: PAceChar;
                                      pucFieldName: PAceChar;
                                      usPropertyID: UNSIGNED16;
                                      pvProperty: pointer;
                                      pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetIndexFileProperty( hObject: ADSHANDLE;
                                          pucTableName: PAceChar;
                                          pucIndexFileName: PAceChar;
                                          usPropertyID: UNSIGNED16;
                                          pvProperty: pointer;
                                          pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetIndexProperty( hObject: ADSHANDLE;
                                      pucTableName: PAceChar;
                                      pucIndexName: PAceChar;
                                      usPropertyID: UNSIGNED16;
                                      pvProperty: pointer;
                                      pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetLinkProperty( hConnect: ADSHANDLE;
                                     pucLinkName: PAceChar;
                                     usPropertyID: UNSIGNED16;
                                     pvProperty: pointer;
                                     pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetTableProperty( hObject: ADSHANDLE;
                                      pucTableName: PAceChar;
                                      usPropertyID: UNSIGNED16;
                                      pvProperty: pointer;
                                      pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetUserGroupProperty( hObject: ADSHANDLE;
                                          pucUserGroupName: PAceChar;
                                          usPropertyID: UNSIGNED16;
                                          pvProperty: pointer;
                                          pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetUserProperty( hObject: ADSHANDLE;
                                     pucUserName: PAceChar;
                                     usPropertyID: UNSIGNED16;
                                     pvProperty: pointer;
                                     pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetViewProperty( hObject: ADSHANDLE;
                                     pucViewName: PAceChar;
                                     usPropertyID: UNSIGNED16;
                                     pvProperty: pointer;
                                     pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetTriggerProperty( hObject: ADSHANDLE;
                                        pucTriggerName: PAceChar;
                                        usPropertyID: UNSIGNED16;
                                        pvProperty: pointer;
                                        pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetProcedureProperty( hObject: ADSHANDLE;
                                          pucProcName: PAceChar;
                                          usPropertyID: UNSIGNED16;
                                          pvProperty: pointer;
                                          pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetRefIntegrityProperty( hObject: ADSHANDLE;
                                             pucRIName: PAceChar;
                                             usPropertyID: UNSIGNED16;
                                             pucProperty: PAceChar;
                                             pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetPermissions( hDBConn: ADSHANDLE;
                                    pucGrantee: PAceChar;
                                    usObjectType: UNSIGNED16;
                                    pucObjectName: PAceChar;
                                    pucParentName: PAceChar;
                                    usGetInherited: UNSIGNED16;
                                    pulPermissions: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGrantPermission( hAdminConn: ADSHANDLE;
                                     usObjectType: UNSIGNED16;
                                     pucObjectName: PAceChar;
                                     pucParentName: PAceChar;
                                     pucGrantee: PAceChar;
                                     ulPermissions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRevokePermission( hAdminConn: ADSHANDLE;
                                      usObjectType: UNSIGNED16;
                                      pucObjectName: PAceChar;
                                      pucParentName: PAceChar;
                                      pucGrantee: PAceChar;
                                      ulPermissions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetDatabaseProperty( hDictionary: ADSHANDLE;
                                         usPropertyID: UNSIGNED16;
                                         pvProperty: pointer;
                                         usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetFieldProperty( hDictionary: ADSHANDLE;
                                      pucTableName: PAceChar;
                                      pucFieldName: PAceChar;
                                      usPropertyID: UNSIGNED16;
                                      pvProperty: pointer;
                                      usPropertyLen: UNSIGNED16;
                                      usValidateOption: UNSIGNED16;
                                      pucFailTable: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetProcedureProperty( hDictionary: ADSHANDLE;
                                          pucProcedureName: PAceChar;
                                          usPropertyID: UNSIGNED16;
                                          pvProperty: pointer;
                                          usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetTableProperty( hDictionary: ADSHANDLE;
                                      pucTableName: PAceChar;
                                      usPropertyID: UNSIGNED16;
                                      pvProperty: pointer;
                                      usPropertyLen: UNSIGNED16;
                                      usValidateOption: UNSIGNED16;
                                      pucFailTable: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetUserGroupProperty( hDictionary: ADSHANDLE;
                                          pucUserGroupName: PAceChar;
                                          usPropertyID: UNSIGNED16;
                                          pvProperty: pointer;
                                          usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetUserProperty( hDictionary: ADSHANDLE;
                                     pucUserName: PAceChar;
                                     usPropertyID: UNSIGNED16;
                                     pvProperty: pointer;
                                     usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetViewProperty( hDictionary: ADSHANDLE;
                                     pucViewName: PAceChar;
                                     usPropertyID: UNSIGNED16;
                                     pvProperty: pointer;
                                     usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetObjectAccessRights( hDictionary: ADSHANDLE;
                                           pucObjectName: PAceChar;
                                           pucAccessorName: PAceChar;
                                           pucAllowedAccess: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddProcedure( hDictionary: ADSHANDLE;
                                  pucName: PAceChar;
                                  pucContainer: PAceChar;
                                  pucProcName: PAceChar;
                                  ulInvokeOption: UNSIGNED32;
                                  pucInParams: PAceChar;
                                  pucOutParams: PAceChar;
                                  pucComments: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddProcedure100( hDictionary: ADSHANDLE;
                                     pucName: PAceChar;
                                     pwcContainer: PWideChar;
                                     pucProcName: PAceChar;
                                     ulInvokeOption: UNSIGNED32;
                                     pucInParams: PAceChar;
                                     pucOutParams: PAceChar;
                                     pucComments: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddTable( hDictionary: ADSHANDLE;
                              pucTableName: PAceChar;
                              pucTablePath: PAceChar;
                              usTableType: UNSIGNED16;
                              usCharType: UNSIGNED16;
                              pucIndexFiles: PAceChar;
                              pucComments: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddTable90( hDictionary: ADSHANDLE;
                                pucTableName: PAceChar;
                                pucTablePath: PAceChar;
                                usTableType: UNSIGNED16;
                                usCharType: UNSIGNED16;
                                pucIndexFiles: PAceChar;
                                pucComments: PAceChar;
                                pucCollation: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddView( hDictionary: ADSHANDLE;
                             pucName: PAceChar;
                             pucComments: PAceChar;
                             pucSQL: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddView100( hDictionary: ADSHANDLE;
                                pucName: PAceChar;
                                pucComments: PAceChar;
                                pwcSQL: PWideChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateTrigger( hDictionary: ADSHANDLE;
                                   pucName: PAceChar;
                                   pucTableName: PAceChar;
                                   ulTriggerType: UNSIGNED32;
                                   ulEventTypes: UNSIGNED32;
                                   ulContainerType: UNSIGNED32;
                                   pucContainer: PAceChar;
                                   pucFunctionName: PAceChar;
                                   ulPriority: UNSIGNED32;
                                   pucComments: PAceChar;
                                   ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateTrigger100( hDictionary: ADSHANDLE;
                                      pucName: PAceChar;
                                      pucTableName: PAceChar;
                                      ulTriggerType: UNSIGNED32;
                                      ulEventTypes: UNSIGNED32;
                                      ulContainerType: UNSIGNED32;
                                      pwcContainer: PWideChar;
                                      pucFunctionName: PAceChar;
                                      ulPriority: UNSIGNED32;
                                      pucComments: PAceChar;
                                      ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRemoveTrigger( hDictionary: ADSHANDLE;
                                   pucName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddIndexFile( hDictionary: ADSHANDLE;
                                  pucTableName: PAceChar;
                                  pucIndexFilePath: PAceChar;
                                  pucComment: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateUser( hDictionary: ADSHANDLE;
                                pucGroupName: PAceChar;
                                pucUserName: PAceChar;
                                pucPassword: PAceChar;
                                pucDescription: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDAddUserToGroup( hDictionary: ADSHANDLE;
                                    pucGroupName: PAceChar;
                                    pucUserName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRemoveUserFromGroup( hDictionary: ADSHANDLE;
                                         pucGroupName: PAceChar;
                                         pucUserName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDeleteUser( hDictionary: ADSHANDLE;
                                pucUserName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateUserGroup( hDictionary: ADSHANDLE;
                                     pucGroupName: PAceChar;
                                     pucDescription: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDeleteUserGroup( hDictionary: ADSHANDLE;
                                     pucGroupName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDeleteIndex( hDictionary: ADSHANDLE;
                                 pucTableName: PAceChar;
                                 pucIndexName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRemoveIndexFile( hDictionary: ADSHANDLE;
                                     pucTableName: PAceChar;
                                     pucIndexFileName: PAceChar;
                                     usDeleteFile: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRemoveProcedure( hDictionary: ADSHANDLE;
                                     pucName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRemoveTable( hObject: ADSHANDLE;
                                 pucTableName: PAceChar;
                                 usDeleteFiles: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRemoveView( hDictionary: ADSHANDLE;
                                pucName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDRenameObject( hDictionary: ADSHANDLE;
                                  pucObjectName: PAceChar;
                                  pucNewObjectName: PAceChar;
                                  usObjectType: UNSIGNED16;
                                  ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDMoveObjectFile( hDictionary: ADSHANDLE;
                                    usObjectType: UNSIGNED16;
                                    pucObjectName: PAceChar;
                                    pucNewPath: PAceChar;
                                    pucIndexFiles: PAceChar;
                                    pucParent: PAceChar;
                                    ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDFindFirstObject( hObject: ADSHANDLE;
                                     usFindObjectType: UNSIGNED16;
                                     pucParentName: PAceChar;
                                     pucObjectName: PAceChar;
                                     pusObjectNameLen: PUNSIGNED16;
                                     phFindHandle: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDFindNextObject( hObject: ADSHANDLE;
                                    hFindHandle: ADSHANDLE;
                                    pucObjectName: PAceChar;
                                    pusObjectNameLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDFindClose( hObject: ADSHANDLE;
                               hFindHandle: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateLink( hDBConn: ADSHANDLE;
                                pucLinkAlias: PAceChar;
                                pucLinkedDDPath: PAceChar;
                                pucUserName: PAceChar;
                                pucPassword: PAceChar;
                                ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDModifyLink( hDBConn: ADSHANDLE;
                                pucLinkAlias: PAceChar;
                                pucLinkedDDPath: PAceChar;
                                pucUserName: PAceChar;
                                pucPassword: PAceChar;
                                ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDropLink( hDBConn: ADSHANDLE;
                              pucLinkedDD: PAceChar;
                              usDropGlobal: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreatePublication( hDictionary: ADSHANDLE;
                                       pucPublicationName: PAceChar;
                                       pucComments: PAceChar;
                                       ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetPublicationProperty( hObject: ADSHANDLE;
                                            pucPublicationName: PAceChar;
                                            usPropertyID: UNSIGNED16;
                                            pvProperty: pointer;
                                            pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetPublicationProperty( hDictionary: ADSHANDLE;
                                            pucPublicationName: PAceChar;
                                            usPropertyID: UNSIGNED16;
                                            pvProperty: pointer;
                                            usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDeletePublication( hDictionary: ADSHANDLE;
                                       pucPublicationName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateArticle( hDictionary: ADSHANDLE;
                                   pucPublicationName: PAceChar;
                                   pucObjectName: PAceChar;
                                   pucRowIdentColumns: PAceChar;
                                   pucFilter: PAceChar;
                                   ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateArticle100( hDictionary: ADSHANDLE;
                                      pucPublicationName: PAceChar;
                                      pucObjectName: PAceChar;
                                      pucRowIdentColumns: PAceChar;
                                      pwcFilter: PWideChar;
                                      ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetArticleProperty( hObject: ADSHANDLE;
                                        pucPublicationName: PAceChar;
                                        pucObjectName: PAceChar;
                                        usPropertyID: UNSIGNED16;
                                        pvProperty: pointer;
                                        pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetArticleProperty( hDictionary: ADSHANDLE;
                                        pucPublicationName: PAceChar;
                                        pucObjectName: PAceChar;
                                        usPropertyID: UNSIGNED16;
                                        pvProperty: pointer;
                                        usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDeleteArticle( hDictionary: ADSHANDLE;
                                   pucPublicationName: PAceChar;
                                   pucObjectName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDCreateSubscription( hDictionary: ADSHANDLE;
                                        pucSubscriptionName: PAceChar;
                                        pucPublicationName: PAceChar;
                                        pucTarget: PAceChar;
                                        pucUser: PAceChar;
                                        pucPassword: PAceChar;
                                        pucReplicationQueue: PAceChar;
                                        usForward: UNSIGNED16;
                                        pucComments: PAceChar;
                                        ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDGetSubscriptionProperty( hObject: ADSHANDLE;
                                             pucSubscriptionName: PAceChar;
                                             usPropertyID: UNSIGNED16;
                                             pvProperty: pointer;
                                             pusPropertyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetSubscriptionProperty( hDictionary: ADSHANDLE;
                                             pucSubscriptionName: PAceChar;
                                             usPropertyID: UNSIGNED16;
                                             pvProperty: pointer;
                                             usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDeleteSubscription( hDictionary: ADSHANDLE;
                                        pucSubscriptionName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDecryptRecord( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDecryptTable( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDeleteCustomKey( hIndex: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDeleteIndex( hIndex: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDeleteRecord( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetKeyColumn( hCursor: ADSHANDLE;
                                pucKeyColumn: PAceChar;
                                pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetKeyFilter( hTable: ADSHANDLE;
                                pucValuesTable: PAceChar;
                                ulOptions: UNSIGNED32;
                                pucFilter: PAceChar;
                                pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDisableEncryption( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDisableLocalConnections:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDisconnect( hConnect: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEnableEncryption( hTable: ADSHANDLE;
                                    pucPassword: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEncryptRecord( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEncryptTable( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEvalLogicalExpr( hTable: ADSHANDLE;
                                   pucExpr: PAceChar;
                                   pbResult: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEvalLogicalExprW( hTable: ADSHANDLE;
                                    pwcExpr: PWideChar;
                                    pbResult: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEvalNumericExpr( hTable: ADSHANDLE;
                                   pucExpr: PAceChar;
                                   pdResult: pDOUBLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEvalStringExpr( hTable: ADSHANDLE;
                                  pucExpr: PAceChar;
                                  pucResult: PAceChar;
                                  pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEvalTestExpr( hTable: ADSHANDLE;
                                pucExpr: PAceChar;
                                pusType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsExtractKey( hIndex: ADSHANDLE;
                              pucKey: PAceChar;
                              pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFailedTransactionRecovery( pucServer: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFileToBinary( hTable: ADSHANDLE;
                                pucFldName: PAceChar;
                                usBinaryType: UNSIGNED16;
                                pucFileName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindConnection( pucServerName: PAceChar;
                                  phConnect: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindConnection25( pucFullPath: PAceChar;
                                    phConnect: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindClose( hConnect: ADSHANDLE;
                             lHandle: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindFirstTable( hConnect: ADSHANDLE;
                                  pucFileMask: PAceChar;
                                  pucFirstFile: PAceChar;
                                  pusFileLen: PUNSIGNED16;
                                  plHandle: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindNextTable( hConnect: ADSHANDLE;
                                 lHandle: ADSHANDLE;
                                 pucFileName: PAceChar;
                                 pusFileLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindFirstTable62( hConnect: ADSHANDLE;
                                    pucFileMask: PAceChar;
                                    pucFirstDD: PAceChar;
                                    pusDDLen: PUNSIGNED16;
                                    pucFirstFile: PAceChar;
                                    pusFileLen: PUNSIGNED16;
                                    plHandle: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindNextTable62( hConnect: ADSHANDLE;
                                   lHandle: ADSHANDLE;
                                   pucDDName: PAceChar;
                                   pusDDLen: PUNSIGNED16;
                                   pucFileName: PAceChar;
                                   pusFileLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetAllIndexes( hTable: ADSHANDLE;
                                 ahIndex: PADSIndexArray;
                                 pusArrayLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFTSIndexes( hTable: ADSHANDLE;
                                 ahIndex: PADSIndexArray;
                                 pusArrayLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFTSIndexInfo( hIndex: ADSHANDLE;
                                   pucOutput: PAceChar;
                                   pulBufLen: PUNSIGNED32;
                                   ppucField: pPChar;
                                   pulMinWordLen: PUNSIGNED32;
                                   pulMaxWordLen: PUNSIGNED32;
                                   ppucDelimiters: pPChar;
                                   ppucNoiseWords: pPChar;
                                   ppucDropChars: pPChar;
                                   ppucConditionalChars: pPChar;
                                   ppucReserved1: pPChar;
                                   ppucReserved2: pPChar;
                                   pulOptions: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFTSIndexInfoW( hIndex: ADSHANDLE;
                                    pwcOutput: PWideChar;
                                    pulBufLen: PUNSIGNED32;
                                    ppwcField: pPWideChar;
                                    pulMinWordLen: PUNSIGNED32;
                                    pulMaxWordLen: PUNSIGNED32;
                                    ppwcDelimiters: pPWideChar;
                                    ppwcNoiseWords: pPWideChar;
                                    ppwcDropChars: pPWideChar;
                                    ppwcConditionalChars: pPWideChar;
                                    ppwcReserved1: pPWideChar;
                                    ppwcReserved2: pPWideChar;
                                    pulOptions: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetAllLocks( hTable: ADSHANDLE;
                               aulLocks: pointer;
                               pusArrayLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetAllTables( ahTable: pointer;
                                pusArrayLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetBinary( hTable: ADSHANDLE;
                             pucFldName: PAceChar;
                             ulOffset: UNSIGNED32;
                             pucBuf: PAceBinary;
                             pulLen: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetBinaryLength( hTable: ADSHANDLE;
                                   pucFldName: PAceChar;
                                   pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetBookmark( hTable: ADSHANDLE;
                               phBookmark: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetBookmark60( hObj: ADSHANDLE;
                                 pucBookmark: PAceChar;
                                 pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetBookmarkLength( hObj: ADSHANDLE;
                                     pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCompareBookmarks( pucBookmark1: PAceChar;
                                    pucBookmark2: PAceChar;
                                    plResult: pSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetCollationLang( pucLang: PAceChar;
                                    pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetCollation( hConnect: ADSHANDLE;
                                pucCollation: PAceChar;
                                pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIntProperty( hObj: ADSHANDLE;
                                  ulPropertyID: UNSIGNED32;
                                  pulProperty: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetConnectionType( hConnect: ADSHANDLE;
                                     pusConnectType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTransactionCount( hConnect: ADSHANDLE;
                                       pulTransactionCount: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetConnectionPath( hConnect: ADSHANDLE;
                                     pucConnectionPath: PAceChar;
                                     pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetConnectionProperty( hConnect: ADSHANDLE;
                                         usPropertyID: UNSIGNED16;
                                         pvProperty: pointer;
                                         pulPropertyLen: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDate( hTable: ADSHANDLE;
                           pucFldName: PAceChar;
                           pucBuf: PAceChar;
                           pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDateFormat( pucFormat: PAceChar;
                                 pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDateFormat60( hConnect: ADSHANDLE;
                                   pucFormat: PAceChar;
                                   pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDecimals( pusDecimals: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDefault( pucDefault: PAceChar;
                              pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDeleted( pbUseDeleted: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDouble( hTable: ADSHANDLE;
                             pucFldName: PAceChar;
                             pdValue: pDOUBLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetEpoch( pusCentury: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetErrorString( ulErrCode: UNSIGNED32;
                                  pucBuf: PAceChar;
                                  pusBufLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetExact( pbExact: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetExact22( hObj: ADSHANDLE;
                              pbExact: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetField( hTable: ADSHANDLE;
                            pucFldName: PAceChar;
                            pucBuf: PAceChar;
                            pulLen: PUNSIGNED32;
                            usOption: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldW( hObj: ADSHANDLE;
                             pucFldName: PAceChar;
                             pwcBuf: PWideChar;
                             pulLen: PUNSIGNED32;
                             usOption: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldDecimals( hTable: ADSHANDLE;
                                    pucFldName: PAceChar;
                                    pusDecimals: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldLength( hTable: ADSHANDLE;
                                  pucFldName: PAceChar;
                                  pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldName( hTable: ADSHANDLE;
                                usFld: UNSIGNED16;
                                pucName: PAceChar;
                                pusBufLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldNum( hTable: ADSHANDLE;
                               pucFldName: PAceChar;
                               pusNum: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldOffset( hTable: ADSHANDLE;
                                  pucFldName: PAceChar;
                                  pulOffset: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldType( hTable: ADSHANDLE;
                                pucFldName: PAceChar;
                                pusType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFilter( hTable: ADSHANDLE;
                             pucFilter: PAceChar;
                             pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetHandleLong( hObj: ADSHANDLE;
                                 pulVal: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetHandleType( hObj: ADSHANDLE;
                                 pusType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexCondition( hIndex: ADSHANDLE;
                                     pucExpr: PAceChar;
                                     pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexExpr( hIndex: ADSHANDLE;
                                pucExpr: PAceChar;
                                pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexFilename( hIndex: ADSHANDLE;
                                    usOption: UNSIGNED16;
                                    pucName: PAceChar;
                                    pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexHandle( hTable: ADSHANDLE;
                                  pucIndexOrder: PAceChar;
                                  phIndex: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexHandleByOrder( hTable: ADSHANDLE;
                                         usOrderNum: UNSIGNED16;
                                         phIndex: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexHandleByExpr( hTable: ADSHANDLE;
                                        pucExpr: PAceChar;
                                        ulDescending: UNSIGNED32;
                                        phIndex: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexName( hIndex: ADSHANDLE;
                                pucName: PAceChar;
                                pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexOrderByHandle( hIndex: ADSHANDLE;
                                         pusIndexOrder: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetJulian( hTable: ADSHANDLE;
                             pucFldName: PAceChar;
                             plDate: pSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetKeyCount( hIndex: ADSHANDLE;
                               usFilterOption: UNSIGNED16;
                               pulCount: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetKeyNum( hIndex: ADSHANDLE;
                             usFilterOption: UNSIGNED16;
                             pulKey: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetKeyLength( hIndex: ADSHANDLE;
                                pusKeyLength: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetKeyType( hIndex: ADSHANDLE;
                              usKeyType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetLastError( pulErrCode: PUNSIGNED32;
                                pucBuf: PAceChar;
                                pusBufLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetLastTableUpdate( hTable: ADSHANDLE;
                                      pucDate: PAceChar;
                                      pusDateLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetLogical( hTable: ADSHANDLE;
                              pucFldName: PAceChar;
                              pbValue: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetLong( hTable: ADSHANDLE;
                           pucFldName: PAceChar;
                           plValue: pSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsGetLongLong( hTable: ADSHANDLE;
                               pucFldName: PAceChar;
                               pqValue: pSIGNED64 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
{$ENDIF}

      function AdsGetMemoBlockSize( hTable: ADSHANDLE;
                                    pusBlockSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetMemoLength( hTable: ADSHANDLE;
                                 pucFldName: PAceChar;
                                 pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetMemoDataType( hTable: ADSHANDLE;
                                   pucFldName: PAceChar;
                                   pusType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetMilliseconds( hTable: ADSHANDLE;
                                   pucFldName: PAceChar;
                                   plTime: pSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsGetMoney( hTbl: ADSHANDLE;
                            pucFldName: PAceChar;
                            pqValue: pSIGNED64 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
{$ENDIF}

      function AdsGetActiveLinkInfo( hDBConn: ADSHANDLE;
                                     usLinkNum: UNSIGNED16;
                                     pucLinkInfo: PAceChar;
                                     pusBufferLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetNumActiveLinks( hDBConn: ADSHANDLE;
                                     pusNumLinks: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetNumFields( hTable: ADSHANDLE;
                                pusCount: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetNumIndexes( hTable: ADSHANDLE;
                                 pusNum: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetNumFTSIndexes( hTable: ADSHANDLE;
                                    pusNum: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetNumLocks( hTable: ADSHANDLE;
                               pusNum: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetNumOpenTables( pusNum: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetRecord( hTable: ADSHANDLE;
                             pucRec: PAceChar;
                             pulLen: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetRecordCount( hTable: ADSHANDLE;
                                  usFilterOption: UNSIGNED16;
                                  pulCount: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetRecordNum( hTable: ADSHANDLE;
                                usFilterOption: UNSIGNED16;
                                pulRec: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetRecordLength( hTable: ADSHANDLE;
                                   pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetRecordCRC( hTable: ADSHANDLE;
                                pulCRC: PUNSIGNED32;
                                ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetRelKeyPos( hIndex: ADSHANDLE;
                                pdPos: pDOUBLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetScope( hIndex: ADSHANDLE;
                            usScopeOption: UNSIGNED16;
                            pucScope: PAceChar;
                            pusBufLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetSearchPath( pucPath: PAceChar;
                                 pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetServerName( hConnect: ADSHANDLE;
                                 pucName: PAceChar;
                                 pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetServerTime( hConnect: ADSHANDLE;
                                 pucDateBuf: PAceChar;
                                 pusDateBufLen: PUNSIGNED16;
                                 plTime: pSIGNED32;
                                 pucTimeBuf: PAceChar;
                                 pusTimeBufLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetShort( hTable: ADSHANDLE;
                            pucFldName: PAceChar;
                            psValue: pSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetString( hTable: ADSHANDLE;
                             pucFldName: PAceChar;
                             pucBuf: PAceChar;
                             pulLen: PUNSIGNED32;
                             usOption: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetStringW( hObj: ADSHANDLE;
                              pucFldName: PAceChar;
                              pwcBuf: PWideChar;
                              pulLen: PUNSIGNED32;
                              usOption: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableAlias( hTable: ADSHANDLE;
                                 pucAlias: PAceChar;
                                 pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableCharType( hTable: ADSHANDLE;
                                    pusCharType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableConnection( hTable: ADSHANDLE;
                                      phConnect: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableFilename( hTable: ADSHANDLE;
                                    usOption: UNSIGNED16;
                                    pucName: PAceChar;
                                    pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableHandle( pucName: PAceChar;
                                  phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableHandle25( hConnect: ADSHANDLE;
                                    pucName: PAceChar;
                                    phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableLockType( hTable: ADSHANDLE;
                                    pusLockType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableMemoSize( hTable: ADSHANDLE;
                                    pusMemoSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableOpenOptions( hTable: ADSHANDLE;
                                       pulOptions: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableRights( hTable: ADSHANDLE;
                                  pusRights: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableType( hTable: ADSHANDLE;
                                pusType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTime( hTable: ADSHANDLE;
                           pucFldName: PAceChar;
                           pucBuf: PAceChar;
                           pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetVersion( pulMajor: PUNSIGNED32;
                              pulMinor: PUNSIGNED32;
                              pucLetter: PAceChar;
                              pucDesc: PAceChar;
                              pusDescLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGotoBookmark( hTable: ADSHANDLE;
                                hBookmark: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGotoBookmark60( hObj: ADSHANDLE;
                                  pucBookmark: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGotoBottom( hObj: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGotoRecord( hTable: ADSHANDLE;
                              ulRec: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGotoTop( hObj: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsImageToClipboard( hTable: ADSHANDLE;
                                    pucFldName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsInTransaction( hConnect: ADSHANDLE;
                                 pbInTrans: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsEmpty( hTable: ADSHANDLE;
                           pucFldName: PAceChar;
                           pbEmpty: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsExprValid( hTable: ADSHANDLE;
                               pucExpr: PAceChar;
                               pbValid: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsFound( hObj: ADSHANDLE;
                           pbFound: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexCompound( hIndex: ADSHANDLE;
                                   pbCompound: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexCandidate( hIndex: ADSHANDLE;
                                    pbCandidate: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexNullable( hIndex: ADSHANDLE;
                                   pbNullable: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexCustom( hIndex: ADSHANDLE;
                                 pbCustom: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexDescending( hIndex: ADSHANDLE;
                                     pbDescending: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexPrimaryKey( hIndex: ADSHANDLE;
                                     pbPrimaryKey: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexFTS( hIndex: ADSHANDLE;
                              pbFTS: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexUnique( hIndex: ADSHANDLE;
                                 pbUnique: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsRecordDeleted( hTable: ADSHANDLE;
                                   pbDeleted: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsRecordEncrypted( hTable: ADSHANDLE;
                                     pbEncrypted: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsRecordLocked( hTable: ADSHANDLE;
                                  ulRec: UNSIGNED32;
                                  pbLocked: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsRecordVisible( hObj: ADSHANDLE;
                                   pbVisible: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsServerLoaded( pucServer: PAceChar;
                                  pbLoaded: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsTableEncrypted( hTable: ADSHANDLE;
                                    pbEncrypted: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsTableLocked( hTable: ADSHANDLE;
                                 pbLocked: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsLocate( hTable: ADSHANDLE;
                          pucExpr: PAceChar;
                          bForward: UNSIGNED16;
                          pbFound: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsLockRecord( hTable: ADSHANDLE;
                              ulRec: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsLockTable( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsLookupKey( hIndex: ADSHANDLE;
                             pucKey: PAceChar;
                             usKeyLen: UNSIGNED16;
                             usDataType: UNSIGNED16;
                             pbFound: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgConnect( pucServerName: PAceChar;
                             pucUserName: PAceChar;
                             pucPassword: PAceChar;
                             phMgmtHandle: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgDisconnect( hMgmtHandle: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetCommStats( hMgmtHandle: ADSHANDLE;
                                  pstCommStats: pADS_MGMT_COMM_STATS;
                                  pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgResetCommStats( hMgmtHandle: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgDumpInternalTables( hMgmtHandle: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetConfigInfo( hMgmtHandle: ADSHANDLE;
                                   pstConfigValues: pADS_MGMT_CONFIG_PARAMS;
                                   pusConfigValuesStructSize: PUNSIGNED16;
                                   pstConfigMemory: pADS_MGMT_CONFIG_MEMORY;
                                   pusConfigMemoryStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetInstallInfo( hMgmtHandle: ADSHANDLE;
                                    pstInstallInfo: pADS_MGMT_INSTALL_INFO;
                                    pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetActivityInfo( hMgmtHandle: ADSHANDLE;
                                     pstActivityInfo: pADS_MGMT_ACTIVITY_INFO;
                                     pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetUserNames( hMgmtHandle: ADSHANDLE;
                                  pucFileName: PAceChar;
                                  astUserInfo: PADSMgUserArray;
                                  pusArrayLen: PUNSIGNED16;
                                  pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetOpenTables( hMgmtHandle: ADSHANDLE;
                                   pucUserName: PAceChar;
                                   usConnNumber: UNSIGNED16;
                                   astOpenTableInfo: PADSMgTableArray;
                                   pusArrayLen: PUNSIGNED16;
                                   pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetOpenIndexes( hMgmtHandle: ADSHANDLE;
                                    pucTableName: PAceChar;
                                    pucUserName: PAceChar;
                                    usConnNumber: UNSIGNED16;
                                    astOpenIndexInfo: PADSMgIndexArray;
                                    pusArrayLen: PUNSIGNED16;
                                    pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetLocks( hMgmtHandle: ADSHANDLE;
                              pucTableName: PAceChar;
                              pucUserName: PAceChar;
                              usConnNumber: UNSIGNED16;
                              astRecordInfo: PADSMgLocksArray;
                              pusArrayLen: PUNSIGNED16;
                              pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetServerType( hMgmtHandle: ADSHANDLE;
                                   pusServerType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgKillUser( hMgmtHandle: ADSHANDLE;
                              pucUserName: PAceChar;
                              usConnNumber: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetWorkerThreadActivity( hMgmtHandle: ADSHANDLE;
                                             astWorkerThreadActivity: PADSMgThreadsArray;
                                             pusArrayLen: PUNSIGNED16;
                                             pusStructSize: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgGetLockOwner( hMgmtHandle: ADSHANDLE;
                                  pucTableName: PAceChar;
                                  ulRecordNumber: UNSIGNED32;
                                  pstUserInfo: pADS_MGMT_USER_INFO;
                                  pusStructSize: PUNSIGNED16;
                                  pusLockType: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsNullTerminateStrings( bNullTerminate: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsOpenIndex( hTable: ADSHANDLE;
                             pucName: PAceChar;
                             ahIndex: PADSIndexArray;
                             pusArrayLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsOpenTable( hConnect: ADSHANDLE;
                             pucName: PAceChar;
                             pucAlias: PAceChar;
                             usTableType: UNSIGNED16;
                             usCharType: UNSIGNED16;
                             usLockType: UNSIGNED16;
                             usCheckRights: UNSIGNED16;
                             ulOptions: UNSIGNED32;
                             phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsOpenTable90( hConnect: ADSHANDLE;
                               pucName: PAceChar;
                               pucAlias: PAceChar;
                               usTableType: UNSIGNED16;
                               usCharType: UNSIGNED16;
                               usLockType: UNSIGNED16;
                               usCheckRights: UNSIGNED16;
                               ulOptions: UNSIGNED32;
                               pucCollation: PAceChar;
                               phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsOpenTable101( hConnect: ADSHANDLE;
                                pucName: PAceChar;
                                phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsPackTable( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsPackTable120( hTable: ADSHANDLE;
                                ulMemoBlockSize: UNSIGNED32;
                                ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRecallRecord( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRecallAllRecords( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRefreshRecord( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearProgressCallback:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRegisterProgressCallback( Callback: TProgressCallback ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRegisterCallbackFunction( Callback: TCallbackFunction;
                                            ulCallbackID: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsRegisterCallbackFunction101( Callback: TCallbackFunction101;
                                               qCallbackID: SIGNED64 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
{$ENDIF}

      function AdsClearCallbackFunction:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetSQLTimeout( hObj: ADSHANDLE;
                                 ulTimeout: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsReindex( hObject: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsReindex61( hObject: ADSHANDLE;
                             ulPageSize: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsReindexFTS( hObject: ADSHANDLE;
                              ulPageSize: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsResetConnection( hConnect: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRollbackTransaction( hConnect: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSeek( hIndex: ADSHANDLE;
                        pucKey: PAceChar;
                        usKeyLen: UNSIGNED16;
                        usDataType: UNSIGNED16;
                        usSeekType: UNSIGNED16;
                        pbFound: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSeekLast( hIndex: ADSHANDLE;
                            pucKey: PAceChar;
                            usKeyLen: UNSIGNED16;
                            usDataType: UNSIGNED16;
                            pbFound: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetBinary( hTable: ADSHANDLE;
                             pucFldName: PAceChar;
                             usBinaryType: UNSIGNED16;
                             ulTotalLength: UNSIGNED32;
                             ulOffset: UNSIGNED32;
                             pucBuf: PAceBinary;
                             ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetCollationLang( pucLang: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetCollation( hConnect: ADSHANDLE;
                                pucCollation: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetDate( hObj: ADSHANDLE;
                           pucFldName: PAceChar;
                           pucValue: PAceChar;
                           usLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetDateFormat( pucFormat: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetDateFormat60( hConnect: ADSHANDLE;
                                   pucFormat: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetDecimals( usDecimals: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetDefault( pucDefault: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsShowDeleted( bShowDeleted: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetDouble( hObj: ADSHANDLE;
                             pucFldName: PAceChar;
                             dValue: DOUBLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetEmpty( hObj: ADSHANDLE;
                            pucFldName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetEpoch( usCentury: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetExact( bExact: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetExact22( hObj: ADSHANDLE;
                              bExact: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetField( hObj: ADSHANDLE;
                            pucFldName: PAceChar;
                            pucBuf: PAceChar;
                            ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetFieldW( hObj: ADSHANDLE;
                             pucFldName: PAceChar;
                             pwcBuf: PWideChar;
                             ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetFilter( hTable: ADSHANDLE;
                             pucFilter: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetFilter100( hTable: ADSHANDLE;
                                pvFilter: pointer;
                                ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetHandleLong( hObj: ADSHANDLE;
                                 ulVal: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetJulian( hObj: ADSHANDLE;
                             pucFldName: PAceChar;
                             lDate: SIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetLogical( hObj: ADSHANDLE;
                              pucFldName: PAceChar;
                              bValue: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetLong( hObj: ADSHANDLE;
                           pucFldName: PAceChar;
                           lValue: SIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsSetLongLong( hObj: ADSHANDLE;
                               pucFldName: PAceChar;
                               qValue: SIGNED64 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
{$ENDIF}

      function AdsSetMilliseconds( hObj: ADSHANDLE;
                                   pucFldName: PAceChar;
                                   lTime: SIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsSetMoney( hObj: ADSHANDLE;
                            pucFldName: PAceChar;
                            qValue: SIGNED64 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
{$ENDIF}

      function AdsSetRecord( hObj: ADSHANDLE;
                             pucRec: PAceChar;
                             ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetRelation( hTableParent: ADSHANDLE;
                               hIndexChild: ADSHANDLE;
                               pucExpr: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetRelKeyPos( hIndex: ADSHANDLE;
                                dPos: DOUBLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetScope( hIndex: ADSHANDLE;
                            usScopeOption: UNSIGNED16;
                            pucScope: PAceChar;
                            usScopeLen: UNSIGNED16;
                            usDataType: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetScopedRelation( hTableParent: ADSHANDLE;
                                     hIndexChild: ADSHANDLE;
                                     pucExpr: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetSearchPath( pucPath: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetServerType( usServerOptions: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetShort( hObj: ADSHANDLE;
                            pucFldName: PAceChar;
                            sValue: SIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetString( hObj: ADSHANDLE;
                             pucFldName: PAceChar;
                             pucBuf: PAceChar;
                             ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetStringW( hObj: ADSHANDLE;
                              pucFldName: PAceChar;
                              pwcBuf: PWideChar;
                              ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetStringFromCodePage( hObj: ADSHANDLE;
                                         ulCodePage: UNSIGNED32;
                                         pucFldName: PAceChar;
                                         pucBuf: PAceChar;
                                         ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetTime( hObj: ADSHANDLE;
                           pucFldName: PAceChar;
                           pucValue: PAceChar;
                           usLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsShowError( pucTitle: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSkip( hObj: ADSHANDLE;
                        lRecs: SIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSkipUnique( hIndex: ADSHANDLE;
                              lRecs: SIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsThreadExit:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsUnlockRecord( hTable: ADSHANDLE;
                                ulRec: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsUnlockTable( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      {* AdsVerifyPassword is obsolete; retained for backward compatibility.
      * Use AdsIsEncryptionEnabled instead.
      *}
      function AdsVerifyPassword( hTable: ADSHANDLE;
                                  pusEnabled: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsEncryptionEnabled( hTable: ADSHANDLE;
                                       pusEnabled: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsWriteAllRecords:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsWriteRecord( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsZapTable( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetAOF( hTable: ADSHANDLE;
                          pucFilter: PAceChar;
                          usOptions: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetAOF100( hTable: ADSHANDLE;
                             pvFilter: pointer;
                             ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEvalAOF( hTable: ADSHANDLE;
                           pucFilter: PAceChar;
                           pusOptLevel: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEvalAOF100( hTable: ADSHANDLE;
                              pvFilter: pointer;
                              ulOptions: UNSIGNED32;
                              pusOptLevel: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearAOF( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRefreshAOF( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetAOF( hTable: ADSHANDLE;
                          pucFilter: PAceChar;
                          pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetAOF100( hTable: ADSHANDLE;
                             ulOptions: UNSIGNED32;
                             pvFilter: pointer;
                             pulLen: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetAOFOptLevel( hTable: ADSHANDLE;
                                  pusOptLevel: PUNSIGNED16;
                                  pucNonOpt: PAceChar;
                                  pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetAOFOptLevel100( hTable: ADSHANDLE;
                                     pusOptLevel: PUNSIGNED16;
                                     pvNonOpt: pointer;
                                     pulExprLen: PUNSIGNED32;
                                     ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsRecordInAOF( hTable: ADSHANDLE;
                                 ulRecordNum: UNSIGNED32;
                                 pusIsInAOF: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCustomizeAOF( hTable: ADSHANDLE;
                                ulNumRecords: UNSIGNED32;
                                pulRecords: PUNSIGNED32;
                                usOption: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsInitRawKey( hIndex: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsBuildRawKey( hIndex: ADSHANDLE;
                               pucKey: PAceChar;
                               pusKeyLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsBuildRawKey100( hIndex: ADSHANDLE;
                                  pucKey: PAceChar;
                                  pusKeyLen: PUNSIGNED16;
                                  ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateSQLStatement( hConnect: ADSHANDLE;
                                      phStatement: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsPrepareSQL( hStatement: ADSHANDLE;
                              pucSQL: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsPrepareSQLW( hStatement: ADSHANDLE;
                               pwcSQL: PWideChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCachePrepareSQL( hConnect: ADSHANDLE;
                                   pucSQL: PAceChar;
                                   phStatement: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCachePrepareSQLW( hConnect: ADSHANDLE;
                                    pwcSQL: PWideChar;
                                    phStatement: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsExecuteSQL( hStatement: ADSHANDLE;
                              phCursor: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsExecuteSQLDirect( hStatement: ADSHANDLE;
                                    pucSQL: PAceChar;
                                    phCursor: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsExecuteSQLDirectW( hStatement: ADSHANDLE;
                                     pwcSQL: PWideChar;
                                     phCursor: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCloseSQLStatement( hStatement: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtSetTableRights( hStatement: ADSHANDLE;
                                      usCheckRights: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtSetTableReadOnly( hStatement: ADSHANDLE;
                                        usReadOnly: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtSetTableLockType( hStatement: ADSHANDLE;
                                        usLockType: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtSetTableCharType( hStatement: ADSHANDLE;
                                        usCharType: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtSetTableType( hStatement: ADSHANDLE;
                                    usTableType: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtSetTableCollation( hStatement: ADSHANDLE;
                                         pucCollation: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtConstrainUpdates( hStatement: ADSHANDLE;
                                        usConstrain: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtEnableEncryption( hStatement: ADSHANDLE;
                                        pucPassword: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtDisableEncryption( hStatement: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtSetTablePassword( hStatement: ADSHANDLE;
                                        pucTableName: PAceChar;
                                        pucPassword: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtClearTablePasswords( hStatement: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsStmtReadAllColumns( hStatement: ADSHANDLE;
                                      usReadColumns: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearSQLParams( hStatement: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetTimeStamp( hObj: ADSHANDLE;
                                pucFldName: PAceChar;
                                pucBuf: PAceChar;
                                ulLen: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsClearSQLAbortFunc:UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRegisterSQLAbortFunc( Callback: TSQLAbortFunc ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRegisterUDF( hObj: ADSHANDLE;
                               usType: UNSIGNED16;
                               Callback: TUDFFunc ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetNumParams( hStatement: ADSHANDLE;
                                pusNumParams: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetLastAutoinc( hObj: ADSHANDLE;
                                  pulAutoIncVal: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsIndexUserDefined( hIndex: ADSHANDLE;
                                      pbUserDefined: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRestructureTable( hObj: ADSHANDLE;
                                    pucName: PAceChar;
                                    pucPassword: PAceChar;
                                    usTableType: UNSIGNED16;
                                    usCharType: UNSIGNED16;
                                    usLockType: UNSIGNED16;
                                    usCheckRights: UNSIGNED16;
                                    pucAddFields: PAceChar;
                                    pucDeleteFields: PAceChar;
                                    pucChangeFields: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRestructureTable90( hObj: ADSHANDLE;
                                      pucName: PAceChar;
                                      pucPassword: PAceChar;
                                      usTableType: UNSIGNED16;
                                      usCharType: UNSIGNED16;
                                      usLockType: UNSIGNED16;
                                      usCheckRights: UNSIGNED16;
                                      pucAddFields: PAceChar;
                                      pucDeleteFields: PAceChar;
                                      pucChangeFields: PAceChar;
                                      pucCollation: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRestructureTable120( hObj: ADSHANDLE;
                                       pucName: PAceChar;
                                       pucPassword: PAceChar;
                                       usTableType: UNSIGNED16;
                                       usCharType: UNSIGNED16;
                                       usLockType: UNSIGNED16;
                                       usCheckRights: UNSIGNED16;
                                       pucAddFields: PAceChar;
                                       pucDeleteFields: PAceChar;
                                       pucChangeFields: PAceChar;
                                       pucCollation: PAceChar;
                                       ulMemoBlockSize: UNSIGNED32;
                                       ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetSQLStatementHandle( hCursor: ADSHANDLE;
                                         phStmt: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetSQLStatement( hStmt: ADSHANDLE;
                                   pucSQL: PAceChar;
                                   pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFlushFileBuffers( hTable: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDDeployDatabase( pucDestination: PAceChar;
                                    pucDestinationPassword: PAceChar;
                                    pucSource: PAceChar;
                                    pucSourcePassword: PAceChar;
                                    usServerTypes: UNSIGNED16;
                                    usValidateOption: UNSIGNED16;
                                    usBackupFiles: UNSIGNED16;
                                    ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsVerifySQL( hStatement: ADSHANDLE;
                             pucSQL: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsVerifySQLW( hStatement: ADSHANDLE;
                              pwcSQL: PWideChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDisableUniqueEnforcement( hConnection: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEnableUniqueEnforcement( hConnection: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDisableRI( hConnection: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEnableRI( hConnection: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDisableAutoIncEnforcement( hConnection: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsEnableAutoIncEnforcement( hConnection: ADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsRollbackTransaction80( hConnect: ADSHANDLE;
                                         pucSavepoint: PAceChar;
                                         ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsCreateSavepoint( hConnect: ADSHANDLE;
                                   pucSavepoint: PAceChar;
                                   ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDFreeTable( pucTableName: PAceChar;
                               pucPassword: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetIndexProperty( hAdminConn: ADSHANDLE;
                                      pucTableName: PAceChar;
                                      pucIndexName: PAceChar;
                                      usPropertyID: UNSIGNED16;
                                      pvProperty: pointer;
                                      usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsFieldBinary( hTable: ADSHANDLE;
                                 pucFldName: PAceChar;
                                 pbBinary: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsNull( hTable: ADSHANDLE;
                          pucFldName: PAceChar;
                          pbNull: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsNullable( hTable: ADSHANDLE;
                              pucFldName: PAceChar;
                              pbNullable: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetNull( hTable: ADSHANDLE;
                           pucFldName: PAceChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetTableCollation( hTbl: ADSHANDLE;
                                     pucCollation: PAceChar;
                                     pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetIndexCollation( hIndex: ADSHANDLE;
                                     pucCollation: PAceChar;
                                     pusLen: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetDataLength( hTable: ADSHANDLE;
                                 pucFldName: PAceChar;
                                 ulOptions: UNSIGNED32;
                                 pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetIndexDirection( hIndex: ADSHANDLE;
                                     usReverseDirection: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsMgKillUser90( hMgmtHandle: ADSHANDLE;
                                pucUserName: PAceChar;
                                usConnNumber: UNSIGNED16;
                                usPropertyID: UNSIGNED16;
                                pvProperty: pointer;
                                usPropertyLen: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsGetFieldLength100( hTable: ADSHANDLE;
                                     pucFldName: PAceChar;
                                     ulOptions: UNSIGNED32;
                                     pulLength: PUNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetRightsChecking( ulOptions: UNSIGNED32 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsSetTableTransactionFree( hTable: ADSHANDLE;
                                           usTransFree: UNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsIsTableTransactionFree( hTable: ADSHANDLE;
                                          pusTransFree: PUNSIGNED16 ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsFindServers( ulOptions: UNSIGNED32;
                               phTable: pADSHANDLE ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsBinaryToFileW( hTable: ADSHANDLE;
                                 pucFldName: PAceChar;
                                 pwcFileName: PWideChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
      function AdsFileToBinaryW( hTable: ADSHANDLE;
                                 pucFldName: PAceChar;
                                 usBinaryType: UNSIGNED16;
                                 pwcFileName: PWideChar ):UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}

      function AdsDDSetTriggerProperty( hDictionary : ADSHANDLE;
                                        pucTriggerName : PAceChar;
                                        usPropertyID : UNSIGNED16;
                                        pvProperty : pointer;
                                        usPropertyLen : UNSIGNED16 ) : UNSIGNED32; {$IFDEF WIN32}stdcall;{$ENDIF}{$IFDEF LINUX}cdecl;{$ENDIF}
implementation
{$IFNDEF FPC}
  {$IFDEF ADSDELPHI2010_OR_NEWER}
    {$WARN SYMBOL_PLATFORM OFF}
  {$ENDIF}
{$ENDIF}

      function AdsAddCustomKey; external ACE_LIB name 'AdsAddCustomKey' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsAppendRecord; external ACE_LIB name 'AdsAppendRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsApplicationExit; external ACE_LIB name 'AdsApplicationExit' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsAtBOF; external ACE_LIB name 'AdsAtBOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsAtEOF; external ACE_LIB name 'AdsAtEOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsBeginTransaction; external ACE_LIB name 'AdsBeginTransaction' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsBinaryToFile; external ACE_LIB name 'AdsBinaryToFile' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCacheOpenCursors; external ACE_LIB name 'AdsCacheOpenCursors' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCacheOpenTables; external ACE_LIB name 'AdsCacheOpenTables' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCacheRecords; external ACE_LIB name 'AdsCacheRecords' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCancelUpdate; external ACE_LIB name 'AdsCancelUpdate' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCancelUpdate90; external ACE_LIB name 'AdsCancelUpdate90' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCheckExistence; external ACE_LIB name 'AdsCheckExistence' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearAllScopes; external ACE_LIB name 'AdsClearAllScopes' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearDefault; external ACE_LIB name 'AdsClearDefault' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearFilter; external ACE_LIB name 'AdsClearFilter' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearRelation; external ACE_LIB name 'AdsClearRelation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearScope; external ACE_LIB name 'AdsClearScope' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCloneTable; external ACE_LIB name 'AdsCloneTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCloseAllIndexes; external ACE_LIB name 'AdsCloseAllIndexes' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCloseAllTables; external ACE_LIB name 'AdsCloseAllTables' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCloseIndex; external ACE_LIB name 'AdsCloseIndex' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCloseTable; external ACE_LIB name 'AdsCloseTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCloseCachedTables; external ACE_LIB name 'AdsCloseCachedTables' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCommitTransaction; external ACE_LIB name 'AdsCommitTransaction' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsConnect; external ACE_LIB name 'AdsConnect' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsConnect26; external ACE_LIB name 'AdsConnect26' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsConnect60; external ACE_LIB name 'AdsConnect60' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsConnect101; external ACE_LIB name 'AdsConnect101' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearCachePool; external ACE_LIB name 'AdsClearCachePool' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsReapUnusedConnections; external ACE_LIB name 'AdsReapUnusedConnections' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsConnectionAlive; external ACE_LIB name 'AdsIsConnectionAlive' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsContinue; external ACE_LIB name 'AdsContinue' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsConvertTable; external ACE_LIB name 'AdsConvertTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCopyTable; external ACE_LIB name 'AdsCopyTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCopyTableContents; external ACE_LIB name 'AdsCopyTableContents' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCopyTableStructure; external ACE_LIB name 'AdsCopyTableStructure' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateIndex; external ACE_LIB name 'AdsCreateIndex' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateIndex61; external ACE_LIB name 'AdsCreateIndex61' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateIndex90; external ACE_LIB name 'AdsCreateIndex90' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateFTSIndex; external ACE_LIB name 'AdsCreateFTSIndex' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateTable; external ACE_LIB name 'AdsCreateTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateTable71; external ACE_LIB name 'AdsCreateTable71' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateTable90; external ACE_LIB name 'AdsCreateTable90' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreate; external ACE_LIB name 'AdsDDCreate' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreate101; external ACE_LIB name 'AdsDDCreate101' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateRefIntegrity; external ACE_LIB name 'AdsDDCreateRefIntegrity' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateRefIntegrity62; external ACE_LIB name 'AdsDDCreateRefIntegrity62' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRemoveRefIntegrity; external ACE_LIB name 'AdsDDRemoveRefIntegrity' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetDatabaseProperty; external ACE_LIB name 'AdsDDGetDatabaseProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetFieldProperty; external ACE_LIB name 'AdsDDGetFieldProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetIndexFileProperty; external ACE_LIB name 'AdsDDGetIndexFileProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetIndexProperty; external ACE_LIB name 'AdsDDGetIndexProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetLinkProperty; external ACE_LIB name 'AdsDDGetLinkProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetTableProperty; external ACE_LIB name 'AdsDDGetTableProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetUserGroupProperty; external ACE_LIB name 'AdsDDGetUserGroupProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetUserProperty; external ACE_LIB name 'AdsDDGetUserProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetViewProperty; external ACE_LIB name 'AdsDDGetViewProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetTriggerProperty; external ACE_LIB name 'AdsDDGetTriggerProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetProcedureProperty; external ACE_LIB name 'AdsDDGetProcedureProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetRefIntegrityProperty; external ACE_LIB name 'AdsDDGetRefIntegrityProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetPermissions; external ACE_LIB name 'AdsDDGetPermissions' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGrantPermission; external ACE_LIB name 'AdsDDGrantPermission' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRevokePermission; external ACE_LIB name 'AdsDDRevokePermission' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetDatabaseProperty; external ACE_LIB name 'AdsDDSetDatabaseProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetFieldProperty; external ACE_LIB name 'AdsDDSetFieldProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetProcedureProperty; external ACE_LIB name 'AdsDDSetProcedureProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetTableProperty; external ACE_LIB name 'AdsDDSetTableProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetUserGroupProperty; external ACE_LIB name 'AdsDDSetUserGroupProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetUserProperty; external ACE_LIB name 'AdsDDSetUserProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetViewProperty; external ACE_LIB name 'AdsDDSetViewProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetObjectAccessRights; external ACE_LIB name 'AdsDDSetObjectAccessRights' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddProcedure; external ACE_LIB name 'AdsDDAddProcedure' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddProcedure100; external ACE_LIB name 'AdsDDAddProcedure100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddTable; external ACE_LIB name 'AdsDDAddTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddTable90; external ACE_LIB name 'AdsDDAddTable90' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddView; external ACE_LIB name 'AdsDDAddView' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddView100; external ACE_LIB name 'AdsDDAddView100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateTrigger; external ACE_LIB name 'AdsDDCreateTrigger' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateTrigger100; external ACE_LIB name 'AdsDDCreateTrigger100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRemoveTrigger; external ACE_LIB name 'AdsDDRemoveTrigger' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddIndexFile; external ACE_LIB name 'AdsDDAddIndexFile' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateUser; external ACE_LIB name 'AdsDDCreateUser' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDAddUserToGroup; external ACE_LIB name 'AdsDDAddUserToGroup' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRemoveUserFromGroup; external ACE_LIB name 'AdsDDRemoveUserFromGroup' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDeleteUser; external ACE_LIB name 'AdsDDDeleteUser' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateUserGroup; external ACE_LIB name 'AdsDDCreateUserGroup' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDeleteUserGroup; external ACE_LIB name 'AdsDDDeleteUserGroup' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDeleteIndex; external ACE_LIB name 'AdsDDDeleteIndex' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRemoveIndexFile; external ACE_LIB name 'AdsDDRemoveIndexFile' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRemoveProcedure; external ACE_LIB name 'AdsDDRemoveProcedure' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRemoveTable; external ACE_LIB name 'AdsDDRemoveTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRemoveView; external ACE_LIB name 'AdsDDRemoveView' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDRenameObject; external ACE_LIB name 'AdsDDRenameObject' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDMoveObjectFile; external ACE_LIB name 'AdsDDMoveObjectFile' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDFindFirstObject; external ACE_LIB name 'AdsDDFindFirstObject' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDFindNextObject; external ACE_LIB name 'AdsDDFindNextObject' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDFindClose; external ACE_LIB name 'AdsDDFindClose' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateLink; external ACE_LIB name 'AdsDDCreateLink' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDModifyLink; external ACE_LIB name 'AdsDDModifyLink' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDropLink; external ACE_LIB name 'AdsDDDropLink' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreatePublication; external ACE_LIB name 'AdsDDCreatePublication' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetPublicationProperty; external ACE_LIB name 'AdsDDGetPublicationProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetPublicationProperty; external ACE_LIB name 'AdsDDSetPublicationProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDeletePublication; external ACE_LIB name 'AdsDDDeletePublication' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateArticle; external ACE_LIB name 'AdsDDCreateArticle' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateArticle100; external ACE_LIB name 'AdsDDCreateArticle100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetArticleProperty; external ACE_LIB name 'AdsDDGetArticleProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetArticleProperty; external ACE_LIB name 'AdsDDSetArticleProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDeleteArticle; external ACE_LIB name 'AdsDDDeleteArticle' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDCreateSubscription; external ACE_LIB name 'AdsDDCreateSubscription' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDGetSubscriptionProperty; external ACE_LIB name 'AdsDDGetSubscriptionProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetSubscriptionProperty; external ACE_LIB name 'AdsDDSetSubscriptionProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDeleteSubscription; external ACE_LIB name 'AdsDDDeleteSubscription' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDecryptRecord; external ACE_LIB name 'AdsDecryptRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDecryptTable; external ACE_LIB name 'AdsDecryptTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDeleteCustomKey; external ACE_LIB name 'AdsDeleteCustomKey' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDeleteIndex; external ACE_LIB name 'AdsDeleteIndex' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDeleteRecord; external ACE_LIB name 'AdsDeleteRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetKeyColumn; external ACE_LIB name 'AdsGetKeyColumn' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetKeyFilter; external ACE_LIB name 'AdsGetKeyFilter' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDisableEncryption; external ACE_LIB name 'AdsDisableEncryption' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDisableLocalConnections; external ACE_LIB name 'AdsDisableLocalConnections' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDisconnect; external ACE_LIB name 'AdsDisconnect' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEnableEncryption; external ACE_LIB name 'AdsEnableEncryption' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEncryptRecord; external ACE_LIB name 'AdsEncryptRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEncryptTable; external ACE_LIB name 'AdsEncryptTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEvalLogicalExpr; external ACE_LIB name 'AdsEvalLogicalExpr' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEvalLogicalExprW; external ACE_LIB name 'AdsEvalLogicalExprW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEvalNumericExpr; external ACE_LIB name 'AdsEvalNumericExpr' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEvalStringExpr; external ACE_LIB name 'AdsEvalStringExpr' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEvalTestExpr; external ACE_LIB name 'AdsEvalTestExpr' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsExtractKey; external ACE_LIB name 'AdsExtractKey' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFailedTransactionRecovery; external ACE_LIB name 'AdsFailedTransactionRecovery' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFileToBinary; external ACE_LIB name 'AdsFileToBinary' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindConnection; external ACE_LIB name 'AdsFindConnection' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindConnection25; external ACE_LIB name 'AdsFindConnection25' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindClose; external ACE_LIB name 'AdsFindClose' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindFirstTable; external ACE_LIB name 'AdsFindFirstTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindNextTable; external ACE_LIB name 'AdsFindNextTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindFirstTable62; external ACE_LIB name 'AdsFindFirstTable62' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindNextTable62; external ACE_LIB name 'AdsFindNextTable62' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetAllIndexes; external ACE_LIB name 'AdsGetAllIndexes' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFTSIndexes; external ACE_LIB name 'AdsGetFTSIndexes' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFTSIndexInfo; external ACE_LIB name 'AdsGetFTSIndexInfo' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFTSIndexInfoW; external ACE_LIB name 'AdsGetFTSIndexInfoW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetAllLocks; external ACE_LIB name 'AdsGetAllLocks' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetAllTables; external ACE_LIB name 'AdsGetAllTables' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetBinary; external ACE_LIB name 'AdsGetBinary' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetBinaryLength; external ACE_LIB name 'AdsGetBinaryLength' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetBookmark; external ACE_LIB name 'AdsGetBookmark' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetBookmark60; external ACE_LIB name 'AdsGetBookmark60' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetBookmarkLength; external ACE_LIB name 'AdsGetBookmarkLength' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCompareBookmarks; external ACE_LIB name 'AdsCompareBookmarks' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetCollationLang; external ACE_LIB name 'AdsGetCollationLang' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetCollation; external ACE_LIB name 'AdsGetCollation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIntProperty; external ACE_LIB name 'AdsGetIntProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetConnectionType; external ACE_LIB name 'AdsGetConnectionType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTransactionCount; external ACE_LIB name 'AdsGetTransactionCount' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetConnectionPath; external ACE_LIB name 'AdsGetConnectionPath' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetConnectionProperty; external ACE_LIB name 'AdsGetConnectionProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDate; external ACE_LIB name 'AdsGetDate' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDateFormat; external ACE_LIB name 'AdsGetDateFormat' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDateFormat60; external ACE_LIB name 'AdsGetDateFormat60' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDecimals; external ACE_LIB name 'AdsGetDecimals' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDefault; external ACE_LIB name 'AdsGetDefault' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDeleted; external ACE_LIB name 'AdsGetDeleted' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDouble; external ACE_LIB name 'AdsGetDouble' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetEpoch; external ACE_LIB name 'AdsGetEpoch' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetErrorString; external ACE_LIB name 'AdsGetErrorString' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetExact; external ACE_LIB name 'AdsGetExact' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetExact22; external ACE_LIB name 'AdsGetExact22' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetField; external ACE_LIB name 'AdsGetField' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldW; external ACE_LIB name 'AdsGetFieldW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldDecimals; external ACE_LIB name 'AdsGetFieldDecimals' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldLength; external ACE_LIB name 'AdsGetFieldLength' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldName; external ACE_LIB name 'AdsGetFieldName' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldNum; external ACE_LIB name 'AdsGetFieldNum' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldOffset; external ACE_LIB name 'AdsGetFieldOffset' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldType; external ACE_LIB name 'AdsGetFieldType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFilter; external ACE_LIB name 'AdsGetFilter' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetHandleLong; external ACE_LIB name 'AdsGetHandleLong' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetHandleType; external ACE_LIB name 'AdsGetHandleType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexCondition; external ACE_LIB name 'AdsGetIndexCondition' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexExpr; external ACE_LIB name 'AdsGetIndexExpr' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexFilename; external ACE_LIB name 'AdsGetIndexFilename' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexHandle; external ACE_LIB name 'AdsGetIndexHandle' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexHandleByOrder; external ACE_LIB name 'AdsGetIndexHandleByOrder' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexHandleByExpr; external ACE_LIB name 'AdsGetIndexHandleByExpr' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexName; external ACE_LIB name 'AdsGetIndexName' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexOrderByHandle; external ACE_LIB name 'AdsGetIndexOrderByHandle' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetJulian; external ACE_LIB name 'AdsGetJulian' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetKeyCount; external ACE_LIB name 'AdsGetKeyCount' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetKeyNum; external ACE_LIB name 'AdsGetKeyNum' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetKeyLength; external ACE_LIB name 'AdsGetKeyLength' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetKeyType; external ACE_LIB name 'AdsGetKeyType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetLastError; external ACE_LIB name 'AdsGetLastError' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetLastTableUpdate; external ACE_LIB name 'AdsGetLastTableUpdate' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetLogical; external ACE_LIB name 'AdsGetLogical' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetLong; external ACE_LIB name 'AdsGetLong' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsGetLongLong; external ACE_LIB name 'AdsGetLongLong' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$ENDIF}
      function AdsGetMemoBlockSize; external ACE_LIB name 'AdsGetMemoBlockSize' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetMemoLength; external ACE_LIB name 'AdsGetMemoLength' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetMemoDataType; external ACE_LIB name 'AdsGetMemoDataType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetMilliseconds; external ACE_LIB name 'AdsGetMilliseconds' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsGetMoney; external ACE_LIB name 'AdsGetMoney' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$ENDIF}
      function AdsGetActiveLinkInfo; external ACE_LIB name 'AdsGetActiveLinkInfo' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetNumActiveLinks; external ACE_LIB name 'AdsGetNumActiveLinks' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetNumFields; external ACE_LIB name 'AdsGetNumFields' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetNumIndexes; external ACE_LIB name 'AdsGetNumIndexes' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetNumFTSIndexes; external ACE_LIB name 'AdsGetNumFTSIndexes' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetNumLocks; external ACE_LIB name 'AdsGetNumLocks' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetNumOpenTables; external ACE_LIB name 'AdsGetNumOpenTables' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetRecord; external ACE_LIB name 'AdsGetRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetRecordCount; external ACE_LIB name 'AdsGetRecordCount' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetRecordNum; external ACE_LIB name 'AdsGetRecordNum' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetRecordLength; external ACE_LIB name 'AdsGetRecordLength' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetRecordCRC; external ACE_LIB name 'AdsGetRecordCRC' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetRelKeyPos; external ACE_LIB name 'AdsGetRelKeyPos' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetScope; external ACE_LIB name 'AdsGetScope' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetSearchPath; external ACE_LIB name 'AdsGetSearchPath' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetServerName; external ACE_LIB name 'AdsGetServerName' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetServerTime; external ACE_LIB name 'AdsGetServerTime' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetShort; external ACE_LIB name 'AdsGetShort' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetString; external ACE_LIB name 'AdsGetString' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetStringW; external ACE_LIB name 'AdsGetStringW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableAlias; external ACE_LIB name 'AdsGetTableAlias' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableCharType; external ACE_LIB name 'AdsGetTableCharType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableConnection; external ACE_LIB name 'AdsGetTableConnection' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableFilename; external ACE_LIB name 'AdsGetTableFilename' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableHandle; external ACE_LIB name 'AdsGetTableHandle' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableHandle25; external ACE_LIB name 'AdsGetTableHandle25' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableLockType; external ACE_LIB name 'AdsGetTableLockType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableMemoSize; external ACE_LIB name 'AdsGetTableMemoSize' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableOpenOptions; external ACE_LIB name 'AdsGetTableOpenOptions' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableRights; external ACE_LIB name 'AdsGetTableRights' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableType; external ACE_LIB name 'AdsGetTableType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTime; external ACE_LIB name 'AdsGetTime' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetVersion; external ACE_LIB name 'AdsGetVersion' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGotoBookmark; external ACE_LIB name 'AdsGotoBookmark' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGotoBookmark60; external ACE_LIB name 'AdsGotoBookmark60' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGotoBottom; external ACE_LIB name 'AdsGotoBottom' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGotoRecord; external ACE_LIB name 'AdsGotoRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGotoTop; external ACE_LIB name 'AdsGotoTop' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsImageToClipboard; external ACE_LIB name 'AdsImageToClipboard' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsInTransaction; external ACE_LIB name 'AdsInTransaction' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsEmpty; external ACE_LIB name 'AdsIsEmpty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsExprValid; external ACE_LIB name 'AdsIsExprValid' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsFound; external ACE_LIB name 'AdsIsFound' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexCompound; external ACE_LIB name 'AdsIsIndexCompound' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexCandidate; external ACE_LIB name 'AdsIsIndexCandidate' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexNullable; external ACE_LIB name 'AdsIsIndexNullable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexCustom; external ACE_LIB name 'AdsIsIndexCustom' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexDescending; external ACE_LIB name 'AdsIsIndexDescending' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexPrimaryKey; external ACE_LIB name 'AdsIsIndexPrimaryKey' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexFTS; external ACE_LIB name 'AdsIsIndexFTS' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexUnique; external ACE_LIB name 'AdsIsIndexUnique' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsRecordDeleted; external ACE_LIB name 'AdsIsRecordDeleted' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsRecordEncrypted; external ACE_LIB name 'AdsIsRecordEncrypted' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsRecordLocked; external ACE_LIB name 'AdsIsRecordLocked' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsRecordVisible; external ACE_LIB name 'AdsIsRecordVisible' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsServerLoaded; external ACE_LIB name 'AdsIsServerLoaded' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsTableEncrypted; external ACE_LIB name 'AdsIsTableEncrypted' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsTableLocked; external ACE_LIB name 'AdsIsTableLocked' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsLocate; external ACE_LIB name 'AdsLocate' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsLockRecord; external ACE_LIB name 'AdsLockRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsLockTable; external ACE_LIB name 'AdsLockTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsLookupKey; external ACE_LIB name 'AdsLookupKey' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgConnect; external ACE_LIB name 'AdsMgConnect' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgDisconnect; external ACE_LIB name 'AdsMgDisconnect' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetCommStats; external ACE_LIB name 'AdsMgGetCommStats' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgResetCommStats; external ACE_LIB name 'AdsMgResetCommStats' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgDumpInternalTables; external ACE_LIB name 'AdsMgDumpInternalTables' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetConfigInfo; external ACE_LIB name 'AdsMgGetConfigInfo' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetInstallInfo; external ACE_LIB name 'AdsMgGetInstallInfo' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetActivityInfo; external ACE_LIB name 'AdsMgGetActivityInfo' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetUserNames; external ACE_LIB name 'AdsMgGetUserNames' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetOpenTables; external ACE_LIB name 'AdsMgGetOpenTables' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetOpenIndexes; external ACE_LIB name 'AdsMgGetOpenIndexes' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetLocks; external ACE_LIB name 'AdsMgGetLocks' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetServerType; external ACE_LIB name 'AdsMgGetServerType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgKillUser; external ACE_LIB name 'AdsMgKillUser' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetWorkerThreadActivity; external ACE_LIB name 'AdsMgGetWorkerThreadActivity' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgGetLockOwner; external ACE_LIB name 'AdsMgGetLockOwner' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsNullTerminateStrings; external ACE_LIB name 'AdsNullTerminateStrings' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsOpenIndex; external ACE_LIB name 'AdsOpenIndex' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsOpenTable; external ACE_LIB name 'AdsOpenTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsOpenTable90; external ACE_LIB name 'AdsOpenTable90' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsOpenTable101; external ACE_LIB name 'AdsOpenTable101' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsPackTable; external ACE_LIB name 'AdsPackTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsPackTable120; external ACE_LIB name 'AdsPackTable120' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRecallRecord; external ACE_LIB name 'AdsRecallRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRecallAllRecords; external ACE_LIB name 'AdsRecallAllRecords' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRefreshRecord; external ACE_LIB name 'AdsRefreshRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearProgressCallback; external ACE_LIB name 'AdsClearProgressCallback' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRegisterProgressCallback; external ACE_LIB name 'AdsRegisterProgressCallback' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRegisterCallbackFunction; external ACE_LIB name 'AdsRegisterCallbackFunction' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsRegisterCallbackFunction101; external ACE_LIB name 'AdsRegisterCallbackFunction101' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$ENDIF}
      function AdsClearCallbackFunction; external ACE_LIB name 'AdsClearCallbackFunction' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetSQLTimeout; external ACE_LIB name 'AdsSetSQLTimeout' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsReindex; external ACE_LIB name 'AdsReindex' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsReindex61; external ACE_LIB name 'AdsReindex61' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsReindexFTS; external ACE_LIB name 'AdsReindexFTS' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsResetConnection; external ACE_LIB name 'AdsResetConnection' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRollbackTransaction; external ACE_LIB name 'AdsRollbackTransaction' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSeek; external ACE_LIB name 'AdsSeek' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSeekLast; external ACE_LIB name 'AdsSeekLast' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetBinary; external ACE_LIB name 'AdsSetBinary' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetCollationLang; external ACE_LIB name 'AdsSetCollationLang' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetCollation; external ACE_LIB name 'AdsSetCollation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetDate; external ACE_LIB name 'AdsSetDate' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetDateFormat; external ACE_LIB name 'AdsSetDateFormat' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetDateFormat60; external ACE_LIB name 'AdsSetDateFormat60' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetDecimals; external ACE_LIB name 'AdsSetDecimals' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetDefault; external ACE_LIB name 'AdsSetDefault' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsShowDeleted; external ACE_LIB name 'AdsShowDeleted' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetDouble; external ACE_LIB name 'AdsSetDouble' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetEmpty; external ACE_LIB name 'AdsSetEmpty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetEpoch; external ACE_LIB name 'AdsSetEpoch' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetExact; external ACE_LIB name 'AdsSetExact' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetExact22; external ACE_LIB name 'AdsSetExact22' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetField; external ACE_LIB name 'AdsSetField' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetFieldW; external ACE_LIB name 'AdsSetFieldW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetFilter; external ACE_LIB name 'AdsSetFilter' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetFilter100; external ACE_LIB name 'AdsSetFilter100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetHandleLong; external ACE_LIB name 'AdsSetHandleLong' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetJulian; external ACE_LIB name 'AdsSetJulian' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetLogical; external ACE_LIB name 'AdsSetLogical' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetLong; external ACE_LIB name 'AdsSetLong' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsSetLongLong; external ACE_LIB name 'AdsSetLongLong' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$ENDIF}
      function AdsSetMilliseconds; external ACE_LIB name 'AdsSetMilliseconds' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$IFDEF ADSDELPHI4_OR_NEWER}
      function AdsSetMoney; external ACE_LIB name 'AdsSetMoney' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
{$ENDIF}
      function AdsSetRecord; external ACE_LIB name 'AdsSetRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetRelation; external ACE_LIB name 'AdsSetRelation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetRelKeyPos; external ACE_LIB name 'AdsSetRelKeyPos' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetScope; external ACE_LIB name 'AdsSetScope' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetScopedRelation; external ACE_LIB name 'AdsSetScopedRelation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetSearchPath; external ACE_LIB name 'AdsSetSearchPath' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetServerType; external ACE_LIB name 'AdsSetServerType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetShort; external ACE_LIB name 'AdsSetShort' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetString; external ACE_LIB name 'AdsSetString' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetStringW; external ACE_LIB name 'AdsSetStringW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetStringFromCodePage; external ACE_LIB name 'AdsSetStringFromCodePage' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetTime; external ACE_LIB name 'AdsSetTime' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsShowError; external ACE_LIB name 'AdsShowError' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSkip; external ACE_LIB name 'AdsSkip' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSkipUnique; external ACE_LIB name 'AdsSkipUnique' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsThreadExit; external ACE_LIB name 'AdsThreadExit' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsUnlockRecord; external ACE_LIB name 'AdsUnlockRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsUnlockTable; external ACE_LIB name 'AdsUnlockTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsVerifyPassword; external ACE_LIB name 'AdsVerifyPassword' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsEncryptionEnabled; external ACE_LIB name 'AdsIsEncryptionEnabled' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsWriteAllRecords; external ACE_LIB name 'AdsWriteAllRecords' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsWriteRecord; external ACE_LIB name 'AdsWriteRecord' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsZapTable; external ACE_LIB name 'AdsZapTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetAOF; external ACE_LIB name 'AdsSetAOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetAOF100; external ACE_LIB name 'AdsSetAOF100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEvalAOF; external ACE_LIB name 'AdsEvalAOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEvalAOF100; external ACE_LIB name 'AdsEvalAOF100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearAOF; external ACE_LIB name 'AdsClearAOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRefreshAOF; external ACE_LIB name 'AdsRefreshAOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetAOF; external ACE_LIB name 'AdsGetAOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetAOF100; external ACE_LIB name 'AdsGetAOF100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetAOFOptLevel; external ACE_LIB name 'AdsGetAOFOptLevel' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetAOFOptLevel100; external ACE_LIB name 'AdsGetAOFOptLevel100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsRecordInAOF; external ACE_LIB name 'AdsIsRecordInAOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCustomizeAOF; external ACE_LIB name 'AdsCustomizeAOF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsInitRawKey; external ACE_LIB name 'AdsInitRawKey' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsBuildRawKey; external ACE_LIB name 'AdsBuildRawKey' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsBuildRawKey100; external ACE_LIB name 'AdsBuildRawKey100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateSQLStatement; external ACE_LIB name 'AdsCreateSQLStatement' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsPrepareSQL; external ACE_LIB name 'AdsPrepareSQL' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsPrepareSQLW; external ACE_LIB name 'AdsPrepareSQLW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCachePrepareSQL; external ACE_LIB name 'AdsCachePrepareSQL' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCachePrepareSQLW; external ACE_LIB name 'AdsCachePrepareSQLW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsExecuteSQL; external ACE_LIB name 'AdsExecuteSQL' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsExecuteSQLDirect; external ACE_LIB name 'AdsExecuteSQLDirect' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsExecuteSQLDirectW; external ACE_LIB name 'AdsExecuteSQLDirectW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCloseSQLStatement; external ACE_LIB name 'AdsCloseSQLStatement' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtSetTableRights; external ACE_LIB name 'AdsStmtSetTableRights' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtSetTableReadOnly; external ACE_LIB name 'AdsStmtSetTableReadOnly' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtSetTableLockType; external ACE_LIB name 'AdsStmtSetTableLockType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtSetTableCharType; external ACE_LIB name 'AdsStmtSetTableCharType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtSetTableType; external ACE_LIB name 'AdsStmtSetTableType' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtSetTableCollation; external ACE_LIB name 'AdsStmtSetTableCollation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtConstrainUpdates; external ACE_LIB name 'AdsStmtConstrainUpdates' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtEnableEncryption; external ACE_LIB name 'AdsStmtEnableEncryption' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtDisableEncryption; external ACE_LIB name 'AdsStmtDisableEncryption' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtSetTablePassword; external ACE_LIB name 'AdsStmtSetTablePassword' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtClearTablePasswords; external ACE_LIB name 'AdsStmtClearTablePasswords' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsStmtReadAllColumns; external ACE_LIB name 'AdsStmtReadAllColumns' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearSQLParams; external ACE_LIB name 'AdsClearSQLParams' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetTimeStamp; external ACE_LIB name 'AdsSetTimeStamp' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsClearSQLAbortFunc; external ACE_LIB name 'AdsClearSQLAbortFunc' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRegisterSQLAbortFunc; external ACE_LIB name 'AdsRegisterSQLAbortFunc' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRegisterUDF; external ACE_LIB name 'AdsRegisterUDF' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetNumParams; external ACE_LIB name 'AdsGetNumParams' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetLastAutoinc; external ACE_LIB name 'AdsGetLastAutoinc' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsIndexUserDefined; external ACE_LIB name 'AdsIsIndexUserDefined' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRestructureTable; external ACE_LIB name 'AdsRestructureTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRestructureTable90; external ACE_LIB name 'AdsRestructureTable90' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRestructureTable120; external ACE_LIB name 'AdsRestructureTable120' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetSQLStatementHandle; external ACE_LIB name 'AdsGetSQLStatementHandle' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetSQLStatement; external ACE_LIB name 'AdsGetSQLStatement' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFlushFileBuffers; external ACE_LIB name 'AdsFlushFileBuffers' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDDeployDatabase; external ACE_LIB name 'AdsDDDeployDatabase' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsVerifySQL; external ACE_LIB name 'AdsVerifySQL' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsVerifySQLW; external ACE_LIB name 'AdsVerifySQLW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDisableUniqueEnforcement; external ACE_LIB name 'AdsDisableUniqueEnforcement' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEnableUniqueEnforcement; external ACE_LIB name 'AdsEnableUniqueEnforcement' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDisableRI; external ACE_LIB name 'AdsDisableRI' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEnableRI; external ACE_LIB name 'AdsEnableRI' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDisableAutoIncEnforcement; external ACE_LIB name 'AdsDisableAutoIncEnforcement' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsEnableAutoIncEnforcement; external ACE_LIB name 'AdsEnableAutoIncEnforcement' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsRollbackTransaction80; external ACE_LIB name 'AdsRollbackTransaction80' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsCreateSavepoint; external ACE_LIB name 'AdsCreateSavepoint' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDFreeTable; external ACE_LIB name 'AdsDDFreeTable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetIndexProperty; external ACE_LIB name 'AdsDDSetIndexProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsFieldBinary; external ACE_LIB name 'AdsIsFieldBinary' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsNull; external ACE_LIB name 'AdsIsNull' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsNullable; external ACE_LIB name 'AdsIsNullable' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetNull; external ACE_LIB name 'AdsSetNull' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetTableCollation; external ACE_LIB name 'AdsGetTableCollation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetIndexCollation; external ACE_LIB name 'AdsGetIndexCollation' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetDataLength; external ACE_LIB name 'AdsGetDataLength' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetIndexDirection; external ACE_LIB name 'AdsSetIndexDirection' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsMgKillUser90; external ACE_LIB name 'AdsMgKillUser90' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsGetFieldLength100; external ACE_LIB name 'AdsGetFieldLength100' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetRightsChecking; external ACE_LIB name 'AdsSetRightsChecking' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsSetTableTransactionFree; external ACE_LIB name 'AdsSetTableTransactionFree' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsIsTableTransactionFree; external ACE_LIB name 'AdsIsTableTransactionFree' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFindServers; external ACE_LIB name 'AdsFindServers' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsBinaryToFileW; external ACE_LIB name 'AdsBinaryToFileW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsFileToBinaryW; external ACE_LIB name 'AdsFileToBinaryW' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};
      function AdsDDSetTriggerProperty; external ACE_LIB name 'AdsDDSetTriggerProperty' {$IFDEF ADSDELPHI2010_OR_NEWER} delayed {$ENDIF};

{$IFNDEF FPC}
  {$IFDEF ADSDELPHI2010_OR_NEWER}
    {$WARN SYMBOL_PLATFORM ON}
  {$ENDIF}
{$ENDIF}

end.