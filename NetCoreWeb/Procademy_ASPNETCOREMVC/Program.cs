var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpContext context) =>
{
    string path = context.Request.Path;
    string method = context.Request.Method;
    return "Request path: " + path + " Http Method: " + method;
});

app.Run();