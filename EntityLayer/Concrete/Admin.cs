using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
	public class Admin
	{
		[Key]
		public int AdminID { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string Name { get; set; }

		public string About { get; set; }

		public string Image { get; set; }

		public string Role { get; set; }
	}
}
