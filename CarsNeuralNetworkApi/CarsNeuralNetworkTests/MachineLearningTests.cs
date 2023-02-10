using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CarsNeuralNetworkTests
{
    public class MachineLearningTests
    {
        HttpClient client;
        WebApplicationFactory<Program> application;
        public MachineLearningTests()
        {
            application = new WebApplicationFactory<Program>();
            client = application.CreateClient();
        }

        [Fact]
        public async void knnPredictionTest()
        {
            PredictDto carToPredict = new PredictDto
            {
                BodyType = "Kompakt",
                DriveType = "Na przednie koła",
                GearboxType = "Automatyczna",
                FuelType = "Benzyna",
                Price = "13456",
                Distance = "164842",
                ProductionYear = "2004",
                Capacity = "1598"
            };

            var response = await client.GetAsync($"/KNN/Predict?BodyType={carToPredict.BodyType}&DriveType={carToPredict.DriveType}&GearboxType={carToPredict.GearboxType}&FuelType={carToPredict.FuelType}&Price={carToPredict.Price}&Distance={carToPredict.Distance}&ProductionYear={carToPredict.ProductionYear}&Capacity={carToPredict.Capacity}");
            var data = await response.Content.ReadAsStringAsync();
            PredictionResultDto result = JsonSerializer.Deserialize<PredictionResultDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Opel Astra", result.prefferedClass);
        }

        [Fact]
        public async void knnWithFiltersPredictionTest()
        {
            PredictDto carToPredict = new PredictDto
            {
                BodyType = "Kompakt",
                DriveType = "Na przednie koła",
                GearboxType = "Automatyczna",
                FuelType = "Benzyna",
                Price = "13456",
                Distance = "164842",
                ProductionYear = "2004",
                Capacity = "1598"
            };

            var response = await client.GetAsync($"/KNN/PredictWithFilters?BodyType={carToPredict.BodyType}&DriveType={carToPredict.DriveType}&GearboxType={carToPredict.GearboxType}&FuelType={carToPredict.FuelType}&Price={carToPredict.Price}&Distance={carToPredict.Distance}&ProductionYear={carToPredict.ProductionYear}&Capacity={carToPredict.Capacity}");
            var data = await response.Content.ReadAsStringAsync();
            PredictionResultDto result = JsonSerializer.Deserialize<PredictionResultDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Peugeot 308", result.prefferedClass);
        }

        [Fact]
        public async void neuralNetworkPredictionTest()
        {
            PredictDto carToPredict = new PredictDto
            {
                BodyType = "Kompakt",
                DriveType = "Na wszystkie koła",
                GearboxType = "Manualna",
                FuelType = "Benzyna",
                Price = "18000",
                Distance = "204222",
                ProductionYear = "2005",
                Capacity = "1984"
            };

            var response = await client.GetAsync($"/NeuralNetwork/ReturnCarClass?BodyType={carToPredict.BodyType}&DriveType={carToPredict.DriveType}&GearboxType={carToPredict.GearboxType}&FuelType={carToPredict.FuelType}&Price={carToPredict.Price}&Distance={carToPredict.Distance}&ProductionYear={carToPredict.ProductionYear}&Capacity={carToPredict.Capacity}");
            var data = await response.Content.ReadAsStringAsync();
            PredictionResultDto result = JsonSerializer.Deserialize<PredictionResultDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Audi A3", result.prefferedClass);
        }
    }
}
