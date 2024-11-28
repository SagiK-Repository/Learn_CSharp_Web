using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
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
    string path = context.Request.Path;
    string method = context.Request.Method;
    var userAgent = string.Empty;

    if (path == "/" || path == "/Home")
    {
        await context.Response.WriteAsync("Welcome from ASP.NET Core App!\n");
    }
    else if (path == "/Status")
    {
        context.Response.Headers["Content-Type"] = "text/html";
        context.Response.Headers["MyHeader"] = "Hello, World";

        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("<h2>This is a Text response</h2>");
    }
    else if (method == "GET" && path == "/Product")
    {
        context.Response.StatusCode = 200;

        var query = context.Request.Query;
        if (query.ContainsKey("id") && query.ContainsKey("name"))
        {
            string id = query["id"]!;
            string name = query["name"]!;
            await context.Response.WriteAsync($"Your selected the product with ID : {id}, Name : {name}");
            return;
        }

        await context.Response.WriteAsync("Your are in Products page");
    }
    else if (method == "POST" && path == "/Product")
    {
        string id = "", name = "";
        StreamReader reader = new StreamReader(context.Request.Body);
        string data = await reader.ReadToEndAsync();
        Dictionary<string, StringValues> dict = QueryHelpers.ParseQuery(data);

        if (dict.ContainsKey("id") && dict.ContainsKey("name"))
        {
            await context.Response.WriteAsync($"ID is : {dict["id"]!}\nName is : {dict["name"][0]}");
            return;
        }
        await context.Response.WriteAsync($"Request body contains: {data}");
    }
    else if (path == "/Contact")
    {
        if (context.Request.Headers.ContainsKey("User-Agent"))
            userAgent = context.Request.Headers["User-Agent"];

        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("Request path: " + path + " Http Method: " + method + " User Agent: " + userAgent);
    }
    else if (path == "/favicon.ico")
    {
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("The page you are looking for is not found!");
    }
});

app.Run();