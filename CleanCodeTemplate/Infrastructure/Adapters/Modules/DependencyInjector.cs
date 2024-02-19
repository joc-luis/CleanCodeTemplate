using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Infrastructure.Adapters.Modules.Tools;
using ValidaZione;
using ValidaZione.Interfaces;
using ValidaZione.Langs;

namespace CleanCodeTemplate.Infrastructure.Adapters.Modules;

public static class DependencyInjector
{
    public static IServiceCollection AddTools(this IServiceCollection collection)
    {
        collection.AddSingleton<ICryptographyTool, CryptographyTool>();
        collection.AddScoped<IEmailTool, EmailTool>();
        collection.AddScoped<IValidazione>(v => new Validazione(Language.En));
        collection.AddScoped<IWebTokenTool, WebTokenTool>();
        collection.AddSingleton<ILiteCachingTool, LiteCachingTool>();
        collection.AddSingleton<IBlockedCachingTool, BlockedCachingTool>();
        collection.AddSingleton<IMemoryCachingTool, MemoryCachingTool>();
        return collection;
    }
}