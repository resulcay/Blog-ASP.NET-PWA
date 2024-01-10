using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core.ViewComponents.BlogComponent
{
    public class LastBlogs : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            using var context = new Context();
            var values = context.Blogs.Where(x => x.BlogStatus == true)
                .OrderByDescending(x => x.BlogCreatedAt)
                .Take(3)
                .ToList();

            return View(values);
        }
    }
}
