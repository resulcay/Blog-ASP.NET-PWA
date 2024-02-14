using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Areas.Admin.ViewComponents.Admin
{
    public class MessageCount : ViewComponent
    {
        private readonly MessageManager _messageManager = new(new EfMessageRepository());
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly UserManager<User> _userManager;

        public MessageCount(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            // TODO: will impact performance if there are many
            string allMessageCount = _messageManager.GetEntities().Count.ToString();
            string sentMessageCount = _messageManager.GetSentMessagesByWriter(GetWriterID().Result).Count.ToString();
            string result = allMessageCount + "/" + sentMessageCount;

            ViewBag.result = result;
            return View();
        }

        private async Task<int> GetWriterID()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            return writer.WriterID;
        }
    }
}
