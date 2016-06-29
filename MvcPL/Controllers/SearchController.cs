using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.Entities;
using BLL.Interfaces;
using BLL.Services;
using MvcPL.Infrastructure.Helpers;
using MvcPL.Models;
using Ninject;

namespace MvcPL.Controllers
{
    public class SearchController : Controller
    {
        private readonly ITagService tagService;
        private readonly IPostService postService;
        private readonly IUserService userService;
        private readonly IBlogService blogService;

        public SearchController(IKernel kernel)
        {
            this.tagService = kernel.Get<ITagService>();
            this.postService = kernel.Get<IPostService>();
            this.userService = kernel.Get<IUserService>();
            this.blogService = kernel.Get<IBlogService>();
        }

        public ActionResult Search(string query)
        {
            return View((object)query);
        }

        public ActionResult GetResults(string query)
        {
            if (string.IsNullOrEmpty(query))
                return new EmptyResult();

            var posts = new List<BllPost>();
            var matches = TagService.GetTagMatches(query);

            if (matches.Count != 0)
            {
                foreach (Match match in matches)
                {
                    var tags = tagService.Get(match.Value).ToList();
                    posts = posts.Union(postService.GetByTags(tags), new EqualityComparer()).ToList();
                }
            }
            else
            {
                posts = postService.Get(query).ToList();
            }

            var model = new List<PostViewModel>();
            foreach (var post in posts)
            {
                var blog = blogService.Get(post.BlogId);
                var user = userService.Get(blog.UserId);
                var tagsByPost = tagService.GetByPost(post.Id);
                post.Content = Description.GetDescription(50, post.Content);
                model.Add(new PostViewModel(post, tagsByPost, blog, user));
            }

            if (Request.IsAjaxRequest())
            {
                return Json(model);
            }
            return PartialView(model);
        }
    }
}