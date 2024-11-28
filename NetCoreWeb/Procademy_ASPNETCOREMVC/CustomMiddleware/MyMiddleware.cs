namespace Procademy_ASPNETCOREMVC.CustomMiddleware;

public class MyMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("Custom Middleware started!\n\n");
        await next(context);
        await context.Response.WriteAsync("Custom Middleware finished!\n\n");
    }
}