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
    public class CommentService:ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DalComment> repository;

        public CommentService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<DalComment>();
        }

        public BllComment Get(int id)
        {
            return repository.Get(id)?.ToBll();
        }

        public IEnumerable<BllComment> GetAll()
        {
            return repository.GetAll()?.Select(c => c.ToBll());
        }

        public void Create(BllComment entity)
        {
            repository.Create(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Update(BllComment entity)
        {
            repository.Update(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.Commit();
        }

        public void Delete(BllComment entity)
        {
            repository.Delete(entity.ToDal());
            unitOfWork.Commit();
        }

        public IEnumerable<BllComment> GetByUser(int userId)
        {
            return repository.Get(c => c.UserId == userId)?.Select(x => x.ToBll());
        }
    }
}