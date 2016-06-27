using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Mappers;

namespace DAL.Concrete
{
    public class Repository<TEntity, TDal> : IRepository<TDal>
        where TDal : class, IDalEntity
        where TEntity : class, new()
    {
        private readonly DbContext context;
        private readonly IMapper<TEntity, TDal> mapper;

        public Repository(DbContext context, IMapper<TEntity, TDal> mapper)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));

            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<TDal> GetAll()
        {
            return context.Set<TEntity>()
                .AsEnumerable()
                .Select(x => mapper.ToDal(x));
        }

        public TDal Get(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var result = context.Set<TEntity>().Find(id);

            return result == null ? null : mapper.ToDal(result);
        }

        public IEnumerable<TDal> Get(Expression<Func<TDal, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return context.Set<TEntity>()
                    .Where(mapper.ToDataBaseExpression(predicate))
                    .AsEnumerable()
                    .Select(x => mapper.ToDal(x));
        }

        public void Create(TDal entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var c = context.Set<TEntity>()
                .Add(mapper.ToDataBase(entity));
        }

        public void Update(TDal entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            context.Set<TEntity>().AddOrUpdate(mapper.ToDataBase(entity));
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));

            var result = context.Set<TEntity>().Find(id);
            if (result == null)
                return;

            context.Set<TEntity>().Remove(result);
        }

        public void Delete(TDal entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var result = context.Set<TEntity>().Find(entity.Id);

            context.Set<TEntity>().Remove(result);
        }
    }
}