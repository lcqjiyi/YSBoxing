using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using System.Reflection;

namespace YSBoxing.Core.Helper
{
    public class ExcelToModel<TModel>where TModel:new ()
    {
        public Stream Stream;
        public Dictionary<PropertyInfo, int> Headers = new Dictionary<PropertyInfo, int>();
        public ExcelToModel(Stream stream   ){

            Stream = stream;
        }
        /// <summary>
        /// 验证表头
        /// </summary>
        /// <returns></returns>
        public string VerificationHeader() {
            string error = "";
            using (ExcelPackage excel = new ExcelPackage(Stream))
            {

                ExcelWorksheet worksheet = excel.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;

                Type TModelType = typeof(TModel);

                foreach (PropertyInfo propertyInfo in TModelType.GetTypeInfo().GetProperties())
                {
                    var keywordsAttr = propertyInfo.GetCustomAttribute<KeywordsAttribute>();
                    if (keywordsAttr != null)
                    {
                        bool Find = false;
                        foreach (var item in  keywordsAttr.Keywords.Split(','))
                        {
                            if (Find) break;
                            for (int i = 1; i < ColCount+1; i++)
                            {
                                string value = worksheet.Cells[1, i].Value.ToString();
                                if (value == item && keywordsAttr.AllCells)//全个单元格匹配
                                {
                                    Headers.Add(propertyInfo, i);
                                    Find = true;
                                    break;
                                }
                                if (value.Contains( item) && !keywordsAttr.AllCells)//单元格包含
                                {
                                    Headers.Add(propertyInfo, i);
                                    Find = true;
                                    break;
                                }
                            }
                        }
                        if (Find = false && keywordsAttr.Required) error += "错误没有找到" + keywordsAttr.Keywords + "/n";
                        
                    }
                } 

            }
            return error;
        }

        public List<TModel> GetModelList() {
            List<TModel> Models = new List<TModel>();
            using (ExcelPackage excel = new ExcelPackage(Stream))
            {
                ExcelWorksheet worksheet = excel.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;
                for (int i = 2; i < rowCount+1; i++)
                {
                    TModel model = new TModel();
                    foreach (var Header in Headers)
                    {
                        var cell = worksheet.Cells[i, Header.Value];
                        Type PropertyType = Header.Key.PropertyType;
                        if (PropertyType.Name == typeof(int).Name) {
                            
                            Header.Key.SetValue(model,Convert.ToInt32(cell.Value));
                        }
                        else
                        {
                            Header.Key.SetValue(model,cell.Value.ToString());
                        }
                    }
                    Models.Add(model);
                }
            }
            return Models;
        }
    }
}
