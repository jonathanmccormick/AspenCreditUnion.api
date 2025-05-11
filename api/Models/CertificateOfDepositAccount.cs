namespace AspenCreditUnion.api.Models;

public class CertificateOfDepositAccount : Account
{
    // Additional properties specific to CertificateOfDepositAccount
    public decimal InterestRate { get; set; }
    public DateTime MaturityDate { get; set; }
    public bool AutoRenew { get; set; }
}