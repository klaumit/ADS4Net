using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Advantage.Data.Provider
{
    public class SelectTableDlg : Form
    {
        private Button mCancelButton;
        private Button mOkButton;
        private Label mTableLabel;
        private ListBox mTableList;
        private string mstrConnectionString;
        private string mstrTable = "";
        private Container components;

        public SelectTableDlg(string strConnectionString)
        {
            InitializeComponent();
            mstrConnectionString = strConnectionString;
            LoadTables();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mTableList = new ListBox();
            mCancelButton = new Button();
            mOkButton = new Button();
            mTableLabel = new Label();
            SuspendLayout();
            mTableList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mTableList.Location = new Point(8, 24);
            mTableList.Name = "mTableList";
            mTableList.Size = new Size(200, 147);
            mTableList.TabIndex = 1;
            mTableList.DoubleClick += mTableList_DoubleClick;
            mTableList.SelectedIndexChanged += mTableList_SelectedIndexChanged;
            mCancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mCancelButton.DialogResult = DialogResult.Cancel;
            mCancelButton.Location = new Point(144, 180);
            mCancelButton.Name = "mCancelButton";
            mCancelButton.Size = new Size(64, 23);
            mCancelButton.TabIndex = 19;
            mCancelButton.Text = "Cancel";
            mOkButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mOkButton.DialogResult = DialogResult.OK;
            mOkButton.Enabled = false;
            mOkButton.Location = new Point(72, 180);
            mOkButton.Name = "mOkButton";
            mOkButton.Size = new Size(64, 23);
            mOkButton.TabIndex = 18;
            mOkButton.Text = "OK";
            mTableLabel.Enabled = false;
            mTableLabel.Location = new Point(8, 8);
            mTableLabel.Name = "mTableLabel";
            mTableLabel.Size = new Size(168, 16);
            mTableLabel.TabIndex = 21;
            mTableLabel.Text = "&Table or View:";
            AcceptButton = mOkButton;
            AutoScaleBaseSize = new Size(5, 13);
            CancelButton = mCancelButton;
            ClientSize = new Size(216, 232);
            ControlBox = false;
            Controls.Add(mTableLabel);
            Controls.Add(mCancelButton);
            Controls.Add(mOkButton);
            Controls.Add(mTableList);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(192, 240);
            Name = nameof(SelectTableDlg);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Table";
            ResumeLayout(false);
        }

        private void LoadTables()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (mstrConnectionString.Length > 0)
            {
                var adsConnection = new AdsConnection(mstrConnectionString);
                try
                {
                    adsConnection.Open();
                    var tableNames = adsConnection.GetTableNames();
                    adsConnection.Close();
                    mTableList.Items.AddRange(tableNames);
                    mTableList.Sorted = true;
                    if (mTableList.Items.Count == 0)
                    {
                        var num = (int)MessageBox.Show("Unable to retrieve any tables for the connection.",
                            ConfigWizard.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (mTableList.Items.Count == 1)
                        mTableList.SelectedIndex = 0;
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
                var num = (int)MessageBox.Show("Invalid Connection String", ConfigWizard.MessageBoxTitle,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.Cancel;
                Close();
            }

            Cursor.Current = Cursors.Default;
        }

        private void mTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            mOkButton.Enabled = true;
            mstrTable = mTableList.Items[mTableList.SelectedIndex].ToString();
        }

        private void mTableList_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public string TableName => mstrTable;

        public bool IsValidTableName(string strTableName)
        {
            strTableName = strTableName.Replace("[", "");
            strTableName = strTableName.Replace("]", "");
            strTableName = strTableName.Replace("\"", "");
            strTableName = strTableName.Trim();
            return -1 != mTableList.FindStringExact(strTableName);
        }
    }
}