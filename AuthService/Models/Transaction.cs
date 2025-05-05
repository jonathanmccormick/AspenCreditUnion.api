using System;

namespace AuthService.Models
{
    public enum TransactionType
    {
        Transfer,
        LoanPayment,
        LoanAdvance
    }

    public class Transaction
    {
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public int SourceAccountId { get; set; }  // Changed from string to int
        public int DestinationAccountId { get; set; } // Changed from string to int
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}