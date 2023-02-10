using CarsNeuralApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsNeuralWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class DataController : Controller
    {
        private readonly IDataService _service;

        public DataController(IDataService service)
        {
            _service = service;
        }

        [HttpPost]
        public async void InsertFromJson()
        {
            await _service.InsertJson();
        }

        [HttpPost]
        public async Task InsertTrainData()
        {
            await _service.InsertTrain();
        }

        [HttpPost]
        public async Task InsertTestData()
        {
            await _service.InsertTest();
        }
    }
}