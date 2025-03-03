using System;
using System.Diagnostics;
using BankAccountService.MoneyTransfer.CommandsAndQueries.CreateMoneyTransfer;
using BankAccountService.MoneyTransfer.Common.Exceptions;
using BankAccountService.MoneyTransfer.Common.Models;
using MediatR;

namespace BankAccountService.MoneyTransfer.Messaging.Services
{
    public class MoneyTransferService : IMoneyTransferService
    {
        private readonly IMediator _mediator;

        public MoneyTransferService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void TransferMoney(MoneyTransferModel moneyTransferModel)
        {
            try
            {
                var response = await _mediator.Send(new CreateMoneyTransferCommand
                {
                    Transfer = moneyTransferModel
                });
            }
            catch (BankAccountNotFoundException e)
            {
                Debug.WriteLine(e.Message);
            }
            catch (BalanceTooLowException e)
            {
                Debug.WriteLine(e.Message);
            }
            catch (TransferToSameAccountException e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}