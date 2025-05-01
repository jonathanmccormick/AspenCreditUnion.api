using AuthService.Middleware;

namespace AuthService.Extensions;

public static class TokenValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenValidation(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenValidationMiddleware>();
    }
}