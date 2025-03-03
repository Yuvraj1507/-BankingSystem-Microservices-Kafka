using System;

namespace BankAccountService.MoneyTransfer.Data.Entities
{
    public class MoneyTransfer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Guid AccountFrom { get; set; }
        public Guid AccountTo { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }

        public MoneyTransfer()
        {
        }

        public MoneyTransfer(string title, Guid accountFrom, Guid accountTo, float amount, DateTime date)
        {
            Title = title;
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            Amount = amount;
            Date = date;
        }
    }
}
