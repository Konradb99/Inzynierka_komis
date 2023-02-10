using CarsNeuralNetwork.Models;
using CarsNeuralNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsNeuralNetwork.Handlers
{
    public class NeuralNetworkTrainer
    {
        private SimpleNeuralNetwork nn;

        public NeuralNetworkTrainer(SimpleNeuralNetwork nn)
        {
            this.nn = nn;
        }

        public double[] Train(double[][] trainData, int numParticles, int maxEpochs, double exitError, double probDeath)
        {
            Random rnd = new Random();

            int numWeights = (nn.InputNumber * nn.HiddenNumber) + (nn.HiddenNumber * nn.OutputNumber) + nn.HiddenNumber + nn.OutputNumber;

            // stałe do PSO
            int epoch = 0;
            double minX = -10.0; // for each weight. assumes data has been normalized about 0
            double maxX = 10.0;
            double w = 0.729; // inertia weight
            double c1 = 1.49445; // cognitive/local weight
            double c2 = 1.49445; // social/global weight
            double r1, r2; // cognitive and social randomizations

            Particle[] swarm = new Particle[numParticles];
            double[] bestGlobalPosition = new double[numWeights];
            double bestGlobalError = double.MaxValue;

            //inicjalizacja pierwszej wersji roju
            for (int i = 0; i < swarm.Length; ++i)
            {
                double[] randomPosition = new double[numWeights];
                for (int j = 0; j < randomPosition.Length; ++j)
                {
                    randomPosition[j] = (maxX - minX) * rnd.NextDouble() + minX;
                }

                double error = MeanSquaredError(trainData, randomPosition);
                double[] randomVelocity = new double[numWeights];

                for (int j = 0; j < randomVelocity.Length; ++j)
                {
                    double lo = 0.1 * minX;
                    double hi = 0.1 * maxX;
                    randomVelocity[j] = (hi - lo) * rnd.NextDouble() + lo;

                }

                // dwa ostatnie to best-Position and best-Error
                swarm[i] = new Particle(randomPosition, error, randomVelocity, randomPosition, error);

                //lepszy Error od globalnego?
                if (swarm[i].Error < bestGlobalError)
                {
                    bestGlobalError = swarm[i].Error;
                    swarm[i].Position.CopyTo(bestGlobalPosition, 0);
                }
            }

            //algorytm PSO
            int[] sequence = new int[numParticles];
            for (int i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            while (epoch < maxEpochs)
            {
                if (bestGlobalError < exitError) break; // warunek spełniony

                double[] newVelocity = new double[numWeights];
                double[] newPosition = new double[numWeights];
                double newError;

                Shuffle(sequence, rnd);

                foreach (Particle currentParticle in swarm)
                {
                    for (int j = 0; j < currentParticle.Velocity.Length; ++j)
                    {
                        r1 = rnd.NextDouble();
                        r2 = rnd.NextDouble();

                        // wzór na nową prędkość
                        //v_(t+1) = ω*v_t + r1 * c1 * (bestPos − currPos) + r2 * c2(bestGlobalPos − currPos)
                        //https://www.scielo.org.mx/pdf/cys/v20n4/1405-5546-cys-20-04-00635.pdf

                        newVelocity[j] = (w * currentParticle.Velocity[j]) +
                          (c1 * r1 * (currentParticle.BestPosition[j] - currentParticle.Position[j])) +
                          (c2 * r2 * (bestGlobalPosition[j] - currentParticle.Position[j]));
                    }

                    newVelocity.CopyTo(currentParticle.Velocity, 0);

                    for (int j = 0; j < currentParticle.Position.Length; ++j)
                    {
                        newPosition[j] = currentParticle.Position[j] + newVelocity[j];
                        if (newPosition[j] < minX)
                            newPosition[j] = minX;
                        else if (newPosition[j] > maxX)
                            newPosition[j] = maxX;
                    }

                    newPosition.CopyTo(currentParticle.Position, 0);
                    newError = MeanSquaredError(trainData, newPosition);
                    currentParticle.Error = newError;

                    if (newError < currentParticle.BestError) //sprawdz czy najlepszy dla cząstki
                    {
                        newPosition.CopyTo(currentParticle.BestPosition, 0);
                        currentParticle.BestError = newError;
                    }

                    if (newError < bestGlobalError) // sprawdz czy najlepszy globalny
                    {
                        newPosition.CopyTo(bestGlobalPosition, 0);
                        bestGlobalError = newError;
                    }

                    double die = rnd.NextDouble();
                    if (die < probDeath)
                    {
                        //jesli umarła, utwórz cząstkę o nowej pozycji, tej samej prędkości i nowym errorze
                        for (int j = 0; j < currentParticle.Position.Length; ++j)
                            currentParticle.Position[j] = (maxX - minX) * rnd.NextDouble() + minX;
                        currentParticle.Error = MeanSquaredError(trainData, currentParticle.Position);
                        currentParticle.Position.CopyTo(currentParticle.BestPosition, 0);
                        currentParticle.BestError = currentParticle.Error;

                        if (currentParticle.Error < bestGlobalError)// sprawdz czy najlepszy globalny
                        {
                            bestGlobalError = currentParticle.Error;
                            currentParticle.Position.CopyTo(bestGlobalPosition, 0);
                        }
                    }

                }

                ++epoch;

            }

            NeuralNetworkHandler.SetWeights(nn, bestGlobalPosition);
            double[] retResult = new double[numWeights];
            Array.Copy(bestGlobalPosition, retResult, retResult.Length);
            return retResult;

        }

        private static void Shuffle(int[] sequence, Random rnd)
        {
            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        }

        private double MeanSquaredError(double[][] trainData, double[] weights)
        {
            NeuralNetworkHandler.SetWeights(nn, weights);

            double[] xValues = new double[nn.InputNumber]; // inputs
            double[] tValues = new double[nn.OutputNumber]; // targets
            double sumSquaredError = 0.0;

            foreach (double[] train in trainData)
            {
                // podziel trainData na inputy i wyniki
                Array.Copy(train, xValues, nn.InputNumber);
                Array.Copy(train, nn.InputNumber, tValues, 0, nn.OutputNumber);
                double[] yValues = NeuralNetworkHandler.ComputeOutputs(nn, xValues);

                for (int j = 0; j < yValues.Length; ++j)
                {
                    sumSquaredError += ((yValues[j] - tValues[j]) * (yValues[j] - tValues[j]));
                }
            }

            return sumSquaredError / trainData.Length;
        }

        public double Accuracy(double[][] testData)
        {
            int numCorrect = 0;
            int numWrong = 0;
            double[] xValues = new double[nn.InputNumber]; // inputs
            double[] tValues = new double[nn.OutputNumber]; // targets
            double[] yValues; // policzone "targets"

            foreach (double[] test in testData)
            {
                // podziel trainData na inputy i wyniki
                Array.Copy(test, xValues, nn.InputNumber);
                Array.Copy(test, nn.InputNumber - 1, tValues, 0, nn.OutputNumber);
                yValues = NeuralNetworkHandler.ComputeOutputs(nn, xValues);
                int maxIndex = MaxIndex(yValues);

                if (tValues[maxIndex] == 1.0)
                    ++numCorrect;
                else
                    ++numWrong;
            }
            return (numCorrect * 1.0) / (numCorrect + numWrong); //dzielenie przez zero?
        }

        private static int MaxIndex(double[] vector)
        {
            int bigIndex = 0;
            double biggestVal = vector[0];
            for (int i = 0; i < vector.Length; ++i)
            {
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i]; bigIndex = i;
                }
            }
            return bigIndex;
        }
    }

}