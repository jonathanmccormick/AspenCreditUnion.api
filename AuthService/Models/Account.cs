using System;

namespace AuthService.Models;

public abstract class Account
{
    public int Id { get; set; }
    public string OwnerId { get; set; } = string.Empty; // Foreign key to ApplicationUser
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}