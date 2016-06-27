using System;
using System.Collections.Generic;
using BLL.Entities;
using BLL.Interfaces;
using DAL.DTO;
using DAL.Interfaces;

namespace BLL.Services
{
    public class Service<TDal, TBll> : IService<TBll> 
        where TBll : IBllEntity 
        where TDal : IDalEntity
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRepository<TDal> repository;

        public Service(IUnitOfWork unitOfWork, IRepository<TDal> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public virtual void Create(TBll entity)
        {
            throw new NotImplementedException();
        }

        public TBll Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TBll> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Delete(TBll entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TBll entity)
        {
            throw new NotImplementedException();
        }
    }
}