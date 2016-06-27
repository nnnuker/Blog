using System;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Interfaces;
using ORM;

namespace DAL.Mappers
{
    public class BlogMapper : IMapper<Blog, DalBlog>
    {
        private readonly IVisitor<Blog, DalBlog> visitorMapper;

        public BlogMapper(IVisitor<Blog, DalBlog> visitorMapper)
        {
            if (visitorMapper == null) throw new ArgumentNullException(nameof(visitorMapper));

            this.visitorMapper = visitorMapper;

            this.visitorMapper.Map(t => t.Id, s => s.Id)
                .Map(t => t.Title, s => s.Title)
                .Map(t => t.UserId, s => s.UserId);
        }

        public Blog ToDataBase(DalBlog dalObj)
        {
            if (dalObj == null) throw new ArgumentNullException(nameof(dalObj));

            return new Blog
            {
                Id = dalObj.Id,
                Title = dalObj.Title,
                UserId = dalObj.UserId,
            };
        }

        public DalBlog ToDal(Blog dbEntity)
        {
            if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

            return new DalBlog
            {
                Id = dbEntity.Id,
                Title = dbEntity.Title,
                UserId = dbEntity.UserId
            };
        }

        public Expression<Func<Blog, bool>> ToDataBaseExpression(Expression<Func<DalBlog, bool>> sourceExpression)
        {
            if (sourceExpression == null) throw new ArgumentNullException(nameof(sourceExpression));
            return visitorMapper.ToDataBaseExpression(sourceExpression);
        }
    }
}