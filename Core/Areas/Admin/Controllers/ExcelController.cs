using BusinessLayer.Concrete;
using ClosedXML.Excel;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExcelController : Controller
    {
        private readonly BlogManager _blogManager = new (new EfBlogRepository());
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly CommentManager _commentManager = new(new EfCommentRepository());

        public IActionResult BlogListToExcel()
        {
            return View();
        }

        public IActionResult ExportDynamicBlogToExcel()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Blog Listesi");
            worksheet.Cell(1, 1).Value = "Blog ID";
            worksheet.Cell(1, 2).Value = "Blog Başlığı";
            worksheet.Cell(1, 3).Value = "Blog İçeriği";
            worksheet.Cell(1, 4).Value = "Blog Oluşturulma Tarihi";
            worksheet.Cell(1, 5).Value = "Blog Kategorisi";
            worksheet.Cell(1, 6).Value = "Blog Yazarı";

            int blogRowCount = 2;

            var blogs = _blogManager.GetDetailedBlogList();

            foreach (var blog in blogs)
            {
                worksheet.Cell(blogRowCount, 1).Value = blog.BlogID;
                worksheet.Cell(blogRowCount, 2).Value = blog.BlogTitle;
                worksheet.Cell(blogRowCount, 3).Value = blog.BlogContent;
                worksheet.Cell(blogRowCount, 4).Value = blog.BlogCreatedAt;
                worksheet.Cell(blogRowCount, 5).Value = blog.Category.CategoryName;
                worksheet.Cell(blogRowCount, 6).Value = blog.Writer.WriterNameSurname;
                blogRowCount++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var date = DateTime.Now.ToString();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Blog Listesi- {date}.xlsx");
        }

        public IActionResult ExportDynamicUserToExcel()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Kullanıcı Listesi");
            worksheet.Cell(1, 1).Value = "Kullanıcı ID";
            worksheet.Cell(1, 2).Value = "Kullanıcı Adı";
            worksheet.Cell(1, 3).Value = "Kullanıcı Ad-Soyad";
            worksheet.Cell(1, 4).Value = "Kullanıcı Mail";
            worksheet.Cell(1, 5).Value = "Kullanıcı Hakkında";
            worksheet.Cell(1, 6).Value = "Kullanıcı Görsel Yolu";

            int writerRowCount = 2;

            var writers = _writerManager.GetEntities();

            foreach (var writer in writers)
            {
                worksheet.Cell(writerRowCount, 1).Value = writer.WriterID;
                worksheet.Cell(writerRowCount, 2).Value = writer.WriterUserName;
                worksheet.Cell(writerRowCount, 3).Value = writer.WriterNameSurname;
                worksheet.Cell(writerRowCount, 4).Value = writer.WriterMail;
                worksheet.Cell(writerRowCount, 5).Value = writer.WriterAbout;
                worksheet.Cell(writerRowCount, 6).Value = writer.WriterImage;
                writerRowCount++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var date = DateTime.Now.ToString();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Kullanıcı Listesi- {date}.xlsx");
        }

        public IActionResult ExportDynamicCommentToExcel()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Yorum Listesi");
            worksheet.Cell(1, 1).Value = "Yorum ID";
            worksheet.Cell(1, 2).Value = "Yorum Kullanıcı Adı";
            worksheet.Cell(1, 3).Value = "Yorum Başlığı";
            worksheet.Cell(1, 4).Value = "Yorum İçeriği";
            worksheet.Cell(1, 5).Value = "Ait Olduğu Blog Başlığı";
            worksheet.Cell(1, 6).Value = "Ait Olduğu Blog Yazarı";

            int commentRowCount = 2;

            var comments = _commentManager.GetCommentsWithBlogAndWriter();

            foreach (var comment in comments)
            {
                worksheet.Cell(commentRowCount, 1).Value = comment.CommentID;
                worksheet.Cell(commentRowCount, 2).Value = comment.CommentUserName;
                worksheet.Cell(commentRowCount, 3).Value = comment.CommentTitle;
                worksheet.Cell(commentRowCount, 4).Value = comment.CommentContent;
                worksheet.Cell(commentRowCount, 5).Value = comment.Blog.BlogTitle;
                worksheet.Cell(commentRowCount, 6).Value = comment.Blog.Writer.WriterNameSurname;
                commentRowCount++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var date = DateTime.Now.ToString();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Yorum Listesi- {date}.xlsx");
        }
    }
}
