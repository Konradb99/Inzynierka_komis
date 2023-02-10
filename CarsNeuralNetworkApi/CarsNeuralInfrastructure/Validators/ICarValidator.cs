using CarsNeuralInfrastructure.Models;

namespace CarsNeuralInfrastructure.Validators
{
    public interface ICarValidator
    {
        public bool validateCar(Car newCar);
    }
}