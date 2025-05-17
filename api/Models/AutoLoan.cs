namespace AspenCreditUnion.api.Models;

public class AutoLoan : Loan
{
    // Additional properties specific to AutoLoan
    public string VehicleVin { get; set; } = string.Empty;
    public string VehicleMake { get; set; } = string.Empty;
    public string VehicleModel { get; set; } = string.Empty;
    public int VehicleYear { get; set; }
    public decimal PurchasePrice { get; set; } = 0; // Total price of the vehicle
    public decimal DownPayment { get; set; } = 0; // Initial down payment amount
    public string DealerName { get; set; } = string.Empty; // Name of the dealer where purchased
    public string DealerAddress { get; set; } = string.Empty; // Address of the dealer
    public bool IsRefinance { get; set; } = false; // Whether this is a refinance of an existing loan
    public bool IsLeased { get; set; } = false; // Whether the vehicle is leased
    public string InsuranceProvider { get; set; } = string.Empty; // Insurance company name
    public string InsurancePolicyNumber { get; set; } = string.Empty; // Insurance policy number
    public DateTime? InsuranceExpirationDate { get; set; } // When insurance expires
    public bool GapInsurance { get; set; } = false; // Whether GAP insurance is included
    public bool ExtendedWarranty { get; set; } = false; // Whether extended warranty is included
    public DateTime? WarrantyExpirationDate { get; set; } // When warranty expires
    public string LienHolderName { get; set; } = string.Empty; // Who holds the lien
}