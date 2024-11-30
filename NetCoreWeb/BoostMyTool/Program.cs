using BoostMyTool.Application.Interfaces;
using BoostMyTool.Model;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectInfo = new ConnectionSettings(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddSingleton<ISettings>(connectInfo)
                .AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();