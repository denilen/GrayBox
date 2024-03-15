using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace EPPlusDemo;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        using (var package = new ExcelPackage(@"1.xlsx"))
        {
            var sheet = package.Workbook.Worksheets.Add("1");
            sheet.Cells["A1"].Value = "Hello World!";

            // Save to file
            package.Save();
        }

        using (var package = new ExcelPackage(@"2.xlsx"))
        {
            var worksheet = package.Workbook.Worksheets.Add("2");

            worksheet.Cells["B1"].Value = "This is cell B1"; // Sets the value of Cell B1
            worksheet.Cells[1, 2].Value = "This is cell B1"; // Also sets the value of Cell B1

            worksheet.Cells["A1:B3"].Style.Numberformat.Format    = "#,##0"; //Sets the numberformat for a range
            worksheet.Cells[1, 1, 3, 2].Style.Numberformat.Format = "#,##0"; //Same as above,A1:B3

            worksheet.Cells["A1:B3,D1:E57"].Style.Numberformat.Format = "#,##0"; //Sets the numberformat for a range containing two addresses.
            worksheet.Cells["A:B"].Style.Font.Bold        = true;    //Sets font-bold to true for column A & B
            worksheet.Cells["1:1,A:A,C3"].Style.Font.Bold = true;    //Sets font-bold to true for row 1,column A and cell C3
            worksheet.Cells["A:XFD"].Style.Font.Name      = "Arial"; //Sets font to Arial for all cells in a worksheet.
            worksheet.Cells.Style.Font.Name               = "Arial"; //This is equal to the above.

            worksheet.Cells["A1:B3,D1:E57"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A1:B3,D1:E57"].Style.Fill.BackgroundColor.SetColor(Color.Aquamarine);
            worksheet.Column(2).Width = 350;

            //integer (not really needed unless you need to round numbers, Excel will use default cell properties)
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "0";
            //integer without displaying the number 0 in the cell
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "#";
            //number with 1 decimal place
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "0.0";
            //number with 2 decimal places
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "0.00";
            //number with 2 decimal places and thousand separator
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "#,##0.00";
            //number with 2 decimal places and thousand separator and money symbol
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "€#,##0.00";
            //percentage (1 = 100%, 0.01 = 1%)
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "0%";
            //accounting number format
            worksheet.Cells["A1:A25"].Style.Numberformat.Format = "_-$* #,##0.00_-;-$* #,##0.00_-;_-$* \"-\"??_-;_-@_-";

            worksheet.Cells[1, 5].Style.Numberformat.Format = "###,###,##0.00";
            worksheet.Cells[1, 5].Value                     = 24558.4780;

            package.Save();
        }
    }
}

/*
 
 https://epplussoftware.com/en/Developers/
 
-- Controler --
public IActionResult GetExcel()
{
    using(var package = new ExcelPackage())
    {
        var worksheet = package.Workbook.Worksheets.Add("Test");
        var excelData = package.GetAsByteArray();
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var fileName = "MyWorkbook.xlsx";
        return File(excelData, contentType, fileName);
    }
}

-- LoadFrom --
using (var pck = new ExcelPackage())
{
    //Create a datatable with the directories and files from the root directory...
    DataTable dt = GetDataTable(Utils.GetDirectoryInfo("."));

    var wsDt = pck.Workbook.Worksheets.Add("FromDataTable");

    //Load the datatable and set the number formats...
    wsDt.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.Medium9);
    wsDt.Cells[2, 2, dt.Rows.Count + 1, 2].Style.Numberformat.Format = "#,##0";
    wsDt.Cells[2, 3, dt.Rows.Count + 1, 4].Style.Numberformat.Format = "mm-dd-yy";
    wsDt.Cells[wsDt.Dimension.Address].AutoFitColumns();
    //Select Name and Created-time...
    var collection = (from row in dt.Select() select new { Name = row["Name"], Created_time = (DateTime)row["Created"] });

    var wsEnum = pck.Workbook.Worksheets.Add("FromAnonymous");

    //Load the collection starting from cell A1...
    wsEnum.Cells["A1"].LoadFromCollection(collection, true, TableStyles.Medium9);
}

-- LoadFromText (.csv) --
//Create the format object to describe the text file
var format = new ExcelTextFormat();
format.TextQualifier = '"';
format.SkipLinesBeginning = 2;
format.SkipLinesEnd = 1;


var range = sheet.Cells["A1"].LoadFromText(new FileInfo(@"c:\temp\Sample9-1.csv"), false, format, TableStyles.Medium27, true);



*/