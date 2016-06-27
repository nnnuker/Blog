using System;
using System.Linq.Expressions;
using DAL.DTO;
using DAL.Interfaces;
using ORM;

namespace DAL.Mappers
{
    public class UserMapper : IMapper<User, DalUser>
    {
        private readonly IVisitor<User, DalUser> visitorMapper;

        public UserMapper(IVisitor<User, DalUser> visitorMapper)
        {
            if (visitorMapper == null) throw new ArgumentNullException(nameof(visitorMapper));

            this.visitorMapper = visitorMapper;

            this.visitorMapper.Map(t => t.Id, s => s.Id)
                .Map(t => t.Password, s => s.Password)
                .Map(t => t.Email, s => s.Email)
                .Map(t => t.FirstName, s => s.FirstName)
                .Map(t => t.LastName, s => s.LastName)
                .Map(t => t.RoleId, s => s.RoleId);
        }

        public User ToDataBase(DalUser dalUser)
        {
            if (dalUser == null) throw new ArgumentNullException(nameof(dalUser));

            return new User
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Password = dalUser.Password,
                FirstName = dalUser.FirstName,
                LastName = dalUser.LastName,
                RoleId = dalUser.RoleId,
            };
        }

        public DalUser ToDal(User entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return new DalUser
            {
                Id = entity.Id,
                Email = entity.Email,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                RoleId = entity.RoleId
            };
        }

        public Expression<Func<User, bool>> ToDataBaseExpression(Expression<Func<DalUser, bool>> sourceExpression)
        {
            if (sourceExpression == null) throw new ArgumentNullException(nameof(sourceExpression));
            return visitorMapper.ToDataBaseExpression(sourceExpression);
        }
    }
}