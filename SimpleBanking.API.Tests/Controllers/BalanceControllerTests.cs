using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;
using SimpleBanking.API.Controllers;

namespace SimpleBanking.API.Tests.Controllers
{
    public class BalanceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly BalanceController _client;

        public BalanceControllerTests() 
        {
            _client = new BalanceController();
        }

        [Fact]
        public void ShouldReturnOkWithCorrectBalanceWhenAccountExists()
        {
            var account_id = "1234";
            var accounts = new Dictionary<string, Account>
            {
                { account_id, new Account { Balance = 100 } }
            };
            _client.SetAccounts(accounts);

            var result = _client.GetBalance(account_id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(100, okResult.Value);
        }

        [Fact]
        public void ShouldReturnNotFoundWhenAccountDoesNotExist()
        {
            var account_id = "456";
            var accounts = new Dictionary<string, Account>();
            _client.SetAccounts(accounts);

            var result = _client.GetBalance(account_id);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
