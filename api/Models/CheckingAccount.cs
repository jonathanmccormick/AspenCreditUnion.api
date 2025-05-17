namespace AspenCreditUnion.api.Models;

public class CheckingAccount : Account
{
    // Additional properties specific to CheckingAccount
    public bool HasDebitCard { get; set; } = false; // Whether account has a debit card
    public decimal MonthlyMaintenanceFee { get; set; } = 0; // Monthly fee for maintaining the account
    public bool DirectDepositEnabled { get; set; } = false; // Whether direct deposit is enabled
    public decimal MinimumBalanceRequired { get; set; } = 0; // Minimum balance to avoid fees
    public int FreeTransactionsPerMonth { get; set; } = 0; // Number of free transactions per month
    public decimal TransactionFee { get; set; } = 0; // Fee per transaction after free limit
    public bool OverdraftProtection { get; set; } = false; // Whether overdraft protection is enabled
    public Guid? OverdraftLinkedAccountId { get; set; } // Linked account for overdraft protection
    public bool BillPayEnabled { get; set; } = true; // Whether bill pay feature is enabled
    public decimal ATMFee { get; set; } = 0; // Fee for using out-of-network ATMs
}