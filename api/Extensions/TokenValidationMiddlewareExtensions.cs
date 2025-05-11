using AspenCreditUnion.api.Middleware;

namespace AspenCreditUnion.api.Extensions;

public static class TokenValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenValidation(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenValidationMiddleware>();
    }
}