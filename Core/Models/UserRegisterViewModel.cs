using Microsoft.AspNetCore.Http;

namespace Core.Models
{
    public class UserRegisterViewModel
    {
        public IFormFile WriterImage { get; set; }

        public string WriterUserName { get; set; }

        public string WriterNameSurname { get; set; }

        public string WriterAbout { get; set; }

        public string WriterMail { get; set; }

        public virtual string WriterPassword { get; set; }
    }
}
