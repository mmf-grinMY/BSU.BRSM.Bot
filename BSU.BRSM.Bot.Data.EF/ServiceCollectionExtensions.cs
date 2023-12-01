using BSU.BRSM.Bot.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BSU.BRSM.Bot.Data.EF;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEfRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<BotDbContext>(
            options =>
            {
                options.UseSqlite(connectionString);
            },
            ServiceLifetime.Transient
        );

        services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
            .AddScoped<Dictionary<Type, BotDbContext>>()
            .AddSingleton<DbContextFactory>()
            .AddSingleton<IUserRepository, UserRepository>();

        return services;
    }
}
