using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Advantage.Data.Provider
{
    public class ConfigWizard
    {
        public ConnectionDlg mConnectionDlg;
        public QueryBuildDlg mQueryBuildDlg;
        public QueryTypeDlg mQueryTypeDlg;
        public ResultsDlg mResultsDlg;
        public StoredProcDlg mStoredProcDlg;
        public TableDirectDlg mTableDirectDlg;
        public WelcomeDlg mWelcomeDlg;
        private string mstrConnString = "";
        private AdsCommand mSelectCommand = new AdsCommand();
        private AdsCommand mInsertCommand = new AdsCommand();
        private AdsCommand mDeleteCommand = new AdsCommand();
        private AdsCommand mUpdateCommand = new AdsCommand();
        private DataTableMapping mTableMapping = new DataTableMapping();
        private bool mbOptConcurrency = true;
        private bool mbGenerateCommands = true;
        private bool mbPrimaryKeyRequired;
        private bool mbUseRowversion = true;
        private bool mbRefreshDataset = true;
        private QueryTypes mQueryType = QueryTypes.SqlQuery;
        private string mAdapterName = "";
        private Size mFormSize;
        private Point mFormLocation;
        public static string MessageBoxTitle = "Data Adapter Configuration Wizard";
        public static string DotNetRegKey = "Software\\Extended Systems\\Advantage .NET Data Provider\\";
        public static string DesignerRegKey = "Software\\Extended Systems\\Advantage .NET Data Provider\\Designer";
        public static string HelpLocationKey = "HelpLocation";
        public WizardDialogs mNextDlg = WizardDialogs.Welcome;

        public Size FormSize
        {
            get => mFormSize;
            set => mFormSize = value;
        }

        public Point FormLocation
        {
            get => mFormLocation;
            set => mFormLocation = value;
        }

        public ConfigWizard()
        {
            mConnectionDlg = new ConnectionDlg(this);
            mQueryBuildDlg = new QueryBuildDlg(this);
            mQueryTypeDlg = new QueryTypeDlg(this);
            mResultsDlg = new ResultsDlg(this);
            mStoredProcDlg = new StoredProcDlg(this);
            mTableDirectDlg = new TableDirectDlg(this);
            mWelcomeDlg = new WelcomeDlg(this);
        }

        private void ConfigForm_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            var str1 = "AdvantageDesignerHelp.htm";
            try
            {
                var str2 = typeof(AdsDataAdapterDesigner).Assembly.CodeBase.ToLower().Replace("file:///", "");
                var num = str2.LastIndexOf('/');
                var path = str2.Remove(num + 1, str2.Length - num - 1) + str1;
                if (!File.Exists(path))
                {
                    try
                    {
                        var registryKey = Registry.CurrentUser.OpenSubKey(DotNetRegKey);
                        if (registryKey != null)
                            path = (string)registryKey.GetValue(HelpLocationKey, "") + "\\" + str1;
                    }
                    catch
                    {
                    }
                }

                if (!File.Exists(path))
                    path = str1;
                new Process
                {
                    StartInfo =
                    {
                        FileName = path,
                        UseShellExecute = true
                    }
                }.Start();
                hlpevent.Handled = true;
            }
            catch
            {
                var num = (int)MessageBox.Show("Could not load file " + str1, "File not found");
            }
        }

        public bool ReadyToFinish
        {
            get
            {
                var readyToFinish = false;
                if (mConnectionDlg.Ready)
                {
                    switch (mQueryType)
                    {
                        case QueryTypes.SqlQuery:
                            readyToFinish = mQueryBuildDlg.Ready;
                            break;
                        case QueryTypes.StoredProc:
                            readyToFinish = mStoredProcDlg.Ready;
                            break;
                        case QueryTypes.TableDirect:
                            readyToFinish = mTableDirectDlg.Ready;
                            break;
                    }
                }

                return readyToFinish;
            }
        }

        public string ConnectionString
        {
            get => mstrConnString;
            set => mstrConnString = value;
        }

        public AdsCommand SelectCommand
        {
            get => mSelectCommand;
            set => mSelectCommand = value;
        }

        public AdsCommand InsertCommand
        {
            get => mInsertCommand;
            set => mInsertCommand = value;
        }

        public AdsCommand UpdateCommand
        {
            get => mUpdateCommand;
            set => mUpdateCommand = value;
        }

        public AdsCommand DeleteCommand
        {
            get => mDeleteCommand;
            set => mDeleteCommand = value;
        }

        public bool OptimisticConcurrency
        {
            get => mbOptConcurrency;
            set => mbOptConcurrency = value;
        }

        public bool GenerateCommands
        {
            get => mbGenerateCommands;
            set => mbGenerateCommands = value;
        }

        public QueryTypes QueryType
        {
            get => mQueryType;
            set => mQueryType = value;
        }

        public bool PrimaryKeyRequired
        {
            get => mbPrimaryKeyRequired;
            set => mbPrimaryKeyRequired = value;
        }

        public bool RefreshDataset
        {
            get => mbRefreshDataset;
            set => mbRefreshDataset = value;
        }

        public DataTableMapping TableMapping => mTableMapping;

        public string AdapterName
        {
            get => mAdapterName;
            set => mAdapterName = value;
        }

        public bool UseRowversion
        {
            get => mbUseRowversion;
            set => mbUseRowversion = value;
        }

        public enum QueryTypes
        {
            SqlQuery = 1,
            StoredProc = 2,
            TableDirect = 3,
        }

        public enum WizardDialogs
        {
            Welcome = 1,
            Connection = 2,
            QueryType = 3,
            QueryBuild = 4,
            StoredProc = 5,
            TableDirect = 6,
            Results = 7,
            Finish = 8,
            AllDone = 9,
        }
    }
}