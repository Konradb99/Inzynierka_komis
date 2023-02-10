using CarsNeuralApplication.Services;
using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;
using CarsNeuralKNN.Algorithms;
using CarsNeuralKNN.Encoders;
using CarsNeuralKNN.Metrics;
using System.Data;

namespace CarsNeuralKNN.Algorithm
{
    public class KNNService : IKNNService
    {
        private readonly IDataService _dataService;
        private readonly IDataEncoder _dataEncoder;
        private readonly IMetrics _metrics;
        private readonly IVotingService _votingService;
        private readonly IDataClassifier _dataClassifier;
        private readonly IDataNormalizer _dataNormalizer;
        private IList<CarDto> trainSet;

        public KNNService(
            IDataService dataService,
            IDataEncoder dataEncoder,
            IMetrics metrics,
            IVotingService votingService,
            IDataClassifier dataClassifier,
            IDataNormalizer dataNormalizer)
        {
            _dataService = dataService;
            _dataEncoder = dataEncoder;
            _metrics = metrics;
            _votingService = votingService;
            _dataClassifier = dataClassifier;
            _dataNormalizer = dataNormalizer;
        }

        public async Task<PredictionResultDto> calculatePrediction(PredictDto predictionData)
        {
            IList<double[]> cars_encoded = await prepareDataSet(predictionData, false);

            double[] minAndMaxValues = _dataClassifier.getMinMax(cars_encoded);
            IList<double[]> trainList = _dataNormalizer.NormalizeCars(cars_encoded, minAndMaxValues);
            double[] normalizedPredictionData = _dataNormalizer.NormalizeCar(predictionData, minAndMaxValues);

            List<double> distances = _metrics.MinkowskiMetric(trainList, normalizedPredictionData);

            int neighboursCount = 10;
            if (neighboursCount > trainList.Count)
            {
                neighboursCount = trainList.Count;
            }

            PredictionResultDto result = await _votingService.VoteAlgorithm(neighboursCount, distances, trainSet);

            return result;
        }

        public async Task<PredictionResultDto> calculatePredictionWithFilters(PredictDto predictionData)
        {
            IList<double[]> cars_encoded = await prepareDataSet(predictionData, true);

            double[] minAndMaxValues = _dataClassifier.getMinMaxWithFilters(cars_encoded);
            IList<double[]> trainList = _dataNormalizer.NormalizeCarsWithFilters(cars_encoded, minAndMaxValues);
            double[] normalizedData = _dataNormalizer.NormalizeCarWithFilters(predictionData, minAndMaxValues);

            List<double> distances = _metrics.MinkowskiMetricWithFilters(trainList, normalizedData);

            int neighboursCount = 25;
            if (neighboursCount > trainList.Count)
            {
                neighboursCount = trainList.Count;
            }

            PredictionResultDto result = await _votingService.VoteAlgorithm(neighboursCount, distances, trainSet);

            return result;
        }

        private async Task<IList<double[]>> prepareDataSet(PredictDto predictionData, bool isFilters)
        {
            trainSet = await _dataService.GetTrainData();

            if (isFilters)
            {
                filterCars(predictionData);
            }

            if (trainSet.Count == 0)
            {
                throw new Exception(ErrorMessages.NoCarsException);
            }

            IList<double[]> cars_encoded = new List<double[]>();

            foreach (var car in trainSet)
            {
                cars_encoded.Add(_dataEncoder.EncodeCar(car));
            }

            return cars_encoded;
        }

        private void filterCars(PredictDto predictionData)
        {
            if (predictionData.GearboxType != null && predictionData.GearboxType != "null")
            {
                trainSet = trainSet
                    .Where(p => p.GearboxType == predictionData.GearboxType)
                    .ToList();
            }
            if (predictionData.FuelType != null && predictionData.FuelType != "null")
            {
                trainSet = trainSet
                    .Where(p => p.FuelType == predictionData.FuelType)
                    .ToList();
            }
            if (predictionData.DriveType != null && predictionData.DriveType != "null")
            {
                trainSet = trainSet
                    .Where(p => p.DriveType == predictionData.DriveType)
                    .ToList();
            }
            if (predictionData.BodyType != null && predictionData.BodyType != "null")
            {
                trainSet = trainSet
                    .Where(p => p.BodyType == predictionData.BodyType)
                    .ToList();
            }
        }
    }
}