using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Advantage.Data.Provider.Properties;

namespace Advantage.Data.Provider
{
    public class QueryTypeDlg : Form
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
        private Label mTableDirectLabel;
        private Label mStoredProcLabel;
        private RadioButton mTableDirectRadio;
        private RadioButton mStoredProcRadio;
        private Label mSqlLabel;
        private RadioButton mSqlRadioButton;
        private Label mHowLabel;

        public QueryTypeDlg(ConfigWizard parent)
        {
            mParent = parent;
            InitializeComponent();
        }

        private void QueryTypeDlg_Load(object sender, EventArgs e)
        {
            Size = mParent.FormSize;
            Location = mParent.FormLocation;
            mSqlRadioButton.Checked = false;
            mStoredProcRadio.Checked = false;
            mTableDirectRadio.Checked = false;
            switch (mParent.SelectCommand.CommandType)
            {
                case CommandType.Text:
                    mSqlRadioButton.Checked = true;
                    mParent.QueryType = ConfigWizard.QueryTypes.SqlQuery;
                    break;
                case CommandType.StoredProcedure:
                    mStoredProcRadio.Checked = true;
                    mParent.QueryType = ConfigWizard.QueryTypes.StoredProc;
                    break;
                case CommandType.TableDirect:
                    mTableDirectRadio.Checked = true;
                    mParent.QueryType = ConfigWizard.QueryTypes.TableDirect;
                    break;
                default:
                    mSqlRadioButton.Checked = true;
                    mParent.QueryType = ConfigWizard.QueryTypes.SqlQuery;
                    break;
            }

            if (mParent.ReadyToFinish)
                mFinishButton.Enabled = true;
            else
                mFinishButton.Enabled = false;
        }

        private void mNextButton_Click(object sender, EventArgs e)
        {
            if (mSqlRadioButton.Checked)
            {
                if (mParent.SelectCommand.CommandType != CommandType.Text)
                    ResetCommandTextAndParams();
                mParent.SelectCommand.CommandType = CommandType.Text;
                mParent.InsertCommand.CommandType = CommandType.Text;
                mParent.DeleteCommand.CommandType = CommandType.Text;
                mParent.UpdateCommand.CommandType = CommandType.Text;
                mParent.QueryType = ConfigWizard.QueryTypes.SqlQuery;
                mParent.mNextDlg = ConfigWizard.WizardDialogs.QueryBuild;
                DialogResult = DialogResult.OK;
                Close();
            }
            else if (mStoredProcRadio.Checked)
            {
                if (mParent.SelectCommand.CommandType != CommandType.StoredProcedure)
                    ResetCommandTextAndParams();
                mParent.SelectCommand.CommandType = CommandType.StoredProcedure;
                mParent.InsertCommand.CommandType = CommandType.StoredProcedure;
                mParent.DeleteCommand.CommandType = CommandType.StoredProcedure;
                mParent.UpdateCommand.CommandType = CommandType.StoredProcedure;
                mParent.QueryType = ConfigWizard.QueryTypes.StoredProc;
                mParent.mNextDlg = ConfigWizard.WizardDialogs.StoredProc;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                if (!mTableDirectRadio.Checked)
                    return;
                if (mParent.SelectCommand.CommandType != CommandType.TableDirect)
                    ResetCommandTextAndParams();
                mParent.SelectCommand.CommandType = CommandType.TableDirect;
                mParent.InsertCommand.CommandType = CommandType.Text;
                mParent.DeleteCommand.CommandType = CommandType.Text;
                mParent.UpdateCommand.CommandType = CommandType.Text;
                mParent.QueryType = ConfigWizard.QueryTypes.TableDirect;
                mParent.mNextDlg = ConfigWizard.WizardDialogs.TableDirect;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ResetCommandTextAndParams()
        {
            mParent.SelectCommand.CommandText = "";
            mParent.SelectCommand.Parameters.Clear();
            mParent.InsertCommand.CommandText = "";
            mParent.InsertCommand.Parameters.Clear();
            mParent.DeleteCommand.CommandText = "";
            mParent.DeleteCommand.Parameters.Clear();
            mParent.UpdateCommand.CommandText = "";
            mParent.UpdateCommand.Parameters.Clear();
        }

        private void mCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void mBackButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Connection;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void mFinishButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Finish;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void QueryTypeDlg_FormClosed(object sender, FormClosedEventArgs e)
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
            mBackButton = new Button();
            mNextButton = new Button();
            mCancelButton = new Button();
            mFinishButton = new Button();
            mSeparator = new GroupBox();
            mSubtitleLabel = new Label();
            mTitleLabel = new Label();
            mHeaderSeparator = new GroupBox();
            mHeaderPanel = new Panel();
            mTableDirectLabel = new Label();
            mStoredProcLabel = new Label();
            mTableDirectRadio = new RadioButton();
            mStoredProcRadio = new RadioButton();
            mSqlLabel = new Label();
            mSqlRadioButton = new RadioButton();
            mHowLabel = new Label();
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
            mSubtitleLabel.Text = "The data adapter uses SQL statements or stored procedures.";
            mTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTitleLabel.BackColor = Color.White;
            mTitleLabel.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTitleLabel.Location = new Point(16, 10);
            mTitleLabel.Name = "mTitleLabel";
            mTitleLabel.Size = new Size(410, 13);
            mTitleLabel.TabIndex = 18;
            mTitleLabel.Text = "Choose a Query Type";
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
            mTableDirectLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTableDirectLabel.Location = new Point(40, 256);
            mTableDirectLabel.Name = "mTableDirectLabel";
            mTableDirectLabel.Size = new Size(424, 32);
            mTableDirectLabel.TabIndex = 28;
            mTableDirectLabel.Text = "Open a table directly by specifying its name.";
            mStoredProcLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mStoredProcLabel.Location = new Point(40, 192);
            mStoredProcLabel.Name = "mStoredProcLabel";
            mStoredProcLabel.Size = new Size(424, 32);
            mStoredProcLabel.TabIndex = 26;
            mStoredProcLabel.Text =
                "Choose an existing stored procedure for each operation (select, insert, and delete).";
            mTableDirectRadio.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTableDirectRadio.Location = new Point(24, 232);
            mTableDirectRadio.Name = "mTableDirectRadio";
            mTableDirectRadio.Size = new Size(432, 24);
            mTableDirectRadio.TabIndex = 27;
            mTableDirectRadio.Text = "Use &Table Directly";
            mStoredProcRadio.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mStoredProcRadio.Location = new Point(24, 168);
            mStoredProcRadio.Name = "mStoredProcRadio";
            mStoredProcRadio.Size = new Size(424, 24);
            mStoredProcRadio.TabIndex = 25;
            mStoredProcRadio.Text = "Use &Stored Procedures";
            mSqlLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mSqlLabel.Location = new Point(40, 128);
            mSqlLabel.Name = "mSqlLabel";
            mSqlLabel.Size = new Size(424, 32);
            mSqlLabel.TabIndex = 24;
            mSqlLabel.Text =
                "Specify a Select statement to load data, and the wizard will generate the Insert, Update and Delete statements to save data changes.";
            mSqlRadioButton.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mSqlRadioButton.Location = new Point(24, 104);
            mSqlRadioButton.Name = "mSqlRadioButton";
            mSqlRadioButton.Size = new Size(432, 16);
            mSqlRadioButton.TabIndex = 23;
            mSqlRadioButton.TabStop = true;
            mSqlRadioButton.Text = "Use &SQL statements";
            mHowLabel.Location = new Point(24, 72);
            mHowLabel.Name = "mHowLabel";
            mHowLabel.Size = new Size(432, 23);
            mHowLabel.TabIndex = 22;
            mHowLabel.Text = "How should the data adapter access the database?";
            mHeaderPicture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            mHeaderPicture.BackColor = Color.White;
            mHeaderPicture.Image = Resources.QueryType;
            mHeaderPicture.Location = new Point(416, 3);
            mHeaderPicture.Name = "mHeaderPicture";
            mHeaderPicture.Size = new Size(75, 51);
            mHeaderPicture.TabIndex = 21;
            mHeaderPicture.TabStop = false;
            AcceptButton = mNextButton;
            AutoScaleMode = AutoScaleMode.Inherit;
            CancelButton = mCancelButton;
            ClientSize = new Size(497, 365);
            Controls.Add(mTableDirectLabel);
            Controls.Add(mStoredProcLabel);
            Controls.Add(mTableDirectRadio);
            Controls.Add(mStoredProcRadio);
            Controls.Add(mSqlLabel);
            Controls.Add(mSqlRadioButton);
            Controls.Add(mHowLabel);
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
            Name = nameof(QueryTypeDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Data Adapter Configuration Wizard";
            Load += QueryTypeDlg_Load;
            FormClosed += QueryTypeDlg_FormClosed;
            ((ISupportInitialize)mHeaderPicture).EndInit();
            ResumeLayout(false);
        }
    }
}