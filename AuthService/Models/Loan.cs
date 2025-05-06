using System;

namespace AuthService.Models;

public abstract class Loan
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Changed Id to Guid for global uniqueness
    public string BorrowerId { get; set; } = string.Empty; // Foreign key to ApplicationUser
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
    public LoanStatus Status { get; set; }
}

public enum LoanStatus
{
    Pending,
    Active,
    Closed,
    Defaulted
}