namespace AuthService.Models;

// Request models for loan applications

public class AutoLoanRequest
{
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public string VehicleVin { get; set; } = string.Empty;
    public string VehicleMake { get; set; } = string.Empty;
    public string VehicleModel { get; set; } = string.Empty;
    public int VehicleYear { get; set; }
}

public class MortgageLoanRequest
{
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public string PropertyAddress { get; set; } = string.Empty;
    public decimal PropertyValue { get; set; }
    public int LoanTermYears { get; set; }
    public bool IsFixedRate { get; set; }
}

public class CreditCardLoanRequest
{
    public decimal InterestRate { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal AnnualFee { get; set; }
    public string RewardProgram { get; set; } = string.Empty;
}

public class PersonalLoanRequest
{
    public decimal Principal { get; set; }
    public decimal InterestRate { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int LoanTermMonths { get; set; }
    public bool IsSecured { get; set; }
}

public class HelocLoanRequest
{
    public decimal InterestRate { get; set; }
    public string PropertyAddress { get; set; } = string.Empty;
    public decimal PropertyValue { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentEquity { get; set; }
    public int DrawPeriodMonths { get; set; }
}

public class PersonalLineOfCreditRequest
{
    public decimal InterestRate { get; set; }
    public decimal CreditLimit { get; set; }
    public int DrawPeriodMonths { get; set; }
    public bool IsSecured { get; set; }
}