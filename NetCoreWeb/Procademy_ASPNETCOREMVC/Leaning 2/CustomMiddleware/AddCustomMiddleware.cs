namespace Procademy_ASPNETCOREMVC.CustomMiddleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class AddCustomMiddleware
{
    private readonly RequestDelegate _next;

    public AddCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        await httpContext.Response.WriteAsync("AddCustomMiddleware started!\n\n");
        await _next(httpContext);
        await httpContext.Response.WriteAsync("AddCustomMiddleware finished!\n\n");
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class AddCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseAddCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AddCustomMiddleware>();
    }
}
