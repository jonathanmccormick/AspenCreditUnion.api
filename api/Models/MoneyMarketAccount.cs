namespace AspenCreditUnion.api.Models;

public class MoneyMarketAccount : Account
{
    // Additional properties specific to MoneyMarketAccount
    public decimal InterestRate { get; set; }
    public int TransactionsPerMonth { get; set; }
}