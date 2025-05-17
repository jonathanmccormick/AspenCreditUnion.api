namespace AspenCreditUnion.api.Models;

public class HelocLoan : Loan
{
    // Additional properties specific to Home Equity Line of Credit loan
    public string PropertyAddress { get; set; } = string.Empty;
    public decimal PropertyValue { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentEquity { get; set; }
    public int DrawPeriodMonths { get; set; }
    public int RepaymentPeriodMonths { get; set; } = 240; // 20 years typical repayment period
    public decimal FirstMortgageBalance { get; set; } = 0; // Balance of first mortgage if any
    public decimal CombinedLoanToValueRatio { get; set; } = 0; // CLTV ratio (first + HELOC)
    public bool IsVariableRate { get; set; } = true; // Whether rate is variable
    public string IndexUsed { get; set; } = "Prime"; // Prime, LIBOR, SOFR, etc.
    public decimal MarginRate { get; set; } = 0; // Margin above index rate
    public decimal InitialPromotionalRate { get; set; } = 0; // Promotional rate if applicable
    public DateTime? PromotionalRateEndDate { get; set; } // When promotional rate ends
    public decimal MinimumDrawAmount { get; set; } = 0; // Minimum amount for each draw
    public decimal AnnualFee { get; set; } = 0; // Annual maintenance fee
    public decimal OriginationFee { get; set; } = 0; // Initial fee to establish the HELOC
    public bool HasBalloonPayment { get; set; } = false; // Whether there's a balloon payment
    public DateTime? DrawPeriodEndDate { get; set; } // When draw period ends
    public decimal InterestOnlyPayment { get; set; } = 0; // Interest-only payment amount during draw
    public bool AllowsConversion { get; set; } = false; // Option to convert to fixed-rate loan
    public string PropertyType { get; set; } = string.Empty; // Type of property securing the HELOC
}