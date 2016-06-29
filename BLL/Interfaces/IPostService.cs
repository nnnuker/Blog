using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IPostService : IService<BllPost>
    {
        IEnumerable<BllPost> Get(string text);
        IEnumerable<BllPost> GetByTitle(string title);
        IEnumerable<BllPost> GetByBlog(int blogId);
        IEnumerable<BllPost> GetByTags(IEnumerable<BllTag> tags);
    }
}