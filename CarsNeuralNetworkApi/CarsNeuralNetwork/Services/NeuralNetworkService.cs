using CarsNeuralApplication.Services;
using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;
using CarsNeuralKNN.Algorithm;
using CarsNeuralNetwork.Encoders;
using CarsNeuralNetwork.Handlers;
using CarsNeuralNetwork.Models;

namespace CarsNeuralNetwork.Services
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        private readonly IDataService _dataService;
        private readonly INeuralNetworkDataEncoder _dataEncoder;
        private readonly IKNNService _knnService;
        private IList<CarDto> trainSet;

        public NeuralNetworkService(
            IDataService dataService,
            INeuralNetworkDataEncoder dataEncoder,
            IKNNService knnService)
        {
            _dataService = dataService;
            _dataEncoder = dataEncoder;
            _knnService = knnService;
        }

        public async Task<IList<double[]>> GetTrainSet()
        {
            trainSet = await _dataService.GetTestData();
            IList<double[]> cars_encoded = new List<double[]>();

            foreach (var car in trainSet)
            {
                cars_encoded.Add(_dataEncoder.EncodeCarForNeuralNetwork(car));
            }

            return cars_encoded;
        }

        public async Task LearnDeepNeuralNetworkAsync()
        {
            int inputCount = 8;
            int outputCount = 25;

            List<List<int>> hiddenLayers = new List<List<int>>()
            {
                new List<int>(){2,2,2},

            };
            List<double> learnRate = new List<double>() { 0.03 };
            List<double> momentum = new List<double>() { 0.01 };

            IList<double[]> cars_encoded = await GetTrainSet();

            double[] minAndMaxValues = _dataEncoder.getMinMax(cars_encoded);
            IList<double[]> trainList = _dataEncoder.NormalizeCars(cars_encoded, minAndMaxValues);

            double[][] trainData = trainList.ToArray();
            double[][] testData = new double[2][];
            testData[0] = new double[inputCount];
            testData[1] = new double[inputCount];

            Array.Copy(trainData[0], testData[0], inputCount);
            Array.Copy(trainData[1], testData[1], inputCount);

            for (int v = 0; v < hiddenLayers.Count; v++)
            {
                string fileName = "NeuralNetworkResults/";
                Console.Write("\nPoczątek uczenia sieci dla sieci ");
                List<int> numHidden = hiddenLayers[v];

                Console.Write(inputCount + "-");
                fileName += inputCount + "-";
                for (int h = 0; h < numHidden.Count; h++)
                {
                    Console.Write(numHidden[h] + "-");
                    fileName += numHidden[h] + "-";
                }
                Console.WriteLine(outputCount);
                fileName += outputCount;

                DeepNeuralNetwork nn = new DeepNeuralNetwork(inputCount, numHidden, outputCount);

                DeepNeuralNetworkTrainer trainer = new DeepNeuralNetworkTrainer(nn);

                int maxEpochs = 1000;
                string fileNameTemp = "";

                for (int L = 0; L < learnRate.Count; L++)
                {
                    for (int M = 0; M < momentum.Count; M++)
                    {
                        Console.WriteLine("LearnRate = " + learnRate[L].ToString("F4"));
                        Console.WriteLine("Momentum = " + momentum[M].ToString("F4"));

                        string tempLM = $"%{learnRate[L]}%{momentum[M]}";
                        fileNameTemp = fileName;
                        fileNameTemp += tempLM;
                        StreamWriter sw = new StreamWriter(fileNameTemp + ".txt");
                        double[] weights = trainer.TrainMulti(trainData, maxEpochs, learnRate[L], momentum[M], sw);
                        foreach (double weight in weights)
                        {
                            Console.WriteLine(weight + ",");
                        }
                        sw.Close();
                        Console.WriteLine("Skończona nauka");

                        using (StreamWriter sw2 = new StreamWriter(fileName + "-results.txt"))
                        {
                            string toSave = $"\nLearning rate: {learnRate[L]}   Momentum: {momentum[M]}\n";

                            for (int i = 0; i < testData.Length; i++)
                            {
                                double[] y = DeepNeuralNetworkHandler.ComputeOutputs(testData[i], nn);
                                double[] resultData = new double[outputCount];
                                Array.Copy(trainData[i], inputCount, resultData, 0, outputCount);

                                string temp = DeepNeuralNetworkHandler.ShowVector(y, 3, 3, true);
                                toSave += temp + "\n";
                                temp = DeepNeuralNetworkHandler.ShowVector(resultData, 3, 3, true);
                                toSave += temp + "\n";
                                string wagi = DeepNeuralNetworkHandler.ShowVector(weights, 15, 8, true);
                                toSave += wagi;
                                sw2.Write(toSave);
                            }
                        }
                    }
                }

            }

        }

        public async Task<PredictionResultDto> ReturnPredictionResult(PredictDto predictionData)
        {
            int inputCount = 8;
            int outputCount = 25;
            List<int> hiddenLayers = new List<int>() { 2, 2, 2 };
            DeepNeuralNetwork neuralNetwork = new DeepNeuralNetwork(inputCount, hiddenLayers, outputCount);

            neuralNetwork.SetWeights(NeuralNetworkWeights.weightsAsList.ToArray());
            double[] carFromUser = _dataEncoder.EncodeCarToPredict(predictionData);
            double[] resultCar = DeepNeuralNetworkHandler.ComputeOutputs(carFromUser, neuralNetwork);
            string prefferedCar = _dataEncoder.DecodeCarForNeuralNetwork(resultCar);

            PredictionResultDto KNNPredictionResult = await _knnService.calculatePrediction(predictionData);

            PredictionResultDto finalResult = new PredictionResultDto
            {
                prefferedClass = prefferedCar,
                prefferedCars = KNNPredictionResult.prefferedCars
            };

            return finalResult;
        }

    }
}