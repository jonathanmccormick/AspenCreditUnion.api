using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.Data;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuthController> _logger;
    private readonly TokenValidationService _tokenValidationService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ApplicationDbContext context,
        ILogger<AuthController> logger,
        TokenValidationService tokenValidationService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
        _logger = logger;
        _tokenValidationService = tokenValidationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (model.Password != model.ConfirmPassword)
        {
            return BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Passwords do not match"
            });
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            CreatedAt = DateTime.UtcNow
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password");
            
            // Generate JWT token
            var token = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);
            
            // Register the token in our whitelist
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;

            // Get device info and IP address
            var deviceInfo = Request.Headers["User-Agent"].ToString();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            
            await _tokenValidationService.RegisterTokenAsync(
                token,
                user.Id,
                expiry,
                deviceInfo,
                ipAddress
            );

            return Ok(new AuthResponse
            {
                Success = true,
                Message = "User registered successfully",
                Token = token,
                Expiration = refreshToken.ExpiryDate,
                RefreshToken = refreshToken.Token
            });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return BadRequest(new AuthResponse
        {
            Success = false,
            Message = "User registration failed: " + string.Join(", ", result.Errors.Select(e => e.Description))
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Unauthorized(new AuthResponse
            {
                Success = false,
                Message = "Invalid login attempt"
            });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            // Update last login date
            user.LastLoginAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            _logger.LogInformation("User logged in");

            // Generate JWT token
            var token = await GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);

            // Register the token in our whitelist
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;

            // Get device info and IP address
            var deviceInfo = Request.Headers["User-Agent"].ToString();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            
            await _tokenValidationService.RegisterTokenAsync(
                token,
                user.Id,
                expiry,
                deviceInfo,
                ipAddress
            );

            return Ok(new AuthResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Expiration = refreshToken.ExpiryDate,
                RefreshToken = refreshToken.Token
            });
        }

        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out");
            return StatusCode(StatusCodes.Status403Forbidden, new AuthResponse
            {
                Success = false,
                Message = "Account is locked out"
            });
        }

        return Unauthorized(new AuthResponse
        {
            Success = false,
            Message = "Invalid login attempt"
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "Invalid token" });
        }

        // Get the JWT token from the request header
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        
        // Parse the token to get the claims
        var tokenHandler = new JwtSecurityTokenHandler();
        if (tokenHandler.CanReadToken(token))
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var jwtId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var expiry = jwtToken.ValidTo;
            
            if (!string.IsNullOrEmpty(jwtId))
            {
                // Mark the token as used in our whitelist
                await _tokenValidationService.MarkTokenAsUsedAsync(jwtId);
            }
        }

        // Revoke all active refresh tokens for the user
        var userRefreshTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.RevokedDate == null)
            .ToListAsync();

        foreach (var refreshToken in userRefreshTokens)
        {
            refreshToken.RevokedDate = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        return Ok(new { message = "Logged out successfully" });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest model)
    {
        if (model == null || string.IsNullOrEmpty(model.RefreshToken))
        {
            return BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Refresh token is required"
            });
        }

        var refreshToken = await _context.RefreshTokens
            .SingleOrDefaultAsync(rt => rt.Token == model.RefreshToken);

        if (refreshToken == null || !refreshToken.IsActive)
        {
            return Unauthorized(new AuthResponse
            {
                Success = false,
                Message = "Invalid or expired refresh token"
            });
        }

        var user = await _userManager.FindByIdAsync(refreshToken.UserId);
        if (user == null)
        {
            return Unauthorized(new AuthResponse
            {
                Success = false,
                Message = "User not found"
            });
        }

        // Revoke the current refresh token
        refreshToken.RevokedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Generate a new JWT token and refresh token
        var token = await GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken(user.Id);

        // Register the new token in our whitelist
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        var expiry = jwtToken.ValidTo;

        // Get device info and IP address
        var deviceInfo = Request.Headers["User-Agent"].ToString();
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            
        await _tokenValidationService.RegisterTokenAsync(
            token,
            user.Id,
            expiry,
            deviceInfo,
            ipAddress
        );

        return Ok(new AuthResponse
        {
            Success = true,
            Message = "Token refreshed successfully",
            Token = token,
            Expiration = newRefreshToken.ExpiryDate,
            RefreshToken = newRefreshToken.Token
        });
    }

    // Helper methods
    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not found.");
        var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found.");
        var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience not found.");
        var expiryMinutes = int.Parse(jwtSettings["ExpirationInMinutes"] ?? "60");

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName)
        };

        // Add user roles to claims
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private RefreshToken GenerateRefreshToken(string userId)
    {
        using var rng = RandomNumberGenerator.Create();
        var randomBytes = new byte[64];
        rng.GetBytes(randomBytes);
        
        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(randomBytes),
            ExpiryDate = DateTime.UtcNow.AddDays(7), // Refresh tokens typically last longer than JWT
            CreatedDate = DateTime.UtcNow
        };

        _context.RefreshTokens.Add(refreshToken);
        _context.SaveChanges();

        return refreshToken;
    }

    [Authorize]
    [HttpGet("active-sessions")]
    public async Task<IActionResult> GetActiveSessions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "Invalid token" });
        }

        var activeSessions = await _context.ActiveTokens
            .Where(t => t.UserId == userId && !t.IsUsed && t.ExpiryDate > DateTime.UtcNow)
            .Select(t => new
            {
                t.Id,
                t.DeviceInfo,
                t.IpAddress,
                t.IssuedAt,
                t.ExpiryDate
            })
            .ToListAsync();

        return Ok(new { activeSessions });
    }

    [Authorize]
    [HttpPost("revoke-session/{id}")]
    public async Task<IActionResult> RevokeSession(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "Invalid token" });
        }

        var session = await _context.ActiveTokens
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (session == null)
        {
            return NotFound(new { message = "Session not found" });
        }

        session.IsUsed = true;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Session revoked successfully" });
    }

    [Authorize]
    [HttpPost("revoke-all-sessions")]
    public async Task<IActionResult> RevokeAllSessions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new { message = "Invalid token" });
        }

        await _tokenValidationService.RevokeAllUserTokensAsync(userId);

        return Ok(new { message = "All sessions revoked successfully" });
    }
}