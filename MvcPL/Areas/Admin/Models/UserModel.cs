using System.ComponentModel.DataAnnotations;
using BLL.Entities;

namespace MvcPL.Areas.Admin.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Field required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Field required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field required")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Field required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public UserModel()
        {
            
        }
        public UserModel(BllUser user, string role)
        {
            this.Id = user.Id;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Role = role;
            this.Password = user.Password;
        }

        
    }
}