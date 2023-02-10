using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;

namespace CarsNeuralInfrastructure.Repositories
{
    public interface IFiltersRepository
    {
        public Task<IEnumerable<Car>> filterCars(CarFiltersDto filters);
    }
}