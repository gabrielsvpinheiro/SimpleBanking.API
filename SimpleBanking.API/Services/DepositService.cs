using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;

public interface IDepositService
{
    IActionResult ProcessDeposit(Event eventDetails, Dictionary<string, Account> accounts);
}

public class DepositService : IDepositService
{
    public IActionResult ProcessDeposit(Event eventDetails, Dictionary<string, Account> accounts)
    {
        if (!accounts.ContainsKey(eventDetails.Destination))
        {
            accounts[eventDetails.Destination] = new Account { Id = eventDetails.Destination, Balance = 0 };
        }

        accounts[eventDetails.Destination].Balance += eventDetails.Amount;
        return new CreatedResult("", new { destination = accounts[eventDetails.Destination] });
    }
}
