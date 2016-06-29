using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using Ninject;

namespace MvcPL.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly IUserService userService;
        private readonly ICommentService commentService;

        public CommentController(IKernel kernel)
        {
            this.userService = kernel.Get<IUserService>();
            this.commentService = kernel.Get<ICommentService>();
        }

        public ActionResult DeleteComment(string jsonComment)
        {
            var comment = System.Web.Helpers.Json.Decode<CommentViewModel>(jsonComment);
            commentService.Delete(comment.Id);

            if (Request.IsAjaxRequest())
            {
                return RedirectToAction("GetCommentsByPost", "Comment", new { postId = comment.PostId });
            }

            return RedirectToAction("GetPost", "Home", new { postId = comment.PostId });
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
                return RedirectToAction("GetCommentsByPost", "Comment", new { postId = comment.PostId });
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

        [AllowAnonymous]
        public PartialViewResult GetCommentsByPost(int postId)
        {
            var comments = commentService.GetByPost(postId).Reverse();
            var model = new List<CommentViewModel>();
            foreach (var comment in comments)
            {
                var user = userService.Get(comment.UserId);
                var com = comment.ToViewModel(user);
                if (User.Identity.IsAuthenticated)
                {
                    com.IsMy = user.Email == User.Identity.Name || IsInRoles();
                }
                model.Add(com);
            }

            return PartialView(model);
        }

        private bool IsInRoles()
        {
            return User.IsInRole("Admin");
        }
    }
}