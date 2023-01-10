using CarsNeuralCore.Dto;

namespace CarsNeuralInfrastructure.Repositories
{
    public interface IDataRepository
    {
        public Task InsertJson();

        public Task InsertTrainJson();

        public Task InsertTestJson();

        public Task<IList<CarDto>> GetTrainData();

        public Task<IList<CarDto>> GetTestData();
    }
}