using BusinessLayer.Concrete;
using Core.Areas.Admin.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticAbout : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly AdminManager _adminManager = new(new EfAdminRepository());

        public StatisticAbout(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = GetUserFromSession().Result;
            var data = _adminManager.ComplexQueryData();

            ViewBag.adminName = user.WriterNameSurname;
            ViewBag.adminImage = user.WriterImage;
            ViewBag.adminAbout = user.WriterAbout;

            var model = new LargeWidgetViewModel
            {
                RoleCount = data[0],
                NotificationCount = data[1],
                PassiveCategoryCount = data[2],
                PassiveBlogCount = data[3],
                Date = System.DateTime.Now
            };

            return View(model);
        }

        private async Task<Writer> GetUserFromSession()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var userDTO = _writerManager.GetWriterBySession(userId);

            return userDTO;
        }
    }
}
