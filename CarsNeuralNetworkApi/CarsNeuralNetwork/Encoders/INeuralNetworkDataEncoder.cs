using CarsNeuralCore.Dto;

namespace CarsNeuralNetwork.Encoders
{
    public interface INeuralNetworkDataEncoder
    {
        public double[] EncodeCarForNeuralNetwork(CarDto car);

        public string DecodeCarForNeuralNetwork(double[] car);

        public double[] getMinMax(ICollection<double[]> trainSet);

        public IList<double[]> NormalizeCars(IList<double[]> cars, double[] minMaxValues);

        public double[] EncodeCarToPredict(PredictDto carToEncode);

    }
}