using System;

namespace AspenCreditUnion.api.Models
{
    public enum TransactionType
    {
        Transfer,
        LoanPayment,
        LoanAdvance,
        Deposit,
        Withdrawal,
        Fee,
        Interest,
        DirectDeposit,
        BillPayment,
        ATM,
        Purchase,
        Refund,
        Adjustment,
        Wire,
        ACH,
        Check
    }

    public class Transaction
    {
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public Guid SourceAccountId { get; set; }  // Changed from string to int
        public Guid DestinationAccountId { get; set; } // Changed from string to int
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } = string.Empty; // Foreign key to ApplicationUser
        public string Description { get; set; } = string.Empty; // Description of transaction purpose
        public string ReferenceNumber { get; set; } = string.Empty; // Reference number for external tracking
        public string Category { get; set; } = string.Empty; // Category for expense tracking
        public string Status { get; set; } = "Completed"; // Status (Pending, Completed, Failed, Reversed)
        public decimal Fee { get; set; } = 0; // Any fee associated with the transaction
        public DateTime? SettlementDate { get; set; } // Date when transaction was fully settled/cleared
        public string Currency { get; set; } = "USD"; // Currency of the transaction
        public decimal OriginalAmount { get; set; } // Original amount if currency conversion happened
        public string OriginalCurrency { get; set; } = string.Empty; // Original currency if conversion happened
        public string Location { get; set; } = string.Empty; // Location where transaction occurred (for purchases)
        public string IPAddress { get; set; } = string.Empty; // IP address for fraud detection/security
    }
}