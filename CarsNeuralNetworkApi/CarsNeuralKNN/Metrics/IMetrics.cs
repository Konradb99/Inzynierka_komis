namespace CarsNeuralKNN.Metrics
{
    public interface IMetrics
    {
        public List<double> MinkowskiMetric(IList<double[]> data, double[] target);

        public List<double> MinkowskiMetricWithFilters(IList<double[]> data, double[] target);
    }
}