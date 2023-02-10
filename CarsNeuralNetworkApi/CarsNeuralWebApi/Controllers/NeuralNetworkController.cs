using CarsNeuralCore.Dto;
using Microsoft.AspNetCore.Mvc;
using CarsNeuralNetwork.Services;

namespace CarsNeuralWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class NeuralNetworkController : Controller
    {
        private readonly INeuralNetworkService _neuralNetworkService;

        public NeuralNetworkController(INeuralNetworkService neuralNetworkService)
        {
            _neuralNetworkService = neuralNetworkService;
        }

        [HttpGet]
        public async Task TrainDeepNeuralNetwork()
        {
            await _neuralNetworkService.LearnDeepNeuralNetworkAsync();
        }

        [HttpGet]
        public async Task<PredictionResultDto> ReturnCarClass([FromQuery] PredictDto predictionData)
        {
            return await _neuralNetworkService.ReturnPredictionResult(predictionData);
        }
    }
}