using System;
using System.Linq.Expressions;
using DAL.DTO;

namespace DAL.Interfaces
{
    public interface IVisitor<TEntity, TDal> where TDal : IDalEntity
    {
        IVisitor<TEntity, TDal> Map<TProperty>(
            Expression<Func<TDal, TProperty>> targetProperty,
            Expression<Func<TEntity, TProperty>> sourceProperty);

        Expression<Func<TEntity, bool>> ToDataBaseExpression(
            Expression<Func<TDal, bool>> sourceExpression);
    }
}