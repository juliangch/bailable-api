using bailable_api.Dtos;
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

        public int CreateUser(RegisterUserRequestDto registerUserRequestDto)
        {
            User newUser = new User()
            {
                Name = registerUserRequestDto.Name,
                Email = registerUserRequestDto.Email,
                Password = registerUserRequestDto.Password,
                Role = registerUserRequestDto.Role,
                Surname = registerUserRequestDto.Surname,
            };
            _dbContext.Users.Add(newUser);
            return _dbContext.SaveChanges();
        }

        public User AuthenticateUser(AuthenticateUserRequestDto authUserDto) 
        {
            return _dbContext.Users.First(user => user.Email == authUserDto.Email && user.Password == authUserDto.Password);
        }
    }
}
