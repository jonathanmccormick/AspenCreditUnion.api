namespace AspenCreditUnion.api.Models;

public class PersonalLoan : Loan
{
    // Additional properties specific to PersonalLoan
    public string Purpose { get; set; } = string.Empty;
    public int LoanTermMonths { get; set; }
    public bool IsSecured { get; set; }
    public decimal OriginationFee { get; set; } = 0; // Fee for originating the loan
    public string CollateralDescription { get; set; } = string.Empty; // Description of collateral if secured
    public decimal CollateralValue { get; set; } = 0; // Value of collateral if secured
    public bool HasCoSigner { get; set; } = false; // Whether loan has a co-signer
    public string CoSignerId { get; set; } = string.Empty; // ID of co-signer if applicable
    public decimal MonthlyPaymentAmount { get; set; } = 0; // Fixed monthly payment amount
    public DateTime? FirstPaymentDate { get; set; } // Date first payment is due
    public bool AllowsExtraPayments { get; set; } = true; // Whether extra payments are allowed
    public decimal PrePaymentPenalty { get; set; } = 0; // Penalty for early payoff
    public decimal ApplicationFee { get; set; } = 0; // Fee for the loan application
    public decimal LateFee { get; set; } = 0; // Fee for late payments
    public string RepaymentFrequency { get; set; } = "Monthly"; // Monthly, Bi-weekly, etc.
    public string AmortizationType { get; set; } = "Standard"; // Standard, Balloon, etc.
    public decimal BalloonPaymentAmount { get; set; } = 0; // Amount of balloon payment if applicable
}