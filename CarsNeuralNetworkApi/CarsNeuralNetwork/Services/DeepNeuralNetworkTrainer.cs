using CarsNeuralCore.Constants;
using CarsNeuralNetwork.Models;
using System.Diagnostics;

namespace CarsNeuralNetwork.Handlers
{
    public class DeepNeuralNetworkTrainer
    {
        private DeepNeuralNetwork _neuralNetwork;

        public DeepNeuralNetworkTrainer(DeepNeuralNetwork neuralNetwork)
        {
            _neuralNetwork = neuralNetwork;
        }

        public double[] TrainMulti(double[][] trainData, int maxEpochs, double learnRate, double momentum, StreamWriter sw)
        {
            Stopwatch stoper = new Stopwatch();
            //input
            double[] inputValues = new double[_neuralNetwork.InputCount]; // inputs

            //input-hidden
            double[][] inputHiddenWeightsPreviousDelta = DeepNeuralNetworkHandler.MakeMatrix(_neuralNetwork.InputCount, _neuralNetwork.HiddenLayers[0], 0.0);
            double[][] inputHiddenGradients = DeepNeuralNetworkHandler.MakeMatrix(_neuralNetwork.InputCount, _neuralNetwork.HiddenLayers[0], 0.0);  // input-to-hidden weight gradients

            //hidden
            List<double[]> hiddenSignals = new List<double[]>();
            List<double[]> hiddenBiasesGradients = new List<double[]>();
            List<double[]> hiddenBiasesPreviousDelta = new List<double[]>();
            List<double[][]> hiddenHiddenGradients = new List<double[][]>();
            List<double[][]> hiddenHiddenWeightsPreviousDelta = new List<double[][]>();

            for (int i = 0; i < _neuralNetwork.HiddenLayers.Count; i++)
            {
                hiddenSignals.Add(new double[_neuralNetwork.HiddenLayers[i]]);
                hiddenBiasesGradients.Add(new double[_neuralNetwork.HiddenLayers[i]]);
                hiddenBiasesPreviousDelta.Add(new double[_neuralNetwork.HiddenLayers[i]]);
            }
            for (int i = 0; i < _neuralNetwork.HiddenLayers.Count - 1; i++)
            {
                hiddenHiddenGradients.Add(DeepNeuralNetworkHandler.MakeMatrix(_neuralNetwork.HiddenLayers[i], _neuralNetwork.HiddenLayers[i + 1], 0.0));
                hiddenHiddenWeightsPreviousDelta.Add(DeepNeuralNetworkHandler.MakeMatrix(_neuralNetwork.HiddenLayers[i], _neuralNetwork.HiddenLayers[i + 1], 0.0));
            }

            //hidden-output
            double[][] hiddenOutputsGradients = DeepNeuralNetworkHandler.MakeMatrix(_neuralNetwork.HiddenLayers.Last(), _neuralNetwork.OutputCount, 0.0);
            double[][] hiddenOutputWeightsPreviousDelta = DeepNeuralNetworkHandler.MakeMatrix(_neuralNetwork.HiddenLayers.Last(), _neuralNetwork.OutputCount, 0.0);

            //output
            double[] outputSignals = new double[_neuralNetwork.OutputCount];
            double[] outputBiasesGradients = new double[_neuralNetwork.OutputCount];
            double[] expectedOutputValues = new double[_neuralNetwork.OutputCount];
            double[] outputBiasesPreviousDelta = new double[_neuralNetwork.OutputCount];

            int epoch = 0;
            double derivative = 0.0;
            double errorSignal = 0.0;

            int[] shuffledOrderOfTrainData = new int[trainData.Length];
            for (int i = 0; i < shuffledOrderOfTrainData.Length; ++i)
                shuffledOrderOfTrainData[i] = i;

            int errorInterval = maxEpochs / 10;
            stoper.Start();
            string stringToFile = "";

            while (epoch < maxEpochs)
            {
                ++epoch;

                if (epoch % errorInterval == 0 && epoch < maxEpochs)
                {
                    stoper.Stop();
                    long time = stoper.ElapsedMilliseconds / 1000;

                    double trainError = CalculateError(trainData);
                    Console.WriteLine("Epoka = " + epoch + "  Error = " +
                      trainError.ToString("F4") + "   time (s): " + time);
                    stringToFile += "epoka = " + epoch + "  error = " +
                      trainError.ToString("F4") + "   time (s): " + time + "\n";

                    stoper.Start();
                    if (trainError <= BackPropagationConstants.ExitError)
                        break;
                }

                DeepNeuralNetworkHandler.Shuffle(shuffledOrderOfTrainData);
                for (int ii = 0; ii < trainData.Length; ++ii)
                {
                    int idx = shuffledOrderOfTrainData[ii];
                    Array.Copy(trainData[idx], inputValues, _neuralNetwork.InputCount);
                    Array.Copy(trainData[idx], _neuralNetwork.InputCount, expectedOutputValues, 0, _neuralNetwork.OutputCount);
                    DeepNeuralNetworkHandler.ComputeOutputs(inputValues, _neuralNetwork);

                    //OUTPUT I OUTPUT-HIDDEN
                    // 1. policz sygnaly wyjsciowe
                    for (int k = 0; k < _neuralNetwork.OutputCount; ++k)
                    {
                        errorSignal = expectedOutputValues[k] - _neuralNetwork.Outputs[k];
                        derivative = (1 - _neuralNetwork.Outputs[k]) * _neuralNetwork.Outputs[k];
                        outputSignals[k] = errorSignal * derivative;
                    }

                    // 2. policz gradienty wag miedzy ostatnia ukryta i wyjsciowa
                    for (int j = 0; j < _neuralNetwork.HiddenLayers.Last(); ++j)
                        for (int k = 0; k < _neuralNetwork.OutputCount; ++k)
                            hiddenOutputsGradients[j][k] = outputSignals[k] * _neuralNetwork.hiddenOutputs.Last()[j];

                    // 3. policz gradienty biasow wyjsciowych
                    for (int k = 0; k < _neuralNetwork.OutputCount; ++k)
                        outputBiasesGradients[k] = outputSignals[k] * 1.0; // dummy assoc. input value

                    // 4. policz sygnaly dla ostatniej ukrytej
                    for (int j = 0; j < _neuralNetwork.HiddenLayers.Last(); ++j)
                    {
                        derivative = ActivationFunctions.HyperTanFunction(_neuralNetwork.hiddenOutputs.Last()[j])[1];
                        double sum = 0.0;
                        for (int k = 0; k < _neuralNetwork.OutputCount; ++k)
                        {
                            sum += outputSignals[k] * _neuralNetwork.hiddenOutputWeights[j][k]; // represents error signal
                        }
                        hiddenSignals.Last()[j] = derivative * sum;
                    }

                    for (int i = _neuralNetwork.HiddenLayers.Count - 2; i >= 0; i--)
                    {
                        //5, 8. policz gradienty wag miedzy ukrytymi warstwami
                        for (int j = 0; j < _neuralNetwork.HiddenLayers[i]; ++j)
                        {
                            for (int k = 0; k < _neuralNetwork.HiddenLayers[i + 1]; ++k)
                            {
                                hiddenHiddenGradients[i][j][k] = hiddenSignals[i + 1][k] * _neuralNetwork.hiddenOutputs[i][j];
                            }
                        }
                        //6, 9 policz gradienty ukrytych biasow
                        for (int k = 0; k < _neuralNetwork.HiddenLayers[i + 1]; ++k)
                        {
                            hiddenBiasesGradients[i + 1][k] = hiddenSignals[i + 1][k];
                        }
                        //7, 10. policz sygnaly ukrytych neuronow
                        for (int j = 0; j < _neuralNetwork.HiddenLayers[i]; ++j)
                        {
                            derivative = ActivationFunctions.HyperTanFunction(_neuralNetwork.hiddenOutputs[i][j])[1];
                            double sum = 0.0;
                            for (int k = 0; k < _neuralNetwork.HiddenLayers[i + 1]; ++k)
                            {
                                sum += hiddenSignals[i + 1][k] * _neuralNetwork.hiddenHiddenWeights[i][j][k];
                            }
                            hiddenSignals[i][j] = derivative * sum;
                        }
                    }

                    //11. policz gradienty wag miedzy wejsciowa i pierwsza ukryta
                    for (int i = 0; i < _neuralNetwork.InputCount; ++i)
                        for (int j = 0; j < _neuralNetwork.HiddenLayers[0]; ++j)
                            inputHiddenGradients[i][j] = hiddenSignals[0][j] * _neuralNetwork.Inputs[i];

                    // 12. policz biasy pierwszej ukrytej
                    for (int j = 0; j < _neuralNetwork.HiddenLayers[0]; ++j)
                        hiddenBiasesGradients[0][j] = hiddenSignals[0][j];

                    // == aktualizacja

                    //13. zaktualizuj wagi wejsciowa-pierwsza ukryta
                    for (int i = 0; i < _neuralNetwork.InputCount; ++i)
                    {
                        for (int j = 0; j < _neuralNetwork.HiddenLayers[0]; ++j)
                        {
                            double delta = inputHiddenGradients[i][j] * learnRate;
                            _neuralNetwork.inputHiddenWeights[i][j] += delta;
                            _neuralNetwork.inputHiddenWeights[i][j] += inputHiddenWeightsPreviousDelta[i][j] * momentum;
                            inputHiddenWeightsPreviousDelta[i][j] = delta;
                        }
                    }

                    for (int i = 0; i < _neuralNetwork.HiddenLayers.Count - 1; ++i)
                    {
                        //14, 16. zaktualizuj biasy ukrytych warstw
                        for (int j = 0; j < _neuralNetwork.HiddenLayers[i]; ++j)
                        {
                            double delta = hiddenBiasesGradients[i][j] * learnRate;
                            _neuralNetwork.hiddenBiases[i][j] += delta;
                            _neuralNetwork.hiddenBiases[i][j] += hiddenBiasesPreviousDelta[i][j] * momentum;
                            hiddenBiasesPreviousDelta[i][j] = delta;
                        }
                        //15, 17. zaktualizuj wagi miedzy ukrytymi neuronami
                        for (int j = 0; j < _neuralNetwork.HiddenLayers[i]; ++j)
                        {
                            for (int k = 0; k < _neuralNetwork.HiddenLayers[i + 1]; ++k)
                            {
                                double delta = hiddenHiddenGradients[i][j][k] * learnRate;
                                _neuralNetwork.hiddenHiddenWeights[i][j][k] += delta;
                                _neuralNetwork.hiddenHiddenWeights[i][j][k] += hiddenHiddenWeightsPreviousDelta[i][j][k] * momentum;
                                hiddenHiddenWeightsPreviousDelta[i][j][k] = delta;
                            }
                        }
                    }

                    //18. zaktualizuj biasy ukryte
                    for (int j = 0; j < _neuralNetwork.HiddenLayers.Last(); ++j)
                    {
                        double delta = hiddenBiasesGradients.Last()[j] * learnRate;
                        _neuralNetwork.hiddenBiases.Last()[j] += delta;
                        _neuralNetwork.hiddenBiases.Last()[j] += hiddenBiasesPreviousDelta.Last()[j] * momentum;
                        hiddenBiasesPreviousDelta.Last()[j] = delta;
                    }

                    //19. zaktualizuj wagi ostatnia ukryta-wyjsciowa
                    for (int j = 0; j < _neuralNetwork.HiddenLayers.Last(); ++j)
                    {
                        for (int k = 0; k < _neuralNetwork.OutputCount; ++k)
                        {
                            double delta = hiddenOutputsGradients[j][k] * learnRate;
                            _neuralNetwork.hiddenOutputWeights[j][k] += delta;
                            _neuralNetwork.hiddenOutputWeights[j][k] += hiddenOutputWeightsPreviousDelta[j][k] * momentum;
                            hiddenOutputWeightsPreviousDelta[j][k] = delta;
                        }
                    }

                    //20. zaktualizuj biasy wyjsciowe
                    for (int k = 0; k < _neuralNetwork.OutputCount; ++k)
                    {
                        double delta = outputBiasesGradients[k] * learnRate;
                        _neuralNetwork.outputBiases[k] += delta;
                        _neuralNetwork.outputBiases[k] += outputBiasesPreviousDelta[k] * momentum;
                        outputBiasesPreviousDelta[k] = delta;
                    }
                }
            }

            using (sw)
            {
                sw.Write(stringToFile);
            }
            double[] bestWts = _neuralNetwork.GetWeights();
            return bestWts;
        }

        private double CalculateError(double[][] trainData)
        {
            double sumSquaredError = 0.0;
            double[] inputValues = new double[_neuralNetwork.InputCount];
            double[] expectedOutputValues = new double[_neuralNetwork.OutputCount];

            for (int i = 0; i < trainData.Length; ++i)
            {
                Array.Copy(trainData[i], inputValues, _neuralNetwork.InputCount);
                Array.Copy(trainData[i], _neuralNetwork.InputCount, expectedOutputValues, 0, _neuralNetwork.OutputCount);
                double[] outputValues = DeepNeuralNetworkHandler.ComputeOutputs(inputValues, _neuralNetwork);
                for (int j = 0; j < _neuralNetwork.OutputCount; ++j)
                {
                    double error = expectedOutputValues[j] - outputValues[j];
                    sumSquaredError += error * error;
                }
            }
            return sumSquaredError / trainData.Length;
        }
    }
}