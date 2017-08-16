using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSBoxing.Core.YS;
using System.IO;
using OfficeOpenXml;

namespace YSBoxing.Services.YS
{
    public class YsOrderServices:IYsOrderServices
    {
        public List<InputYsOrder> GetInputYsOrderByExcel(Stream stream) {
            using (ExcelPackage package = new ExcelPackage(stream))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                
            }

            return null;
        }
    }
}
