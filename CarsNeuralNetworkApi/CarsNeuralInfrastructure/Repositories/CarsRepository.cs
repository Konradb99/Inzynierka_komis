using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Mappers;
using CarsNeuralInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;

namespace CarsNeuralInfrastructure.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private readonly CarsNeuralDbContext _dbContext;
        private readonly IFiltersRepository _filtersRepository;

        public CarsRepository(
            CarsNeuralDbContext dbContext,
            IFiltersRepository filtersRepository)
        {
            _dbContext = dbContext;
            _filtersRepository = filtersRepository;
        }

        public async Task<ICollection<CarDto>> GetAllCars()
        {
            ICollection<Car> cars = _dbContext.Cars.ToList();
            ICollection<CarDto> carsToReturn = new List<CarDto>();
            foreach (var car in cars)
            {
                carsToReturn.Add(CarsMapper.CarDtoMapper(car));
            }
            return carsToReturn;
        }

        public async Task<CarPageDto> GetCarsByPage(int pageNumber)
        {
            ICollection<Car> cars = await _dbContext.Cars.Where(c => c.IsSold == false).OrderByDescending(c => c.Id).ToListAsync();

            return createReturnPage(cars, pageNumber);
        }

        public async Task<CarPageDto> GetFilteredCarsByPage(int pageNumber, CarFiltersDto filters)
        {
            IEnumerable<Car> carsFiltered = await _filtersRepository.filterCars(filters);

            return createReturnPage(carsFiltered, pageNumber);
        }

        public async Task<Car> InsertCar(Car newCar)
        {
            Car addedCar = _dbContext.Cars.Add(newCar).Entity;
            await _dbContext.SaveChanges();
            return addedCar;
        }

        public Task<int> GetCounter()
        {
            int counter = _dbContext.Cars.Count();

            return Task.FromResult(counter);
        }

        private CarPageDto createReturnPage(IEnumerable<Car> carsFiltered, int pageNumber)
        {
            int skipSize = (pageNumber - 1) * PageConstants.pageSize;

            ICollection<CarDto> carsToReturn = new List<CarDto>();
            foreach (var car in carsFiltered)
            {
                carsToReturn.Add(CarsMapper.CarDtoMapper(car));
            }
            CarPageDto result = new CarPageDto
            {
                PageNumber = pageNumber,
                Data = carsToReturn.Skip(skipSize).Take(PageConstants.pageSize).ToList(),
            };

            return result;
        }

        public async Task<CarDto> RemoveCar(int carId)
        {
            Car carToRemove = _dbContext.Cars.Where(c => c.Id == carId).FirstOrDefault();

            if (carToRemove != null)
            {
                _dbContext.Cars.Remove(carToRemove);
                await _dbContext.SaveChangesAsync();
                return CarsMapper.CarDtoMapper(carToRemove);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.NoCarByIdException);
            }
        }

        public async Task<CarDto> SellCar(int carId)
        {
            Car carToSell = _dbContext.Cars.Where(c => c.Id == carId).FirstOrDefault();

            if (carToSell != null)
            {
                carToSell.IsSold = true;
                _dbContext.Cars.Update(carToSell);
                await _dbContext.SaveChangesAsync();
                return CarsMapper.CarDtoMapper(carToSell);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.NoCarByIdException);
            }
        }

        public async Task<CarDto> GetCarFromTrainById(int carId)
        {
            CarTrain car = _dbContext.TrainSet.Where(car => car.Id == carId).FirstOrDefault();

            CarDto result = CarsMapper.CarTrainDtoMapper(car);

            return result;
        }

        public async Task<CarDto> GetCarByTrainData(CarDto carToSellFromTrainSet)
        {
            Car car = _dbContext.Cars.Where(c =>
                c.Brand == carToSellFromTrainSet.Brand
                && c.Model == carToSellFromTrainSet.Model
                && c.BodyType == carToSellFromTrainSet.BodyType
                && c.GearboxType == carToSellFromTrainSet.GearboxType
                && c.FuelType == carToSellFromTrainSet.FuelType
                && c.DriveType == carToSellFromTrainSet.DriveType
                && c.Price == carToSellFromTrainSet.Price
                && c.Distance == carToSellFromTrainSet.Distance
                && c.Capacity == carToSellFromTrainSet.Capacity
                && c.ProductionYear == carToSellFromTrainSet.ProductionYear
            ).FirstOrDefault();

            if (car != null)
            {
                return CarsMapper.CarDtoMapper(car);
            }
            else
            {
                return null;
            }
        }
    }
}