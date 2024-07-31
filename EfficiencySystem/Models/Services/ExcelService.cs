using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace EfficiencySystem.Models.Services
{
    public class ExcelService
    {
        public byte[] CreateExcelFile(List<string[]> tableHeaders, List<object[]> tableData)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                //Add worksheet
                excel.Workbook.Worksheets.Add("sheet");
                var WSheet = excel.Workbook.Worksheets["sheet"];
                WSheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                //set headers range
                var HeaderRange = "A1:" + Char.ConvertFromUtf32(tableHeaders[0].Length + 64) + "1";

                //Add headers
                WSheet.Cells[HeaderRange].LoadFromArrays(tableHeaders);
                //Set headers style
                WSheet.Cells[HeaderRange].Style.Font.Bold = true;
                WSheet.Cells[HeaderRange].Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                //Add data
                WSheet.Cells[2, 1].LoadFromArrays(tableData);
                WSheet.Cells.AutoFitColumns();

                return excel.GetAsByteArray();
            }
        }
    }

    public static class ExcelServiceProviderExtensions
    {
        public static void AddExcelService(this IServiceCollection services)
        {
            services.AddTransient<ExcelService>();
        }
    }
}
