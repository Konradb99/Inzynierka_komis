using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CarsNeuralNetworkTests
{
    public class CarsIntegrationTests
    {

        HttpClient client;
        WebApplicationFactory<Program> application;
        public CarsIntegrationTests()
        {
            application = new WebApplicationFactory<Program>();
            client = application.CreateClient();
        }

        [Fact]
        public async void GetCarsPage()
        {
            var testCars = GetTestCars();
            var response = await client.GetAsync("/Cars/GetByPage?pageNumber=1");
            var data = await response.Content.ReadAsStringAsync();
            CarPageDto result = JsonSerializer.Deserialize<CarPageDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Data.Count, testCars.Count());
        }

        [Fact]
        public async void GetCarsPageWithFilters()
        {
            var testCars = GetTestCars();
            var response = await client.GetAsync("/Cars/GetFilteredCars?pageNumber=1&Brand=Audi");
            var data = await response.Content.ReadAsStringAsync();
            CarPageDto result = JsonSerializer.Deserialize<CarPageDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Data.Count, testCars.Count());
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(result.Data.ElementAt(i).Brand, "Audi");
            }
        }

        [Fact]
        public async void GetCarsPageWithFilters_ShouldBeEmpty()
        {
            var response = await client.GetAsync("/Cars/GetFilteredCars?pageNumber=3000");
            var data = await response.Content.ReadAsStringAsync();
            CarPageDto result = JsonSerializer.Deserialize<CarPageDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(0, result.Data.Count);
        }

        [Fact]
        public async void  InsertCar()
        {
            Car newCar = new Car(
                "Audi",
                "A3",
                "Sedan",
                "Na wszystkie ko³a",
                "Automatyczna",
                "Diesel",
                150000,
                15000,
                2022,
                1993);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(newCar);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Cars/InsertCar", httpContent);
            var data = await response.Content.ReadAsStringAsync();

            Car result = JsonSerializer.Deserialize<Car>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private ICollection<CarDto> GetTestCars()
        {
            var testCars = new List<CarDto>();
            testCars.Add(new CarDto { Brand = "Audi", Model = "A3" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "A4" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "A5" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "A3" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "Q5" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "E-Tron" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "A3" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "A4" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "A5" });
            testCars.Add(new CarDto { Brand = "Audi", Model = "Q5" });

            return testCars;
        }
    }
}