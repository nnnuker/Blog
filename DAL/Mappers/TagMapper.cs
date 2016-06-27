using System;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Interfaces;
using ORM;

namespace DAL.Mappers
{
    public class TagMapper : IMapper<Tag, DalTag>
    {
        private readonly IVisitor<Tag, DalTag> visitorMapper;
        public TagMapper(IVisitor<Tag, DalTag> visitorMapper)
        {
            if (visitorMapper == null) throw new ArgumentNullException(nameof(visitorMapper));
            this.visitorMapper = visitorMapper;
            this.visitorMapper.Map(t => t.Id, s => s.Id)
                .Map(t => t.Name, s => s.Name)
                .Map(t => t.PostId, s => s.PostId);
        }

        public Tag ToDataBase(DalTag dalObj)
        {
            if (dalObj == null) throw new ArgumentNullException(nameof(dalObj));

            return new Tag
            {
                Id = dalObj.Id,
                Name = dalObj.Name,
                PostId = dalObj.PostId
            };
        }

        public DalTag ToDal(Tag dbEntity)
        {
            if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

            return new DalTag
            {
                Id = dbEntity.Id,
                Name = dbEntity.Name,
                PostId = dbEntity.PostId
            };
        }

        public Expression<Func<Tag, bool>> ToDataBaseExpression(Expression<Func<DalTag, bool>> sourceExpression)
        {
            if (sourceExpression == null) throw new ArgumentNullException(nameof(sourceExpression));
            return visitorMapper.ToDataBaseExpression(sourceExpression);
        }
    }
}