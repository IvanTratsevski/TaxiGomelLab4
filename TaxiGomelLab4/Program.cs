using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TaxiGomelLab4.Data;
using TaxiGomelLab4.Services;
using TaxiGomelLab4.Models;
using TaxiGomelLab4.Middleware;
using System.Diagnostics;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string conStr = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddDbContext<TaxiGomelContext>(options => options.UseSqlServer(conStr));
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<ICachedService<Car>, CachedCarsService>();
builder.Services.AddScoped<ICachedService<CarModel>, CachedCarModelsService>();
builder.Services.AddScoped<ICachedService<Rate>, CachedRatesService>();

var app = builder.Build();
app.UseDbInitializer();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
