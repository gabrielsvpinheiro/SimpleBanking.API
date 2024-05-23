using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;

namespace SimpleBanking.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class BalanceController : ControllerBase
    {
        private static Dictionary<string, Account> accounts = new Dictionary<string, Account>();

        [HttpGet("balance")]
        public IActionResult GetBalance([FromQuery] string account_id) 
        { 
            if (accounts.ContainsKey(account_id))
            {
                return Ok(accounts[account_id].Balance);
            }
            else
            {
                return NotFound(0);
            }
        }

        [HttpPost("event")]
        public IActionResult PostEvent([FromBody] Event eventDetails)
        {
            switch (eventDetails.Type)
            {
                case "deposit": 
                    if (!accounts.ContainsKey(eventDetails.Destination))
                    {
                        accounts[eventDetails.Destination] = new Account();
                    }

                    accounts[eventDetails.Destination].Balance += eventDetails.Amount;
                    return Created("", new { destination = accounts[eventDetails.Destination] });

                case "withdraw":
                    if (accounts.ContainsKey(eventDetails.Origin)) 
                    {
                        accounts[eventDetails.Origin].Balance -= eventDetails.Amount;
                        return Created("", new { origin = accounts[eventDetails.Origin] });
                    }
                    else
                    {
                        return NotFound(0);
                    }

                case "transfer":
                    if (accounts.ContainsKey(eventDetails.Origin))
                    {
                        if (!accounts.ContainsKey(eventDetails.Destination))
                        {
                            accounts[eventDetails.Destination] = new Account { Id = eventDetails.Destination, Balance = 0 };
                        }

                        accounts[eventDetails.Origin].Balance -= eventDetails.Amount;
                        accounts[eventDetails.Destination].Balance = eventDetails.Amount;

                        return Created("", new
                        {
                            origin = accounts[eventDetails.Origin],
                            destination = accounts[eventDetails.Destination]
                        });
                    }
                    else
                    {
                        return NotFound(0);
                    }
                default:
                    return BadRequest();
            }
        }
    }
}
