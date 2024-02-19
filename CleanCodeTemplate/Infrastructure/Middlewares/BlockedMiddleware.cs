using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;

namespace CleanCodeTemplate.Infrastructure.Middlewares;

public class BlockedMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IBlockedCachingTool _blockedCachingTool;

    public BlockedMiddleware(RequestDelegate next, IBlockedCachingTool blockedCachingTool)
    {
        _next = next;
        _blockedCachingTool = blockedCachingTool;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var webTokenTool = httpContext.RequestServices.GetRequiredService<IWebTokenTool>();

        if (await _blockedCachingTool.ExistsAsync(webTokenTool.SessionAccount.Id.ToString(), default))
        {
            throw new UnauthorizedException();
        }

        await _next(httpContext);
    }
}