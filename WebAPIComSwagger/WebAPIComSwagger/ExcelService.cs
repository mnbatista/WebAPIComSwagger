
using System.Data;
using System.IO;
using ClosedXML.Excel;

namespace WebAPIComSwagger
{
    public class ExcelService : IExcelService
    {
        #region events and methods

        public byte[] ExportToExcel(DataTable table)
        {

            var workbook = new XLWorkbook();
            var m = new MemoryStream();

            workbook.Worksheets.Add(table);
            workbook.SaveAs(m);

            return m.ToArray();
        }

        #endregion
    }
    public interface IExcelService
    {
        byte[] ExportToExcel(DataTable table);
    }
}