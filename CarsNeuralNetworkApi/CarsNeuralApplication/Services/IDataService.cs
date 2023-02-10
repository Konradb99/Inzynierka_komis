using CarsNeuralCore.Dto;

namespace CarsNeuralApplication.Services
{
    public interface IDataService
    {
        public Task InsertJson();

        public Task InsertTrain();

        public Task InsertTest();

        public Task<IList<CarDto>> GetTrainData();

        public Task<IList<CarDto>> GetTestData();
    }
}