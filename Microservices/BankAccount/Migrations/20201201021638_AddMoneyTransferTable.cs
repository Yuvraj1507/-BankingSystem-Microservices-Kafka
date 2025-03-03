using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BankAccountService.Migrations
{
    public partial class AddMoneyTransferTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyTransfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountFrom = table.Column<Guid>(nullable: false),
                    AccountTo = table.Column<Guid>(nullable: false),
                    Amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyTransfers", x => x.Id);
                    table.ForeignKey("PK_AccountFrom", x => x.AccountFrom, "BankAccounts", "Id");
                    table.ForeignKey("PK_AccountTo", x => x.AccountTo, "BankAccounts", "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyTransfers");
        }
    }
}
