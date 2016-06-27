using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO;
using DAL.Interfaces;

namespace BLL.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DalUser> repository;

        public UserService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<DalUser>();
        }

        public BllUser Get(int id)
        {
            return repository.Get(id)?.ToBll();
        }

        public IEnumerable<BllUser> GetAll()
        {
            return repository.GetAll()?.Select(user => user.ToBll());
        }

        public void Create(BllUser entity)
        {
            repository.Create(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Update(BllUser entity)
        {
            repository.Update(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.Commit();
        }

        public void Delete(BllUser entity)
        {
            repository.Delete(entity.ToDal());
            unitOfWork.Commit();
        }

        public BllUser Get(string email)
        {
            return repository.Get(user => user.Email == email).FirstOrDefault()?.ToBll();
        }
    }
}