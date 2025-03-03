using System;
using System.Threading.Tasks;
using BankAccountService.CommandsAndQueries.CreateBankAccount;
using BankAccountService.CommandsAndQueries.GetAccountBalance;
using BankAccountService.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : Controller {

        private IMediator _mediator;

        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public IActionResult createBankAccount([FromHeader] Guid AccountId)
        {
            try
            {
                var response = _mediator.Send(new CreateBankAccountCommand(AccountId));
                return Ok(response.Result);
            } catch (UserAlreadyHaveBankAccountException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("balance")]
        public async Task<IActionResult> getAccountBalanceAsync([FromHeader] Guid AccountId)
        {
            try
            {
                var response = await _mediator.Send(new GetAccountBalanceQuery(AccountId));
                return Ok(response);
            } catch(BankAccountNotFoundException e)
            {
                return BadRequest(e.Message);
            }
          
        }

    }
}
