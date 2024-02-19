using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using Core.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly WriterManager _writerManager = new(new EfWriterRepository());

        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel userModel, string confirmPassword)
        {
            bool isJpg = string.Equals(userModel.WriterImage?.ContentType, "image/jpg", StringComparison.OrdinalIgnoreCase);
            bool isJpeg = string.Equals(userModel.WriterImage?.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase);
            bool isPng = string.Equals(userModel.WriterImage?.ContentType, "image/png", StringComparison.OrdinalIgnoreCase);

            if (userModel.WriterPassword != confirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
            }

            if (userModel.WriterImage == null)
            {
                ModelState.AddModelError("WriterImage", "Lütfen bir profil resmi yükleyiniz.");
            }
            else if (!isJpg && !isJpeg && !isPng)
            {
                ModelState.AddModelError("WriterImage", "Lütfen geçerli bir profil resmi yükleyiniz.");
            }

            Writer writer = new();
            WriterValidator writerValidator = new();

            var extension = Path.GetExtension(userModel.WriterImage?.FileName);
            var newImageName = Guid.NewGuid() + extension;

            writer.WriterImage = "/WriterImageFiles/" + newImageName;
            writer.WriterNameSurname = userModel.WriterNameSurname;
            writer.WriterUserName = userModel.WriterUserName;
            writer.WriterMail = userModel.WriterMail;
            writer.WriterPassword = userModel.WriterPassword;
            writer.WriterAbout = userModel.WriterAbout;
            writer.WriterStatus = true;

            var result = writerValidator.Validate(writer);

            if (result.IsValid && userModel.WriterImage != null && (isJpg || isJpeg || isPng))
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var location = Path.Combine(directory + newImageName);
                var stream = new FileStream(location, FileMode.Create);
                userModel.WriterImage.CopyTo(stream);

                User user = new()
                {
                    NameSurname = userModel.WriterNameSurname,
                    UserName = userModel.WriterUserName,
                    Email = userModel.WriterMail,
                    Image = writer.WriterImage,
                    IsActive = true
                };

                var task = await _userManager.CreateAsync(user, userModel.WriterPassword);

                if (task.Succeeded)
                {
                    string id = await _userManager.GetUserIdAsync(user);
                    writer.WriterPassword = null;
                    writer.UserID = int.Parse(id);
                    _writerManager.AddEntity(writer);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.ErrorMessage);
                    }
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }
    }
}
