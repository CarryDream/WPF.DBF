namespace WF.DBF
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            ofdDBFFile = new OpenFileDialog();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            dataGridView1 = new DataGridView();
            oName = new DataGridViewTextBoxColumn();
            oType = new DataGridViewTextBoxColumn();
            oLength = new DataGridViewTextBoxColumn();
            isOutput = new DataGridViewCheckBoxColumn();
            nName = new DataGridViewTextBoxColumn();
            nType = new DataGridViewTextBoxColumn();
            nLength = new DataGridViewTextBoxColumn();
            nDefaultValue = new DataGridViewTextBoxColumn();
            groupBox2 = new GroupBox();
            txtDefaultValue = new TextBox();
            txtLength = new TextBox();
            cmbType = new ComboBox();
            txtName = new TextBox();
            btnAddHeader = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnDBFReader = new Button();
            btnShowContent = new Button();
            btnConvertExcel = new Button();
            btnExportExcel = new Button();
            btnExportDBF = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            txtDBFFilePath = new TextBox();
            btnChooseDBFFile = new Button();
            label1 = new Label();
            tabPage2 = new TabPage();
            groupBox3 = new GroupBox();
            tableLayoutPanel7 = new TableLayoutPanel();
            dataGridView2 = new DataGridView();
            eName = new DataGridViewTextBoxColumn();
            eType = new DataGridViewTextBoxColumn();
            eLength = new DataGridViewTextBoxColumn();
            eIsOutput = new DataGridViewCheckBoxColumn();
            enName = new DataGridViewTextBoxColumn();
            enType = new DataGridViewTextBoxColumn();
            enLength = new DataGridViewTextBoxColumn();
            enDefaultValue = new DataGridViewTextBoxColumn();
            groupBox4 = new GroupBox();
            txtExcelDefaultValue = new TextBox();
            txtExcelLength = new TextBox();
            cmbExcelType = new ComboBox();
            txtExcelName = new TextBox();
            btnAddExcelHeader = new Button();
            tableLayoutPanel6 = new TableLayoutPanel();
            btnExcelReader = new Button();
            btnShowExcelContent = new Button();
            btnExcelConvertDBF = new Button();
            btnExcelToExcel = new Button();
            btnExcelToDBF = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            txtExcelFilePath = new TextBox();
            btnChooseExcelFile = new Button();
            label2 = new Label();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            btnBatchSplitDBFHeader = new Button();
            tabPage5 = new TabPage();
            tableLayoutPanel8 = new TableLayoutPanel();
            miniToolStrip = new MenuStrip();
            ofdExcelFile = new OpenFileDialog();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox3.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            groupBox4.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            SuspendLayout();
            // 
            // ofdDBFFile
            // 
            ofdDBFFile.FileName = "ofdDBFFile";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(934, 500);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel3);
            tabPage1.Controls.Add(tableLayoutPanel2);
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(926, 470);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "读取DBF";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 66);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(920, 401);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel4);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(914, 395);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "DBF表头";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(dataGridView1, 0, 0);
            tableLayoutPanel4.Controls.Add(groupBox2, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel4.Size = new Size(908, 373);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { oName, oType, oLength, isOutput, nName, nType, nLength, nDefaultValue });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(902, 313);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // oName
            // 
            oName.DataPropertyName = "oName";
            oName.HeaderText = "源名称";
            oName.Name = "oName";
            oName.ReadOnly = true;
            oName.Width = 107;
            // 
            // oType
            // 
            oType.DataPropertyName = "oType";
            oType.HeaderText = "源类型";
            oType.Name = "oType";
            oType.ReadOnly = true;
            oType.Width = 108;
            // 
            // oLength
            // 
            oLength.DataPropertyName = "oLength";
            oLength.HeaderText = "源长度";
            oLength.Name = "oLength";
            oLength.ReadOnly = true;
            oLength.Width = 107;
            // 
            // isOutput
            // 
            isOutput.DataPropertyName = "isOutput";
            isOutput.HeaderText = "是否输出";
            isOutput.Name = "isOutput";
            isOutput.Width = 108;
            // 
            // nName
            // 
            nName.DataPropertyName = "nName";
            nName.HeaderText = "输出名称";
            nName.Name = "nName";
            nName.Width = 107;
            // 
            // nType
            // 
            nType.DataPropertyName = "nType";
            nType.HeaderText = "输出类型";
            nType.Name = "nType";
            nType.Width = 107;
            // 
            // nLength
            // 
            nLength.DataPropertyName = "nLength";
            nLength.HeaderText = "输出长度";
            nLength.Name = "nLength";
            nLength.Width = 108;
            // 
            // nDefaultValue
            // 
            nDefaultValue.DataPropertyName = "nDefaultValue";
            nDefaultValue.HeaderText = "输出默认值";
            nDefaultValue.Name = "nDefaultValue";
            nDefaultValue.Width = 107;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txtDefaultValue);
            groupBox2.Controls.Add(txtLength);
            groupBox2.Controls.Add(cmbType);
            groupBox2.Controls.Add(txtName);
            groupBox2.Controls.Add(btnAddHeader);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 322);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(902, 48);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "添加DBF表头";
            // 
            // txtDefaultValue
            // 
            txtDefaultValue.Location = new Point(372, 19);
            txtDefaultValue.Name = "txtDefaultValue";
            txtDefaultValue.PlaceholderText = "输出默认值";
            txtDefaultValue.Size = new Size(128, 23);
            txtDefaultValue.TabIndex = 4;
            // 
            // txtLength
            // 
            txtLength.Location = new Point(261, 19);
            txtLength.Name = "txtLength";
            txtLength.PlaceholderText = "输出长度";
            txtLength.Size = new Size(105, 23);
            txtLength.TabIndex = 3;
            // 
            // cmbType
            // 
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "请选择输出类型" });
            cmbType.Location = new Point(134, 17);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(121, 25);
            cmbType.TabIndex = 2;
            // 
            // txtName
            // 
            txtName.Location = new Point(6, 17);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "输出名称";
            txtName.Size = new Size(122, 23);
            txtName.TabIndex = 1;
            txtName.Leave += txtName_Leave;
            // 
            // btnAddHeader
            // 
            btnAddHeader.Location = new Point(827, 17);
            btnAddHeader.Name = "btnAddHeader";
            btnAddHeader.Size = new Size(75, 23);
            btnAddHeader.TabIndex = 0;
            btnAddHeader.Text = "添加";
            btnAddHeader.UseVisualStyleBackColor = true;
            btnAddHeader.Click += btnAddHeader_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 5;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 106F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 151F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 437F));
            tableLayoutPanel2.Controls.Add(btnDBFReader, 0, 0);
            tableLayoutPanel2.Controls.Add(btnShowContent, 1, 0);
            tableLayoutPanel2.Controls.Add(btnConvertExcel, 2, 0);
            tableLayoutPanel2.Controls.Add(btnExportExcel, 3, 0);
            tableLayoutPanel2.Controls.Add(btnExportDBF, 4, 0);
            tableLayoutPanel2.Dock = DockStyle.Top;
            tableLayoutPanel2.Location = new Point(3, 37);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(920, 29);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // btnDBFReader
            // 
            btnDBFReader.Location = new Point(3, 3);
            btnDBFReader.Name = "btnDBFReader";
            btnDBFReader.Size = new Size(75, 23);
            btnDBFReader.TabIndex = 3;
            btnDBFReader.Text = "读取文件";
            btnDBFReader.UseVisualStyleBackColor = true;
            btnDBFReader.Click += btnDBFReader_Click;
            // 
            // btnShowContent
            // 
            btnShowContent.Location = new Point(99, 3);
            btnShowContent.Name = "btnShowContent";
            btnShowContent.Size = new Size(75, 23);
            btnShowContent.TabIndex = 5;
            btnShowContent.Text = "数据查询";
            btnShowContent.UseVisualStyleBackColor = true;
            btnShowContent.Click += btnShowContent_Click;
            // 
            // btnConvertExcel
            // 
            btnConvertExcel.Location = new Point(194, 3);
            btnConvertExcel.Name = "btnConvertExcel";
            btnConvertExcel.Size = new Size(87, 23);
            btnConvertExcel.TabIndex = 6;
            btnConvertExcel.Text = "DBF转Excel";
            btnConvertExcel.UseVisualStyleBackColor = true;
            btnConvertExcel.Click += btnConvertExcel_Click;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(300, 3);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(139, 23);
            btnExportExcel.TabIndex = 7;
            btnExportExcel.Text = "按表头导出Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            btnExportExcel.Click += btnExportExcel_Click;
            // 
            // btnExportDBF
            // 
            btnExportDBF.Location = new Point(451, 3);
            btnExportDBF.Name = "btnExportDBF";
            btnExportDBF.Size = new Size(148, 23);
            btnExportDBF.TabIndex = 8;
            btnExportDBF.Text = "按表头导出DBF";
            btnExportDBF.UseVisualStyleBackColor = true;
            btnExportDBF.Click += btnExportDBF_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.760141F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92.23986F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            tableLayoutPanel1.Controls.Add(txtDBFFilePath, 1, 0);
            tableLayoutPanel1.Controls.Add(btnChooseDBFFile, 2, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 37.9629631F));
            tableLayoutPanel1.Size = new Size(920, 34);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // txtDBFFilePath
            // 
            txtDBFFilePath.AllowDrop = true;
            txtDBFFilePath.Dock = DockStyle.Fill;
            txtDBFFilePath.Location = new Point(67, 3);
            txtDBFFilePath.Name = "txtDBFFilePath";
            txtDBFFilePath.PlaceholderText = "请选择要读取的DBF文件绝对路径";
            txtDBFFilePath.Size = new Size(765, 23);
            txtDBFFilePath.TabIndex = 1;
            txtDBFFilePath.DragDrop += txtDBFFilePath_DragDrop;
            txtDBFFilePath.DragEnter += txtDBFFilePath_DragEnter;
            // 
            // btnChooseDBFFile
            // 
            btnChooseDBFFile.Location = new Point(838, 3);
            btnChooseDBFFile.Name = "btnChooseDBFFile";
            btnChooseDBFFile.Size = new Size(75, 23);
            btnChooseDBFFile.TabIndex = 2;
            btnChooseDBFFile.Text = "...浏览";
            btnChooseDBFFile.UseVisualStyleBackColor = true;
            btnChooseDBFFile.Click += btnChooseDBFFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(35, 17);
            label1.TabIndex = 0;
            label1.Text = "文件:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(tableLayoutPanel6);
            tabPage2.Controls.Add(tableLayoutPanel5);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(926, 470);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "读取Excel";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(tableLayoutPanel7);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(3, 66);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(920, 401);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Excel表头";
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(dataGridView2, 0, 0);
            tableLayoutPanel7.Controls.Add(groupBox4, 0, 1);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(3, 19);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel7.Size = new Size(914, 379);
            tableLayoutPanel7.TabIndex = 1;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { eName, eType, eLength, eIsOutput, enName, enType, enLength, enDefaultValue });
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(3, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(908, 319);
            dataGridView2.TabIndex = 0;
            dataGridView2.CellContentClick += dataGridView2_CellContentClick;
            // 
            // eName
            // 
            eName.DataPropertyName = "oName";
            eName.HeaderText = "源名称";
            eName.Name = "eName";
            eName.ReadOnly = true;
            eName.Width = 107;
            // 
            // eType
            // 
            eType.DataPropertyName = "oType";
            eType.HeaderText = "源类型";
            eType.Name = "eType";
            eType.ReadOnly = true;
            eType.Width = 108;
            // 
            // eLength
            // 
            eLength.DataPropertyName = "oLength";
            eLength.HeaderText = "源长度";
            eLength.Name = "eLength";
            eLength.ReadOnly = true;
            eLength.Width = 107;
            // 
            // eIsOutput
            // 
            eIsOutput.DataPropertyName = "isOutput";
            eIsOutput.HeaderText = "是否输出";
            eIsOutput.Name = "eIsOutput";
            eIsOutput.Width = 108;
            // 
            // enName
            // 
            enName.DataPropertyName = "nName";
            enName.HeaderText = "输出名称";
            enName.Name = "enName";
            enName.Width = 107;
            // 
            // enType
            // 
            enType.DataPropertyName = "nType";
            enType.HeaderText = "输出类型";
            enType.Name = "enType";
            enType.Width = 107;
            // 
            // enLength
            // 
            enLength.DataPropertyName = "nLength";
            enLength.HeaderText = "输出长度";
            enLength.Name = "enLength";
            enLength.Width = 108;
            // 
            // enDefaultValue
            // 
            enDefaultValue.DataPropertyName = "nDefaultValue";
            enDefaultValue.HeaderText = "输出默认值";
            enDefaultValue.Name = "enDefaultValue";
            enDefaultValue.Width = 107;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(txtExcelDefaultValue);
            groupBox4.Controls.Add(txtExcelLength);
            groupBox4.Controls.Add(cmbExcelType);
            groupBox4.Controls.Add(txtExcelName);
            groupBox4.Controls.Add(btnAddExcelHeader);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(3, 328);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(908, 48);
            groupBox4.TabIndex = 0;
            groupBox4.TabStop = false;
            groupBox4.Text = "添加Excel表头";
            // 
            // txtExcelDefaultValue
            // 
            txtExcelDefaultValue.Location = new Point(372, 19);
            txtExcelDefaultValue.Name = "txtExcelDefaultValue";
            txtExcelDefaultValue.PlaceholderText = "输出默认值";
            txtExcelDefaultValue.Size = new Size(128, 23);
            txtExcelDefaultValue.TabIndex = 4;
            // 
            // txtExcelLength
            // 
            txtExcelLength.Location = new Point(261, 19);
            txtExcelLength.Name = "txtExcelLength";
            txtExcelLength.PlaceholderText = "输出长度";
            txtExcelLength.Size = new Size(105, 23);
            txtExcelLength.TabIndex = 3;
            // 
            // cmbExcelType
            // 
            cmbExcelType.FormattingEnabled = true;
            cmbExcelType.Items.AddRange(new object[] { "请选择输出类型" });
            cmbExcelType.Location = new Point(134, 17);
            cmbExcelType.Name = "cmbExcelType";
            cmbExcelType.Size = new Size(121, 25);
            cmbExcelType.TabIndex = 2;
            // 
            // txtExcelName
            // 
            txtExcelName.Location = new Point(6, 17);
            txtExcelName.Name = "txtExcelName";
            txtExcelName.PlaceholderText = "输出名称";
            txtExcelName.Size = new Size(122, 23);
            txtExcelName.TabIndex = 1;
            txtExcelName.Leave += txtExcelName_Leave;
            // 
            // btnAddExcelHeader
            // 
            btnAddExcelHeader.Location = new Point(827, 17);
            btnAddExcelHeader.Name = "btnAddExcelHeader";
            btnAddExcelHeader.Size = new Size(75, 23);
            btnAddExcelHeader.TabIndex = 0;
            btnAddExcelHeader.Text = "添加";
            btnAddExcelHeader.UseVisualStyleBackColor = true;
            btnAddExcelHeader.Click += btnAddExcelHeader_Click;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 5;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 96F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 106F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 151F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 437F));
            tableLayoutPanel6.Controls.Add(btnExcelReader, 0, 0);
            tableLayoutPanel6.Controls.Add(btnShowExcelContent, 1, 0);
            tableLayoutPanel6.Controls.Add(btnExcelConvertDBF, 2, 0);
            tableLayoutPanel6.Controls.Add(btnExcelToExcel, 3, 0);
            tableLayoutPanel6.Controls.Add(btnExcelToDBF, 4, 0);
            tableLayoutPanel6.Dock = DockStyle.Top;
            tableLayoutPanel6.Location = new Point(3, 37);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(920, 29);
            tableLayoutPanel6.TabIndex = 3;
            // 
            // btnExcelReader
            // 
            btnExcelReader.Location = new Point(3, 3);
            btnExcelReader.Name = "btnExcelReader";
            btnExcelReader.Size = new Size(75, 23);
            btnExcelReader.TabIndex = 3;
            btnExcelReader.Text = "读取文件";
            btnExcelReader.UseVisualStyleBackColor = true;
            btnExcelReader.Click += btnExcelReader_Click;
            // 
            // btnShowExcelContent
            // 
            btnShowExcelContent.Location = new Point(99, 3);
            btnShowExcelContent.Name = "btnShowExcelContent";
            btnShowExcelContent.Size = new Size(75, 23);
            btnShowExcelContent.TabIndex = 5;
            btnShowExcelContent.Text = "数据查询";
            btnShowExcelContent.UseVisualStyleBackColor = true;
            btnShowExcelContent.Click += btnShowExcelContent_Click;
            // 
            // btnExcelConvertDBF
            // 
            btnExcelConvertDBF.Location = new Point(194, 3);
            btnExcelConvertDBF.Name = "btnExcelConvertDBF";
            btnExcelConvertDBF.Size = new Size(87, 23);
            btnExcelConvertDBF.TabIndex = 6;
            btnExcelConvertDBF.Text = "Excel转DBF";
            btnExcelConvertDBF.UseVisualStyleBackColor = true;
            btnExcelConvertDBF.Click += btnExcelConvertDBF_Click;
            // 
            // btnExcelToExcel
            // 
            btnExcelToExcel.Location = new Point(300, 3);
            btnExcelToExcel.Name = "btnExcelToExcel";
            btnExcelToExcel.Size = new Size(139, 23);
            btnExcelToExcel.TabIndex = 7;
            btnExcelToExcel.Text = "按表头导出Excel";
            btnExcelToExcel.UseVisualStyleBackColor = true;
            btnExcelToExcel.Click += btnExcelToExcel_Click;
            // 
            // btnExcelToDBF
            // 
            btnExcelToDBF.Location = new Point(451, 3);
            btnExcelToDBF.Name = "btnExcelToDBF";
            btnExcelToDBF.Size = new Size(148, 23);
            btnExcelToDBF.TabIndex = 8;
            btnExcelToDBF.Text = "按表头导出DBF";
            btnExcelToDBF.UseVisualStyleBackColor = true;
            btnExcelToDBF.Click += btnExcelToDBF_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.760141F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 92.23986F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 84F));
            tableLayoutPanel5.Controls.Add(txtExcelFilePath, 1, 0);
            tableLayoutPanel5.Controls.Add(btnChooseExcelFile, 2, 0);
            tableLayoutPanel5.Controls.Add(label2, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Top;
            tableLayoutPanel5.Location = new Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 37.9629631F));
            tableLayoutPanel5.Size = new Size(920, 34);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // txtExcelFilePath
            // 
            txtExcelFilePath.AllowDrop = true;
            txtExcelFilePath.Dock = DockStyle.Fill;
            txtExcelFilePath.Location = new Point(67, 3);
            txtExcelFilePath.Name = "txtExcelFilePath";
            txtExcelFilePath.PlaceholderText = "请选择要读取的Excel文件绝对路径";
            txtExcelFilePath.Size = new Size(765, 23);
            txtExcelFilePath.TabIndex = 1;
            txtExcelFilePath.DragDrop += txtExcelFilePath_DragDrop;
            txtExcelFilePath.DragEnter += txtExcelFilePath_DragEnter;
            // 
            // btnChooseExcelFile
            // 
            btnChooseExcelFile.Location = new Point(838, 3);
            btnChooseExcelFile.Name = "btnChooseExcelFile";
            btnChooseExcelFile.Size = new Size(75, 23);
            btnChooseExcelFile.TabIndex = 2;
            btnChooseExcelFile.Text = "...浏览";
            btnChooseExcelFile.UseVisualStyleBackColor = true;
            btnChooseExcelFile.Click += btnChooseExcelFile_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(35, 17);
            label2.TabIndex = 0;
            label2.Text = "文件:";
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(926, 470);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "读取剪切板";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(btnBatchSplitDBFHeader);
            tabPage4.Location = new Point(4, 26);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(926, 470);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "其它";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnBatchSplitDBFHeader
            // 
            btnBatchSplitDBFHeader.Location = new Point(26, 41);
            btnBatchSplitDBFHeader.Name = "btnBatchSplitDBFHeader";
            btnBatchSplitDBFHeader.Size = new Size(216, 23);
            btnBatchSplitDBFHeader.TabIndex = 0;
            btnBatchSplitDBFHeader.Text = "批量拆分DBF文件文件表头";
            btnBatchSplitDBFHeader.UseVisualStyleBackColor = true;
            btnBatchSplitDBFHeader.Click += btnBatchSplitDBFHeader_Click;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(tableLayoutPanel8);
            tabPage5.Location = new Point(4, 26);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(926, 470);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "介绍";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 1;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 3);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Size = new Size(920, 464);
            tableLayoutPanel8.TabIndex = 0;
            // 
            // miniToolStrip
            // 
            miniToolStrip.AccessibleName = "New item selection";
            miniToolStrip.AccessibleRole = AccessibleRole.ComboBox;
            miniToolStrip.AutoSize = false;
            miniToolStrip.Dock = DockStyle.None;
            miniToolStrip.Location = new Point(6, 2);
            miniToolStrip.Name = "miniToolStrip";
            miniToolStrip.Size = new Size(652, 24);
            miniToolStrip.TabIndex = 3;
            // 
            // ofdExcelFile
            // 
            ofdExcelFile.FileName = "ofdExcelFile";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 500);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DBF工具 By jinht";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            tableLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog ofdDBFFile;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel2;
        private Button btnDBFReader;
        private Button btnShowContent;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtDBFFilePath;
        private Button btnChooseDBFFile;
        private Label label1;
        private MenuStrip miniToolStrip;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Button btnConvertExcel;
        private GroupBox groupBox2;
        private TextBox txtName;
        private Button btnAddHeader;
        private ComboBox cmbType;
        private TableLayoutPanel tableLayoutPanel4;
        private TextBox txtDefaultValue;
        private TextBox txtLength;
        private DataGridViewTextBoxColumn oName;
        private DataGridViewTextBoxColumn oType;
        private DataGridViewTextBoxColumn oLength;
        private DataGridViewCheckBoxColumn isOutput;
        private DataGridViewTextBoxColumn nName;
        private DataGridViewTextBoxColumn nType;
        private DataGridViewTextBoxColumn nLength;
        private DataGridViewTextBoxColumn nDefaultValue;
        private Button btnExportExcel;
        private Button btnExportDBF;
        private TableLayoutPanel tableLayoutPanel5;
        private TextBox txtExcelFilePath;
        private Button btnChooseExcelFile;
        private Label label2;
        private GroupBox groupBox3;
        private TableLayoutPanel tableLayoutPanel7;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private GroupBox groupBox4;
        private TextBox txtExcelDefaultValue;
        private TextBox txtExcelLength;
        private ComboBox cmbExcelType;
        private TextBox txtExcelName;
        private Button btnAddExcelHeader;
        private TableLayoutPanel tableLayoutPanel6;
        private Button btnExcelReader;
        private Button btnShowExcelContent;
        private Button btnExcelConvertDBF;
        private Button btnExcelToExcel;
        private Button btnExcelToDBF;
        private OpenFileDialog ofdExcelFile;
        private DataGridViewTextBoxColumn eName;
        private DataGridViewTextBoxColumn eType;
        private DataGridViewTextBoxColumn eLength;
        private DataGridViewCheckBoxColumn eIsOutput;
        private DataGridViewTextBoxColumn enName;
        private DataGridViewTextBoxColumn enType;
        private DataGridViewTextBoxColumn enLength;
        private DataGridViewTextBoxColumn enDefaultValue;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TableLayoutPanel tableLayoutPanel8;
        private Button btnBatchSplitDBFHeader;
    }
}