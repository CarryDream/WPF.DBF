using ClosedXML.Excel;
using SocialExplorer.IO.FastDBF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.DBF.Entity;
using WF.DBF.Utils;

namespace WF.DBF
{
    public partial class DataViewForm : Form
    {
        // 读取到的 datatable 文件内容
        private DataTable dt;
        // 选择显示的标题内容
        private BindingList<DataGridViewDBFField> bs = new BindingList<DataGridViewDBFField>();
        // 获取已选择的表头信息
        private List<DataGridViewDBFField> selectedFields = new List<DataGridViewDBFField>();
        // 当前筛选出来的结果
        private DataTable queryDt;
        // 文件保存默认路径
        private string defaultSavePath = string.Empty;

        public DataTable Dt { get => dt; set => dt = value; }
        internal BindingList<DataGridViewDBFField> Bs { get => bs; set => bs = value; }
        public DataTable QueryDt { get => queryDt; set => queryDt = value; }
        public string DefaultSavePath { get => defaultSavePath; set => defaultSavePath = value; }

        public DataViewForm()
        {
            InitializeComponent();
        }

        private void DataViewForm_Load(object sender, EventArgs e)
        {
            // 关闭自动添加列
            // this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSize = true;
            this.dataGridView1.DataSource = Dt;
            // 字段太多的话, 不建议设置自动填满
            // this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Refresh();
            // this.dataGridView1.EndEdit();
            // 设置 dataGridView 序号
            this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);

            // 显示统计信息
            this.labelStatics.Text = "统计信息：共 " + Dt.Rows.Count + " 条记录，" + Dt.Columns.Count + " 列。";

            this.comboBox1.Items.Clear();
            this.comboBox1.Items.Add("请选择列");
            foreach (DataColumn dc in Dt.Columns)
            {
                this.comboBox1.Items.Add(dc.ColumnName);
            }

            comboBox1.SelectedIndex = 0;

            // 获取已选择的表头信息
            this.selectedFields.Clear();
            int cnt = bs.Count;
            for (int i = 0; i < cnt; i++)
            {
                if (bs[i].IsOutput)
                {
                    this.selectedFields.Add(bs[i]);
                }
            }
        }

        private void dataGridView1_RowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor);
            e.Graphics.DrawString((e.RowIndex + 1).ToString(System.Globalization.CultureInfo.CurrentUICulture), this.dataGridView1.DefaultCellStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedIndex > 0)
            {
                string columnName = this.comboBox1.Text;
                string filter = this.textBox1.Text;
                Dt.DefaultView.RowFilter = columnName + " like '%" + filter + "%'";
            }

            QueryDt = Dt.DefaultView.ToTable();
            QueryDt.TableName = Dt.TableName;
            this.dataGridView1.DataSource = QueryDt;
            this.dataGridView1.Refresh();
            // 显示统计信息
            this.labelStatics.Text = "统计信息：共 " + QueryDt.Rows.Count + " 条记录，" + QueryDt.Columns.Count + " 列。";
        }

        private void btnConvertExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.InitialDirectory = this.defaultSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.FileName = QueryDt.TableName + "_" + QueryDt.Rows.Count + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.defaultSavePath = Path.GetDirectoryName(filename);

                XLWorkbook wb = new XLWorkbook();
                // Add worksheet with data
                var worksheet = wb.Worksheets.Add(QueryDt, "Sheet1");
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

        private void btnConvertDBF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存DBF文件";
            saveFileDialog.InitialDirectory = this.defaultSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF文件|*.dbf";
            saveFileDialog.FileName = QueryDt.TableName + "_" + QueryDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.defaultSavePath = Path.GetDirectoryName(filename);

                DBFUtil.DataTableToDBF(filename, QueryDt);
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (this.selectedFields.Count == 0)
            {
                MessageBox.Show("请至少选择一个表头!");
                return;
            }

            // 2. 获取DBF文件内容信息
            int rows = QueryDt.Rows.Count;
            int columns = QueryDt.Columns.Count;

            // 设置表头
            DataTable excelDt = new DataTable();
            excelDt.TableName = QueryDt.TableName + "_new_" + rows;
            for (int i = 0; i < this.selectedFields.Count; i++)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = this.selectedFields[i].NName;
                col.Caption = this.selectedFields[i].NName;
                col.DataType = Type.GetType(this.selectedFields[i].NType);
                excelDt.Columns.Add(col);
            }

            // 3. 封装成新的DataTable
            for (int i = 0; i < rows; i++)
            {
                DataRow dr = excelDt.NewRow();
                for (int j = 0; j < this.selectedFields.Count; j++)
                {
                    if (String.IsNullOrEmpty(this.selectedFields[j].NDefaultValue))
                    {
                        dr[j] = QueryDt.Rows[i][this.selectedFields[j].OName];
                    }
                    else
                    {
                        dr[j] = this.selectedFields[j].NDefaultValue;
                    }
                }
                excelDt.Rows.Add(dr);
            }

            // 4. 选择导出文件路径
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存Excel文件";
            saveFileDialog.InitialDirectory = this.defaultSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel文件|*.xlsx";
            saveFileDialog.FileName = excelDt.TableName + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.defaultSavePath = Path.GetDirectoryName(filename);

                XLWorkbook wb = new XLWorkbook();
                // Add worksheet with data
                var worksheet = wb.Worksheets.Add(excelDt, "Sheet1");
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
            if (this.selectedFields.Count == 0)
            {
                MessageBox.Show("请至少选择一个表头!");
                return;
            }

            // 2. 选择 DBF 文件导出路径
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "保存DBF文件";
            saveFileDialog.InitialDirectory = this.defaultSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF文件|*.dbf";
            saveFileDialog.FileName = QueryDt.TableName + "_new_" + QueryDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.defaultSavePath = Path.GetDirectoryName(filename);

                // 3. 获取DBF文件内容信息
                int rows = QueryDt.Rows.Count;
                int columns = QueryDt.Columns.Count;

                // 设置默认编码为 GB2312
                Encoding encoding = DBFUtil.GetEncoding("GB2312");
                DbfFile dbf = new DbfFile(encoding);
                dbf.Open(filename, FileMode.Create);
                foreach (DataGridViewDBFField field in this.selectedFields)
                {
                    dbf.Header.AddColumn(new DbfColumn(field.NName, DbfColumn.DbfColumnType.Character, field.NLength, 0));
                }

                // 4. 封装成新的对象
                for (int i = 0; i < rows; i++)
                {
                    DbfRecord record = new DbfRecord(dbf.Header);
                    for (int j = 0; j < this.selectedFields.Count; j++)
                    {
                        if (String.IsNullOrEmpty(this.selectedFields[j].NDefaultValue))
                        {
                            record[this.selectedFields[j].NName] = QueryDt.Rows[i][this.selectedFields[j].OName].ToString();
                        }
                        else
                        {
                            record[this.selectedFields[j].NName] = this.selectedFields[j].NDefaultValue;
                        }
                    }

                    dbf.Write(record, true);
                }

                // 5. 导出DBF文件
                dbf.Close();
                MessageBox.Show("文件保存成功! 路径: " + filename);
            }
        }
    }

}
