using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data
{
    public class DataContext: IdentityDbContext<Entities.Account>
    {

        public DbSet<Entities.Account> Accounts { get; set; }

        public DataContext(DbContextOptions<DataContext> options) :base(options) { }
    }
}
