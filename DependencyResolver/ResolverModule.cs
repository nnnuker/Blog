using System;
using System.Data.Entity;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Services;
using DAL.Concrete;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Mappers;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class ResolverModule
    {
        public static void Configure(this IKernel kernel)
        {
            kernel.Bind<DbContext>().To<BlogModel>().InRequestScope();

            kernel.Bind<IVisitor<User, DalUser>>().To<Visitor<User, DalUser>>().InRequestScope();
            kernel.Bind<IVisitor<Role, DalRole>>().To<Visitor<Role, DalRole>>().InRequestScope();
            kernel.Bind<IVisitor<Blog, DalBlog>>().To<Visitor<Blog, DalBlog>>().InRequestScope();
            kernel.Bind<IVisitor<Post, DalPost>>().To<Visitor<Post, DalPost>>().InRequestScope();
            kernel.Bind<IVisitor<Tag, DalTag>>().To<Visitor<Tag, DalTag>>().InRequestScope();
            kernel.Bind<IVisitor<Comment, DalComment>>().To<Visitor<Comment, DalComment>>().InRequestScope();

            kernel.Bind<IMapper<Role, DalRole>>().To<RoleMapper>().InSingletonScope();
            kernel.Bind<IMapper<User, DalUser>>().To<UserMapper>().InSingletonScope();
            kernel.Bind<IMapper<Blog, DalBlog>>().To<BlogMapper>().InSingletonScope();
            kernel.Bind<IMapper<Tag, DalTag>>().To<TagMapper>().InSingletonScope();
            kernel.Bind<IMapper<Post, DalPost>>().To<PostMapper>().InSingletonScope();
            kernel.Bind<IMapper<Comment, DalComment>>().To<CommentMapper>().InSingletonScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<IRepository<DalUser>>().To<Repository<User, DalUser>>().InRequestScope();
            kernel.Bind<IRepository<DalRole>>().To<Repository<Role, DalRole>>().InRequestScope();
            kernel.Bind<IRepository<DalBlog>>().To<Repository<Blog, DalBlog>>().InRequestScope();
            kernel.Bind<IRepository<DalPost>>().To<Repository<Post, DalPost>>().InRequestScope();
            kernel.Bind<IRepository<DalTag>>().To<Repository<Tag, DalTag>>().InRequestScope();
            kernel.Bind<IRepository<DalComment>>().To<Repository<Comment, DalComment>>().InRequestScope();

            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IService<BllRole>>().To<RoleService>().InRequestScope();
            kernel.Bind<IBlogService>().To<BlogService>().InRequestScope();
            kernel.Bind<ICommentService>().To<CommentService>().InRequestScope();
            kernel.Bind<IPostService>().To<PostService>().InRequestScope();
            kernel.Bind<ITagService>().To<TagService>().InRequestScope();
        }
    }
}