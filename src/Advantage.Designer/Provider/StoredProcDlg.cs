using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using Advantage.Data.Provider.Properties;

namespace Advantage.Data.Provider
{
    public class StoredProcDlg : Form
    {
        private IContainer components;
        protected Button mBackButton;
        protected Button mNextButton;
        protected Button mCancelButton;
        protected Button mFinishButton;
        protected GroupBox mSeparator;
        protected PictureBox mHeaderPicture;
        protected Label mSubtitleLabel;
        protected Label mTitleLabel;
        protected GroupBox mHeaderSeparator;
        protected Panel mHeaderPanel;
        private GridComboBox mGridComboBox;
        private DataGridEx mDataGrid;
        private PictureBox mInsertArrow;
        private PictureBox mDeleteArrow;
        private PictureBox mUpdateArrow;
        private PictureBox mSelectArrow;
        private Label mSetParamsLabel;
        private ComboBox mDeleteComboBox;
        private ComboBox mUpdateComboBox;
        private ComboBox mInsertComboBox;
        private ComboBox mSelectComboBox;
        private Label mDeleteLabel;
        private Label mUpdateLabel;
        private Label mInsertLabel;
        private Label mSelectLabel;
        private Label mInstructLabel;
        private Timer mTimer;
        private ConfigWizard mParent;
        private string mstrOriginalParamLabel;
        private static int GridParamColumn = 0;
        private static int GridDataColumn = 1;
        private AdsCommand mCurrentCommand;
        private AdsConnection mConnection;
        private DataTable mSelectDataTable;
        private DataTable mInsertDataTable;
        private DataTable mDeleteDataTable;
        private DataTable mUpdateDataTable;
        private DataTable mCurrentTable;
        private DataGridTableStyle mSelectTableStyle;
        private DataGridTableStyle mNonselectTableStyle;
        private DataGridTextBoxColumn mDataColumnColumn;
        private DataGridTextBoxColumn mParameterColumn;
        private DataGridTextBoxColumn mSourceColumnColumn;

        protected override void Dispose(bool disposing)
        {
            if (mConnection != null && mConnection.State != ConnectionState.Closed)
                mConnection.Close();
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            var componentResourceManager = new ComponentResourceManager(typeof(StoredProcDlg));
            mBackButton = new Button();
            mNextButton = new Button();
            mCancelButton = new Button();
            mFinishButton = new Button();
            mSeparator = new GroupBox();
            mHeaderPicture = new PictureBox();
            mSubtitleLabel = new Label();
            mTitleLabel = new Label();
            mHeaderSeparator = new GroupBox();
            mHeaderPanel = new Panel();
            mInsertArrow = new PictureBox();
            mDeleteArrow = new PictureBox();
            mUpdateArrow = new PictureBox();
            mSelectArrow = new PictureBox();
            mSetParamsLabel = new Label();
            mDeleteComboBox = new ComboBox();
            mUpdateComboBox = new ComboBox();
            mInsertComboBox = new ComboBox();
            mSelectComboBox = new ComboBox();
            mDeleteLabel = new Label();
            mUpdateLabel = new Label();
            mInsertLabel = new Label();
            mSelectLabel = new Label();
            mInstructLabel = new Label();
            mTimer = new Timer(components);
            mGridComboBox = new GridComboBox();
            mDataGrid = new DataGridEx();
            mSelectTableStyle = new DataGridTableStyle();
            mDataColumnColumn = new DataGridTextBoxColumn();
            mNonselectTableStyle = new DataGridTableStyle();
            mParameterColumn = new DataGridTextBoxColumn();
            mSourceColumnColumn = new DataGridTextBoxColumn();
            ((ISupportInitialize)mHeaderPicture).BeginInit();
            ((ISupportInitialize)mInsertArrow).BeginInit();
            ((ISupportInitialize)mDeleteArrow).BeginInit();
            ((ISupportInitialize)mUpdateArrow).BeginInit();
            ((ISupportInitialize)mSelectArrow).BeginInit();
            mDataGrid.BeginInit();
            SuspendLayout();
            mBackButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mBackButton.Location = new Point(244, 328);
            mBackButton.Name = "mBackButton";
            mBackButton.Size = new Size(75, 26);
            mBackButton.TabIndex = 41;
            mBackButton.Text = "< &Back";
            mBackButton.Click += mBackButton_Click;
            mNextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mNextButton.Location = new Point(328, 328);
            mNextButton.Name = "mNextButton";
            mNextButton.Size = new Size(75, 26);
            mNextButton.TabIndex = 42;
            mNextButton.Text = "&Next >";
            mNextButton.Click += mNextButton_Click;
            mCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mCancelButton.DialogResult = DialogResult.Cancel;
            mCancelButton.Location = new Point(160, 328);
            mCancelButton.Name = "mCancelButton";
            mCancelButton.Size = new Size(75, 26);
            mCancelButton.TabIndex = 40;
            mCancelButton.Text = "Cancel";
            mCancelButton.Click += mCancelButton_Click;
            mFinishButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mFinishButton.Location = new Point(412, 328);
            mFinishButton.Name = "mFinishButton";
            mFinishButton.Size = new Size(75, 26);
            mFinishButton.TabIndex = 43;
            mFinishButton.Text = "&Finish";
            mFinishButton.Click += mFinishButton_Click;
            mSeparator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mSeparator.Location = new Point(0, 313);
            mSeparator.Name = "mSeparator";
            mSeparator.Size = new Size(499, 2);
            mSeparator.TabIndex = 44;
            mSeparator.TabStop = false;
            mHeaderPicture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mHeaderPicture.BackColor = Color.White;
            mHeaderPicture.Image = Resources.QueryBuild;
            mHeaderPicture.Location = new Point(416, 3);
            mHeaderPicture.Name = "mHeaderPicture";
            mHeaderPicture.Size = new Size(75, 51);
            mHeaderPicture.TabIndex = 45;
            mHeaderPicture.TabStop = false;
            mSubtitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mSubtitleLabel.BackColor = Color.White;
            mSubtitleLabel.Location = new Point(32, 25);
            mSubtitleLabel.Name = "mSubtitleLabel";
            mSubtitleLabel.Size = new Size(367, 26);
            mSubtitleLabel.TabIndex = 46;
            mSubtitleLabel.Text = "Choose the stored procedures to call and specify any required parameters.";
            mTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTitleLabel.BackColor = Color.White;
            mTitleLabel.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTitleLabel.Location = new Point(16, 10);
            mTitleLabel.Name = "mTitleLabel";
            mTitleLabel.Size = new Size(410, 13);
            mTitleLabel.TabIndex = 47;
            mTitleLabel.Text = "Bind Commands to Existing Stored Procedures";
            mHeaderSeparator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mHeaderSeparator.Location = new Point(0, 58);
            mHeaderSeparator.Name = "mHeaderSeparator";
            mHeaderSeparator.Size = new Size(499, 2);
            mHeaderSeparator.TabIndex = 48;
            mHeaderSeparator.TabStop = false;
            mHeaderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mHeaderPanel.BackColor = Color.White;
            mHeaderPanel.Location = new Point(0, 0);
            mHeaderPanel.Name = "mHeaderPanel";
            mHeaderPanel.Size = new Size(497, 58);
            mHeaderPanel.TabIndex = 49;
            mInsertArrow.Image = (Image)componentResourceManager.GetObject("mInsertArrow.Image");
            mInsertArrow.Location = new Point(240, 176);
            mInsertArrow.Name = "mInsertArrow";
            mInsertArrow.Size = new Size(16, 24);
            mInsertArrow.TabIndex = 78;
            mInsertArrow.TabStop = false;
            mInsertArrow.Visible = false;
            mDeleteArrow.Image = (Image)componentResourceManager.GetObject("mDeleteArrow.Image");
            mDeleteArrow.Location = new Point(240, 256);
            mDeleteArrow.Name = "mDeleteArrow";
            mDeleteArrow.Size = new Size(16, 24);
            mDeleteArrow.TabIndex = 77;
            mDeleteArrow.TabStop = false;
            mDeleteArrow.Visible = false;
            mUpdateArrow.Image = (Image)componentResourceManager.GetObject("mUpdateArrow.Image");
            mUpdateArrow.Location = new Point(240, 216);
            mUpdateArrow.Name = "mUpdateArrow";
            mUpdateArrow.Size = new Size(16, 24);
            mUpdateArrow.TabIndex = 76;
            mUpdateArrow.TabStop = false;
            mUpdateArrow.Visible = false;
            mSelectArrow.Image = (Image)componentResourceManager.GetObject("mSelectArrow.Image");
            mSelectArrow.Location = new Point(240, 136);
            mSelectArrow.Name = "mSelectArrow";
            mSelectArrow.Size = new Size(16, 24);
            mSelectArrow.TabIndex = 75;
            mSelectArrow.TabStop = false;
            mSelectArrow.Visible = false;
            mSetParamsLabel.Location = new Point(272, 116);
            mSetParamsLabel.Name = "mSetParamsLabel";
            mSetParamsLabel.Size = new Size(200, 16);
            mSetParamsLabel.TabIndex = 74;
            mSetParamsLabel.Text = "Set Select procedure parameters:";
            mDeleteComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mDeleteComboBox.Location = new Point(80, 256);
            mDeleteComboBox.Name = "mDeleteComboBox";
            mDeleteComboBox.Size = new Size(144, 21);
            mDeleteComboBox.TabIndex = 30;
            mDeleteComboBox.SelectedIndexChanged += mDeleteComboBox_SelectedIndexChanged;
            mDeleteComboBox.Enter += mDeleteComboBox_Enter;
            mUpdateComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mUpdateComboBox.Location = new Point(80, 216);
            mUpdateComboBox.Name = "mUpdateComboBox";
            mUpdateComboBox.Size = new Size(144, 21);
            mUpdateComboBox.TabIndex = 20;
            mUpdateComboBox.SelectedIndexChanged += mUpdateComboBox_SelectedIndexChanged;
            mUpdateComboBox.Enter += mUpdateComboBox_Enter;
            mInsertComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mInsertComboBox.Location = new Point(80, 176);
            mInsertComboBox.Name = "mInsertComboBox";
            mInsertComboBox.Size = new Size(144, 21);
            mInsertComboBox.TabIndex = 10;
            mInsertComboBox.SelectedIndexChanged += mInsertComboBox_SelectedIndexChanged;
            mInsertComboBox.Enter += mInsertComboBox_Enter;
            mSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mSelectComboBox.Location = new Point(80, 136);
            mSelectComboBox.Name = "mSelectComboBox";
            mSelectComboBox.Size = new Size(144, 21);
            mSelectComboBox.TabIndex = 0;
            mSelectComboBox.SelectedIndexChanged += mSelectComboBox_SelectedIndexChanged;
            mSelectComboBox.Enter += mSelectComboBox_Enter;
            mDeleteLabel.Location = new Point(16, 264);
            mDeleteLabel.Name = "mDeleteLabel";
            mDeleteLabel.Size = new Size(48, 16);
            mDeleteLabel.TabIndex = 85;
            mDeleteLabel.Text = "&Delete:";
            mUpdateLabel.Location = new Point(16, 224);
            mUpdateLabel.Name = "mUpdateLabel";
            mUpdateLabel.Size = new Size(48, 16);
            mUpdateLabel.TabIndex = 83;
            mUpdateLabel.Text = "&Update:";
            mInsertLabel.Location = new Point(16, 184);
            mInsertLabel.Name = "mInsertLabel";
            mInsertLabel.Size = new Size(48, 16);
            mInsertLabel.TabIndex = 81;
            mInsertLabel.Text = "&Insert:";
            mSelectLabel.Location = new Point(16, 144);
            mSelectLabel.Name = "mSelectLabel";
            mSelectLabel.Size = new Size(48, 16);
            mSelectLabel.TabIndex = 79;
            mSelectLabel.Text = "&Select:";
            mInstructLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mInstructLabel.Location = new Point(16, 72);
            mInstructLabel.Name = "mInstructLabel";
            mInstructLabel.Size = new Size(448, 32);
            mInstructLabel.TabIndex = 73;
            mInstructLabel.Text =
                "Select the stored procedure for each operation. If the procedure requires parameters, specify which column in the data row contains the parameter value.";
            mTimer.Interval = 1;
            mGridComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mGridComboBox.Location = new Point(224, 112);
            mGridComboBox.Name = "mGridComboBox";
            mGridComboBox.Size = new Size(32, 21);
            mGridComboBox.TabIndex = 88;
            mGridComboBox.TabStop = false;
            mGridComboBox.Visible = false;
            mGridComboBox.Leave += mGridComboBox_Leave;
            mGridComboBox.KeyDown += mGridComboBox_KeyDown;
            mDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mDataGrid.CaptionVisible = false;
            mDataGrid.DataMember = "";
            mDataGrid.HeaderForeColor = SystemColors.ControlText;
            mDataGrid.Location = new Point(272, 136);
            mDataGrid.Name = "mDataGrid";
            mDataGrid.ReadOnly = true;
            mDataGrid.RowHeadersVisible = false;
            mDataGrid.Size = new Size(208, 160);
            mDataGrid.TabIndex = 1;
            mDataGrid.TableStyles.AddRange(new DataGridTableStyle[2]
            {
                mSelectTableStyle,
                mNonselectTableStyle
            });
            mDataGrid.Scroll += mDataGrid_Scroll;
            mDataGrid.Resize += mDataGrid_Resize;
            mDataGrid.Enter += mDataGrid_Enter;
            mDataGrid.CurrentCellChanged += mDataGrid_CurrentCellChanged;
            mSelectTableStyle.DataGrid = mDataGrid;
            mSelectTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[1]
            {
                mDataColumnColumn
            });
            mSelectTableStyle.HeaderForeColor = SystemColors.ControlText;
            mSelectTableStyle.MappingName = "selectdata";
            mSelectTableStyle.PreferredColumnWidth = 150;
            mSelectTableStyle.PreferredRowHeight = 21;
            mSelectTableStyle.ReadOnly = true;
            mSelectTableStyle.RowHeadersVisible = false;
            mDataColumnColumn.Format = "";
            mDataColumnColumn.FormatInfo = null;
            mDataColumnColumn.HeaderText = "Data Column";
            mDataColumnColumn.MappingName = "Data Column";
            mDataColumnColumn.ReadOnly = true;
            mDataColumnColumn.Width = 250;
            mNonselectTableStyle.DataGrid = mDataGrid;
            mNonselectTableStyle.GridColumnStyles.AddRange(new DataGridColumnStyle[2]
            {
                mParameterColumn,
                mSourceColumnColumn
            });
            mNonselectTableStyle.HeaderForeColor = SystemColors.ControlText;
            mNonselectTableStyle.MappingName = "nonselectdata";
            mNonselectTableStyle.PreferredColumnWidth = 150;
            mNonselectTableStyle.PreferredRowHeight = 21;
            mNonselectTableStyle.ReadOnly = true;
            mNonselectTableStyle.RowHeadersVisible = false;
            mParameterColumn.Format = "";
            mParameterColumn.FormatInfo = null;
            mParameterColumn.HeaderText = "Parameter";
            mParameterColumn.MappingName = "Parameter";
            mParameterColumn.ReadOnly = true;
            mParameterColumn.Width = 5;
            mSourceColumnColumn.Format = "";
            mSourceColumnColumn.FormatInfo = null;
            mSourceColumnColumn.HeaderText = "Source Column";
            mSourceColumnColumn.MappingName = "Source Column";
            mSourceColumnColumn.ReadOnly = true;
            mSourceColumnColumn.Width = 5;
            AcceptButton = mNextButton;
            AutoScaleMode = AutoScaleMode.Inherit;
            CancelButton = mCancelButton;
            ClientSize = new Size(497, 365);
            Controls.Add(mGridComboBox);
            Controls.Add(mDataGrid);
            Controls.Add(mInsertArrow);
            Controls.Add(mDeleteArrow);
            Controls.Add(mUpdateArrow);
            Controls.Add(mSelectArrow);
            Controls.Add(mSetParamsLabel);
            Controls.Add(mDeleteComboBox);
            Controls.Add(mUpdateComboBox);
            Controls.Add(mInsertComboBox);
            Controls.Add(mSelectComboBox);
            Controls.Add(mDeleteLabel);
            Controls.Add(mUpdateLabel);
            Controls.Add(mInsertLabel);
            Controls.Add(mSelectLabel);
            Controls.Add(mInstructLabel);
            Controls.Add(mHeaderPicture);
            Controls.Add(mSubtitleLabel);
            Controls.Add(mTitleLabel);
            Controls.Add(mHeaderSeparator);
            Controls.Add(mHeaderPanel);
            Controls.Add(mBackButton);
            Controls.Add(mNextButton);
            Controls.Add(mCancelButton);
            Controls.Add(mFinishButton);
            Controls.Add(mSeparator);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(505, 392);
            Name = nameof(StoredProcDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Data Adapter Configuration Wizard";
            Load += StoredProcDlg_Load;
            FormClosed += StoredProcDlg_FormClosed;
            ((ISupportInitialize)mHeaderPicture).EndInit();
            ((ISupportInitialize)mInsertArrow).EndInit();
            ((ISupportInitialize)mDeleteArrow).EndInit();
            ((ISupportInitialize)mUpdateArrow).EndInit();
            ((ISupportInitialize)mSelectArrow).EndInit();
            mDataGrid.EndInit();
            ResumeLayout(false);
        }

        public StoredProcDlg(ConfigWizard parent)
        {
            mParent = parent;
            InitializeComponent();
            mstrOriginalParamLabel = mSetParamsLabel.Text;
            mSelectDataTable = new DataTable();
            mSelectDataTable.TableName = "selectdata";
            mSelectDataTable.Columns.Add(new DataColumn("Data Column", typeof(string)));
            mInsertDataTable = new DataTable();
            mInsertDataTable.TableName = "nonselectdata";
            mInsertDataTable.Columns.Add(new DataColumn("Parameter", typeof(string)));
            mInsertDataTable.Columns.Add(new DataColumn("Source Column", typeof(string)));
            mDeleteDataTable = new DataTable();
            mDeleteDataTable.TableName = "nonselectdata";
            mDeleteDataTable.Columns.Add(new DataColumn("Parameter", typeof(string)));
            mDeleteDataTable.Columns.Add(new DataColumn("Source Column", typeof(string)));
            mUpdateDataTable = new DataTable();
            mUpdateDataTable.TableName = "nonselectdata";
            mUpdateDataTable.Columns.Add(new DataColumn("Parameter", typeof(string)));
            mUpdateDataTable.Columns.Add(new DataColumn("Source Column", typeof(string)));
        }

        private void StoredProcDlg_Load(object sender, EventArgs e)
        {
            Size = mParent.FormSize;
            Location = mParent.FormLocation;
            Cursor.Current = Cursors.WaitCursor;
            UpdateDataGrid(StoreProcOperation.Select);
            mSelectComboBox.Focus();
            if (mParent.ReadyToFinish)
            {
                mFinishButton.Enabled = true;
                mNextButton.Enabled = true;
            }
            else
            {
                mFinishButton.Enabled = false;
                mNextButton.Enabled = false;
            }

            if (mParent.ConnectionString.Length > 0)
            {
                mConnection = new AdsConnection(mParent.ConnectionString);
                try
                {
                    mConnection.Open();
                    var ddObjects = mConnection.GetDDObjects((AdsConnection.AdsObjectType)8, "");
                    mSelectComboBox.Items.Clear();
                    mInsertComboBox.Items.Clear();
                    mUpdateComboBox.Items.Clear();
                    mDeleteComboBox.Items.Clear();
                    mSelectComboBox.Items.Add("");
                    mInsertComboBox.Items.Add("");
                    mUpdateComboBox.Items.Add("");
                    mDeleteComboBox.Items.Add("");
                    if (ddObjects.Length == 0)
                    {
                        var num = (int)MessageBox.Show("No Stored Procedures found.", ConfigWizard.MessageBoxTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        if (mConnection != null &&
                            mConnection.State != ConnectionState.Closed)
                            mConnection.Close();
                        mNextButton.Enabled = true;
                        return;
                    }

                    mSelectComboBox.Items.AddRange(ddObjects);
                    mInsertComboBox.Items.AddRange(ddObjects);
                    mUpdateComboBox.Items.AddRange(ddObjects);
                    mDeleteComboBox.Items.AddRange(ddObjects);
                    mParent.SelectCommand.Connection = mConnection;
                    mParent.InsertCommand.Connection = mConnection;
                    mParent.DeleteCommand.Connection = mConnection;
                    mParent.UpdateCommand.Connection = mConnection;
                    if (mParent.SelectCommand.CommandText != null &&
                        mParent.SelectCommand.CommandText != "")
                        mSelectComboBox.SelectedIndex =
                            mSelectComboBox.FindStringExact(mParent.SelectCommand.CommandText);
                    if (mSelectComboBox.SelectedIndex == -1 || mSelectComboBox.SelectedIndex == 0)
                    {
                        mSelectDataTable.Rows.Clear();
                        mParent.SelectCommand.Parameters.Clear();
                    }

                    if (mParent.InsertCommand.CommandText != null)
                        mInsertComboBox.SelectedIndex =
                            mInsertComboBox.FindStringExact(mParent.InsertCommand.CommandText);
                    if (mInsertComboBox.SelectedIndex == -1 || mInsertComboBox.SelectedIndex == 0)
                    {
                        mInsertDataTable.Rows.Clear();
                        mParent.InsertCommand.Parameters.Clear();
                    }

                    if (mParent.UpdateCommand.CommandText != null)
                        mUpdateComboBox.SelectedIndex =
                            mUpdateComboBox.FindStringExact(mParent.UpdateCommand.CommandText);
                    if (mUpdateComboBox.SelectedIndex == -1 || mUpdateComboBox.SelectedIndex == 0)
                    {
                        mUpdateDataTable.Rows.Clear();
                        mParent.UpdateCommand.Parameters.Clear();
                    }

                    if (mParent.DeleteCommand.CommandText != null)
                        mDeleteComboBox.SelectedIndex =
                            mDeleteComboBox.FindStringExact(mParent.DeleteCommand.CommandText);
                    if (mDeleteComboBox.SelectedIndex != -1)
                    {
                        if (mDeleteComboBox.SelectedIndex != 0)
                            goto label_29;
                    }

                    mDeleteDataTable.Rows.Clear();
                    mParent.DeleteCommand.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    var num = (int)MessageBox.Show("Connection failed to open.\n\n" + ex,
                        ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    if (mConnection != null && mConnection.State != ConnectionState.Closed)
                        mConnection.Close();
                    mNextButton.Enabled = true;
                }

                label_29:
                mParent.SelectCommand.CommandType = CommandType.StoredProcedure;
                mParent.InsertCommand.CommandType = CommandType.StoredProcedure;
                mParent.DeleteCommand.CommandType = CommandType.StoredProcedure;
                mParent.UpdateCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                var num1 = (int)MessageBox.Show("Invalid Connection String", ConfigWizard.MessageBoxTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            Cursor.Current = Cursors.Default;
            if (mSelectComboBox.SelectedIndex >= 0)
                mSelectComboBox_SelectedIndexChanged(null, null);
            FillDataTable(StoreProcOperation.Insert, mParent.InsertCommand,
                mInsertDataTable);
            FillDataTable(StoreProcOperation.Delete, mParent.DeleteCommand,
                mDeleteDataTable);
            FillDataTable(StoreProcOperation.Update, mParent.UpdateCommand,
                mUpdateDataTable);
        }

        protected bool SaveData()
        {
            if (mConnection != null && mConnection.State != ConnectionState.Closed)
                mConnection.Close();
            if (mSelectComboBox.Text.Length == 0)
            {
                var num = (int)MessageBox.Show("No Stored Procedure selected for Select operation",
                    ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mSelectComboBox.Focus();
                return false;
            }

            mParent.QueryType = ConfigWizard.QueryTypes.StoredProc;
            mParent.OptimisticConcurrency = false;
            mParent.GenerateCommands = false;
            if (mInsertComboBox.Text == "" && mInsertDataTable.Rows.Count > 0)
                mInsertDataTable.Rows.Clear();
            if (mUpdateComboBox.Text == "" && mUpdateDataTable.Rows.Count > 0)
                mUpdateDataTable.Rows.Clear();
            if (mDeleteComboBox.Text == "" && mDeleteDataTable.Rows.Count > 0)
                mDeleteDataTable.Rows.Clear();
            var flag = true;
            if (!CheckTableComplete(mInsertDataTable))
            {
                flag = false;
                mInsertComboBox.Focus();
            }
            else if (!CheckTableComplete(mDeleteDataTable))
            {
                flag = false;
                mInsertComboBox.Focus();
            }
            else if (!CheckTableComplete(mUpdateDataTable))
            {
                flag = false;
                mInsertComboBox.Focus();
            }

            return flag ||
                   MessageBox.Show(
                       "Some parameter bindings are missing. Values from the dataset will not be used for those parameters.",
                       ConfigWizard.MessageBoxTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button2) != DialogResult.Cancel;
        }

        private bool CheckTableComplete(DataTable table)
        {
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
            {
                var s = row[GridDataColumn].ToString();
                if (s.Length == 0)
                    return false;
                if (mGridComboBox.FindStringExact(s) == -1)
                {
                    row[GridDataColumn] = "";
                    return false;
                }
            }

            return true;
        }

        private void FillDataGrid(StoreProcOperation type)
        {
            switch (type)
            {
                case StoreProcOperation.Select:
                    mCurrentCommand = mParent.SelectCommand;
                    mCurrentTable = mSelectDataTable;
                    break;
                case StoreProcOperation.Insert:
                    mCurrentCommand = mParent.InsertCommand;
                    mCurrentTable = mInsertDataTable;
                    break;
                case StoreProcOperation.Delete:
                    mCurrentCommand = mParent.DeleteCommand;
                    mCurrentTable = mDeleteDataTable;
                    break;
                case StoreProcOperation.Update:
                    mCurrentCommand = mParent.UpdateCommand;
                    mCurrentTable = mUpdateDataTable;
                    break;
                default:
                    return;
            }

            try
            {
                mCurrentCommand.DeriveParameters();
                FillDataTable(type, mCurrentCommand, mCurrentTable);
                if (StoreProcOperation.Select == type)
                {
                    CheckValidSourceColumns(mInsertDataTable);
                    CheckValidSourceColumns(mDeleteDataTable);
                    CheckValidSourceColumns(mUpdateDataTable);
                }

                mDataGrid.TabStop = mCurrentTable.Rows.Count != 0;
                mDataGrid.DataSource = new DataView(mCurrentTable);
            }
            catch (Exception ex)
            {
                var num = (int)MessageBox.Show("Error generating column info.\n\n" + ex,
                    ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FillDataTable(
            StoreProcOperation type,
            AdsCommand command,
            DataTable table)
        {
            table.Rows.Clear();
            if (StoreProcOperation.Select == type)
            {
                mGridComboBox.Items.Clear();
                mGridComboBox.Items.Add("");
                foreach (AdsParameter parameter in (DbParameterCollection)command.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Output)
                    {
                        table.Rows.Add(parameter.ParameterName);
                        mGridComboBox.Items.Add(parameter.ParameterName);
                    }
                }
            }
            else
            {
                foreach (AdsParameter parameter in (DbParameterCollection)command.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.Input)
                        table.Rows.Add(parameter.ParameterName,
                            parameter.SourceColumn);
                }
            }
        }

        private void CheckValidSourceColumns(DataTable table)
        {
            foreach (DataRow row in (InternalDataCollectionBase)table.Rows)
            {
                if (mGridComboBox.FindStringExact(row[1].ToString()) == -1)
                    row[GridDataColumn] = "";
            }
        }

        private void UpdateDataGrid(StoreProcOperation type)
        {
            try
            {
                mSelectArrow.Visible = false;
                mUpdateArrow.Visible = false;
                mDeleteArrow.Visible = false;
                mInsertArrow.Visible = false;
                mDataGrid.Enabled = false;
                string str;
                switch (type)
                {
                    case StoreProcOperation.Select:
                        mSelectArrow.Visible = true;
                        if (mCurrentTable == mSelectDataTable)
                            return;
                        mCurrentTable = mSelectDataTable;
                        mCurrentCommand = mParent.SelectCommand;
                        str = mstrOriginalParamLabel;
                        break;
                    case StoreProcOperation.Insert:
                        mInsertArrow.Visible = true;
                        if (mCurrentTable == mInsertDataTable)
                            return;
                        mCurrentTable = mInsertDataTable;
                        mCurrentCommand = mParent.InsertCommand;
                        mDataGrid.TabIndex = mInsertComboBox.TabIndex + 1;
                        mGridComboBox.TabIndex = mInsertComboBox.TabIndex + 2;
                        str = mstrOriginalParamLabel.Replace("Select", "Insert");
                        break;
                    case StoreProcOperation.Delete:
                        mDeleteArrow.Visible = true;
                        if (mCurrentTable == mDeleteDataTable)
                            return;
                        mCurrentTable = mDeleteDataTable;
                        mCurrentCommand = mParent.DeleteCommand;
                        mDataGrid.TabIndex = mDeleteComboBox.TabIndex + 1;
                        mGridComboBox.TabIndex = mDeleteComboBox.TabIndex + 2;
                        str = mstrOriginalParamLabel.Replace("Select", "Delete");
                        break;
                    case StoreProcOperation.Update:
                        mUpdateArrow.Visible = true;
                        if (mCurrentTable == mUpdateDataTable)
                            return;
                        mCurrentTable = mUpdateDataTable;
                        mCurrentCommand = mParent.UpdateCommand;
                        mDataGrid.TabIndex = mUpdateComboBox.TabIndex + 1;
                        mGridComboBox.TabIndex = mUpdateComboBox.TabIndex + 2;
                        str = mstrOriginalParamLabel.Replace("Select", "Update");
                        break;
                    default:
                        return;
                }

                mSetParamsLabel.Text = str;
                mDataGrid.TabStop = mCurrentTable.Rows.Count != 0;
                mDataGrid.DataSource = new DataView(mCurrentTable);
                mDataGrid.Enabled = true;
                StoredProcPage_Resize(null, null);
            }
            catch (Exception ex)
            {
                var num = (int)MessageBox.Show("Error updating column info.\n\n" + ex,
                    ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public bool Ready => mParent.SelectCommand.CommandText.Length > 0;

        private void mSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mSelectComboBox.Text.Length > 0)
            {
                mParent.SelectCommand.CommandText = mSelectComboBox.Text;
                FillDataGrid(StoreProcOperation.Select);
                mNextButton.Enabled = true;
                mFinishButton.Enabled = true;
            }
            else
            {
                mNextButton.Enabled = false;
                mFinishButton.Enabled = false;
            }
        }

        private void mInsertComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mInsertComboBox.Text.Length > 0)
            {
                if (!(mParent.InsertCommand.CommandText != mInsertComboBox.Text))
                    return;
                mParent.InsertCommand.CommandText = mInsertComboBox.Text;
                FillDataGrid(StoreProcOperation.Insert);
            }
            else
            {
                mParent.InsertCommand.CommandText = null;
                mParent.InsertCommand.Parameters.Clear();
                mInsertDataTable.Rows.Clear();
            }
        }

        private void mUpdateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mUpdateComboBox.Text.Length > 0)
            {
                if (!(mParent.UpdateCommand.CommandText != mUpdateComboBox.Text))
                    return;
                mParent.UpdateCommand.CommandText = mUpdateComboBox.Text;
                FillDataGrid(StoreProcOperation.Update);
            }
            else
            {
                mParent.UpdateCommand.CommandText = null;
                mParent.UpdateCommand.Parameters.Clear();
                mUpdateDataTable.Rows.Clear();
            }
        }

        private void mDeleteComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mDeleteComboBox.Text.Length > 0)
            {
                if (!(mParent.DeleteCommand.CommandText != mDeleteComboBox.Text))
                    return;
                mParent.DeleteCommand.CommandText = mDeleteComboBox.Text;
                FillDataGrid(StoreProcOperation.Delete);
            }
            else
            {
                mParent.DeleteCommand.CommandText = null;
                mParent.DeleteCommand.Parameters.Clear();
                mDeleteDataTable.Rows.Clear();
            }
        }

        private void mSelectComboBox_Enter(object sender, EventArgs e)
        {
            UpdateDataGrid(StoreProcOperation.Select);
        }

        private void mInsertComboBox_Enter(object sender, EventArgs e)
        {
            UpdateDataGrid(StoreProcOperation.Insert);
        }

        private void mUpdateComboBox_Enter(object sender, EventArgs e)
        {
            UpdateDataGrid(StoreProcOperation.Update);
        }

        private void mDeleteComboBox_Enter(object sender, EventArgs e)
        {
            UpdateDataGrid(StoreProcOperation.Delete);
        }

        private void StoredProcPage_Resize(object sender, EventArgs e)
        {
            mGridComboBox.Hide();
            if (mCurrentTable == null || mCurrentTable == mSelectDataTable)
            {
                mDataColumnColumn.Width = mDataGrid.InsideWidth - 4;
            }
            else
            {
                var num = mParameterColumn.Width + (mDataGrid.InsideWidth - 4 - mParameterColumn.Width -
                                                    mSourceColumnColumn.Width) / 2;
                mParameterColumn.Width = num;
                mSourceColumnColumn.Width = mDataGrid.InsideWidth - num - 4;
            }
        }

        private void mDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (mDataGrid.CurrentCell.ColumnNumber != GridDataColumn ||
                mDataGrid.CurrentCell.RowNumber >= mCurrentTable.Rows.Count)
            {
                mGridComboBox.Visible = false;
            }
            else
            {
                var currentCellBounds = mDataGrid.GetCurrentCellBounds();
                var s = mCurrentTable.Rows[mDataGrid.CurrentCell.RowNumber][GridDataColumn]
                    .ToString();
                mGridComboBox.Width = currentCellBounds.Width;
                mGridComboBox.Height = currentCellBounds.Height;
                mGridComboBox.Left = currentCellBounds.Left + mDataGrid.Left;
                mGridComboBox.Top = currentCellBounds.Top + mDataGrid.Top;
                mGridComboBox.Text = s;
                mGridComboBox.SelectedIndex = mGridComboBox.FindStringExact(s);
                mTimer.Start();
                mGridComboBox.Visible = true;
            }
        }

        private void mTimer_Elapsed(object obj, EventArgs args)
        {
            mTimer.Stop();
            mGridComboBox.Focus();
            mGridComboBox.SelectAll();
        }

        private void mDataGrid_Scroll(object sender, EventArgs e) => mGridComboBox.Hide();

        private void mDataGrid_Resize(object sender, EventArgs e) => mGridComboBox.Hide();

        private void mGridComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            var currentCell = mDataGrid.CurrentCell;
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    if (e.Shift)
                    {
                        currentCell.ColumnNumber = GridParamColumn;
                    }
                    else
                    {
                        if (mDataGrid.CurrentRowIndex + 1 >= mCurrentTable.Rows.Count)
                        {
                            mGridComboBox.Hide();
                            break;
                        }

                        currentCell.ColumnNumber = GridParamColumn;
                        ++currentCell.RowNumber;
                    }

                    mDataGrid.CurrentCell = currentCell;
                    mDataGrid.Focus();
                    mGridComboBox.Hide();
                    break;
                case Keys.Return:
                    mGridComboBox.Hide();
                    currentCell.ColumnNumber = GridParamColumn;
                    mDataGrid.CurrentCell = currentCell;
                    break;
                case Keys.Escape:
                    mGridComboBox.Text =
                        mCurrentTable.Rows[mDataGrid.CurrentRowIndex][GridDataColumn]
                            .ToString();
                    mGridComboBox.Hide();
                    currentCell.ColumnNumber = GridParamColumn;
                    mDataGrid.CurrentCell = currentCell;
                    break;
            }
        }

        private void mGridComboBox_Leave(object sender, EventArgs e)
        {
            mCurrentTable.Rows[mDataGrid.CurrentRowIndex][GridDataColumn] =
                mGridComboBox.Text;
            try
            {
                mCurrentCommand.Parameters[
                        (string)mCurrentTable.Rows[mDataGrid.CurrentRowIndex][GridParamColumn]]
                    .SourceColumn = mGridComboBox.Text;
            }
            catch (Exception ex)
            {
                var num = (int)MessageBox.Show("Error locating parameter.\n\n" + ex,
                    ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mGridComboBox.Hide();
        }

        private void mDataGrid_Enter(object sender, EventArgs e)
        {
            if (mCurrentTable.Rows.Count == 0)
                GetNextControl(mDataGrid, true)?.Focus();
            else
                mDataGrid_CurrentCellChanged(null, null);
        }

        private void mBackButton_Click(object sender, EventArgs e)
        {
            if (mConnection != null && mConnection.State != ConnectionState.Closed)
                mConnection.Close();
            mParent.mNextDlg = ConfigWizard.WizardDialogs.QueryType;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void mNextButton_Click(object sender, EventArgs e)
        {
            if (!SaveData())
                return;
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Results;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mFinishButton_Click(object sender, EventArgs e)
        {
            if (!SaveData())
                return;
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Finish;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void StoredProcDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            mParent.FormLocation = Location;
            mParent.FormSize = Size;
        }

        private enum StoreProcOperation
        {
            Select = 1,
            Insert = 2,
            Delete = 3,
            Update = 4,
        }
    }
}