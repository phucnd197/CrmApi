using Crm_Api.Shared.Model;
using Shared.Contracts.Services;

namespace Crm_Api.Middleware;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var logger = context.RequestServices.GetRequiredService<IEsoftLog<ErrorLoggingMiddleware>>();
            logger.LogError(ex, "Unhandled exception: {error}", ex.Message);
            await context.Response.WriteAsJsonAsync(
                Result.Fail<object>("Something went wrong, please try again in a few minute"));
        }
    }
}