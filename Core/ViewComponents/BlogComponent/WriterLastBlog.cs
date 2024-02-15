using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2010.Excel;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreDemo.ViewComponents.BlogComponent
{
    public class WriterLastBlog : ViewComponent
    {
        private readonly BlogManager _blogManager = new(new EfBlogRepository());

        public IViewComponentResult Invoke(int id)
        {
            ViewBag.i = id;
            var blog = _blogManager.GetEntityById(id);
            var values = _blogManager.GetBlogListByWriter(blog.WriterID, false);
            return View(values);
        }
    }
}
