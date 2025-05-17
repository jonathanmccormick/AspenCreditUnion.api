using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspenCreditUnion.api.Migrations
{
    /// <inheritdoc />
    public partial class EnhancedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyMarketAccount_InterestRate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SavingsAccount_InterestRate",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalAmount",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "OriginalCurrency",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SettlementDate",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualFee",
                table: "Loans",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AdjustableRateMargin",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllowsChecks",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllowsConversion",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AllowsExtraPayments",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmortizationType",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ApplicationFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AutoPay",
                table: "Loans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AutoPayEnabled",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AutoPayFromAccountId",
                table: "Loans",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AutoPayType",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AvailableCredit",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BalloonPaymentAmount",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CashAdvanceFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CashAdvanceInterestRate",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CashAdvanceLimit",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoSignerId",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CollateralDescription",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CollateralValue",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CombinedLoanToValueRatio",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CreditCardLoan_AnnualFee",
                table: "Loans",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Loans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentCashbackBalance",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerAddress",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DealerName",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DownPayment",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DrawPeriodEndDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EscrowBalance",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EscrowEnabled",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ExtendedWarranty",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstAdjustmentDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FirstMortgageBalance",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstPaymentDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ForeignTransactionFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GapInsurance",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GracePeriodDays",
                table: "Loans",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasBalloonPayment",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasCoSigner",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HomeownersInsurance",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndexUsed",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InitialPromotionalRate",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsuranceExpirationDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyNumber",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceProvider",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InterestOnlyPayment",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLeased",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimaryResidence",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRefinance",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVariableRate",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVirtual",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaymentDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastStatementDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LastStatementMinimumPayment",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LateFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LatePaymentFee",
                table: "Loans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "LienHolderName",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LinkedAccountId",
                table: "Loans",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoanNumber",
                table: "Loans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "LoanToValueRatio",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MarginRate",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumDrawAmount",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumPayment",
                table: "Loans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumPaymentAmount",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumPaymentPercentage",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyPaymentAmount",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyPrincipalAndInterest",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MortgageLoan_DownPayment",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MortgageLoan_PropertyType",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Loans",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextPaymentDue",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OriginationFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OverLimitFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PMIAmount",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentDay",
                table: "Loans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDueDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentFrequencyType",
                table: "Loans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_AnnualFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_AvailableCredit",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PersonalLineOfCreditLoan_DrawPeriodEndDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalLineOfCreditLoan_IndexUsed",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PersonalLineOfCreditLoan_IsSecured",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PersonalLineOfCreditLoan_IsVariableRate",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_LateFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_MarginRate",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_MinimumDrawAmount",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_MinimumPaymentAmount",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_MinimumPaymentPercentage",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_OriginationFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLineOfCreditLoan_OverLimitFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonalLineOfCreditLoan_RepaymentPeriodMonths",
                table: "Loans",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalLoan_CoSignerId",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalLoan_CollateralDescription",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLoan_CollateralValue",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PersonalLoan_HasCoSigner",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLoan_LateFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLoan_OriginationFee",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PersonalLoan_PrePaymentPenalty",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrePaymentPenalty",
                table: "Loans",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PromotionalRateEndDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PropertyTaxes",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyType",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RateAdjustmentFrequency",
                table: "Loans",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RateCap",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RateIndex",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepaymentFrequency",
                table: "Loans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RepaymentPeriodMonths",
                table: "Loans",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RewardPointsBalance",
                table: "Loans",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StatementBalance",
                table: "Loans",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountPaid",
                table: "Loans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterestPaid",
                table: "Loans",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyExpirationDate",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Accounts",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ATMFee",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "AccruedInterest",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BillPayEnabled",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BranchId",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CheckFee",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CheckWritingEnabled",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompoundingFrequency",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "DirectDepositEnabled",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EarlyWithdrawalPenalty",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExcessTransactionFee",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreeChecksPerMonth",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FreeTransactionsPerMonth",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GoalAmount",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GoalBasedSaving",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GracePeriodDays",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasDebitCard",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HighestTierRate",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InterestPaymentFrequency",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLaddered",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LadderPosition",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastInterestPaid",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastRenewalDate",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "LowBalanceFee",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumBalanceRequired",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MinimumDepositAmount",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MoneyMarketAccount_MinimumBalanceRequired",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyMaintenanceFee",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "OverdraftLimit",
                table: "Accounts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "OverdraftLinkedAccountId",
                table: "Accounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OverdraftProtection",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RateTierStructure",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RenewalTerms",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SavingGoal",
                table: "Accounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SavingsAccount_MinimumBalanceRequired",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetDate",
                table: "Accounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TermMonths",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TieredInterestRates",
                table: "Accounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalInterestEarned",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TransactionFee",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WithdrawalFee",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WithdrawalsPerMonth",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "YearToDateInterestEarned",
                table: "Accounts",
                type: "numeric",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoanPayment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoanId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PrincipalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    InterestAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    FeesAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    LateFeeAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    PaymentSource = table.Column<string>(type: "text", nullable: false),
                    IsAutoPay = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConfirmationNumber = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanPayment_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanPayment_LoanId",
                table: "LoanPayment",
                column: "LoanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanPayment");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OriginalAmount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OriginalCurrency",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SettlementDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AdjustableRateMargin",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AllowsChecks",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AllowsConversion",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AllowsExtraPayments",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AmortizationType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ApplicationFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AutoPay",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AutoPayEnabled",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AutoPayFromAccountId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AutoPayType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AvailableCredit",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "BalloonPaymentAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CashAdvanceFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CashAdvanceInterestRate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CashAdvanceLimit",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CoSignerId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CollateralDescription",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CollateralValue",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CombinedLoanToValueRatio",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CreditCardLoan_AnnualFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "CurrentCashbackBalance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DealerAddress",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DealerName",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DownPayment",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "DrawPeriodEndDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "EscrowBalance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "EscrowEnabled",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ExtendedWarranty",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "FirstAdjustmentDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "FirstMortgageBalance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "FirstPaymentDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ForeignTransactionFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "GapInsurance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "GracePeriodDays",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "HasBalloonPayment",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "HasCoSigner",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "HomeownersInsurance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IndexUsed",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "InitialPromotionalRate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "InsuranceExpirationDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "InsuranceProvider",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "InterestOnlyPayment",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsLeased",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsPrimaryResidence",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsRefinance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsVariableRate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsVirtual",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LastPaymentDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LastStatementDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LastStatementMinimumPayment",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LateFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LatePaymentFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LienHolderName",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LinkedAccountId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "LoanToValueRatio",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MarginRate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MinimumDrawAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MinimumPayment",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MinimumPaymentAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MinimumPaymentPercentage",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MonthlyPaymentAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MonthlyPrincipalAndInterest",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MortgageLoan_DownPayment",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "MortgageLoan_PropertyType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "NextPaymentDue",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "OriginationFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "OverLimitFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PMIAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PaymentDay",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PaymentDueDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PaymentFrequencyType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_AnnualFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_AvailableCredit",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_DrawPeriodEndDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_IndexUsed",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_IsSecured",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_IsVariableRate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_LateFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_MarginRate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_MinimumDrawAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_MinimumPaymentAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_MinimumPaymentPercentage",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_OriginationFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_OverLimitFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLineOfCreditLoan_RepaymentPeriodMonths",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLoan_CoSignerId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLoan_CollateralDescription",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLoan_CollateralValue",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLoan_HasCoSigner",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLoan_LateFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLoan_OriginationFee",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PersonalLoan_PrePaymentPenalty",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PrePaymentPenalty",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PromotionalRateEndDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PropertyTaxes",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RateAdjustmentFrequency",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RateCap",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RateIndex",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RepaymentFrequency",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RepaymentPeriodMonths",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "RewardPointsBalance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "StatementBalance",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TotalAmountPaid",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "TotalInterestPaid",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "WarrantyExpirationDate",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ATMFee",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccruedInterest",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BillPayEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CheckFee",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CheckWritingEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CompoundingFrequency",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DirectDepositEnabled",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "EarlyWithdrawalPenalty",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ExcessTransactionFee",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FreeChecksPerMonth",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FreeTransactionsPerMonth",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "GoalAmount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "GoalBasedSaving",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "GracePeriodDays",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "HasDebitCard",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "HighestTierRate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "InterestPaymentFrequency",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IsLaddered",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LadderPosition",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastInterestPaid",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastRenewalDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LowBalanceFee",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MinimumBalanceRequired",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MinimumDepositAmount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MoneyMarketAccount_MinimumBalanceRequired",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "MonthlyMaintenanceFee",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OverdraftLimit",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OverdraftLinkedAccountId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "OverdraftProtection",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RateTierStructure",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RenewalTerms",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SavingGoal",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "SavingsAccount_MinimumBalanceRequired",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TargetDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TermMonths",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TieredInterestRates",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TotalInterestEarned",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TransactionFee",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "WithdrawalFee",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "WithdrawalsPerMonth",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "YearToDateInterestEarned",
                table: "Accounts");

            migrationBuilder.AlterColumn<decimal>(
                name: "AnnualFee",
                table: "Loans",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "Accounts",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<decimal>(
                name: "MoneyMarketAccount_InterestRate",
                table: "Accounts",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SavingsAccount_InterestRate",
                table: "Accounts",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);
        }
    }
}
