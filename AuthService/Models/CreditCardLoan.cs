namespace AuthService.Models;

public class CreditCardLoan : Loan
{
    // Additional properties specific to CreditCardLoan
    public decimal CreditLimit { get; set; }
    public decimal AnnualFee { get; set; }
    public string RewardProgram { get; set; } = string.Empty;
}