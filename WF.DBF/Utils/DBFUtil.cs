using SocialExplorer.IO.FastDBF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WF.DBF.Utils
{
    #region TDbfTable更新历史
    //-----------------------------------------------------------------------------------------------
    // 1) 2013-06-10: (1) 自 ReadDbf.cs 移植过来
    //                (2) 读记录时, 首先读删除标志字节, 然后读记录. 原代码这个地方忽略了删除标志字节.
    //                (3) 原代码 MoveNext() 后没有再判是否文件尾, 在读修改结构后的表时报错误.
    // 2) 2013-06-11: (1) int.money.decimal.double 直接用 BitConverter 转换函数
    //                (2) Date类型转换时, 如果字符串为空, 则为1899-12-30日.
    //                (3) Time类型转换时, 如果天数和毫秒数全为0, 则为1899-12-30日.
    //                (4) Time类型转换时, 毫秒数存在误差, 有1000以下的整数数, 需要+1秒.
    //                (5) 读记录值时,  在定位首指针后, 顺序读每个记录数组.
    // 3) 2013-06-17  (1) 打开文件后需要关闭文件流, 增加一个 CloseFileStream() 方法
    // 4) 2013-06-18  (1) 把 CloseFileStream() 放到 try{}finally{} 结构中
    //-----------------------------------------------------------------------------------------------------
    #endregion

    internal class DBFUtil
    {

        #region 获取文件编码
        public static System.Text.Encoding GetDBFFileEncoding(string dbfFilePath)
        {
            System.IO.FileStream fs = new System.IO.FileStream(dbfFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            return GetDBFFileEncoding(fs);
        }

        public static System.Text.Encoding GetDBFFileEncoding(System.IO.FileStream fs)
        {
            string encoding = "GB2312";
            byte[] buffer = new byte[1];
            fs.Seek(29, System.IO.SeekOrigin.Begin);
            fs.Read(buffer, 0, 1);
            if (buffer[0] == 0x43)
            {
                encoding = "ASCII";
            }
            else if (buffer[0] == 0x45)
            {
                encoding = "GBK";
            }
            else if (buffer[0] == 0x46)
            {
                encoding = "GB2312";
            }
            else if (buffer[0] == 0x4e)
            {
                encoding = "BIG5";
            }
            else if (buffer[0] == 0x55)
            {
                encoding = "UTF-8";
            }
            return GetEncoding(encoding);
        }

        public static System.Text.Encoding GetEncoding(string encodingName)
        {
            if (string.IsNullOrEmpty(encodingName) == true)
            {
                return System.Text.Encoding.Default;
            }

            if (encodingName.ToUpper() == "GB2312")
            {
                // 注册字符集。注意：.net framework 框架中支持 GB2312 编码，.net core 框架中默认不支持 GB2312 编码
                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                return System.Text.Encoding.GetEncoding("GB2312");
            }

            if (encodingName.ToUpper() == "UNICODE")
            {
                return System.Text.Encoding.Unicode;
            }

            if (encodingName.ToUpper() == "UTF8")
            {
                return System.Text.Encoding.UTF8;
            }

            if (encodingName.ToUpper() == "UTF7")
            {
                return System.Text.Encoding.UTF7;
            }

            if (encodingName.ToUpper() == "UTF32")
            {
                return System.Text.Encoding.UTF32;
            }

            if (encodingName.ToUpper() == "ASCII")
            {
                return System.Text.Encoding.ASCII;
            }

            return System.Text.Encoding.Default;
        }
        #endregion

        /// <summary>
        /// 从DBF读取文件到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataTable DBFToDataTable(string fileName) {

            // 获取文件编码
            Encoding encoding = GetDBFFileEncoding(fileName);
            // 返回的结果集
            DataTable dt = new DataTable();
            // 获取 dbf 文件对象
            DbfFile dbf = new DbfFile(encoding);
            dbf.Open(fileName, FileMode.Open);

            // 创建 DataTable 的列
            DbfHeader dh = dbf.Header;
            for (int index = 0; index < dh.ColumnCount; index++)
            {
                dt.Columns.Add(dh[index].Name);
            }

            return ReadDbfRecords(dbf, dt);
        }

        /// <summary>
        /// 异步从DBF读取文件到DataTable
        /// </summary>
        /// <param name="fileName">DBF文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>DataTable对象</returns>
        public static async Task<DataTable> DBFToDataTableAsync(string fileName, IProgress<(int current, int total, string message)> progress = null, CancellationToken cancellationToken = default)
        {
            // 使用FileShare.ReadWrite允许其他进程读取文件
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                return await Task.Run(() => {
                    try
                    {
                        // 获取文件编码
                        Encoding encoding = GetDBFFileEncoding(fileName);
                        // 返回的结果集
                        DataTable dt = new DataTable();
                        dt.TableName = Path.GetFileNameWithoutExtension(fileName);

                        progress?.Report((0, 100, "正在打开文件..."));

                        // 获取 dbf 文件对象
                        DbfFile dbf = new DbfFile(encoding);
                        // 使用文件流打开DBF文件
                        dbf.Open(fs);

                        progress?.Report((10, 100, "正在读取表结构..."));

                        // 创建 DataTable 的列
                        DbfHeader dh = dbf.Header;
                        for (int index = 0; index < dh.ColumnCount; index++)
                        {
                            dt.Columns.Add(dh[index].Name);

                            // 检查是否取消
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                        progress?.Report((20, 100, "正在读取数据..."));

                        return ReadDbfRecords(dbf, dt, progress, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        // 记录错误信息
                        System.Diagnostics.Debug.WriteLine($"DBF读取错误: {ex.Message}");
                        throw; // 重新抛出异常
                    }
                }, cancellationToken);
            }
        }

        /// <summary>
        /// 读取DBF记录到DataTable
        /// </summary>
        private static DataTable ReadDbfRecords(DbfFile dbf, DataTable dt, IProgress<(int current, int total, string message)> progress = null, CancellationToken cancellationToken = default)
        {

            try
            {
                // 预读取总记录数
                int totalRecords = 0;
                while (dbf.Read(totalRecords) != null) {
                    totalRecords++;
                    // 每100条记录检查一次是否取消
                    if (totalRecords % 100 == 0 && cancellationToken != default)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }

                // 重新读取数据
                // 加载数据到 DataTable 里
                int i = 0;
                int batchSize = 1000; // 批量处理大小

                // 预分配内存以提高性能
                dt.BeginLoadData();

                while (i < totalRecords) {
                    // 批量处理数据
                    int endBatch = Math.Min(i + batchSize, totalRecords);

                    for (int recordIndex = i; recordIndex < endBatch; recordIndex++)
                    {
                        // 获取记录
                        DbfRecord record = dbf.Read(recordIndex);
                        if (record == null) continue;

                        // 将该行数据放到 DataRow 里
                        DataRow dr = dt.NewRow();
                        Object[] objs = new object[record.ColumnCount];
                        for (int index = 0; index < record.ColumnCount; index++)
                        {
                            objs[index] = record[index];
                        }

                        dr.ItemArray = objs;
                        dt.Rows.Add(dr);
                    }

                    i = endBatch;

                    // 报告进度
                    if (progress != null)
                    {
                        int progressValue = 20 + (int)((float)i / totalRecords * 80);
                        progress.Report((progressValue, 100, $"正在读取数据... {i}/{totalRecords}"));
                    }

                    // 检查是否取消
                    if (cancellationToken != default)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }

                dt.EndLoadData();
            }
            finally
            {
                dbf.Close();
            }
            return dt;
        }

        public static void DataTableToDBF(string initFile, string fileName, DataTable dt) {
            // 设置默认编码为 GB2312
            Encoding encoding = GetEncoding("GB2312");
            // 获取一个 DBF 文件对象
            DbfFile dbf = new DbfFile(encoding);
            dbf.Open(initFile, FileMode.OpenOrCreate);
            DbfHeader initHeader = dbf.Header;

            // 如果文件存在, 需要删除文件
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            dbf.Open(fileName, FileMode.OpenOrCreate);
            // 创建 DBF 文件的结构
            dbf.Header.Unlock();
            for (int i = 0; i < initHeader.ColumnCount; i++)
            {
                DbfColumn initC = initHeader[i];
                dbf.Header.AddColumn(initC);
            }

            // 读取 DataTable 写入到 DBF 文件中
            foreach(DataRow dr in dt.Rows)
            {
                // 将 DataRow 里的数据封装到 DbfRecord 里面
                DbfRecord record = new DbfRecord(dbf.Header);
                foreach(DataColumn dc in dt.Columns)
                {
                    try
                    {
                        record[dc.ColumnName] = dr[dc.ColumnName].ToString();
                    } catch
                    {
                        continue;
                    }
                }

                dbf.Write(record, true);
            }

            // 一定要 Close 才会把数据完全写入到 DBF 文件中
            dbf.Close();

        }

        public static void DataTableToDBF(string fileName, DataTable dt)
        {
            DataTableToDBF(fileName, dt, null, default);
        }

        /// <summary>
        /// 异步将DataTable导出为DBF文件
        /// </summary>
        /// <param name="fileName">输出文件路径</param>
        /// <param name="dt">数据表</param>
        /// <param name="progress">进度回调</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public static async Task DataTableToDBFAsync(string fileName, DataTable dt, IProgress<(int current, int total, string message)> progress = null, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => DataTableToDBF(fileName, dt, progress, cancellationToken), cancellationToken);
        }

        /// <summary>
        /// 将DataTable导出为DBF文件
        /// </summary>
        private static void DataTableToDBF(string fileName, DataTable dt, IProgress<(int current, int total, string message)> progress = null, CancellationToken cancellationToken = default)
        {
            progress?.Report((0, 100, "准备导出文件..."));

            // 确保文件句柄正确释放
            DbfFile dbf = null;

            try
            {
                // 如果文件存在, 需要删除文件
                if (File.Exists(fileName))
                {
                    // 尝试删除文件，如果被占用则等待一会再试
                    int retryCount = 0;
                    bool deleted = false;

                    while (!deleted && retryCount < 3)
                    {
                        try
                        {
                            File.Delete(fileName);
                            deleted = true;
                        }
                        catch (IOException)
                        {
                            retryCount++;
                            if (retryCount >= 3) throw; // 重试多次后还是失败，抛出异常

                            // 等待一会再试
                            Thread.Sleep(100);
                        }
                    }
                }

                progress?.Report((10, 100, "创建文件结构..."));

                // 设置默认编码为 GB2312
                Encoding encoding = GetEncoding("GB2312");
                // 获取一个 DBF 文件对象
                dbf = new DbfFile(encoding);

                // 使用FileShare.None确保文件不被其他进程访问
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                {
                    dbf.Open(fs);

                    // 创建 DBF 文件的结构
                    foreach (DataColumn dc in dt.Columns) {
                        dbf.Header.AddColumn(new DbfColumn(dc.ColumnName, DbfColumn.DbfColumnType.Character, 255, 0));

                        // 检查是否取消
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    progress?.Report((20, 100, "写入数据..."));

                    // 读取 DataTable 写入到 DBF 文件中
                    int totalRows = dt.Rows.Count;
                    int batchSize = 1000; // 批量处理大小

                    for (int i = 0; i < totalRows; i += batchSize)
                    {
                        int endBatch = Math.Min(i + batchSize, totalRows);

                        for (int rowIndex = i; rowIndex < endBatch; rowIndex++)
                        {
                            DataRow dr = dt.Rows[rowIndex];
                            // 将 DataRow 里的数据封装到 DbfRecord 里面
                            DbfRecord record = new DbfRecord(dbf.Header);
                            foreach (DataColumn dc in dt.Columns)
                            {
                                try
                                {
                                    record[dc.ColumnName] = dr[dc.ColumnName].ToString();
                                }
                                catch
                                {
                                    continue;
                                }
                            }

                            dbf.Write(record, true);
                        }

                        // 报告进度
                        if (progress != null)
                        {
                            int progressValue = 20 + (int)((float)endBatch / totalRows * 80);
                            progress.Report((progressValue, 100, $"写入数据... {endBatch}/{totalRows}"));
                        }

                        // 检查是否取消
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    progress?.Report((100, 100, "完成"));

                    // 在关闭文件流前先关闭DBF文件
                    dbf.Close();
                }
            }
            catch (OperationCanceledException)
            {
                // 如果操作被取消，删除可能创建的不完整文件
                if (File.Exists(fileName))
                {
                    try { File.Delete(fileName); } catch { }
                }
                throw; // 重新抛出异常
            }
            catch (Exception ex)
            {
                // 记录错误信息
                System.Diagnostics.Debug.WriteLine($"DBF写入错误: {ex.Message}");

                // 如果出错，删除可能创建的不完整文件
                if (File.Exists(fileName))
                {
                    try { File.Delete(fileName); } catch { }
                }

                throw; // 重新抛出异常
            }
            finally
            {
                // 确保关闭DBF文件
                if (dbf != null)
                {
                    try { dbf.Close(); } catch { }
                }
            }

        }

        /// <summary>
        /// 将数据表写入到DBF文件中
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="fileName">文件名称</param>
        static void WriteToDbf(DataTable dt, string fileName)
        {
            System.Diagnostics.Debug.WriteLine("Writing to: " + fileName + " ...");

            //连接字符串
            string sConn =
                "Provider=Microsoft.Jet.OLEDB.4.0; " +
                "Data Source=" + System.IO.Directory.GetCurrentDirectory() + "; " +
                "Extended Properties=dBASE IV;";
            OleDbConnection conn = new OleDbConnection(sConn);
            conn.Open();

            try
            {
                //如果存在同名文件则先删除
                if (File.Exists(fileName))
                {
                    System.Diagnostics.Debug.WriteLine("Delete file: " + fileName + " ...");
                    File.Delete(fileName);
                }

                OleDbCommand cmd;

                //建立新表
                StringBuilder sbCreate = new StringBuilder();
                sbCreate.Append("CREATE TABLE " + fileName + " (");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sbCreate.Append(dt.Columns[i].ColumnName);
                    sbCreate.Append(" char(25)");
                    if (i != dt.Columns.Count - 1)
                    {
                        sbCreate.Append(", ");
                    }
                    else
                    {
                        sbCreate.Append(')');
                    }
                }

                System.Diagnostics.Debug.WriteLine("\nCreating Table ...");
                System.Diagnostics.Debug.WriteLine(sbCreate.ToString());
                cmd = new OleDbCommand(sbCreate.ToString(), conn);
                cmd.ExecuteNonQuery();

                //插入各行
                StringBuilder sbInsert = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    sbInsert.Clear();
                    sbInsert.Append("INSERT INTO " + fileName + " (");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sbInsert.Append(dt.Columns[i].ColumnName);
                        if (i != dt.Columns.Count - 1)
                        {
                            sbInsert.Append(", ");
                        }
                    }
                    sbInsert.Append(") VALUES (");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sbInsert.Append("'" + dr[i].ToString() + "'");
                        if (i != dt.Columns.Count - 1)
                        {
                            sbInsert.Append(", ");
                        }
                    }
                    sbInsert.Append(')');

                    System.Diagnostics.Debug.WriteLine("\nInserting lines ...");
                    System.Diagnostics.Debug.WriteLine(sbInsert.ToString());
                    cmd = new OleDbCommand(sbInsert.ToString(), conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            conn.Close();
        }

        #region DBF 文件头
        public class TDbfHeader
        {
            public const int HeaderSize = 32;
            public sbyte Version;
            public byte LastModifyYear;
            public byte LastModifyMonth;
            public byte LastModifyDay;
            public int RecordCount;
            public ushort HeaderLength;
            public ushort RecordLength;
            public byte[] Reserved = new byte[16];
            public sbyte TableFlag;
            public sbyte CodePageFlag;
            public byte[] Reserved2 = new byte[2];
        }
        #endregion

        #region DBF 字段
        public class TDbfField
        {
            public const int FieldSize = 32;
            public byte[] NameBytes = new byte[11];  // 字段名称
            public byte TypeChar;
            public byte Length;
            public byte Precision;
            public byte[] Reserved = new byte[2];
            public sbyte DbaseivID;
            public byte[] Reserved2 = new byte[10];
            public sbyte ProductionIndex;

            public bool IsString
            {
                get
                {
                    if (TypeChar == 'C')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsMoney
            {
                get
                {
                    if (TypeChar == 'Y')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsNumber
            {
                get
                {
                    if (TypeChar == 'N')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsFloat
            {
                get
                {
                    if (TypeChar == 'F')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsDate
            {
                get
                {
                    if (TypeChar == 'D')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsTime
            {
                get
                {
                    if (TypeChar == 'T')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsDouble
            {
                get
                {
                    if (TypeChar == 'B')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsInt
            {
                get
                {
                    if (TypeChar == 'I')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsLogic
            {
                get
                {
                    if (TypeChar == 'L')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsMemo
            {
                get
                {
                    if (TypeChar == 'M')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public bool IsGeneral
            {
                get
                {
                    if (TypeChar == 'G')
                    {
                        return true;
                    }

                    return false;
                }
            }

            public Type FieldType
            {
                get
                {
                    if (this.IsString == true)
                    {
                        return typeof(string);
                    }
                    else if (this.IsMoney == true || this.IsNumber == true || this.IsFloat == true)
                    {
                        return typeof(decimal);
                    }
                    else if (this.IsDate == true || this.IsTime == true)
                    {
                        return typeof(System.DateTime);
                    }
                    else if (this.IsDouble == true)
                    {
                        return typeof(double);
                    }
                    else if (this.IsInt == true)
                    {
                        return typeof(System.Int32);
                    }
                    else if (this.IsLogic == true)
                    {
                        return typeof(bool);
                    }
                    else if (this.IsMemo == true)
                    {
                        return typeof(string);
                    }
                    else if (this.IsMemo == true)
                    {
                        return typeof(string);
                    }
                    else
                    {
                        return typeof(string);
                    }
                }
            }

            public string GetFieldName()
            {
                return GetFieldName(System.Text.Encoding.Default);
            }

            public string GetFieldName(System.Text.Encoding encoding)
            {
                string fieldName = encoding.GetString(NameBytes);
                int i = fieldName.IndexOf('\0');
                if (i > 0)
                {
                    return fieldName.Substring(0, i).Trim();
                }

                return fieldName.Trim();
            }
        }
        #endregion

        #region DBF 记录
        public class TDbfTable : IDisposable
        {
            private const byte DeletedFlag = 0x2A;
            // odbc中空日期对应的转换日期
            private DateTime NullDateTime = new DateTime(1899, 12, 30);

            private string _dbfFileName = null;

            private System.Text.Encoding _encoding = System.Text.Encoding.Default;
            private System.IO.FileStream _fileStream = null;
            private System.IO.BinaryReader _binaryReader = null;

            private bool _isFileOpened;
            private byte[] _recordBuffer;
            private int _fieldCount = 0;

            private TDbfHeader _dbfHeader = null;
            private TDbfField[] _dbfFields;
            private System.Data.DataTable _dbfTable = null;

            public TDbfTable(string fileName)
            {
                this._dbfFileName = fileName.Trim();
                try
                {
                    // 默认编码格式
                    // System.Diagnostics.Debug.WriteLine("文件编码：" + GetDBFFileEncoding(fileName));
                    this._encoding = GetDBFFileEncoding(fileName);
                    this.OpenDbfFile();
                }
                finally
                {
                    this.CloseFileStream();
                }
            }

            public TDbfTable(string fileName, string encodingName)
            {
                this._dbfFileName = fileName.Trim();
                this._encoding = GetEncoding(encodingName);
                try
                {
                    this.OpenDbfFile();
                }
                finally
                {
                    this.CloseFileStream();
                }
            }

            void System.IDisposable.Dispose()
            {
                this.Dispose(true);  // TODO:  添加 DBFFile.System.IDisposable.Dispose 实现
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing == true)
                {
                    this.Close();
                }
            }

            public void Close()
            {
                this.CloseFileStream();

                _recordBuffer = null;
                _dbfHeader = null;
                _dbfFields = null;

                _isFileOpened = false;
                _fieldCount = 0;
            }

            private void CloseFileStream()
            {
                if (_fileStream != null)
                {
                    _fileStream.Close();
                    _fileStream = null;
                }

                if (_binaryReader != null)
                {
                    _binaryReader.Close();
                    _binaryReader = null;
                }
            }

            private void OpenDbfFile()
            {
                this.Close();

                if (string.IsNullOrEmpty(_dbfFileName) == true)
                {
                    throw new Exception("filename is empty or null.");
                }

                if (System.IO.File.Exists(_dbfFileName) == false)
                {
                    throw new Exception(this._dbfFileName + " does not exist.");
                }

                try
                {
                    this.GetFileStream();
                    this.ReadHeader();
                    this.ReadFields();
                    this.GetRecordBufferBytes();
                    this.CreateDbfTable();
                    this.GetDbfRecords();
                }
                catch (Exception e)
                {
                    this.Close();
                    throw e;
                }
            }

            public void GetFileStream()
            {
                try
                {
                    this._fileStream = File.Open(this._dbfFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    this._binaryReader = new BinaryReader(this._fileStream, _encoding);
                    this._isFileOpened = true;
                }
                catch
                {
                    throw new Exception("fail to read  " + this._dbfFileName + ".");
                }
            }

            private void ReadHeader()
            {
                this._dbfHeader = new TDbfHeader();

                try
                {
                    this._dbfHeader.Version = this._binaryReader.ReadSByte();
                    this._dbfHeader.LastModifyYear = this._binaryReader.ReadByte();
                    this._dbfHeader.LastModifyMonth = this._binaryReader.ReadByte();
                    this._dbfHeader.LastModifyDay = this._binaryReader.ReadByte();
                    this._dbfHeader.RecordCount = this._binaryReader.ReadInt32();
                    this._dbfHeader.HeaderLength = this._binaryReader.ReadUInt16();
                    this._dbfHeader.RecordLength = this._binaryReader.ReadUInt16();
                    this._dbfHeader.Reserved = this._binaryReader.ReadBytes(16);
                    this._dbfHeader.TableFlag = this._binaryReader.ReadSByte();
                    this._dbfHeader.CodePageFlag = this._binaryReader.ReadSByte();
                    this._dbfHeader.Reserved2 = this._binaryReader.ReadBytes(2);

                    this._fieldCount = GetFieldCount();

                    System.Diagnostics.Debug.WriteLine($"RecordLength: {this._dbfHeader.RecordLength}");
                    System.Diagnostics.Debug.WriteLine($"HeaderLength: {this._dbfHeader.HeaderLength}");
                    System.Diagnostics.Debug.WriteLine($"RecordCount: {this._dbfHeader.RecordCount}");
                    System.Diagnostics.Debug.WriteLine($"FieldCount: {this._fieldCount}");
                }
                catch
                {
                    throw new Exception("fail to read file header.");
                }
            }

            private int GetFieldCount()
            {
                // 由于有些dbf文件的文件头最后有附加区段，但是有些文件没有，在此使用笨方法计算字段数目
                // 就是测试每一个存储字段结构区域的第一个字节的值，如果不为0x0D，表示存在一个字段
                // 否则从此处开始不再存在字段信息
                int fCount = (this._dbfHeader.HeaderLength - TDbfHeader.HeaderSize - 1) / TDbfField.FieldSize;

                int actualCount = 0;
                for (int k = 0; k < fCount; k++)
                {
                    _fileStream.Seek(TDbfHeader.HeaderSize + k * TDbfField.FieldSize, SeekOrigin.Begin);
                    byte flag = this._binaryReader.ReadByte();

                    if (flag != 0x0D)  // 如果不是0x0D，表示字段存在
                    {
                        actualCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                System.Diagnostics.Debug.WriteLine($"Actual FieldCount: {actualCount}");

                return actualCount;
            }

            private void ReadFields()
            {
                _dbfFields = new TDbfField[_fieldCount];

                try
                {
                    _fileStream.Seek(TDbfHeader.HeaderSize, SeekOrigin.Begin);
                    for (int k = 0; k < _fieldCount; k++)
                    {
                        this._dbfFields[k] = new TDbfField();
                        this._dbfFields[k].NameBytes = this._binaryReader.ReadBytes(11);
                        this._dbfFields[k].TypeChar = this._binaryReader.ReadByte();

                        this._binaryReader.ReadBytes(4);  // 保留, 源代码是读 UInt32()给 Offset

                        this._dbfFields[k].Length = this._binaryReader.ReadByte();
                        this._dbfFields[k].Precision = this._binaryReader.ReadByte();
                        this._dbfFields[k].Reserved = this._binaryReader.ReadBytes(2);
                        this._dbfFields[k].DbaseivID = this._binaryReader.ReadSByte();
                        this._dbfFields[k].Reserved2 = this._binaryReader.ReadBytes(10);
                        this._dbfFields[k].ProductionIndex = this._binaryReader.ReadSByte();
                    }
                }
                catch
                {
                    throw new Exception("fail to read field information.");
                }
            }

            private void GetRecordBufferBytes()
            {
                this._recordBuffer = new byte[this._dbfHeader.RecordLength];

                System.Diagnostics.Debug.WriteLine($"Allocating RecordBuffer of length: {this._dbfHeader.RecordLength}");

                if (this._recordBuffer == null)
                {
                    throw new Exception("fail to allocate memory.");
                }
            }

            private void CreateDbfTable()
            {
                if (_dbfTable != null)
                {
                    _dbfTable.Clear();
                    _dbfTable = null;
                }

                _dbfTable = new System.Data.DataTable();
                _dbfTable.TableName = this.TableName;

                for (int k = 0; k < this._fieldCount; k++)
                {
                    System.Data.DataColumn col = new System.Data.DataColumn();
                    string colText = this._dbfFields[k].GetFieldName(_encoding);

                    if (string.IsNullOrEmpty(colText) == true)
                    {
                        throw new Exception("the " + (k + 1) + "th column name is null.");
                    }

                    col.ColumnName = colText;
                    col.Caption = colText;
                    col.DataType = this._dbfFields[k].FieldType;
                    // col.MaxLength = this._dbfFields[k].Length;
                    _dbfTable.Columns.Add(col);
                    // System.Diagnostics.Debug.WriteLine($"字段名：{colText}, 字段类型：{col.DataType}, 字段长度：{this._dbfFields[k].Length}");
                }
            }

            public void GetDbfRecords()
            {
                try
    {
        this._fileStream.Seek(this._dbfHeader.HeaderLength, SeekOrigin.Begin);

        for (int k = 0; k < this.RecordCount; k++)
        {
            if (ReadRecordBuffer(k) != DeletedFlag)
            {
                System.Data.DataRow row = _dbfTable.NewRow();
                for (int i = 0; i < this._fieldCount; i++)
                {
                    row[i] = this.GetFieldValue(i);
                }
                _dbfTable.Rows.Add(row);
            }
        }
    }
    catch (ArgumentOutOfRangeException e)
    {
        throw new Exception("参数超出范围。", e);
    }
    catch (Exception ex)
    {
        throw new Exception("获取 DBF 表时失败。", ex);
    }
            }

            private byte ReadRecordBuffer(int recordIndex)
            {
                try
                {
                    byte deleteFlag = this._binaryReader.ReadByte();  // 删除标志
                    int bytesToRead = this._dbfHeader.RecordLength - 1;

                    System.Diagnostics.Debug.WriteLine($"读取记录 {recordIndex}，需要读取的字节数：{bytesToRead}");

                    if (bytesToRead <= 0)
                    {
                        throw new Exception($"记录长度无效（RecordLength: {this._dbfHeader.RecordLength}），无法读取数据。");
                    }

                    this._recordBuffer = this._binaryReader.ReadBytes(bytesToRead);  // 读取记录数据

                    if (this._recordBuffer.Length != bytesToRead)
                    {
                        throw new Exception($"实际读取字节数与预期不符：预期 {bytesToRead}，实际 {this._recordBuffer.Length}。");
                    }

                    return deleteFlag;
                }
                catch (Exception ex)
                {
                    throw new Exception($"读取记录缓冲区时发生错误：{ex.Message}", ex);
                }
            }

            private string GetFieldValue(int fieldIndex)
            {
                string fieldValue = null;

                int offset = 0;
                for (int i = 0; i < fieldIndex; i++)
                {
                    offset += _dbfFields[i].Length;
                }

                // 确保 offset 和 Length 合法
                if (offset < 0 || offset + _dbfFields[fieldIndex].Length > this._recordBuffer.Length)
                {
                    throw new ArgumentOutOfRangeException($"Field index {fieldIndex} has invalid offset or length.");
                }

                byte[] tmp = CopySubBytes(this._recordBuffer, offset, this._dbfFields[fieldIndex].Length);


                if (this._dbfFields[fieldIndex].IsInt == true)
                {
                    int val = System.BitConverter.ToInt32(tmp, 0);
                    fieldValue = val.ToString();
                }
                else if (this._dbfFields[fieldIndex].IsDouble == true)
                {
                    double val = System.BitConverter.ToDouble(tmp, 0);
                    fieldValue = val.ToString();
                }
                else if (this._dbfFields[fieldIndex].IsMoney == true)
                {
                    long val = System.BitConverter.ToInt64(tmp, 0);  // 将字段值放大10000倍，变成long型存储，然后缩小10000倍。
                    fieldValue = ((decimal)val / 10000).ToString();
                }
                else if (this._dbfFields[fieldIndex].IsDate == true)
                {
                    DateTime date = ToDate(tmp);
                    fieldValue = date.ToString();

                }
                else if (this._dbfFields[fieldIndex].IsTime == true)
                {
                    DateTime time = ToTime(tmp);
                    fieldValue = time.ToString();

                }
                else
                {
                   //  System.Diagnostics.Debug.WriteLine($"编码：{this._encoding}, 字段名：{this._dbfFields[fieldIndex].GetFieldName(_encoding)}, 字段类型：{this._dbfFields[fieldIndex].FieldType}, 字段长度：{this._dbfFields[fieldIndex].Length}, 字段值：{Convert.ToString(tmp)}");
                    fieldValue = this._encoding.GetString(tmp);
                }

                fieldValue = fieldValue.Trim();

                // 如果本字段类型是数值相关型，进一步处理字段值
                if (this._dbfFields[fieldIndex].IsNumber == true || this._dbfFields[fieldIndex].IsFloat == true)    // N - 数值型, F - 浮点型
                {
                    if (fieldValue.Length == 0)
                    {
                        fieldValue = "0";
                    }
                    else if (fieldValue == ".")
                    {
                        fieldValue = "0";
                    }
                    else
                    {
                        decimal val = 0;

                        if (decimal.TryParse(fieldValue, out val) == false)  // 将字段值先转化为Decimal类型然后再转化为字符串型，消除类似“.000”的内容, 如果不能转化则为0
                        {
                            val = 0;
                        }

                        fieldValue = val.ToString();
                    }
                }
                else if (this._dbfFields[fieldIndex].IsLogic == true)    // L - 逻辑型
                {
                    if (fieldValue != "T" && fieldValue != "Y")
                    {
                        fieldValue = "false";
                    }
                    else
                    {
                        fieldValue = "true";
                    }
                }
                else if (this._dbfFields[fieldIndex].IsDate == true || this._dbfFields[fieldIndex].IsTime == true)   // D - 日期型  T - 日期时间型
                {
                    // 暂时不做任何处理
                }

                return fieldValue;
            }

            private static byte[] CopySubBytes(byte[] buf, int startIndex, long length)
            {
                if (startIndex >= buf.Length)
                {
                    throw new ArgumentOutOfRangeException("startIndex");
                }

                if (length == 0)
                {
                    throw new ArgumentOutOfRangeException("length", "length must be great than 0.");
                }

                if (length > buf.Length - startIndex)
                {
                    length = buf.Length - startIndex;  // 子数组的长度超过从startIndex起到buf末尾的长度时，修正为剩余长度
                }

                byte[] target = new byte[length];
                Array.Copy(buf, startIndex, target, 0, length);
                return target;
            }

            private DateTime ToDate(byte[] buf)
            {
                if (buf.Length != 8)
                {
                    throw new ArgumentException("date array length must be 8.", "buf");
                }

                string dateStr = System.Text.Encoding.ASCII.GetString(buf).Trim();
                if (dateStr.Length < 8)
                {
                    return NullDateTime;
                }

                int year = int.Parse(dateStr.Substring(0, 4));
                int month = int.Parse(dateStr.Substring(4, 2));
                int day = int.Parse(dateStr.Substring(6, 2));

                return new DateTime(year, month, day);
            }

            private DateTime ToTime(byte[] buf)
            {
                if (buf.Length != 8)
                {
                    throw new ArgumentException("time array length must be 8.", "buf");
                }

                try
                {
                    byte[] tmp = CopySubBytes(buf, 0, 4);
                    tmp.Initialize();
                    int days = System.BitConverter.ToInt32(tmp, 0);  // ( ToInt32(tmp); // 获取天数

                    tmp = CopySubBytes(buf, 4, 4);  // 获取毫秒数
                    int milliSeconds = System.BitConverter.ToInt32(tmp, 0);  // ToInt32(tmp);

                    if (days == 0 && milliSeconds == 0)
                    {
                        return NullDateTime;
                    }

                    int seconds = milliSeconds / 1000;
                    int milli = milliSeconds % 1000;  // vfp实际上没有毫秒级, 是秒转换来的, 测试时发现2秒钟转换为1999毫秒的情况
                    if (milli > 0)
                    {
                        seconds += 1;
                    }

                    DateTime date = DateTime.MinValue;  // 在最小日期时间的基础上添加刚获取的天数和秒数，得到日期字段数值
                    date = date.AddDays(days - 1721426);
                    date = date.AddSeconds(seconds);

                    return date;
                }
                catch
                {
                    return new DateTime();
                }
            }

            public string TableName
            {
                get { return System.IO.Path.GetFileNameWithoutExtension(this._dbfFileName); }
            }

            public System.Text.Encoding Encoding
            {
                get { return this._encoding; }
            }

            public int RecordLength
            {
                get
                {
                    if (this.IsFileOpened == false)
                    {
                        return 0;
                    }

                    return this._dbfHeader.RecordLength;
                }
            }

            public int FieldCount
            {
                get
                {
                    if (this.IsFileOpened == false)
                    {
                        return 0;
                    }

                    return this._dbfFields.Length;
                }
            }

            public int RecordCount
            {
                get
                {
                    if (this.IsFileOpened == false || this._dbfHeader == null)
                    {
                        return 0;
                    }

                    return this._dbfHeader.RecordCount;
                }
            }

            public bool IsFileOpened
            {
                get
                {
                    return this._isFileOpened;
                }
            }

            public System.Data.DataTable Table
            {
                get
                {
                    if (_isFileOpened == false)
                    {
                        return null;
                    }
                    return _dbfTable;
                }
            }

            public TDbfField[] DbfFields
            {
                get
                {
                    if (_isFileOpened == false)
                    {
                        return null;
                    }

                    return _dbfFields;
                }
            }
        }
        #endregion
    }
}
