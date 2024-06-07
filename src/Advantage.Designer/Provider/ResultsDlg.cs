using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using Advantage.Data.Provider.Properties;

namespace Advantage.Data.Provider
{
    public class ResultsDlg : Form
    {
        private const string strExpr = "Expr";
        private ConfigWizard mParent;
        private ArrayList mLabels = new ArrayList();
        private ArrayList mIcons = new ArrayList();
        private int miMessageNumber;
        private bool bCloseAfterObjGen;
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
        private PictureBox checkPictureBox;
        private PictureBox warnPictureBox;
        private Label mLabel6;
        private PictureBox mPictureBox6;
        private Label mLabel5;
        private PictureBox mPictureBox5;
        private Label mLabel4;
        private PictureBox mPictureBox4;
        private Label mLabel3;
        private PictureBox mPictureBox3;
        private Label mLabel2;
        private PictureBox mPictureBox2;
        private Label mLabel1;
        private PictureBox mPictureBox1;
        private Label mApplyLabel;
        private Label mDetailsLabel;
        private Label mResultLabel;

        public ResultsDlg(ConfigWizard parent)
        {
            mParent = parent;
            InitializeComponent();
            mLabels.Add(mLabel1);
            mLabels.Add(mLabel2);
            mLabels.Add(mLabel3);
            mLabels.Add(mLabel4);
            mLabels.Add(mLabel5);
            mLabels.Add(mLabel6);
            mIcons.Add(mPictureBox1);
            mIcons.Add(mPictureBox2);
            mIcons.Add(mPictureBox3);
            mIcons.Add(mPictureBox4);
            mIcons.Add(mPictureBox5);
            mIcons.Add(mPictureBox6);
        }

        private void ResultsDlg_Load(object sender, EventArgs e)
        {
            Size = mParent.FormSize;
            Location = mParent.FormLocation;
            foreach (Control mLabel in mLabels)
                mLabel.Visible = false;
            foreach (Control mIcon in mIcons)
                mIcon.Visible = false;
            if (mParent.QueryType == ConfigWizard.QueryTypes.StoredProc)
                GenerateForStoredProc();
            else
                GenerateForSqlQuery();
            if (!bCloseAfterObjGen)
                return;
            DialogResult = DialogResult.OK;
            mParent.mNextDlg = ConfigWizard.WizardDialogs.AllDone;
            Close();
        }

        private void GenerateForStoredProc()
        {
            miMessageNumber = 0;
            var bSuccess = true;
            if (mParent.SelectCommand.CommandText != null)
            {
                DisplayMessage(IconType.OK, "Generated SELECT statement.");
            }
            else
            {
                DisplayMessage(IconType.Warn, "Select Command is empty.");
                bSuccess = false;
            }

            if (bSuccess)
            {
                try
                {
                    var tableMapping = mParent.TableMapping;
                    tableMapping.SourceTable = "Table";
                    tableMapping.DataSetTable = mParent.SelectCommand.CommandText;
                    tableMapping.ColumnMappings.Clear();
                    foreach (AdsParameter parameter in (DbParameterCollection)mParent.SelectCommand.Parameters)
                    {
                        if (ParameterDirection.Output == parameter.Direction)
                            tableMapping.ColumnMappings.Add(parameter.ParameterName,
                                parameter.ParameterName);
                    }

                    DisplayMessage(IconType.OK, "Generated table mappings.");
                }
                catch (Exception ex)
                {
                    DisplayMessage(IconType.Warn, "Error generating table mappings. " + ex.Message);
                    bSuccess = false;
                }
            }

            if (mParent.InsertCommand.CommandText != null)
                DisplayMessage(IconType.OK, "Generated INSERT statement.");
            if (mParent.UpdateCommand.CommandText != null)
                DisplayMessage(IconType.OK, "Generated UPDATE statement.");
            if (mParent.DeleteCommand.CommandText != null)
                DisplayMessage(IconType.OK, "Generated DELETE statement.");
            if (!bSuccess)
                bCloseAfterObjGen = false;
            SetResultLabel(bSuccess);
        }

        private void GenerateForSqlQuery()
        {
            miMessageNumber = 0;
            var bSuccess = true;
            AdsConnection adsConnection = null;
            AdsDataReader adsDataReader = null;
            DataTable dataTable = null;
            AdsDataAdapter adsDataAdapter = null;
            AdsCommandBuilder adsCommandBuilder = null;
            try
            {
                adsConnection = new AdsConnection(mParent.ConnectionString);
                mParent.SelectCommand.Connection = adsConnection;
                adsConnection.Open();
            }
            catch (Exception ex)
            {
                DisplayMessage(IconType.Warn, "Unable to open connection. " + ex.Message);
                bSuccess = false;
            }

            if (bSuccess)
            {
                DisplayMessage(IconType.OK, "Generated SELECT Statement.");
                try
                {
                    adsDataReader =
                        mParent.SelectCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
                }
                catch (Exception ex)
                {
                    DisplayMessage(IconType.Warn, "Unable to execute data reader. " + ex.Message);
                    bSuccess = false;
                }
            }

            if (bSuccess)
            {
                try
                {
                    dataTable = adsDataReader.GetSchemaTable();
                    if (adsDataReader.IsStatic)
                    {
                        DisplayMessage(IconType.Warn,
                            "Unable to generate table mapping for static cursor.");
                        bSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage(IconType.Warn, "Unable to read schema. " + ex.Message);
                    bSuccess = false;
                }
            }

            if (bSuccess)
            {
                var flag1 = false;
                var flag2 = false;
                try
                {
                    var str1 = "";
                    if (dataTable.Rows.Count > 0)
                    {
                        str1 = dataTable.Rows[0]["BaseTableName"].ToString();
                    }
                    else
                    {
                        DisplayMessage(IconType.Warn,
                            "Unable to generate table mapping. No data returned.");
                        bSuccess = false;
                    }

                    if (bSuccess)
                    {
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                        {
                            if (row["IsRowversion"].Equals(true))
                                flag2 = true;
                            if (row["IsKey"].Equals(true) || row["IsUnique"].Equals(true))
                                flag1 = true;
                        }

                        if (mParent.PrimaryKeyRequired && !flag1)
                            DisplayMessage(IconType.Warn,
                                "Could not uniquely identify columns in \"" + str1 + "\".");
                        if (mParent.UseRowversion && !flag2)
                            mParent.UseRowversion = false;
                        var tableMapping = mParent.TableMapping;
                        tableMapping.ColumnMappings.Clear();
                        tableMapping.SourceTable = "Table";
                        tableMapping.DataSetTable = str1;
                        var num = 1;
                        foreach (DataRow row in (InternalDataCollectionBase)dataTable.Rows)
                        {
                            var str2 = row["ColumnName"].ToString();
                            while (tableMapping.ColumnMappings.IndexOf(str2) != -1)
                            {
                                str2 = "Expr" + num;
                                ++num;
                            }

                            tableMapping.ColumnMappings.Add(str2, str2);
                        }

                        DisplayMessage(IconType.OK, "Generated table mappings.");
                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage(IconType.Warn, "Error generating table mapping. " + ex.Message);
                    bSuccess = false;
                }

                adsDataReader?.Close();
            }

            if (bSuccess)
            {
                if (!mParent.GenerateCommands)
                {
                    mParent.InsertCommand.CommandText = null;
                    mParent.InsertCommand.Parameters.Clear();
                    mParent.DeleteCommand.CommandText = null;
                    mParent.DeleteCommand.Parameters.Clear();
                    mParent.UpdateCommand.CommandText = null;
                    mParent.UpdateCommand.Parameters.Clear();
                }
                else
                {
                    try
                    {
                        adsDataAdapter = new AdsDataAdapter(mParent.SelectCommand);
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(IconType.Warn, "Error configuring the adapter. " + ex.Message);
                        bSuccess = false;
                    }

                    if (bSuccess)
                    {
                        try
                        {
                            adsCommandBuilder = new AdsCommandBuilder(adsDataAdapter)
                            {
                                UsePKOnlyInWhereClause = !mParent.OptimisticConcurrency,
                                UseRowversionOnlyInWhereClause = mParent.UseRowversion
                            };
                            adsCommandBuilder.RequirePrimaryKey = adsCommandBuilder.UsePKOnlyInWhereClause;
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(IconType.Warn, "Error generating statements. " + ex.Message);
                            bSuccess = false;
                        }
                    }
                }

                if (bSuccess)
                {
                    try
                    {
                        mParent.InsertCommand = adsCommandBuilder.GetInsertCommand();
                        if (mParent.RefreshDataset)
                        {
                            var insertCommand = mParent.InsertCommand;
                            insertCommand.CommandText = insertCommand.CommandText + "; " +
                                                        adsCommandBuilder.GetInsertRefreshStatement();
                        }

                        DisplayMessage(IconType.OK, "Generated INSERT statement.");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(IconType.Warn, "Error creating INSERT statement. " + ex.Message);
                        bSuccess = false;
                    }

                    try
                    {
                        mParent.DeleteCommand = adsCommandBuilder.GetDeleteCommand();
                        DisplayMessage(IconType.OK, "Generated DELETE statement.");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(IconType.Warn, "Error creating DELETE statement. " + ex.Message);
                        bSuccess = false;
                    }

                    try
                    {
                        mParent.UpdateCommand = adsCommandBuilder.GetUpdateCommand();
                        if (mParent.RefreshDataset)
                        {
                            var updateCommand = mParent.UpdateCommand;
                            updateCommand.CommandText = updateCommand.CommandText + "; " +
                                                        adsCommandBuilder.GetUpdateRefreshStatement();
                        }

                        DisplayMessage(IconType.OK, "Generated UPDATE statement.");
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(IconType.Warn, "Error creating UPDATE statement. " + ex.Message);
                        bSuccess = false;
                    }
                }
            }

            if (adsConnection != null && adsConnection.State != ConnectionState.Closed)
                adsConnection.Close();
            SetResultLabel(bSuccess);
            if (bSuccess)
                return;
            bCloseAfterObjGen = false;
        }

        private void SetResultLabel(bool bSuccess)
        {
            if (bSuccess)
                mResultLabel.Text =
                    "The data adapter \"" + mParent.AdapterName + "\" was configured successfully.";
            else
                mResultLabel.Text =
                    "The wizard detect the following problems when configuring the data adapter \"" +
                    mParent.AdapterName + "\".";
        }

        private void DisplayMessage(IconType icon, string strMessage)
        {
            if (miMessageNumber > 5)
                return;
            switch (icon)
            {
                case IconType.OK:
                    ((PictureBox)mIcons[miMessageNumber]).Image = checkPictureBox.Image;
                    ((Control)mIcons[miMessageNumber]).Visible = true;
                    break;
                case IconType.Warn:
                    ((PictureBox)mIcons[miMessageNumber]).Image = warnPictureBox.Image;
                    ((Control)mIcons[miMessageNumber]).Visible = true;
                    break;
                default:
                    ((Control)mIcons[miMessageNumber]).Visible = false;
                    break;
            }

            ((Control)mLabels[miMessageNumber]).Text = strMessage;
            ((Control)mLabels[miMessageNumber]).Visible = true;
            ++miMessageNumber;
        }

        internal bool Hidden
        {
            set => bCloseAfterObjGen = value;
        }

        private void mCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void mBackButton_Click(object sender, EventArgs e)
        {
            if (mParent.QueryType == ConfigWizard.QueryTypes.TableDirect)
                mParent.mNextDlg = ConfigWizard.WizardDialogs.TableDirect;
            else if (mParent.QueryType == ConfigWizard.QueryTypes.SqlQuery)
                mParent.mNextDlg = ConfigWizard.WizardDialogs.QueryBuild;
            else if (mParent.QueryType == ConfigWizard.QueryTypes.StoredProc)
                mParent.mNextDlg = ConfigWizard.WizardDialogs.StoredProc;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mFinishButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.AllDone;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ResultsDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            mParent.FormLocation = Location;
            mParent.FormSize = Size;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            var componentResourceManager = new ComponentResourceManager(typeof(ResultsDlg));
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
            checkPictureBox = new PictureBox();
            warnPictureBox = new PictureBox();
            mLabel6 = new Label();
            mPictureBox6 = new PictureBox();
            mLabel5 = new Label();
            mPictureBox5 = new PictureBox();
            mLabel4 = new Label();
            mPictureBox4 = new PictureBox();
            mLabel3 = new Label();
            mPictureBox3 = new PictureBox();
            mLabel2 = new Label();
            mPictureBox2 = new PictureBox();
            mLabel1 = new Label();
            mPictureBox1 = new PictureBox();
            mApplyLabel = new Label();
            mDetailsLabel = new Label();
            mResultLabel = new Label();
            ((ISupportInitialize)mHeaderPicture).BeginInit();
            ((ISupportInitialize)checkPictureBox).BeginInit();
            ((ISupportInitialize)warnPictureBox).BeginInit();
            ((ISupportInitialize)mPictureBox6).BeginInit();
            ((ISupportInitialize)mPictureBox5).BeginInit();
            ((ISupportInitialize)mPictureBox4).BeginInit();
            ((ISupportInitialize)mPictureBox3).BeginInit();
            ((ISupportInitialize)mPictureBox2).BeginInit();
            ((ISupportInitialize)mPictureBox1).BeginInit();
            SuspendLayout();
            mBackButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mBackButton.Location = new Point(244, 328);
            mBackButton.Name = "mBackButton";
            mBackButton.Size = new Size(75, 26);
            mBackButton.TabIndex = 14;
            mBackButton.Text = "< &Back";
            mBackButton.Click += mBackButton_Click;
            mNextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mNextButton.Enabled = false;
            mNextButton.Location = new Point(328, 328);
            mNextButton.Name = "mNextButton";
            mNextButton.Size = new Size(75, 26);
            mNextButton.TabIndex = 15;
            mNextButton.Text = "&Next >";
            mCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mCancelButton.DialogResult = DialogResult.Cancel;
            mCancelButton.Location = new Point(160, 328);
            mCancelButton.Name = "mCancelButton";
            mCancelButton.Size = new Size(75, 26);
            mCancelButton.TabIndex = 13;
            mCancelButton.Text = "Cancel";
            mCancelButton.Click += mCancelButton_Click;
            mFinishButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mFinishButton.Location = new Point(412, 328);
            mFinishButton.Name = "mFinishButton";
            mFinishButton.Size = new Size(75, 26);
            mFinishButton.TabIndex = 16;
            mFinishButton.Text = "&Finish";
            mFinishButton.Click += mFinishButton_Click;
            mSeparator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mSeparator.Location = new Point(0, 313);
            mSeparator.Name = "mSeparator";
            mSeparator.Size = new Size(499, 2);
            mSeparator.TabIndex = 12;
            mSeparator.TabStop = false;
            mHeaderPicture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mHeaderPicture.BackColor = Color.White;
            mHeaderPicture.Image = Resources.QueryBuild;
            mHeaderPicture.Location = new Point(416, 3);
            mHeaderPicture.Name = "mHeaderPicture";
            mHeaderPicture.Size = new Size(75, 51);
            mHeaderPicture.TabIndex = 21;
            mHeaderPicture.TabStop = false;
            mSubtitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mSubtitleLabel.BackColor = Color.White;
            mSubtitleLabel.Location = new Point(32, 25);
            mSubtitleLabel.Name = "mSubtitleLabel";
            mSubtitleLabel.Size = new Size(367, 26);
            mSubtitleLabel.TabIndex = 19;
            mSubtitleLabel.Text =
                "Review the list of tasks the wizard has performed. Click Finish to complete or Back to make changes.";
            mTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTitleLabel.BackColor = Color.White;
            mTitleLabel.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTitleLabel.Location = new Point(16, 10);
            mTitleLabel.Name = "mTitleLabel";
            mTitleLabel.Size = new Size(410, 13);
            mTitleLabel.TabIndex = 18;
            mTitleLabel.Text = "View Wizard  Results";
            mHeaderSeparator.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mHeaderSeparator.Location = new Point(0, 58);
            mHeaderSeparator.Name = "mHeaderSeparator";
            mHeaderSeparator.Size = new Size(499, 2);
            mHeaderSeparator.TabIndex = 20;
            mHeaderSeparator.TabStop = false;
            mHeaderPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mHeaderPanel.BackColor = Color.White;
            mHeaderPanel.Location = new Point(0, 0);
            mHeaderPanel.Name = "mHeaderPanel";
            mHeaderPanel.Size = new Size(497, 58);
            mHeaderPanel.TabIndex = 17;
            checkPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            checkPictureBox.Image = (Image)componentResourceManager.GetObject("checkPictureBox.Image");
            checkPictureBox.Location = new Point(480, 88);
            checkPictureBox.Name = "checkPictureBox";
            checkPictureBox.Size = new Size(12, 12);
            checkPictureBox.TabIndex = 42;
            checkPictureBox.TabStop = false;
            checkPictureBox.Visible = false;
            warnPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            warnPictureBox.Image = (Image)componentResourceManager.GetObject("warnPictureBox.Image");
            warnPictureBox.Location = new Point(480, 72);
            warnPictureBox.Name = "warnPictureBox";
            warnPictureBox.Size = new Size(12, 12);
            warnPictureBox.TabIndex = 41;
            warnPictureBox.TabStop = false;
            warnPictureBox.Visible = false;
            mLabel6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mLabel6.Location = new Point(64, 260);
            mLabel6.Name = "mLabel6";
            mLabel6.Size = new Size(416, 26);
            mLabel6.TabIndex = 35;
            mLabel6.Visible = false;
            mPictureBox6.Image = (Image)componentResourceManager.GetObject("mPictureBox6.Image");
            mPictureBox6.Location = new Point(32, 260);
            mPictureBox6.Name = "mPictureBox6";
            mPictureBox6.Size = new Size(12, 12);
            mPictureBox6.TabIndex = 40;
            mPictureBox6.TabStop = false;
            mPictureBox6.Visible = false;
            mLabel5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mLabel5.Location = new Point(64, 232);
            mLabel5.Name = "mLabel5";
            mLabel5.Size = new Size(416, 26);
            mLabel5.TabIndex = 33;
            mLabel5.Visible = false;
            mPictureBox5.Image = (Image)componentResourceManager.GetObject("mPictureBox5.Image");
            mPictureBox5.Location = new Point(32, 232);
            mPictureBox5.Name = "mPictureBox5";
            mPictureBox5.Size = new Size(12, 12);
            mPictureBox5.TabIndex = 39;
            mPictureBox5.TabStop = false;
            mPictureBox5.Visible = false;
            mLabel4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mLabel4.Location = new Point(64, 204);
            mLabel4.Name = "mLabel4";
            mLabel4.Size = new Size(416, 26);
            mLabel4.TabIndex = 32;
            mLabel4.Visible = false;
            mPictureBox4.Image = (Image)componentResourceManager.GetObject("mPictureBox4.Image");
            mPictureBox4.Location = new Point(32, 204);
            mPictureBox4.Name = "mPictureBox4";
            mPictureBox4.Size = new Size(12, 12);
            mPictureBox4.TabIndex = 38;
            mPictureBox4.TabStop = false;
            mPictureBox4.Visible = false;
            mLabel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mLabel3.Location = new Point(64, 176);
            mLabel3.Name = "mLabel3";
            mLabel3.Size = new Size(416, 26);
            mLabel3.TabIndex = 30;
            mLabel3.Visible = false;
            mPictureBox3.Image = (Image)componentResourceManager.GetObject("mPictureBox3.Image");
            mPictureBox3.Location = new Point(32, 176);
            mPictureBox3.Name = "mPictureBox3";
            mPictureBox3.Size = new Size(12, 12);
            mPictureBox3.TabIndex = 36;
            mPictureBox3.TabStop = false;
            mPictureBox3.Visible = false;
            mLabel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mLabel2.Location = new Point(64, 148);
            mLabel2.Name = "mLabel2";
            mLabel2.Size = new Size(416, 26);
            mLabel2.TabIndex = 29;
            mLabel2.Visible = false;
            mPictureBox2.Image = (Image)componentResourceManager.GetObject("mPictureBox2.Image");
            mPictureBox2.Location = new Point(32, 148);
            mPictureBox2.Name = "mPictureBox2";
            mPictureBox2.Size = new Size(12, 12);
            mPictureBox2.TabIndex = 34;
            mPictureBox2.TabStop = false;
            mPictureBox2.Visible = false;
            mLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mLabel1.Location = new Point(64, 120);
            mLabel1.Name = "mLabel1";
            mLabel1.Size = new Size(416, 26);
            mLabel1.TabIndex = 28;
            mLabel1.Visible = false;
            mPictureBox1.Image = (Image)componentResourceManager.GetObject("mPictureBox1.Image");
            mPictureBox1.Location = new Point(32, 120);
            mPictureBox1.Name = "mPictureBox1";
            mPictureBox1.Size = new Size(12, 12);
            mPictureBox1.TabIndex = 31;
            mPictureBox1.TabStop = false;
            mPictureBox1.Visible = false;
            mApplyLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            mApplyLabel.Location = new Point(24, 290);
            mApplyLabel.Name = "mApplyLabel";
            mApplyLabel.Size = new Size(408, 16);
            mApplyLabel.TabIndex = 37;
            mApplyLabel.Text = "To apply these settings to your adapter, click Finish.";
            mDetailsLabel.Location = new Point(24, 104);
            mDetailsLabel.Name = "mDetailsLabel";
            mDetailsLabel.Size = new Size(80, 16);
            mDetailsLabel.TabIndex = 27;
            mDetailsLabel.Text = "Details:";
            mResultLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mResultLabel.Location = new Point(24, 72);
            mResultLabel.Name = "mResultLabel";
            mResultLabel.Size = new Size(448, 32);
            mResultLabel.TabIndex = 26;
            mResultLabel.Text = "Result";
            AcceptButton = mNextButton;
            AutoScaleMode = AutoScaleMode.Inherit;
            CancelButton = mCancelButton;
            ClientSize = new Size(497, 365);
            Controls.Add(checkPictureBox);
            Controls.Add(warnPictureBox);
            Controls.Add(mLabel6);
            Controls.Add(mPictureBox6);
            Controls.Add(mLabel5);
            Controls.Add(mPictureBox5);
            Controls.Add(mLabel4);
            Controls.Add(mPictureBox4);
            Controls.Add(mLabel3);
            Controls.Add(mPictureBox3);
            Controls.Add(mLabel2);
            Controls.Add(mPictureBox2);
            Controls.Add(mLabel1);
            Controls.Add(mPictureBox1);
            Controls.Add(mApplyLabel);
            Controls.Add(mDetailsLabel);
            Controls.Add(mResultLabel);
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
            Name = nameof(ResultsDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Data Adapter Configuration Wizard";
            Load += ResultsDlg_Load;
            FormClosed += ResultsDlg_FormClosed;
            ((ISupportInitialize)mHeaderPicture).EndInit();
            ((ISupportInitialize)checkPictureBox).EndInit();
            ((ISupportInitialize)warnPictureBox).EndInit();
            ((ISupportInitialize)mPictureBox6).EndInit();
            ((ISupportInitialize)mPictureBox5).EndInit();
            ((ISupportInitialize)mPictureBox4).EndInit();
            ((ISupportInitialize)mPictureBox3).EndInit();
            ((ISupportInitialize)mPictureBox2).EndInit();
            ((ISupportInitialize)mPictureBox1).EndInit();
            ResumeLayout(false);
        }

        public enum IconType
        {
            OK = 1,
            Warn = 2,
            None = 3,
        }
    }
}