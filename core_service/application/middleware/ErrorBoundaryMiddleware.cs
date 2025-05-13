namespace core_service.application.middleware;

public class ErrorBoundaryMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            var mes = $@"
                         Исключение: {ex.Message}
                         Метод: {ex.TargetSite}
                         Трассировка стека: {ex.StackTrace}
                       ";
            
            await context.Response.WriteAsync(mes);
        }
    }
}
