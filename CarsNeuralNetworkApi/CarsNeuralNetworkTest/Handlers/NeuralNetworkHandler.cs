using CarsNeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsNeuralNetwork.Handlers
{
    public static class NeuralNetworkHandler
    {
        public static double[][] MakeMatrix(int rows, int cols) // helper for ctor
        {
            double[][] result = new double[rows][];
            for (int r = 0; r < result.Length; ++r)
                result[r] = new double[cols];
            return result;
        }

        public static void SetWeights(SimpleNeuralNetwork nn, double[] weights)
        {
            int weightsNumber = (nn.InputNumber * nn.HiddenNumber) + (nn.HiddenNumber * nn.OutputNumber) + nn.HiddenNumber + nn.OutputNumber;
            if (weights.Length != weightsNumber)
                throw new Exception("Bad weights array length: ");

            int k = 0; // counter dla weightsNumber

            for (int i = 0; i < nn.InputNumber; ++i)
            {
                for (int j = 0; j < nn.HiddenNumber; ++j)
                {
                    nn.InputHiddenWeights[i][j] = weights[k++];
                }
            }

            for (int i = 0; i < nn.HiddenNumber; ++i)
            {
                nn.HiddenBiases[i] = weights[k++];
            }

            for (int i = 0; i < nn.HiddenNumber; ++i)
            {
                for (int j = 0; j < nn.OutputNumber; ++j)
                {
                    nn.HiddenOutputWeights[i][j] = weights[k++];
                }
            }

            for (int i = 0; i < nn.OutputNumber; ++i)
            {
                nn.OutputBiases[i] = weights[k++];
            }
        }

        public static double[] GetWeights(SimpleNeuralNetwork nn)
        {
            int numWeights = (nn.InputNumber * nn.HiddenNumber) + (nn.HiddenNumber * nn.OutputNumber) + nn.HiddenNumber + nn.OutputNumber;
            double[] result = new double[numWeights];
            int k = 0;

            for (int i = 0; i < nn.InputHiddenWeights.Length; ++i)
            {
                for (int j = 0; j < nn.InputHiddenWeights[0].Length; ++j)
                {
                    result[k++] = nn.InputHiddenWeights[i][j];
                }
            }

            for (int i = 0; i < nn.HiddenBiases.Length; ++i)
            {
                result[k++] = nn.HiddenBiases[i];
            }

            for (int i = 0; i < nn.HiddenOutputWeights.Length; ++i)
            {
                for (int j = 0; j < nn.HiddenOutputWeights[0].Length; ++j)
                {
                    result[k++] = nn.HiddenOutputWeights[i][j];
                }
            }

            for (int i = 0; i < nn.OutputBiases.Length; ++i)
            {
                result[k++] = nn.OutputBiases[i];
            }

            return result;
        }

        public static double[] ComputeOutputs(SimpleNeuralNetwork nn, double[] xValues)
        {
            if (xValues.Length != nn.InputNumber)
                throw new Exception("Bad xValues array length");

            double[] hSums = new double[nn.HiddenNumber]; // sumy które wchodza do ukrytych neuronów
            double[] oSums = new double[nn.OutputNumber]; // sumy które wchodzą do wyjsciowych neuronów

            for (int i = 0; i < xValues.Length; ++i) //kopiuj xValues jako inputy do sieci
                nn.Inputs[i] = xValues[i];

            for (int j = 0; j < nn.HiddenNumber; ++j)  // oblicz sumę z input-hidden, czyli suma foreach(nn.Inputs) {weights * input}
                for (int i = 0; i < nn.InputNumber; ++i)
                    hSums[j] += nn.Inputs[i] * nn.InputHiddenWeights[i][j];

            for (int i = 0; i < nn.HiddenNumber; ++i)  //dodaj biasy do tych sum
                hSums[i] += nn.HiddenBiases[i];

            for (int i = 0; i < nn.HiddenNumber; ++i)   // zastosuj funkcję aktywacji
                nn.HiddenOutputs[i] = ActivationFunctions.LinearFunction(hSums[i]); // liniowa robi robotę, hyper jeszcze bardziej, a treshold dla 0 nie działa.

            for (int j = 0; j < nn.OutputNumber; ++j)   // oblicz sumę z  hidden-output, czyli suma foreach(HiddenOutputs) {weights * output}
                for (int i = 0; i < nn.HiddenNumber; ++i)
                    oSums[j] += nn.HiddenOutputs[i] * nn.HiddenOutputWeights[i][j];

            for (int i = 0; i < nn.OutputNumber; ++i)  //dodaj biasy do tych sum
                oSums[i] += nn.OutputBiases[i];

            double[] softOut = Softmax(oSums); // softmax dla wyrównania outputów
            Array.Copy(softOut, nn.Outputs, softOut.Length);

            double[] retResult = new double[nn.OutputNumber];
            Array.Copy(nn.Outputs, retResult, retResult.Length);
            return retResult;
        }

        private static double[] Softmax(double[] outputsSums)
        {
            //wyrówna outputy, by suma dawała mniej wiecej 1
            double max = outputsSums[0];
            for (int i = 0; i < outputsSums.Length; ++i)
                if (outputsSums[i] > max) max = outputsSums[i];

            double scale = 0.0;
            for (int i = 0; i < outputsSums.Length; ++i)
                scale += Math.Exp(outputsSums[i] - max);

            double[] result = new double[outputsSums.Length];
            for (int i = 0; i < outputsSums.Length; ++i)
                result[i] = Math.Exp(outputsSums[i] - max) / scale;

            return result;
        }

        public static void ShowVector(double[] vector, int valuesPerRow, int decimals, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
            {
                if (i % valuesPerRow == 0) Console.WriteLine("");
                Console.Write(vector[i].ToString("F" + decimals).PadLeft(decimals + 4) + " ");
            }
            if (newLine == true) Console.WriteLine("");
        }

        public static void ShowMatrix(double[][] matrix, int numRows, int decimals, bool newLine)
        {
            for (int i = 0; i < numRows; ++i)
            {
                Console.Write(i.ToString().PadLeft(3) + ": ");
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    if (matrix[i][j] >= 0.0) Console.Write(" "); else Console.Write("-"); ;
                    Console.Write(Math.Abs(matrix[i][j]).ToString("F" + decimals) + " ");
                }
                Console.WriteLine("");
            }
            if (newLine == true) Console.WriteLine("");
        }
    }
}