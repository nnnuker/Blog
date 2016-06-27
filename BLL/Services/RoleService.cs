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
    public class RoleService:IService<BllRole>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DalRole> repository;

        public RoleService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<DalRole>();
        }

        public BllRole Get(int id)
        {
            return repository.Get(id)?.ToBll();
        }

        public IEnumerable<BllRole> GetAll()
        {
            return repository.GetAll()?.Select(role => role.ToBll());
        }

        public void Create(BllRole entity)
        {
            repository.Create(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Update(BllRole entity)
        {
            repository.Update(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.Commit();
        }

        public void Delete(BllRole entity)
        {
            repository.Delete(entity.ToDal());
            unitOfWork.Commit();
        }
    }
}