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
    public DbSet<RevokedToken> RevokedTokens { get; set; }
    public DbSet<ActiveToken> ActiveTokens { get; set; }

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

        // Configure RevokedToken
        builder.Entity<RevokedToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.JwtId).IsRequired().HasMaxLength(450);
            entity.HasIndex(e => e.JwtId);
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
    }
}