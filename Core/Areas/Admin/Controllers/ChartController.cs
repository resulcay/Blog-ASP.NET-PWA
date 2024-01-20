using Core.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryChart()
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
