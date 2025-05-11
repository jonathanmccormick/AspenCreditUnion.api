using System;

namespace AspenCreditUnion.api.Models;

public abstract class Account
{
    public Guid Id { get; set; } = Guid.NewGuid(); // Changed Id to Guid for global uniqueness
    public string OwnerId { get; set; } = string.Empty; // Foreign key to ApplicationUser
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}