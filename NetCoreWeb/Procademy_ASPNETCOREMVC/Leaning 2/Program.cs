using Procademy_ASPNETCOREMVC.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyMiddleware>();
var app = builder.Build();

// Middleware 1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Welcome from ASP.NET Core App!\n");
    await next(context);
});

// Middleware 2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("\n\n");
    await next(context);
});

// Middleware 3, 4, 5
app.UseMiddleware<MyMiddleware>();
app.MyMiddleware();
app.UseAddCustomMiddleware();

// Middleware 6
app.UseWhen(context => context.Request.Query.ContainsKey("IsAuthorized") && context.Request.Query["IsAuthorized"] == "true",
    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Middleware 6 called\n");
            await next(context);
        });
    });

// Middleware 7
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("Welcome from ASP.NET Core App!\n");
});

app.Run();