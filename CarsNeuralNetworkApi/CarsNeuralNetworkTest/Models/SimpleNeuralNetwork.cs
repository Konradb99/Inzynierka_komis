using CarsNeuralNetwork.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsNeuralNetwork
{
    public class SimpleNeuralNetwork
    {
        //private static Random rnd; // for BP to initialize wts, in PSO

        public SimpleNeuralNetwork(int inputNumber, int hiddenNumber, int outputNumber)
        {
            //rnd = new Random(16); // for particle initialization. 16 just gives nice demo
            InputNumber = inputNumber;
            HiddenNumber = hiddenNumber;
            OutputNumber = outputNumber;
            Inputs = new double[inputNumber];
            InputHiddenWeights = NeuralNetworkHandler.MakeMatrix(inputNumber, hiddenNumber);
            HiddenBiases = new double[hiddenNumber];
            HiddenOutputs = new double[hiddenNumber];
            HiddenOutputWeights = NeuralNetworkHandler.MakeMatrix(hiddenNumber, outputNumber);
            OutputBiases = new double[outputNumber];
            Outputs = new double[outputNumber];
        } // ctor

        public int InputNumber
        {
            get; set;
        }

        public int HiddenNumber
        {
            get; set;
        }

        public int OutputNumber
        {
            get; set;
        }

        public double[] Inputs
        {
            get; set;
        }
        public double[][] InputHiddenWeights
        {
            get; set;
        }

        public double[] HiddenBiases
        {
            get; set;
        }

        public double[] HiddenOutputs
        {
            get; set;
        }

        public double[][] HiddenOutputWeights
        {
            get; set;
        }

        public double[] OutputBiases
        {
            get; set;
        }

        public double[] Outputs
        {
            get; set;
        }

    }
}