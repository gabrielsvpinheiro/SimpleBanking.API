using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;
using SimpleBanking.API.Controllers;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;

namespace SimpleBanking.API.Tests.Controllers
{
    public class BalanceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly BalanceController _controller;
        private readonly HttpClient _client;

        public BalanceControllerTests(WebApplicationFactory<Program> factory)
        {
            var accounts = new Dictionary<string, Account>
            {
                { "1234", new Account { Id = "1234", Balance = 100 } }
            };
            _controller = new BalanceController(accounts);
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Reset_ReturnsOk()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/reset");

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode(); 
            Assert.Equal("OK", response.ReasonPhrase);
        }

        [Fact]
        public void ShouldReturnOkWithCorrectBalanceWhenAccountExists()
        {
            var account_id = "1234";

            var result = _controller.GetBalance(account_id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(100, okResult.Value);
        }

        [Fact]
        public void ShouldReturnNotFoundWhenAccountDoesNotExist()
        {
            var account_id = "456";

            var result = _controller.GetBalance(account_id);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task TestTransferFromNonExistingAccount()
        {
            var json = JsonConvert.SerializeObject(new { type = "transfer", origin = "200", amount = 15, destination = "300" });
            var response = await _client.PostAsync("/event", new StringContent(json, Encoding.UTF8, "application/json"));
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("0", content);
        }
    }
}
