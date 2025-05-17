namespace AspenCreditUnion.api.Models;

public class CreditCardLoan : Loan
{
    // Additional properties specific to CreditCardLoan
    public decimal CreditLimit { get; set; }
    public decimal AnnualFee { get; set; }
    public string RewardProgram { get; set; } = string.Empty;
    public string CardType { get; set; } = "Standard"; // Standard, Gold, Platinum, etc.
    public decimal CashAdvanceLimit { get; set; } = 0; // Maximum cash advance amount
    public decimal CashAdvanceFee { get; set; } = 0; // Fee for cash advances
    public decimal CashAdvanceInterestRate { get; set; } = 0; // Different rate for cash advances
    public decimal ForeignTransactionFee { get; set; } = 0; // Fee for foreign transactions
    public decimal LateFee { get; set; } = 0; // Fee for late payments
    public decimal OverLimitFee { get; set; } = 0; // Fee for exceeding credit limit
    public decimal MinimumPaymentPercentage { get; set; } = 2.0m; // Percentage of balance for min payment
    public decimal MinimumPaymentAmount { get; set; } = 25.0m; // Minimum fixed amount for min payment
    public decimal AvailableCredit { get; set; } = 0; // Available credit (limit - balance)
    public decimal StatementBalance { get; set; } = 0; // Balance as of last statement
    public DateTime? LastStatementDate { get; set; } // Date of last statement
    public DateTime? PaymentDueDate { get; set; } // When payment is due
    public decimal LastStatementMinimumPayment { get; set; } = 0; // Min payment from last statement
    public bool AutoPayEnabled { get; set; } = false; // Whether autopay is enabled
    public string AutoPayType { get; set; } = "Minimum"; // Minimum, Statement Balance, or Fixed Amount
    public Guid? AutoPayFromAccountId { get; set; } // Account to pay from
    public int GracePeriodDays { get; set; } = 25; // Interest-free period if paid in full
    public int RewardPointsBalance { get; set; } = 0; // Current reward points balance
    public decimal CurrentCashbackBalance { get; set; } = 0; // Current cashback balance
    public bool IsVirtual { get; set; } = false; // Whether it's a virtual card
    public bool IsSecured { get; set; } = false; // Whether it's a secured credit card
}