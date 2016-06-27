using System;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Interfaces;
using ORM;

namespace DAL.Mappers
{
    public class PostMapper : IMapper<Post, DalPost>
    {
        private readonly IVisitor<Post, DalPost> visitorMapper;
        public PostMapper(IVisitor<Post, DalPost> visitorMapper)
        {
            if (visitorMapper == null) throw new ArgumentNullException(nameof(visitorMapper));
            this.visitorMapper = visitorMapper;
            this.visitorMapper.Map(t => t.Id, s => s.Id)
                .Map(t => t.Content, s => s.Content)
                .Map(t => t.Title, s => s.Title)
                .Map(t => t.BlogId, s => s.BlogId);
        }

        public Post ToDataBase(DalPost dalObj)
        {
            if (dalObj == null) throw new ArgumentNullException(nameof(dalObj));

            return new Post
            {
                Id = dalObj.Id,
                Content = dalObj.Content,
                Title = dalObj.Title,
                BlogId = dalObj.BlogId
            };
        }

        public DalPost ToDal(Post dbEntity)
        {
            if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

            return new DalPost
            {
                Id = dbEntity.Id,
                Content = dbEntity.Content,
                Title = dbEntity.Title,
                BlogId = dbEntity.BlogId
            };
        }

        public Expression<Func<Post, bool>> ToDataBaseExpression(Expression<Func<DalPost, bool>> sourceExpression)
        {
            if (sourceExpression == null) throw new ArgumentNullException(nameof(sourceExpression));
            return visitorMapper.ToDataBaseExpression(sourceExpression);
        }
    }
}