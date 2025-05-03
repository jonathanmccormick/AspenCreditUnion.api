using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountAndLoanModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutoRenew = table.Column<bool>(type: "bit", nullable: true),
                    MoneyMarketAccount_InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransactionsPerMonth = table.Column<int>(type: "int", nullable: true),
                    SavingsAccount_InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LoanType = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    VehicleVin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleMake = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleYear = table.Column<int>(type: "int", nullable: true),
                    CreditCardLoan_CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AnnualFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RewardProgram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrentEquity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DrawPeriodMonths = table.Column<int>(type: "int", nullable: true),
                    MortgageLoan_PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MortgageLoan_PropertyValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoanTermYears = table.Column<int>(type: "int", nullable: true),
                    IsFixedRate = table.Column<bool>(type: "bit", nullable: true),
                    PersonalLineOfCreditLoan_CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PersonalLineOfCreditLoan_DrawPeriodMonths = table.Column<int>(type: "int", nullable: true),
                    IsSecured = table.Column<bool>(type: "bit", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanTermMonths = table.Column<int>(type: "int", nullable: true),
                    PersonalLoan_IsSecured = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
