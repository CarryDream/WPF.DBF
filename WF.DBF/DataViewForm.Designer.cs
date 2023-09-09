namespace WF.DBF
{
    partial class DataViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataViewForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            btnExportDBF = new Button();
            btnExportExcel = new Button();
            btnConvertDBF = new Button();
            btnConvertExcel = new Button();
            labelStatics = new Label();
            btnQuery = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            comboBox1 = new ComboBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dataGridView1 = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(998, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnExportDBF);
            groupBox1.Controls.Add(btnExportExcel);
            groupBox1.Controls.Add(btnConvertDBF);
            groupBox1.Controls.Add(btnConvertExcel);
            groupBox1.Controls.Add(labelStatics);
            groupBox1.Controls.Add(btnQuery);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(992, 94);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "查询条件";
            // 
            // btnExportDBF
            // 
            btnExportDBF.Location = new Point(803, 17);
            btnExportDBF.Name = "btnExportDBF";
            btnExportDBF.Size = new Size(139, 23);
            btnExportDBF.TabIndex = 9;
            btnExportDBF.Text = "按表头导出DBF";
            btnExportDBF.UseVisualStyleBackColor = true;
            btnExportDBF.Click += btnExportDBF_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(674, 17);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(123, 23);
            btnExportExcel.TabIndex = 8;
            btnExportExcel.Text = "按表头导出Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // btnConvertDBF
            // 
            btnConvertDBF.Location = new Point(566, 17);
            btnConvertDBF.Name = "btnConvertDBF";
            btnConvertDBF.Size = new Size(75, 23);
            btnConvertDBF.TabIndex = 7;
            btnConvertDBF.Text = "导出DBF";
            btnConvertDBF.UseVisualStyleBackColor = true;
            btnConvertDBF.Click += btnConvertDBF_Click;
            // 
            // btnConvertExcel
            // 
            btnConvertExcel.Location = new Point(485, 17);
            btnConvertExcel.Name = "btnConvertExcel";
            btnConvertExcel.Size = new Size(75, 23);
            btnConvertExcel.TabIndex = 6;
            btnConvertExcel.Text = "导出Excel";
            btnConvertExcel.UseVisualStyleBackColor = true;
            btnConvertExcel.Click += btnConvertExcel_Click;
            // 
            // labelStatics
            // 
            labelStatics.AutoSize = true;
            labelStatics.Location = new Point(15, 55);
            labelStatics.Name = "labelStatics";
            labelStatics.Size = new Size(0, 17);
            labelStatics.TabIndex = 5;
            // 
            // btnQuery
            // 
            btnQuery.Location = new Point(404, 17);
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(75, 23);
            btnQuery.TabIndex = 4;
            btnQuery.Text = "查询";
            btnQuery.UseVisualStyleBackColor = true;
            btnQuery.Click += btnQuery_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(257, 17);
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "模糊查询";
            textBox1.Size = new Size(141, 23);
            textBox1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(197, 19);
            label2.Name = "label2";
            label2.Size = new Size(63, 17);
            label2.TabIndex = 2;
            label2.Text = "查询内容: ";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(61, 17);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(130, 25);
            comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 20);
            label1.Name = "label1";
            label1.Size = new Size(51, 17);
            label1.TabIndex = 0;
            label1.Text = "选择列: ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 103);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(992, 344);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "数据列表";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 19);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(986, 322);
            dataGridView1.TabIndex = 0;
            // 
            // DataViewForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 450);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "DataViewForm";
            Text = "查看内容";
            Load += DataViewForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private DataGridView dataGridView1;
        private Label labelStatics;
        private Button btnQuery;
        private TextBox textBox1;
        private Label label2;
        private ComboBox comboBox1;
        private Label label1;
        private Button btnConvertDBF;
        private Button btnConvertExcel;
        private Button btnExportExcel;
        private Button btnExportDBF;
    }
}