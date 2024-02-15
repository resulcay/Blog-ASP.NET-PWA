using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Core.ViewComponents.Login
{
    public class SignedLastBlog : ViewComponent
    {
		private readonly WriterManager _writerManager = new(new EfWriterRepository());
		private readonly BlogManager _blogManager = new(new EfBlogRepository());
		private readonly UserManager<User> _userManager;

		public SignedLastBlog(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public IViewComponentResult Invoke()
        {
			var writer = GetWriterID().Result;

			var values = _blogManager.GetBlogListByWriter(writer.WriterID, false);
			return View(values);
		}

		private async Task<EntityLayer.Concrete.Writer> GetWriterID()
		{
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			string userId = await _userManager.GetUserIdAsync(user);
			var writer = _writerManager.GetWriterBySession(userId);

			return writer;
		}
	}
}
