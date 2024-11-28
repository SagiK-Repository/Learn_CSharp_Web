var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.Use(async (context, next) =>
{
    Endpoint endpoint = context.GetEndpoint()!;
    if (endpoint != null)
        await context.Response.WriteAsync(endpoint.DisplayName!);
    await next();
});

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

    _ = endpoints.MapGet("/Product/{id:int}", async (context) =>
    {
        var id = context.Request.RouteValues["ID"];
        
        if (id != null)
            await context.Response.WriteAsync("\n\nThis is product with ID : " + id);

        await context.Response.WriteAsync("\n\nYour are in Product page");
    });

    _ = endpoints.MapGet("/Product/book/author/{authorname=john-smith}/{bookid=1}", async (context) =>
    {
        var bookId = Convert.ToInt32(context.Request.RouteValues["bookid"]);
        var authorName = Convert.ToString(context.Request.RouteValues["authorname"]); 
        await context.Response.WriteAsync($"\n\nThis is the book authored by {authorName}, book id: {bookId}" );
    });
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("The page you are looking for is not found!");
});

app.Run();