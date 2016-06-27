using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.DTO;

namespace DAL.Interfaces
{
    public interface IRepository<TDal> where TDal : IDalEntity
    {
        IEnumerable<TDal> GetAll();
        TDal Get(int id);
        IEnumerable<TDal> Get(Expression<Func<TDal, bool>> predicate);
        void Create(TDal dal);
        void Update(TDal dal);
        void Delete(int id);
        void Delete(TDal dal);
    }
}