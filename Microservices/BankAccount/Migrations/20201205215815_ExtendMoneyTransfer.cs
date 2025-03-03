using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccountService.Migrations
{
    public partial class ExtendMoneyTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "MoneyTransfers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MoneyTransfers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MoneyTransfers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "MoneyTransfers");
        }
    }
}
