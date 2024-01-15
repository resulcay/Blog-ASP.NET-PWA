using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using DateTimeExtensions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Core.ViewComponents.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        readonly MessageManager manager = new(new EfMessageRepository());

        public IViewComponentResult Invoke()
        {
            string writerMail = "ali@gmail.com";
            var values = manager.GetMessagesByWriter(writerMail);

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
