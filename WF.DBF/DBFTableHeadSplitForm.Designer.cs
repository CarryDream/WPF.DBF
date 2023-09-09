namespace WF.DBF
{
    partial class DBFTableHeadSplitForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            txtReadFilePath = new TextBox();
            label2 = new Label();
            txtDestFilePath = new TextBox();
            button1 = new Button();
            button2 = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            txtDBFLength = new TextBox();
            txtDBFTitle = new TextBox();
            label4 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            txtDestField1Length = new TextBox();
            txtDestField1Name = new TextBox();
            label5 = new Label();
            label6 = new Label();
            groupBox3 = new GroupBox();
            txtDestField2Length = new TextBox();
            txtDestField2Name = new TextBox();
            label7 = new Label();
            label8 = new Label();
            fbdReadPath = new FolderBrowserDialog();
            fbdDestPath = new FolderBrowserDialog();
            tableLayoutPanel3 = new TableLayoutPanel();
            button3 = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            txtStatus = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 87F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(txtReadFilePath, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(txtDestFilePath, 1, 1);
            tableLayoutPanel1.Controls.Add(button1, 2, 0);
            tableLayoutPanel1.Controls.Add(button2, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 33F));
            tableLayoutPanel1.Size = new Size(800, 64);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 31);
            label1.TabIndex = 0;
            label1.Text = "读取文件路径:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtReadFilePath
            // 
            txtReadFilePath.Dock = DockStyle.Fill;
            txtReadFilePath.Location = new Point(93, 3);
            txtReadFilePath.Name = "txtReadFilePath";
            txtReadFilePath.PlaceholderText = "请选择读取文件夹路径";
            txtReadFilePath.Size = new Size(617, 23);
            txtReadFilePath.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 31);
            label2.Name = "label2";
            label2.Size = new Size(84, 33);
            label2.TabIndex = 2;
            label2.Text = "输出文件路径:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtDestFilePath
            // 
            txtDestFilePath.Dock = DockStyle.Fill;
            txtDestFilePath.Location = new Point(93, 34);
            txtDestFilePath.Name = "txtDestFilePath";
            txtDestFilePath.PlaceholderText = "请选择文件输出路径";
            txtDestFilePath.Size = new Size(617, 23);
            txtDestFilePath.TabIndex = 3;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Fill;
            button1.Location = new Point(716, 3);
            button1.Name = "button1";
            button1.Size = new Size(81, 25);
            button1.TabIndex = 4;
            button1.Text = "...选择";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Fill;
            button2.Location = new Point(716, 34);
            button2.Name = "button2";
            button2.Size = new Size(81, 27);
            button2.TabIndex = 4;
            button2.Text = "...选择";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45.3405037F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 54.6594963F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel2.Controls.Add(groupBox3, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(0, 64);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(800, 95);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtDBFLength);
            groupBox1.Controls.Add(txtDBFTitle);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(229, 89);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "要拆分的字段";
            // 
            // txtDBFLength
            // 
            txtDBFLength.Location = new Point(68, 56);
            txtDBFLength.Name = "txtDBFLength";
            txtDBFLength.Size = new Size(136, 23);
            txtDBFLength.TabIndex = 2;
            // 
            // txtDBFTitle
            // 
            txtDBFTitle.Location = new Point(68, 25);
            txtDBFTitle.Name = "txtDBFTitle";
            txtDBFTitle.Size = new Size(136, 23);
            txtDBFTitle.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 59);
            label4.Name = "label4";
            label4.Size = new Size(59, 17);
            label4.TabIndex = 1;
            label4.Text = "字段长度:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 28);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 0;
            label3.Text = "表头名称:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtDestField1Length);
            groupBox2.Controls.Add(txtDestField1Name);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(238, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(278, 89);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "拆分后的字段1";
            // 
            // txtDestField1Length
            // 
            txtDestField1Length.Location = new Point(76, 56);
            txtDestField1Length.Name = "txtDestField1Length";
            txtDestField1Length.Size = new Size(136, 23);
            txtDestField1Length.TabIndex = 2;
            // 
            // txtDestField1Name
            // 
            txtDestField1Name.Location = new Point(76, 25);
            txtDestField1Name.Name = "txtDestField1Name";
            txtDestField1Name.Size = new Size(136, 23);
            txtDestField1Name.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 28);
            label5.Name = "label5";
            label5.Size = new Size(59, 17);
            label5.TabIndex = 0;
            label5.Text = "表头名称:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(14, 59);
            label6.Name = "label6";
            label6.Size = new Size(59, 17);
            label6.TabIndex = 1;
            label6.Text = "字段长度:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtDestField2Length);
            groupBox3.Controls.Add(txtDestField2Name);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label8);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(522, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(275, 89);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "拆分后的字段2";
            // 
            // txtDestField2Length
            // 
            txtDestField2Length.Location = new Point(75, 56);
            txtDestField2Length.Name = "txtDestField2Length";
            txtDestField2Length.Size = new Size(136, 23);
            txtDestField2Length.TabIndex = 2;
            // 
            // txtDestField2Name
            // 
            txtDestField2Name.Location = new Point(75, 25);
            txtDestField2Name.Name = "txtDestField2Name";
            txtDestField2Name.Size = new Size(136, 23);
            txtDestField2Name.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(13, 59);
            label7.Name = "label7";
            label7.Size = new Size(59, 17);
            label7.TabIndex = 1;
            label7.Text = "字段长度:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(13, 28);
            label8.Name = "label8";
            label8.Size = new Size(59, 17);
            label8.TabIndex = 0;
            label8.Text = "表头名称:";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(button3, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Top;
            tableLayoutPanel3.Location = new Point(0, 159);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(800, 36);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.Location = new Point(3, 3);
            button3.Name = "button3";
            button3.Size = new Size(794, 30);
            button3.TabIndex = 0;
            button3.Text = "开始转换";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(txtStatus, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Bottom;
            tableLayoutPanel4.Location = new Point(0, 370);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(800, 80);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // txtStatus
            // 
            txtStatus.AutoSize = true;
            txtStatus.Dock = DockStyle.Fill;
            txtStatus.Location = new Point(3, 0);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(794, 80);
            txtStatus.TabIndex = 0;
            txtStatus.Text = "状态: ";
            // 
            // DBFTableHeadSplitForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Name = "DBFTableHeadSplitForm";
            Text = "批量处理DBF文件表头拆分";
            Load += DBFTableHeadSplitForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private TextBox txtReadFilePath;
        private Label label2;
        private TextBox txtDestFilePath;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox groupBox1;
        private TextBox txtDBFLength;
        private TextBox txtDBFTitle;
        private Label label4;
        private Label label3;
        private GroupBox groupBox2;
        private TextBox txtDestField1Length;
        private TextBox txtDestField1Name;
        private Label label5;
        private Label label6;
        private GroupBox groupBox3;
        private TextBox txtDestField2Length;
        private TextBox txtDestField2Name;
        private Label label7;
        private Label label8;
        private Button button1;
        private FolderBrowserDialog fbdReadPath;
        private FolderBrowserDialog fbdDestPath;
        private Button button2;
        private TableLayoutPanel tableLayoutPanel3;
        private Button button3;
        private TableLayoutPanel tableLayoutPanel4;
        private Label txtStatus;
    }
}