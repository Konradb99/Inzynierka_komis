using CarsNeuralNetworkAccurancyTester.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarsNeuralWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class AccurancyController : Controller
    {
        private readonly IAccurancyTesterService _accurancyTesterService;

        public AccurancyController(IAccurancyTesterService accurancyTesterService)
        {
            _accurancyTesterService = accurancyTesterService;
        }

        [HttpGet]
        public async Task GetAccurancy()
        {
            await _accurancyTesterService.getAccurancy();
        }
    }
}
