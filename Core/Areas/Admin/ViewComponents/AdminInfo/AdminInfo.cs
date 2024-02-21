using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Areas.Admin.ViewComponents.AdminInfo
{
    public class AdminInfo : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminInfo(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var user = GetUserFromSession().Result;
            return View(user);
        }

        private async Task<User> GetUserFromSession()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return user;
        }
    }
}
