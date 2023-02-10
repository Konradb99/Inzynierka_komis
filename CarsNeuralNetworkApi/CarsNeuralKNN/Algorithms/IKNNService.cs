using CarsNeuralCore.Dto;

namespace CarsNeuralKNN.Algorithm
{
    public interface IKNNService
    {
        public Task<PredictionResultDto> calculatePrediction(PredictDto predictionData);

        public Task<PredictionResultDto> calculatePredictionWithFilters(PredictDto predictionData);
    }
}