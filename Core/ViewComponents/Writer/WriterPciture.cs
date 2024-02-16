﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.ViewComponents.Writer
{
    public class WriterPicture : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly WriterManager _writerManager = new(new EfWriterRepository());

        public WriterPicture(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var writer = GetWriterID().Result;
            return View(writer);
        }

        private async Task<EntityLayer.Concrete.Writer> GetWriterID()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            return writer;
        }
    }
}
