using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using Core.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Controllers
{
    [Authorize]
    public class WriterController : Controller
    {
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> _userManager;

        public WriterController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            _userManager = userManager;
        }

        public PartialViewResult WriterNavBarPartial()
        {
            return PartialView();
        }

        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
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

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            UserUpdateViewModel model = new()
            {
                WriterUserName = writer.WriterUserName,
                WriterNameSurname = writer.WriterNameSurname,
                WriterMail = writer.WriterMail,
                WriterAbout = writer.WriterAbout,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel userModel, string confirmPassword)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            if (userModel.WriterPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
            }

            writer.WriterNameSurname = userModel.WriterNameSurname;
            writer.WriterUserName = userModel.WriterUserName;
            writer.WriterMail = userModel.WriterMail;
            writer.WriterAbout = userModel.WriterAbout;
            writer.WriterPassword = userModel.WriterPassword;

            WriterValidator writerValidator = new();
            var validationResult = writerValidator.Validate(writer);

            if (validationResult.IsValid)
            {
                user.NameSurname = userModel.WriterNameSurname;
                user.UserName = userModel.WriterUserName;
                user.Email = userModel.WriterMail;
                user.Image = writer.WriterImage;
                user.SecurityStamp = Guid.NewGuid().ToString();

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userModel.WriterPassword);

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    writer.WriterPassword = null;
                    writer.User = null;
                    _writerManager.UpdateEntity(writer);
                    await signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Login");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View(userModel);
        }
    }
}