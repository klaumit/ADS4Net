// (c) 2017 SAP SE or an SAP affiliate company. All rights reserved.
//
// No part of this publication may be reproduced or transmitted in any form or
// for any purpose without the express permission of SAP SE or an SAP affiliate
// company.
//
// The information contained herein may be changed without prior notice.  Some
// software products marketed by SAP SE and its distributors contain
// proprietary software components of other software vendors. National product
// specifications may vary.
//
// These materials are provided by SAP SE or an SAP affiliate company for
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

/*******************************************************************************
* Source File  : adsbackup.c
* Description  : Command line backup utility for use with Advantage Database
*                Server.  This utility packages up all the required information
*                to perform a backup or restore and executes the backup or
*                restore on the specified server.
* Notes        : This utility loads ACE32.DLL or libace.so dynamically so it can
*                be used with different versions of ACE.  The minimum ACE version
*                requirement is v6.0.  However, if the server OS is NetWare, the
*                ACE version must be 7.0 or greater.
*
*                To compile under linux use: gcc -ldl adsbackup.c
*******************************************************************************/
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/timeb.h>

#if defined( WIN32 )

   #include <windows.h>
   #include <conio.h>

   #if defined( x64 )
      #define ACE32_MODULE_NAME  "ace64.dll"
   #else
      #define ACE32_MODULE_NAME  "ace32.dll"
   #endif

#elif defined( __linux__ )

   #include <dlfcn.h>
   #include <errno.h>
   #include <termios.h>
   #include <sys/time.h>
   #include <string.h>
   #include <stdlib.h>

   #ifndef FALSE
      #define FALSE           0
   #endif

   #ifndef TRUE
      #define TRUE            1
   #endif

   #define ACE

   typedef void*              HMODULE;
   #define ACE32_MODULE_NAME  "libace.so"
   #define GetLastError()     (errno)
   #define stricmp( x, y )    strcasecmp( x, y )
#else
   #error WIN32 or __linux__ not defined
#endif

#include "ace.h"

/* ACE API function pointers */
ADSCONNECT60_PTR                 gpfnAdsConnect60;
ADSCONNECT101_PTR                gpfnAdsConnect101;
ADSDISCONNECT_PTR                gpfnAdsDisconnect;
ADSGETLASTERROR_PTR              gpfnAdsGetLastError;
ADSCREATESQLSTATEMENT_PTR        gpfnAdsCreateSQLStatement;
ADSSTMTSETTABLETYPE_PTR          gpfnAdsStmtSetTableType;
ADSSTMTSETTABLECHARTYPE_PTR      gpfnAdsStmtSetTableCharType;
ADSSTMTSETTABLECOLLATION_PTR     gpfnAdsStmtSetTableCollation;
ADSSTMTSETTABLELOCKTYPE_PTR      gpfnAdsStmtSetTableLockType;
ADSSTMTSETTABLERIGHTS_PTR        gpfnAdsStmtSetTableRights;
ADSEXECUTESQLDIRECT_PTR          gpfnAdsExecuteSQLDirect;
ADSPREPARESQL_PTR                gpfnAdsPrepareSQL;
ADSSETSTRING_PTR                 gpfnAdsSetString;
ADSSETLONG_PTR                   gpfnAdsSetLong;
ADSEXECUTESQL_PTR                gpfnAdsExecuteSQL;
ADSREGISTERCALLBACKFUNCTION_PTR  gpfnAdsRegisterCallbackFunction;
ADSGETLONG_PTR                   gpfnAdsGetLong;
ADSSKIP_PTR                      gpfnAdsSkip;
ADSATEOF_PTR                     gpfnAdsAtEOF;
ADSGOTOTOP_PTR                   gpfnAdsGotoTop;



/* Option variables */
UNSIGNED8  gucDatabaseMode = FALSE;        // TRUE if we're operating on a database, FALSE if free tables
UNSIGNED8  gucDontOverwrite = FALSE;       // TRUE if we don't want to overwrite files already backed up
UNSIGNED8  gucMetadataOnly = FALSE;        // TRUE if we only want to backup the DD metadata
UNSIGNED8  gucDiff = FALSE;                // TRUE if we're doing a differential backup
UNSIGNED8  gucPrepareDiff = FALSE;         // TRUE if we're preparing a differential backup
UNSIGNED8  gucRestore = FALSE;             // TRUE if we're doing a restore, FALSE for doing a backup
UNSIGNED8  gucVerbose = FALSE;             // TRUE to turn on Verbose mode for more output information
UNSIGNED8  gucDebugSnapshot = FALSE;       // TRUE to perform a debug snapshot
SIGNED32   glSeverity = 1;                 // Minimum severity level of errors to report (default to 1 to get all)
UNSIGNED8  *gpucInitialTime = NULL;
UNSIGNED8  *gpucReleaseTime = NULL;
UNSIGNED8  *gpucFinalTime = NULL;
UNSIGNED8  *gpucSourcePath = NULL;         // Source path of backup or restore
UNSIGNED8  *gpucDestinationPath = NULL;    // Destination path of backup or restore
UNSIGNED8  *gpucIncludeList = NULL;        // List of tables in include in a backup
UNSIGNED8  *gpucExcludeList = NULL;        // List of tables to exclude from a backup
UNSIGNED8  *gpucFreeTablePasswords = NULL; // Free table password(s)
UNSIGNED8  *gpucPassword = NULL;           // DD user password in database mode, free table password in free table mode
UNSIGNED8  *gpucOptions = NULL;            // Options string for Backup/Restore SQL stmt
UNSIGNED8  *gpucFileMask = NULL;           // File mask for free table backups, ie *.adt
UNSIGNED8  *gpucOutputFile = NULL;         // Path+filename of output file
UNSIGNED8  *gpucOutputPath = NULL;         // Path to store output file (filename determined by adsbackup)
UNSIGNED8  *gpucConnectionPath = NULL;     // Connection path of ADS server to perform backup or restore on
UNSIGNED8  *gpucConnectionStr = NULL;      // Connection string for the AdsConnect101 API
UNSIGNED8  *gpucTableTypeMap = NULL;       // Table type map to add to free table backup/restore options
UNSIGNED16 gusTableType = ADS_ADT;         // Table type of SQL statement
UNSIGNED16 gusCharType = ADS_ANSI;         // Character type of SQL statement (used if the collation API not available)
UNSIGNED8  *gpucCollation = NULL;          // Charset or new VFP-compatible collations.  Default
                                           // value is ANSI
UNSIGNED8  *gpucUserName = "ADSSYS";       // DD Username to use
UNSIGNED16 gusLockType = ADS_PROPRIETARY_LOCKING; // Locking type of SQL statement
UNSIGNED16 gusRightsChecking = ADS_CHECKRIGHTS; // Rights checking mode of SQL statement
UNSIGNED8  gucDumpEmptyOutput = TRUE;      // TRUE if we want to insert the backup/restore output into a table when there are no errors



/* Version information string for ADSVER.EXE utility */
char *gpcIdAxsRevisionStr = "EsIAx!@# 12.00.0.02";


#if defined( __linux__ )
/*******************************************************************************
*  Module       : LoadLibrary
*  Created      : 11/26/2001
*  Last Mod     :
*  Return       : handle of the shared object
*  Desc         : load a shared object
*  Notes        : win compatibility layer
*******************************************************************************/
void *LoadLibrary( char *pucLibName )
{
   return dlopen( pucLibName, RTLD_NOW );
}



/*******************************************************************************
*  Module       : FreeLibrary
*  Created      : 11/26/2001
*  Last Mod     :
*  Return       : non-zero on success, 0 on error
*  Desc         : unload a shared object
*  Notes        : win compatibility layer
*******************************************************************************/
unsigned short FreeLibrary( void *hLib )
{
   /* return boolean value */
   return !(dlclose( hLib ));
}



/*******************************************************************************
*  Module       : GetProcAddress
*  Created      : 11/26/2001
*  Last Mod     :
*  Return       : pointer to function
*  Desc         : get pointer to function pucFunc
*  Notes        : win compatibility layer
*******************************************************************************/
void *GetProcAddress( void *hLib, char *pucFunc )
{
   return dlsym( hLib, pucFunc );
}



static struct termios stored_settings;
/*******************************************************************************
* Module      : set_keypress
* Created     : 6/24/2005
* Last Mod    :
* Return      :
* Description : Configures the terminal to return control after reading one byte.
*               Also set it to not echo the input to look nicer.
* Notes       : Derived from the comp.unix.programmer FAQ
********************************************************************************/
void set_keypress(void)
{
    struct termios new_settings;

    tcgetattr(0,&stored_settings);

    new_settings = stored_settings;

    /* Disable canonical mode */
    new_settings.c_lflag &= (~ICANON);
    /* Also disable echo as we don't want to see the input */
    new_settings.c_lflag &= (~ECHO);

    new_settings.c_cc[VTIME] = 0;
    new_settings.c_cc[VMIN] = 1;

    tcsetattr(0,TCSANOW,&new_settings);
    return;
} /* set_keypress */



/*******************************************************************************
* Module      : reset_keypress
* Created     : 6/24/2005
* Last Mod    :
* Return      :
* Description : Reset the terminal to it's original settings
* Notes       : Derived from the comp.unix.programmer FAQ
********************************************************************************/
void reset_keypress(void)
{
    tcsetattr(0,TCSANOW,&stored_settings);
    return;
} /* reset_keypress */

#endif /* __linux__ */



/*******************************************************************************
* Module      : HitEscape
* Created     : 6/22/2005
* Last Mod    :
* Return      : TRUE or FALSE
* Description : Returns TRUE if the ESCAPE key has been hit
* Notes       :
********************************************************************************/
UNSIGNED8 HitEscape( void )
{
   int iHit = 0;

#ifdef WIN32
   iHit = kbhit();
   if ( iHit )
      iHit = getch();
#else
   fd_set rfds;
   struct timeval tv;

   FD_ZERO( &rfds );
   FD_SET( 0, &rfds );

   /* Wait for zero seconds */
   tv.tv_sec = 0;
   tv.tv_usec = 0;

   /* Check stdin for any input */
   if ( select( 1, &rfds, NULL, NULL, &tv ))
      iHit = getchar();
#endif

   return iHit == 27 ? TRUE : FALSE;
} /* HitEscape */



/*******************************************************************************
* Module      : ShowPercentage
* Created     : 6/22/2005
* Last Mod    :
* Return      : 0 or 1
* Description : Progress callback function for use with AdsExecuteSQLDirect
* Notes       : Return zero to continue the SQL query, return non-zero to abort it.
********************************************************************************/
UNSIGNED32 WINAPI ShowPercentage( UNSIGNED16 usPercentDone, UNSIGNED32 ulCallbackID )
{
   // If the ESCAPE key has been struck, return a non-zero value to abort the operation
   if ( HitEscape() )
      return 1;

   printf( "\rPercent Complete: %2d%%  Press ESC to abort.", usPercentDone );
   return 0;

}  /* ShowPercentage */



/*******************************************************************************
* Module      : PrintUsage
* Created     : 6/21/2005
* Last Mod    :
* Return      :
* Description : Print the list of accepted options and command line arguments
* Notes       :
********************************************************************************/
void PrintUsage( void )
{
   printf( "Advantage Database Server Backup Utility\n" );
   printf( "Usage:\n" );
   printf( "       Free Table Backup\n" );
   printf( "          adsbackup [options] <src path> <file mask> <dest path>\n" );
   printf( "       Data Dictionary Backup\n" );
   printf( "          adsbackup [options] <src database> <dest path>\n" );
   printf( "Valid options include:\n" );
   printf( "   -a  Prepare a database for a differential backup\n" );
   printf( "   -c[ANSI|OEM|<dynamic collation>]  Character type (default ANSI)\n" );
   printf( "       The dynamic collations include the VFP-compatible collations that are\n" );
   printf( "       loaded at runtime.  Refer to the help file for the collation names.\n" );
   printf( "   -d  Don't overwrite existing backup tables\n" );
   printf( "   -e<file1, .. ,filen>  Exclude file list\n" );
   printf( "   -f  Differential backup\n" );
   printf( "   -h[ON|OFF]  Rights checking (default ON)\n" );
   printf( "   -i<file1, .. ,filen>  Include file list\n" );
   printf( "   -m  Backup metadata only\n" );
   printf( "   -n<path>  Path to store the backup output table\n" );
   printf( "   -o<filepath>  Path and filename to the backup output table\n" );
   printf( "   -p  Password(s): Database user password if source path is a database\n" );
   printf( "                    List of free table passwords if source path is a directory\n" );
   printf( "                    Free table usage can pass a single password for all\n" );
   printf( "                    encrypted tables, or a name/value pair for each table, for\n" );
   printf( "                    example, \"table1=pass1;table2=pass2\"\n" );
   printf( "   -q[PROP|COMPAT]  Locking mode, proprietary or compatible (default PROP)\n" );
   printf( "   -r  Restore database or free table files\n" );
   printf( "   -s<server path|connection string>\n"
           "                    Connection path of ADS server to perform backup or restore or\n"
           "                    a connection string\n" );
// printf( "   -t<server port>  Connection port of ADS server to perform backup or restore\n" ); // Option 't' used by JDBC backup utility only
   printf( "   -u[ADT|CDX|VFP|NTX]  Table type of backup output table (default ADT)\n" );
   printf( "   -v[1-10]  Lowest level of error severity to return (default 1)\n" );
   printf( "   -w  Table Type Map used to perform a free table backup or restore with\n" );
   printf( "       mixed table types. Used to provide the table type for each table\n" );
   printf( "       extension. For example, -wadt=ADS_ADT,dbf=ADS_CDX\n" );
   printf( "   -x  Don't create the output table if no errors are logged\n" );
   printf( "   -y  Database user name (if using a non-AdsSys user)\n" );
   printf( "   -z  Other online backup/restore options.  Pass any options that are valid\n"
           "       as Backup and Restore Options in the help file.  These can be used in\n"
           "       addition to or in place of the other command line parameters.  For\n"
           "       example,\n"
           "           -z \"Include=a*.adt;diff;ArchiveFile=backup.tar\"\n" );
   printf( "\n" );
   printf( "For the options that require values, the space between the option and the value\n"
           "is not required.  For example, -pPassword and -p Password are equivalent.\n\n" );
   printf( "Examples:\n" );
#ifdef __linux__
   printf( "   adsbackup -p Pass //server/share/mydata.add //server/share/backup\n" );
   printf( "   adsbackup -pTablePass //server/share *.adt //server/share/backup\n" );
   printf( "   adsbackup -r -pPass //server/share/backup/mydata.add //server/share/backup/mydata.add\n" );
   printf( "   adsbackup -r -pTablePass //server/share/backup //server/share\n" );
   printf( "\n   The following example (entered as a single command) shows how to use the\n"
           "   -s option to provide a connection string specifying FIPS mode and a TLS\n"
           "   connection.\n" );
   printf( "   adsbackup -s \"Data Source=//server/share/;CommType=TLS; \\\n"
           "             FIPS=TRUE;TLSCertificate=/path/clientcert.pem; \\\n"
           "             TLSCommonName=www.myserver.com;DDPassword=dictionarypass;\" \\\n"
           "             //server/share/mydata.add //server/share/backup\n" );
   printf( "   adsbackup //server/share/mydata.add //server/share/backup \\\n"
           "             -z \"ArchiveFileCompressed=mydata.tar.gz;ForceArchiveExtract\"\n" );
   printf( "   adsbackup -r //server/share/backup/mydata.add \\\n"
           "                //server/share/restore/mydata.add \\\n"
           "             -s //server/share/ \\\n"
           "             -z \"ArchiveFileCompressed=mydata.tar.gz\"\n" );
#else
   printf( "   adsbackup -pPass \\\\server\\share\\mydata.add \\\\server\\share\\backup\n" );
   printf( "   adsbackup -pTablePass \\\\server\\share *.adt \\\\server\\share\\backup\n" );
   printf( "   adsbackup -r -pPass \\\\server\\share\\backup\\mydata.add \\\\server\\share\\backup\\mydata.add\n" );
   printf( "   adsbackup -r -pTablePass \\\\server\\share\\backup \\\\server\\share\n" );
   printf( "\n   The following example (entered as a single command) shows how to use the\n"
           "   -s option to provide a connection string specifying FIPS mode and a TLS\n"
           "   connection.\n" );
   printf( "   adsbackup -s \"Data Source=\\\\server\\share\\;CommType=TLS; \\\n"
           "             FIPS=TRUE;TLSCertificate=c:\\path\\clientcert.pem; \\\n"
           "             TLSCommonName=www.myserver.com;DDPassword=dictionarypass;\" \\\n"
           "             \\\\server\\share\\mydata.add \\\\server\\share\\backup\n" );
   printf( "   adsbackup \\\\server\\share\\mydata.add \\\\server\\share\\backup \\\n"
           "             -z \"ArchiveFileCompressed=mydata.tar.gz;ForceArchiveExtract\"\n" );
   printf( "   adsbackup -r \\\\server\\share\\backup\\mydata.add \\\n"
           "                \\\\server\\share\\restore\\mydata.add \\\n"
           "             -s \\\\server\\share\\ \\\n"
           "             -z \"ArchiveFileCompressed=mydata.tar.gz\"\n" );
#endif
   exit( 0 );
} /* PrintUsage */



/*******************************************************************************
* Module      : GetEntryPoint
* Created     : 6/21/2005
* Last Mod    :
* Return      : zero on success, otherwise last error number
* Description : Get a specific ACE entrypoint via GetProcAddress
* Notes       :
********************************************************************************/
UNSIGNED32 GetEntryPoint( HMODULE hAce32, char *pcEntrypointName, void **ppfnEntrypoint )
{
   *ppfnEntrypoint = GetProcAddress( hAce32, pcEntrypointName );
   if ( *ppfnEntrypoint == NULL )
      return GetLastError();
   else
      return AE_SUCCESS;
} /* GetEntryPoint */



/*******************************************************************************
* Module      : GetAdsEntrypoints
* Created     : 6/21/2005
* Last Mod    :
* Return      : zero on success, otherwise last error number
* Description : Get all required ACE entrypoint function pointers
* Notes       :
********************************************************************************/
UNSIGNED32 GetAdsEntrypoints( HMODULE hAce32 )
{
   UNSIGNED32 ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsConnect60", (void**)&gpfnAdsConnect60 );
   if ( ulRetCode || ( gpfnAdsConnect60 == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsConnect101", (void**)&gpfnAdsConnect101 );
   if ( ulRetCode || ( gpfnAdsConnect101 == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsDisconnect", (void**)&gpfnAdsDisconnect );
   if ( ulRetCode || ( gpfnAdsDisconnect == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsCreateSQLStatement", (void**)&gpfnAdsCreateSQLStatement );
   if ( ulRetCode || ( gpfnAdsCreateSQLStatement == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsStmtSetTableType", (void**)&gpfnAdsStmtSetTableType );
   if ( ulRetCode || ( gpfnAdsStmtSetTableType == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsStmtSetTableLockType", (void**)&gpfnAdsStmtSetTableLockType );
   if ( ulRetCode || ( gpfnAdsStmtSetTableLockType == NULL ))
      return ulRetCode;

   // This is a new entrypoint.  In order to work with older ACE DLLs, allow this to fail and we
   // will use the old setchartype api.
   ulRetCode = GetEntryPoint( hAce32, "AdsStmtSetTableCollation", (void**)&gpfnAdsStmtSetTableCollation );
   if ( ulRetCode )
      // ensure it is null
      gpfnAdsStmtSetTableCollation = NULL;

   ulRetCode = GetEntryPoint( hAce32, "AdsStmtSetTableCharType", (void**)&gpfnAdsStmtSetTableCharType );
   if ( ulRetCode || ( gpfnAdsStmtSetTableCharType == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsStmtSetTableRights", (void**)&gpfnAdsStmtSetTableRights );
   if ( ulRetCode || ( gpfnAdsStmtSetTableRights == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsExecuteSQLDirect", (void**)&gpfnAdsExecuteSQLDirect );
   if ( ulRetCode || ( gpfnAdsExecuteSQLDirect == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsPrepareSQL", (void**)&gpfnAdsPrepareSQL );
   if ( ulRetCode || ( gpfnAdsPrepareSQL == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsSetString", (void**)&gpfnAdsSetString );
   if ( ulRetCode || ( gpfnAdsSetString == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsSetLong", (void**)&gpfnAdsSetLong );
   if ( ulRetCode || ( gpfnAdsSetLong == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsExecuteSQL", (void**)&gpfnAdsExecuteSQL );
   if ( ulRetCode || ( gpfnAdsExecuteSQL == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsGetLastError", (void**)&gpfnAdsGetLastError );
   if ( ulRetCode || ( gpfnAdsGetLastError == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsRegisterCallbackFunction", (void**)&gpfnAdsRegisterCallbackFunction );
   if ( ulRetCode || ( gpfnAdsRegisterCallbackFunction == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsGetLong", (void**)&gpfnAdsGetLong );
   if ( ulRetCode || ( gpfnAdsGetLong == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsAtEOF", (void**)&gpfnAdsAtEOF );
   if ( ulRetCode || ( gpfnAdsAtEOF == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsGotoTop", (void**)&gpfnAdsGotoTop );
   if ( ulRetCode || ( gpfnAdsGotoTop == NULL ))
      return ulRetCode;

   ulRetCode = GetEntryPoint( hAce32, "AdsSkip", (void**)&gpfnAdsSkip );
   if ( ulRetCode || ( gpfnAdsSkip == NULL ))
      return ulRetCode;

   return AE_SUCCESS;
} /* GetAdsEntrypoints */



#ifdef __linux__
// I copied this out of wincompat.c (mlw).
/*******************************************************************************
*  Module       :  strupr
*  Created      :  04/04/2001   Jeremy Mullin
*  Last Mod     :
*  Global Vars  :
*  Return       :  pointer to string
*  Description  :  Make a char buffer all uppercase.
*  Notes        :  Only toupper existed in unix clib
*******************************************************************************/
char *strupr( char *string )
{
   while ( *string )
      {
      *string = toupper( *string );
      string++;
      }

   return string;
}
#endif


/*******************************************************************************
* Module      : DumpArguments
* Created     : 01/04/2008
* Last Mod    :
* Return      :
* Description : Print out what arguments were given in the case of an error
* Notes       : Only prints out arguments, not options
********************************************************************************/
void DumpArguments( void )
{
   printf( "%s arguments:\n", gucRestore ? "Restore" : "Backup" );
   printf( "Source path: %s\nDestination path: %s\n",
           gpucSourcePath ? gpucSourcePath : (UNSIGNED8*)"NULL",
           gpucDestinationPath? gpucDestinationPath : (UNSIGNED8*)"NULL" );

   if ( gpucFileMask )
      printf( "File mask: %s\n", gpucFileMask );

} /* DumpArguments */



/*******************************************************************************
* Module      : ParseOptions
* Created     : 6/21/2005
* Last Mod    :
* Return      :
* Description : Parse the command line options and arguments into the global
*               option & argument variables.
* Notes       :
********************************************************************************/
UNSIGNED32 ParseOptions( int argc, char *argv[] )
{
   int        iArg;
   UNSIGNED32 ulAllocationSize;
   char       *pcValue;
   int        bValueInNextArg;
   int        bValueUsed;
   char       aucDDPass[50 + 1];  // max password is currently 20, but allow more for
                                  // now in case it grows since may not find this bit of
                                  // code to update later (there is apparently no constant
                                  // in ACE for max password)
   char       *pucDDPassPos = NULL;
   char       *pcGeneralOptions = NULL;

   for ( iArg = 1; iArg < argc; iArg++ )
      {
      // All options should be preceded by a '-'
      if ( argv[iArg][0] == '-' )
         {
         // Do a bit of prep work for parameter values.  This utility originally required
         // parameter values to immediately follow the option.  e.g., -pPASSWORD.  I'm
         // updating it to allow a space in between them, but for obvious reasons must
         // continue to support the non-spaced version.
         pcValue = NULL;
         bValueInNextArg = FALSE;
         bValueUsed = FALSE;
         if ( argv[iArg][1] != '\0' )
            {
            // A parameter letter exists (it isn't just a standalone -)
            if (( iArg + 1 < argc ) && // there is at least one more arg
                ( argv[iArg][2] == '\0' ))   // it is a standalone option letter with nothing after
               {
               // point the value at the next parameter in case we want it
               pcValue = argv[iArg + 1];
               bValueInNextArg = TRUE;
               }
            else
               {
               // there is something immediately following the letter (-cANSI) or it
               // is the very last parameter
               pcValue = &argv[iArg][2];
               bValueInNextArg = FALSE;
               }
            }

         switch ( argv[iArg][1] )
            {
            case 'a':
            case 'A':
               gucPrepareDiff = TRUE;
               break;

            case 'b':
            case 'B':
               gucDebugSnapshot = TRUE;
               break;

            case 'c':
            case 'C':
               bValueUsed = TRUE;  // this is an option with a value

               // This is the collation language.  It can be ANSI or OEM to represent the
               // collations stamped into the server (or in the adslocal.cfg file).  Or it
               // can be one of the new VFP-compatible collations (e.g., MACHINE_VFP_BIN_1252
               // or GERMAN_VFP_CI_AS_437).  There are 60+ of those and more could be added in
               // the future, so don't try to validate it here.  Just pass it directly to the
               // API where it will be checked.
               gpucCollation = pcValue;

               // Set the char type as well in case collation API not available
               if ( !stricmp( pcValue, "ANSI" ))
                  gusCharType = ADS_ANSI;
               else if ( !stricmp( pcValue, "OEM" ))
                  gusCharType = ADS_OEM;
               else
                  // It might be a valid VFP collation.  We will catch the error later if needed
                  gusCharType = 0;

               break;

            case 'd':
            case 'D':
               gucDontOverwrite = TRUE;
               break;

            case 'e':
            case 'E':
               bValueUsed = TRUE;  // this is an option with a value
               gpucExcludeList = pcValue;
               break;

            case 'f':
            case 'F':
               gucDiff = TRUE;
               break;

            case 'g':
            case 'G':
               gucVerbose = TRUE;
               break;

            case 'h':
            case 'H':
               bValueUsed = TRUE;  // this is an option with a value
               if ( !stricmp( pcValue, "ON" ))
                  gusRightsChecking = ADS_CHECKRIGHTS;
               else if ( !stricmp( pcValue, "OFF" ))
                  gusRightsChecking = ADS_IGNORERIGHTS;
               else
                  {
                  printf( "Invalid rights checking option: %s\n", pcValue );
                  return AE_INVALID_OPTION;
                  }
               break;

            case 'i':
            case 'I':
               bValueUsed = TRUE;  // this is an option with a value
               gpucIncludeList = pcValue;
               break;

            case 'j':
            case 'J':
               bValueUsed = TRUE;  // this is an option with a value
               gpucReleaseTime = pcValue;
               break;

            case 'k':
            case 'K':
               bValueUsed = TRUE;  // this is an option with a value
               gpucFinalTime = pcValue;
               break;

            case 'l':
            case 'L':
               bValueUsed = TRUE;  // this is an option with a value
               gpucInitialTime = pcValue;
               break;

            case 'm':
            case 'M':
               gucMetadataOnly = TRUE;
               break;

            case 'n':
            case 'N':
               bValueUsed = TRUE;  // this is an option with a value
               gpucOutputPath = pcValue;
               break;

            case 'o':
            case 'O':
               bValueUsed = TRUE;  // this is an option with a value
               gpucOutputFile = pcValue;
               break;

            case 'p':
            case 'P':
               bValueUsed = TRUE;  // this is an option with a value
               gpucPassword = pcValue;
               break;

            case 'q':
            case 'Q':
               bValueUsed = TRUE;  // this is an option with a value
               if ( !stricmp( pcValue, "PROP" ))
                  gusLockType = ADS_PROPRIETARY_LOCKING;
               else if ( !stricmp( pcValue, "COMPAT" ))
                  gusLockType = ADS_COMPATIBLE_LOCKING;
               else
                  {
                  printf( "Invalid locking type: %s\n", pcValue );
                  return AE_INVALID_OPTION;
                  }
               break;

            case 'r':
            case 'R':
               gucRestore = TRUE;

               /*
                * If the source and destination paths have already been
                * parsed, the destination path may be incorrectly stored.
                */
               if ( gpucFileMask )
                  {
                  gpucDestinationPath = gpucFileMask;
                  gpucFileMask = NULL;
                  }
               break;

            case 's':
            case 'S':
               bValueUsed = TRUE;  // this is an option with a value
               gpucConnectionPath = pcValue;
               break;

/*
            case 't':
            case 'T':
                Option 't' used by the JDBC client only
               break;
*/

            case 'u':
            case 'U':
               bValueUsed = TRUE;  // this is an option with a value
               if ( !stricmp( pcValue, "ADT" ))
                  gusTableType = ADS_ADT;
               else if ( !stricmp( pcValue, "CDX" ))
                  gusTableType = ADS_CDX;
               else if ( !stricmp( pcValue, "NTX" ))
                  gusTableType = ADS_NTX;
               else if ( !stricmp( pcValue, "VFP" ))
                  gusTableType = ADS_VFP;
               else
                  {
                  printf( "Invalid table type: %s\n", pcValue );
                  return AE_INVALID_OPTION;
                  }
               break;

            case 'v':
            case 'V':
               bValueUsed = TRUE;  // this is an option with a value
               glSeverity = atol( pcValue );
               break;

            case 'w':
            case 'W':
               bValueUsed = TRUE;  // this is an option with a value
               gpucTableTypeMap = pcValue;
               break;

            case 'x':
            case 'X':
               gucDumpEmptyOutput = FALSE;  //Don’t create the output table if no errors are logged
               break;

            case 'y':
            case 'Y':
               bValueUsed = TRUE;  // this is an option with a value
               gpucUserName = pcValue;
               break;

            case 'z':
               bValueUsed = TRUE;  // this is an option with a value
               pcGeneralOptions = pcValue;
               break;

            case '?':
               PrintUsage();
               break;

            default:
               printf( "Unrecognized option: %s\n", argv[iArg] );
               return AE_INVALID_OPTION;
            }  // switch

         if ( bValueUsed && bValueInNextArg )
            // The option was one requiring a value and the value was in the
            // next command line arg (as opposed to being pasted right up next to
            // the option letter with no space in between).  So increment to next arg
            iArg++;
         }
      else
         {
         /* Not an option, must be the source or destination path or file mask */
         if ( gpucSourcePath == NULL )
            gpucSourcePath = argv[iArg];
         else
            {
            /* Do a quick check to see if we're really doing a DB or free table operation */
            if (( !gucDatabaseMode ) && ( stricmp( (char*)&gpucSourcePath[ strlen( gpucSourcePath ) - 4 ], ".ADD" ) == 0 ))
                  gucDatabaseMode = TRUE;

             if (( gucDatabaseMode == FALSE ) &&
                  ( gucRestore == FALSE ) &&
                  ( gpucFileMask == NULL ))
               gpucFileMask = argv[iArg];
             else if ( gpucDestinationPath )
                {
                printf( "Invalid option, too many paths given: %s\n", argv[iArg] );
                return AE_INVALID_OPTION;
                }
             else
                gpucDestinationPath = argv[iArg];
            }
         }
      }

   // Before allocating anything, first validate the arguments
   if ( gpucSourcePath == NULL )
      {
      printf( "Missing argument, no source path given\n" );
      DumpArguments();
      return AE_INVALID_OPTION;
      }

   if ( gpucDestinationPath == NULL )
      {
      printf( "Missing argument, no destination path given\n" );
      DumpArguments();
      return AE_INVALID_OPTION;
      }

   if ( gpucFileMask )
      {
      if ( gucDatabaseMode )
         printf( "Warning: File mask argument ignored for database backups/restores: %s\n", gpucFileMask );

      else if ( gucRestore )
         printf( "Warning: File mask argument ignored for restore operations: %s\n", gpucFileMask );
      }

   // If they gave the connection path option, we need to determine if it is simply
   // a path or if it is a connection string.  The simple check is to look for equals
   // sign delimiters. This gets a false positive, if they have a file path with
   // equals signs in it ... but that seems unlikely and they can work around it
   // simply by adding "data source=" in front of their path.
   if ( gpucConnectionPath && strchr( gpucConnectionPath, '=' ))
      {
      int bHasDS = TRUE;
      ulAllocationSize = strlen( gpucConnectionPath ) + 2;

      // Assume it is a connection string.  If it does not contain the data source
      // option, then we want to add it using the source path.
      ulAllocationSize += strlen( gpucSourcePath ) + 13;

      if ( gpucPassword && *gpucPassword )
         ulAllocationSize += strlen( gpucPassword ) + 10;
      if ( gpucUserName && *gpucUserName )
         ulAllocationSize += strlen( gpucUserName ) + 8;

      gpucConnectionStr = malloc( ulAllocationSize );
      if ( gpucConnectionStr == NULL )
         {
         printf( "Memory allocation failure.\n" );
         return AE_ALLOCATION_FAILED;
         }

      // this is a kludgey way to do a stristr
      strcpy( gpucConnectionStr, gpucConnectionPath );
      strupr( gpucConnectionStr );
      if (( NULL == strstr( gpucConnectionStr, "DATA SOURCE" )) &&
          ( NULL == strstr( gpucConnectionStr, "DATASOURCE" )))
         // apparently does not have data source in it
         bHasDS = FALSE;

      // While we have the upper case version, find the position of the DDPassword
      // entry
      pucDDPassPos = strstr( gpucConnectionStr, "DDPASSWORD" );

      // get the lower case version again
      strcpy( gpucConnectionStr, gpucConnectionPath );
      strcat( gpucConnectionStr, ";" );

      if ( !bHasDS )
         {
         sprintf( gpucConnectionStr + strlen( gpucConnectionStr ), "Data Source=%s;",
                  gpucSourcePath );
         }

      // add user id and user name
      if ( gpucUserName && *gpucUserName )
         sprintf( gpucConnectionStr + strlen( gpucConnectionStr ), "User ID=%s;",
                  gpucUserName );

      if ( gpucPassword && *gpucPassword )
         sprintf( gpucConnectionStr + strlen( gpucConnectionStr ), "Password=%s;",
                  gpucPassword );

      // It would be possible to add a few of the other options to the connection
      // string (e.g., compat/proprietary locking info) but they are already handled
      // specifically on the sql statement itself, so there is no real value in that.

      }

   // If the ddpassword option is given in the connection string, then we also want to
   // grab it and add it to the sp_backup options.  This prevents us from needing to
   // add another command line option to the utility.
   memset( aucDDPass, 0, sizeof( aucDDPass ));
   if ( pucDDPassPos != NULL )
      {
      char *pcPos;
      int  iLen;

      // find the equals sign and then copy all of it in
      pcPos = pucDDPassPos;
      while (( *pcPos ) && ( *pcPos != '=' ))
         pcPos++;

      if ( *pcPos )
         {
         pcPos++;  // skip over =
         iLen = 0;
         // this won't work if they have a ; in the password itself
         while (( *pcPos ) && ( *pcPos != ';' ) &&
                ( iLen < sizeof( aucDDPass ) - 1 ))
            {
            aucDDPass[iLen++] = *pcPos;
            pcPos++;
            }
         }
      }

   // the constants here are the length of the keyword + 1 for semicolon and possibly
   // another +1 for equals sign if a value is involved.
   ulAllocationSize = ( gpucIncludeList ? ( strlen( gpucIncludeList ) + 9 ) : 0 ) +
                      ( gpucExcludeList ? ( strlen( gpucExcludeList ) + 9 ) : 0 ) +
                      ( gucDontOverwrite ? 14 : 0 ) +
                      ( gucMetadataOnly ? 9 : 0 ) +
                      ( gucPrepareDiff ? 12 : 0 ) +
                      ( gucDiff ? 5 : 0 ) +
                      ( gucDebugSnapshot ? 14 : 0 ) +
                      ( gucVerbose ? 8 : 0 ) +
                      ( gpucInitialTime ? ( strlen( gpucInitialTime ) + 13 ) : 0 ) +
                      ( gpucReleaseTime ? ( strlen( gpucReleaseTime ) + 13 ) : 0 ) +
                      ( gpucFinalTime ? ( strlen( gpucFinalTime ) + 11 ) : 0 ) +
                      ( gpucTableTypeMap ? ( strlen( gpucTableTypeMap ) + 14 ) : 0 ) +
                      ( aucDDPass[0] ? ( strlen( aucDDPass ) + 12 ) : 0 ) +
                      ( pcGeneralOptions ? ( strlen( pcGeneralOptions ) + 1 ) : 0 );

   if ( ulAllocationSize )
      {
      // Allocate 1 extra for the NULL terminator
      gpucOptions = malloc( ulAllocationSize + 1 );
      if ( gpucOptions == NULL )
         {
         printf( "Memory allocation failure.\n" );
         return AE_ALLOCATION_FAILED;
         }

      // Start with an empty string
      gpucOptions[0] = '\0';

      if ( gpucIncludeList )
         {
         strcat( gpucOptions, "INCLUDE=" );
         strcat( gpucOptions, gpucIncludeList );
         strcat( gpucOptions, ";" );
         }

      if ( gpucExcludeList )
         {
         strcat( gpucOptions, "EXCLUDE=" );
         strcat( gpucOptions, gpucExcludeList );
         strcat( gpucOptions, ";" );
         }

      if ( gucDontOverwrite )
         strcat( gpucOptions, "DONTOVERWRITE;" );

      if ( gucMetadataOnly )
         strcat( gpucOptions, "METAONLY;" );

      if ( gucPrepareDiff )
         strcat( gpucOptions, "PREPAREDIFF;" );

      if ( gucDiff )
         strcat( gpucOptions, "DIFF;" );

      if ( gucDebugSnapshot )
         strcat( gpucOptions, "DEBUGSNAPSHOT;" );

      if ( gucVerbose )
         strcat( gpucOptions, "VERBOSE;" );

      if ( gpucInitialTime )
         {
         strcat( gpucOptions, "INITIALTIME=" );
         strcat( gpucOptions, gpucInitialTime );
         strcat( gpucOptions, ";" );
         }

      if ( gpucReleaseTime )
         {
         strcat( gpucOptions, "RELEASETIME=" );
         strcat( gpucOptions, gpucReleaseTime );
         strcat( gpucOptions, ";" );
         }

      if ( gpucFinalTime )
         {
         strcat( gpucOptions, "FINALTIME=" );
         strcat( gpucOptions, gpucFinalTime );
         strcat( gpucOptions, ";" );
         }

      if ( gpucTableTypeMap )
         {
         strcat( gpucOptions, "TABLETYPEMAP=" );
         strcat( gpucOptions, gpucTableTypeMap );
         strcat( gpucOptions, ";" );
         }

      if ( aucDDPass[0] )
         {
         strcat( gpucOptions, "DDPassword=" );
         strcat( gpucOptions, aucDDPass );
         strcat( gpucOptions, ";" );
         }

      if ( pcGeneralOptions )
         {
         strcat( gpucOptions, pcGeneralOptions );
         if ( gpucOptions[strlen(gpucOptions) - 1] != ';' )
            strcat( gpucOptions, ";" );
         else
            // avoid the debug printf error on allocation size
            ulAllocationSize--;
         }
      }

#ifdef _DEBUG
   /* Debug check to make sure option string is the correct size */
   if ( ulAllocationSize && gpucOptions &&
        ( ulAllocationSize != strlen( gpucOptions )))
      printf( "Option string size is wrong.\n" );
#endif

   return AE_SUCCESS;
} /* ParseOptions */



/*******************************************************************************
* Module      : GetOutputTableName
* Created     : 6/23/2005
* Last Mod    :
* Return      :
* Description : Generate the backup output file name
* Notes       : Generates a filename based on the current date & time in this
*               format: CCYYMMDDHHMMSS.  Uses provided path if specified by the
*               -n option.
********************************************************************************/
UNSIGNED8 *GetOutputTableName( UNSIGNED8 *pucOutputTable )
{
   struct timeb stTimeb;
   struct tm   *pstTmPtr;

   ftime( &stTimeb );
   pstTmPtr = localtime( ( time_t * ) &(stTimeb.time ));
   if ( pstTmPtr == NULL )
      {
      printf( "An Error occurred while getting the current time: %d\n", GetLastError() );
      return NULL;
      }

   if ( gpucOutputPath )
      {
      if (( gpucOutputPath[ strlen( gpucOutputPath ) ] == '\\' ) ||
          ( gpucOutputPath[ strlen( gpucOutputPath ) ] == '/' ))
         sprintf( (char*)pucOutputTable, "%s%s_%.4d%.2d%.2d%.2d%.2d%.2d.%s",
                  gpucOutputPath,
                  gucRestore ? "restore" : "backup",
                  1900L + pstTmPtr->tm_year,
                  pstTmPtr->tm_mon + 1,
                  pstTmPtr->tm_mday,
                  pstTmPtr->tm_hour,
                  pstTmPtr->tm_min,
                  pstTmPtr->tm_sec,
                  gusTableType == ADS_ADT ? "adt" : "dbf" );
      else
         sprintf( (char*)pucOutputTable, "%s%c%s_%.4d%.2d%.2d%.2d%.2d%.2d.%s",
                  gpucOutputPath,
#ifdef __linux__
                  '/',
#else
                  '\\',
#endif
                  gucRestore ? "restore" : "backup",
                  1900L + pstTmPtr->tm_year,
                  pstTmPtr->tm_mon + 1,
                  pstTmPtr->tm_mday,
                  pstTmPtr->tm_hour,
                  pstTmPtr->tm_min,
                  pstTmPtr->tm_sec,
                  gusTableType == ADS_ADT ? "adt" : "dbf" );

      }
   else
      sprintf( (char*)pucOutputTable, "%s_%.4d%.2d%.2d%.2d%.2d%.2d.%s",
               gucRestore ? "restore" : "backup",
               1900L + pstTmPtr->tm_year,
               pstTmPtr->tm_mon + 1,
               pstTmPtr->tm_mday,
               pstTmPtr->tm_hour,
               pstTmPtr->tm_min,
               pstTmPtr->tm_sec,
               gusTableType == ADS_ADT ? "adt" : "dbf" );

   return pucOutputTable;
} /* GetOutputTableName */



/*******************************************************************************
* Module      : CheckForBackupError
* Created     : 6/23/2005
* Last Mod    :
* Return      :
* Description : Search the backup output table for errors worth returning from
*               main()
* Notes       : Backup severity values are:
*                                            HIGH        10
*                                            MEDHIGH     7
*                                            MED         5
*                                            LOW         1
*                                            NONE        0
********************************************************************************/
UNSIGNED32 CheckForBackupError( ADSHANDLE hResults )
{
   UNSIGNED32 ulRetCode;
   UNSIGNED32 ulBackupError = AE_SUCCESS;
   UNSIGNED32 ulErrors = 0;
   UNSIGNED32 ulFirstError;
   UNSIGNED16 usEOF;
   UNSIGNED8  aucError[ ADS_MAX_ERROR_LEN + 1 ];
   UNSIGNED16 usErrorLen;
   UNSIGNED32 ulSeverity;


   /* If the handle is 0 then there is not a result, print out success and exit. */
   if ( hResults == 0 )
      {
      printf( "Backup v%s - 0 error(s)\n", strstr( gpcIdAxsRevisionStr, " " ) + 1  );
      return AE_SUCCESS;
      }

   ulRetCode = (*gpfnAdsGotoTop)( hResults );
   if ( ulRetCode != AE_SUCCESS )
      {
      usErrorLen = sizeof( aucError );
      (*gpfnAdsGetLastError)( &ulRetCode, aucError, &usErrorLen );
      printf( "%s\n", aucError );
      return ulRetCode;
      }



   ulRetCode = (*gpfnAdsAtEOF)( hResults, &usEOF );
   if ( ulRetCode != AE_SUCCESS )
      {
      usErrorLen = sizeof( aucError );
      (*gpfnAdsGetLastError)( &ulRetCode, aucError, &usErrorLen );
      printf( "%s\n", aucError );
      return ulRetCode;
      }


   ulFirstError = 0;
   while ( usEOF != TRUE )
      {
      UNSIGNED32 ulError;

      if ( ulFirstError == 0 )
         {
         // Attempt to get the first severe error code
         ulRetCode = (*gpfnAdsGetLong)( hResults, "ErrorCode", (SIGNED32*)&ulError );
         if ( ulRetCode != AE_SUCCESS )
            {
            usErrorLen = sizeof( aucError );
            (*gpfnAdsGetLastError)( &ulRetCode, aucError, &usErrorLen );
            printf( "%s\n", aucError );
            return ulRetCode;
            }

         ulBackupError = ulError;
         }

      ulRetCode = (*gpfnAdsGetLong)( hResults, "Severity", (SIGNED32*)&ulSeverity );
      if ( ulRetCode != AE_SUCCESS )
         {
         usErrorLen = sizeof( aucError );
         (*gpfnAdsGetLastError)( &ulRetCode, aucError, &usErrorLen );
         printf( "%s\n", aucError );
         return ulRetCode;
         }

      if ( ulSeverity >= 5 )
         ulErrors++;

      ulRetCode = (*gpfnAdsSkip)( hResults, 1 );
      if ( ulRetCode != AE_SUCCESS )
         {
         usErrorLen = sizeof( aucError );
         (*gpfnAdsGetLastError)( &ulRetCode, aucError, &usErrorLen );
         printf( "%s\n", aucError );
         return ulRetCode;
         }


      ulRetCode = (*gpfnAdsAtEOF)( hResults, &usEOF );
      if ( ulRetCode != AE_SUCCESS )
         {
         usErrorLen = sizeof( aucError );
         (*gpfnAdsGetLastError)( &ulRetCode, aucError, &usErrorLen );
         printf( "%s\n", aucError );
         return ulRetCode;
         }

      }

   printf( "Backup v%s - %d error(s), log table %s\n", strstr( gpcIdAxsRevisionStr, " " ) + 1, ulErrors, gpucOutputFile  );

   // Return the severe error we found in the top record
   return ulBackupError;
} /* CheckForBackupError */



/*******************************************************************************
* Module      : CreateSQLText
* Created     : 6/23/2005
* Last Mod    :
* Global Vars :
* Return      : AE_ALLOCATION_FAILED or AE_SUCCESS
* Description : Allocate memory and create the SQL statment text for executing
*               the backup or restore query.
* Notes       : This function determines the allocation size based on the length
*               of the current SQL script text.
********************************************************************************/
UNSIGNED32 CreateSQLText(  ADSHANDLE  hStmt )
{
   UNSIGNED8  *pucNullField = "";
   UNSIGNED32 ulRetCode;
   UNSIGNED8  *pucPassword = ( gpucPassword ? gpucPassword : (UNSIGNED8*)"" );
   UNSIGNED8  *pucOptions = ( gpucOptions ? gpucOptions : (UNSIGNED8*)"" );
   UNSIGNED32 ulAllocationSize;
   UNSIGNED8  *pucSQLText = NULL;             //Pointer to the SQL script


   //A pointer to the SQL for database Restore
   UNSIGNED8 *pucDBRestoreSQL = "DECLARE cur Cursor, iRecCount Integer;\n" \
      "DECLARE @SourcePath String, @Password String, @DestinationPath String, @Options String, @Severity Integer;\n" \
      "@SourcePath = :SourcePath;\n" \
      "@Password = :Password;\n"\
      "@DestinationPath = :DestinationPath;\n" \
      "@Options = :Options;\n" \
      "@Severity = :Severity;\n" \
      "OPEN cur AS EXECUTE PROCEDURE sp_RestoreDatabase( @SourcePath, @Password, @DestinationPath, @Options );\n";

   //A pointer to the SQL for Free Table Restore
   UNSIGNED8 *pucFTableRestoreSQL = "DECLARE cur Cursor, iRecCount Integer;\n" \
      "DECLARE @SourcePath String, @DestinationPath String, @Options String, @Password String, @Severity Integer;\n"\
      "@SourcePath = :SourcePath;\n" \
      "@DestinationPath = :DestinationPath;\n" \
      "@Password = :Password;\n"\
      "@Options = :Options;\n" \
      "@Severity = :Severity;\n"\
      "OPEN cur AS EXECUTE PROCEDURE sp_RestoreFreeTables( @SourcePath, @DestinationPath, @Options, @Password );\n";

   //A pointer to the SQL for Database Backup
   UNSIGNED8 *pucDBbackupSQL = "DECLARE cur Cursor, iRecCount Integer;\n" \
      "DECLARE @DestinationPath String, @Options String, @Severity Integer;\n"\
      "@DestinationPath = :DestinationPath;\n"\
      "@Options = :Options;\n"\
      "@Severity = :Severity;\n"\
      "OPEN cur AS EXECUTE PROCEDURE sp_BackupDatabase( @DestinationPath, @Options );\n";

   //A pointer to the SQL for Free Table Backup
   UNSIGNED8 *pucFTablebackupSQL = "DECLARE cur Cursor, iRecCount Integer;\n" \
      "DECLARE @SourcePath String, @FileMask String, @DestinationPath String, @Options String, @Password String, @Severity Integer;\n"\
      "@SourcePath = :SourcePath;\n"\
      "@FileMask = :FileMask;\n"\
      "@DestinationPath = :DestinationPath;\n"\
      "@Password = :Password;\n"\
      "@Options = :Options;\n"\
      "@Severity = :Severity;\n"\
      "OPEN cur AS EXECUTE PROCEDURE sp_BackupFreeTables(@SourcePath, @FileMask, @DestinationPath, @Options, @Password);\n";

   // A pointer to the SQL for create and insert to the output file table
   UNSIGNED8 *pucOutputFileTableSQL = "CREATE TABLE [%s] ( [Severity] Integer%s, [ErrorCode] Integer%s, [ErrorMsg] Memo%s,"\
      "[TableName] Memo%s, [MoreInfo] Memo%s, [SourceFile] char(32)%s, " \
      "[SourceLine] Integer%s ) AS FREE TABLE;\n" \
      "iRecCount = 0;\n" \
      "WHILE FETCH cur DO " \
      "INSERT INTO [%s] VALUES( cur.[Severity], cur.[Error Code], cur.[Error Message], "\
      "cur.[Table Name], cur.[Additional Info], cur.[Source File], cur.[Source Line] );\n" \
      "iRecCount = iRecCount + 1;\n" \
      "END;\n" \
      "CLOSE cur;\n";

   // A pointer to the SQL for Dump Empty Output
   UNSIGNED8 *pucDumpEmptyOutputSQL = "IF iRecCount = 0 THEN DROP TABLE [%s]; ENDIF;\n";

   // A pointer to the SQL for getting output
   UNSIGNED8 *pucGetOutputSQL = "IF iRecCount > 0 THEN SELECT [Severity], [ErrorCode] FROM [%s] WHERE [Severity] >= @Severity; ENDIF;\n";

   //Calculate the allocation size for the SQL script
   if ( gucRestore )
      {
      // Restore
      if ( gucDatabaseMode )
         // Database Restore
         ulAllocationSize = strlen( pucDBRestoreSQL );
      else
         //Free Table Restore
         ulAllocationSize = strlen( pucFTableRestoreSQL );
      }
   else
      {
      //Backup
      if ( gucDatabaseMode )
         // Database Backup
         ulAllocationSize = strlen( pucDBbackupSQL );
      else
         //Free Table Backup
         ulAllocationSize = strlen( pucFTablebackupSQL );
      }
   ulAllocationSize += strlen( pucOutputFileTableSQL )
                    + strlen( " NULL" )*7 //in case pucNullField = " NULL"
                    + strlen( pucDumpEmptyOutputSQL )
                    + strlen( pucGetOutputSQL )
                    + strlen( gpucOutputFile )*4; //OutputFile showed up in pucDumpEmptyOutputSQL,
                                                //pucGetOutputSQL and 2 times in pucOutputFileTableSQL

   // Allocate enough memory to contain the necessary SQL text
   pucSQLText = malloc( ulAllocationSize + 1 );
   if ( pucSQLText == NULL )
      {
      printf( "Memory allocation failure.\n" );
      return AE_ALLOCATION_FAILED;
      }

   // Finally, complete the SQL script. Copying each SQL into pucSQLText
   if ( gucRestore )
      {
      // Restore
      if ( gucDatabaseMode )
         strcpy( pucSQLText, pucDBRestoreSQL );
      else
         strcpy( pucSQLText, pucFTableRestoreSQL );
      }
   else
      {
      if ( gucDatabaseMode )
         strcpy( pucSQLText, pucDBbackupSQL );
      else
         strcpy( pucSQLText, pucFTablebackupSQL );
      }

   // If the table type is VFP, then make the fields nullable so that any null
   // values from the procedure execution can be inserted into it without error.
   if ( gusTableType == ADS_VFP )
      pucNullField = " NULL";   // the fields will be nullable

   //append pucOutputFileTableSQL to pucSQLText
   sprintf( pucSQLText + strlen( pucSQLText ),
            pucOutputFileTableSQL,
            gpucOutputFile,
            pucNullField,
            pucNullField,
            pucNullField,
            pucNullField,
            pucNullField,
            pucNullField,
            pucNullField,
            gpucOutputFile);


   if ( gucDumpEmptyOutput == FALSE )
      sprintf( pucSQLText + strlen( pucSQLText ), pucDumpEmptyOutputSQL, gpucOutputFile );

   sprintf( pucSQLText + strlen( pucSQLText ), pucGetOutputSQL, gpucOutputFile );

   //After all the concatenation, exe the prepareed SQL script, if failed return the error code and exit function
   ulRetCode = (*gpfnAdsPrepareSQL)( hStmt, pucSQLText );
   if( ulRetCode != AE_SUCCESS )
      return ulRetCode;

   if ( pucSQLText )
      free( pucSQLText );

   // set the common parameters
   ulRetCode = gpfnAdsSetString( hStmt, "DestinationPath", gpucDestinationPath, strlen( gpucDestinationPath ) );
   if( ulRetCode != AE_SUCCESS )
      return ulRetCode;

   ulRetCode = gpfnAdsSetString( hStmt, "Options", pucOptions, strlen( pucOptions ) );
   if( ulRetCode != AE_SUCCESS )
      return ulRetCode;

   ulRetCode = gpfnAdsSetLong( hStmt, "Severity", glSeverity );
   if( ulRetCode != AE_SUCCESS )
      return ulRetCode;

   if ( gucRestore )
      {
      ulRetCode = gpfnAdsSetString( hStmt, "SourcePath", gpucSourcePath, strlen( gpucSourcePath ) );
      if( ulRetCode != AE_SUCCESS )
         return ulRetCode;

      ulRetCode = gpfnAdsSetString( hStmt, "Password", pucPassword, strlen( pucPassword ) );
      if( ulRetCode != AE_SUCCESS )
         return ulRetCode;
      }
   else
      {
      if ( !gucDatabaseMode )
         {
         ulRetCode = gpfnAdsSetString( hStmt, "SourcePath", gpucSourcePath, strlen( gpucSourcePath ) );
         if( ulRetCode != AE_SUCCESS )
            return ulRetCode;

         ulRetCode = gpfnAdsSetString( hStmt, "FileMask", gpucFileMask, strlen( gpucFileMask ) );
         if( ulRetCode != AE_SUCCESS )
            return ulRetCode;

         ulRetCode = gpfnAdsSetString( hStmt, "Password", pucPassword, strlen( pucPassword ) );
         if( ulRetCode != AE_SUCCESS )
            return ulRetCode;
         }
      }

   return AE_SUCCESS;
} /* CreateSQLText */



#define CHECK_ADS_ERROR( ulErr )                                  \
   if ( ulErr != AE_SUCCESS )                                     \
      {                                                           \
      usErrorLen = sizeof( aucError );                            \
      (*gpfnAdsGetLastError)( &ulRetCode, aucError, &usErrorLen );\
      printf( "%s\n", aucError );                                 \
      goto mainExit;                                              \
      }




/*******************************************************************************
* Module      : main
* Created     : 6/23/2005
* Last Mod    :
* Return      : Errors >= minimum severity if found (default severity = 1, LOW)
* Description : Main function of the AdsBackup utility
* Notes       :
********************************************************************************/
int main( int argc, char *argv[] )
{
   HMODULE    hAce32 = 0;
   UNSIGNED32 ulRetCode;
   ADSHANDLE  hConnect = 0;
   ADSHANDLE  hResults;
   ADSHANDLE  hStmt;
   UNSIGNED8  aucError[ ADS_MAX_ERROR_LEN + 1 ];
   UNSIGNED16 usErrorLen;
   UNSIGNED8  aucOutputTable[ ADS_MAX_PATH + 1 ];
   UNSIGNED8  aucUserName[ ADS_MAX_USER_NAME + 1 ];

   // A minimum of two arguments are required to do anything (not including the exe name)
   if ( argc < 3 )
      PrintUsage();

#ifdef __linux__
   /* For Linux, configure the terminal for use with the callback function */
   set_keypress();
#endif

   ulRetCode = ParseOptions( argc, argv );
   if ( ulRetCode )
      goto mainExit;

   // Load ACE32.DLL dynamically
   hAce32 = LoadLibrary( ACE32_MODULE_NAME );
   if ( hAce32 == NULL )
      {
      ulRetCode = GetLastError();
      printf( "Failed to load %s.  Last error: %d\n", ACE32_MODULE_NAME, ulRetCode );
      goto mainExit;
      }

   /* Get the required ACE API function pointers */
   ulRetCode = GetAdsEntrypoints( hAce32 );
   if ( ulRetCode != AE_SUCCESS )
      {
      printf( "Failed to get all required entrypoints.  Last error: %d\n", ulRetCode );
      goto mainExit;
      }


   /*
    * Now that we have the entrypoints, make sure that the they did not specify a
    * VFP collation language if we are not using v9.x or greater DLLs.
    */
   if ( gpfnAdsStmtSetTableCollation == NULL )
      {
      // We don't have the API - make sure they specified ANSI or OEM as the type.
      if (( gusCharType != ADS_ANSI ) && ( gusCharType != ADS_OEM ))
         {
         // We did not recognize the character type.  We cannot set a VFP collation.
         printf( "Invalid character type: %s\n", gpucCollation );
         goto mainExit;
         }
      }

   aucUserName[0] = 0x00;
   if ( gucDatabaseMode )
      {
      if ( gpucUserName )
         strncpy( aucUserName, gpucUserName, ADS_MAX_USER_NAME );
      else
         strcpy( aucUserName, "ADSSYS" );
      }

   // Get a connection to the Advantage server
   if ( gpucConnectionStr )
      ulRetCode = (*gpfnAdsConnect101)( gpucConnectionStr, NULL, &hConnect );
   else if ( gpucConnectionPath )
      ulRetCode = (*gpfnAdsConnect60)( gpucConnectionPath, ADS_REMOTE_SERVER, aucUserName,
                                       gucDatabaseMode ? gpucPassword : NULL, ADS_DEFAULT,
                                       &hConnect );
   else
      ulRetCode = (*gpfnAdsConnect60)( gpucSourcePath, ADS_REMOTE_SERVER, aucUserName,
                                       gucDatabaseMode ? gpucPassword : NULL, ADS_DEFAULT,
                                       &hConnect );

   CHECK_ADS_ERROR( ulRetCode );

   // Create an SQL statement to work with
   ulRetCode = (*gpfnAdsCreateSQLStatement)( hConnect, &hStmt );
   CHECK_ADS_ERROR( ulRetCode );

   /* Set the table type if it isn't the default */
   if ( gusTableType != ADS_ADT )
      {
      ulRetCode = (*gpfnAdsStmtSetTableType)( hStmt, gusTableType );
      CHECK_ADS_ERROR( ulRetCode );
      }

   /*
    * Set the character type (collation language) if it isn't the default (and
    * if the API exists (it was added in v9.0)
    */
   if (( gpucCollation != NULL ) && ( gpfnAdsStmtSetTableCollation != NULL ))
      {
      ulRetCode = (*gpfnAdsStmtSetTableCollation)( hStmt, gpucCollation );
      CHECK_ADS_ERROR( ulRetCode );
      }
   else if ( gusCharType != ADS_ANSI )
      {
      ulRetCode = (*gpfnAdsStmtSetTableCharType)( hStmt, gusCharType );
      CHECK_ADS_ERROR( ulRetCode );
      }


   /* Set the lock type if it isn't the default */
   if ( gusLockType != ADS_PROPRIETARY_LOCKING )
      {
      ulRetCode = (*gpfnAdsStmtSetTableLockType)( hStmt, gusLockType );
      CHECK_ADS_ERROR( ulRetCode );
      }

   /* Set the rights checking flag if it isn't the default */
   if ( gusRightsChecking != ADS_CHECKRIGHTS )
      {
      ulRetCode = (*gpfnAdsStmtSetTableRights)( hStmt, gusRightsChecking );
      CHECK_ADS_ERROR( ulRetCode );
      }

   // Register a callback function to provide percentage information and abort ability
   ulRetCode = (*gpfnAdsRegisterCallbackFunction)( ShowPercentage, 1 );
   CHECK_ADS_ERROR( ulRetCode );

   // If no output file was specified, create a new output file path
   if ( gpucOutputFile == NULL )
      gpucOutputFile = GetOutputTableName( aucOutputTable );

   // If the output file is still NULL, an error has occurred
   if ( gpucOutputFile == NULL )
      goto mainExit;

   /* Allocate memory and create the SQL statement to perform the backup or restore */
   ulRetCode = CreateSQLText( hStmt );
   if ( ulRetCode )
      goto mainExit;


   // Begin the backup or restore
   ulRetCode = gpfnAdsExecuteSQL( hStmt, &hResults );

   CHECK_ADS_ERROR( ulRetCode );

   if ( gucRestore )
      printf( "\rPercent Complete: 100%%                     \nRestore Complete\n\n" );
   else
      printf( "\rPercent Complete: 100%%                     \nBackup Complete\n\n" );

   // Look for a severe enough error in the output table
   if ( ulRetCode == AE_SUCCESS )
      ulRetCode = CheckForBackupError( hResults );

mainExit:
   if ( hConnect )
      (*gpfnAdsDisconnect)( hConnect );

   if ( hAce32 )
      FreeLibrary( hAce32 );

   if ( gpucOptions )
      free( gpucOptions );

   if ( gpucConnectionStr )
      free( gpucConnectionStr );

#ifdef __linux__
   /* For Linux, restore the original terminal configuration */
   reset_keypress();
#endif

   return ulRetCode;

} /* main */

