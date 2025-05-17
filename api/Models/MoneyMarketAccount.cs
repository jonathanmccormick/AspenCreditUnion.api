namespace AspenCreditUnion.api.Models;

public class MoneyMarketAccount : Account
{
    // Additional properties specific to MoneyMarketAccount
    // Note: InterestRate is already in base Account class
    public int TransactionsPerMonth { get; set; }
    public decimal MinimumBalanceRequired { get; set; } = 1000; // Minimum balance to avoid fees
    public decimal LowBalanceFee { get; set; } = 0; // Fee charged if balance falls below minimum
    public decimal ExcessTransactionFee { get; set; } = 0; // Fee for exceeding transaction limit
    public bool CheckWritingEnabled { get; set; } = true; // Whether check writing is available
    public int FreeChecksPerMonth { get; set; } = 0; // Number of free checks per month
    public decimal CheckFee { get; set; } = 0; // Fee per check after free limit
    public bool TieredInterestRates { get; set; } = false; // Whether interest rates vary by balance tiers
    public string RateTierStructure { get; set; } = string.Empty; // Description of rate tiers if applicable
    public decimal HighestTierRate { get; set; } = 0; // Highest possible interest rate in tier structure
}