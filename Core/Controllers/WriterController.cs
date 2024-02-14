using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using Core.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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