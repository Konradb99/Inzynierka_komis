using CarsNeuralApplication.Services;
using CarsNeuralCore.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarsNeuralWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly ICurrentUserService _service;

        public UserController(ICurrentUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<UserDto> LoginUser([FromQuery] LoginUserDto user)
        {
            return await _service.LoginUser(user);
        }

        [HttpPost]
        public async Task<bool> RegisterUser([FromBody] RegisterUserDto user)
        {
            return await _service.RegisterUser(user);
        }
    }
}