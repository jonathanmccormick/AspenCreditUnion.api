using System.Security.Claims;
using AuthService.Data;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<LoansController> _logger;

    public LoansController(
        ApplicationDbContext dbContext,
        ILogger<LoansController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetLoans()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var loans = await _dbContext.Loans
            .Where(l => l.BorrowerId == userId)
            .ToListAsync();

        var detailedLoans = loans.Select(GetDetailedLoanResponse).ToList();
        return Ok(detailedLoans);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoan(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var loan = await _dbContext.Loans
            .FirstOrDefaultAsync(l => l.Id == id && l.BorrowerId == userId);

        if (loan == null)
        {
            return NotFound(new { message = "Loan not found" });
        }

        return Ok(GetDetailedLoanResponse(loan));
    }

    [HttpGet("types")]
    public IActionResult GetLoanTypes()
    {
        var loanTypes = new[]
        {
            new { Type = "Auto", Description = "Loans for vehicle purchases" },
            new { Type = "Mortgage", Description = "Home loans for property purchases" },
            new { Type = "CreditCard", Description = "Credit card with revolving credit line" },
            new { Type = "Personal", Description = "Personal loans for various purposes" },
            new { Type = "Heloc", Description = "Home equity line of credit secured by your home" },
            new { Type = "PersonalLineOfCredit", Description = "Revolving personal line of credit" }
        };

        return Ok(loanTypes);
    }

    [HttpPost("auto")]
    public async Task<IActionResult> ApplyForAutoLoan([FromBody] AutoLoanRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var autoLoan = new AutoLoan
        {
            BorrowerId = userId,
            Principal = request.Principal,
            InterestRate = request.InterestRate,
            StartDate = DateTime.UtcNow,
            Status = LoanStatus.Pending,
            VehicleVin = request.VehicleVin,
            VehicleMake = request.VehicleMake,
            VehicleModel = request.VehicleModel,
            VehicleYear = request.VehicleYear
        };

        _dbContext.Loans.Add(autoLoan);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoan), new { id = autoLoan.Id }, GetDetailedLoanResponse(autoLoan));
    }

    [HttpPost("mortgage")]
    public async Task<IActionResult> ApplyForMortgageLoan([FromBody] MortgageLoanRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var mortgageLoan = new MortgageLoan
        {
            BorrowerId = userId,
            Principal = request.Principal,
            InterestRate = request.InterestRate,
            StartDate = DateTime.UtcNow,
            Status = LoanStatus.Pending,
            PropertyAddress = request.PropertyAddress,
            PropertyValue = request.PropertyValue,
            LoanTermYears = request.LoanTermYears,
            IsFixedRate = request.IsFixedRate
        };

        _dbContext.Loans.Add(mortgageLoan);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoan), new { id = mortgageLoan.Id }, GetDetailedLoanResponse(mortgageLoan));
    }

    [HttpPost("credit-card")]
    public async Task<IActionResult> ApplyForCreditCard([FromBody] CreditCardLoanRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var creditCardLoan = new CreditCardLoan
        {
            BorrowerId = userId,
            Principal = 0, // Initial balance is zero
            InterestRate = request.InterestRate,
            StartDate = DateTime.UtcNow,
            Status = LoanStatus.Pending,
            CreditLimit = request.CreditLimit,
            AnnualFee = request.AnnualFee,
            RewardProgram = request.RewardProgram
        };

        _dbContext.Loans.Add(creditCardLoan);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoan), new { id = creditCardLoan.Id }, GetDetailedLoanResponse(creditCardLoan));
    }

    [HttpPost("personal")]
    public async Task<IActionResult> ApplyForPersonalLoan([FromBody] PersonalLoanRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var personalLoan = new PersonalLoan
        {
            BorrowerId = userId,
            Principal = request.Principal,
            InterestRate = request.InterestRate,
            StartDate = DateTime.UtcNow,
            Status = LoanStatus.Pending,
            Purpose = request.Purpose,
            LoanTermMonths = request.LoanTermMonths,
            IsSecured = request.IsSecured
        };

        _dbContext.Loans.Add(personalLoan);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoan), new { id = personalLoan.Id }, GetDetailedLoanResponse(personalLoan));
    }

    [HttpPost("heloc")]
    public async Task<IActionResult> ApplyForHeloc([FromBody] HelocLoanRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var helocLoan = new HelocLoan
        {
            BorrowerId = userId,
            Principal = 0, // Initial draw is zero
            InterestRate = request.InterestRate,
            StartDate = DateTime.UtcNow,
            Status = LoanStatus.Pending,
            PropertyAddress = request.PropertyAddress,
            PropertyValue = request.PropertyValue,
            CreditLimit = request.CreditLimit,
            CurrentEquity = request.CurrentEquity,
            DrawPeriodMonths = request.DrawPeriodMonths
        };

        _dbContext.Loans.Add(helocLoan);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoan), new { id = helocLoan.Id }, GetDetailedLoanResponse(helocLoan));
    }

    [HttpPost("personal-line-of-credit")]
    public async Task<IActionResult> ApplyForPersonalLineOfCredit([FromBody] PersonalLineOfCreditRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var personalLineOfCredit = new PersonalLineOfCreditLoan
        {
            BorrowerId = userId,
            Principal = 0, // Initial draw is zero
            InterestRate = request.InterestRate,
            StartDate = DateTime.UtcNow,
            Status = LoanStatus.Pending,
            CreditLimit = request.CreditLimit,
            DrawPeriodMonths = request.DrawPeriodMonths,
            IsSecured = request.IsSecured
        };

        _dbContext.Loans.Add(personalLineOfCredit);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLoan), new { id = personalLineOfCredit.Id }, GetDetailedLoanResponse(personalLineOfCredit));
    }

    private object GetDetailedLoanResponse(Loan loan)
    {
        // Base properties common to all loans
        var response = new 
        {
            loan.Id,
            loan.BorrowerId,
            loan.Principal,
            loan.InterestRate,
            loan.StartDate,
            loan.EndDate,
            Status = loan.Status.ToString(),
            LoanType = loan.GetType().Name,
            // Add specific properties based on loan type
            Details = GetLoanTypeSpecificDetails(loan)
        };
        
        return response;
    }

    private object GetLoanTypeSpecificDetails(Loan loan)
    {
        return loan switch
        {
            AutoLoan auto => new { 
                auto.VehicleVin, 
                auto.VehicleMake, 
                auto.VehicleModel, 
                auto.VehicleYear 
            },
            MortgageLoan mortgage => new { 
                mortgage.PropertyAddress, 
                mortgage.PropertyValue, 
                mortgage.LoanTermYears, 
                mortgage.IsFixedRate 
            },
            CreditCardLoan card => new { 
                card.CreditLimit, 
                card.AnnualFee, 
                card.RewardProgram 
            },
            PersonalLoan personal => new { 
                personal.Purpose, 
                personal.LoanTermMonths, 
                personal.IsSecured 
            },
            HelocLoan heloc => new { 
                heloc.PropertyAddress, 
                heloc.PropertyValue, 
                heloc.CreditLimit, 
                heloc.CurrentEquity, 
                heloc.DrawPeriodMonths 
            },
            PersonalLineOfCreditLoan ploc => new { 
                ploc.CreditLimit, 
                ploc.DrawPeriodMonths, 
                ploc.IsSecured 
            },
            _ => new { }
        };
    }
}