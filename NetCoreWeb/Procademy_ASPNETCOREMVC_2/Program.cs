var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.Map("/Home", async (context) =>
    {
        await context.Response.WriteAsync("Your are in Home page");
    });

    _ = endpoints.MapGet("/Product", async (context) =>
    {
        await context.Response.WriteAsync("Your are in Product page");
    });

    _ = endpoints.MapPost("/Product", async (context) =>
    {
        await context.Response.WriteAsync("A new Product created");
    });
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("The page you are looking for is not found!");
});

app.Run();