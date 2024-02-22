using BusinessLayer.Concrete;
using Core.Areas.Admin.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Areas.Admin.ViewComponents.Statistic
{
    public class SmallWidget : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public SmallWidget(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly AdminManager _adminManager = new(new EfAdminRepository());

        public IViewComponentResult Invoke()
        {
            var user = GetUserFromSession().Result;
            var data = _adminManager.SmallQueryData(user.WriterID);

            var model = new SmallWidgetViewModel
            {
                UserCount = data[0],
                CategoryCount = data[1],
                ReceivedMessageCount = data[2],
                SentMessageCount = data[3],
                RoleCount = data[4],
                Profit = 50.992
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