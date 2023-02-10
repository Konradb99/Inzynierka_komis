using CarsNeuralCore.Dto;

namespace CarsNeuralKNN.Encoders
{
    public interface IDataNormalizer
    {
        public IList<double[]> NormalizeCars(IList<double[]> carsArray, double[] minMaxValues);

        public IList<double[]> NormalizeCarsWithFilters(IList<double[]> carsArray, double[] minMaxValues);

        public double[] NormalizeCar(PredictDto carToNormalize, double[] minMaxValues);

        public double[] NormalizeCarWithFilters(PredictDto carToNormalize, double[] minMaxValues);
    }
}