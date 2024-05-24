using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;

public interface IWithdrawService
{
    IActionResult ProcessWithdraw(Event eventDetails, Dictionary<string, Account> accounts);
}

public class WithdrawService : IWithdrawService
{
    public IActionResult ProcessWithdraw(Event eventDetails, Dictionary<string, Account> accounts)
    {
        if (accounts.ContainsKey(eventDetails.Origin))
        {
            accounts[eventDetails.Origin].Balance -= eventDetails.Amount;
            return new CreatedResult("", new { origin = accounts[eventDetails.Origin] });
        }

        return new NotFoundObjectResult(0);
    }
}
