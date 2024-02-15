using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.ViewComponents.Login
{
    public class TopBarInfo : ViewComponent
    {
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly UserManager<User> _userManager;

        public TopBarInfo(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var value = SigningInfo().Result;

            ViewBag.Writer = value;
            
            return View();
        }

        private async Task<string> SigningInfo()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                string userId = await _userManager.GetUserIdAsync(user);
                var writer = _writerManager.GetWriterBySession(userId);

                if (writer != null)
                {
                    return writer.WriterNameSurname;
                }

                return null;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
