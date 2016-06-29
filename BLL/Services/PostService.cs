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
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DalPost> repository;
        private readonly IRepository<DalTag> tagRepository;

        public PostService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<DalPost>();
            this.tagRepository = unitOfWork.GetRepository<DalTag>();
        }

        public BllPost Get(int id)
        {
            return repository.Get(id)?.ToBll();
        }

        public IEnumerable<BllPost> GetAll()
        {
            return repository.GetAll()?.Select(blog => blog.ToBll());
        }

        public void Create(BllPost entity)
        {
            repository.Create(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Update(BllPost entity)
        {
            repository.Update(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.Commit();
        }

        public void Delete(BllPost entity)
        {
            repository.Delete(entity.ToDal());
            unitOfWork.Commit();
        }

        public IEnumerable<BllPost> Get(string text)
        {
            return repository.Get(c => c.Content.Contains(text))?.Select(x => x.ToBll());
        }

        public IEnumerable<BllPost> GetByTitle(string title)
        {
            return repository.Get(post => post.Title == title).Select(x => x.ToBll());
        }

        public IEnumerable<BllPost> GetByBlog(int blogId)
        {
            return repository.Get(post => post.BlogId == blogId).Select(x=>x.ToBll());
        }

        public IEnumerable<BllPost> GetByTags(IEnumerable<BllTag> tags)
        {
            return tags.Select(tag => repository.Get(tag.PostId).ToBll()).ToList();
        }
    }
}