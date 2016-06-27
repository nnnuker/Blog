using BLL.Entities;

namespace BLL.Interfaces
{
    public interface IUserService : IService<BllUser>
    {
        BllUser Get(string email);
    }
}