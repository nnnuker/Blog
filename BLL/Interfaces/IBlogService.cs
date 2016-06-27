using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IBlogService : IService<BllBlog>
    {
        IEnumerable<BllBlog> Get(string title);
        IEnumerable<BllBlog> GetByUser(int userId);
    }
}