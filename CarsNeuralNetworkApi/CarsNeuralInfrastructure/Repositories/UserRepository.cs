using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Entities;

namespace CarsNeuralInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarsNeuralDbContext _dbContext;

        public UserRepository(CarsNeuralDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> LoginUser(LoginUserDto user)
        {
            User UserToLogin = _dbContext
                .Users
                .FirstOrDefault(p =>
                p.Username == user.Username
                && p.Password == user.Password);

            if (UserToLogin == null)
            {
                throw new Exception(ErrorMessages.NotAuthorizedException);
            }
            else
            {
                UserDto LoggedUser = new UserDto { Username = UserToLogin.Username, Role = UserToLogin.Role };
                return LoggedUser;
            }
        }

        public async Task<bool> RegisterUser(RegisterUserDto user)
        {
            if (user.password != user.repeatPassword)
            {
                throw new Exception(ErrorMessages.DifferentPasswordsException);
            }
            else
            {
                if (_dbContext.Users.FirstOrDefault(p => p.Username == user.username) != null)
                {
                    throw new Exception(ErrorMessages.UserAlreadyExists);
                }
                _dbContext
                    .Users
                    .Add(new User { Password = user.password, Username = user.username, Role = UserConstants.defaultUserRole });
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}