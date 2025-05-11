using System.Security.Claims;
using AspenCreditUnion.api.Services;

namespace AspenCreditUnion.api.Middleware;

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
                var isValid = await tokenValidationService.IsTokenValidAsync(token);
                
                if (!isValid)
                {
                    // If the token is not in our whitelist or is marked as used, return 401 Unauthorized
                    context.Response.Clear();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"message\":\"Invalid or expired token\"}");
                    return;
                }
            }
        }

        await _next(context);
    }
}