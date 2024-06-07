using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Advantage.Data.Provider.Properties;

namespace Advantage.Data.Provider
{
    public class WelcomeDlg : Form
    {
        private ConfigWizard mParent;
        private IContainer components;
        protected Button mBackButton;
        protected Button mNextButton;
        protected Button mCancelButton;
        protected Button mFinishButton;
        protected GroupBox mSeparator;
        protected PictureBox mWatermarkPicture;
        protected Label mTitleLabel;
        private Label label2;
        private Label label1;
        private Panel panel1;

        public WelcomeDlg(ConfigWizard parent)
        {
            mParent = parent;
            InitializeComponent();
        }

        private void mNextButton_Click(object sender, EventArgs e)
        {
            mParent.mNextDlg = ConfigWizard.WizardDialogs.Connection;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void WelcomeDlg_Load(object sender, EventArgs e)
        {
            Size = mParent.FormSize;
            if (mParent.FormLocation.IsEmpty)
                return;
            Location = mParent.FormLocation;
        }

        private void mCancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void WelcomeDlg_FormClosed(object sender, FormClosedEventArgs e)
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
            var componentResourceManager = new ComponentResourceManager(typeof(WelcomeDlg));
            mBackButton = new Button();
            mNextButton = new Button();
            mCancelButton = new Button();
            mFinishButton = new Button();
            mSeparator = new GroupBox();
            mWatermarkPicture = new PictureBox();
            mTitleLabel = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1 = new Panel();
            ((ISupportInitialize)mWatermarkPicture).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            mBackButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mBackButton.Enabled = false;
            mBackButton.Location = new Point(244, 328);
            mBackButton.Name = "mBackButton";
            mBackButton.Size = new Size(75, 26);
            mBackButton.TabIndex = 14;
            mBackButton.Text = "< &Back";
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
            mFinishButton.Enabled = false;
            mFinishButton.Location = new Point(412, 328);
            mFinishButton.Name = "mFinishButton";
            mFinishButton.Size = new Size(75, 26);
            mFinishButton.TabIndex = 16;
            mFinishButton.Text = "&Finish";
            mSeparator.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mSeparator.Location = new Point(0, 313);
            mSeparator.Name = "mSeparator";
            mSeparator.Size = new Size(499, 2);
            mSeparator.TabIndex = 12;
            mSeparator.TabStop = false;
            mWatermarkPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            mWatermarkPicture.BackColor = Color.White;
            mWatermarkPicture.Image = Resources.Welcome;
            mWatermarkPicture.Location = new Point(0, 0);
            mWatermarkPicture.Name = "mWatermarkPicture";
            mWatermarkPicture.Size = new Size(164, 312);
            mWatermarkPicture.TabIndex = 18;
            mWatermarkPicture.TabStop = false;
            mTitleLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTitleLabel.BackColor = Color.White;
            mTitleLabel.Font = new Font("Verdana", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mTitleLabel.Location = new Point(170, 13);
            mTitleLabel.Name = "mTitleLabel";
            mTitleLabel.Size = new Size(292, 39);
            mTitleLabel.TabIndex = 17;
            mTitleLabel.Text = "Welcome to the Advantage Data Adapter Configuration Wizard";
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Location = new Point(16, 220);
            label2.Name = "label2";
            label2.Size = new Size(280, 23);
            label2.TabIndex = 20;
            label2.Text = "Click Next to continue.";
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.BackColor = Color.White;
            label1.Location = new Point(176, 96);
            label1.Name = "label1";
            label1.Size = new Size(280, 112);
            label1.TabIndex = 19;
            label1.Text = componentResourceManager.GetString("label1.Text");
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label2);
            panel1.Location = new Point(160, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(339, 312);
            panel1.TabIndex = 21;
            AcceptButton = mNextButton;
            AutoScaleMode = AutoScaleMode.Inherit;
            CancelButton = mCancelButton;
            ClientSize = new Size(497, 365);
            Controls.Add(mTitleLabel);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(mWatermarkPicture);
            Controls.Add(mBackButton);
            Controls.Add(mNextButton);
            Controls.Add(mCancelButton);
            Controls.Add(mFinishButton);
            Controls.Add(mSeparator);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(505, 392);
            Name = nameof(WelcomeDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Data Adapter Configuration Wizard";
            Load += WelcomeDlg_Load;
            FormClosed += WelcomeDlg_FormClosed;
            ((ISupportInitialize)mWatermarkPicture).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}