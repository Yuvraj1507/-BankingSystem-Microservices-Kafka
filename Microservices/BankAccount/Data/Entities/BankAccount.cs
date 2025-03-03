using System;
using Microsoft.AspNetCore.Identity;

namespace BankAccountService.Data.Entities
{
    public class BankAccount
    {
        public Guid Id { get; set; }
        public float Balance { get; set; }

        public BankAccount(Guid id)
        {
            Id = id;
            Balance = 100;
        }
    }
}
