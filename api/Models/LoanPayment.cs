using System;

namespace AspenCreditUnion.api.Models
{
    /// <summary>
    /// Represents an individual payment made on a loan
    /// </summary>
    public class LoanPayment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LoanId { get; set; } // Foreign key to the loan
        public DateTime PaymentDate { get; set; } // When the payment was made
        public decimal Amount { get; set; } // Total payment amount
        public decimal PrincipalAmount { get; set; } // Portion applied to principal
        public decimal InterestAmount { get; set; } // Portion applied to interest
        public decimal FeesAmount { get; set; } = 0; // Any fees included in payment
        public decimal LateFeeAmount { get; set; } = 0; // Any late fees included
        public string PaymentMethod { get; set; } = string.Empty; // How payment was made (ACH, check, etc.)
        public string PaymentSource { get; set; } = string.Empty; // Source of payment (account number, etc.)
        public bool IsAutoPay { get; set; } = false; // Whether it was an automatic payment
        public string Status { get; set; } = "Completed"; // Payment status (Pending, Completed, Failed, Reversed)
        public Guid? TransactionId { get; set; } // Reference to transaction record if applicable
        public string ConfirmationNumber { get; set; } = string.Empty; // Payment confirmation number
        public string Notes { get; set; } = string.Empty; // Any notes about the payment
    }
}
