﻿using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core.Controllers
{
    public class AdminController : Controller
    {
        readonly Message2Manager messageManager = new(new EfMessage2Repository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        public IActionResult Index()
        {
            // TODO: will impact performance if there are many
            string allMessageCount = messageManager.GetEntities().Count.ToString();
            string sentMessageCount = messageManager.GetSentMessagesByWriter(GetWriterID()).Count.ToString();
            string result = allMessageCount + "/" + sentMessageCount;
            
            ViewBag.result = result;

            return View();
        }

        public PartialViewResult AdminNavbarPartial()
        {
            return PartialView();
        }

        private int GetWriterID()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerID = writerManager.GetWriterIDBySession(userMail);

            return writerID;
        }
    }
}
