using System;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Interfaces;
using ORM;

namespace DAL.Mappers
{
    public class CommentMapper : IMapper<Comment, DalComment>
    {
        private readonly IVisitor<Comment, DalComment> visitorMapper;

        public CommentMapper(IVisitor<Comment, DalComment> visitorMapper)
        {
            if (visitorMapper == null) throw new ArgumentNullException(nameof(visitorMapper));

            this.visitorMapper = visitorMapper;

            this.visitorMapper.Map(t => t.Id, s => s.Id)
                .Map(t => t.Content, s => s.Content)
                .Map(t => t.PostId, s => s.PostId)
                .Map(t => t.UserId, s => s.UserId);
        }

        public DalComment ToDal(Comment dbEntity)
        {
            if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

            return new DalComment
            {
                Id = dbEntity.Id,
                Content = dbEntity.Content,
                PostId = dbEntity.PostId,
                UserId = dbEntity.UserId
            };
        }

        public Comment ToDataBase(DalComment dalObj)
        {
            if (dalObj == null) throw new ArgumentNullException(nameof(dalObj));

            return new Comment
            {
                Id = dalObj.Id,
                Content = dalObj.Content,
                PostId = dalObj.PostId,
                UserId = dalObj.UserId
            };
        }

        public Expression<Func<Comment, bool>> ToDataBaseExpression(Expression<Func<DalComment, bool>> sourceExpression)
        {
            if (sourceExpression == null) throw new ArgumentNullException(nameof(sourceExpression));

            return visitorMapper.ToDataBaseExpression(sourceExpression);
        }
    }
}