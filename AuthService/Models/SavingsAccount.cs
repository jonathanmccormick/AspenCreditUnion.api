namespace AuthService.Models;

public class SavingsAccount : Account
{
    // Additional properties specific to SavingsAccount
    public decimal InterestRate { get; set; }
}