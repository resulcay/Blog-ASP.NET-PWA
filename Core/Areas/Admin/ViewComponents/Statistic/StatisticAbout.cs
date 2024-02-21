using BusinessLayer.Concrete;
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


        public StatisticAbout(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = GetUserFromSession().Result;

            ViewBag.adminName = user.WriterNameSurname;
            ViewBag.adminImage = user.WriterImage;
            ViewBag.adminAbout = user.WriterAbout;

            return View();
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
