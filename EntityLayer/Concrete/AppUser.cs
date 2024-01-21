using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
	public class AppUser : IdentityUser<int>
	{
		public string NameSurname { get; set; }

		public string Image { get; set; }
	}
}
