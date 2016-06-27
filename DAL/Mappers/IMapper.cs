using System;
using System.Linq.Expressions;
using DAL.DTO;

namespace DAL.Mappers
{
    public interface IMapper<TEntity, TDal> where TDal : IDalEntity
    {
        TEntity ToDataBase(TDal dalObj);
        TDal ToDal(TEntity dbEntity);
        Expression<Func<TEntity, bool>> ToDataBaseExpression(Expression<Func<TDal, bool>> sourceExpression);
    }
}