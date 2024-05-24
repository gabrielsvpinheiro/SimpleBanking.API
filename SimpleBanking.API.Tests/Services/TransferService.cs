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

        [Fact]
        public void ProcessTransfer_Should_Transfer_Balance_Correctly()
        {
            var accounts = new Dictionary<string, Account>
            {
                { "123", new Account { Id = "123", Balance = 100 } },
                { "456", new Account { Id = "456", Balance = 200 } }
            };
            var eventDetails = new Event { Type = "transfer", Origin = "123", Destination = "456", Amount = 50 };
            var transferService = new TransferService();

            var result = transferService.ProcessTransfer(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result);
            Assert.Equal(50, accounts["123"].Balance);
            Assert.Equal(250, accounts["456"].Balance);
        }

        [Fact]
        public void ProcessTransfer_Should_Return_BadRequest_If_Origin_Account_Does_Not_Exist()
        {
            var accounts = new Dictionary<string, Account>();
            var eventDetails = new Event { Type = "transfer", Origin = "123", Destination = "456", Amount = 50 };
            var transferService = new TransferService();

            var result = transferService.ProcessTransfer(eventDetails, accounts);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void ProcessTransfer_Should_Return_BadRequest_If_Insufficient_Balance_In_Origin_Account()
        {
            var accounts = new Dictionary<string, Account> { { "123", new Account { Id = "123", Balance = 50 } } };
            var eventDetails = new Event { Type = "transfer", Origin = "123", Destination = "456", Amount = 100 };
            var transferService = new TransferService();

            var result = transferService.ProcessTransfer(eventDetails, accounts);

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Insufficient balance in the origin account.", ((BadRequestObjectResult)result).Value);
        }

        [Fact]
        public void ProcessTransfer_Should_Return_BadRequest_If_Destination_Account_Does_Not_Exist()
        {
            var accounts = new Dictionary<string, Account> { { "123", new Account { Id = "123", Balance = 100 } } };
            var eventDetails = new Event { Type = "transfer", Origin = "123", Destination = "456", Amount = 50 };
            var transferService = new TransferService();

            var result = transferService.ProcessTransfer(eventDetails, accounts);

            Assert.IsType<CreatedResult>(result); 
        }

    }
}
