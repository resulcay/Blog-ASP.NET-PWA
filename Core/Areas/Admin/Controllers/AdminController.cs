using Core.Areas.Admin.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult AdminNavbarPartial()
        {
            return PartialView();
        }

        public IActionResult Search(AdminSearchViewModel searchViewModel)
        {
            var roles = GetRoles().Result;

            if (roles.Contains("Admin"))
            {
                searchViewModel.UserType = "Admin";
            }
            else if (roles.Contains("Writer"))
            {
                searchViewModel.UserType = "Writer";
            }
            else
            {
                searchViewModel.UserType = "User/Member";
            }

            var searchResults = SearchPages(searchViewModel);

            return View(searchResults);
        }

        private static List<AdminSearchResultViewModel> SearchPages(AdminSearchViewModel model)
        {
            List<AdminSearchResultViewModel> searchResults = new();

            List<(string controller, string action)> adminControllerAndActions = new()
            {
                ("Admin", "Index"),
                ("Blog", "Index"),
                ("Category", "Index"),
                ("Category", "AddCategory"),
                ("Chart", "Index"),
                ("Comment","Index"),
                ("Excel", "BlogListToExcel"),
                ("Message", "Inbox"),
                ("Message", "SendBox"),
                ("Message", "SendMessage"),
                ("Role", "Index"),
                ("Role", "UserRoleList"),
                ("Role", "AddRole"),
                ("Widget", "Index"),
                ("Writer", "Index"),
                ("Writer", "WriterAdd"),

            };

            List<(string controller, string action)> writerControllerAndActions = new()
            {
                ("About", "Index"),
                ("Blog", "BlogAdd"),
                ("Writer", "WriterEditProfile"),
                ("Dashboard", "Index"),
                ("Message", "Inbox"),
                ("Message", "SendBox"),
                ("Message", "SendMessage"),
            };

            List<(string controller, string action)> userControllerAndActions = new()
            {
                ("Blog", "Index"),
                ("Contact", "Index"),
                ("Home", "Index"),
                ("Login", "Index"),
                ("Register", "Index"),

            };

            foreach (var (controller, action) in userControllerAndActions)
            {
                if (string.Equals(model.SearchTerm, controller, System.StringComparison.OrdinalIgnoreCase)
                    || string.Equals(model.SearchTerm, action, System.StringComparison.OrdinalIgnoreCase))
                {
                    var result = new AdminSearchResultViewModel
                    {
                        Payload = "Şu terim için sonuçlar: " + model.SearchTerm,
                        ControllerName = "/" + controller + "/",
                        ActionName = action + "/",
                    };

                    searchResults.Add(result);
                }
            }

            if (model.UserType == "Admin")
            {
                foreach (var (controller, action) in adminControllerAndActions)
                {
                    if (string.Equals(model.SearchTerm, controller, System.StringComparison.OrdinalIgnoreCase)
                        || string.Equals(model.SearchTerm, action, System.StringComparison.OrdinalIgnoreCase))
                    {
                        var result = new AdminSearchResultViewModel
                        {
                            Payload = "Şu terim için sonuçlar: " + model.SearchTerm,
                            ControllerName = "/Admin/" + controller + "/",
                            ActionName = action + "/",
                        };

                        searchResults.Add(result);
                    }
                }

                foreach (var (controller, action) in writerControllerAndActions)
                {
                    if (string.Equals(model.SearchTerm, controller, System.StringComparison.OrdinalIgnoreCase)
                        || string.Equals(model.SearchTerm, action, System.StringComparison.OrdinalIgnoreCase))
                    {
                        var result = new AdminSearchResultViewModel
                        {
                            Payload = "Şu terim için sonuçlar: " + model.SearchTerm,
                            ControllerName = "/" + controller + "/",
                            ActionName = action + "/",
                        };

                        searchResults.Add(result);
                    }
                }
            }

            if (model.UserType == "Writer")
            {
                foreach (var (controller, action) in writerControllerAndActions)
                {
                    if (string.Equals(model.SearchTerm, controller, System.StringComparison.OrdinalIgnoreCase)
                        || string.Equals(model.SearchTerm, action, System.StringComparison.OrdinalIgnoreCase))
                    {
                        var result = new AdminSearchResultViewModel
                        {
                            Payload = "Şu terim için sonuçlar: " + model.SearchTerm,
                            ControllerName = "/" + controller + "/",
                            ActionName = action + "/",
                        };

                        searchResults.Add(result);
                    }
                }
            }

            return searchResults;
        }

        private async Task<List<string>> GetRoles()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var roles = _userManager.GetRolesAsync(user).Result.ToList();

                return roles;
            }
            catch
            {
                List<string> roles = new();
                return roles;
            }
        }
    }
}
