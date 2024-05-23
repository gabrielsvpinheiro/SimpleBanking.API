using Microsoft.AspNetCore.Mvc;
using SimpleBanking.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBanking.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class BalanceController : ControllerBase
    {
        private readonly Dictionary<string, Account> _accounts;

        public BalanceController(Dictionary<string, Account> accounts)
        {
            _accounts = accounts;
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _accounts.Clear();
            return Ok();
        }

        [HttpGet("balance")]
        public IActionResult GetBalance([FromQuery] string account_id)
        {
            if (_accounts.ContainsKey(account_id))
            {
                return Ok(_accounts[account_id].Balance);
            }
            
            return NotFound(0);
        }

        [HttpPost("event")]
        public IActionResult PostEvent([FromBody] Event eventDetails)
        {
            switch (eventDetails.Type)
            {
                case "deposit":
                    if (!_accounts.ContainsKey(eventDetails.Destination))
                    {
                        _accounts[eventDetails.Destination] = new Account { Id = eventDetails.Destination, Balance = 0 };
                    }

                    _accounts[eventDetails.Destination].Balance += eventDetails.Amount;
                    return Created("", new { destination = _accounts[eventDetails.Destination] });

                case "withdraw":
                    if (_accounts.ContainsKey(eventDetails.Origin))
                    {
                        _accounts[eventDetails.Origin].Balance -= eventDetails.Amount;
                        return Created("", new { origin = _accounts[eventDetails.Origin] });
                    }
                    else
                    {
                        return NotFound(0);
                    }

                case "transfer":
                    if (_accounts.ContainsKey(eventDetails.Origin))
                    {
                        if (!_accounts.ContainsKey(eventDetails.Destination))
                        {
                            _accounts[eventDetails.Destination] = new Account { Id = eventDetails.Destination, Balance = 0 };
                        }

                        _accounts[eventDetails.Origin].Balance -= eventDetails.Amount;
                        _accounts[eventDetails.Destination].Balance += eventDetails.Amount;

                        return Created("", new
                        {
                            origin = _accounts[eventDetails.Origin],
                            destination = _accounts[eventDetails.Destination]
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