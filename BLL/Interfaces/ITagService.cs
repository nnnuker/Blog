using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface ITagService : IService<BllTag>
    {
        BllTag Get(string tag);
        IEnumerable<BllTag> GetByPost(int postId);
        void Create(IEnumerable<BllTag> tags);
        void Update(IEnumerable<BllTag> tags, int postId);
    }
}