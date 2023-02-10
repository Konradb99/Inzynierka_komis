using CarsNeuralCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsNeuralNetwork.Services
{
    public interface INeuralNetworkService
    {
        Task<IList<double[]>> GetTrainSet();

        Task LearnDeepNeuralNetworkAsync();

        Task<PredictionResultDto> ReturnPredictionResult(PredictDto predictionData);
    }
}