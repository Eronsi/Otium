using Microsoft.Extensions.DependencyInjection;
using Otium.Repositories.Abstractions;
using Otium.Repositories.Implementations;
using Otium.Services.Abstractions;
using Otium.Services.Implementations;

namespace Otium.Services.Extensions;

public static class Initializer
{
    public static void InitializeRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<IParamsRepository, ParamsRepository>();
        services.AddScoped<IParamsValuesRepository, ParamsValuesRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
    
    public static void InitializeServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<IParamsService, ParamsService>();
        services.AddScoped<IParamsValuesService, ParamsValuesService>();
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}