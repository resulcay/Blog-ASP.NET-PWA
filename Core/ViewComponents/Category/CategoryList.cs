using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CoreDemo.ViewComponents.Category
{
    public class CategoryList : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var context = new Context();
            var categoriesWithBlogCounts = context.Categories.Include(x => x.Blogs).Where(c => c.Blogs.Any()).OrderByDescending(c => c.Blogs.Count).Take(7).ToList();

            return View(categoriesWithBlogCounts);
        }
    }
}
