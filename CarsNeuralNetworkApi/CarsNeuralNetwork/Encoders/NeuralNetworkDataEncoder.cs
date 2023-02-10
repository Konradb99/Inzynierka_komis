using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;

namespace CarsNeuralNetwork.Encoders
{
    public class NeuralNetworkDataEncoder : INeuralNetworkDataEncoder
    {
        public double[] getMinMax(ICollection<double[]> trainSet)
        {
            ICollection<double> bodyColumn = new List<double>();
            ICollection<double> driveColumn = new List<double>();
            ICollection<double> fuelColumn = new List<double>();
            ICollection<double> gearboxColumn = new List<double>();
            ICollection<double> priceColumn = new List<double>();
            ICollection<double> distanceColumn = new List<double>();
            ICollection<double> yearColumn = new List<double>();
            ICollection<double> capacityColumn = new List<double>();

            foreach (var carsColumn in trainSet)
            {
                bodyColumn.Add(carsColumn[0]);
                driveColumn.Add(carsColumn[1]);
                fuelColumn.Add(carsColumn[2]);
                gearboxColumn.Add(carsColumn[3]);
                priceColumn.Add(carsColumn[4]);
                distanceColumn.Add(carsColumn[5]);
                yearColumn.Add(carsColumn[6]);
                capacityColumn.Add(carsColumn[7]);
            }

            double minBody = bodyColumn.Min();
            double maxBody = bodyColumn.Max();
            double minDrive = driveColumn.Min();
            double maxDrive = driveColumn.Max();
            double minGearbox = gearboxColumn.Min();
            double maxGearbox = gearboxColumn.Max();
            double minFuel = fuelColumn.Min();
            double maxFuel = fuelColumn.Max();
            double minPrice = priceColumn.Min();
            double maxPrice = priceColumn.Max();
            double minDistance = distanceColumn.Min();
            double maxDistance = distanceColumn.Max();
            double minYear = yearColumn.Min();
            double maxYear = yearColumn.Max();
            double minCapacity = capacityColumn.Min();
            double maxCapacity = capacityColumn.Max();

            double[] result = new double[]
            {
                minBody, maxBody,
                minDrive, maxDrive,
                minGearbox, maxGearbox,
                minFuel, maxFuel,
                minPrice, maxPrice,
                minDistance, maxDistance,
                minYear, maxYear,
                minCapacity, maxCapacity
            };

            return result;
        }

        public IList<double[]> NormalizeCars(IList<double[]> carsArray, double[] minMaxValues)
        {
            foreach (var car in carsArray)
            {
                car[0] = (car[0] - minMaxValues[0]) / minMaxValues[1];
                car[1] = (car[1] - minMaxValues[2]) / minMaxValues[3];
                car[2] = (car[2] - minMaxValues[4]) / minMaxValues[5];
                car[3] = (car[3] - minMaxValues[6]) / minMaxValues[7];
                car[4] = (car[4] - minMaxValues[8]) / minMaxValues[9];
                car[5] = (car[5] - minMaxValues[10]) / minMaxValues[11];
                car[6] = (car[6] - minMaxValues[12]) / minMaxValues[13];
                car[7] = (car[7] - minMaxValues[14]) / minMaxValues[15];
            }

            return carsArray;
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

            if (carToEncode.Price == null || carToEncode.Price == "null")
            {
                carToEncode.Price = "0";
            }

            if (carToEncode.Distance == null || carToEncode.Distance != "null")
            {
                carToEncode.Distance = "0";
            }

            if (carToEncode.ProductionYear == null || carToEncode.ProductionYear != "null")
            {
                carToEncode.ProductionYear = "0";
            }

            if (carToEncode.Capacity == null || carToEncode.Capacity != "null")
            {
                carToEncode.Capacity = "0";
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

        public double[] EncodeCarForNeuralNetwork(CarDto carToEncode)
        {
            double[] encodedCar;
            int models = 5;
            // Values 0-1 for categorical data
            // Brand, Model, Type, Drive, Gearbox, Fuel
            int modelEncoded = 0, bodyEncoded = 0, driveEncoded = 0, gearboxEncoded = 0, fuelEncoded = 0;

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

            encodedCar = new double[8 + 25];

            encodedCar[0] = bodyEncoded;
            encodedCar[1] = driveEncoded;
            encodedCar[2] = gearboxEncoded;
            encodedCar[3] = fuelEncoded;
            encodedCar[4] = carToEncode.Price ?? 0;
            encodedCar[5] = carToEncode.Distance ?? 0;
            encodedCar[6] = carToEncode.ProductionYear ?? 0;
            encodedCar[7] = carToEncode.Capacity ?? 0;

            int k = 8;
            for (int i = 0; i < CarConstants.brand.Length; i++)
            {
                for (int j = 0; j < models; j++)
                {
                    if (CarConstants.brand[i] == carToEncode.Brand)
                    {
                        if (j == modelEncoded)
                        {
                            encodedCar[k] = 1;
                        }
                        else
                        {
                            encodedCar[k] = 0;
                        }
                    }
                    else
                    {
                        encodedCar[k] = 0;
                    }
                    k++;
                }
            }

            return encodedCar;
        }

        public string DecodeCarForNeuralNetwork(double[] car)
        {
            string prefferedClass = "";
            int indexOfCar = Array.IndexOf(car, car.Max());
            int brandInteger = (int)Math.Floor((double)(indexOfCar / CarConstants.brand.Length));
            int modelInteger = indexOfCar % CarConstants.brand.Length;
            string brand = CarConstants.brand[brandInteger];
            string model = "";

            switch (brand)
            {
                case "Audi":
                    model = CarConstants.modelAudi[modelInteger];
                    break;

                case "Ford":
                    model = CarConstants.modelFord[modelInteger];
                    break;

                case "Opel":
                    model = CarConstants.modelOpel[modelInteger];
                    break;

                case "BMW":
                    model = CarConstants.modelBmw[modelInteger];
                    break;

                case "Peugeot":
                    model = CarConstants.modelPeugeot[modelInteger];
                    break;
            }

            prefferedClass = $"{brand} {model}";
            return prefferedClass;
        }
    }
}