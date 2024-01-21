using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Category
{
	public class CategoryList : ViewComponent
	{
		readonly CategoryManager manager = new(new EfCategoryRepository());

		public IViewComponentResult Invoke()
		{
			var categoriesWithBlogCounts = manager.GetCategoryListWithBlogCount();
			return View(categoriesWithBlogCounts);
		}
	}
}
