using BLL.Entities;
using DAL.DTO;

namespace BLL.Mappers
{
    public static class Mappers
    {
        #region User mapper

        public static DalUser ToDal(this BllUser user)
        {
            return new DalUser()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = user.RoleId,
                Email = user.Email,
                Password = user.Password
            };
        }

        public static BllUser ToBll(this DalUser user)
        {
            return new BllUser()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = user.RoleId,
                Email = user.Email,
                Password = user.Password
            };
        }

        #endregion

        #region Blog mapper

        public static DalBlog ToDal(this BllBlog blog)
        {
            return new DalBlog
            {
                Id = blog.Id,
                Title = blog.Title,
                UserId = blog.UserId
            };
        }

        public static BllBlog ToBll(this DalBlog blog)
        {
            return new BllBlog
            {
                Id = blog.Id,
                Title = blog.Title,
                UserId = blog.UserId
            };
        }

        #endregion

        #region Comment mapper

        public static DalComment ToDal(this BllComment comment)
        {
            return new DalComment
            {
                Id = comment.Id,
                Content = comment.Content,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
        }

        public static BllComment ToBll(this DalComment comment)
        {
            return new BllComment
            {
                Id = comment.Id,
                Content = comment.Content,
                PostId = comment.PostId,
                UserId = comment.UserId
            };
        }

        #endregion

        #region Post mapper

        public static DalPost ToDal(this BllPost post)
        {
            return new DalPost
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title,
                BlogId = post.BlogId
            };
        }

        public static BllPost ToBll(this DalPost post)
        {
            return new BllPost
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title,
                BlogId = post.BlogId
            };
        }

        #endregion

        #region Role mapper

        public static DalRole ToDal(this BllRole role)
        {
            return new DalRole
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static BllRole ToBll(this DalRole role)
        {
            return new BllRole
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        #endregion

        #region Tag mapper

        public static DalTag ToDal(this BllTag tag)
        {
            return new DalTag
            {
                Id = tag.Id,
                Name = tag.Name,
                PostId = tag.PostId
            };
        }

        public static BllTag ToBll(this DalTag tag)
        {
            return new BllTag
            {
                Id = tag.Id,
                Name = tag.Name,
                PostId = tag.PostId
            };
        }

        #endregion
    }
}