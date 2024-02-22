using BusinessLayer.Concrete;
using Core.Areas.Admin.Models;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChartController : Controller
    {
        private readonly CategoryManager _categoryManager = new(new EfCategoryRepository());

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult BlogPartial()
        {
            return PartialView();
        }

        public PartialViewResult UserPartial()
        {
            return PartialView();
        }

        public IActionResult CategoryChart()
        {
            List<CategoryModel> list = new();
            Dictionary<string, int> dictionary = _categoryManager.GetCategoryWithBlogCount();
            List<string> keys = dictionary.Keys.ToList();
            List<int> values = dictionary.Values.ToList();

            for (int i = 0; i < dictionary.Count; i++)
            {
                CategoryModel categoryModel = new()
                {
                    CategoryName = keys[i],
                    CategoryCount = values[i]
                };

                list.Add(categoryModel);
            }

            string jsonString = JsonConvert.SerializeObject(new { jsonList = list });

            return Content(jsonString, "application/json");
        }

        public IActionResult UserChart()
        {
            List<CategoryModel> list = new()
            {
                new(){ CategoryName = "Teknoloji", CategoryCount = 10},
                new(){ CategoryName = "Yazılım", CategoryCount = 6},
                new(){ CategoryName = "Spor", CategoryCount = 3},
            };

            string jsonString = JsonConvert.SerializeObject(new { jsonList = list });

            return Content(jsonString, "application/json");
        }
    }
}
