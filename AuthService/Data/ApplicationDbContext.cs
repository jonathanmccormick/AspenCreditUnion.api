using AuthService.Models;
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
        
        // Configure RefreshToken
        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
        });

        // Configure RevokedToken
        builder.Entity<RevokedToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.JwtId).IsRequired();
            entity.HasIndex(e => e.JwtId);
        });
        
        // Configure ActiveToken
        builder.Entity<ActiveToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.JwtId).IsRequired();
            entity.Property(e => e.UserId).IsRequired();
            entity.HasIndex(e => e.JwtId);
            entity.HasIndex(e => e.UserId);
        });
    }
}