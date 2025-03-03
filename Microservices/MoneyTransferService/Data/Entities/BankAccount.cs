using System;

namespace BankAccountService.MoneyTransfer.Data.Entities
{
    public class BankAccount
    {
        public Guid Id { get; set; }
        public float Balance { get; set; }

        public BankAccount()
        {
            ;
        }

        public BankAccount(Guid id)
        {
            Id = id;
            Balance = 100;
        }
    }
}
