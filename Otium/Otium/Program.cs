using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Otium.Domain.Models.Settings;
using Otium.Repositories;
using Otium.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Admin/Login");
        options.AccessDeniedPath = new PathString("/Admin/Login");
    });

var aspEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var appSettingsPath = "appsettings" + (aspEnv is null ? "" : $".{aspEnv}") + ".json";

#if DEBUG
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

Console.WriteLine("Debug mode");
#else
builder.Services.AddControllersWithViews();
System.Console.WriteLine("Release mode");
#endif

// Setting up the DI

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

// Getting configuration from appsettings.json

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile(appSettingsPath, true, true)
    .AddUserSecrets<Program>()
    .Build();

var dbConnectionSettings = configuration.GetSection("MsSql")
    .Get<DbConnectionModel>();
builder.Services.AddDbContext<ApplicationDbContext>(
    o => o.UseSqlServer(
        dbConnectionSettings.ConnectionString
    )
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (ctx, next) =>
{
    await next();
    if (ctx.Response.StatusCode != 200 && ctx.Response.StatusCode != 302)
        ctx.Response.Redirect($"/Error/{ctx.Response.StatusCode}");
});

app.Run();