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
        readonly MessageManager manager = new(new EfMessageRepository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerID = writerManager.GetWriterIDBySession(userMail);

            var values = manager.GetReceivedMessagesByWriter(writerID);
            values.RemoveAll(x => x.SenderID == writerID);
            values = values.OrderByDescending(x => x.MessageDate).ToList();

            foreach (var item in values)
            {
                var now = DateTime.Now;
                var tempDate = item.MessageDate;
                var relativeDate = tempDate.ToNaturalText(now, false);

                relativeDate = LocalizeRelativeDateTerm(relativeDate);

                item.MessageDetails = relativeDate;
            }

            return View(values);
        }

        private static string LocalizeRelativeDateTerm(string relativeDate)
        {
            if (relativeDate.Contains("second"))
            {
                if (relativeDate.Contains("seconds"))
                {
                    relativeDate = relativeDate.Replace("seconds", "saniye önce");
                }
                relativeDate = relativeDate.Replace("second", "saniye önce");
            }
            else if (relativeDate.Contains("minute"))
            {
                if (relativeDate.Contains("minutes"))
                {
                    relativeDate = relativeDate.Replace("minutes", "dakika önce");
                }
                relativeDate = relativeDate.Replace("minute", "dakika önce");
            }
            else if (relativeDate.Contains("hour"))
            {
                if (relativeDate.Contains("hours"))
                {
                    relativeDate = relativeDate.Replace("hours", "saat önce");
                }
                relativeDate = relativeDate.Replace("hour", "saat önce");
            }
            else if (relativeDate.Contains("day"))
            {
                if (relativeDate.Contains("days"))
                {
                    relativeDate = relativeDate.Replace("days", "gün önce");
                }
                relativeDate = relativeDate.Replace("day", "gün önce");
            }
            else if (relativeDate.Contains("week"))
            {
                if (relativeDate.Contains("weeks"))
                {
                    relativeDate = relativeDate.Replace("weeks", "hafta önce");
                }
                relativeDate = relativeDate.Replace("week", "hafta önce");
            }
            else if (relativeDate.Contains("month"))
            {
                if (relativeDate.Contains("months"))
                {
                    relativeDate = relativeDate.Replace("months", "ay önce");
                }
                relativeDate = relativeDate.Replace("month", "ay önce");
            }
            else if (relativeDate.Contains("year"))
            {
                if (relativeDate.Contains("years"))
                {
                    relativeDate = relativeDate.Replace("years", "yıl önce");
                }
                relativeDate = relativeDate.Replace("year", "yıl önce");
            }

            return relativeDate;
        }
    }
}
