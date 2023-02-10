using CarsNeuralCore.Dto;

namespace CarsNeuralApplication.Services
{
    public interface ICurrentUserService
    {
        public Task<UserDto> LoginUser(LoginUserDto user);

        public Task<bool> RegisterUser(RegisterUserDto user);
    }
}