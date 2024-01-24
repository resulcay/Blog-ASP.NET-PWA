using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        readonly Message2Manager messageManager = new(new EfMessage2Repository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        public IActionResult Inbox()
        {
            var values = messageManager.GetDetailedMessages();
            values = values.OrderByDescending(x => x.MessageDate).ToList();
            return View(values);
        }

        public IActionResult Sendbox()
        {
            var values = messageManager.GetSentMessagesByWriter(GetWriterID());
            values = values.OrderByDescending(x => x.MessageDate).ToList();
            return View(values);
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
