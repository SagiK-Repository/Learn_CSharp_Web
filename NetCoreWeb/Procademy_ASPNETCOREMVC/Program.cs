var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    var userAgent = string.Empty;
    
    if (context.Request.Headers.ContainsKey("User-Agent"))
        userAgent = context.Request.Headers["User-Agent"];

    return "Request path: " + path + " Http Method: " + method + " User Agent: " + userAgent;
});

app.Run();