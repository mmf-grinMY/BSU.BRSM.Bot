using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BSU.BRSM.Bot.Data.EF;
internal class DbContextFactory
{
    private readonly IHttpContextAccessor httpContextAccessor;
    public DbContextFactory(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public BotDbContext Create(Type repositoryType)
    {
        var services = httpContextAccessor.HttpContext.RequestServices;
        var dbContexts = services.GetService<Dictionary<Type, BotDbContext>>();
        
        if (!dbContexts.ContainsKey(repositoryType))
            dbContexts[repositoryType] = services.GetService<BotDbContext>();

        return dbContexts[repositoryType];
    }
}