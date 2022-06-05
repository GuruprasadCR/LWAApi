using LWAApi.Error_handlings;
using LWAApi.Models;
using LWAApi.Repositories.Contracts;
using LWAApi.Views;
using Microsoft.EntityFrameworkCore;
using NLog;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using PdfSharp.Pdf;
using System.Diagnostics;
using PdfSharp.Drawing;

namespace LWAApi.Repositories
{
    public class UserRepository :IUserRepository
    {

        readonly DatabaseContext _dbContext = new();
        readonly Logger loggerx = LogManager.GetCurrentClassLogger();

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<User> GetUsers()
        {
            try
            {
                return _dbContext.User.ToList();                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public User GetUser(int UserId)
        {
            try
            {
                User? User = _dbContext.User.Find(UserId);
                if (User != null)
                {
                    //To text
                    StreamWriter writer = new StreamWriter(@"C:\Users\Ei12756\Desktop\LWAapifile\Getuserbyid.txt", true);
                    writer.WriteLine("UserId:" + User.UserId);
                    writer.WriteLine("FirstName:" + User.FirstName);
                    writer.WriteLine("LastName:" + User.LastName);
                    writer.WriteLine("Phonenumber:" + User.phonenumber);
                    writer.WriteLine();
                    writer.Close();

                    //To XML
                    string filename =@"C:\Users\Ei12756\Desktop\LWAapifile\Getuser.xml";
                    XmlTextWriter write = new XmlTextWriter(filename,System.Text.Encoding.UTF8);
                    write.Formatting = Formatting.Indented;
                    write.WriteStartDocument();
                    write.WriteStartElement("User");
                   // write.WriteElementString("UserID", User.UserId);
                    write.WriteElementString("FirstName", User.FirstName);
                    write.WriteElementString("LastName", User.LastName);
                    write.WriteElementString("Phonenumber", User.phonenumber);
                    write.WriteEndElement();
                    write.WriteEndDocument();
                    write.Flush();


                    //TO Excel
                    string filepath = @"C:\Users\Ei12756\Desktop\LWAapifile\Get.xlsx";
                    FileInfo file = new FileInfo(filepath);
                    //filePath = Server.MapPath("ExcelDemo.xlsx");

                    ExcelPackage package = new ExcelPackage(file);
                    var Worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    Worksheet.Cells[1, 1].Value = "UserID";
                    Worksheet.Cells[1, 1].Style.Font.Bold = true;
                    Worksheet.Cells[1, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    Worksheet.Cells[1, 2].Value = "FirstName";
                    Worksheet.Cells[1, 2].Style.Font.Bold = true;
                    Worksheet.Cells[1, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    Worksheet.Cells[1, 3].Value = "LastName";
                    Worksheet.Cells[1, 3].Style.Font.Bold = true;
                    Worksheet.Cells[1, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    Worksheet.Cells[1, 4].Value = "Phonenumber";
                    Worksheet.Cells[1, 4].Style.Font.Bold = true;
                    Worksheet.Cells[1, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    Worksheet.Cells[2, 1].Value = User.UserId;
                    Worksheet.Cells[2, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    Worksheet.Cells[2, 2].Value = User.FirstName;
                    Worksheet.Cells[2, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    Worksheet.Cells[2, 3].Value = User.LastName;
                    Worksheet.Cells[2, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    Worksheet.Cells[2, 4].Value = User.phonenumber;
                    Worksheet.Cells[2, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                    package.Save();
                    package.Dispose();

                    //Csvfile
                    StringBuilder csvc = new StringBuilder();
                    csvc.AppendLine("UserId, FirstName,LastName,Phonenumber");
                    csvc.AppendLine($"{ User.UserId},{ User.FirstName},{ User.LastName},{ User.phonenumber}");
                    string csvpath= @"C:\Users\Ei12756\Desktop\LWAapifile\Getcsv.csv";
                    File.AppendAllText(csvpath, csvc.ToString());


                    //To pdf
                    PdfDocument pdf = new PdfDocument();
                    PdfPage page = pdf.AddPage();
                    XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    gfx.DrawString($"UserId = { User.UserId}, FirstName={ User.FirstName},  LastName={ User.LastName},Phonenumber={ User.phonenumber} ", font, XBrushes.Black,
                             new XRect(0, 0, page.Width, page.Height),
                                                   XStringFormat.TopLeft);

                    string pdfFilename = @"C:\Users\Ei12756\Desktop\LWAapifile\Get.pdf"; 
                    pdf.Save(pdfFilename);

                   

                    return User;
                    
                }
              
                else 
                {
                    
                    throw new Exception("Not registered");

                }
            }
            catch(Exception ex)
            {
                loggerx.Error("incorrect value Inserted");
                throw new CustomExceptions("Usernot exist", ex);
            }
        }
        public async void CreateUser(User User)
        {
            try
            {
                _dbContext.User.Add(User);
                _dbContext.SaveChanges();

                StreamWriter writer = new StreamWriter(@"C:\Users\Ei12756\Desktop\LWAapifile\Demo.txt",true);
                writer.WriteLine("UserId:" + User.UserId);
                writer.WriteLine("FirstName:" + User.FirstName);
                writer.WriteLine("LastName:" + User.LastName);
                writer.WriteLine("Phonenumber:" +User.phonenumber);
                writer.WriteLine();
                writer.Close();
            }
            catch
            {
                throw new Exception("UserID already exist");
            }

        }
        public void UpdateUser( User  User)
        {
            try
            {
                _dbContext.Entry(User).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }

        }


        public User DeleteUser(int UserId)
        {
            try
            {

                User? User = _dbContext.User.Find(UserId);

                if (User != null)
                {
                    _dbContext.User.Remove(User);
                    _dbContext.SaveChanges();
                    return User;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw new Exception("User not exist"); 
            }
        }




        public bool DoesUserEixsts(int UserId)
        {
            return _dbContext.User.Any(e => e.UserId == UserId);
        }
    }
}
