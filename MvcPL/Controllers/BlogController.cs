using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Entities;
using BLL.Interfaces;
using Microsoft.Ajax.Utilities;
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
        private readonly ICommentService commentService;

        public BlogController(IKernel kernel)
        {
            this.blogService = kernel.Get<IBlogService>();
            this.userService = kernel.Get<IUserService>();
            this.postService = kernel.Get<IPostService>();
            this.tagService = kernel.Get<ITagService>();
            this.commentService = kernel.Get<ICommentService>();
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

            var userId = userService.Get(User.Identity.Name).Id;

            blogService.Create(new BllBlog
            {
                Title = model.Title,
                UserId = userId
            });

            return RedirectToAction("IndexUserBlogs", "Home", new { userId });
        }

        public ActionResult EditBlog(int id)
        {
            TempData["BlogId"] = id;
            return PartialView();
        }

        public ActionResult Delete(int blogId)
        {
            blogService.Delete(blogId);
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var post = postService.Get(id);
            postService.Delete(post);

            return RedirectToAction("PostsIndex", "Home", new { blogId = post.BlogId});
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
            return RedirectToAction("GetPost", "Home", new { postId = post.Id});
        }

        public ActionResult DeleteComment(string jsonComment)
        {
            var comment = System.Web.Helpers.Json.Decode<CommentViewModel>(jsonComment);
            commentService.Delete(comment.Id);

            if (Request.IsAjaxRequest())
            {
                return RedirectToAction("GetCommentsByPost", "Home", new {postId = comment.PostId});
            }

            return RedirectToAction("GetPost", "Home", new {postId = comment.PostId});
        }

        public ActionResult EditComment(string jsonComment)
        {
            var model = System.Web.Helpers.Json.Decode<CommentViewModel>(jsonComment);

            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(CommentViewModel comment)
        {
            if (comment.Id == 0)
            {
                commentService.Create(comment.ToBllComment());
            }
            else
            {
                commentService.Update(comment.ToBllComment());
            }

            if (Request.IsAjaxRequest())
            {
                return RedirectToAction("GetCommentsByPost", "Home", new { postId = comment.PostId });
            }

            return RedirectToAction("GetPost", "Home", new { postId = comment.PostId });
        }

        public PartialViewResult AddComment(int postId)
        {
            var currentUser = userService.Get(User.Identity.Name);
            var model = new CommentViewModel
            {
                PostId = postId,
                Content = "",
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                IsMy = true,
                UserId = currentUser.Id
            };
            return PartialView("EditComment", model);
        }
    }
}