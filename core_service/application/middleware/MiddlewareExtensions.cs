namespace core_service.application.middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseErrorBoundary(this IApplicationBuilder builder)
        => builder.UseMiddleware<ErrorBoundaryMiddleware>();
}
