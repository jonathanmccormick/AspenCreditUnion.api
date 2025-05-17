using System;
using System.Collections.Generic;

namespace AspenCreditUnion.api.Models;

public abstract class Loan
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Changed Id to Guid for global uniqueness
    public string BorrowerId { get; set; } = string.Empty; // Foreign key to ApplicationUser
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public LoanStatus Status { get; set; }
    public string LoanNumber { get; set; } = string.Empty; // Unique loan identifier for reference
    public string Name { get; set; } = string.Empty; // Nickname for the loan
    public decimal MinimumPayment { get; set; } = 0; // Minimum monthly payment required
    public DateTime? NextPaymentDue { get; set; } // Date when next payment is due
    public DateTime? LastPaymentDate { get; set; } // Date when the most recent payment was made
    public DateTime? FirstPaymentDate { get; set; } // Date when the first payment was/is due
    public int PaymentDay { get; set; } = 1; // Day of month/week when payments are due (depends on frequency)
    public decimal TotalInterestPaid { get; set; } = 0; // Track total interest paid over life of loan
    public decimal TotalAmountPaid { get; set; } = 0; // Track total amount paid (principal + interest)
    public decimal LatePaymentFee { get; set; } = 0; // Fee charged for late payments
    public PaymentFrequency PaymentFrequencyType { get; set; } = PaymentFrequency.Monthly; // How often payments are due
    public string Currency { get; set; } = "USD"; // Currency of the loan
    public bool AutoPay { get; set; } = false; // Whether automatic payments are enabled
    public Guid? LinkedAccountId { get; set; } // Account for auto-payments, if enabled
    
    // Collection to track payment history
    public List<LoanPayment> PaymentHistory { get; set; } = new List<LoanPayment>();
}

public enum LoanStatus
{
    Pending,
    Active,
    Closed,
    Defaulted
}

public enum PaymentFrequency
{
    Daily,
    Weekly,
    BiWeekly,
    Monthly,
    Quarterly,
    SemiAnnually,
    Annually
}