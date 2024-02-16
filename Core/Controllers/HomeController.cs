using Core.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search(SearchViewModel searchViewModel)
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

        private static List<SearchResultViewModel> SearchPages(SearchViewModel model)
        {
            List<SearchResultViewModel> searchResults = new();

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
                    var result = new SearchResultViewModel
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
                        var result = new SearchResultViewModel
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
                        var result = new SearchResultViewModel
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
                        var result = new SearchResultViewModel
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
