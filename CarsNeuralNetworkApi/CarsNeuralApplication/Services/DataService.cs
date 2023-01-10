using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Repositories;

namespace CarsNeuralApplication.Services
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _repository;

        public DataService(IDataRepository repository)
        {
            this._repository = repository;
        }

        public async Task InsertJson()
        {
            await _repository.InsertJson();
        }

        public async Task InsertTrain()
        {
            await _repository.InsertTrainJson();
        }

        public async Task InsertTest()
        {
            await _repository.InsertTestJson();
        }

        public async Task<IList<CarDto>> GetTrainData()
        {
            return await _repository.GetTrainData();
        }

        public async Task<IList<CarDto>> GetTestData()
        {
            return await _repository.GetTestData();
        }
    }
}