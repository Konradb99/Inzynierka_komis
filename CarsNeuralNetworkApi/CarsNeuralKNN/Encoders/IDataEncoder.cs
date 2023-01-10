using CarsNeuralCore.Dto;

namespace CarsNeuralKNN.Encoders
{
    public interface IDataEncoder
    {
        public double[] EncodeCar(CarDto car);

        public double[] EncodeCarWithFilters(CarDto car);

        public double[] EncodeCarToPredictWithFilters(PredictDto carToEncode);

        public double[] EncodeCarToPredict(PredictDto carToEncode);
    }
}