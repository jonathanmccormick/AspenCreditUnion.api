using System.IdentityModel.Tokens.Jwt;
using AuthService.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services;

public class TokenValidationService
{
    private readonly ApplicationDbContext _context;

    public TokenValidationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsTokenRevokedAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (!tokenHandler.CanReadToken(token))
        {
            return true; // Invalid token format, consider it revoked
        }

        var jwtToken = tokenHandler.ReadJwtToken(token);
        var jwtId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

        if (string.IsNullOrEmpty(jwtId))
        {
            return true; // No JTI claim, consider it revoked
        }

        // Check if the token is in the revoked tokens list
        var isRevoked = await _context.RevokedTokens.AnyAsync(rt => rt.JwtId == jwtId);

        return isRevoked;
    }

    // Clean up expired revoked tokens (can be called periodically via background service)
    public async Task CleanupExpiredTokensAsync()
    {
        var expiredTokens = await _context.RevokedTokens
            .Where(rt => rt.ExpiryDate < DateTime.UtcNow)
            .ToListAsync();

        if (expiredTokens.Any())
        {
            _context.RevokedTokens.RemoveRange(expiredTokens);
            await _context.SaveChangesAsync();
        }
    }
}