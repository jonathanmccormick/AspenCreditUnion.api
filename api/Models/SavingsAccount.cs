namespace AspenCreditUnion.api.Models;

public class SavingsAccount : Account
{
    // Additional properties specific to SavingsAccount
    // Note: InterestRate is already in the base Account class
    public int WithdrawalsPerMonth { get; set; } = 6; // Regulation D withdrawal limit
    public decimal WithdrawalFee { get; set; } = 0; // Fee for exceeding withdrawal limit
    public DateTime? LastInterestPaid { get; set; } // Date interest was last paid
    public string CompoundingFrequency { get; set; } = "Daily"; // How often interest compounds
    public string InterestPaymentFrequency { get; set; } = "Monthly"; // How often interest is paid
    public decimal MinimumBalanceRequired { get; set; } = 0; // Minimum balance to earn interest
    public decimal YearToDateInterestEarned { get; set; } = 0; // Interest earned this year
    public decimal TotalInterestEarned { get; set; } = 0; // Total interest earned over account life
    public bool GoalBasedSaving { get; set; } = false; // Whether account is for a specific saving goal
    public string SavingGoal { get; set; } = string.Empty; // Goal description if applicable
    public decimal GoalAmount { get; set; } = 0; // Target amount if goal-based
    public DateTime? TargetDate { get; set; } // Target date to reach goal
}