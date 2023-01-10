using CarsNeuralApplication.Services;
using CarsNeuralCore.Dto;
using CarsNeuralKNN.Algorithm;
using CarsNeuralNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsNeuralNetworkAccurancyTester.Services
{
    public class AccurancyTesterService : IAccurancyTesterService
    {
        private readonly IDataService _dataService;
        private readonly IKNNService _kNNService;
        private readonly INeuralNetworkService _neuralNetworkService;

        public AccurancyTesterService(
            IDataService dataService,
            IKNNService kNNService,
            INeuralNetworkService neuralNetworkService)
        {
            _dataService = dataService;
            _kNNService = kNNService;
            _neuralNetworkService = neuralNetworkService;
        }

        public async Task getAccurancy()
        {
            ICollection<CarDto> testSet = await _dataService.GetTestData();

            double accurancyCounterKnn = 0;
            double accurancyCounterFiltersKnn = 0;
            double accurancyCounterNeural = 0;

            foreach (var testElement in testSet)
            {
                PredictDto testItem = new PredictDto()
                {
                    BodyType = testElement.BodyType,
                    DriveType = testElement.DriveType,
                    GearboxType = testElement.GearboxType,
                    FuelType = testElement.FuelType,
                    Price = testElement.Price.ToString(),
                    Distance = testElement.Distance.ToString(),
                    ProductionYear = testElement.ProductionYear.ToString(),
                    Capacity = testElement.Capacity.ToString(),
                };

                PredictionResultDto returnedValueKnn = await _kNNService.calculatePrediction(testItem);
                PredictionResultDto returnedValueFilters = await _kNNService.calculatePredictionWithFilters(testItem);
                PredictionResultDto returnedValueNeural = await _neuralNetworkService.ReturnPredictionResult(testItem);

                string expectedClass = $"{testElement.Brand} {testElement.Model}";

                if (returnedValueKnn.prefferedClass == expectedClass)
                {
                    accurancyCounterKnn += 1;
                }

                if (returnedValueFilters.prefferedClass == expectedClass)
                {
                    accurancyCounterFiltersKnn += 1;
                }

                if (returnedValueNeural.prefferedClass == expectedClass)
                {
                    accurancyCounterNeural += 1;
                }
            }

            string finalAccurancyKnn = string.Format("{0:0.00}", accurancyCounterKnn / testSet.Count() * 100);
            string finalAccurancyFilters = string.Format("{0:0.00}", accurancyCounterFiltersKnn / testSet.Count() * 100);
            string finalAccurancyNeural = string.Format("{0:0.00}", accurancyCounterNeural / testSet.Count() * 100);

            Console.WriteLine($"Total test samples count: {testSet.Count()}");
            Console.WriteLine($"Good guessed cars for KNN: {accurancyCounterKnn}");
            Console.WriteLine($"Good guessed cars for KNN wit Filters: {accurancyCounterFiltersKnn}");
            Console.WriteLine($"Good guessed cars for Neural Network: {accurancyCounterNeural}");
            Console.WriteLine($"Accurancy for KNN: {finalAccurancyKnn}%");
            Console.WriteLine($"Accurancy for KNN with Filters: {finalAccurancyFilters}%");
            Console.WriteLine($"Accurancy for Neural Network: {finalAccurancyNeural}%");
        }
    }
}
