using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
	public class UserRegisterViewModel
	{
		[Display(Name = "Ad-Soyad")]
		[Required(ErrorMessage = "Lütfen ad-soyad giriniz.")]
		public string NameSurname { get; set; }

		[Display(Name = "Şifre")]
		[Required(ErrorMessage = "Lütfen şifre giriniz.")]
		public string Password { get; set; }

		[Display(Name = "Şifre-Tekrar")]
		[Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "E-mail")]
		[Required(ErrorMessage = "Lütfen e-mail giriniz.")]
		public string Email { get; set; }

		[Display(Name = "Kullanıcı-Adı")]
		[Required(ErrorMessage = "Lütfen kullancıı adı giriniz.")]
		public string UserName { get; set; }
	}
}
