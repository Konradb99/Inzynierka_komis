using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;

namespace CarsNeuralInfrastructure.Mappers
{
    public static class CarsMapper
    {
        public static CarDto CarDtoMapper(Car car)
        {
            CarDto mappedCar = new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                BodyType = car.BodyType,
                DriveType = car.DriveType,
                GearboxType = car.GearboxType,
                FuelType = car.FuelType,
                Price = car.Price,
                Distance = car.Distance,
                ProductionYear = car.ProductionYear,
                Capacity = car.Capacity,
                IsSold = car.IsSold
            };

            return mappedCar;
        }

        public static CarDto CarTestDtoMapper(CarTest car)
        {
            CarDto mappedCar = new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                BodyType = car.BodyType,
                DriveType = car.DriveType,
                GearboxType = car.GearboxType,
                FuelType = car.FuelType,
                Price = car.Price,
                Distance = car.Distance,
                ProductionYear = car.ProductionYear,
                Capacity = car.Capacity,
                IsSold = false
            };

            return mappedCar;
        }

        public static CarDto CarTrainDtoMapper(CarTrain car)
        {
            CarDto mappedCar = new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                BodyType = car.BodyType,
                DriveType = car.DriveType,
                GearboxType = car.GearboxType,
                FuelType = car.FuelType,
                Price = car.Price,
                Distance = car.Distance,
                ProductionYear = car.ProductionYear,
                Capacity = car.Capacity,
                IsSold = false
            };

            return mappedCar;
        }
    }
}