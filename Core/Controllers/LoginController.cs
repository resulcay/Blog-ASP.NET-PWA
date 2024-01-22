using Core.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public LoginController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserLoginViewModel userLoginViewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(userLoginViewModel.UserName, userLoginViewModel.Password, false, true);
				if (result.Succeeded)
				{
					//  return RedirectToAction("Index", "Dashboard");
					return RedirectToAction("Index", "Home");
				}
				else
				{
					return RedirectToAction("Index", "Login");
				}
			}
			return View();
		}

		//[HttpPost]
		//public async Task<IActionResult> Index(Writer writer)
		//{
		//    using var context = new Context();
		//    var value = context.Writers.FirstOrDefault(x => x.WriterMail == writer.WriterMail && x.WriterPassword == writer.WriterPassword);

		//    if (value != null)
		//    {
		//        var claims = new List<Claim>
		//        {
		//            new (ClaimTypes.Name, writer.WriterMail)
		//        };

		//        var userIdentity = new ClaimsIdentity(claims, "login");
		//        ClaimsPrincipal principal = new(userIdentity);
		//        await HttpContext.SignInAsync(principal);

		//        return RedirectToAction("Index", "Dashboard");
		//    }

		//    return View();
		//}
	}
}
