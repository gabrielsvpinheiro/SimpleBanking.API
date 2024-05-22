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
    }
}
