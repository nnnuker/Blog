using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;

namespace MvcPL.Models
{
    public class PostMainModel
    {
        public int PageNumber { get; set; }
        public IEnumerable<PostModel> MainModels { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BlogTitle { get; set; }
        public int BlogId { get; set; }

        public PostMainModel()
        {
        }

        public PostMainModel(int pageNumber, IEnumerable<PostModel> mainModels, BllUser user, BllBlog blog)
        {
            PageNumber = pageNumber;
            MainModels = mainModels;
            UserId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            BlogTitle = blog.Title;
            BlogId = blog.Id;
        }
    }

    public class PostModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<BllTag> Tags { get; set; }

        public PostModel()
        {
        }

        public PostModel(BllPost post, IEnumerable<BllTag> tags)
        {
            PostId = post.Id;
            Title = post.Title;
            Content = post.Content;
            Tags = tags;
        }

        public PostModel(BllPost post, IEnumerable<BllTag> tags, int wordsInDescription, 
            Func<int, string, string> getDescription)
        {
            PostId = post.Id;
            Title = post.Title;
            Content = getDescription(wordsInDescription, post.Content);
            Tags = tags;
        }
    }

    public class PostViewModel : PostModel
    {
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public PostViewModel()
        {
        }

        public PostViewModel(BllPost post, IEnumerable<BllTag> tags, BllBlog blog, BllUser user):base(post, tags)
        {
            UserId = user.Id;
            BlogId = blog.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}