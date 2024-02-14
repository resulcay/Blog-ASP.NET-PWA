using ClosedXML.Excel;
using Core.Areas.Admin.Models;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExcelController : Controller
    {
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

            foreach (var blog in GetBlogList())
            {
                worksheet.Cell(blogRowCount, 1).Value = blog.ID;
                worksheet.Cell(blogRowCount, 2).Value = blog.Title;
                worksheet.Cell(blogRowCount, 3).Value = blog.Content;
                worksheet.Cell(blogRowCount, 4).Value = blog.CreateDate;
                worksheet.Cell(blogRowCount, 5).Value = blog.Category;
                worksheet.Cell(blogRowCount, 6).Value = blog.Writer;
                blogRowCount++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var date = DateTime.Now.ToString();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Blog Listesi- {date}.xlsx");
        }

        private static List<BlogModel> GetBlogList()
        {
            using var context = new Context();

            var blogList = context.Blogs.Include(a => a.Category).Include(b => b.Writer).Select(x => new BlogModel()
            {
                ID = x.BlogID,
                Title = x.BlogTitle,
                Content = x.BlogContent,
                CreateDate = x.BlogCreatedAt,
                Category = x.Category.CategoryName,
                Writer = x.Writer.WriterNameSurname
            }).ToList();

            return blogList;
        }

    }
}
