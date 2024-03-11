using ClosedXML.Excel;
using Markdig;
using SocialExplorer.IO.FastDBF;
using System.ComponentModel;
using System.Data;
using System.Text;
using WF.DBF.Entity;
using WF.DBF.Utils;
using static WF.DBF.Utils.DBFUtil;
using Color = System.Drawing.Color;

namespace WF.DBF
{
    public partial class Form1 : Form
    {
        // DBF 读取到的数据
        private DataTable dbfDt = null;
        // DBF 选择的表头
        private BindingList<DataGridViewDBFField> dbfDgv = new BindingList<DataGridViewDBFField>();
        // DBF 文件打开目录
        private string dbfOpenPath = string.Empty;
        // DBF 文件保存目录
        private string dbfSavePath = string.Empty;

        // Excel 读取到的数据
        private DataTable excelDt = null;
        // Excel 选择的表头
        private BindingList<DataGridViewDBFField> excelDgv = new BindingList<DataGridViewDBFField>();
        // Excel 文件打开目录
        private string excelOpenPath = string.Empty;
        // Excel 文件保存目录
        private string excelSavePath = string.Empty;


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 关闭窗口前执行的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确实要退出吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ======> 读取 DBF
            // 禁用按钮
            this.btnShowContent.Enabled = false;
            this.btnConvertExcel.Enabled = false;
            this.btnExportExcel.Enabled = false;
            this.btnExportDBF.Enabled = false;

            this.cmbType.Items.Clear();
            this.cmbType.Items.Insert(0, "请选择输出类型");
            this.cmbType.Items.Add("System.String");
            this.cmbType.SelectedIndex = 0;

            // ======> 读取 Excel
            this.btnShowExcelContent.Enabled = false;
            this.btnExcelConvertDBF.Enabled = false;
            this.btnExcelToExcel.Enabled = false;
            this.btnExcelToDBF.Enabled = false;

            this.cmbExcelType.Items.Clear();
            this.cmbExcelType.Items.Insert(0, "请选择输出类型");
            this.cmbExcelType.Items.Add("System.String");
            this.cmbExcelType.SelectedIndex = 0;

        }

        // =========>> DBF 文件读取 <<==========

        /// <summary>
        /// 选择 DBF 文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseDBFFile_Click(object sender, EventArgs e)
        {
            this.ofdDBFFile.Title = "请选择DBF文件";
            this.ofdDBFFile.Filter = "DBF文件 (*.dbf)|*.dbf";
            this.ofdDBFFile.FileName = "";
            this.ofdDBFFile.FilterIndex = 1;
            this.ofdDBFFile.RestoreDirectory = true;
            if (this.ofdDBFFile.ShowDialog() == DialogResult.OK)
            {
                this.txtDBFFilePath.Text = this.ofdDBFFile.FileName;
                this.dbfOpenPath = Path.GetDirectoryName(this.ofdDBFFile.FileName);
                this.dbfSavePath = this.dbfOpenPath;
            }
        }

        /// <summary>
        /// 读取 DBF 文件内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDBFReader_Click(object sender, EventArgs e)
        {
            string dbfFilePath = this.txtDBFFilePath.Text;
            if (dbfFilePath == "")
            {
                MessageBox.Show("请选择要读取的DBF文件");
                return;
            }

            DBFUtil.TDbfTable dbfTable = new DBFUtil.TDbfTable(dbfFilePath);
            // 表格内容赋值
            dbfDt = dbfTable.Table;
            // 启用按钮
            this.btnShowContent.Enabled = true;
            this.btnConvertExcel.Enabled = true;
            this.btnExportExcel.Enabled = true;
            this.btnExportDBF.Enabled = true;

            dbfDgv.Clear();
            TDbfField[] dbfFields = dbfTable.DbfFields;
            int fieldCount = dbfFields.Length;
            for (int i = 0; i < fieldCount; i++)
            {
                dbfDgv.Add(new DataGridViewDBFField(
                    dbfFields[i].GetFieldName(dbfTable.Encoding),
                    dbfFields[i].FieldType.ToString(),
                    dbfFields[i].Length,
                    false
                ));
            }

            this.dataGridView1.AutoSize = true;
            this.dataGridView1.DataSource = dbfDgv;
            // DataGridView 内容宽度自动填满
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Refresh();
            // 设置 DataGridView 序号
            this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
        }

        private void dataGridView1_RowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = !Convert.ToBoolean(cell.Value);
                if (cell.Value.ToString() == "True")
                {
                    this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                    if (this.dataGridView1.Rows[e.RowIndex].Cells["nName"].Value == null)
                    {
                        string oName = this.dataGridView1.Rows[e.RowIndex].Cells["oName"].Value.ToString();
                        if (oName.StartsWith("报名号") || oName.StartsWith("考生号") || oName.ToUpper() == "BMH" || oName.ToUpper() == "KSH" || oName.ToUpper() == "GKBMH")
                        {
                            oName = "KSH";
                            this.dataGridView1.Rows[e.RowIndex].Cells["nLength"].Value = "14";
                        }
                        else if (oName.StartsWith("证件号") || oName.StartsWith("身份证") || oName.ToUpper().StartsWith("SFZ") || oName.ToUpper().StartsWith("ZJH"))
                        {
                            oName = "ZJH";
                            this.dataGridView1.Rows[e.RowIndex].Cells["nLength"].Value = "20";
                        }
                        else if (oName.StartsWith("姓名") || oName.StartsWith("考生姓名") || oName.ToUpper() == "XM" || oName.ToUpper() == "KSXM")
                        {
                            oName = "KSXM";
                            this.dataGridView1.Rows[e.RowIndex].Cells["nLength"].Value = "30";
                        }

                        this.dataGridView1.Rows[e.RowIndex].Cells["nName"].Value = oName;
                    }

                    if (this.dataGridView1.Rows[e.RowIndex].Cells["nType"].Value == null)
                    {
                        this.dataGridView1.Rows[e.RowIndex].Cells["nType"].Value = this.dataGridView1.Rows[e.RowIndex].Cells["oType"].Value;
                    }
                    if (this.dataGridView1.Rows[e.RowIndex].Cells["nLength"].Value.ToString() == "0")
                    {
                        this.dataGridView1.Rows[e.RowIndex].Cells["nLength"].Value = this.dataGridView1.Rows[e.RowIndex].Cells["oLength"].Value;
                    }
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void btnShowContent_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm()
            {
                Dt = this.dbfDt,
                QueryDt = this.dbfDt,
                Bs = this.dbfDgv
            };
            dataViewForm.ShowDialog();
        }

        private void btnConvertExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.InitialDirectory = this.dbfSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.FileName = dbfDt.TableName + "_" + dbfDt.Rows.Count + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                // 赋值保存DBF文件路径
                this.dbfSavePath = Path.GetDirectoryName(filename);

                XLWorkbook wb = new XLWorkbook();
                // Add worksheet with data
                var worksheet = wb.Worksheets.Add(dbfDt, "Sheet1");
                // Formattings Sheet
                worksheet.Table(0).Theme = XLTableTheme.TableStyleLight20;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.SheetView.FreezeRows(1);
                worksheet.Columns().AdjustToContents(10.0, 50.0);
                // Export the Excel file
                wb.SaveAs(filename);
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }

        private void btnAddHeader_Click(object sender, EventArgs e)
        {
            var name = this.txtName.Text;
            var type = this.cmbType.Text;
            var length = this.txtLength.Text;
            var defaultValue = this.txtDefaultValue.Text;

            if (name == "")
            {
                MessageBox.Show("输出名称不能为空!");
                return;
            }

            if (type == "")
            {
                MessageBox.Show("输出类型不能为空!");
                return;
            }

            if (length == "")
            {
                MessageBox.Show("输出长度不能为空!");
                return;
            }

            DataGridViewDBFField dgv = new DataGridViewDBFField();
            dgv.IsOutput = true;
            dgv.NName = name;
            dgv.NType = type;
            dgv.NLength = int.Parse(length);
            dgv.NDefaultValue = defaultValue;
            dbfDgv.Add(dgv);

            this.dataGridView1.DataSource = dbfDgv;
            this.dataGridView1.Refresh();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // 1. 获取已选择的表头信息
            int cnt = dbfDgv.Count;
            List<DataGridViewDBFField> fields = new List<DataGridViewDBFField>();
            for (int i = 0; i < cnt; i++)
            {
                if (dbfDgv[i].IsOutput)
                {
                    fields.Add(dbfDgv[i]);
                }
            }

            if (fields.Count == 0)
            {
                MessageBox.Show("请至少选择一个表头!");
                return;
            }

            // 2. 获取DBF文件内容信息
            int rows = dbfDt.Rows.Count;
            int columns = dbfDt.Columns.Count;

            // 设置表头
            DataTable excelDataTable = new DataTable();
            excelDataTable.TableName = dbfDt.TableName + "_new_" + dbfDt.Rows.Count;
            for (int i = 0; i < fields.Count; i++)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = fields[i].NName;
                col.Caption = fields[i].NName;
                col.DataType = Type.GetType(fields[i].NType);
                excelDataTable.Columns.Add(col);
            }

            // 3. 封装成新的DataTable
            for (int i = 0; i < rows; i++)
            {
                DataRow dr = excelDataTable.NewRow();
                for (int j = 0; j < fields.Count; j++)
                {
                    if (String.IsNullOrEmpty(fields[j].NDefaultValue))
                    {
                        dr[j] = dbfDt.Rows[i][fields[j].OName];
                    }
                    else
                    {
                        dr[j] = fields[j].NDefaultValue;
                    }
                }
                excelDataTable.Rows.Add(dr);
            }

            // 4. 选择导出文件路径
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.InitialDirectory = this.dbfSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.FileName = excelDataTable.TableName + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.dbfSavePath = Path.GetDirectoryName(filename);

                XLWorkbook wb = new XLWorkbook();
                // Add worksheet with data
                var worksheet = wb.Worksheets.Add(excelDataTable, "Sheet1");
                // Formattings Sheet
                worksheet.Table(0).Theme = XLTableTheme.TableStyleLight20;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.SheetView.FreezeRows(1);
                worksheet.Columns().AdjustToContents(10.0, 50.0);

                // 5. 导出Excel文件
                wb.SaveAs(filename);
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }

        private void btnExportDBF_Click(object sender, EventArgs e)
        {
            // 1. 获取已选择的表头信息
            int cnt = dbfDgv.Count;
            List<DataGridViewDBFField> fields = new List<DataGridViewDBFField>();
            for (int i = 0; i < cnt; i++)
            {
                if (dbfDgv[i].IsOutput)
                {
                    fields.Add(dbfDgv[i]);
                }
            }

            if (fields.Count == 0)
            {
                MessageBox.Show("请至少选择一个表头!");
                return;
            }

            // 2. 选择 DBF 文件导出路径
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存DBF文件";
            saveFileDialog.InitialDirectory = this.dbfSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF文件|*.dbf";
            saveFileDialog.FileName = dbfDt.TableName + "_new_" + dbfDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.dbfSavePath = Path.GetDirectoryName(filename);

                // 3. 获取DBF文件内容信息
                int rows = dbfDt.Rows.Count;
                int columns = dbfDt.Columns.Count;

                // 设置默认编码为 GB2312
                Encoding encoding = DBFUtil.GetEncoding("GB2312");
                DbfFile dbf = new DbfFile(encoding);
                dbf.Open(filename, FileMode.Create);
                foreach (DataGridViewDBFField field in fields)
                {
                    dbf.Header.AddColumn(new DbfColumn(field.NName, DbfColumn.DbfColumnType.Character, field.NLength, 0));
                }

                // 4. 封装成新的对象
                for (int i = 0; i < rows; i++)
                {
                    DbfRecord record = new DbfRecord(dbf.Header);
                    for (int j = 0; j < fields.Count; j++)
                    {
                        if (String.IsNullOrEmpty(fields[j].NDefaultValue))
                        {
                            record[fields[j].NName] = dbfDt.Rows[i][fields[j].OName].ToString();
                        }
                        else
                        {
                            record[fields[j].NName] = fields[j].NDefaultValue;
                        }
                    }

                    dbf.Write(record, true);
                }

                // 5. 导出DBF文件
                dbf.Close();
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }

        // =========>> Excel文件读取 <<==========

        private void btnChooseExcelFile_Click(object sender, EventArgs e)
        {
            this.ofdExcelFile.Title = "请选择Excel文件";
            this.ofdExcelFile.Filter = "Excel文件 (*.xlsx)|*.xlsx";
            this.ofdExcelFile.FileName = "";
            this.ofdExcelFile.FilterIndex = 1;
            this.ofdExcelFile.RestoreDirectory = true;
            if (this.ofdExcelFile.ShowDialog() == DialogResult.OK)
            {
                this.txtExcelFilePath.Text = this.ofdExcelFile.FileName;
                this.excelOpenPath = Path.GetDirectoryName(this.ofdExcelFile.FileName);
                this.excelSavePath = this.excelOpenPath;
            }
        }

        private void btnExcelReader_Click(object sender, EventArgs e)
        {
            string excelFilePath = this.txtExcelFilePath.Text;
            if (excelFilePath == "")
            {
                MessageBox.Show("请选择要读取的Excel文件");
                return;
            }

            // 启用按钮
            this.btnShowExcelContent.Enabled = true;
            this.btnExcelConvertDBF.Enabled = true;
            this.btnExcelToExcel.Enabled = true;
            this.btnExcelToDBF.Enabled = true;

            DataTable excelDataTable = ExcelUtil.readFile(excelFilePath);
            // 获取表头
            excelDgv.Clear();
            List<string> excelHeader = new List<string>();
            foreach (DataColumn dc in excelDataTable.Columns)
            {
                // 此处根据字段名称设置默认字段长度
                int length = 2;
                string columnName = dc.ColumnName.ToLower();
                // 根据字段名称设置字段默认长度
                if (columnName.Equals("ksh") || columnName.Equals("bmh") || columnName.Equals("gkbmh"))
                    length = 14;
                else if (columnName.Equals("zjh") || columnName.Equals("sfzh") || columnName.Equals("idno"))
                    length = 20;
                else if (columnName.Equals("xm") || columnName.Equals("name") || columnName.Equals("ksxm"))
                    length = 40;
                else if (columnName.Equals("yxdh"))
                    length = 3;
                else if (columnName.Equals("zyzdh"))
                    length = 2;
                else if (columnName.Equals("zydh"))
                    length = 2;

                excelDgv.Add(new DataGridViewDBFField(
                    dc.ColumnName,
                    "System.String",
                    length,
                    false
                ));
                excelHeader.Add(dc.ColumnName);
            }

            // 表格内容赋值
            excelDt = excelDataTable;

            // 关闭自动添加列
            this.dataGridView2.AutoSize = true;
            this.dataGridView2.DataSource = excelDgv;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.Refresh();
            // 设置 dataGridView 序号
            this.dataGridView2.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView2_RowPostPaint);
        }

        private void dataGridView2_RowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView2.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView2.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)this.dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = !Convert.ToBoolean(cell.Value);
                //MessageBox.Show("是否选中: " + cell.Value.ToString() + ", 字段名称: " + this.dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (cell.Value.ToString() == "True")
                {
                    this.dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                    if (this.dataGridView2.Rows[e.RowIndex].Cells["enName"].Value == null)
                    {
                        string eName = this.dataGridView2.Rows[e.RowIndex].Cells["eName"].Value.ToString();
                        if (eName.StartsWith("报名号") || eName.StartsWith("考生号") || eName.ToUpper() == "BMH" || eName.ToUpper() == "KSH" || eName.ToUpper() == "GKBMH")
                        {
                            eName = "KSH";
                            this.dataGridView2.Rows[e.RowIndex].Cells["enLength"].Value = "14";
                        }
                        else if (eName.StartsWith("证件号") || eName.StartsWith("身份证") || eName.ToUpper().StartsWith("SFZ") || eName.ToUpper().StartsWith("ZJH"))
                        {
                            eName = "ZJH";
                            this.dataGridView2.Rows[e.RowIndex].Cells["enLength"].Value = "20";
                        }
                        else if (eName.StartsWith("姓名") || eName.StartsWith("考生姓名") || eName.ToUpper() == "XM" || eName.ToUpper() == "KSXM")
                        {
                            eName = "KSXM";
                            this.dataGridView2.Rows[e.RowIndex].Cells["enLength"].Value = "30";
                        }

                        this.dataGridView2.Rows[e.RowIndex].Cells["enName"].Value = eName;
                    }
                    if (this.dataGridView2.Rows[e.RowIndex].Cells["enType"].Value == null)
                    {
                        this.dataGridView2.Rows[e.RowIndex].Cells["enType"].Value = this.dataGridView2.Rows[e.RowIndex].Cells["eType"].Value;
                    }
                    if (this.dataGridView2.Rows[e.RowIndex].Cells["enLength"].Value.ToString() == "0")
                    {
                        this.dataGridView2.Rows[e.RowIndex].Cells["enLength"].Value = this.dataGridView2.Rows[e.RowIndex].Cells["eLength"].Value;
                    }
                }
                else
                {
                    this.dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void btnShowExcelContent_Click(object sender, EventArgs e)
        {
            DataViewForm dataViewForm = new DataViewForm()
            {
                Dt = this.excelDt,
                QueryDt = this.excelDt,
                Bs = this.excelDgv
            };
            dataViewForm.ShowDialog();
        }

        private void btnExcelConvertDBF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存DBF文件";
            saveFileDialog.InitialDirectory = this.excelSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF文件|*.dbf";
            saveFileDialog.FileName = excelDt.TableName + "_" + excelDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.excelSavePath = Path.GetDirectoryName(filename);

                DBFUtil.DataTableToDBF(filename, excelDt);
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }

        private void btnAddExcelHeader_Click(object sender, EventArgs e)
        {
            var name = this.txtExcelName.Text;
            var type = this.cmbExcelType.Text;
            var length = this.txtExcelLength.Text;
            var defaultValue = this.txtExcelDefaultValue.Text;

            if (name == "")
            {
                MessageBox.Show("输出名称不能为空!");
                return;
            }

            if (type == "")
            {
                MessageBox.Show("输出类型不能为空!");
                return;
            }

            if (length == "")
            {
                MessageBox.Show("输出长度不能为空!");
                return;
            }

            DataGridViewDBFField dgv = new DataGridViewDBFField();
            dgv.IsOutput = true;
            dgv.NName = name;
            dgv.NType = type;
            dgv.NLength = int.Parse(length);
            dgv.NDefaultValue = defaultValue;
            excelDgv.Add(dgv);

            this.dataGridView2.DataSource = excelDgv;
            this.dataGridView2.Refresh();
        }

        private void btnExcelToExcel_Click(object sender, EventArgs e)
        {
            // 1. 获取已选择的表头信息
            int cnt = excelDgv.Count;
            List<DataGridViewDBFField> fields = new List<DataGridViewDBFField>();
            for (int i = 0; i < cnt; i++)
            {
                if (excelDgv[i].IsOutput)
                {
                    fields.Add(excelDgv[i]);
                }
            }

            if (fields.Count == 0)
            {
                MessageBox.Show("请至少选择一个表头!");
                return;
            }

            // 2. 获取DBF文件内容信息
            int rows = excelDt.Rows.Count;
            int columns = excelDt.Columns.Count;

            // 设置表头
            DataTable excelDataTable = new DataTable();
            excelDataTable.TableName = excelDt.TableName + "_new_" + +excelDt.Rows.Count;
            for (int i = 0; i < fields.Count; i++)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = fields[i].NName;
                col.Caption = fields[i].NName;
                col.DataType = Type.GetType(fields[i].NType);
                excelDataTable.Columns.Add(col);
            }

            // 3. 封装成新的DataTable
            for (int i = 0; i < rows; i++)
            {
                DataRow dr = excelDataTable.NewRow();
                for (int j = 0; j < fields.Count; j++)
                {
                    if (String.IsNullOrEmpty(fields[j].NDefaultValue))
                    {
                        dr[j] = excelDt.Rows[i][fields[j].OName];
                    }
                    else
                    {
                        dr[j] = fields[j].NDefaultValue;
                    }
                }
                excelDataTable.Rows.Add(dr);
            }

            // 4. 选择导出文件路径
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.InitialDirectory = this.excelSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.FileName = excelDataTable.TableName + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.excelSavePath = Path.GetDirectoryName(filename);

                XLWorkbook wb = new XLWorkbook();
                // Add worksheet with data
                var worksheet = wb.Worksheets.Add(excelDataTable, "Sheet1");
                // Formattings Sheet
                worksheet.Table(0).Theme = XLTableTheme.TableStyleLight20;
                worksheet.Row(1).Style.Font.Bold = true;
                worksheet.SheetView.FreezeRows(1);
                worksheet.Columns().AdjustToContents(10.0, 50.0);

                // 5. 导出Excel文件
                wb.SaveAs(filename);
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }

        private void btnExcelToDBF_Click(object sender, EventArgs e)
        {
            // 1. 获取已选择的表头信息
            int cnt = excelDgv.Count;
            List<DataGridViewDBFField> fields = new List<DataGridViewDBFField>();
            for (int i = 0; i < cnt; i++)
            {
                if (excelDgv[i].IsOutput)
                {
                    fields.Add(excelDgv[i]);
                }
            }

            if (fields.Count == 0)
            {
                MessageBox.Show("请至少选择一个表头!");
                return;
            }

            // 2. 选择 DBF 文件导出路径
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存DBF文件";
            saveFileDialog.InitialDirectory = this.excelSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF文件|*.dbf";
            saveFileDialog.FileName = excelDt.TableName + "_new_" + +excelDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.excelSavePath = Path.GetDirectoryName(filename);

                // 3. 获取DBF文件内容信息
                int rows = excelDt.Rows.Count;
                int columns = excelDt.Columns.Count;

                // 设置默认编码为 GB2312
                Encoding encoding = DBFUtil.GetEncoding("GB2312");
                DbfFile dbf = new DbfFile(encoding);
                dbf.Open(filename, FileMode.Create);
                foreach (DataGridViewDBFField field in fields)
                {
                    dbf.Header.AddColumn(new DbfColumn(field.NName, DbfColumn.DbfColumnType.Character, field.NLength, 0));
                }

                // 4. 封装成新的对象
                for (int i = 0; i < rows; i++)
                {
                    DbfRecord record = new DbfRecord(dbf.Header);
                    for (int j = 0; j < fields.Count; j++)
                    {
                        if (String.IsNullOrEmpty(fields[j].NDefaultValue))
                        {
                            record[fields[j].NName] = excelDt.Rows[i][fields[j].OName].ToString();
                        }
                        else
                        {
                            record[fields[j].NName] = fields[j].NDefaultValue;
                        }
                    }

                    dbf.Write(record, true);
                }

                // 5. 导出DBF文件
                dbf.Close();
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }

        /// <summary>
        /// 按键后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // 如果按下了回车键
            if (e.KeyCode == Keys.Enter)
            {
                // 调用查询按钮的事件处理函数
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            // 文本框失去焦点事件
            string name = this.txtName.Text.ToString();
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            name = name.ToUpper();
            this.txtName.Text = name;
            this.cmbType.SelectedIndex = 1;

            if (name == "YXDH")
            {

                this.txtLength.Text = "3";
                if (string.IsNullOrEmpty(this.txtDefaultValue.Text) || this.txtDefaultValue.Text.Length != 3)
                {
                    this.txtDefaultValue.Text = "";
                }
            }
            else if (name == "ZYZDH")
            {
                this.txtLength.Text = "2";
                if (string.IsNullOrEmpty(this.txtDefaultValue.Text) || this.txtDefaultValue.Text.Length > 2)
                {
                    this.txtDefaultValue.Text = "01";
                }
            }
            else if (name == "ZYDH" || name == "ZYDH_CH")
            {
                this.txtLength.Text = "16";
                if (string.IsNullOrEmpty(this.txtDefaultValue.Text) || this.txtDefaultValue.Text.Length > 2)
                {
                    this.txtDefaultValue.Text = "01";
                }
            }
        }

        private void txtExcelName_Leave(object sender, EventArgs e)
        {
            // 文本框失去焦点事件
            string name = this.txtExcelName.Text.ToString();
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            name = name.ToUpper();
            this.txtExcelName.Text = name;
            this.cmbExcelType.SelectedIndex = 1;

            if (name == "YXDH")
            {
                this.txtExcelLength.Text = "3";
                if (string.IsNullOrEmpty(this.txtExcelDefaultValue.Text) || this.txtExcelDefaultValue.Text.Length != 3)
                {
                    this.txtExcelDefaultValue.Text = "";
                }
            }
            else if (name == "ZYZDH")
            {
                this.txtExcelLength.Text = "2";
                if (string.IsNullOrEmpty(this.txtExcelDefaultValue.Text) || this.txtExcelDefaultValue.Text.Length > 2)
                {
                    this.txtExcelDefaultValue.Text = "01";
                }
            }
            else if (name == "ZYDH" || name == "ZYDH_CH")
            {
                this.txtExcelLength.Text = "16";
                if (string.IsNullOrEmpty(this.txtExcelDefaultValue.Text) || this.txtExcelDefaultValue.Text.Length > 2)
                {
                    this.txtExcelDefaultValue.Text = "01";
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 4)
            {
                tabPage5.Text = "介绍";
                tabPage5.Controls.Clear();
                string introducePath = @"Resources\md\Introduce.md";
                // 解决中文乱码问题
                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string mdHtml = Markdown.ToHtml(File.ReadAllText(introducePath, Encoding.GetEncoding("GB2312")));
                WebBrowser webBrowser = new WebBrowser();
                webBrowser.Dock = DockStyle.Fill;
                webBrowser.DocumentText = mdHtml;
                tabPage5.Controls.Add(webBrowser);
            }
        }

        private void btnBatchSplitDBFHeader_Click(object sender, EventArgs e)
        {
            DBFTableHeadSplitForm tableHeadSplitForm = new DBFTableHeadSplitForm();
            tableHeadSplitForm.ShowDialog();
        }

        private void txtDBFFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtDBFFilePath_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            txtDBFFilePath.Text = path;
        }

        private void txtExcelFilePath_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void txtExcelFilePath_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            txtExcelFilePath.Text = path;
        }
    }
}