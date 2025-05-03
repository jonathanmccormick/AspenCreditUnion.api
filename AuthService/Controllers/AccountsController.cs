using System.Security.Claims;
using System.Text.Json;
using AuthService.Data;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(
        ApplicationDbContext dbContext,
        ILogger<AccountsController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounts()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var accounts = await _dbContext.Accounts
            .Where(a => a.OwnerId == userId)
            .ToListAsync();

        var detailedAccounts = accounts.Select(GetDetailedAccountResponse).ToList();
        return Ok(detailedAccounts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == id && a.OwnerId == userId);

        if (account == null)
        {
            return NotFound(new { message = "Account not found" });
        }

        return Ok(GetDetailedAccountResponse(account));
    }

    [HttpGet("types")]
    public IActionResult GetAccountTypes()
    {
        var accountTypes = new[]
        {
            new { Type = "Checking", Description = "A standard checking account for everyday transactions" },
            new { Type = "Savings", Description = "A savings account that earns interest" },
            new { Type = "CD", Description = "Certificate of Deposit with fixed term and higher interest rate" },
            new { Type = "MoneyMarket", Description = "High-interest savings account with limited transactions" }
        };

        return Ok(accountTypes);
    }

    [HttpPost("checking")]
    public async Task<IActionResult> CreateCheckingAccount([FromBody] CreateCheckingAccountRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var checkingAccount = new CheckingAccount
        {
            OwnerId = userId,
            Balance = request.InitialDeposit,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Accounts.Add(checkingAccount);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAccount), new { id = checkingAccount.Id }, GetDetailedAccountResponse(checkingAccount));
    }

    [HttpPost("savings")]
    public async Task<IActionResult> CreateSavingsAccount([FromBody] CreateSavingsAccountRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var savingsAccount = new SavingsAccount
        {
            OwnerId = userId,
            Balance = request.InitialDeposit,
            InterestRate = request.InterestRate,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Accounts.Add(savingsAccount);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAccount), new { id = savingsAccount.Id }, GetDetailedAccountResponse(savingsAccount));
    }

    [HttpPost("cd")]
    public async Task<IActionResult> CreateCDAccount([FromBody] CreateCDAccountRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var cdAccount = new CertificateOfDepositAccount
        {
            OwnerId = userId,
            Balance = request.InitialDeposit,
            InterestRate = request.InterestRate,
            MaturityDate = request.MaturityDate,
            AutoRenew = request.AutoRenew,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Accounts.Add(cdAccount);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAccount), new { id = cdAccount.Id }, GetDetailedAccountResponse(cdAccount));
    }

    [HttpPost("money-market")]
    public async Task<IActionResult> CreateMoneyMarketAccount([FromBody] CreateMoneyMarketAccountRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { message = "Invalid token" });
        }

        var moneyMarketAccount = new MoneyMarketAccount
        {
            OwnerId = userId,
            Balance = request.InitialDeposit,
            InterestRate = request.InterestRate,
            TransactionsPerMonth = request.TransactionsPerMonth,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Accounts.Add(moneyMarketAccount);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAccount), new { id = moneyMarketAccount.Id }, GetDetailedAccountResponse(moneyMarketAccount));
    }

    private object GetDetailedAccountResponse(Account account)
    {
        // Base properties common to all accounts
        var response = new 
        {
            account.Id,
            account.OwnerId,
            account.Balance,
            account.CreatedAt,
            AccountType = account.GetType().Name,
            // Add specific properties based on account type
            Details = GetAccountTypeSpecificDetails(account)
        };
        
        return response;
    }

    private object GetAccountTypeSpecificDetails(Account account)
    {
        return account switch
        {
            CheckingAccount _ => new { },
            SavingsAccount savings => new { savings.InterestRate },
            CertificateOfDepositAccount cd => new { cd.InterestRate, cd.MaturityDate, cd.AutoRenew },
            MoneyMarketAccount mm => new { mm.InterestRate, mm.TransactionsPerMonth },
            _ => new { }
        };
    }
}