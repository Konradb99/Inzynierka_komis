using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;

namespace CarsNeuralApplication.Services
{
    public interface ICarsService
    {
        public Task<Car> InsertCar(Car newCar);

        public Task<ICollection<CarDto>> GetAllCars();

        public Task<CarPageDto> GetCarsByPage(int pageNumber);

        public Task<CarPageDto> GetFilteredCarsByPage(int pageNumber, CarFiltersDto filters);

        public Task<int> GetCounter();

        public Task<CarDto> SellCar(int carId);

        public Task<CarDto> RemoveCar(int carId);
    }
}