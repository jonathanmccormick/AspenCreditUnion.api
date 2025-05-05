using System;

namespace AuthService.Models
{
    public class TransactionRequest
    {
        public TransactionType Type { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}