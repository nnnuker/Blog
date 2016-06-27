using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email required")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}