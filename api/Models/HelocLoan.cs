namespace AspenCreditUnion.api.Models;

public class HelocLoan : Loan
{
    // Additional properties specific to Home Equity Line of Credit loan
    public string PropertyAddress { get; set; } = string.Empty;
    public decimal PropertyValue { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentEquity { get; set; }
    public int DrawPeriodMonths { get; set; }
}