using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WF.DBF.Utils
{
    internal class DataTableHelper<T> where T : class
    {
        public static List<T> DataTableConvertToModel(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            List<T> modelList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T model = Activator.CreateInstance(typeof(T)) as T;
                for (int i = 0; i < dr.Table.Columns.Count; i ++)
                {
                    PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                    {
                        propertyInfo.SetValue(model, Convert.ChangeType(dr[i], propertyInfo.PropertyType), null);
                    }
                }
                modelList.Add(model);
            }
            return modelList;
        }
    }
}
