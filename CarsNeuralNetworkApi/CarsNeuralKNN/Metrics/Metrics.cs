namespace CarsNeuralKNN.Metrics
{
    public class Metrics : IMetrics
    {
        public List<double> MinkowskiMetric(IList<double[]> data, double[] target)
        {
            List<double> result = new List<double>();

            foreach (var dataItem in data)
            {
                double distance = 0;
                distance += Math.Pow(Math.Abs(dataItem[2] - target[0]), 2);
                distance += Math.Pow(Math.Abs(dataItem[3] - target[1]), 2);
                distance += Math.Pow(Math.Abs(dataItem[4] - target[2]), 2);
                distance += Math.Pow(Math.Abs(dataItem[5] - target[3]), 2);
                distance += Math.Pow(Math.Abs(dataItem[6] - target[4]), 2);
                distance += Math.Pow(Math.Abs(dataItem[7] - target[5]), 2);
                distance += Math.Pow(Math.Abs(dataItem[8] - target[6]), 2);
                distance += Math.Pow(Math.Abs(dataItem[9] - target[7]), 2);
                distance = Math.Sqrt(distance);
                result.Add(distance);
            }

            return result;
        }

        public List<double> MinkowskiMetricWithFilters(IList<double[]> data, double[] target)
        {
            List<double> result = new List<double>();

            foreach (var dataItem in data)
            {
                double distance = 0;
                distance += Math.Pow(Math.Abs(dataItem[0] - target[0]), 2);
                distance += Math.Pow(Math.Abs(dataItem[1] - target[1]), 2);
                distance += Math.Pow(Math.Abs(dataItem[2] - target[2]), 2);
                distance += Math.Pow(Math.Abs(dataItem[3] - target[3]), 2);
                distance = Math.Sqrt(distance);
                result.Add(distance);
            }

            return result;
        }
    }
}