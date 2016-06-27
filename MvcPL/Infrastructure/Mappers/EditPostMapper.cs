using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BLL.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class EditPostMapper
    {
        public static IEnumerable<BllTag> ToTags(this EditPostModel post)
        {
            string tags = post.Tags ?? "";

            var matches = Regex.Matches(tags, @"#{1,1}[\w]+");

            var bllTags = new List<BllTag>();

            foreach (Match match in matches)
            {
                bllTags.Add(new BllTag {Name = match.Value, PostId = post.Id});
            }

            return bllTags;
        }

        public static BllPost ToPost(this EditPostModel post)
        {
            return new BllPost
            {
                BlogId = post.BlogId,
                Content = post.Content,
                Id = post.Id,
                Title = post.Title
            };
        }

        public static EditPostModel ToEditPost(this BllPost post, IEnumerable<BllTag> tags)
        {
            var stringTags = tags.Select(x => x.Name);
            string tagsConcat = stringTags.Aggregate((current, tag) => current + " " + tag);
            return new EditPostModel
            {
                BlogId = post.BlogId,
                Content = post.Content,
                Id = post.Id,
                Tags = tagsConcat,
                Title = post.Title
            };
        }
    }
}