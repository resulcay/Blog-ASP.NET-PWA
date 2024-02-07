using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Timers;

namespace ReporterWindowsService
{
    public partial class BlogReporter : ServiceBase
    {
        readonly Timer timer = new Timer();

        public BlogReporter()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToExcel();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            // 86400000 milliseconds is 24 hours
            timer.Interval = 10000;
            timer.Enabled = true;
        }
        protected override void OnStop()
        {

        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToExcel();
        }
        public void WriteToExcel()
        {
            BlogDataAccess _blogManager = new BlogDataAccess();

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            string filepath = Path.Combine(path, "ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter stream = File.AppendText(filepath))
            {
                try
                {
                    List<Blog> blogs = _blogManager.GetAllBlogs();
                    ExportBlogToExcel(blogs);
                }
                catch (Exception ex)
                {
                    stream.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void ExportBlogToExcel(List<Blog> blogs)
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filePath = $"C:\\CustomWindowsServices\\BlogReportService\\Bloglar_{now}.xlsx";

            using (ExcelPackage excelPackage = new ExcelPackage())
            {

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Blogs");

                worksheet.Cells["A1"].Value = "Blog ID";
                worksheet.Cells["B1"].Value = "Blog Başlığı";
                worksheet.Cells["C1"].Value = "Blog İçeriği";
                worksheet.Cells["D1"].Value = "Blog Oluşturulma Tarihi";
                worksheet.Cells["E1"].Value = "Blog Kategorisi";
                worksheet.Cells["F1"].Value = "Blog Yazarı";

                int blogRowCount = 2;

                foreach (var blog in blogs)
                {
                    worksheet.Cells[$"A{blogRowCount}"].Value = blog.BlogID;
                    worksheet.Cells[$"B{blogRowCount}"].Value = blog.BlogTitle;
                    worksheet.Cells[$"C{blogRowCount}"].Value = blog.BlogContent;
                    worksheet.Cells[$"D{blogRowCount}"].Value = blog.BlogCreatedAt.ToString();
                    worksheet.Cells[$"E{blogRowCount}"].Value = blog.Category;
                    worksheet.Cells[$"F{blogRowCount}"].Value = blog.Writer;

                    blogRowCount++;
                }

                File.WriteAllBytes(filePath, excelPackage.GetAsByteArray());

                SendEmailWithAttachment(filePath);
            }
        }

        static void SendEmailWithAttachment(string filePath)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                int smtpPort = 587;
                string smtpServer = "mail.pureblog.com.tr";
                string smtpUsername = "test@pureblog.com.tr";
                string smtpPassword = "8d8b3S3NnAcYZTf";
                string senderEmail = "test@pureblog.com.tr";
                string recipientEmail = "karayip630@gmail.com";
                string subject = "Günlük Blog Raporu";
                string htmlBody = @"<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
</head>

<body style=""font-family: Arial, sans-serif; margin: 20px;"">
    <div
        style=""display: flex; justify-content: center; flex-direction: column; align-items: center;padding: 20px; border: 1px solid #ccc; margin-left: auto; margin-right: auto;"">
        <h1 style=""color: #333;"">Son Eklenen Bloglar (Günlük Rapor)</h1>
        <p style=""color: #666;"">
            Dosya ekinde son eklenen blogları excel formatında görebilirsiniz.
        </p>

        <p style=""color: #666;"">
            <a href=""https://pureblog.com.tr"">Web Sitemi Ziyaret edebilirsiniz.</a>.
        </p>

        <img src=""https://firebasestorage.googleapis.com/v0/b/tekno-fest-d65ab.appspot.com/o/cup.gif?alt=media&token=789741b9-d547-43d7-8428-89198bb60de1""/>
</body>
</html>";

                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, htmlBody)
                {
                    IsBodyHtml = true
                };

                Attachment attachment = new Attachment(filePath);
                mailMessage.Attachments.Add(attachment);

                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(mailMessage);
                mailMessage.Dispose();
                attachment.Dispose();
            }
            catch (Exception ex)
            {
                string path = @"C:\CustomWindowsServices\BlogReportService\EmailServiceError.txt";

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine($"Error: {ex}");
                }
            }

        }
    }
}