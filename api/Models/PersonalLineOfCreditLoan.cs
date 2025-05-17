namespace AspenCreditUnion.api.Models;

public class PersonalLineOfCreditLoan : Loan
{
    // Additional properties for personal line of credit
    public decimal CreditLimit { get; set; }
    public int DrawPeriodMonths { get; set; }
    public bool IsSecured { get; set; }
    public int RepaymentPeriodMonths { get; set; } = 60; // Typical 5-year repayment period
    public decimal AvailableCredit { get; set; } = 0; // Current available credit
    public DateTime? DrawPeriodEndDate { get; set; } // When draw period ends
    public decimal MinimumDrawAmount { get; set; } = 0; // Minimum amount for each draw
    public decimal AnnualFee { get; set; } = 0; // Annual maintenance fee
    public decimal OriginationFee { get; set; } = 0; // Fee to establish the line of credit
    public string CollateralDescription { get; set; } = string.Empty; // Description if secured
    public decimal CollateralValue { get; set; } = 0; // Value of collateral if secured
    public decimal MinimumPaymentPercentage { get; set; } = 2.0m; // % of balance for min payment
    public decimal MinimumPaymentAmount { get; set; } = 25.0m; // Minimum fixed payment amount
    public bool AllowsChecks { get; set; } = true; // Whether checks can be used to access funds
    public bool IsVariableRate { get; set; } = true; // Whether rate is variable
    public string IndexUsed { get; set; } = "Prime"; // Prime, LIBOR, SOFR, etc.
    public decimal MarginRate { get; set; } = 0; // Margin above index rate
    public decimal LateFee { get; set; } = 0; // Fee for late payments
    public decimal OverLimitFee { get; set; } = 0; // Fee for exceeding credit limit
    public bool HasCoSigner { get; set; } = false; // Whether line has a co-signer
    public string CoSignerId { get; set; } = string.Empty; // ID of co-signer if applicable
}