using BookStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookStore.Models;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? connectionString;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (env == "Development")
{
    connectionString = builder.Configuration.GetConnectionString("BookStoreConnection");
} else
{
    var connHost = Environment.GetEnvironmentVariable("Host");
    var connDb = Environment.GetEnvironmentVariable("Database");
    var connUser = Environment.GetEnvironmentVariable("User");
    var connPass = Environment.GetEnvironmentVariable("Password");
    connectionString = $"Server={connHost};Port=3306;Database={connDb};User={connUser};Password={connPass};";
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString));
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddRazorPages();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    await ConfigureIdentity.CreateAdminUserAsync(services);
    SeedData.SeedDB(context);
}

app.UseSession();

app.MapRazorPages();

app.Run();