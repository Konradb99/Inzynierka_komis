using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Repositories;

namespace CarsNeuralApplication.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IUserRepository _repository;

        public CurrentUserService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async Task<UserDto> LoginUser(LoginUserDto user)
        {
            return await _repository.LoginUser(user);
        }

        public async Task<bool> RegisterUser(RegisterUserDto user)
        {
            return await _repository.RegisterUser(user);
        }
    }
}