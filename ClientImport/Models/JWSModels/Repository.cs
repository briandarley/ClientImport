using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace ClientImport.Models.JWSModels
{
    public class Repository
    {
        public IEnumerable<Record> Records { get; set; }

        public void WriteRecordsToExcelFile(string path)
        {
            var newFile = new FileInfo(path);
            if (newFile.Exists)
            {
                newFile.Delete();
            }
            var pck = new ExcelPackage(newFile);
            //Add the Content sheet
            var ws = pck.Workbook.Worksheets.Add("Content");
          
            var t = typeof (Record);
            var propertyInfos = t.GetProperties().Where(c => c.GetCustomAttribute(typeof(ColumnAttribute)) != null).Select(c => c).ToList();
            var columnAttributes = t.GetProperties().Where(c => c.GetCustomAttribute(typeof (ColumnAttribute)) != null).Select(c=>c.GetCustomAttribute< ColumnAttribute>()).ToList();
            var columnIndex = 65;

            var mapping = new Dictionary<string, string>();
            //set column header
            foreach (var columnAttribute in columnAttributes)
            {
                var colName = Convert.ToString((char) columnIndex);
                if (columnIndex > 90)
                {
                    colName = Convert.ToString((char) (columnIndex - 26));
                    colName = "A" + colName;
                }
                ws.Cells[$"{colName}1"].Value = columnAttribute.Name;

                mapping.Add(columnAttribute.Name,colName);
                columnIndex++;
            }
            var row = 2;

            if (Records!= null)
            {
                foreach (var record in Records)
                {
                    foreach (var propertyInfo in propertyInfos)
                    {
                        var colAttribute = propertyInfo.GetCustomAttribute<ColumnAttribute>();
                        var colMap = mapping[colAttribute.Name];
                        var colValue = propertyInfo.GetValue(record, BindingFlags.Public, null, null, null);

                        if (propertyInfo.PropertyType == typeof (DateTime))
                        {
                            ws.Cells[$"{colMap}{row}"].Style.Numberformat.Format = "yyyy-mm-dd";
                        }

                        ws.Cells[$"{colMap}{row}"].Value = colValue;




                    }
                    row++;
                }
            }

            pck.Save();
        }
    }
}
