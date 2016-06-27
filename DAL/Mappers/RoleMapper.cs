using System;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Interfaces;
using ORM;

namespace DAL.Mappers
{
    public class RoleMapper : IMapper<Role, DalRole>
    {
        private readonly IVisitor<Role, DalRole> visitorMapper;
        public RoleMapper(IVisitor<Role, DalRole> visitorMapper)
        {
            if (visitorMapper == null) throw new ArgumentNullException(nameof(visitorMapper));
            this.visitorMapper = visitorMapper;
            this.visitorMapper.Map(t => t.Id, s => s.Id)
                .Map(t => t.Name, s => s.Name);
        }

        public Role ToDataBase(DalRole dalObj)
        {
            if (dalObj == null) throw new ArgumentNullException(nameof(dalObj));

            return new Role
            {
                Id = dalObj.Id,
                Name = dalObj.Name
            };
        }

        public DalRole ToDal(Role dbEntity)
        {
            if (dbEntity == null) throw new ArgumentNullException(nameof(dbEntity));

            return new DalRole
            {
                Id = dbEntity.Id,
                Name = dbEntity.Name
            };
        }

        public Expression<Func<Role, bool>> ToDataBaseExpression(Expression<Func<DalRole, bool>> sourceExpression)
        {
            if (sourceExpression == null) throw new ArgumentNullException(nameof(sourceExpression));
            return visitorMapper.ToDataBaseExpression(sourceExpression);
        }
    }
}