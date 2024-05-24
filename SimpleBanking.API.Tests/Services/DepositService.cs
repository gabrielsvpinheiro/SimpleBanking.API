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

        [Fact]
        public void ProcessDeposit_Should_Update_Existing_Account_Balance()
        {
            var accounts = new Dictionary<string, Account>
            {
                { "123", new Account { Id = "123", Balance = 100 } }
            };
            var eventDetails = new Event { Type = "deposit", Destination = "123", Amount = 50 };
            var depositService = new DepositService();

            var result = depositService.ProcessDeposit(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result);
            Assert.Equal(150, accounts["123"].Balance);
        }

        [Fact]
        public void ProcessDeposit_Should_Add_New_Account_And_Update_Balance()
        {
            var accounts = new Dictionary<string, Account>();
            var eventDetails = new Event { Type = "deposit", Destination = "456", Amount = 200 };
            var depositService = new DepositService();

            var result = depositService.ProcessDeposit(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result);
            Assert.True(accounts.ContainsKey("456"));
            Assert.Equal(200, accounts["456"].Balance);
        }
    }
}
