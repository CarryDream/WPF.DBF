using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WF.DBF.Utils
{
    internal class ExcelUtil
    {

        public static DataTable readFile(string excelFilePath)
        {
            return ReadExcelFile(excelFilePath, null, default);
        }

        /// <summary>
        /// 异步读取Excel文件
        /// </summary>
        /// <param name="excelFilePath">文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>DataTable对象</returns>
        public static async Task<DataTable> ReadFileAsync(string excelFilePath, IProgress<(int current, int total, string message)> progress = null, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => ReadExcelFile(excelFilePath, progress, cancellationToken), cancellationToken);
        }

        /// <summary>
        /// 读取Excel文件到DataTable
        /// </summary>
        private static DataTable ReadExcelFile(string excelFilePath, IProgress<(int current, int total, string message)> progress = null, CancellationToken cancellationToken = default)
        {
            progress?.Report((0, 100, "正在打开文件..."));

            DataTable dt = new DataTable();
            dt.TableName = System.IO.Path.GetFileNameWithoutExtension(excelFilePath);

            using (FileStream fileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                progress?.Report((10, 100, "正在加载Excel文件..."));

                // 使用文件流打开Excel文件，而不是直接使用文件路径
                using (XLWorkbook wb = new XLWorkbook(fileStream))
                {
                    // 获取第一张表的数据
                    var sheet = wb.Worksheets.First();

                    progress?.Report((20, 100, "正在读取表结构..."));

                    // 预读取行数
                    var allRows = sheet.RowsUsed().ToList();
                    int totalRows = allRows.Count;

                    // 检查是否取消
                    cancellationToken.ThrowIfCancellationRequested();

                    // 预分配内存以提高性能
                    dt.BeginLoadData();

                    try
                    {
                        // 处理第一行（表头）
                        if (totalRows > 0)
                        {
                            var headerRow = allRows[0];
                            var headerCells = headerRow.CellsUsed().ToList();

                            foreach (var cell in headerCells)
                            {
                                DataColumn col = new DataColumn();
                                col.ColumnName = cell.Value.ToString();
                                col.Caption = cell.Value.ToString();
                                dt.Columns.Add(col);

                                // 检查是否取消
                                cancellationToken.ThrowIfCancellationRequested();
                            }

                            progress?.Report((30, 100, "正在读取数据..."));

                            // 处理数据行
                            int batchSize = 1000; // 批量处理大小

                            for (int i = 1; i < totalRows; i += batchSize)
                            {
                                int endBatch = Math.Min(i + batchSize, totalRows);

                                for (int rowIndex = i; rowIndex < endBatch; rowIndex++)
                                {
                                    var row = allRows[rowIndex];
                                    var cells = row.CellsUsed().ToList();

                                    DataRow dr = dt.NewRow();
                                    foreach (var cell in cells)
                                    {
                                        int columnIndex = cell.Address.ColumnNumber - 1;
                                        if (columnIndex < dt.Columns.Count)
                                        {
                                            dr[columnIndex] = cell.Value;
                                        }
                                    }
                                    dt.Rows.Add(dr);
                                }

                                // 报告进度
                                if (progress != null)
                                {
                                    int progressValue = 30 + (int)((float)(endBatch - 1) / (totalRows - 1) * 70);
                                    progress.Report((progressValue, 100, $"正在读取数据... {endBatch - 1}/{totalRows - 1}"));
                                }

                                // 检查是否取消
                                cancellationToken.ThrowIfCancellationRequested();
                            }
                        }
                    }
                    finally
                    {
                        dt.EndLoadData();
                    }

                    progress?.Report((100, 100, "完成"));
                }

                return dt;
            }
        }

    }
}
