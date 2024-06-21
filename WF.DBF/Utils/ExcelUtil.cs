using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF.DBF.Utils
{
    internal class ExcelUtil
    {

        public static DataTable readFile(string excelFilePath)
        {
            DataTable dt = new DataTable();
            dt.TableName = System.IO.Path.GetFileNameWithoutExtension(excelFilePath);
            using (FileStream fileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (XLWorkbook wb = new XLWorkbook(excelFilePath))
                {
                    // 获取第一张表的数据
                    var sheet = wb.Worksheets.First();
                    // 获取已经被使用的行, 跳过第一个标题行
                    // var rows = sheet.RowsUsed().Skip(1);
                    var rows = sheet.RowsUsed();
                    foreach (var row in rows)
                    {
                        // 获取已经被使用的列
                        var cells = row.CellsUsed();
                        // 如果是第一行, 则为表头
                        if (row.RowNumber() == 1)
                        {
                            foreach (var cell in cells)
                            {
                                DataColumn col = new DataColumn();
                                col.ColumnName = cell.Value.ToString();
                                col.Caption = cell.Value.ToString();
                                dt.Columns.Add(col);
                            }
                        }
                        else
                        {
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
                    }
                }

                return dt;
            }
        }

    }
}
