using BLL.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class CommentViewMapper
    {
        public static CommentViewModel ToViewModel(this BllComment comment, BllUser user)
        {
            return new CommentViewModel()
            {
                Content = comment.Content,
                Id = comment.Id,
                PostId = comment.PostId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id
            };
        }

        public static BllComment ToBllComment(this CommentViewModel comment)
        {
            return new BllComment()
            {
                Content = comment.Content,
                Id = comment.Id,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
        }
    }
}