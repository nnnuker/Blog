using System;
using DAL.DTO;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRepository<T> GetRepository<T>() where T : IDalEntity;
        //void Rollback();
    }
}