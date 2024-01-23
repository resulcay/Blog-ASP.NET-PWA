using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DateTimeExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Core.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        readonly Message2Manager manager = new(new EfMessage2Repository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerID = writerManager.GetWriterIDBySession(userMail);

            var values = manager.GetReceivedMessagesByWriter(writerID);

            foreach (var item in values)
            {
                var now = DateTime.Now;
                var tempDate = item.MessageDate;
                var relativeDate = tempDate.ToNaturalText(now, false);

                item.MessageDetails = relativeDate;
            }

            return View(values);
        }
    }
}
