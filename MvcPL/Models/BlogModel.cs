using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class BlogModel
    {
        [Required(ErrorMessage = "Title required")]
        [StringLength(50, ErrorMessage = "Title length should not exceed 50 characters")]
        public string Title { get; set; }
    }
}