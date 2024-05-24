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
        private readonly IDepositService _depositService;
        private readonly IWithdrawService _withdrawService;
        private readonly ITransferService _transferService;

        public BalanceController(Dictionary<string, Account> accounts, IDepositService depositService, IWithdrawService withdrawService, ITransferService transferService)
        {
            _accounts = accounts;
            _depositService = depositService;
            _withdrawService = withdrawService;
            _transferService = transferService;
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _accounts.Clear();
            return Ok("OK");
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
            const string eventDeposit = "deposit";
            const string eventWithdraw = "withdraw";
            const string eventTransfer = "transfer";

            string eventType = eventDetails.Type;

            switch (eventType)
            {
                case eventDeposit:
                    return _depositService.ProcessDeposit(eventDetails, _accounts);

                case eventWithdraw:
                    return _withdrawService.ProcessWithdraw(eventDetails, _accounts);

                case eventTransfer:
                    return _transferService.ProcessTransfer(eventDetails, _accounts);

                default:
                    return BadRequest();
            }
        }
    }
}
