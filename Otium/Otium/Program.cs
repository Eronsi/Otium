using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Otium.Domain.Models.Settings;
using Otium.Repositories;
using Otium.Repositories.Implementations;
using Otium.Repositories.Interfaces;
using Otium.Services.Abstractions;
using Otium.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Setting up the DI for the repositories

builder.Services.AddScoped<IAdminsRepository, AdminsRepository>();
builder.Services.AddScoped<ICallbacksRepository, CallbacksRepository>();
// builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>(); 
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IParamsRepository, ParamsRepository>();
builder.Services.AddScoped<IParamsValuesRepository, ParamsValuesRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();

// Setting up the DI for the services

builder.Services.AddScoped<IAdminService, AdminsService>();
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
    .AddJsonFile("appsettings.json", true, true)
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

app.UseHsts();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Use(async (ctx, next) =>
{
    await next();
    if (ctx.Response.StatusCode != 200)
        ctx.Response.Redirect($"/Error/{ctx.Response.StatusCode}");
});

app.Run();