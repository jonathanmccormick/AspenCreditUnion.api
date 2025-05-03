using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ActiveToken> ActiveTokens { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configure Identity entities for SQL Server
        builder.Entity<ApplicationUser>(entity => 
        {
            entity.Property(e => e.Id).HasMaxLength(450);
        });
        
        builder.Entity<IdentityRole>(entity => 
        {
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.RoleId).HasMaxLength(450);
        });

        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);
        });
        
        // Configure RefreshToken
        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).IsRequired().HasMaxLength(450);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(450);
        });
        
        // Configure ActiveToken
        builder.Entity<ActiveToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.JwtId).IsRequired().HasMaxLength(450);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(450);
            entity.HasIndex(e => e.JwtId);
            entity.HasIndex(e => e.UserId);
        });
        
        // Configure Account hierarchy with TPH
        builder.Entity<Account>()
            .HasDiscriminator<string>("AccountType")
            .HasValue<CheckingAccount>("CheckingAccount")
            .HasValue<SavingsAccount>("SavingsAccount")
            .HasValue<CertificateOfDepositAccount>("CertificateOfDepositAccount")
            .HasValue<MoneyMarketAccount>("MoneyMarketAccount");
        
        builder.Entity<Account>()
            .Property(a => a.Balance)
            .HasPrecision(18, 2);
            
        builder.Entity<SavingsAccount>()
            .Property(a => a.InterestRate)
            .HasPrecision(5, 2);
            
        builder.Entity<CertificateOfDepositAccount>()
            .Property(a => a.InterestRate)
            .HasPrecision(5, 2);
            
        builder.Entity<MoneyMarketAccount>()
            .Property(a => a.InterestRate)
            .HasPrecision(5, 2);
            
        // Configure Loan hierarchy with TPH
        builder.Entity<Loan>()
            .HasDiscriminator<string>("LoanType")
            .HasValue<AutoLoan>("AutoLoan")
            .HasValue<MortgageLoan>("MortgageLoan")
            .HasValue<CreditCardLoan>("CreditCardLoan")
            .HasValue<PersonalLoan>("PersonalLoan")
            .HasValue<HelocLoan>("HelocLoan")
            .HasValue<PersonalLineOfCreditLoan>("PersonalLineOfCreditLoan");
            
        builder.Entity<Loan>()
            .Property(l => l.Principal)
            .HasPrecision(18, 2);
            
        builder.Entity<Loan>()
            .Property(l => l.InterestRate)
            .HasPrecision(5, 2);
            
        builder.Entity<MortgageLoan>()
            .Property(l => l.PropertyValue)
            .HasPrecision(18, 2);
            
        builder.Entity<CreditCardLoan>()
            .Property(l => l.CreditLimit)
            .HasPrecision(18, 2);
            
        builder.Entity<CreditCardLoan>()
            .Property(l => l.AnnualFee)
            .HasPrecision(10, 2);
            
        builder.Entity<HelocLoan>()
            .Property(l => l.PropertyValue)
            .HasPrecision(18, 2);
            
        builder.Entity<HelocLoan>()
            .Property(l => l.CreditLimit)
            .HasPrecision(18, 2);
            
        builder.Entity<HelocLoan>()
            .Property(l => l.CurrentEquity)
            .HasPrecision(18, 2);
            
        builder.Entity<PersonalLineOfCreditLoan>()
            .Property(l => l.CreditLimit)
            .HasPrecision(18, 2);
    }
}