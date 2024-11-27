var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    var userAgent = string.Empty;

    if (path == "/" || path == "/Home")
    {
        context.Response.Headers["Content-Type"] = "text/html";
        context.Response.Headers["MyHeader"] = "Hello, World";

        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("<h2>This is a Text response</h2>");
    }
    else if (path == "/Contact")
    {
        if (context.Request.Headers.ContainsKey("User-Agent"))
            userAgent = context.Request.Headers["User-Agent"];

        context.Response.StatusCode = 200;
        await context.Response.WriteAsync("Request path: " + path + " Http Method: " + method + " User Agent: " + userAgent);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("The page you are looking for is not found!");
    }
});