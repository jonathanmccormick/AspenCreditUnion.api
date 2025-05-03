namespace AuthService.Models;

public class PersonalLoan : Loan
{
    // Additional properties specific to PersonalLoan
    public string Purpose { get; set; } = string.Empty;
    public int LoanTermMonths { get; set; }
    public bool IsSecured { get; set; }
}