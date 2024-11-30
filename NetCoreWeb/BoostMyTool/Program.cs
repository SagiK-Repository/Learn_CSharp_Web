using BoostMyTool.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectInfo = new ConnectionDBInfo(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddSingleton<ConnectionDBInfo>(connectInfo);

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
