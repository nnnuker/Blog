using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interfaces;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using Ninject;

namespace MvcPL.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly ITagService tagService;

        public BlogController(IKernel kernel)
        {
            this.blogService = kernel.Get<IBlogService>();
            this.userService = kernel.Get<IUserService>();
            this.postService = kernel.Get<IPostService>();
            this.tagService = kernel.Get<ITagService>();
        }

        public ActionResult CreateBlog()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBlog(BlogModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var blogs = blogService.Get(model.Title);
            var userId = userService.Get(User.Identity.Name).Id;

            if (blogs.Count(x => x.UserId == userId && x.Title == model.Title) != 0)
            {
                ModelState.AddModelError("", "Blog with the same name is already exist.");
                return View(model);
            }

            blogService.Create(new BllBlog
            {
                Title = model.Title,
                UserId = userId
            });

            var blogId = blogService.GetByUser(userId).Last().Id;

            return RedirectToAction("EditBlog", new {id = blogId});
        }

        public ActionResult EditBlog(int id)
        {
            TempData["BlogId"] = id;
            ViewBag.BlogTitle = blogService.Get(id).Title;
            var model = postService.GetByBlog(id);
            model = model.Reverse();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            blogService.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var post = postService.Get(id);
            postService.Delete(post);

            return RedirectToAction("EditBlog", new { id = post.BlogId});
        }

        public ActionResult EditPost(int id)
        {
            var post = postService.Get(id);
            var tags = tagService.GetByPost(id);
            var model = post.ToEditPost(tags);
            return View(model);
        }

        public ActionResult CreatePost()
        {
            return View("EditPost");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(EditPostModel post)
        {
            var val = (int?)TempData["BlogId"];
            if (post.Id == 0)
            {
                if (val.HasValue)
                {
                    post.BlogId = val.Value;
                    postService.Create(post.ToPost());
                    var id = postService.GetByBlog(post.BlogId).LastOrDefault()?.Id;
                    post.Id = id ?? 0;
                    tagService.Create(post.ToTags());
                }
            }
            else
            {
                postService.Update(post.ToPost());
                tagService.Update(post.ToTags(), post.Id);
            }
            return RedirectToAction("EditBlog", new {id = val ?? 0});
        }

        //public ActionResult GetUserBlogs(int userId)
        //{
        //    return View();
        //}
    }
}