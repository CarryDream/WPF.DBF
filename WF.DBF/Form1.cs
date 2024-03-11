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
        // DBF ��ȡ��������
        private DataTable dbfDt = null;
        // DBF ѡ��ı�ͷ
        private BindingList<DataGridViewDBFField> dbfDgv = new BindingList<DataGridViewDBFField>();
        // DBF �ļ���Ŀ¼
        private string dbfOpenPath = string.Empty;
        // DBF �ļ�����Ŀ¼
        private string dbfSavePath = string.Empty;

        // Excel ��ȡ��������
        private DataTable excelDt = null;
        // Excel ѡ��ı�ͷ
        private BindingList<DataGridViewDBFField> excelDgv = new BindingList<DataGridViewDBFField>();
        // Excel �ļ���Ŀ¼
        private string excelOpenPath = string.Empty;
        // Excel �ļ�����Ŀ¼
        private string excelSavePath = string.Empty;


        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �رմ���ǰִ�е��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("ȷʵҪ�˳���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ======> ��ȡ DBF
            // ���ð�ť
            this.btnShowContent.Enabled = false;
            this.btnConvertExcel.Enabled = false;
            this.btnExportExcel.Enabled = false;
            this.btnExportDBF.Enabled = false;

            this.cmbType.Items.Clear();
            this.cmbType.Items.Insert(0, "��ѡ���������");
            this.cmbType.Items.Add("System.String");
            this.cmbType.SelectedIndex = 0;

            // ======> ��ȡ Excel
            this.btnShowExcelContent.Enabled = false;
            this.btnExcelConvertDBF.Enabled = false;
            this.btnExcelToExcel.Enabled = false;
            this.btnExcelToDBF.Enabled = false;

            this.cmbExcelType.Items.Clear();
            this.cmbExcelType.Items.Insert(0, "��ѡ���������");
            this.cmbExcelType.Items.Add("System.String");
            this.cmbExcelType.SelectedIndex = 0;

        }

        // =========>> DBF �ļ���ȡ <<==========

        /// <summary>
        /// ѡ�� DBF �ļ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChooseDBFFile_Click(object sender, EventArgs e)
        {
            this.ofdDBFFile.Title = "��ѡ��DBF�ļ�";
            this.ofdDBFFile.Filter = "DBF�ļ� (*.dbf)|*.dbf";
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
        /// ��ȡ DBF �ļ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDBFReader_Click(object sender, EventArgs e)
        {
            string dbfFilePath = this.txtDBFFilePath.Text;
            if (dbfFilePath == "")
            {
                MessageBox.Show("��ѡ��Ҫ��ȡ��DBF�ļ�");
                return;
            }

            DBFUtil.TDbfTable dbfTable = new DBFUtil.TDbfTable(dbfFilePath);
            // ������ݸ�ֵ
            dbfDt = dbfTable.Table;
            // ���ð�ť
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
            // DataGridView ���ݿ���Զ�����
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.Refresh();
            // ���� DataGridView ���
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
                        if (oName.StartsWith("������") || oName.StartsWith("������") || oName.ToUpper() == "BMH" || oName.ToUpper() == "KSH" || oName.ToUpper() == "GKBMH")
                        {
                            oName = "KSH";
                            this.dataGridView1.Rows[e.RowIndex].Cells["nLength"].Value = "14";
                        }
                        else if (oName.StartsWith("֤����") || oName.StartsWith("���֤") || oName.ToUpper().StartsWith("SFZ") || oName.ToUpper().StartsWith("ZJH"))
                        {
                            oName = "ZJH";
                            this.dataGridView1.Rows[e.RowIndex].Cells["nLength"].Value = "20";
                        }
                        else if (oName.StartsWith("����") || oName.StartsWith("��������") || oName.ToUpper() == "XM" || oName.ToUpper() == "KSXM")
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
            saveFileDialog.Title = "����Excel�ļ�";
            saveFileDialog.InitialDirectory = this.dbfSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel�ļ�|*.xlsx";
            saveFileDialog.FileName = dbfDt.TableName + "_" + dbfDt.Rows.Count + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                // ��ֵ����DBF�ļ�·��
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
                MessageBox.Show("�ļ�����ɹ�! ·��: " + filename);
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
                MessageBox.Show("������Ʋ���Ϊ��!");
                return;
            }

            if (type == "")
            {
                MessageBox.Show("������Ͳ���Ϊ��!");
                return;
            }

            if (length == "")
            {
                MessageBox.Show("������Ȳ���Ϊ��!");
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
            // 1. ��ȡ��ѡ��ı�ͷ��Ϣ
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
                MessageBox.Show("������ѡ��һ����ͷ!");
                return;
            }

            // 2. ��ȡDBF�ļ�������Ϣ
            int rows = dbfDt.Rows.Count;
            int columns = dbfDt.Columns.Count;

            // ���ñ�ͷ
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

            // 3. ��װ���µ�DataTable
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

            // 4. ѡ�񵼳��ļ�·��
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "����Excel�ļ�";
            saveFileDialog.InitialDirectory = this.dbfSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel�ļ�|*.xlsx";
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

                // 5. ����Excel�ļ�
                wb.SaveAs(filename);
                MessageBox.Show("�ļ�����ɹ�! ·��: " + filename);
            }
        }

        private void btnExportDBF_Click(object sender, EventArgs e)
        {
            // 1. ��ȡ��ѡ��ı�ͷ��Ϣ
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
                MessageBox.Show("������ѡ��һ����ͷ!");
                return;
            }

            // 2. ѡ�� DBF �ļ�����·��
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "����DBF�ļ�";
            saveFileDialog.InitialDirectory = this.dbfSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF�ļ�|*.dbf";
            saveFileDialog.FileName = dbfDt.TableName + "_new_" + dbfDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.dbfSavePath = Path.GetDirectoryName(filename);

                // 3. ��ȡDBF�ļ�������Ϣ
                int rows = dbfDt.Rows.Count;
                int columns = dbfDt.Columns.Count;

                // ����Ĭ�ϱ���Ϊ GB2312
                Encoding encoding = DBFUtil.GetEncoding("GB2312");
                DbfFile dbf = new DbfFile(encoding);
                dbf.Open(filename, FileMode.Create);
                foreach (DataGridViewDBFField field in fields)
                {
                    dbf.Header.AddColumn(new DbfColumn(field.NName, DbfColumn.DbfColumnType.Character, field.NLength, 0));
                }

                // 4. ��װ���µĶ���
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

                // 5. ����DBF�ļ�
                dbf.Close();
                MessageBox.Show("�ļ�����ɹ�! ·��: " + filename);
            }
        }

        // =========>> Excel�ļ���ȡ <<==========

        private void btnChooseExcelFile_Click(object sender, EventArgs e)
        {
            this.ofdExcelFile.Title = "��ѡ��Excel�ļ�";
            this.ofdExcelFile.Filter = "Excel�ļ� (*.xlsx)|*.xlsx";
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
                MessageBox.Show("��ѡ��Ҫ��ȡ��Excel�ļ�");
                return;
            }

            // ���ð�ť
            this.btnShowExcelContent.Enabled = true;
            this.btnExcelConvertDBF.Enabled = true;
            this.btnExcelToExcel.Enabled = true;
            this.btnExcelToDBF.Enabled = true;

            DataTable excelDataTable = ExcelUtil.readFile(excelFilePath);
            // ��ȡ��ͷ
            excelDgv.Clear();
            List<string> excelHeader = new List<string>();
            foreach (DataColumn dc in excelDataTable.Columns)
            {
                // �˴������ֶ���������Ĭ���ֶγ���
                int length = 2;
                string columnName = dc.ColumnName.ToLower();
                // �����ֶ����������ֶ�Ĭ�ϳ���
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

            // ������ݸ�ֵ
            excelDt = excelDataTable;

            // �ر��Զ������
            this.dataGridView2.AutoSize = true;
            this.dataGridView2.DataSource = excelDgv;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.Refresh();
            // ���� dataGridView ���
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
                //MessageBox.Show("�Ƿ�ѡ��: " + cell.Value.ToString() + ", �ֶ�����: " + this.dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (cell.Value.ToString() == "True")
                {
                    this.dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                    if (this.dataGridView2.Rows[e.RowIndex].Cells["enName"].Value == null)
                    {
                        string eName = this.dataGridView2.Rows[e.RowIndex].Cells["eName"].Value.ToString();
                        if (eName.StartsWith("������") || eName.StartsWith("������") || eName.ToUpper() == "BMH" || eName.ToUpper() == "KSH" || eName.ToUpper() == "GKBMH")
                        {
                            eName = "KSH";
                            this.dataGridView2.Rows[e.RowIndex].Cells["enLength"].Value = "14";
                        }
                        else if (eName.StartsWith("֤����") || eName.StartsWith("���֤") || eName.ToUpper().StartsWith("SFZ") || eName.ToUpper().StartsWith("ZJH"))
                        {
                            eName = "ZJH";
                            this.dataGridView2.Rows[e.RowIndex].Cells["enLength"].Value = "20";
                        }
                        else if (eName.StartsWith("����") || eName.StartsWith("��������") || eName.ToUpper() == "XM" || eName.ToUpper() == "KSXM")
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
            saveFileDialog.Title = "����DBF�ļ�";
            saveFileDialog.InitialDirectory = this.excelSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF�ļ�|*.dbf";
            saveFileDialog.FileName = excelDt.TableName + "_" + excelDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.excelSavePath = Path.GetDirectoryName(filename);

                DBFUtil.DataTableToDBF(filename, excelDt);
                MessageBox.Show("�ļ�����ɹ�! ·��: " + filename);
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
                MessageBox.Show("������Ʋ���Ϊ��!");
                return;
            }

            if (type == "")
            {
                MessageBox.Show("������Ͳ���Ϊ��!");
                return;
            }

            if (length == "")
            {
                MessageBox.Show("������Ȳ���Ϊ��!");
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
            // 1. ��ȡ��ѡ��ı�ͷ��Ϣ
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
                MessageBox.Show("������ѡ��һ����ͷ!");
                return;
            }

            // 2. ��ȡDBF�ļ�������Ϣ
            int rows = excelDt.Rows.Count;
            int columns = excelDt.Columns.Count;

            // ���ñ�ͷ
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

            // 3. ��װ���µ�DataTable
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

            // 4. ѡ�񵼳��ļ�·��
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "����Excel�ļ�";
            saveFileDialog.InitialDirectory = this.excelSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel�ļ�|*.xlsx";
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

                // 5. ����Excel�ļ�
                wb.SaveAs(filename);
                MessageBox.Show("�ļ�����ɹ�! ·��: " + filename);
            }
        }

        private void btnExcelToDBF_Click(object sender, EventArgs e)
        {
            // 1. ��ȡ��ѡ��ı�ͷ��Ϣ
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
                MessageBox.Show("������ѡ��һ����ͷ!");
                return;
            }

            // 2. ѡ�� DBF �ļ�����·��
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "����DBF�ļ�";
            saveFileDialog.InitialDirectory = this.excelSavePath;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = "dbf";
            saveFileDialog.Filter = "DBF�ļ�|*.dbf";
            saveFileDialog.FileName = excelDt.TableName + "_new_" + +excelDt.Rows.Count + ".dbf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = saveFileDialog.FileName;
                this.excelSavePath = Path.GetDirectoryName(filename);

                // 3. ��ȡDBF�ļ�������Ϣ
                int rows = excelDt.Rows.Count;
                int columns = excelDt.Columns.Count;

                // ����Ĭ�ϱ���Ϊ GB2312
                Encoding encoding = DBFUtil.GetEncoding("GB2312");
                DbfFile dbf = new DbfFile(encoding);
                dbf.Open(filename, FileMode.Create);
                foreach (DataGridViewDBFField field in fields)
                {
                    dbf.Header.AddColumn(new DbfColumn(field.NName, DbfColumn.DbfColumnType.Character, field.NLength, 0));
                }

                // 4. ��װ���µĶ���
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

                // 5. ����DBF�ļ�
                dbf.Close();
                MessageBox.Show("�ļ�����ɹ�! ·��: " + filename);
            }
        }

        /// <summary>
        /// �����󴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // ��������˻س���
            if (e.KeyCode == Keys.Enter)
            {
                // ���ò�ѯ��ť���¼�������
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            // �ı���ʧȥ�����¼�
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
            // �ı���ʧȥ�����¼�
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
                tabPage5.Text = "����";
                tabPage5.Controls.Clear();
                string introducePath = @"Resources\md\Introduce.md";
                // ���������������
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