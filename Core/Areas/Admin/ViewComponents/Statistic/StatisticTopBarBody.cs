using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticTopBarBody : ViewComponent
    {
        readonly BlogManager blogManager = new(new EfBlogRepository());

        public IViewComponentResult Invoke()
        {
            var lastBlog = blogManager.GetLastBlogs().First();
            string content = lastBlog.BlogContent;

            if (content.Length > 100)
            {
                content = content[..200] + "...";
            }

            ViewBag.lastBlog = lastBlog.BlogTitle + " - " + content;

            return View();
        }
    }
}
