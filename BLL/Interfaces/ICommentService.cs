using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface ICommentService:IService<BllComment>
    {
        IEnumerable<BllComment> GetByUser(int userId);
    }
}