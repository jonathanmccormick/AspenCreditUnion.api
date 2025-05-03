using System.IdentityModel.Tokens.Jwt;
using AuthService.Data;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services;

public class TokenValidationService
{
    private readonly ApplicationDbContext _context;

    public TokenValidationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsTokenValidAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (!tokenHandler.CanReadToken(token))
        {
            return false; // Invalid token format
        }

        var jwtToken = tokenHandler.ReadJwtToken(token);
        var jwtId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

        if (string.IsNullOrEmpty(jwtId))
        {
            return false; // No JTI claim, consider it invalid
        }

        // Check if the token exists in the active tokens list and is not marked as used
        var activeToken = await _context.ActiveTokens
            .FirstOrDefaultAsync(at => at.JwtId == jwtId && !at.IsUsed);

        return activeToken != null;
    }

    public async Task<ActiveToken> RegisterTokenAsync(string token, string userId, DateTime expiryDate, string? deviceInfo = null, string? ipAddress = null)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var jwtId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value 
            ?? throw new ArgumentException("Token does not contain a valid JTI claim");

        var activeToken = new ActiveToken
        {
            JwtId = jwtId,
            UserId = userId,
            IssuedAt = DateTime.UtcNow,
            ExpiryDate = expiryDate,
            IsUsed = false,
            DeviceInfo = deviceInfo,
            IpAddress = ipAddress
        };

        await _context.ActiveTokens.AddAsync(activeToken);
        await _context.SaveChangesAsync();

        return activeToken;
    }

    public async Task MarkTokenAsUsedAsync(string jwtId)
    {
        var token = await _context.ActiveTokens.FirstOrDefaultAsync(t => t.JwtId == jwtId);
        if (token != null)
        {
            token.IsUsed = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RevokeAllUserTokensAsync(string userId)
    {
        var userTokens = await _context.ActiveTokens
            .Where(t => t.UserId == userId && !t.IsUsed)
            .ToListAsync();

        foreach (var token in userTokens)
        {
            token.IsUsed = true;
        }

        await _context.SaveChangesAsync();
    }

    // Clean up expired tokens
    public async Task CleanupExpiredTokensAsync()
    {
        // Clean up expired active tokens
        var expiredActiveTokens = await _context.ActiveTokens
            .Where(at => at.ExpiryDate < DateTime.UtcNow)
            .ToListAsync();

        if (expiredActiveTokens.Any())
        {
            _context.ActiveTokens.RemoveRange(expiredActiveTokens);
            await _context.SaveChangesAsync();
        }
    }
}