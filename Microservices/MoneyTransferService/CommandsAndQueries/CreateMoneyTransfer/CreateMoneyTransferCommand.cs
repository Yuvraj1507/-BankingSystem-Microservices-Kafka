using BankAccountService.MoneyTransfer.Common.Models;
using BankAccountService.MoneyTransfer.Data;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BankAccountService.MoneyTransfer.Common.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccountService.MoneyTransfer.CommandsAndQueries.CreateMoneyTransfer
{
    public class CreateMoneyTransferCommand : IRequest
    {
        public MoneyTransferModel Transfer { get; set; }

        public class CreateMoneyTransferCommandHandler : IRequestHandler<CreateMoneyTransferCommand>
        {
            private IServiceScopeFactory serviceScopeFactory;

            public CreateMoneyTransferCommandHandler(IServiceScopeFactory serviceScopeFactory)
            {
                this.serviceScopeFactory = serviceScopeFactory;
            }


            public async Task<Unit> Handle(CreateMoneyTransferCommand request, CancellationToken cancellationToken)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dataContext = scopedServices.GetRequiredService<DataContext>();

                    var moneyTransfer = request.Transfer;
                    if (moneyTransfer.SourceAccountId == moneyTransfer.TargetAccountId)
                    {
                        throw new TransferToSameAccountException();
                    }

                    var accountFrom = dataContext.BankAccounts.Find(moneyTransfer.SourceAccountId);
                    var accountTo = dataContext.BankAccounts.Find(moneyTransfer.TargetAccountId);
                    if (accountFrom == null || accountTo == null)
                    {
                        throw new BankAccountNotFoundException();
                    }
                    if (moneyTransfer.Amount > accountFrom.Balance)
                    {
                        throw new BalanceTooLowException();
                    }
                    accountFrom.Balance -= moneyTransfer.Amount;
                    accountTo.Balance += moneyTransfer.Amount;
                    var newMoneyTransfer = new Data.Entities.MoneyTransfer(moneyTransfer.Title, accountFrom.Id, accountTo.Id, moneyTransfer.Amount, DateTime.Now);
                    dataContext.MoneyTransfers.Add(newMoneyTransfer);
                    dataContext.BankAccounts.Update(accountFrom);
                    dataContext.BankAccounts.Update(accountTo);
                    dataContext.SaveChanges();

                    return Unit.Value;
                }
            }
        }
    }
}
