using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Repositories;

namespace CarsNeuralKNN.Algorithms
{
    public class VotingService : IVotingService
    {
        private readonly ICarsRepository _carsRepository;

        public VotingService(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public async Task<PredictionResultDto> VoteAlgorithm(int neighboursCount, List<double> distances, IList<CarDto> trainSet)
        {
            for (int i = 0; i < neighboursCount; i++)
            {
                for (int j = i; j < distances.Count; j++)
                {
                    if (distances[i] > distances[j])
                    {
                        double temponaryDistance = distances[i];
                        distances[i] = distances[j];
                        distances[j] = temponaryDistance;

                        CarDto temponaryCarDto = trainSet.ElementAt(i);
                        CarDto elementToAdd = trainSet.ElementAt(j);
                        trainSet.RemoveAt(i);
                        trainSet.Insert(i, elementToAdd);
                        trainSet.RemoveAt(j);
                        trainSet.Insert(j, temponaryCarDto);
                    }
                }
            }

            Dictionary<string, int> voting = new Dictionary<string, int>();

            ICollection<CarDto> voteSet = trainSet.Take(neighboursCount).ToList();

            foreach (var car in voteSet)
            {
                string carClass = $"{car.Brand} {car.Model}";
                int currentVoteCount;
                voting.TryGetValue(carClass, out currentVoteCount);
                voting[carClass] = currentVoteCount + 1;
            }

            string prefferedClass = voting.MaxBy(item => item.Value).Key;

            ICollection<CarDto> resultSet = trainSet.Take(neighboursCount).ToList();
            ICollection<CarDto> prefferedCars = new List<CarDto>();

            foreach (var item in resultSet)
            {
                CarDto newCar = await _carsRepository.GetCarByTrainData(item);
                if (newCar != null)
                {
                    prefferedCars.Add(newCar);
                }
            }

            PredictionResultDto result = new PredictionResultDto { prefferedClass = prefferedClass, prefferedCars = prefferedCars };

            return result;
        }
    }
}