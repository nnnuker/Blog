using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IService<TBll> where TBll : IBllEntity
    {
        TBll Get(int id);
        IEnumerable<TBll> GetAll();
        void Create(TBll entity);
        void Update(TBll entity);
        void Delete(int id);
        void Delete(TBll entity);
    }
}