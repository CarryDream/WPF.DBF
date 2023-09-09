using SocialExplorer.IO.FastDBF;
using System.Text;
using WF.DBF.Utils;
using static WF.DBF.Utils.DBFUtil;

namespace WF.DBF
{
    public partial class DBFTableHeadSplitForm : Form
    {
        public DBFTableHeadSplitForm()
        {
            InitializeComponent();
        }

        private void DBFTableHeadSplitForm_Load(object sender, EventArgs e)
        {

            // 初始化显示信息
            this.txtDBFTitle.Text = "ZYZDM";
            this.txtDBFLength.Text = "5";
            this.txtDestField1Name.Text = "YXDH";
            this.txtDestField1Length.Text = "3";
            this.txtDestField2Name.Text = "ZYZDH";
            this.txtDestField2Length.Text = "2";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fbdReadPath.Description = "请选择读取DBF文件的路径";
            if (this.fbdReadPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(fbdReadPath.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.txtReadFilePath.Text = fbdReadPath.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.fbdDestPath.Description = "请选择输出DBF文件的路径";
            if (this.fbdDestPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(fbdDestPath.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.txtDestFilePath.Text = fbdDestPath.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string statusText = "状态：";
            string readFilePath = this.txtReadFilePath.Text;
            string destFilePath = this.txtDestFilePath.Text;
            // 1. 读取读取文件目录下, 所有的DBF文件, 遍历子目录
            List<string> dbfFiles = listDirectory(readFilePath, null);
            statusText += "共读取到: " + dbfFiles.Count + " 个DBF文件\n";
            this.txtStatus.Text = statusText;

            // 2. 输出文件路径, 如果不存在创建新的目录
            if (Directory.Exists(destFilePath))
            {
                deleteDirectory(destFilePath);
            }
            if (!Directory.Exists(destFilePath))
            {
                Directory.CreateDirectory(destFilePath);
            }

            // 3. 遍历所有DBF文件, 对表头字段进行拆分, 写入到新的文件 
            Encoding encoding = DBFUtil.GetEncoding("GB2312");
            foreach (string df in dbfFiles)
            {
                // 获取 DBF 文件对象
                DBFUtil.TDbfTable dbfTable = new DBFUtil.TDbfTable(df);
                // 读取到的文件内容总条数
                int rows = dbfTable.Table.Rows.Count;
                // 输出文件路径
                string fName = df.Substring(df.LastIndexOf("\\") + 1); ;  // 获取文件名
                string suffixPath = df.Replace(readFilePath, "").Replace(fName, "");   // 获取子目录

                string extName = df.Substring(df.LastIndexOf(".") + 1); // 获取文件扩展名称
                string newFileName = dbfTable.TableName + "_new_" + rows + "." + extName;    // 新的文件名称
                if (!Directory.Exists(destFilePath + suffixPath))
                {
                    Directory.CreateDirectory(destFilePath + suffixPath);
                }
                string destFile = destFilePath + suffixPath + newFileName;
                Path.GetDirectoryName(destFile);
                DbfFile destDbf = new DbfFile(encoding);
                destDbf.Open(destFile, FileMode.Create);

                // 记录需要拆分的表头字段索引
                int splitIndex = -1;
                string splitName = string.Empty;
                TDbfField[] dbfFields = dbfTable.DbfFields;
                // 设置表头
                for (int i = 0; i < dbfFields.Length; i++)
                {
                    string fieldName = dbfFields[i].GetFieldName(dbfTable.Encoding);
                    if (fieldName.ToUpper() == this.txtDBFTitle.Text)
                    {
                        splitIndex = i;
                        splitName = fieldName;
                        destDbf.Header.AddColumn(new DbfColumn(this.txtDestField1Name.Text, DbfColumn.DbfColumnType.Character, int.Parse(this.txtDestField1Length.Text), 0));
                        destDbf.Header.AddColumn(new DbfColumn(this.txtDestField2Name.Text, DbfColumn.DbfColumnType.Character, int.Parse(this.txtDestField2Length.Text), 0));
                    }
                    else
                    {
                        destDbf.Header.AddColumn(new DbfColumn(fieldName, DbfColumn.DbfColumnType.Character, dbfFields[i].Length, 0));
                    }
                }

                for (int i = 0; i < rows; i++)
                {
                    // 输出内容
                    DbfRecord record = new DbfRecord(destDbf.Header);
                    // 读取内容
                    for (int j = 0; j < dbfFields.Length; j++)
                    {
                        if (j == splitIndex)
                        {
                            string value = dbfTable.Table.Rows[i][splitName].ToString();
                            if (value.Length >= int.Parse(this.txtDBFLength.Text))
                            {
                                record[this.txtDestField1Name.Text] = value.Substring(0, int.Parse(this.txtDestField1Length.Text));
                                record[this.txtDestField2Name.Text] = value.Substring(int.Parse(this.txtDestField1Length.Text), int.Parse(this.txtDestField2Length.Text));
                            }
                            else
                            {
                                record[this.txtDestField1Name.Text] = value;
                                record[this.txtDestField2Name.Text] = string.Empty;
                            }
                        }
                        else
                        {
                            string fieldName = dbfFields[j].GetFieldName(dbfTable.Encoding);
                            record[fieldName] = dbfTable.Table.Rows[i][fieldName].ToString();
                        }
                    }
                    destDbf.Write(record);
                }

                // 导出 DBF 文件
                destDbf.Close();

            }
        }


        private List<string> listDirectory(string path, List<string> files)
        {
            if (files == null)
            {
                files = new List<string>();
            }

            if (Directory.Exists(path))
            {
                foreach (var item in Directory.GetFiles(path))
                {
                    if (File.Exists(item))
                    {
                        if (item.ToUpper().EndsWith(".DBF"))
                            files.Add(item);
                    }
                    else
                    {
                        if (Directory.Exists(item))
                        {
                            listDirectory(item, files);
                        }
                    }
                }

                foreach (var item in Directory.GetDirectories(path))
                {
                    if (File.Exists(item))
                    {
                        if (item.ToUpper().EndsWith(".DBF"))
                            files.Add(item);
                    }
                    else
                    {
                        if (Directory.Exists(item))
                        {
                            listDirectory(item, files);
                        }
                    }
                }
            }

            return files;
        }


        public static void deleteDirectory(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

        }
    }
}
