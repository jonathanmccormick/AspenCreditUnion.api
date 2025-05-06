using System;

namespace AuthService.Models
{
    public class TransactionRequest
    {
        public TransactionType Type { get; set; }
        public Guid SourceAccountId { get; set; }
        public Guid DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}