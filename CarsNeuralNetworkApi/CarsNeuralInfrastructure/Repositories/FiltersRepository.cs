using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsNeuralInfrastructure.Repositories
{
    public class FiltersRepository : IFiltersRepository
    {
        private readonly CarsNeuralDbContext _dbContext;

        public FiltersRepository(CarsNeuralDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Car>> filterCars(CarFiltersDto filters)
        {
            IEnumerable<Car> cars = await _dbContext.Cars.Where(c => c.IsSold == false).OrderByDescending(c => c.Id).ToListAsync();
            IEnumerable<Car> carsFiltered = filterByNumerical(cars, filters);
            carsFiltered = filterByCategorical(carsFiltered, filters);

            return carsFiltered;
        }

        private IEnumerable<Car> filterByCategorical(IEnumerable<Car> carsFiltered, CarFiltersDto filters)
        {
            if (filters.BodyType != null && filters.BodyType != "null")
            {
                carsFiltered = carsFiltered.Where(p => p.BodyType == filters.BodyType).ToList();
            }

            if (filters.GearboxType != null && filters.GearboxType != "null")
            {
                carsFiltered = carsFiltered.Where(p => p.GearboxType == filters.GearboxType).ToList();
            }

            if (filters.Brand != null && filters.Brand != "null")
            {
                carsFiltered = carsFiltered.Where(p => p.Brand == filters.Brand).ToList();
            }

            if (filters.Model != null && filters.Model != "null")
            {
                carsFiltered = carsFiltered.Where(p => p.Model == filters.Model).ToList();
            }

            if (filters.FuelType != null && filters.FuelType != "null")
            {
                carsFiltered = carsFiltered.Where(p => p.FuelType == filters.FuelType).ToList();
            }

            if (filters.DriveType != null && filters.DriveType != "null")
            {
                carsFiltered = carsFiltered.Where(p => p.DriveType == filters.DriveType).ToList();
            }

            return carsFiltered;
        }

        private IEnumerable<Car> filterByNumerical(IEnumerable<Car> cars, CarFiltersDto filters)
        {
            if (filters.DistanceMax == null || filters.DistanceMax == "null" || filters.DistanceMax == "0")
            {
                filters.DistanceMax = cars.Max(p => p.Distance).ToString();
            }

            if (filters.DistanceMin == null || filters.DistanceMin == "null")
            {
                filters.DistanceMin = cars.Min(p => p.Distance).ToString();
            }

            if (filters.PriceMax == null || filters.PriceMax == "null" || filters.PriceMax == "0")
            {
                filters.PriceMax = cars.Max(p => p.Price).ToString();
            }

            if (filters.PriceMin == null || filters.PriceMin == "null")
            {
                filters.PriceMin = cars.Min(p => p.Price).ToString();
            }

            if (filters.CapacityMax == null || filters.CapacityMax == "null" || filters.CapacityMax == "0")
            {
                filters.CapacityMax = cars.Max(p => p.Capacity).ToString();
            }

            if (filters.CapacityMin == null || filters.CapacityMin == "null")
            {
                filters.CapacityMin = cars.Min(p => p.Capacity).ToString();
            }

            if (filters.ProductionYearMax == null || filters.ProductionYearMax == "null" || filters.ProductionYearMax == "0")
            {
                filters.ProductionYearMax = cars.Max(p => p.ProductionYear).ToString();
            }

            if (filters.ProductionYearMin == null || filters.ProductionYearMin == "null")
            {
                filters.ProductionYearMin = cars.Min(p => p.ProductionYear).ToString();
            }

            IEnumerable<Car> carsFiltered = cars.Where(p =>
                p.Distance >= int.Parse(filters.DistanceMin) &&
                p.Distance <= int.Parse(filters.DistanceMax) &&
                p.Capacity >= int.Parse(filters.CapacityMin) &&
                p.Capacity <= int.Parse(filters.CapacityMax) &&
                p.Price >= int.Parse(filters.PriceMin) &&
                p.Price <= int.Parse(filters.PriceMax) &&
                p.ProductionYear >= int.Parse(filters.ProductionYearMin) &&
                p.ProductionYear <= int.Parse(filters.ProductionYearMax)
            ).ToList();

            return carsFiltered;
        }
    }
}