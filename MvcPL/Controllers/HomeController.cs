using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interfaces;
using MvcPL.Models;
using Ninject;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly ITagService tagService;
        private static readonly int itemsToLoad = 3;

        public HomeController(IKernel kernel)
        {
            this.blogService = kernel.Get<IBlogService>();
            this.userService = kernel.Get<IUserService>();
            this.postService = kernel.Get<IPostService>();
            this.tagService = kernel.Get<ITagService>();
        }

        public ActionResult Index(int pageNumber = 1)
        {
            if (pageNumber <= 1) pageNumber = 1;
            return View(pageNumber);
        }

        public PartialViewResult GetBlogs(int pageNumber = 1)
        {
            if (pageNumber <= 1) pageNumber = 1;

            var allBlogs = blogService.GetAll();
            var blogs = allBlogs.Skip((pageNumber - 1) * itemsToLoad).Take(itemsToLoad);

            var mainModels = new List<MainModel>();

            foreach (var blog in blogs)
            {
                var user = userService.Get(blog.UserId);
                var post = postService.GetByBlog(blog.Id).LastOrDefault();
                if (post != null)
                {
                    post.Content = post.Content.Split(' ').Take(50).Aggregate((x, y) => x + " " + y) + "...";
                    mainModels.Add(new MainModel
                    {
                        BlogId = blog.Id,
                        Title = blog.Title,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        LastPost = post
                    });
                }
            }

            ViewBag.HasPrevius = pageNumber > 1;
            ViewBag.HasNext = allBlogs.Count() > pageNumber * itemsToLoad;

            var model = new BlogMainModel { MainModels = mainModels, PageNumber = pageNumber };

            return PartialView("_GetBlogs", model);
        }

        public PartialViewResult GetUserBlogs()
        {
            IEnumerable<BllBlog> blogs = new List<BllBlog>();
            if (!Request.IsAuthenticated) return PartialView("_GetUserBlogs", blogs);

            var user = userService.Get(User.Identity.Name);
            blogs = blogService.GetByUser(user.Id);
            return PartialView("_GetUserBlogs", blogs);
        }

        public ActionResult PostsIndex(int blogId, int pageNumber = 1)
        {
            if (pageNumber < 1) pageNumber = 1;
            var blog = blogService.Get(blogId);
            var user = userService.Get(blog.UserId);
            var model = new PostMainModel
            {
                BlogId = blogId,
                PageNumber = pageNumber,
                BlogTitle = blog.Title,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(model);
        }

        public PartialViewResult GetBlogPosts(int blogId, int pageNumber = 1)
        {
            var blog = blogService.Get(blogId);
            var user = userService.Get(blog.UserId);

            var allPosts = postService.GetByBlog(blog.Id);
            var posts = allPosts.Skip((pageNumber - 1) * itemsToLoad).Take(itemsToLoad);

            var mainModels = new List<PostModel>();

            foreach (var post in posts)
            {
                var tags = tagService.GetByPost(post.Id);
                mainModels.Add(new PostModel
                {
                    PostId = post.Id,
                    Content = post.Content.Split(' ').Take(50).Aggregate((x, y) => x + " " + y) + "...",
                    Title = post.Title,
                    Tags = tags
                });
            }

            var model = new PostMainModel
            {
                BlogId = blog.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PageNumber = pageNumber,
                MainModels = mainModels
            };

            ViewBag.HasPrevius = pageNumber > 1;
            ViewBag.HasNext = allPosts.Count() > pageNumber * itemsToLoad;

            return PartialView("_GetBlogPosts", model);
        }

        public ActionResult GetPost(int postId)
        {
            var post = postService.Get(postId);
            var blog = blogService.Get(post.BlogId);
            var user = userService.Get(blog.UserId);
            var tags = tagService.GetByPost(post.Id);

            var model = new PostViewModel
            {
                BlogId = blog.Id,
                Content = post.Content,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PostId = post.Id,
                Title = post.Title,
                Tags = tags
            };

            return View(model);
        }
    }
}