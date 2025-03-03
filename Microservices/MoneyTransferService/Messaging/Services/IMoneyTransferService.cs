using BankAccountService.MoneyTransfer.Common.Models;

namespace BankAccountService.MoneyTransfer.Messaging.Services
{
    public interface IMoneyTransferService
    {
        void TransferMoney(MoneyTransferModel moneyTransferModel);
    }
}