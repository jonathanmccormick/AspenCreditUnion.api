namespace AspenCreditUnion.api.Models;

public class MortgageLoan : Loan
{
    // Additional properties specific to MortgageLoan
    public string PropertyAddress { get; set; } = string.Empty;
    public decimal PropertyValue { get; set; }
    public int LoanTermYears { get; set; }
    public bool IsFixedRate { get; set; }
}