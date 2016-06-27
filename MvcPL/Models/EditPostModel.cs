using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class EditPostModel
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        [Required(ErrorMessage = "Title required")]
        [StringLength(100, ErrorMessage = "Title length should not exceed 100 characters")]
        public string Title { get; set; }
        [RegularExpression(@"#{1,1}[\w]+[\s]*", ErrorMessage = "Wrong tags write")]
        public string Tags { get; set; }
        [Required(ErrorMessage = "Content required")]
        public string Content { get; set; }
    }
}