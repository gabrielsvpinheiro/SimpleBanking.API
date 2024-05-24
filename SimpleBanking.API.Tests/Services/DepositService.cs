using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;
using SimpleBanking.API.Controllers;

namespace SimpleBanking.API.Tests.Services
{
    public class DepositServiceTests
    {
        [Fact]
        public void ProcessDeposit_Should_Return_CreatedResult()
        {
            var accounts = new Dictionary<string, Account>();
            var eventDetails = new Event { Type = "deposit", Destination = "123", Amount = 100 };
            var depositService = new DepositService();

            var result = depositService.ProcessDeposit(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result);
        }
    }
}
