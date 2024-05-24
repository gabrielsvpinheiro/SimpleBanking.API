using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;

namespace SimpleBanking.API.Tests.Services
{
    public class WithdrawServiceTests
    {
        [Fact]
        public void ProcessWithdraw_Should_Return_CreatedResult()
        {
            var accounts = new Dictionary<string, Account> { { "123", new Account { Id = "123", Balance = 100 } } };
            var eventDetails = new Event { Type = "withdraw", Origin = "123", Amount = 50 };
            var withdrawalService = new WithdrawService();

            var result = withdrawalService.ProcessWithdraw(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result);
        }
    }
}
