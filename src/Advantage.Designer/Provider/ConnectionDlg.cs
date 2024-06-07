using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Advantage.Data.Provider.Properties;
using Microsoft.Win32;

namespace Advantage.Data.Provider
{
    public class ConnectionDlg : Form
    {
        private ConfigWizard mParent;
        public static string MRUkey = "ConnectionMRU";
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
        private Button mTestButton;
        private Button mConnStringButton;
        private TextBox mConnectionText;
        private Label mConnectionPrompt;
        private Label mConnectionLabel;

        public ConnectionDlg(ConfigWizard parent)
        {
            mParent = parent;
            InitializeComponent();
            mConnectionText.Text = mParent.ConnectionString;
            if (mConnectionText.Text.Length != 0)
                return;
            try
            {
                var registryKey = Registry.CurrentUser.OpenSubKey(ConfigWizard.DesignerRegKey);
                if (registryKey == null)
                    return;
                mConnectionText.Text = (string)registryKey.GetValue(MRUkey, "");
            }
            catch (Exception ex)
            {
            }
        }

        private void ConnectionDlg_Load(object sender, EventArgs e)
        {
            Size = mParent.FormSize;
            Location = mParent.FormLocation;
            if (mParent.ReadyToFinish)
                mFinishButton.Enabled = true;
            else
                mFinishButton.Enabled = false;
            if (mConnectionText.Text.Length == 0)
                mNextButton.Enabled = false;
            else
                mNextButton.Enabled = true;
        }

        protected bool ValidateConnectionString(ref string strResult)
        {
            if (mConnectionText.Text.Length == 0)
            {
                strResult = "No Connection String was entered.\n\n";
                return false;
            }

            try
            {
                var adsConnection = new AdsConnection(mConnectionText.Text);
                adsConnection.Open();
                adsConnection.Close();
            }
            catch (Exception ex)
            {
                strResult = "Connection failed to open.\n\n" + ex;
                return false;
            }

            strResult = "Connected successfully.";
            return true;
        }

        private void mNextButton_Click(object sender, EventArgs e)
        {
            var strResult = "";
            while (!ValidateConnectionString(ref strResult))
            {
                switch (MessageBox.Show(strResult, ConfigWizard.MessageBoxTitle, MessageBoxButtons.AbortRetryIgnore,
                            MessageBoxIcon.Hand))
                {
                    case DialogResult.Abort:
                        mConnectionText.Focus();
                        return;
                    case DialogResult.Retry:
                        continue;
                    default:
                        goto label_4;
                }
            }

            label_4:
            mParent.ConnectionString = mConnectionText.Text;
            try
            {
                Registry.CurrentUser.CreateSubKey(ConfigWizard.DesignerRegKey)
                    .SetValue(MRUkey, mConnectionText.Text);
            }
            catch (Exception ex)
            {
            }

            mParent.mNextDlg = ConfigWizard.WizardDialogs.QueryType;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mConnStringButton_Click(object sender, EventArgs e)
        {
            var buildConnString = new BuildConnString(mConnectionText.Text);
            if (buildConnString.ShowDialog(this) != DialogResult.OK)
                return;
            mConnectionText.Text = buildConnString.ConnectionString;
        }

        private void mTestButton_Click(object sender, EventArgs e)
        {
            var strResult = "";
            if (!ValidateConnectionString(ref strResult))
                mConnectionText.Focus();
            var num = (int)MessageBox.Show(strResult, ConfigWizard.MessageBoxTitle);
        }

        private void mConnectionText_TextChanged(object sender, EventArgs e)
        {
            if (mConnectionText.Text.Length == 0)
            {
                mNextButton.Enabled = false;
                mFinishButton.Enabled = false;
            }
            else
                mNextButton.Enabled = true;
        }

        public bool Ready
        {
            get
            {
                var strResult = "";
                return ValidateConnectionString(ref strResult);
            }
        }

        private void mCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void mBackButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Welcome;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mFinishButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Finish;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ConnectionDlg_FormClosed(object sender, FormClosedEventArgs e)
        {
            mParent.FormSize = Size;
            mParent.FormLocation = Location;
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
            mTestButton = new Button();
            mConnStringButton = new Button();
            mConnectionText = new TextBox();
            mConnectionPrompt = new Label();
            mConnectionLabel = new Label();
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
            mHeaderPicture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mHeaderPicture.BackColor = Color.White;
            mHeaderPicture.Image = Resources.Connection;
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
                "The data adapter will execute queries using this connection to load and update data.";
            mTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTitleLabel.BackColor = Color.White;
            mTitleLabel.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTitleLabel.Location = new Point(16, 10);
            mTitleLabel.Name = "mTitleLabel";
            mTitleLabel.Size = new Size(410, 13);
            mTitleLabel.TabIndex = 18;
            mTitleLabel.Text = "Choose Your Data Connection";
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
            mTestButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            mTestButton.Location = new Point(24, 264);
            mTestButton.Name = "mTestButton";
            mTestButton.Size = new Size(144, 26);
            mTestButton.TabIndex = 26;
            mTestButton.Text = "&Test Connection String";
            mTestButton.Click += mTestButton_Click;
            mConnStringButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mConnStringButton.Location = new Point(400, 136);
            mConnStringButton.Name = "mConnStringButton";
            mConnStringButton.Size = new Size(80, 26);
            mConnStringButton.TabIndex = 25;
            mConnStringButton.Text = "&Build String";
            mConnStringButton.Click += mConnStringButton_Click;
            mConnectionText.Anchor =
                AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mConnectionText.Location = new Point(24, 136);
            mConnectionText.Multiline = true;
            mConnectionText.Name = "mConnectionText";
            mConnectionText.Size = new Size(376, 72);
            mConnectionText.TabIndex = 24;
            mConnectionText.TextChanged += mConnectionText_TextChanged;
            mConnectionPrompt.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mConnectionPrompt.Location = new Point(24, 120);
            mConnectionPrompt.Name = "mConnectionPrompt";
            mConnectionPrompt.Size = new Size(432, 16);
            mConnectionPrompt.TabIndex = 23;
            mConnectionPrompt.Text = "&What connection string should the data adapter use?";
            mConnectionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mConnectionLabel.Location = new Point(24, 72);
            mConnectionLabel.Name = "mConnectionLabel";
            mConnectionLabel.Size = new Size(432, 40);
            mConnectionLabel.TabIndex = 22;
            mConnectionLabel.Text = "Enter the connection string for the data connection.";
            AcceptButton = mNextButton;
            AutoScaleMode = AutoScaleMode.Inherit;
            CancelButton = mCancelButton;
            ClientSize = new Size(497, 365);
            Controls.Add(mTestButton);
            Controls.Add(mConnStringButton);
            Controls.Add(mConnectionText);
            Controls.Add(mConnectionPrompt);
            Controls.Add(mConnectionLabel);
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
            Name = nameof(ConnectionDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Data Adapter Configuration Wizard";
            Load += ConnectionDlg_Load;
            FormClosed += ConnectionDlg_FormClosed;
            ((ISupportInitialize)mHeaderPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}