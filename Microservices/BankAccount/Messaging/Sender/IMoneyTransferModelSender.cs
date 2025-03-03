using BankAccountService.Common.Models;
using System;

namespace BankAccountService.Messaging.Send
{
    public interface IMoneyTransferModelSender
    {
        void SendMoneyTransferModel(MoneyTransferModel moneyTransferModel);
    }
}