namespace AuthService.Models;

// Request models for account creation

public class CreateCheckingAccountRequest
{
    public decimal InitialDeposit { get; set; }
}

public class CreateSavingsAccountRequest
{
    public decimal InitialDeposit { get; set; }
    public decimal InterestRate { get; set; }
}

public class CreateCDAccountRequest
{
    public decimal InitialDeposit { get; set; }
    public decimal InterestRate { get; set; }
    public DateTime MaturityDate { get; set; }
    public bool AutoRenew { get; set; }
}

public class CreateMoneyMarketAccountRequest
{
    public decimal InitialDeposit { get; set; }
    public decimal InterestRate { get; set; }
    public int TransactionsPerMonth { get; set; }
}