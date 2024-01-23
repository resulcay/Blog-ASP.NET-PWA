using Core.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public class WriterController : Controller
    {
        readonly UserManager<AppUser> userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
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
            var value = await userManager.FindByNameAsync(User.Identity.Name);

            UserUpdateViewModel model = new()
            {
                UserName = value.UserName,
                NameSurname = value.NameSurname,
                Email = value.Email,
                ImageUrl = value.Image
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel model)
        {
            var value = await userManager.FindByNameAsync(User.Identity.Name);

            value.NameSurname = model.NameSurname;
            value.UserName = model.UserName;
            value.Email = model.Email;
            value.Image = model.ImageUrl;
            value.PasswordHash = userManager.PasswordHasher.HashPassword(value, model.Password);

            var result = await userManager.UpdateAsync(value);
            
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
            }

            return View(model);
        }

        //[HttpPost]
        //public IActionResult WriterEditProfile(Writer writer, string confirmPassword, dynamic image)
        //{
        //	if (writer.WriterPassword != confirmPassword)
        //	{
        //		ModelState.AddModelError("ConfirmPassword", "Şifreler eşleşmiyor.");
        //	}

        //	if (image == null)
        //	{
        //		ModelState.AddModelError("WriterImage", "Lütfen bir profil resmi yükleyiniz.");
        //	}

        //	WriterValidator writerValidator = new();
        //	ValidationResult result = writerValidator.Validate(writer);

        //	if (result.IsValid && ModelState.IsValid)
        //	{
        //		writerManager.UpdateEntity(writer);
        //		return RedirectToAction("Index", "Dashboard");
        //	}
        //	else
        //	{
        //		foreach (var item in result.Errors)
        //		{
        //			ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
        //		}
        //	}

        //	return View();
        //}
    }
}
