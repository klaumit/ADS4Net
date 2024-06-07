using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Advantage.Data.Provider.Properties;

namespace Advantage.Data.Provider
{
    public class TableDirectDlg : Form
    {
        private ConfigWizard mParent;
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
        private Button mAdvancedButton;
        private Label mInstructLabel;
        private Label mTableLabel;
        private ListBox mTableList;

        public TableDirectDlg(ConfigWizard parent)
        {
            mParent = parent;
            InitializeComponent();
        }

        private void TableDirectDlg_Load(object sender, EventArgs e)
        {
            Size = mParent.FormSize;
            Location = mParent.FormLocation;
            Cursor.Current = Cursors.WaitCursor;
            if (Ready)
            {
                mNextButton.Enabled = true;
                mFinishButton.Enabled = true;
            }
            else
            {
                mNextButton.Enabled = false;
                mFinishButton.Enabled = false;
            }

            if (mParent.ConnectionString.Length > 0)
            {
                var adsConnection = new AdsConnection(mParent.ConnectionString);
                try
                {
                    adsConnection.Open();
                    var tableNames = adsConnection.GetTableNames();
                    adsConnection.Close();
                    mTableList.Items.Clear();
                    mTableList.Items.AddRange(tableNames);
                    mTableList.Sorted = true;
                    if (mTableList.Items.Count > 0)
                    {
                        mTableList.Enabled = true;
                        mTableLabel.Enabled = true;
                        mTableList.SelectedIndex =
                            mTableList.FindStringExact(mParent.SelectCommand.CommandText);
                    }
                    else
                    {
                        var num = (int)MessageBox.Show("Unable to retrieve any tables for the connection.",
                            ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    var num = (int)MessageBox.Show("Connection failed to open.\n\n" + ex,
                        ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                adsConnection.Close();
            }
            else
            {
                var num1 = (int)MessageBox.Show("Invalid Connection String", ConfigWizard.MessageBoxTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            mParent.SelectCommand.CommandType = CommandType.TableDirect;
            mParent.InsertCommand.CommandType = CommandType.Text;
            mParent.DeleteCommand.CommandType = CommandType.Text;
            mParent.UpdateCommand.CommandType = CommandType.Text;
            Cursor.Current = Cursors.Default;
        }

        private void mFinishButton_Click(object sender, EventArgs e)
        {
            if (!SaveData())
                return;
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Finish;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mAdvancedButton_Click(object sender, EventArgs e)
        {
            var advSqlDlg = new AdvSqlDlg(mParent);
            if (advSqlDlg.ShowDialog(this) != DialogResult.OK)
                return;
            mParent.OptimisticConcurrency = advSqlDlg.OptimisticConcurrency;
            mParent.GenerateCommands = advSqlDlg.GenerateCommands;
            mParent.PrimaryKeyRequired = advSqlDlg.PrimaryKeyRequired;
            mParent.UseRowversion = advSqlDlg.UseRowversion;
            mParent.RefreshDataset = advSqlDlg.RefreshDataset;
        }

        public bool Ready => mTableList.SelectedIndex != -1;

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

        private void mBackButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.QueryType;
            DialogResult = DialogResult.OK;
            Close();
        }

        protected bool SaveData()
        {
            var selectedIndex = mTableList.SelectedIndex;
            if (selectedIndex == -1)
            {
                var num = (int)MessageBox.Show("No Table was selected.", ConfigWizard.MessageBoxTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            mParent.SelectCommand.CommandText = mTableList.Items[selectedIndex].ToString();
            mParent.SelectCommand.CommandType = CommandType.TableDirect;
            mParent.QueryType = ConfigWizard.QueryTypes.TableDirect;
            return true;
        }

        private void TableDirectDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            mParent.FormLocation = Location;
            mParent.FormSize = Size;
        }

        private void mTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ready)
            {
                mNextButton.Enabled = true;
                mFinishButton.Enabled = true;
            }
            else
            {
                mNextButton.Enabled = false;
                mFinishButton.Enabled = false;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
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
            mAdvancedButton = new Button();
            mInstructLabel = new Label();
            mTableLabel = new Label();
            mTableList = new ListBox();
            ((ISupportInitialize)mHeaderPicture).BeginInit();
            SuspendLayout();
            mBackButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mBackButton.Location = new Point(244, 328);
            mBackButton.Name = "mBackButton";
            mBackButton.Size = new Size(75, 26);
            mBackButton.TabIndex = 3;
            mBackButton.Text = "< &Back";
            mBackButton.Click += mBackButton_Click;
            mNextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mNextButton.Location = new Point(328, 328);
            mNextButton.Name = "mNextButton";
            mNextButton.Size = new Size(75, 26);
            mNextButton.TabIndex = 4;
            mNextButton.Text = "&Next >";
            mNextButton.Click += mNextButton_Click;
            mCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mCancelButton.DialogResult = DialogResult.Cancel;
            mCancelButton.Location = new Point(160, 328);
            mCancelButton.Name = "mCancelButton";
            mCancelButton.Size = new Size(75, 26);
            mCancelButton.TabIndex = 2;
            mCancelButton.Text = "Cancel";
            mCancelButton.Click += mCancelButton_Click;
            mFinishButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mFinishButton.Location = new Point(412, 328);
            mFinishButton.Name = "mFinishButton";
            mFinishButton.Size = new Size(75, 26);
            mFinishButton.TabIndex = 5;
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
                "The selected table will be used to create the INSERT, UPDATE and DELETE statements.";
            mTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTitleLabel.BackColor = Color.White;
            mTitleLabel.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTitleLabel.Location = new Point(16, 10);
            mTitleLabel.Name = "mTitleLabel";
            mTitleLabel.Size = new Size(410, 13);
            mTitleLabel.TabIndex = 18;
            mTitleLabel.Text = "Use Table Directly to Generate SQL Statements";
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
            mAdvancedButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            mAdvancedButton.Location = new Point(16, 272);
            mAdvancedButton.Name = "mAdvancedButton";
            mAdvancedButton.Size = new Size(128, 23);
            mAdvancedButton.TabIndex = 1;
            mAdvancedButton.Text = "&Advanced Options...";
            mAdvancedButton.Click += mAdvancedButton_Click;
            mInstructLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            mInstructLabel.Location = new Point(16, 104);
            mInstructLabel.Name = "mInstructLabel";
            mInstructLabel.Size = new Size(224, 128);
            mInstructLabel.TabIndex = 28;
            mInstructLabel.Text =
                "Select a table name. The table will be opened directly by the Advantage .NET Data Provider.";
            mTableLabel.Enabled = false;
            mTableLabel.Location = new Point(264, 80);
            mTableLabel.Name = "mTableLabel";
            mTableLabel.Size = new Size(112, 16);
            mTableLabel.TabIndex = 27;
            mTableLabel.Text = "&Tables and Views:";
            mTableList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mTableList.Enabled = false;
            mTableList.Location = new Point(264, 96);
            mTableList.Name = "mTableList";
            mTableList.Size = new Size(216, 199);
            mTableList.TabIndex = 0;
            mTableList.SelectedIndexChanged += mTableList_SelectedIndexChanged;
            AcceptButton = mNextButton;
            AutoScaleMode = AutoScaleMode.Inherit;
            CancelButton = mCancelButton;
            ClientSize = new Size(497, 365);
            Controls.Add(mAdvancedButton);
            Controls.Add(mInstructLabel);
            Controls.Add(mTableLabel);
            Controls.Add(mTableList);
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
            Name = nameof(TableDirectDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Data Adapter Configuration Wizard";
            Load += TableDirectDlg_Load;
            FormClosed += TableDirectDlg_FormClosed;
            ((ISupportInitialize)mHeaderPicture).EndInit();
            ResumeLayout(false);
        }
    }
}