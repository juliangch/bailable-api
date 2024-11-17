using bailable_api.Database;
using bailable_api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace bailable_api.Service
{
    public interface IUserService
    {
        User GetUserById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly List<User> _users;
        private readonly UserDao userDao;

        public UserService(ContextDb contextDb)
        {
            userDao = new UserDao(contextDb);
            _users = new List<User>();
        }

        public User GetUserById(Guid id)
        {
            var user = userDao.GetUserById(id);
            return user;
        }
    }
}
