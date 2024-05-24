using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;

public interface ITransferService
{
    IActionResult ProcessTransfer(Event eventDetails, Dictionary<string, Account> accounts);
}

public class TransferService : ITransferService
{
    public IActionResult ProcessTransfer(Event eventDetails, Dictionary<string, Account> accounts)
    {
        if (accounts.ContainsKey(eventDetails.Origin))
        {
            if (!accounts.ContainsKey(eventDetails.Destination))
            {
                accounts[eventDetails.Destination] = new Account { Id = eventDetails.Destination, Balance = 0 };
            }

            if (accounts[eventDetails.Origin].Balance >= eventDetails.Amount)
            {
                accounts[eventDetails.Origin].Balance -= eventDetails.Amount;
                accounts[eventDetails.Destination].Balance += eventDetails.Amount;

                return new CreatedResult("", new
                {
                    origin = accounts[eventDetails.Origin],
                    destination = accounts[eventDetails.Destination]
                });
            }

            return new BadRequestObjectResult("Insufficient balance in the origin account.");
        }

        return new NotFoundObjectResult(0);
    }
}
