namespace AspenCreditUnion.api.Models;

public class PersonalLineOfCreditLoan : Loan
{
    // Additional properties for personal line of credit
    public decimal CreditLimit { get; set; }
    public int DrawPeriodMonths { get; set; }
    public bool IsSecured { get; set; }
}