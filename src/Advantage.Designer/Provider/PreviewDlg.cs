using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Advantage.Data.Provider
{
    public class PreviewDlg : Form
    {
        private AdsDataAdapter mAdapter;
        private AdsConnection mConnection;
        private AdsCommand mCommand;
        private DataSet mDataSet = new DataSet("data");
        private string ErrorTitle = "Data Adapter Preview";
        private Label mAdapterLabel;
        private Label mConnStringLabel;
        private Label mCmdTextLabel;
        private DataGrid mDataGrid;
        private Label mAdapterPrompt;
        private Label mConnStringPrompt;
        private Label mCmdTextPrompt;
        private Button mCloseButton;
        private Label mTablesPrompt;
        private Label mTableNameLabel;
        private Container components;

        public PreviewDlg(AdsDataAdapter adapter, string name)
        {
            InitializeComponent();
            mAdapter = adapter;
            if (mAdapter != null)
                mCommand = mAdapter.SelectCommand;
            if (mCommand != null)
                mConnection = mCommand.Connection;
            mAdapterLabel.Text = name;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mAdapterLabel = new Label();
            mConnStringLabel = new Label();
            mCmdTextLabel = new Label();
            mDataGrid = new DataGrid();
            mAdapterPrompt = new Label();
            mConnStringPrompt = new Label();
            mCmdTextPrompt = new Label();
            mCloseButton = new Button();
            mTablesPrompt = new Label();
            mTableNameLabel = new Label();
            mDataGrid.BeginInit();
            SuspendLayout();
            mAdapterLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mAdapterLabel.Location = new Point(128, 16);
            mAdapterLabel.Name = "mAdapterLabel";
            mAdapterLabel.Size = new Size(384, 12);
            mAdapterLabel.TabIndex = 1;
            mAdapterLabel.Text = "mAdapterLabel";
            mConnStringLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mConnStringLabel.Location = new Point(128, 40);
            mConnStringLabel.Name = "mConnStringLabel";
            mConnStringLabel.Size = new Size(384, 12);
            mConnStringLabel.TabIndex = 3;
            mConnStringLabel.Text = "mConnStringLabel";
            mCmdTextLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mCmdTextLabel.Location = new Point(128, 64);
            mCmdTextLabel.Name = "mCmdTextLabel";
            mCmdTextLabel.Size = new Size(384, 12);
            mCmdTextLabel.TabIndex = 5;
            mCmdTextLabel.Text = "mCmdTextLabel";
            mDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mDataGrid.DataMember = "";
            mDataGrid.HeaderForeColor = SystemColors.ControlText;
            mDataGrid.Location = new Point(16, 120);
            mDataGrid.Name = "mDataGrid";
            mDataGrid.ReadOnly = true;
            mDataGrid.Size = new Size(496, 160);
            mDataGrid.TabIndex = 8;
            mAdapterPrompt.Location = new Point(16, 16);
            mAdapterPrompt.Name = "mAdapterPrompt";
            mAdapterPrompt.Size = new Size(104, 16);
            mAdapterPrompt.TabIndex = 0;
            mAdapterPrompt.Text = "Data Adapter:";
            mConnStringPrompt.Location = new Point(16, 40);
            mConnStringPrompt.Name = "mConnStringPrompt";
            mConnStringPrompt.Size = new Size(104, 16);
            mConnStringPrompt.TabIndex = 2;
            mConnStringPrompt.Text = "Connection String:";
            mCmdTextPrompt.Location = new Point(16, 64);
            mCmdTextPrompt.Name = "mCmdTextPrompt";
            mCmdTextPrompt.Size = new Size(96, 16);
            mCmdTextPrompt.TabIndex = 4;
            mCmdTextPrompt.Text = "Select Text:";
            mCloseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            mCloseButton.Location = new Point(440, 288);
            mCloseButton.Name = "mCloseButton";
            mCloseButton.Size = new Size(72, 23);
            mCloseButton.TabIndex = 9;
            mCloseButton.Text = "&Close";
            mCloseButton.Click += mCloseButton_Click;
            mTablesPrompt.Location = new Point(16, 88);
            mTablesPrompt.Name = "mTablesPrompt";
            mTablesPrompt.Size = new Size(100, 16);
            mTablesPrompt.TabIndex = 6;
            mTablesPrompt.Text = "Data &Table:";
            mTableNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            mTableNameLabel.Location = new Point(128, 88);
            mTableNameLabel.Name = "mTableNameLabel";
            mTableNameLabel.Size = new Size(384, 12);
            mTableNameLabel.TabIndex = 7;
            mTableNameLabel.Text = "mTableNameLabel";
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(528, 325);
            Controls.Add(mTableNameLabel);
            Controls.Add(mTablesPrompt);
            Controls.Add(mCloseButton);
            Controls.Add(mCmdTextPrompt);
            Controls.Add(mConnStringPrompt);
            Controls.Add(mAdapterPrompt);
            Controls.Add(mDataGrid);
            Controls.Add(mCmdTextLabel);
            Controls.Add(mConnStringLabel);
            Controls.Add(mAdapterLabel);
            MinimumSize = new Size(500, 300);
            Name = nameof(PreviewDlg);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Advantage Data Adapter Preview";
            Load += PreviewDlg_Load;
            mDataGrid.EndInit();
            ResumeLayout(false);
        }

        private void mCloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PreviewDlg_Load(object sender, EventArgs e)
        {
            if (mConnection == null || mAdapter == null || mCommand == null)
            {
                var num = (int)MessageBox.Show("Adapter is not fully configured. Cannot preview data.",
                    ErrorTitle);
                Cursor.Current = Cursors.Default;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    mConnStringLabel.Text = mConnection.ConnectionString;
                    mCmdTextLabel.Text = mCommand.CommandText;
                    if (mConnection.State == ConnectionState.Closed)
                        mConnection.Open();
                }
                catch (Exception ex)
                {
                    var num = (int)MessageBox.Show("Error opening connection. Cannot preview data.\n\n" + ex,
                        ErrorTitle);
                    Cursor.Current = Cursors.Default;
                    return;
                }

                try
                {
                    mAdapter.Fill(mDataSet);
                    mDataGrid.DataSource = mDataSet.Tables[0];
                    FormatDateTimeColumns();
                    AutoSizeColumns();
                    mTableNameLabel.Text = mDataSet.Tables[0].TableName;
                }
                catch (Exception ex)
                {
                    var num = (int)MessageBox.Show(
                        "Error filling the DataSet. Cannot preview data.\n\n" + ex, ErrorTitle);
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void FormatDateTimeColumns()
        {
            var dataSource = (DataTable)mDataGrid.DataSource;
            mDataGrid.TableStyles.Clear();
            var table =
                new DataGridTableStyle(
                    (CurrencyManager)BindingContext[mDataSet, dataSource.TableName]);
            mDataGrid.TableStyles.Add(table);
            for (var index = 0; index < dataSource.Columns.Count; ++index)
            {
                if (dataSource.Columns[index].DataType == (object)typeof(DateTime))
                {
                    var gridColumnStyle = mDataGrid.TableStyles[0].GridColumnStyles[index];
                    if (gridColumnStyle != null &&
                        gridColumnStyle.GetType() == (object)typeof(DataGridTextBoxColumn))
                        ((DataGridTextBoxColumn)gridColumnStyle).Format = "G";
                }
                else
                {
                    var column = new DataGridTextBoxColumn();
                    table.GridColumnStyles.Add(column);
                }
            }

            mDataGrid.TableStyles.Add(table);
        }

        private void AutoSizeColumns()
        {
            if (mDataGrid.TableStyles.Count == 0)
                return;
            var graphics = Graphics.FromHwnd(mDataGrid.Handle);
            var format = new StringFormat(StringFormat.GenericTypographic);
            var dataSource = (DataTable)mDataGrid.DataSource;
            for (var index = 0; index < dataSource.Columns.Count; ++index)
            {
                var sizeF = graphics.MeasureString(dataSource.Columns[index].ToString(), mDataGrid.Font, 500,
                    format);
                var width = sizeF.Width;
                for (var rowIndex = 0; rowIndex < dataSource.Rows.Count; ++rowIndex)
                {
                    sizeF = graphics.MeasureString(mDataGrid[rowIndex, index].ToString(), mDataGrid.Font, 500,
                        format);
                    if (sizeF.Width > (double)width)
                        width = sizeF.Width;
                }

                mDataGrid.TableStyles[0].GridColumnStyles[index].Width = (int)width + 8;
            }

            graphics.Dispose();
        }
    }
}