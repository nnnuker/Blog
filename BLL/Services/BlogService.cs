using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.DTO;
using DAL.Interfaces;

namespace BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DalBlog> repository;

        public BlogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<DalBlog>();
        }

        public BllBlog Get(int id)
        {
            return repository.Get(id)?.ToBll();
        }

        public IEnumerable<BllBlog> GetAll()
        {
            return repository.GetAll()?.Select(blog => blog.ToBll());
        }

        public void Create(BllBlog entity)
        {
            repository.Create(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Update(BllBlog entity)
        {
            repository.Update(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.Commit();
        }

        public void Delete(BllBlog entity)
        {
            repository.Delete(entity.ToDal());
            unitOfWork.Commit();
        }

        public IEnumerable<BllBlog> Get(string title)
        {
            return repository.Get(blog => blog.Title == title)?.Select(x => x.ToBll());
        }

        public IEnumerable<BllBlog> GetByUser(int userId)
        {
            return repository.Get(blog => blog.UserId == userId)?.Select(x => x.ToBll());
        }
    }
}