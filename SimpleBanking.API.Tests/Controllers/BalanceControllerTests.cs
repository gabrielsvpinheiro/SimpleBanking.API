using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;
using SimpleBanking.API.Controllers;

namespace SimpleBanking.API.Tests.Controllers
{
    public class BalanceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly BalanceController _controller;

        public BalanceControllerTests()
        {
            var accounts = new Dictionary<string, Account>
            {
                { "1234", new Account { Id = "1234", Balance = 100 } }
            };
            _controller = new BalanceController(accounts);
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
    }
}
