using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adı girin.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifre girin.")]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}