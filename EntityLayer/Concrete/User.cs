using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class User : IdentityUser<int>
    {
        public string NameSurname { get; set; }

        public string Image { get; set; }

        public bool IsActive { get; set; }
    }
}
