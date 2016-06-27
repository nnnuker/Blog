using System.Collections.Generic;
using BLL.Entities;

namespace MvcPL.Models
{
    public class PostMainModel
    {
        public int PageNumber { get; set; }
        public IEnumerable<PostModel> MainModels { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BlogTitle { get; set; }
        public int BlogId { get; set; }
    }

    public class PostModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<BllTag> Tags { get; set; }
    }

    public class PostViewModel : PostModel
    {
        public int BlogId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}