using ClosedXML.Excel;
using LWAApi.Models;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Data;
using System.Text;








namespace LWAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        readonly DatabaseContext _dbContext = new();

        public FilesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("ExcelFile")]
        public IActionResult Get()
        {
            using (var Workbook = new XLWorkbook())
            {
                var worksheet = Workbook.Worksheets.Add("Sheet1");
                worksheet.Column(4).Width = 12;
                worksheet.Column(2).Width = 12;
                worksheet.Column(3).Width = 12;
                worksheet.Column(1).Width = 8;
                //worksheet.Columns().AdjustToContents();




                //worksheet.Cells().Style.Alignment.SetWrapText(true);



                //worksheet.Row(1).Style.Font.FontSize=14;
                //worksheet.Cells[Sheet1.Dimension.Address].AutoFitColumns();
                var CR = 1;

                worksheet.Cell(CR, 1).Value = "Userid";
                worksheet.Cell(CR, 2).Value = "FirstName";
                worksheet.Cell(CR, 3).Value = "LastName";

                worksheet.Cell(CR, 4).Value = "Phonenumber";

                foreach (var row in _dbContext.User)
                {
                    CR++;
                    worksheet.Cell(CR, 1).Value = row.UserId;
                    worksheet.Cell(CR, 2).Value = row.FirstName;
                    worksheet.Cell(CR, 3).Value = row.LastName;
                    worksheet.Cell(CR, 4).Value = row.phonenumber;
                }

                using (var stream = new MemoryStream())
                {
                    Workbook.SaveAs(stream);
                    var Content = stream.ToArray();
                    return File(Content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "usersDetails.xlsx");

                }

            }



        }

        [HttpGet("pdf")]
        public IActionResult Getpdf()
        {
            //To pdf
            PdfDocument pdf = new PdfDocument();
            PdfPage page = pdf.AddPage();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            XRect rect = new XRect(40, 100, 250, 220);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
           

            foreach (var row in _dbContext.User)
            {
                gfx.DrawString($"UserId = { row.UserId}, FirstName={ row.FirstName},  LastName={ row.LastName},Phonenumber={ row.phonenumber} ", font, XBrushes.Black,
                         new XRect(0, 0, page.Width, page.Height),
                                               XStringFormat.TopLeft);

               

                //tf.DrawString($"UserId = { row.UserId}", font, XBrushes.Black, rect, XStringFormats.TopLeft);

            }
            using (var stream = new MemoryStream())
            {
                pdf.Save(stream);
                var Content = stream.ToArray();
                return File(Content, "application/pdf", "usersDetails.pdf");

            }

            string pdfFilename = @"C:\Users\Ei12756\Desktop\LWAapifile\New.pdf";
            pdf.Save(pdfFilename);

            return File(pdfFilename, "application / pdf");

            


        }



        [HttpGet("pdf2")]
        public IActionResult Getpdf2()
        {
            //To pdf
            PdfDocument pdf = new PdfDocument();
            PdfPage page = pdf.AddPage();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            //XRect rect = new XRect(40, 100, 250, 220);
            //gfx.DrawRectangle(XBrushes.SeaShell, rect);

            XStringFormat format = new XStringFormat();
          
            // Row elements
            int el1_width = 80;
            int el2_width = 200;

            // page structure options
            double lineHeight = 20;
            int marginLeft = 20;
            int marginTop = 20;

            int el_height = 30;
            int rect_height = 17;

            int interLine_X_1 = 2;
            int interLine_X_2 = 2 * interLine_X_1;

            int offSetX_1 = el1_width;
            int offSetX_2 = el1_width + el2_width;

            XSolidBrush rect_style1 = new XSolidBrush(XColors.LightGray);
            XSolidBrush rect_style2 = new XSolidBrush(XColors.Blue);
            XSolidBrush rect_style3 = new XSolidBrush(XColors.Red);

           var i= 0;
            foreach (var row in _dbContext.User)
            {
                i++;
                double dist_Y = lineHeight * (i + 1);
                double dist_Y2 = dist_Y - 2;
                gfx.DrawRectangle(rect_style2, marginLeft, marginTop, page.Width - 2 * marginLeft, rect_height);

                tf.DrawString("UserId", font, XBrushes.White,
                              new XRect(marginLeft, marginTop, el1_width, el_height));
                tf.DrawString("FirstName", font, XBrushes.White,
                              new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop, el2_width, el_height), format);

                //gfx.DrawRectangle(rect_style1, marginLeft + offSetX_2 + interLine_X_2, dist_Y2 + marginTop, el1_width, rect_height);
            
    tf.DrawString($"FirstName = { row.FirstName}",
                    font,XBrushes.Black,
                    new XRect(marginLeft + offSetX_1 + interLine_X_1, dist_Y + marginTop, el2_width, el_height),
                    format);

                //ELEMENT 1 - SMALL 80
                gfx.DrawRectangle(rect_style1, marginLeft, marginTop + dist_Y2, el1_width, rect_height);
                tf.DrawString(

                   $" { row.UserId}",
                    font,
                    XBrushes.Black,
                    new XRect(marginLeft, marginTop + dist_Y, el1_width, el_height),
                    format);
                //Element 2
                gfx.DrawRectangle(rect_style1, marginLeft + offSetX_1 + interLine_X_1, dist_Y2 + marginTop, el2_width, rect_height);
                tf.DrawString(
                    $"{row.FirstName}",
                    font,
                    XBrushes.Black,
                    new XRect(marginLeft + offSetX_1 + interLine_X_1, marginTop + dist_Y, el2_width, el_height),
                    format);




            }
            using (var stream = new MemoryStream())
            {
                pdf.Save(stream);
                var Content = stream.ToArray();
                return File(Content, "application/pdf", "usersDetails.pdf");

            }

            string pdfFilename = @"C:\Users\Ei12756\Desktop\LWAapifile\New.pdf";
            pdf.Save(pdfFilename);

            return File(pdfFilename, "application / pdf");




        }
    }
}
