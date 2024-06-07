using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Advantage.Data.Provider.Properties;

namespace Advantage.Data.Provider
{
    public class QueryBuildDlg : Form
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
        private Button mQueryBuildButton;
        private Button mAdvancedButton;
        private Label mQueryLabel;
        private TextBox mQueryText;
        private Label mInstructLabel;
        private ConfigWizard mParent;

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
            mSubtitleLabel = new Label();
            mTitleLabel = new Label();
            mHeaderSeparator = new GroupBox();
            mHeaderPanel = new Panel();
            mQueryBuildButton = new Button();
            mAdvancedButton = new Button();
            mQueryLabel = new Label();
            mQueryText = new TextBox();
            mInstructLabel = new Label();
            mHeaderPicture = new PictureBox();
            ((ISupportInitialize)mHeaderPicture).BeginInit();
            SuspendLayout();
            mBackButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mBackButton.Location = new Point(244, 328);
            mBackButton.Name = "mBackButton";
            mBackButton.Size = new Size(75, 26);
            mBackButton.TabIndex = 14;
            mBackButton.Text = "< &Back";
            mBackButton.Click += mBackButton_Click;
            mNextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mNextButton.Location = new Point(328, 328);
            mNextButton.Name = "mNextButton";
            mNextButton.Size = new Size(75, 26);
            mNextButton.TabIndex = 15;
            mNextButton.Text = "&Next >";
            mNextButton.Click += mNextButton_Click;
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
            mSubtitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mSubtitleLabel.BackColor = Color.White;
            mSubtitleLabel.Location = new Point(32, 25);
            mSubtitleLabel.Name = "mSubtitleLabel";
            mSubtitleLabel.Size = new Size(367, 26);
            mSubtitleLabel.TabIndex = 19;
            mSubtitleLabel.Text =
                "The Select statement will be used to create the INSERT, UPDATE and DELETE statements.";
            mTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTitleLabel.BackColor = Color.White;
            mTitleLabel.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTitleLabel.Location = new Point(16, 10);
            mTitleLabel.Name = "mTitleLabel";
            mTitleLabel.Size = new Size(410, 13);
            mTitleLabel.TabIndex = 18;
            mTitleLabel.Text = "Generate the SQL Statements";
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
            mQueryBuildButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mQueryBuildButton.Location = new Point(344, 272);
            mQueryBuildButton.Name = "mQueryBuildButton";
            mQueryBuildButton.Size = new Size(128, 23);
            mQueryBuildButton.TabIndex = 26;
            mQueryBuildButton.Text = "&Select Generator...";
            mQueryBuildButton.Click += mQueryBuildButton_Click;
            mAdvancedButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            mAdvancedButton.Location = new Point(24, 272);
            mAdvancedButton.Name = "mAdvancedButton";
            mAdvancedButton.Size = new Size(128, 23);
            mAdvancedButton.TabIndex = 25;
            mAdvancedButton.Text = "&Advanced Options...";
            mAdvancedButton.Click += mAdvancedButton_Click;
            mQueryLabel.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mQueryLabel.Location = new Point(24, 120);
            mQueryLabel.Name = "mQueryLabel";
            mQueryLabel.Size = new Size(424, 16);
            mQueryLabel.TabIndex = 24;
            mQueryLabel.Text = "&What data should the data adapter load into the dataset?";
            mQueryText.AcceptsReturn = true;
            mQueryText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mQueryText.Location = new Point(24, 136);
            mQueryText.Multiline = true;
            mQueryText.Name = "mQueryText";
            mQueryText.Size = new Size(448, 128);
            mQueryText.TabIndex = 23;
            mQueryText.TextChanged += mQueryText_TextChanged;
            mInstructLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mInstructLabel.Location = new Point(24, 72);
            mInstructLabel.Name = "mInstructLabel";
            mInstructLabel.Size = new Size(456, 40);
            mInstructLabel.TabIndex = 22;
            mInstructLabel.Text =
                "Type in your SQL SELECT statement or use the generator to generate simple statements that may be modified.";
            mHeaderPicture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mHeaderPicture.BackColor = Color.White;
            mHeaderPicture.Image = Resources.QueryBuild;
            mHeaderPicture.Location = new Point(416, 3);
            mHeaderPicture.Name = "mHeaderPicture";
            mHeaderPicture.Size = new Size(75, 51);
            mHeaderPicture.TabIndex = 21;
            mHeaderPicture.TabStop = false;
            AcceptButton = mNextButton;
            AutoScaleMode = AutoScaleMode.Inherit;
            CancelButton = mCancelButton;
            ClientSize = new Size(497, 365);
            Controls.Add(mQueryBuildButton);
            Controls.Add(mAdvancedButton);
            Controls.Add(mQueryLabel);
            Controls.Add(mQueryText);
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
            Name = nameof(QueryBuildDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Data Adapter Configuration Wizard";
            Load += QueryBuildDlg_Load;
            FormClosed += QueryBuildDlg_FormClosed;
            ((ISupportInitialize)mHeaderPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        public QueryBuildDlg(ConfigWizard parent)
        {
            mParent = parent;
            InitializeComponent();
        }

        private void QueryBuildDlg_Load(object sender, EventArgs e)
        {
            mQueryText.Text = mParent.SelectCommand.CommandText;
            if (mParent.ReadyToFinish)
            {
                mNextButton.Enabled = true;
                mFinishButton.Enabled = true;
            }
            else
                mFinishButton.Enabled = false;

            mParent.SelectCommand.CommandType = CommandType.Text;
            mParent.InsertCommand.CommandType = CommandType.Text;
            mParent.DeleteCommand.CommandType = CommandType.Text;
            mParent.UpdateCommand.CommandType = CommandType.Text;
            Size = mParent.FormSize;
            Location = mParent.FormLocation;
        }

        protected bool SaveData()
        {
            if (mQueryText.Text.Length == 0)
            {
                mQueryText.Select(0, mQueryText.Text.Length);
                return false;
            }

            mParent.SelectCommand.CommandText = mQueryText.Text;
            mParent.SelectCommand.CommandType = CommandType.Text;
            mParent.QueryType = ConfigWizard.QueryTypes.SqlQuery;
            if (!mParent.PrimaryKeyRequired)
            {
                if (!mParent.OptimisticConcurrency)
                {
                    var num = (int)MessageBox.Show(
                        "\"Require Primary Key\" cannot be disabled when the Advanced Option \"Optimistic Concurrency\" is also disabled.",
                        ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

            try
            {
                var adsConnection = new AdsConnection(mParent.ConnectionString);
                adsConnection.Open();
                var adsCommand = new AdsCommand(mQueryText.Text, adsConnection);
                try
                {
                    adsCommand.VerifySQL(mQueryText.Text);
                }
                catch (Exception ex)
                {
                    var num = (int)MessageBox.Show("Error parsing SELECT statement.\n\n" + ex);
                    mQueryText.Select(0, mQueryText.Text.Length);
                    return false;
                }

                adsConnection.Close();
            }
            catch (Exception ex)
            {
                var num = (int)MessageBox.Show("Error opening connection.\n\n" + ex);
                mQueryText.Select(0, mQueryText.Text.Length);
                return false;
            }

            return true;
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

        private void mQueryBuildButton_Click(object sender, EventArgs e)
        {
            var selectDlg = new SelectDlg(mParent.ConnectionString, mQueryText.Text);
            if (selectDlg.ShowDialog(this) != DialogResult.OK)
                return;
            mQueryText.Text = selectDlg.SelectQuery;
        }

        private void mQueryText_TextChanged(object sender, EventArgs e)
        {
            if (mQueryText.Text.Length > 0)
                mNextButton.Enabled = true;
            else
                mNextButton.Enabled = false;
            if (mQueryText.Text.Length > 0)
                mFinishButton.Enabled = true;
            else
                mFinishButton.Enabled = false;
        }

        public bool Ready => mQueryText.Text.Length > 0;

        private void mCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void mBackButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.QueryType;
            DialogResult = DialogResult.OK;
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

        private void QueryBuildDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            mParent.FormLocation = Location;
            mParent.FormSize = Size;
        }
    }
}