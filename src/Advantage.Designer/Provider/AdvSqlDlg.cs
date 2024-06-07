using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Advantage.Data.Provider
{
    public class AdvSqlDlg : Form
    {
        private Label mInstructLabel;
        private CheckBox mGenerateCmdsCheckbox;
        private Label mGenerateCmdsLabel;
        private CheckBox mOptConcurCheckBox;
        private Label mOptConcurLabel;
        private Button mCancelButton;
        private Button mOkButton;
        private CheckBox mPKCheckBox;
        private Label mPriKeyLabel;
        private CheckBox mRowversionCheckBox;
        private Label mUseJustRowversionLabel;
        private Label mRefreshDataSetLabel;
        private CheckBox mRefreshDatasetCheckBox;
        private Container components;

        public AdvSqlDlg(ConfigWizard wizard)
        {
            InitializeComponent();
            mGenerateCmdsCheckbox.Checked = wizard.GenerateCommands;
            mOptConcurCheckBox.Checked = wizard.OptimisticConcurrency;
            mRowversionCheckBox.Checked = wizard.UseRowversion;
            mPKCheckBox.Checked = wizard.PrimaryKeyRequired;
            mRefreshDatasetCheckBox.Checked = wizard.RefreshDataset;
            if (mOptConcurCheckBox.Checked)
                return;
            mPKCheckBox.Checked = true;
            mPKCheckBox.Enabled = false;
            mPriKeyLabel.Enabled = false;
            mRowversionCheckBox.Checked = false;
            mRowversionCheckBox.Enabled = false;
            mUseJustRowversionLabel.Enabled = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mInstructLabel = new Label();
            mGenerateCmdsCheckbox = new CheckBox();
            mGenerateCmdsLabel = new Label();
            mOptConcurCheckBox = new CheckBox();
            mOptConcurLabel = new Label();
            mCancelButton = new Button();
            mOkButton = new Button();
            mPKCheckBox = new CheckBox();
            mPriKeyLabel = new Label();
            mRowversionCheckBox = new CheckBox();
            mUseJustRowversionLabel = new Label();
            mRefreshDatasetCheckBox = new CheckBox();
            mRefreshDataSetLabel = new Label();
            SuspendLayout();
            mInstructLabel.Location = new Point(16, 8);
            mInstructLabel.Name = "mInstructLabel";
            mInstructLabel.Size = new Size(480, 24);
            mInstructLabel.TabIndex = 0;
            mInstructLabel.Text =
                "Additional Insert, Update and Delete statements can be generated to update the data source.";
            mGenerateCmdsCheckbox.Checked = true;
            mGenerateCmdsCheckbox.CheckState = CheckState.Checked;
            mGenerateCmdsCheckbox.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mGenerateCmdsCheckbox.Location = new Point(16, 40);
            mGenerateCmdsCheckbox.Name = "mGenerateCmdsCheckbox";
            mGenerateCmdsCheckbox.Size = new Size(336, 16);
            mGenerateCmdsCheckbox.TabIndex = 1;
            mGenerateCmdsCheckbox.Text = "&Generate Insert, Update and Delete Statements";
            mGenerateCmdsLabel.Location = new Point(32, 64);
            mGenerateCmdsLabel.Name = "mGenerateCmdsLabel";
            mGenerateCmdsLabel.Size = new Size(464, 16);
            mGenerateCmdsLabel.TabIndex = 2;
            mGenerateCmdsLabel.Text =
                "Generate Insert, Update and Delete statements based on your Select statement.";
            mOptConcurCheckBox.Checked = true;
            mOptConcurCheckBox.CheckState = CheckState.Checked;
            mOptConcurCheckBox.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mOptConcurCheckBox.Location = new Point(16, 88);
            mOptConcurCheckBox.Name = "mOptConcurCheckBox";
            mOptConcurCheckBox.Size = new Size(328, 16);
            mOptConcurCheckBox.TabIndex = 2;
            mOptConcurCheckBox.Text = "&Use Optimistic Concurrency";
            mOptConcurCheckBox.CheckedChanged += mOptConcurCheckBox_CheckedChanged;
            mOptConcurLabel.Location = new Point(32, 112);
            mOptConcurLabel.Name = "mOptConcurLabel";
            mOptConcurLabel.Size = new Size(464, 32);
            mOptConcurLabel.TabIndex = 4;
            mOptConcurLabel.Text =
                "Modifies Update and Delete statements to detect whether the database has changed since the record was loaded into the dataset. This helps prevent concurrency conflicts.";
            mCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mCancelButton.DialogResult = DialogResult.Cancel;
            mCancelButton.Location = new Point(442, 330);
            mCancelButton.Name = "mCancelButton";
            mCancelButton.Size = new Size(64, 23);
            mCancelButton.TabIndex = 7;
            mCancelButton.Text = "Cancel";
            mOkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mOkButton.DialogResult = DialogResult.OK;
            mOkButton.Location = new Point(370, 330);
            mOkButton.Name = "mOkButton";
            mOkButton.Size = new Size(64, 23);
            mOkButton.TabIndex = 6;
            mOkButton.Text = "OK";
            mPKCheckBox.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mPKCheckBox.Location = new Point(16, 200);
            mPKCheckBox.Name = "mPKCheckBox";
            mPKCheckBox.Size = new Size(208, 16);
            mPKCheckBox.TabIndex = 4;
            mPKCheckBox.Text = "&Require Primary Key";
            mPriKeyLabel.Location = new Point(32, 224);
            mPriKeyLabel.Name = "mPriKeyLabel";
            mPriKeyLabel.Size = new Size(464, 32);
            mPriKeyLabel.TabIndex = 16;
            mPriKeyLabel.Text =
                "Enforces whether or not a Primary Key or Unique Identifier is required when generating statements for the given table or Select statement.";
            mRowversionCheckBox.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mRowversionCheckBox.Location = new Point(32, 144);
            mRowversionCheckBox.Name = "mRowversionCheckBox";
            mRowversionCheckBox.Size = new Size(392, 24);
            mRowversionCheckBox.TabIndex = 3;
            mRowversionCheckBox.Text = "Use &Only Rowversion Fields";
            mUseJustRowversionLabel.Location = new Point(48, 168);
            mUseJustRowversionLabel.Name = "mUseJustRowversionLabel";
            mUseJustRowversionLabel.Size = new Size(440, 16);
            mUseJustRowversionLabel.TabIndex = 18;
            mUseJustRowversionLabel.Text =
                "Use only the rowversion field (if available) for optimistic concurrency checking.";
            mRefreshDatasetCheckBox.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            mRefreshDatasetCheckBox.Location = new Point(16, 264);
            mRefreshDatasetCheckBox.Name = "mRefreshDatasetCheckBox";
            mRefreshDatasetCheckBox.Size = new Size(152, 16);
            mRefreshDatasetCheckBox.TabIndex = 5;
            mRefreshDatasetCheckBox.Text = "Re&fresh the DataSet";
            mRefreshDataSetLabel.Location = new Point(32, 288);
            mRefreshDataSetLabel.Name = "mRefreshDataSetLabel";
            mRefreshDataSetLabel.Size = new Size(464, 32);
            mRefreshDataSetLabel.TabIndex = 19;
            mRefreshDataSetLabel.Text =
                "Adds a Select statement after Insert and Update statements to retrieve auto-updating column values, default values, and other values calculated by the database.";
            AcceptButton = mOkButton;
            AutoScaleBaseSize = new Size(5, 13);
            CancelButton = mCancelButton;
            ClientSize = new Size(514, 359);
            ControlBox = false;
            Controls.Add(mRefreshDataSetLabel);
            Controls.Add(mRefreshDatasetCheckBox);
            Controls.Add(mUseJustRowversionLabel);
            Controls.Add(mRowversionCheckBox);
            Controls.Add(mPriKeyLabel);
            Controls.Add(mPKCheckBox);
            Controls.Add(mCancelButton);
            Controls.Add(mOkButton);
            Controls.Add(mOptConcurLabel);
            Controls.Add(mOptConcurCheckBox);
            Controls.Add(mGenerateCmdsLabel);
            Controls.Add(mGenerateCmdsCheckbox);
            Controls.Add(mInstructLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = nameof(AdvSqlDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Advanced Options";
            ResumeLayout(false);
        }

        private void mOptConcurCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mOptConcurCheckBox.Checked)
            {
                mPKCheckBox.Enabled = true;
                mPriKeyLabel.Enabled = true;
                mRowversionCheckBox.Enabled = true;
                mUseJustRowversionLabel.Enabled = true;
            }
            else
            {
                mPKCheckBox.Checked = true;
                mPKCheckBox.Enabled = false;
                mPriKeyLabel.Enabled = false;
                mRowversionCheckBox.Checked = false;
                mRowversionCheckBox.Enabled = false;
                mUseJustRowversionLabel.Enabled = false;
            }
        }

        public bool GenerateCommands => mGenerateCmdsCheckbox.Checked;

        public bool OptimisticConcurrency => mOptConcurCheckBox.Checked;

        public bool PrimaryKeyRequired => mPKCheckBox.Checked;

        public bool UseRowversion => mRowversionCheckBox.Checked;

        public bool RefreshDataset => mRefreshDatasetCheckBox.Checked;
    }
}