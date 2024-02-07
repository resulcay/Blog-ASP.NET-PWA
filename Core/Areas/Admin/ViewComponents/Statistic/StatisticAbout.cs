using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Core.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticAbout : ViewComponent
    {
        //readonly AdminManager adminManager = new(new EfAdminRepository());

        public IViewComponentResult Invoke()
        {
            //EntityLayer.Concrete.Admin admin = adminManager.GetEntityById(1);
            //ViewBag.adminName = admin.Name;
            //ViewBag.adminImage = admin.Image;
            //ViewBag.adminAbout = admin.About;
            return View();
        }
    }
}
