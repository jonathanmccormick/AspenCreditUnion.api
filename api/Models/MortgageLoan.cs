namespace AspenCreditUnion.api.Models;

public class MortgageLoan : Loan
{
    // Additional properties specific to MortgageLoan
    public string PropertyAddress { get; set; } = string.Empty;
    public decimal PropertyValue { get; set; }
    public int LoanTermYears { get; set; }
    public bool IsFixedRate { get; set; }
    public string PropertyType { get; set; } = string.Empty; // Single family, condo, multi-family, etc.
    public decimal DownPayment { get; set; } = 0; // Initial down payment amount
    public decimal LoanToValueRatio { get; set; } = 0; // LTV ratio as a percentage
    public decimal MonthlyPrincipalAndInterest { get; set; } = 0; // P&I payment
    public decimal PropertyTaxes { get; set; } = 0; // Annual property taxes
    public decimal HomeownersInsurance { get; set; } = 0; // Annual insurance premium
    public decimal PMIAmount { get; set; } = 0; // Private mortgage insurance amount
    public bool EscrowEnabled { get; set; } = true; // Whether escrow account is used
    public decimal EscrowBalance { get; set; } = 0; // Current escrow account balance
    public bool IsPrimaryResidence { get; set; } = true; // Whether property is primary residence
    public decimal AdjustableRateMargin { get; set; } = 0; // For ARM loans, the margin above index
    public string RateIndex { get; set; } = string.Empty; // For ARMs, the index used (LIBOR, SOFR, etc.)
    public int RateAdjustmentFrequency { get; set; } = 0; // In months, how often rate adjusts (ARM)
    public decimal RateCap { get; set; } = 0; // Maximum rate adjustment cap
    public bool PrePaymentPenalty { get; set; } = false; // Whether prepayment penalty applies
    public DateTime? FirstAdjustmentDate { get; set; } // For ARMs, when first adjustment occurs
}