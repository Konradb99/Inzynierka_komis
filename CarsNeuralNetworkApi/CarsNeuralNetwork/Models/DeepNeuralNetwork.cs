using CarsNeuralNetwork.Handlers;

namespace CarsNeuralNetwork.Models
{
    public class DeepNeuralNetwork
    {
        public double[][] inputHiddenWeights;
        public List<List<List<double>>> hiddenHiddenWeights = new List<List<List<double>>>();
        public List<double[]> hiddenBiases = new List<double[]>();
        public List<double[]> hiddenOutputs = new List<double[]>();
        public double[][] hiddenOutputWeights;
        public double[] outputBiases;

        private Random random;
        private int hiddenhiddenWeightsCounter = 0;
        private int hiddenNeuronsCounter = 0;

        public DeepNeuralNetwork(int inputCount, List<int> hiddenLayers, int outputCount)
        {
            InputCount = inputCount;
            HiddenLayers = hiddenLayers;
            OutputCount = outputCount;
            random = new Random();

            Inputs = new double[inputCount];

            inputHiddenWeights = DeepNeuralNetworkHandler.MakeMatrix(inputCount, hiddenLayers[0], random.NextDouble());

            for (int i = 0; i < hiddenLayers.Count; i++)
            {
                hiddenBiases.Add(new double[hiddenLayers[i]]);
                hiddenOutputs.Add(new double[hiddenLayers[i]]);
                for (int j = 0; j < hiddenLayers[i]; j++)
                {
                    hiddenNeuronsCounter++;
                }
            }
            for (int i = 0; i < hiddenLayers.Count - 1; i++)
            {
                hiddenHiddenWeights.Add(new List<List<double>>());
                for (int j = 0; j < hiddenLayers[i]; j++)
                {
                    hiddenHiddenWeights[i].Add(new List<double>());
                    for (int k = 0; k < hiddenLayers[i + 1]; k++)
                    {
                        hiddenHiddenWeights[i][j].Add(random.NextDouble());
                        hiddenhiddenWeightsCounter++;
                    }
                }
            }

            hiddenOutputWeights = DeepNeuralNetworkHandler.MakeMatrix(hiddenLayers.Last(), outputCount, random.NextDouble());
            outputBiases = new double[outputCount];
            Outputs = new double[outputCount];
        }

        public int InputCount { get; set; }

        public List<int> HiddenLayers { get; set; }

        public int OutputCount { get; set; }

        public double[] Inputs { get; set; }

        public double[] Outputs { get; set; }

        public List<List<List<double>>> HiddenHiddenWeights { get; set; }

        public void SetWeights(double[] weights)
        {
            int k = 0;

            for (int i = 0; i < InputCount; ++i)
                for (int j = 0; j < HiddenLayers[0]; ++j)
                    inputHiddenWeights[i][j] = weights[k++];

            for (int i = 0; i < HiddenLayers.Count - 1; i++)
            {
                for (int j = 0; j < HiddenLayers[i]; j++)
                {
                    for (int l = 0; l < HiddenLayers[i + 1]; l++)
                    {
                        hiddenHiddenWeights[i][j][l] = weights[k++];
                    }
                    hiddenBiases[i][j] = weights[k++];
                }
            }

            for (int i = 0; i < HiddenLayers.Last(); i++)
            {
                for (int j = 0; j < OutputCount; ++j)
                    hiddenOutputWeights[i][j] = weights[k++];
            }

            for (int i = 0; i < OutputCount; ++i)
                outputBiases[i] = weights[k++];
        }

        public double[] GetWeights()
        {
            List<double> result = new List<double>();
            int k = 0;

            for (int i = 0; i < InputCount; ++i)
                for (int j = 0; j < HiddenLayers[0]; ++j)
                    result.Add(inputHiddenWeights[i][j]);

            for (int i = 0; i < HiddenLayers.Count - 1; i++)
            {
                for (int j = 0; j < HiddenLayers[i]; j++)
                {
                    for (int l = 0; l < HiddenLayers[i + 1]; l++)
                    {
                        result.Add(hiddenHiddenWeights[i][j][l]);
                    }
                    result.Add(hiddenBiases[i][j]);
                }
            }

            for (int i = 0; i < HiddenLayers.Last(); i++)
            {
                for (int j = 0; j < OutputCount; ++j)
                    result.Add(hiddenOutputWeights[i][j]);
            }

            for (int i = 0; i < OutputCount; i++)
                result.Add(outputBiases[i]);

            return result.ToArray();
        }
    }
}