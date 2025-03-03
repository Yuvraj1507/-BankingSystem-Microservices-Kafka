using BankAccountService.Common.Exceptions;
using BankAccountService.Common.Models;
using BankAccountService.Data;
using BankAccountService.Data.Entities;
using BankAccountService.Messaging.Send;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccountService.CommandsAndQueries.CreateTransfer
{
    public class SendMoneyTransferCommand : IRequest<ResponseModel>
    {
        public MoneyTransferModel Transfer { get; private set; }

        public SendMoneyTransferCommand(Guid accountId, MoneyTransferModel transfer)
        {
            Transfer = transfer;
            Transfer.SourceAccountId = accountId;
        }

        public class SendTransferCommandHandler : IRequestHandler<SendMoneyTransferCommand, ResponseModel>
        {
            private readonly IMoneyTransferModelSender moneyTransferModelSender;

            public SendTransferCommandHandler(IMoneyTransferModelSender moneyTransferModelSender)
            {
                this.moneyTransferModelSender = moneyTransferModelSender;
            }

            public async Task<ResponseModel> Handle(SendMoneyTransferCommand request, CancellationToken cancellationToken)
            {
                var moneyTransfer = request.Transfer;

                moneyTransferModelSender.SendMoneyTransferModel(moneyTransfer);

                return new SendMoneyTransferResponse(HttpStatusCode.OK);
            }
        }
    }
}
