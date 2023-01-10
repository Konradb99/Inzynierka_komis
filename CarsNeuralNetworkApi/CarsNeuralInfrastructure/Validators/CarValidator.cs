using CarsNeuralCore.Constants;
using CarsNeuralInfrastructure.Models;

namespace CarsNeuralInfrastructure.Validators
{
    public class CarValidator : ICarValidator
    {
        public bool validateCar(Car newCar)
        {
            validateBrand(newCar.Brand);
            validateModel(newCar.Brand, newCar.Model);
            validateGearbox(newCar.GearboxType);
            validateDrive(newCar.DriveType);
            validateBody(newCar.BodyType);
            validateFuel(newCar.FuelType);
            validateDistance(newCar.Distance);
            validatePrice(newCar.Price);
            validateYear(newCar.ProductionYear);
            validateCapacity(newCar.Capacity);

            return true;
        }

        private void validateFuel(string fuelType)
        {
            if (!CarConstants.fuels.Contains(fuelType))
            {
                throw new ArgumentException(ErrorMessages.BadFuelException);
            }
        }

        private void validateGearbox(string gearboxType)
        {
            if (!CarConstants.gearboxes.Contains(gearboxType))
            {
                throw new ArgumentException(ErrorMessages.BadGearboxException);
            }
        }

        private void validateDrive(string driveType)
        {
            if (!CarConstants.drives.Contains(driveType))
            {
                throw new ArgumentException(ErrorMessages.BadDriveException);
            }
        }

        private void validateBody(string bodyType)
        {
            if (!CarConstants.bodies.Contains(bodyType))
            {
                throw new ArgumentException(ErrorMessages.BadBodyException);
            }
        }

        private void validateDistance(int distance)
        {
            if (distance < 0)
            {
                throw new ArgumentException(ErrorMessages.BadMileageException);
            }
        }

        private void validatePrice(int price)
        {
            if (price < 0)
            {
                throw new ArgumentException(ErrorMessages.BadPriceException);
            }
        }

        private void validateYear(int productionYear)
        {
            if (productionYear < 0)
            {
                throw new ArgumentException(ErrorMessages.BadYearException);
            }
            else if (productionYear > DateTime.Now.Year)
            {
                throw new ArgumentException(ErrorMessages.BadYearException);
            }
        }

        private void validateCapacity(double capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException(ErrorMessages.BadCapacityException);
            }
        }

        private void validateModel(string brand, string model)
        {
            switch (brand)
            {
                case "Audi":
                    if (!CarConstants.modelAudi.Contains(model))
                    {
                        throw new ArgumentException(ErrorMessages.BadModelException);
                    }
                    break;

                case "Ford":
                    if (!CarConstants.modelFord.Contains(model))
                    {
                        throw new ArgumentException(ErrorMessages.BadModelException);
                    }
                    break;

                case "Opel":
                    if (!CarConstants.modelOpel.Contains(model))
                    {
                        throw new ArgumentException(ErrorMessages.BadModelException);
                    }
                    break;

                case "BMW":
                    if (!CarConstants.modelBmw.Contains(model))
                    {
                        throw new ArgumentException(ErrorMessages.BadModelException);
                    }
                    break;

                case "Peugeot":
                    if (!CarConstants.modelPeugeot.Contains(model))
                    {
                        throw new ArgumentException(ErrorMessages.BadModelException);
                    }
                    break;

                default:
                    throw new ArgumentException(ErrorMessages.BadBrandException);
            };
        }

        private void validateBrand(string brand)
        {
            if (!CarConstants.brand.Contains(brand))
            {
                throw new ArgumentException(ErrorMessages.BadBrandException);
            }
        }
    }
}