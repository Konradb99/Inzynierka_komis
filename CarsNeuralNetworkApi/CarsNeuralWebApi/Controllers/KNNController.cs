using CarsNeuralCore.Dto;
using CarsNeuralKNN.Algorithm;
using Microsoft.AspNetCore.Mvc;

namespace CarsNeuralWebApi.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class KNNController : Controller
    {
        private readonly IKNNService _kNNService;

        public KNNController(IKNNService kNNService)
        {
            _kNNService = kNNService;
        }

        [HttpGet]
        public async Task<PredictionResultDto> PredictWithFilters([FromQuery] PredictDto predictionData)
        {
            return await _kNNService.calculatePredictionWithFilters(predictionData);
        }

        [HttpGet]
        public async Task<PredictionResultDto> Predict([FromQuery] PredictDto predictionData)
        {
            return await _kNNService.calculatePrediction(predictionData);
        }
    }
}