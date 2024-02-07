using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Core.Areas.Admin.ViewComponents.Admin
{
    public class MessageCount : ViewComponent
    {
        readonly MessageManager messageManager = new(new EfMessageRepository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        public IViewComponentResult Invoke()
        {
            // TODO: will impact performance if there are many
            string allMessageCount = messageManager.GetEntities().Count.ToString();
            string sentMessageCount = messageManager.GetSentMessagesByWriter(GetWriterID()).Count.ToString();
            string result = allMessageCount + "/" + sentMessageCount;

            ViewBag.result = result;
            return View();
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
