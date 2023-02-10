using CarsNeuralCore.Constants;
using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Mappers;
using CarsNeuralInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CarsNeuralInfrastructure.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly CarsNeuralDbContext _dbContext;

        public DataRepository(CarsNeuralDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<CarDto>> GetTestData()
        {
            IList<CarTest> cars = await _dbContext.TestSet.Take(1500).ToListAsync();
            IList<CarDto> testResult = new List<CarDto>();
            foreach (var car in cars)
            {
                testResult.Add(CarsMapper.CarTestDtoMapper(car));
            }
            return testResult;
        }

        public async Task<IList<CarDto>> GetTrainData()
        {
            IList<CarTrain> cars = await _dbContext.TrainSet.ToListAsync();
            IList<CarDto> trainResult = new List<CarDto>();
            foreach (var car in cars)
            {
                trainResult.Add(CarsMapper.CarTrainDtoMapper(car));
            }
            return trainResult;
        }

        public async Task InsertJson()
        {
            List<Car> items = new List<Car>();
            using (StreamReader r = new StreamReader(FilePathConstants.mainFilePath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Car>>(json);
            }
            foreach (var item in items)
            {
                await _dbContext.Cars.AddAsync(item);
            }
            await _dbContext.SaveChanges();
        }

        public async Task InsertTrainJson()
        {
            List<CarTrain> items = new List<CarTrain>();
            using (StreamReader r = new StreamReader(FilePathConstants.trainFilePath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<CarTrain>>(json);
            }
            foreach (var item in items)
            {
                await _dbContext.TrainSet.AddAsync(item);
            }
            await _dbContext.SaveChanges();
        }

        public async Task InsertTestJson()
        {
            List<CarTest> items = new List<CarTest>();
            using (StreamReader r = new StreamReader(FilePathConstants.testFilePath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<CarTest>>(json);
            }
            foreach (var item in items)
            {
                await _dbContext.TestSet.AddAsync(item);
            }
            await _dbContext.SaveChanges();
        }
    }
}