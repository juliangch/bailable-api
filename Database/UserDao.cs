using bailable_api.Models;

namespace bailable_api.Database
{
    public class UserDao
    {
        private readonly ContextDb _dbContext;

        public UserDao(ContextDb dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserById(Guid id) 
        {
            return _dbContext.Users.FirstOrDefault(user => user.UserId == id);
        }

    }
}
