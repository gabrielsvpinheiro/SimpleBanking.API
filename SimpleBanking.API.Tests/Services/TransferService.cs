using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;

namespace SimpleBanking.API.Tests.Services
{
    public class TransferServiceTests
    {
        [Fact]
        public void ProcessTransfer_Should_Return_CreatedResult()
        {
            var accounts = new Dictionary<string, Account> { { "123", new Account { Id = "123", Balance = 100 } } };
            var eventDetails = new Event { Type = "transfer", Origin = "123", Destination = "456", Amount = 50 };
            var transferService = new TransferService();

            var result = transferService.ProcessTransfer(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result);
        }
    }
}
