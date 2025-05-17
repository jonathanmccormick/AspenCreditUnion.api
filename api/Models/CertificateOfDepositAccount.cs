namespace AspenCreditUnion.api.Models;

public class CertificateOfDepositAccount : Account
{
    // Additional properties specific to CertificateOfDepositAccount
    // Note: InterestRate is already in base Account class
    public DateTime MaturityDate { get; set; }
    public bool AutoRenew { get; set; }
    public int TermMonths { get; set; } // Term length in months
    public decimal EarlyWithdrawalPenalty { get; set; } = 0; // Penalty for early withdrawal (usually months of interest)
    public decimal MinimumDepositAmount { get; set; } = 1000; // Minimum deposit required
    public string RenewalTerms { get; set; } = string.Empty; // Terms for renewal
    public int GracePeriodDays { get; set; } = 10; // Grace period after maturity for withdrawal without penalty
    public bool IsLaddered { get; set; } = false; // Part of a CD ladder strategy
    public int LadderPosition { get; set; } = 0; // Position in CD ladder if applicable
    public DateTime LastRenewalDate { get; set; } = DateTime.UtcNow; // Date of last renewal
    public decimal AccruedInterest { get; set; } = 0; // Interest accrued but not yet credited
}