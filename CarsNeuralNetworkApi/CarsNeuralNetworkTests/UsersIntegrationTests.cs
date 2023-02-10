using CarsNeuralCore.Dto;
using CarsNeuralInfrastructure.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CarsNeuralNetworkTests
{
    public class UsersIntegrationTests
    {

        HttpClient client;
        WebApplicationFactory<Program> application;
        public UsersIntegrationTests()
        {
            application = new WebApplicationFactory<Program>();
            client = application.CreateClient();
        }

        [Fact]
        public async void registerUser()
        {
            RegisterUserDto newUser = new RegisterUserDto
            {
                username = "username",
                password = "password",
                repeatPassword = "password"
            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User/RegisterUser", httpContent);

            var data = await response.Content.ReadAsStringAsync();
            bool result = JsonSerializer.Deserialize<bool>(data);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result, true);

            LoginUserDto userToLogin = new LoginUserDto
            {
                Username = "username",
                Password = "password"
            };

            response = await client.GetAsync($"/User/LoginUser?username={userToLogin.Username}&password={userToLogin.Password}");
            data = await response.Content.ReadAsStringAsync();
            UserDto loginResult = JsonSerializer.Deserialize<UserDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);            
            Assert.Equal(newUser.username, loginResult.Username);
        }

        [Fact]
        public async void registerExistingUser()
        {
            RegisterUserDto newUser = new RegisterUserDto
            {
                username = "username_existing",
                password = "password",
                repeatPassword = "password"
            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User/RegisterUser", httpContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            response = await client.PostAsync("/User/RegisterUser", httpContent);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async void loginUser()
        {
            await registerUserForLogin("username_goodPassword");

            LoginUserDto userToLogin = new LoginUserDto
            {
                Username = "username_goodPassword",
                Password = "password"
            };

            var response = await client.GetAsync($"/User/LoginUser?username={userToLogin.Username}&password={userToLogin.Password}");
            var data = await response.Content.ReadAsStringAsync();
            UserDto loginResult = JsonSerializer.Deserialize<UserDto>(data,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(loginResult.Username, userToLogin.Username);
            Assert.Equal(loginResult.Role, "Employee");
        }

        [Fact]
        public async void loginUser_NotExisting()
        {
            LoginUserDto userToLogin = new LoginUserDto
            {
                Username = "username_notexisting",
                Password = "password"
            };

            var response = await client.GetAsync($"/User/LoginUser?username={userToLogin.Username}&password={userToLogin.Password}");
            var data = await response.Content.ReadAsStringAsync();

            string expectedError = "System.Exception: Not authorized";

            int index = data.IndexOfAny(new char[] { '\r', '\n' });
            string resultError = index == -1 ? data : data.Substring(0, index);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal(resultError, expectedError);
        }

        [Fact]
        public async void loginUser_WrongPassword()
        {
            await registerUserForLogin("username_badPassword"); 

            LoginUserDto userToLogin = new LoginUserDto
            {
                Username = "username_badPassword",
                Password = "password1234"
            };

            var response = await client.GetAsync($"/User/LoginUser?username={userToLogin.Username}&password={userToLogin.Password}");
            var data = await response.Content.ReadAsStringAsync();

            string expectedError = "System.Exception: Not authorized";

            int index = data.IndexOfAny(new char[] { '\r', '\n' });
            string resultError = index == -1 ? data : data.Substring(0, index);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal(resultError, expectedError);
        }

        private async Task registerUserForLogin(string username)
        {
            RegisterUserDto newUser = new RegisterUserDto
            {
                username = username,
                password = "password",
                repeatPassword = "password"
            };
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(newUser);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/User/RegisterUser", httpContent);
            var data = await response.Content.ReadAsStringAsync();
            bool result = JsonSerializer.Deserialize<bool>(data);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result, true);
        }
    }
}