using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BLL.Entities;
using BLL.Interfaces;
using DAL.DTO;
using DAL.Interfaces;
using BLL.Mappers;

namespace BLL.Services
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DalTag> repository;

        public TagService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));

            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.GetRepository<DalTag>();
        }

        public BllTag Get(int id)
        {
            return repository.Get(id)?.ToBll();
        }

        public IEnumerable<BllTag> GetAll()
        {
            return repository.GetAll()?.Select(blog => blog.ToBll());
        }

        public void Create(BllTag entity)
        {
            repository.Create(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Update(BllTag entity)
        {
            repository.Update(entity.ToDal());
            unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.Commit();
        }

        public void Delete(BllTag entity)
        {
            repository.Delete(entity.ToDal());
            unitOfWork.Commit();
        }

        public IEnumerable<BllTag> Get(string tag)
        {
            return repository.Get(t => t.Name.Contains(tag)).Select(x => x.ToBll());
        }

        public IEnumerable<BllTag> GetByPost(int postId)
        {
            return repository.Get(tag => tag.PostId == postId).Select(x => x.ToBll());
        }

        public void Create(IEnumerable<BllTag> tags)
        {
            foreach (var tag in tags)
            {
                Create(tag);
            }
        }

        public void Update(IEnumerable<BllTag> tags, int postId)
        {
            var postTags = repository.Get(post => post.PostId == postId);

            foreach (var postTag in postTags)
            {
                repository.Delete(postTag);
            }

            foreach (var tag in tags)
            {
                Create(tag);
            }
        }

        public static MatchCollection GetTagMatches(string tag)
        {
            return Regex.Matches(tag, @"#{1,1}[\w]+");
        }
    }
}