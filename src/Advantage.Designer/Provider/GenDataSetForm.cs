using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Advantage.Data.Provider
{
    public class GenDataSetForm : Form
    {
        private Button btnOK;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        public RadioButton rbExisting;
        private RadioButton rbNew;
        public CheckedListBox lbTables;
        public CheckBox cbAddToDesigner;
        public ComboBox comboExisting;
        private Container components;
        public TextBox ebNew;
        public ArrayList ExistingDataSets;
        public ArrayList TableList;
        public int iThisAdapterIndex;

        public GenDataSetForm()
        {
            InitializeComponent();
            TableList = new ArrayList();
            iThisAdapterIndex = -1;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnOK = new Button();
            btnCancel = new Button();
            label1 = new Label();
            label2 = new Label();
            rbExisting = new RadioButton();
            rbNew = new RadioButton();
            label3 = new Label();
            lbTables = new CheckedListBox();
            cbAddToDesigner = new CheckBox();
            comboExisting = new ComboBox();
            ebNew = new TextBox();
            SuspendLayout();
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(208, 288);
            btnOK.Name = "btnOK";
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(288, 288);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            label1.Location = new Point(16, 16);
            label1.Name = "label1";
            label1.Size = new Size(280, 23);
            label1.TabIndex = 3;
            label1.Text = "Generate a dataset that includes the specified tables.";
            label2.Location = new Point(16, 40);
            label2.Name = "label2";
            label2.TabIndex = 4;
            label2.Text = "Choose a dataset:";
            rbExisting.Location = new Point(32, 64);
            rbExisting.Name = "rbExisting";
            rbExisting.Size = new Size(80, 24);
            rbExisting.TabIndex = 5;
            rbExisting.Text = "Existing";
            rbExisting.CheckedChanged += rbExisting_CheckedChanged;
            rbNew.Checked = true;
            rbNew.Location = new Point(32, 96);
            rbNew.Name = "rbNew";
            rbNew.Size = new Size(72, 24);
            rbNew.TabIndex = 6;
            rbNew.TabStop = true;
            rbNew.Text = "New";
            rbNew.CheckedChanged += rbNew_CheckedChanged;
            label3.Location = new Point(16, 136);
            label3.Name = "label3";
            label3.Size = new Size(336, 23);
            label3.TabIndex = 7;
            label3.Text = "&Choose which table(s) to add to the dataset:";
            lbTables.CheckOnClick = true;
            lbTables.Location = new Point(16, 168);
            lbTables.Name = "lbTables";
            lbTables.Size = new Size(344, 79);
            lbTables.TabIndex = 8;
            cbAddToDesigner.Checked = true;
            cbAddToDesigner.CheckState = CheckState.Checked;
            cbAddToDesigner.Location = new Point(24, 256);
            cbAddToDesigner.Name = "cbAddToDesigner";
            cbAddToDesigner.Size = new Size(320, 24);
            cbAddToDesigner.TabIndex = 9;
            cbAddToDesigner.Text = "&Add this dataset to the designer.";
            comboExisting.Enabled = false;
            comboExisting.Location = new Point(112, 64);
            comboExisting.Name = "comboExisting";
            comboExisting.Size = new Size(248, 21);
            comboExisting.TabIndex = 10;
            ebNew.Location = new Point(112, 96);
            ebNew.Name = "ebNew";
            ebNew.Size = new Size(248, 20);
            ebNew.TabIndex = 11;
            ebNew.Text = "";
            AutoScaleBaseSize = new Size(5, 13);
            CancelButton = btnCancel;
            ClientSize = new Size(378, 327);
            Controls.Add(ebNew);
            Controls.Add(comboExisting);
            Controls.Add(cbAddToDesigner);
            Controls.Add(lbTables);
            Controls.Add(label3);
            Controls.Add(rbNew);
            Controls.Add(rbExisting);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = nameof(GenDataSetForm);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Generate Dataset";
            Load += GenDataSetForm_Load;
            ResumeLayout(false);
        }

        private void rbNew_CheckedChanged(object sender, EventArgs e)
        {
            comboExisting.Enabled = rbExisting.Checked;
            ebNew.Enabled = rbNew.Checked;
        }

        private void rbExisting_CheckedChanged(object sender, EventArgs e)
        {
            comboExisting.Enabled = rbExisting.Checked;
            ebNew.Enabled = rbNew.Checked;
        }

        private void GenDataSetForm_Load(object sender, EventArgs e)
        {
            for (var index = 0; index < TableList.Count; ++index)
                lbTables.Items.Add(TableList[index].ToString());
            if (iThisAdapterIndex >= 0)
                lbTables.SetItemChecked(iThisAdapterIndex, true);
            if (comboExisting.Items.Count <= 0)
                return;
            comboExisting.SelectedIndex = 0;
            rbExisting.Checked = true;
        }
    }
}