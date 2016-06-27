using System;
using System.Collections.Generic;
using System.Data.Entity;
using DAL.DTO;
using DAL.Interfaces;
using Ninject;
using Ninject.Parameters;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private readonly DbContext context;
        private readonly IKernel kernel;
        private bool disposed;
        private readonly Dictionary<Type, object> repositories;

        #endregion

        #region Constructor

        public UnitOfWork(DbContext context, IKernel kernel)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (kernel == null) throw new ArgumentNullException(nameof(kernel));

            this.context = context;
            this.kernel = kernel;
            repositories = new Dictionary<Type, object>();
        }

        #endregion

        #region Public methods

        public void Commit()
        {
            context.SaveChanges();
        }

        public IRepository<T> GetRepository<T>() where T : IDalEntity
        {
            if (!repositories.ContainsKey(typeof(T)))
            {
                repositories.Add(typeof(T), kernel.Get<IRepository<T>>(new ConstructorArgument("context", context)));
            }
            return repositories[typeof(T)] as IRepository<T>;
        }

        #endregion

        #region Dispose pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            disposed = true;
        }

        #endregion
    }
}