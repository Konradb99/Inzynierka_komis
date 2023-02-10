using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;
using CarsNeuralInfrastructure.Repositories;
using CarsNeuralInfrastructure.Validators;

namespace CarsNeuralApplication.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _repository;
        private readonly ICarValidator _validator;

        public CarsService(ICarsRepository repository, ICarValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ICollection<CarDto>> GetAllCars()
        {
            return await _repository.GetAllCars();
        }

        public async Task<CarPageDto> GetCarsByPage(int pageNumber)
        {
            return await _repository.GetCarsByPage(pageNumber);
        }

        public async Task<CarPageDto> GetFilteredCarsByPage(int pageNumber, CarFiltersDto filters)
        {
            return await _repository.GetFilteredCarsByPage(pageNumber, filters);
        }

        public async Task<Car> InsertCar(Car newCar)
        {
            if (_validator.validateCar(newCar))
            {
                return await _repository.InsertCar(newCar);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.BadCarException);
            }
        }

        public async Task<int> GetCounter()
        {
            return await _repository.GetCounter();
        }

        public async Task<CarDto> SellCar(int carId)
        {
            return await _repository.SellCar(carId);
        }

        public async Task<CarDto> RemoveCar(int carId)
        {
            return await _repository.RemoveCar(carId);
        }
    }
}