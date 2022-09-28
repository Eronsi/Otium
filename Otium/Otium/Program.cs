using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Otium.Domain.Models.Settings;
using Otium.Repositories;
using Otium.Repositories.Implementations;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;
using Otium.Services.Implementations;

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

// Setting up the DI for the repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICallbacksRepository, CallbacksRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>(); 
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IParamsRepository, ParamsRepository>();
builder.Services.AddScoped<IParamsValuesRepository, ParamsValuesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

// Setting up the DI for the services

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICallbacksService, CallbacksService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IParamsService, ParamsService>();
builder.Services.AddScoped<IParamsValuesService, ParamsValuesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IEmailService, EmailService>();

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