using BoostMyTool.Application.Interfaces;
using BoostMyTool.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var connectInfo = new ConnectionSettings(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddSingleton<ISettings>(connectInfo);

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