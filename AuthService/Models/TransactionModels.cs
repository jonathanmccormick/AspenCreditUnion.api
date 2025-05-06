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

    public class TransactionDetail
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string SourceName { get; set; }
        public string DestinationName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}