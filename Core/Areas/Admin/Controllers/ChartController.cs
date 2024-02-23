using BusinessLayer.Concrete;
using Core.Areas.Admin.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly AdminManager _adminManager = new(new EfAdminRepository());

        public ChartController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

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

        public IActionResult RoleChart()
        {
            var roleModels = new List<RoleModel>();
            List<string> roles = _adminManager.GetRoles();

            foreach (string role in roles)
            {
                List<User> usersAssociatedWithRole = _userManager.GetUsersInRoleAsync(role).Result.ToList();

                roleModels.Add(
                    new RoleModel()
                    {
                        RoleName = role,
                        RoleCount = usersAssociatedWithRole.Count
                    });
            }

            string jsonString = JsonConvert.SerializeObject(new { jsonList = roleModels });

            return Content(jsonString, "application/json");
        }
    }
}
