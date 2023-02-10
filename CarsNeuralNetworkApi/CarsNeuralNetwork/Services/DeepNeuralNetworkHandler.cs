using CarsNeuralNetwork.Models;

namespace CarsNeuralNetwork.Handlers
{
    public class DeepNeuralNetworkHandler
    {
        public static double[][] MakeMatrix(int rows, int columns, double value)
        {
            double[][] result = new double[rows][];
            for (int r = 0; r < result.Length; ++r)
                result[r] = new double[columns];
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < columns; ++j)
                    result[i][j] = value;
            return result;
        }

        public static double[] ComputeOutputs(double[] inputValues, DeepNeuralNetwork neuralNetwork)
        {
            List<double[]> hiddenSums = new List<double[]>();
            for (int i = 0; i < neuralNetwork.HiddenLayers.Count; i++)
            {
                hiddenSums.Add(new double[neuralNetwork.HiddenLayers[i]]);
                for (int j = 0; j < hiddenSums[i].Length; j++)
                {
                    hiddenSums[i][j] = 0;
                }
            }

            double[] outputSums = new double[neuralNetwork.OutputCount];

            for (int i = 0; i < inputValues.Length; ++i)
                neuralNetwork.Inputs[i] = inputValues[i];

            for (int j = 0; j < neuralNetwork.HiddenLayers[0]; ++j)
            {
                for (int i = 0; i < neuralNetwork.InputCount; ++i)
                {
                    hiddenSums[0][j] += neuralNetwork.Inputs[i] * neuralNetwork.inputHiddenWeights[i][j];
                }
            }

            for (int i = 0; i < neuralNetwork.HiddenLayers[0]; ++i)
            {
                hiddenSums[0][i] += neuralNetwork.hiddenBiases[0][i];
            }

            for (int i = 0; i < neuralNetwork.HiddenLayers[0]; ++i)
            {
                neuralNetwork.hiddenOutputs[0][i] = ActivationFunctions.HyperTanFunction(hiddenSums[0][i])[0];
            }

            for (int i = 1; i < neuralNetwork.HiddenLayers.Count; i++)
            {
                for (int k = 0; k < neuralNetwork.HiddenLayers[i]; k++)
                {
                    for (int j = 0; j < neuralNetwork.HiddenLayers[i - 1]; j++)
                    {
                        hiddenSums[i][k] += neuralNetwork.hiddenOutputs[i - 1][j] * neuralNetwork.hiddenHiddenWeights[i - 1][j][k];
                    }
                }
                for (int k = 0; k < neuralNetwork.HiddenLayers[i]; k++)
                {
                    for (int j = 0; j < neuralNetwork.HiddenLayers[i - 1]; j++)
                    {
                        hiddenSums[i][k] += neuralNetwork.hiddenBiases[i][k];
                    }
                }

                for (int j = 0; j < neuralNetwork.HiddenLayers[i]; j++)
                    neuralNetwork.hiddenOutputs[i][j] = ActivationFunctions.HyperTanFunction(hiddenSums[i][j])[0];

            }

            for (int j = 0; j < neuralNetwork.OutputCount; ++j)
            {
                for (int i = 0; i < neuralNetwork.HiddenLayers.Last(); ++i)
                {
                    outputSums[j] += neuralNetwork.hiddenOutputs.Last()[i] * neuralNetwork.hiddenOutputWeights[i][j];
                }
            }

            for (int i = 0; i < neuralNetwork.OutputCount; ++i)
            {
                outputSums[i] += neuralNetwork.outputBiases[i];
            }

            double[] softOut = Softmax(outputSums);
            Array.Copy(softOut, neuralNetwork.Outputs, softOut.Length);

            double[] retResult = new double[neuralNetwork.OutputCount];
            Array.Copy(neuralNetwork.Outputs, retResult, retResult.Length);
            return retResult;
        }

        public static double[] Softmax(double[] outputSums)
        {
            double sum = 0.0;
            for (int i = 0; i < outputSums.Length; ++i)
                sum += Math.Exp(outputSums[i]);

            double[] result = new double[outputSums.Length];
            for (int i = 0; i < outputSums.Length; ++i)
                result[i] = Math.Exp(outputSums[i]) / sum;

            return result;
        }

        public static void Shuffle(int[] sequence)
        {
            Random rnd = new Random();
            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int temp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = temp;
            }
        }

        public static string ShowVector(double[] vector, int decimals, int lineLength, bool newLine)
        {
            string result = "";
            for (int i = 0; i < vector.Length; ++i)
            {
                if (i > 0 && i % lineLength == 0)
                {
                    result += "";
                }
                if (vector[i] >= 0)
                {
                    result += " ";
                }
                result += vector[i].ToString("F" + decimals) + " ";
            }
            if (newLine == true)
            {
                Console.WriteLine("");
            }
            Console.WriteLine(result);
            return result;
        }

        public static void ShowMatrix(double[][] matrix, int rowsNumber, int decimals, bool indices)
        {
            int len = matrix.Length.ToString().Length;
            for (int i = 0; i < rowsNumber; ++i)
            {
                if (indices == true)
                    Console.Write("[" + i.ToString().PadLeft(len) + "]  ");
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    double v = matrix[i][j];
                    if (v >= 0.0)
                        Console.Write(" ");
                    Console.Write(v.ToString("F" + decimals) + "  ");
                }
                Console.WriteLine("");
            }
        }

    }
}