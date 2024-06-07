using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using AdvantageClientEngine;

namespace Advantage.Data.Provider
{
    public class BuildConnString : Form
    {
        private const string PropTableType = "TableType";
        private const string PropTableTypeLong = "Advantage Table Type";
        private const string PropCharType = "CharType";
        private const string PropCharTypeLong = "Advantage Character Data Type";
        private const string PropUnicodeCollation = "UnicodeCollation";
        private const string PropLockType = "LockMode";
        private const string PropLockTypeLong = "Advantage Locking Mode";
        private const string PropCheckRights = "SecurityMode";
        private const string PropCheckRightsLong = "Advantage Security Mode";
        private const string PropServerType = "ServerType";
        private const string PropServerTypeLong = "Advantage Server Type";
        private const string PropShowDeleted = "ShowDeleted";
        private const string PropShowDeletedLong = "Show Deleted Records in DBF Tables with Advantage";
        private const string PropIncUserCount = "IncrementUserCount";
        private const string PropIncUserCountLong = "Increment User Count";
        private const string PropStoredProcConn = "StoredProcedureConnection";
        private const string PropStoredProcConnLong = "Stored Procedure Connection";
        private const string PropEncryptionPassword = "EncryptionPassword";
        private const string PropEncryptionPasswordLong = "Advantage Encryption Password";
        private const string PropDbfsUseNulls = "DbfsUseNulls";
        private const string PropDbfsUseNullsLong = "Use NULL values in DBF Tables with Advantage";
        private const string PropFilterOptions = "FilterOptions";
        private const string PropFilterOptionsLong = "Advantage Filter Options";
        private const string PropTrimTrailingSpaces = "TrimTrailingSpaces";
        private const string PropTrimTrailingSpacesLong = "Trim Trailing Spaces";
        private const string PropCompression = "Compression";
        private const string PropCompressionLong = "Advantage Compression";
        private const string PropConnectionHandle = "ConnectionHandle";
        private const string PropShared = "Shared";
        private const string PropReadOnly = "ReadOnly";
        private const string PropDataSource = "Data Source";
        private const string PropInitialCatalog = "Initial Catalog";
        private const string PropUserID = "User ID";
        private const string PropPassword = "Password";
        private const string PropPooling = "Pooling";
        private const string PropMinPoolSize = "Min Pool Size";
        private const string PropMaxPoolSize = "Max Pool Size";
        private const string PropConnectionReset = "Connection Reset";
        private const string PropConnectionLifetime = "Connection Lifetime";
        private const string PropConnectTimeout = "Connect Timeout";
        private const string PropTransScopeEnlist = "Enlist";
        private const string PropCommType = "CommType";
        private const string AdsPrefix = "ADS_";
        private const string AdsCompressPrefix = "ADS_COMPRESS_";
        private const string AdsLockingSuffix = "_LOCKING";
        private const string AdsServerSuffix = "_SERVER";
        private Label mDataSourceLabel;
        private TextBox mDataSourceText;
        private TextBox mUserNameText;
        private Label mUserNameLabel;
        private TextBox mPasswordText;
        private Label mPasswordLabel;
        private Button mOkButton;
        private Button mCancelButton;
        private string mConnectionString;
        private DataTable mDataTable;
        private DataGridEx mDataGrid;
        private GridComboBox mGridComboBox;
        private Label mPropertiesLabel;
        private GridTextBox mGridTextBox;
        private DataGridTableStyle mTableStyle;
        private DataGridTextBoxColumn mNameTextBoxColumn;
        private DataGridTextBoxColumn mValueTextBoxColumn;
        private Button mBrowse;
        private OpenFileDialog mOpenFileDlg;
        private Label mPathLabel;
        private RadioButton mPathRadio;
        private RadioButton mDictRadio;
        private SourceType mType = SourceType.Path;
        private string mstrPath = "";
        private string mstrDict = "";
        private bool mbIntTextOnly;
        private Timer mTimer;
        private IContainer components;

        public BuildConnString(string connString)
        {
            InitializeComponent();
            InitGrid();
            mConnectionString = connString;
            InitializeParameters();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            mDataSourceLabel = new Label();
            mDataSourceText = new TextBox();
            mUserNameText = new TextBox();
            mUserNameLabel = new Label();
            mPasswordText = new TextBox();
            mPasswordLabel = new Label();
            mOkButton = new Button();
            mCancelButton = new Button();
            mPropertiesLabel = new Label();
            mDataGrid = new DataGridEx();
            mTableStyle = new DataGridTableStyle();
            mNameTextBoxColumn = new DataGridTextBoxColumn();
            mValueTextBoxColumn = new DataGridTextBoxColumn();
            mGridComboBox = new GridComboBox();
            mGridTextBox = new GridTextBox();
            mBrowse = new Button();
            mOpenFileDlg = new OpenFileDialog();
            mPathLabel = new Label();
            mPathRadio = new RadioButton();
            mDictRadio = new RadioButton();
            mTimer = new Timer(components);
            mDataGrid.BeginInit();
            SuspendLayout();
            mDataSourceLabel.Location = new Point(8, 8);
            mDataSourceLabel.Name = "mDataSourceLabel";
            mDataSourceLabel.Size = new Size(72, 16);
            mDataSourceLabel.TabIndex = 0;
            mDataSourceLabel.Text = "Data Source:";
            mDataSourceText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mDataSourceText.Location = new Point(88, 40);
            mDataSourceText.Name = "mDataSourceText";
            mDataSourceText.Size = new Size(184, 20);
            mDataSourceText.TabIndex = 4;
            mDataSourceText.Text = "";
            mUserNameText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mUserNameText.Location = new Point(88, 64);
            mUserNameText.Name = "mUserNameText";
            mUserNameText.Size = new Size(208, 20);
            mUserNameText.TabIndex = 7;
            mUserNameText.Text = "";
            mUserNameText.Enter += mUserNameText_Enter;
            mUserNameLabel.Location = new Point(8, 64);
            mUserNameLabel.Name = "mUserNameLabel";
            mUserNameLabel.Size = new Size(72, 16);
            mUserNameLabel.TabIndex = 6;
            mUserNameLabel.Text = "&User Name:";
            mPasswordText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mPasswordText.HideSelection = false;
            mPasswordText.Location = new Point(88, 88);
            mPasswordText.Name = "mPasswordText";
            mPasswordText.PasswordChar = '*';
            mPasswordText.Size = new Size(208, 20);
            mPasswordText.TabIndex = 9;
            mPasswordText.Text = "";
            mPasswordLabel.Location = new Point(8, 88);
            mPasswordLabel.Name = "mPasswordLabel";
            mPasswordLabel.Size = new Size(72, 16);
            mPasswordLabel.TabIndex = 8;
            mPasswordLabel.Text = "&Password:";
            mOkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mOkButton.Location = new Point(160, 317);
            mOkButton.Name = "mOkButton";
            mOkButton.Size = new Size(64, 23);
            mOkButton.TabIndex = 15;
            mOkButton.Text = "OK";
            mOkButton.Click += mOkButton_Click;
            mCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mCancelButton.DialogResult = DialogResult.Cancel;
            mCancelButton.Location = new Point(232, 317);
            mCancelButton.Name = "mCancelButton";
            mCancelButton.Size = new Size(64, 23);
            mCancelButton.TabIndex = 16;
            mCancelButton.Text = "Cancel";
            mPropertiesLabel.Location = new Point(8, 120);
            mPropertiesLabel.Name = "mPropertiesLabel";
            mPropertiesLabel.Size = new Size(72, 16);
            mPropertiesLabel.TabIndex = 10;
            mPropertiesLabel.Text = "P&roperties:";
            mDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mDataGrid.CaptionVisible = false;
            mDataGrid.DataMember = "";
            mDataGrid.HeaderForeColor = SystemColors.ControlText;
            mDataGrid.Location = new Point(8, 136);
            mDataGrid.Name = "mDataGrid";
            mDataGrid.ReadOnly = true;
            mDataGrid.Size = new Size(288, 168);
            mDataGrid.TabIndex = 11;
            mDataGrid.TableStyles.AddRange(new DataGridTableStyle[1]
            {
                mTableStyle
            });
            mDataGrid.Scroll += mDataGrid_Scroll;
            mDataGrid.CurrentCellChanged += mDataGrid_CurrentCellChanged;
            mDataGrid.Enter += mDataGrid_Enter;
            mTableStyle.DataGrid = mDataGrid;
            mTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[2]
            {
                mNameTextBoxColumn,
                mValueTextBoxColumn
            });
            mTableStyle.HeaderForeColor = SystemColors.ControlText;
            mTableStyle.MappingName = "Advantage Properties";
            mTableStyle.PreferredColumnWidth = 150;
            mTableStyle.PreferredRowHeight = 21;
            mTableStyle.ReadOnly = true;
            mTableStyle.RowHeadersVisible = false;
            mNameTextBoxColumn.Format = "";
            mNameTextBoxColumn.FormatInfo = null;
            mNameTextBoxColumn.HeaderText = "Name";
            mNameTextBoxColumn.MappingName = "Name";
            mNameTextBoxColumn.NullText = "";
            mNameTextBoxColumn.ReadOnly = true;
            mNameTextBoxColumn.Width = 150;
            mValueTextBoxColumn.Format = "";
            mValueTextBoxColumn.FormatInfo = null;
            mValueTextBoxColumn.HeaderText = "Value";
            mValueTextBoxColumn.MappingName = "Value";
            mValueTextBoxColumn.NullText = "";
            mValueTextBoxColumn.ReadOnly = true;
            mValueTextBoxColumn.Width = 150;
            mGridComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mGridComboBox.Location = new Point(232, 112);
            mGridComboBox.Name = "mGridComboBox";
            mGridComboBox.Size = new Size(32, 21);
            mGridComboBox.TabIndex = 12;
            mGridComboBox.TabStop = false;
            mGridComboBox.Visible = false;
            mGridComboBox.KeyDown += mGridComboBox_KeyDown;
            mGridComboBox.Leave += mGridComboBox_Leave;
            mGridTextBox.Location = new Point(272, 112);
            mGridTextBox.Name = "mGridTextBox";
            mGridTextBox.Size = new Size(24, 20);
            mGridTextBox.TabIndex = 13;
            mGridTextBox.TabStop = false;
            mGridTextBox.Text = "";
            mGridTextBox.Visible = false;
            mGridTextBox.KeyDown += mGridTextBox_KeyDown;
            mGridTextBox.Leave += mGridTextBox_Leave;
            mBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mBrowse.Location = new Point(272, 40);
            mBrowse.Name = "mBrowse";
            mBrowse.Size = new Size(24, 20);
            mBrowse.TabIndex = 5;
            mBrowse.Text = "...";
            mBrowse.Click += mBrowse_Click;
            mOpenFileDlg.DefaultExt = "ADD";
            mOpenFileDlg.Filter = "Advantage Data Dictionaries (*.ADD)|*.ADD";
            mOpenFileDlg.Title = "Select Advantage Data Dictionary";
            mOpenFileDlg.ValidateNames = false;
            mPathLabel.Location = new Point(8, 40);
            mPathLabel.Name = "mPathLabel";
            mPathLabel.Size = new Size(72, 16);
            mPathLabel.TabIndex = 3;
            mPathLabel.Text = "P&ath:";
            mPathRadio.Checked = true;
            mPathRadio.Location = new Point(88, 8);
            mPathRadio.Name = "mPathRadio";
            mPathRadio.Size = new Size(64, 16);
            mPathRadio.TabIndex = 1;
            mPathRadio.TabStop = true;
            mPathRadio.Text = "Pat&h";
            mPathRadio.CheckedChanged += mPathRadio_CheckedChanged;
            mDictRadio.Location = new Point(160, 8);
            mDictRadio.Name = "mDictRadio";
            mDictRadio.Size = new Size(112, 16);
            mDictRadio.TabIndex = 2;
            mDictRadio.Text = "&Data Dictionary";
            mDictRadio.CheckedChanged += mDictRadio_CheckedChanged;
            mTimer.Interval = 1;
            mTimer.Tick += mTimer_Elapsed;
            AcceptButton = mOkButton;
            AutoScaleBaseSize = new Size(5, 13);
            CancelButton = mCancelButton;
            ClientSize = new Size(304, 349);
            ControlBox = false;
            Controls.Add(mGridTextBox);
            Controls.Add(mPasswordText);
            Controls.Add(mUserNameText);
            Controls.Add(mDataSourceText);
            Controls.Add(mDictRadio);
            Controls.Add(mPathRadio);
            Controls.Add(mPathLabel);
            Controls.Add(mBrowse);
            Controls.Add(mGridComboBox);
            Controls.Add(mPropertiesLabel);
            Controls.Add(mCancelButton);
            Controls.Add(mOkButton);
            Controls.Add(mDataGrid);
            Controls.Add(mPasswordLabel);
            Controls.Add(mUserNameLabel);
            Controls.Add(mDataSourceLabel);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(312, 250);
            Name = nameof(BuildConnString);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Build Connection String";
            Resize += BuildConnString_Resize;
            Load += BuildConnString_Load;
            mDataGrid.EndInit();
            ResumeLayout(false);
        }

        private void InitGrid()
        {
            mDataTable = new DataTable();
            mDataTable.TableName = "Advantage Properties";
            mDataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            mDataTable.Columns.Add(new DataColumn("Value", typeof(string)));
            mDataTable.Rows.Add("TableType", "ADT");
            mDataTable.Rows.Add("CharType", "");
            mDataTable.Rows.Add("UnicodeCollation", "");
            mDataTable.Rows.Add("LockMode", "");
            mDataTable.Rows.Add("SecurityMode", "");
            mDataTable.Rows.Add("ServerType", "REMOTE");
            mDataTable.Rows.Add("ShowDeleted", "");
            mDataTable.Rows.Add("IncrementUserCount", "");
            mDataTable.Rows.Add("StoredProcedureConnection", "");
            mDataTable.Rows.Add("EncryptionPassword", "");
            mDataTable.Rows.Add("DbfsUseNulls", "");
            mDataTable.Rows.Add("FilterOptions", "");
            mDataTable.Rows.Add("TrimTrailingSpaces", "");
            mDataTable.Rows.Add("Compression", "");
            mDataTable.Rows.Add("Shared", "");
            mDataTable.Rows.Add("ReadOnly", "");
            mDataTable.Rows.Add("Initial Catalog", "");
            mDataTable.Rows.Add("Pooling", "");
            mDataTable.Rows.Add("Min Pool Size", "");
            mDataTable.Rows.Add("Max Pool Size", "");
            mDataTable.Rows.Add("Connection Reset", "");
            mDataTable.Rows.Add("Connection Lifetime", "");
            mDataTable.Rows.Add("Connect Timeout", "");
            mDataTable.Rows.Add("CommType", "");
            mDataTable.Rows.Add("Enlist", "");
            mDataTable.PrimaryKey = new DataColumn[1]
            {
                mDataTable.Columns[0]
            };
            mDataGrid.DataMember = "Advantage Properties";
            mDataGrid.DataSource = new DataView(mDataTable);
        }

        private void InitializeParameters()
        {
            var strConnect = mConnectionString.Trim().Trim();
            var keys = new object[1];
            while (strConnect.Length > 0)
            {
                string strProp;
                string strValue;
                GetPropPair(ref strConnect, out strProp, out strValue);
                if (strValue.IndexOf("ADS_") == 0)
                {
                    if (strValue.IndexOf("ADS_COMPRESS_") == 0)
                        strValue.Remove(0, 13);
                    else
                        strValue.Remove(0, 4);
                    if (strValue.EndsWith("_SERVER"))
                        strValue.Remove(strValue.Length - 7, 7);
                    else if (strValue.EndsWith("_LOCKING"))
                        strValue.Remove(strValue.Length - 8, 8);
                }

                switch (strProp)
                {
                    case "Data Source":
                        if (strValue.ToUpper().EndsWith(".ADD"))
                            mDictRadio.Checked = true;
                        mDataSourceText.Text = strValue;
                        continue;
                    case "User ID":
                        mUserNameText.Text = strValue;
                        continue;
                    case "Password":
                        mPasswordText.Text = strValue;
                        continue;
                    default:
                        keys[0] = strProp;
                        var dataRow1 = mDataTable.Rows.Find(keys);
                        if (dataRow1 != null)
                        {
                            dataRow1[1] = strValue;
                            continue;
                        }

                        switch (strProp)
                        {
                            case "Advantage Table Type":
                                strProp = "TableType";
                                break;
                            case "Advantage Character Data Type":
                                strProp = "CharType";
                                break;
                            case "Advantage Locking Mode":
                                strProp = "LockMode";
                                break;
                            case "Advantage Security Mode":
                                strProp = "SecurityMode";
                                break;
                            case "Advantage Server Type":
                                strProp = "ServerType";
                                break;
                            case "Show Deleted Records in DBF Tables with Advantage":
                                strProp = "ShowDeleted";
                                break;
                            case "Increment User Count":
                                strProp = "IncrementUserCount";
                                break;
                            case "Stored Procedure Connection":
                                strProp = "StoredProcedureConnection";
                                break;
                            case "Advantage Encryption Password":
                                strProp = "EncryptionPassword";
                                break;
                            case "Use NULL values in DBF Tables with Advantage":
                                strProp = "DbfsUseNulls";
                                break;
                            case "Advantage Filter Options":
                                strProp = "FilterOptions";
                                break;
                            case "Trim Trailing Spaces":
                                strProp = "TrimTrailingSpaces";
                                break;
                            case "Advantage Compression":
                                strProp = "Compression";
                                break;
                            default:
                                continue;
                        }

                        keys[0] = strProp;
                        var dataRow2 = mDataTable.Rows.Find(keys);
                        if (dataRow2 != null)
                        {
                            dataRow2[1] = strValue;
                        }

                        continue;
                }
            }
        }

        public static void GetPropPair(ref string strConnect, out string strProp, out string strValue)
        {
            var minValue = char.MinValue;
            var length = strConnect.IndexOf('=');
            if (length == -1)
                throw new ArgumentException("Separator '=' not found while parsing connection string.");
            strProp = strConnect.Substring(0, length).Trim();
            strConnect = strConnect.Remove(0, length + 1).Trim();
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

        public bool UpdateConnectionString()
        {
            if (mDataSourceText.Text.Length == 0)
            {
                var num = (int)MessageBox.Show("Data Source is empty.", ConfigWizard.MessageBoxTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }

            mConnectionString = "Data Source = " + mDataSourceText.Text;
            if (mUserNameText.Text.Length > 0)
            {
                var buildConnString = this;
                buildConnString.mConnectionString =
                    buildConnString.mConnectionString + "; User ID = " + mUserNameText.Text;
            }

            if (mPasswordText.Text.Length > 0)
            {
                if (mUserNameText.Text.Length == 0)
                {
                    var num = (int)MessageBox.Show("A password was given, but User Name is empty.",
                        ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    mUserNameText.Focus();
                    return false;
                }

                if (MessageBox.Show(
                        "The Password is saved as clear text and is readable in the source code and the complete assembly.",
                        ConfigWizard.MessageBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) ==
                    DialogResult.Cancel)
                    return false;
                var buildConnString = this;
                buildConnString.mConnectionString =
                    buildConnString.mConnectionString + "; Password = " + mPasswordText.Text;
            }

            foreach (DataRow row in (InternalDataCollectionBase)mDataTable.Rows)
            {
                var str = row[1].ToString();
                if (str.Length > 0)
                {
                    var buildConnString = this;
                    buildConnString.mConnectionString =
                        buildConnString.mConnectionString + "; " + row[0] + " = " + str;
                }
            }

            return true;
        }

        private void mOkButton_Click(object sender, EventArgs e)
        {
            if (!UpdateConnectionString())
                return;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BuildConnString_Resize(object sender, EventArgs e)
        {
            mGridComboBox.Hide();
            mGridTextBox.Hide();
            var width1 = mTableStyle.GridColumnStyles[0].Width;
            var width2 = mTableStyle.GridColumnStyles[1].Width;
            var num = width1 + (mDataGrid.InsideWidth - 4 - width1 - width2) / 2;
            mTableStyle.GridColumnStyles[0].Width = num;
            mTableStyle.GridColumnStyles[1].Width = mDataGrid.InsideWidth - num - 4;
        }

        private void mDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            string.Format("mDataGrid_CurrentCellChanged: {0}, {1}", mDataGrid.CurrentCell.ColumnNumber,
                mDataTable.Rows[mDataGrid.CurrentRowIndex][0]);
            if (mDataGrid.CurrentCell.ColumnNumber != 1 ||
                mDataGrid.CurrentCell.RowNumber >= mDataTable.Rows.Count)
            {
                mGridComboBox.Visible = false;
                mGridTextBox.Visible = false;
            }
            else
            {
                mbIntTextOnly = false;
                mGridComboBox.Items.Clear();
                mGridComboBox.Items.Add("");
                bool flag;
                switch (mDataTable.Rows[mDataGrid.CurrentRowIndex][0].ToString())
                {
                    case "EncryptionPassword":
                    case "Initial Catalog":
                        flag = true;
                        break;
                    case "Min Pool Size":
                    case "Max Pool Size":
                    case "Connection Lifetime":
                    case "Connect Timeout":
                        mbIntTextOnly = true;
                        flag = true;
                        break;
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
                        flag = false;
                        mGridComboBox.Items.Add("TRUE");
                        mGridComboBox.Items.Add("FALSE");
                        break;
                    case "TableType":
                        flag = false;
                        mGridComboBox.Items.Add("ADT");
                        mGridComboBox.Items.Add("VFP");
                        mGridComboBox.Items.Add("CDX");
                        mGridComboBox.Items.Add("NTX");
                        break;
                    case "CharType":
                        flag = false;
                        mGridComboBox.Items.AddRange(Enum.GetNames(typeof(ACE.AdsCharTypes)));
                        break;
                    case "UnicodeCollation":
                        flag = false;
                        try
                        {
                            var adsConnection = new AdsConnection("data source=.;servertype=local;");
                            adsConnection.Open();
                            var command = adsConnection.CreateCommand();
                            command.CommandText =
                                "select x.Name from ( execute procedure sp_getcollations(null) )x where x.UnicodeLocale is null;";
                            command.CommandType = CommandType.Text;
                            var adsDataReader = command.ExecuteReader();
                            while (adsDataReader.Read())
                            {
                                mGridComboBox.Items.Add(adsDataReader.GetString(0).Trim());
                                mGridComboBox.Items.Add(
                                    adsDataReader.GetString(0).Trim() + "_ads_ci");
                            }

                            adsDataReader.Close();
                            adsConnection.Close();
                            adsConnection.Dispose();
                            break;
                        }
                        catch
                        {
                            break;
                        }
                    case "LockMode":
                        flag = false;
                        mGridComboBox.Items.Add("COMPATIBLE");
                        mGridComboBox.Items.Add("PROPRIETARY");
                        break;
                    case "SecurityMode":
                        flag = false;
                        mGridComboBox.Items.Add("CHECKRIGHTS");
                        mGridComboBox.Items.Add("IGNORERIGHTS");
                        break;
                    case "ServerType":
                        flag = false;
                        mGridComboBox.Items.Add("LOCAL");
                        mGridComboBox.Items.Add("REMOTE");
                        mGridComboBox.Items.Add("AIS");
                        break;
                    case "FilterOptions":
                        flag = false;
                        mGridComboBox.Items.Add("IGNORE_WHEN_COUNTING");
                        mGridComboBox.Items.Add("RESPECT_WHEN_COUNTING");
                        break;
                    case "Compression":
                        flag = false;
                        mGridComboBox.Items.Add("INTERNET");
                        mGridComboBox.Items.Add("ALWAYS");
                        mGridComboBox.Items.Add("NEVER");
                        break;
                    case "CommType":
                        flag = false;
                        mGridComboBox.Items.Add("UDP_IP");
                        mGridComboBox.Items.Add("TCP_IP");
                        mGridComboBox.Items.Add("IPX");
                        break;
                    default:
                        flag = true;
                        break;
                }

                var currentCellBounds = mDataGrid.GetCurrentCellBounds();
                var s = mDataTable.Rows[mDataGrid.CurrentCell.RowNumber][1].ToString();
                if (flag)
                {
                    mGridTextBox.Text = s;
                    mGridTextBox.Width = currentCellBounds.Width;
                    mGridTextBox.Height = currentCellBounds.Height;
                    mGridTextBox.Left = currentCellBounds.Left + mDataGrid.Left;
                    mGridTextBox.Top = currentCellBounds.Top + mDataGrid.Top + 1;
                    mGridTextBox.Visible = true;
                    mTimer.Start();
                }
                else
                {
                    mGridComboBox.Width = currentCellBounds.Width;
                    mGridComboBox.Height = currentCellBounds.Height;
                    mGridComboBox.Left = currentCellBounds.Left + mDataGrid.Left;
                    mGridComboBox.Top = currentCellBounds.Top + mDataGrid.Top;
                    mGridComboBox.Text = s;
                    mGridComboBox.SelectedIndex = mGridComboBox.FindStringExact(s);
                    if (mGridComboBox.SelectedIndex == -1)
                        mGridComboBox.SelectedIndex = mGridComboBox.FindString(s);
                    mTimer.Start();
                }

                mGridTextBox.Visible = flag;
                mGridComboBox.Visible = !flag;
            }
        }

        private void mGridTextBox_Leave(object sender, EventArgs e)
        {
            var str = mGridTextBox.Text;
            if (str.Length > 0)
            {
                if (mbIntTextOnly)
                {
                    try
                    {
                        var int32 = Convert.ToInt32(str);
                        if (int32 < 0)
                        {
                            var num = (int)MessageBox.Show("Non-negative integer value expected.",
                                ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            str = "";
                        }
                        else
                            str = int32.ToString();
                    }
                    catch (Exception ex)
                    {
                        str = "";
                        var num = (int)MessageBox.Show("Integer value expected.", ConfigWizard.MessageBoxTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    mGridTextBox.Text = str;
                }
            }

            mDataTable.Rows[mDataGrid.CurrentRowIndex][1] = str;
        }

        private void mGridComboBox_Leave(object sender, EventArgs e)
        {
            mDataTable.Rows[mDataGrid.CurrentRowIndex][1] = mGridComboBox.Text;
            mGridComboBox.Hide();
        }

        private void mPathRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (mPathRadio.Checked)
            {
                mType = SourceType.Path;
                mDataSourceText.Text = mstrPath;
                mPathLabel.Text = "P&ath:";
                mDataSourceText.Width = mUserNameText.Width - mBrowse.Width;
            }
            else
                mstrPath = mDataSourceText.Text;
        }

        private void mDictRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (mDictRadio.Checked)
            {
                mType = SourceType.Dict;
                mDataSourceText.Text = mstrDict;
                mPathLabel.Text = "Diction&ary:";
                mDataSourceText.Width = mUserNameText.Width - mBrowse.Width;
            }
            else
                mstrDict = mDataSourceText.Text;
        }

        private void mBrowse_Click(object sender, EventArgs e)
        {
            switch (mType)
            {
                case SourceType.Path:
                    var str = new BrowseForFolderClass().BrowseForFolder("Select Folder");
                    if (str.Length <= 0)
                        break;
                    mDataSourceText.Text = str;
                    break;
                case SourceType.Dict:
                    mOpenFileDlg.FileName = mDataSourceText.Text;
                    if (mOpenFileDlg.ShowDialog(this) != DialogResult.OK)
                        break;
                    mDataSourceText.Text = mOpenFileDlg.FileName;
                    break;
                default:
                    var num = (int)MessageBox.Show("Unsupported Type", ConfigWizard.MessageBoxTitle,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        private void mDataGrid_Scroll(object sender, EventArgs e)
        {
            mGridComboBox.Hide();
            mGridTextBox.Hide();
        }

        private void BuildConnString_Load(object sender, EventArgs e)
        {
            BuildConnString_Resize(null, null);
            mDataGrid.CurrentCell = mDataGrid.CurrentCell with
            {
                ColumnNumber = 0,
                RowNumber = 0
            };
        }

        private void mGridComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            var currentCell = mDataGrid.CurrentCell;
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    mGridComboBox.Hide();
                    if (e.Shift)
                    {
                        currentCell.ColumnNumber = 0;
                    }
                    else
                    {
                        if (mDataGrid.CurrentRowIndex + 1 >= mDataTable.Rows.Count)
                            break;
                        currentCell.ColumnNumber = 0;
                        ++currentCell.RowNumber;
                    }

                    mDataGrid.CurrentCell = currentCell;
                    break;
                case Keys.Return:
                    mGridComboBox.Hide();
                    currentCell.ColumnNumber = 0;
                    mDataGrid.CurrentCell = currentCell;
                    break;
                case Keys.Escape:
                    mGridComboBox.Text = mDataTable.Rows[mDataGrid.CurrentRowIndex][1].ToString();
                    mGridComboBox.Hide();
                    currentCell.ColumnNumber = 0;
                    mDataGrid.CurrentCell = currentCell;
                    break;
            }
        }

        private void mTimer_Elapsed(object obj, EventArgs args)
        {
            mTimer.Stop();
            if (mGridComboBox.Visible)
                mGridComboBox.Focus();
            if (!mGridTextBox.Visible)
                return;
            mGridTextBox.SelectAll();
            mGridTextBox.Focus();
        }

        private void mGridTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var currentCell = mDataGrid.CurrentCell;
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    mGridTextBox.Hide();
                    if (e.Shift)
                    {
                        currentCell.ColumnNumber = 0;
                    }
                    else
                    {
                        if (mDataGrid.CurrentRowIndex + 1 >= mDataTable.Rows.Count)
                            break;
                        currentCell.ColumnNumber = 0;
                        ++currentCell.RowNumber;
                    }

                    mDataGrid.CurrentCell = currentCell;
                    break;
                case Keys.Return:
                    mGridTextBox.Hide();
                    break;
                case Keys.Escape:
                    mGridTextBox.Text = mDataTable.Rows[mDataGrid.CurrentRowIndex][1].ToString();
                    mGridTextBox.Hide();
                    break;
                case Keys.Up:
                    if (currentCell.RowNumber == 0)
                        break;
                    --currentCell.RowNumber;
                    mDataGrid.CurrentCell = currentCell;
                    break;
                case Keys.Down:
                    if (currentCell.RowNumber + 1 < mDataTable.Rows.Count)
                        ++currentCell.RowNumber;
                    mDataGrid.CurrentCell = currentCell;
                    break;
            }
        }

        private void mDataGrid_Enter(object sender, EventArgs e)
        {
            mDataGrid_CurrentCellChanged(null, null);
        }

        private void mUserNameText_Enter(object sender, EventArgs e)
        {
        }

        public string ConnectionString => mConnectionString;

        private enum SourceType
        {
            Alias = 1,
            Path = 2,
            Dict = 3,
        }
    }
}