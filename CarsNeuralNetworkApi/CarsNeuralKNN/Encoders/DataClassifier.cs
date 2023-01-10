namespace CarsNeuralKNN.Encoders
{
    public class DataClassifier : IDataClassifier
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
                bodyColumn.Add(carsColumn[2]);
                driveColumn.Add(carsColumn[3]);
                fuelColumn.Add(carsColumn[4]);
                gearboxColumn.Add(carsColumn[5]);
                priceColumn.Add(carsColumn[6]);
                distanceColumn.Add(carsColumn[7]);
                yearColumn.Add(carsColumn[8]);
                capacityColumn.Add(carsColumn[9]);
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

        public double[] getMinMaxWithFilters(ICollection<double[]> trainSet)
        {
            ICollection<double> priceColumn = new List<double>();
            ICollection<double> distanceColumn = new List<double>();
            ICollection<double> yearColumn = new List<double>();
            ICollection<double> capacityColumn = new List<double>();

            foreach (var carsColumn in trainSet)
            {
                priceColumn.Add(carsColumn[0]);
                distanceColumn.Add(carsColumn[1]);
                yearColumn.Add(carsColumn[2]);
                capacityColumn.Add(carsColumn[3]);
            }

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
                minPrice, maxPrice,
                minDistance, maxDistance,
                minYear, maxYear,
                minCapacity, maxCapacity
            };

            return result;
        }
    }
}