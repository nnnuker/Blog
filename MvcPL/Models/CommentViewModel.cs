using System.ComponentModel.DataAnnotations;
using BLL.Entities;

namespace MvcPL.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Content required")]
        public string Content { get; set; }
        public int PostId { get; set; }
        public bool IsMy { get; set; }
        public int UserId { get; set; }
    }
}