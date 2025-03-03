using System;
using System.IO;
using System.Threading.Tasks;
using BankAccountService.CommandsAndQueries.CreateTransfer;
using BankAccountService.Common.Exceptions;
using BankAccountService.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Aspose.Cells;
using BankAccountService.CommandsAndQueries.GetMoneyTransferReport;

namespace BankAccountService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoneyTransferController : Controller
    {

        private IMediator _mediator;

        public MoneyTransferController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> makeTransfer([FromHeader] Guid AccountId, [FromBody] MoneyTransferModel transfer)
        {
            try
            {
                var response = await _mediator.Send(new SendMoneyTransferCommand(AccountId, transfer));
                return Ok(response);
            }
            catch (BankAccountNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (BalanceTooLowException e)
            {
                return BadRequest(e.Message);
            }
            catch (TransferToSameAccountException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("report")]
        public async Task<IActionResult> getReport([FromHeader] Guid AccountId, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                var response = await _mediator.Send(new GetMoneyTransferReportQuery(AccountId, from, to));
                var file = ((GetAccountBalanceResponseData)response.Data).File;
                var cd = new ContentDispositionHeaderValue("attachment")
                {
                    FileNameStar = "report.xlsx"
                };
                Response.Headers.Add(HeaderNames.ContentDisposition, cd.ToString());
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (BankAccountNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
