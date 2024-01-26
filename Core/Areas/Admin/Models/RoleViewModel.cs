using System.ComponentModel.DataAnnotations;

namespace Core.Areas.Admin.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Lütfen bir rol adı giriniz.")]
        public string Name { get; set; }
    }
}
