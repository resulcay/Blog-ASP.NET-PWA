using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreDemo.ViewComponents.Category
{
	public class CategoryList : ViewComponent
	{
		readonly CategoryManager manager = new(new EfCategoryRepository());

		public IViewComponentResult Invoke() 
		{
			var values = manager.CategoriesWithBlogCounts();

			return View(values);
		}
	}
}
