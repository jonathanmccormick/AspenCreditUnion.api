using System;

namespace AspenCreditUnion.api.Models;

public abstract class Account
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Changed Id to Guid for global uniqueness
    public string OwnerId { get; set; } = string.Empty; // Foreign key to ApplicationUser
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string AccountNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal InterestRate { get; set; }
    public string AccountType { get; set; } = string.Empty;
    public AccountStatus Status { get; set; } = AccountStatus.Active;
    public string Currency { get; set; } = "USD";
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public decimal OverdraftLimit { get; set; } = 0;
    public string BranchId { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
}

public enum AccountStatus
{
    Active,
    Inactive,
    Closed,
    Frozen,
    PendingApproval,
    Dormant,
    Restricted
}