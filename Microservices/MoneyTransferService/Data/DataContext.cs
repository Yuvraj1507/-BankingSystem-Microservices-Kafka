using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankAccountService.MoneyTransfer.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entities.BankAccount> BankAccounts { get; set; }
        public DbSet<Entities.MoneyTransfer> MoneyTransfers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
