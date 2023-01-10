using CarsNeuralApplication.Services;
using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarsNeuralWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class CarsController : Controller
    {
        private readonly ICarsService _service;

        public CarsController(ICarsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<Car> InsertCar([FromBody] Car newCar)
        {
            return await _service.InsertCar(newCar);
        }

        [HttpGet]
        public async Task<ICollection<CarDto>> Get()
        {
            return await _service.GetAllCars();
        }

        [HttpGet]
        public async Task<CarPageDto> GetByPage(int pageNumber)
        {
            return await _service.GetCarsByPage(pageNumber);
        }

        [HttpGet]
        public async Task<CarPageDto> GetFilteredCars(int pageNumber, [FromQuery] CarFiltersDto filters)
        {
            return await _service.GetFilteredCarsByPage(pageNumber, filters);
        }

        [HttpGet]
        public async Task<int> GetCounter()
        {
            return await _service.GetCounter();
        }

        [HttpDelete]
        public async Task<CarDto> RemoveCar([FromBody] int carId)
        {
            return await _service.RemoveCar(carId);
        }

        [HttpPut]
        public async Task<CarDto> SellCar([FromBody] int carId)
        {
            return await _service.SellCar(carId);
        }
    }
}