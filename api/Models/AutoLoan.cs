namespace AspenCreditUnion.api.Models;

public class AutoLoan : Loan
{
    // Additional properties specific to AutoLoan
    public string VehicleVin { get; set; } = string.Empty;
    public string VehicleMake { get; set; } = string.Empty;
    public string VehicleModel { get; set; } = string.Empty;
    public int VehicleYear { get; set; }
}