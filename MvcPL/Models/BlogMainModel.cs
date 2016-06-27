using System.Collections.Generic;
using BLL.Entities;

namespace MvcPL.Models
{
    public class BlogMainModel
    {
        public int PageNumber { get; set; }
        public IEnumerable<MainModel> MainModels { get; set; }
    }

    public class MainModel
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public BllPost LastPost { get; set; }
    }
}