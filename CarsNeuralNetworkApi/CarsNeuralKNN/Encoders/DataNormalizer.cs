using CarsNeuralCore.Dto;

namespace CarsNeuralKNN.Encoders
{
    public class DataNormalizer : IDataNormalizer
    {
        private readonly IDataEncoder _dataEncoder;

        public DataNormalizer(IDataEncoder dataEncoder)
        {
            _dataEncoder = dataEncoder;
        }

        public IList<double[]> NormalizeCars(IList<double[]> carsArray, double[] minMaxValues)
        {
            foreach (var car in carsArray)
            {
                car[2] = (car[2] - minMaxValues[0]) / minMaxValues[1];
                car[3] = (car[3] - minMaxValues[2]) / minMaxValues[3];
                car[4] = (car[4] - minMaxValues[4]) / minMaxValues[5];
                car[5] = (car[5] - minMaxValues[6]) / minMaxValues[7];
                car[6] = (car[6] - minMaxValues[8]) / minMaxValues[9];
                car[7] = (car[7] - minMaxValues[10]) / minMaxValues[11];
                car[8] = (car[8] - minMaxValues[12]) / minMaxValues[13];
                car[9] = (car[9] - minMaxValues[14]) / minMaxValues[15];
            }

            return carsArray;
        }

        public IList<double[]> NormalizeCarsWithFilters(IList<double[]> carsArray, double[] minMaxValues)
        {
            foreach (var car in carsArray)
            {
                car[0] = (car[0] - minMaxValues[0]) / minMaxValues[1];
                car[1] = (car[1] - minMaxValues[2]) / minMaxValues[3];
                car[2] = (car[2] - minMaxValues[4]) / minMaxValues[5];
                car[3] = (car[3] - minMaxValues[6]) / minMaxValues[7];
            }

            return carsArray;
        }

        public double[] NormalizeCar(PredictDto carToNormalize, double[] minMaxValues)
        {
            double[] encodedCar = _dataEncoder.EncodeCarToPredict(carToNormalize);

            encodedCar[0] = (encodedCar[0] - minMaxValues[0]) / minMaxValues[1];
            encodedCar[1] = (encodedCar[1] - minMaxValues[2]) / minMaxValues[3];
            encodedCar[2] = (encodedCar[2] - minMaxValues[4]) / minMaxValues[5];
            encodedCar[3] = (encodedCar[3] - minMaxValues[6]) / minMaxValues[7];
            encodedCar[4] = (encodedCar[4] - minMaxValues[8]) / minMaxValues[9];
            encodedCar[5] = (encodedCar[5] - minMaxValues[10]) / minMaxValues[11];
            encodedCar[6] = (encodedCar[6] - minMaxValues[12]) / minMaxValues[13];
            encodedCar[7] = (encodedCar[7] - minMaxValues[14]) / minMaxValues[15];

            return encodedCar;
        }

        public double[] NormalizeCarWithFilters(PredictDto carToNormalize, double[] minMaxValues)
        {
            double[] encodedCar = _dataEncoder.EncodeCarToPredictWithFilters(carToNormalize);

            encodedCar[0] = (encodedCar[0] - minMaxValues[0]) / minMaxValues[1];
            encodedCar[1] = (encodedCar[1] - minMaxValues[2]) / minMaxValues[3];
            encodedCar[2] = (encodedCar[2] - minMaxValues[4]) / minMaxValues[5];
            encodedCar[3] = (encodedCar[3] - minMaxValues[6]) / minMaxValues[7];

            return encodedCar;
        }
    }
}