using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.ViewComponents.Category
{
	public class CategoryListDashboard : ViewComponent
	{
		readonly CategoryManager manager = new(new EfCategoryRepository());

		public IViewComponentResult Invoke()
		{
			var values = manager.GetEntities();
			return View(values);
		}
	}
}
