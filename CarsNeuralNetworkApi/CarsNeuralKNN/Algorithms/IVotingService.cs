using CarsNeuralCore.Dto;

namespace CarsNeuralKNN.Algorithms
{
    public interface IVotingService
    {
        public Task<PredictionResultDto> VoteAlgorithm(int neighboursCount, List<double> distances, IList<CarDto> trainSet);
    }
}