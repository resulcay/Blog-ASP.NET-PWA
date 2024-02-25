using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WriterController : Controller
    {
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly UserManager<User> _userManager;

        public WriterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetWriterByID(int writerId)
        {
            var writer = _writerManager.GetEntityById(writerId);
            var jsonWriter = JsonConvert.SerializeObject(writer);

            return Json(jsonWriter);
        }

        public IActionResult WriterList()
        {
            var writers = _writerManager.GetEntities();
            var jsonWriters = JsonConvert.SerializeObject(writers);

            return Json(jsonWriters);
        }

        public IActionResult ActivateUser(int id)
        {
            Writer writer = _writerManager.GetEntityById(id);
            User user = _userManager.FindByIdAsync(writer.UserID.ToString()).Result;

            writer.WriterStatus = true;
            user.IsActive = true;
            user.LockoutEnd = null;

            _userManager.UpdateAsync(user);
            _writerManager.UpdateEntity(writer);

            return RedirectToAction("Index", "Writer");
        }

        public IActionResult DeactivateUser(int id)
        {
            Writer writer = _writerManager.GetEntityById(id);
            User user = _userManager.FindByIdAsync(writer.UserID.ToString()).Result;

            writer.WriterStatus = false;
            user.IsActive = false;
            user.LockoutEnd  = DateTime.Now.AddYears(100);

            _userManager.UpdateAsync(user);
            _writerManager.UpdateEntity(writer);
            _userManager.UpdateSecurityStampAsync(user);

            return RedirectToAction("Index", "Writer");
        }

        public IActionResult DeleteUser(int id)
        {
            Writer writer = _writerManager.GetEntityById(id);
            User user = _userManager.FindByIdAsync(writer.UserID.ToString()).Result;

            _userManager.UpdateSecurityStampAsync(user);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + user.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _writerManager.DeleteEntity(writer);
            _userManager.DeleteAsync(user);

            return RedirectToAction("Index", "Writer");
        }
    }
}