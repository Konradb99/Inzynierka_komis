using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;

namespace CarsNeuralKNN.Encoders
{
    public class DataEncoder : IDataEncoder
    {
        public double[] EncodeCarWithFilters(CarDto carToEncode)
        {
            double[] encodedCar;

            encodedCar = new double[]
                {
                carToEncode.Price ?? 0,
                carToEncode.Distance ?? 0,
                carToEncode.ProductionYear ?? 0,
                carToEncode.Capacity ?? 0
                };

            return encodedCar;
        }

        public double[] EncodeCar(CarDto carToEncode)
        {
            double[] encodedCar;

            // Values 0-1 for categorical data
            // Brand, Model, Type, Drive, Gearbox, Fuel
            int modelEncoded = 0, brandEncoded = 0, bodyEncoded = 0, driveEncoded = 0, gearboxEncoded = 0, fuelEncoded = 0;

            if (carToEncode.Brand != "null" && carToEncode.Brand != null)
            {
                brandEncoded = Array.IndexOf(CarConstants.brand, carToEncode.Brand);
            }
            if (carToEncode.BodyType != "null" && carToEncode.BodyType != null)
            {
                bodyEncoded = Array.IndexOf(CarConstants.bodies, carToEncode.BodyType);
            }
            if (carToEncode.DriveType != "null" && carToEncode.DriveType != null)
            {
                driveEncoded = Array.IndexOf(CarConstants.drives, carToEncode.DriveType);
            }
            if (carToEncode.GearboxType != "null" && carToEncode.GearboxType != null)
            {
                gearboxEncoded = Array.IndexOf(CarConstants.gearboxes, carToEncode.GearboxType);
            }
            if (carToEncode.FuelType != "null" && carToEncode.FuelType != null)
            {
                fuelEncoded = Array.IndexOf(CarConstants.fuels, carToEncode.FuelType);
            }

            switch (carToEncode.Brand)
            {
                case "Audi:":
                    if (carToEncode.Model != "null" && carToEncode.Model != null)
                    {
                        modelEncoded = Array.IndexOf(CarConstants.modelAudi, carToEncode.Model);
                    }
                    break;

                case "Ford":
                    if (carToEncode.Model != "null" && carToEncode.Model != null)
                    {
                        modelEncoded = Array.IndexOf(CarConstants.modelFord, carToEncode.Model);
                    }
                    break;

                case "Opel":
                    if (carToEncode.Model != "null" && carToEncode.Model != null)
                    {
                        modelEncoded = Array.IndexOf(CarConstants.modelOpel, carToEncode.Model);
                    }
                    break;

                case "BMW":
                    if (carToEncode.Model != "null" && carToEncode.Model != null)
                    {
                        modelEncoded = Array.IndexOf(CarConstants.modelBmw, carToEncode.Model);
                    }
                    break;

                case "Peugeot":
                    if (carToEncode.Model != "null" && carToEncode.Model != null)
                    {
                        modelEncoded = Array.IndexOf(CarConstants.modelPeugeot, carToEncode.Model);
                    };
                    break;
            }

            encodedCar = new double[]
                { brandEncoded,
                modelEncoded,
                bodyEncoded,
                driveEncoded,
                gearboxEncoded,
                fuelEncoded,
                carToEncode.Price ?? 0,
                carToEncode.Distance ?? 0,
                carToEncode.ProductionYear ?? 0,
                carToEncode.Capacity ?? 0
                };

            return encodedCar;
        }
        public double[] EncodeCarToPredict(PredictDto carToEncode)
        {
            double[] encodedCar;

            int bodyEncoded = 0, driveEncoded = 0, gearboxEncoded = 0, fuelEncoded = 0;
            if (carToEncode.BodyType != "null" && carToEncode.BodyType != null)
            {
                bodyEncoded = Array.IndexOf(CarConstants.bodies, carToEncode.BodyType);
            }
            if (carToEncode.DriveType != "null" && carToEncode.DriveType != null)
            {
                driveEncoded = Array.IndexOf(CarConstants.drives, carToEncode.DriveType);
            }
            if (carToEncode.GearboxType != "null" && carToEncode.GearboxType != null)
            {
                gearboxEncoded = Array.IndexOf(CarConstants.gearboxes, carToEncode.GearboxType);
            }
            if (carToEncode.FuelType != "null" && carToEncode.FuelType != null)
            {
                fuelEncoded = Array.IndexOf(CarConstants.fuels, carToEncode.FuelType);
            }

            encodedCar = new double[]
                {
                bodyEncoded,
                driveEncoded,
                gearboxEncoded,
                fuelEncoded,
                int.Parse(carToEncode.Price),
                int.Parse(carToEncode.Distance),
                int.Parse(carToEncode.ProductionYear),
                int.Parse(carToEncode.Capacity)
                };

            return encodedCar;
        }

        public double[] EncodeCarToPredictWithFilters(PredictDto carToEncode)
        {
            double[] encodedCar;

            encodedCar = new double[]
                {
                int.Parse(carToEncode.Price),
                int.Parse(carToEncode.Distance),
                int.Parse(carToEncode.ProductionYear),
                int.Parse(carToEncode.Capacity)
                };

            return encodedCar;
        }
    }
}