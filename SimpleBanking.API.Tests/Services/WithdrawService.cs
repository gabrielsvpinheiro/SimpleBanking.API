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

        [Fact]
        public void ProcessWithdraw_Should_Update_Account_Balance_Correctly()
        {
            var accounts = new Dictionary<string, Account> { { "123", new Account { Id = "123", Balance = 100 } } };
            var eventDetails = new Event { Type = "withdraw", Origin = "123", Amount = 50 };
            var withdrawalService = new WithdrawService();

            var result = withdrawalService.ProcessWithdraw(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result);
            Assert.Equal(50, accounts["123"].Balance);
        }

        [Fact]
        public void ProcessWithdraw_Should_Return_NotFound_If_Account_Does_Not_Exist()
        {
            var accounts = new Dictionary<string, Account>();
            var eventDetails = new Event { Type = "withdraw", Origin = "123", Amount = 50 };
            var withdrawalService = new WithdrawService();

            var result = withdrawalService.ProcessWithdraw(eventDetails, accounts);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
