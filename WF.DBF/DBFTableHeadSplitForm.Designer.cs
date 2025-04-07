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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBFTableHeadSplitForm));
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            txtReadFilePath = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            txtDestFilePath = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            txtDBFLength = new System.Windows.Forms.TextBox();
            txtDBFTitle = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            txtDestField1Length = new System.Windows.Forms.TextBox();
            txtDestField1Name = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            txtDestField2Length = new System.Windows.Forms.TextBox();
            txtDestField2Name = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            fbdReadPath = new System.Windows.Forms.FolderBrowserDialog();
            fbdDestPath = new System.Windows.Forms.FolderBrowserDialog();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            button3 = new System.Windows.Forms.Button();
            tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            txtStatus = new System.Windows.Forms.Label();
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
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(txtReadFilePath, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(txtDestFilePath, 1, 1);
            tableLayoutPanel1.Controls.Add(button1, 2, 0);
            tableLayoutPanel1.Controls.Add(button2, 2, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            tableLayoutPanel1.Size = new System.Drawing.Size(800, 64);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(84, 31);
            label1.TabIndex = 0;
            label1.Text = "读取文件路径:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReadFilePath
            // 
            txtReadFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            txtReadFilePath.Location = new System.Drawing.Point(93, 3);
            txtReadFilePath.Name = "txtReadFilePath";
            txtReadFilePath.PlaceholderText = "请选择读取文件夹路径";
            txtReadFilePath.Size = new System.Drawing.Size(617, 23);
            txtReadFilePath.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Location = new System.Drawing.Point(3, 31);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(84, 33);
            label2.TabIndex = 2;
            label2.Text = "输出文件路径:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDestFilePath
            // 
            txtDestFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            txtDestFilePath.Location = new System.Drawing.Point(93, 34);
            txtDestFilePath.Name = "txtDestFilePath";
            txtDestFilePath.PlaceholderText = "请选择文件输出路径";
            txtDestFilePath.Size = new System.Drawing.Size(617, 23);
            txtDestFilePath.TabIndex = 3;
            // 
            // button1
            // 
            button1.Dock = System.Windows.Forms.DockStyle.Fill;
            button1.Location = new System.Drawing.Point(716, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(81, 25);
            button1.TabIndex = 4;
            button1.Text = "...选择";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Dock = System.Windows.Forms.DockStyle.Fill;
            button2.Location = new System.Drawing.Point(716, 34);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(81, 27);
            button2.TabIndex = 4;
            button2.Text = "...选择";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.340504F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.659496F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel2.Controls.Add(groupBox3, 2, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 64);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(800, 95);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(txtDBFLength);
            groupBox1.Controls.Add(txtDBFTitle);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Location = new System.Drawing.Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(229, 89);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "要拆分的字段";
            // 
            // txtDBFLength
            // 
            txtDBFLength.Location = new System.Drawing.Point(68, 56);
            txtDBFLength.Name = "txtDBFLength";
            txtDBFLength.Size = new System.Drawing.Size(136, 23);
            txtDBFLength.TabIndex = 2;
            // 
            // txtDBFTitle
            // 
            txtDBFTitle.Location = new System.Drawing.Point(68, 25);
            txtDBFTitle.Name = "txtDBFTitle";
            txtDBFTitle.Size = new System.Drawing.Size(136, 23);
            txtDBFTitle.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 59);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(59, 17);
            label4.TabIndex = 1;
            label4.Text = "字段长度:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 28);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 17);
            label3.TabIndex = 0;
            label3.Text = "表头名称:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtDestField1Length);
            groupBox2.Controls.Add(txtDestField1Name);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label6);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox2.Location = new System.Drawing.Point(238, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(278, 89);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "拆分后的字段1";
            // 
            // txtDestField1Length
            // 
            txtDestField1Length.Location = new System.Drawing.Point(76, 56);
            txtDestField1Length.Name = "txtDestField1Length";
            txtDestField1Length.Size = new System.Drawing.Size(136, 23);
            txtDestField1Length.TabIndex = 2;
            // 
            // txtDestField1Name
            // 
            txtDestField1Name.Location = new System.Drawing.Point(76, 25);
            txtDestField1Name.Name = "txtDestField1Name";
            txtDestField1Name.Size = new System.Drawing.Size(136, 23);
            txtDestField1Name.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(14, 28);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(59, 17);
            label5.TabIndex = 0;
            label5.Text = "表头名称:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(14, 59);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(59, 17);
            label6.TabIndex = 1;
            label6.Text = "字段长度:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(txtDestField2Length);
            groupBox3.Controls.Add(txtDestField2Name);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label8);
            groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox3.Location = new System.Drawing.Point(522, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(275, 89);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "拆分后的字段2";
            // 
            // txtDestField2Length
            // 
            txtDestField2Length.Location = new System.Drawing.Point(75, 56);
            txtDestField2Length.Name = "txtDestField2Length";
            txtDestField2Length.Size = new System.Drawing.Size(136, 23);
            txtDestField2Length.TabIndex = 2;
            // 
            // txtDestField2Name
            // 
            txtDestField2Name.Location = new System.Drawing.Point(75, 25);
            txtDestField2Name.Name = "txtDestField2Name";
            txtDestField2Name.Size = new System.Drawing.Size(136, 23);
            txtDestField2Name.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(13, 59);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 17);
            label7.TabIndex = 1;
            label7.Text = "字段长度:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(13, 28);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(59, 17);
            label8.TabIndex = 0;
            label8.Text = "表头名称:";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(button3, 0, 0);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 159);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new System.Drawing.Size(800, 36);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // button3
            // 
            button3.Dock = System.Windows.Forms.DockStyle.Fill;
            button3.Location = new System.Drawing.Point(3, 3);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(794, 30);
            button3.TabIndex = 0;
            button3.Text = "开始转换";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(txtStatus, 0, 0);
            tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            tableLayoutPanel4.Location = new System.Drawing.Point(0, 370);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new System.Drawing.Size(800, 80);
            tableLayoutPanel4.TabIndex = 3;
            // 
            // txtStatus
            // 
            txtStatus.AutoSize = true;
            txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            txtStatus.Location = new System.Drawing.Point(3, 0);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new System.Drawing.Size(794, 80);
            txtStatus.TabIndex = 0;
            txtStatus.Text = "状态: ";
            // 
            // DBFTableHeadSplitForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel3);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Icon = ((System.Drawing.Icon)resources.GetObject("$this.Icon"));
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
        private System.Windows.Forms.TextBox txtReadFilePath;
        private Label label2;
        private System.Windows.Forms.TextBox txtDestFilePath;
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