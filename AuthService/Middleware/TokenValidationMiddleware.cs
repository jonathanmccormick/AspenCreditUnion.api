using System.Security.Claims;
using AuthService.Services;

namespace AuthService.Middleware;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, TokenValidationService tokenValidationService)
    {
        var authHeader = context.Request.Headers["Authorization"].ToString();
        
        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
        {
            var token = authHeader.Substring("Bearer ".Length).Trim();
            
            if (!string.IsNullOrEmpty(token))
            {
                var isRevoked = await tokenValidationService.IsTokenRevokedAsync(token);
                
                if (isRevoked)
                {
                    // If the token is revoked, clear the user identity and return 401 Unauthorized
                    context.Response.Clear();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\":\"Token has been revoked\"}");
                    return;
                }
            }
        }

        await _next(context);
    }
}