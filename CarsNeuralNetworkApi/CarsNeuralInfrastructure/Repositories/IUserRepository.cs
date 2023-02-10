using CarsNeuralCore.Dto;

namespace CarsNeuralInfrastructure.Repositories
{
    public interface IUserRepository
    {
        public Task<UserDto> LoginUser(LoginUserDto user);

        public Task<bool> RegisterUser(RegisterUserDto user);
    }
}