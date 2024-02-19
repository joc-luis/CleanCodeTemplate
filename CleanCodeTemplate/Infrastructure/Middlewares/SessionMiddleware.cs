using CleanCodeTemplate.Business.Modules.Tools;

namespace CleanCodeTemplate.Infrastructure.Middlewares;

public class SessionMiddleware
{
    private readonly RequestDelegate _next;

    public SessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        string? token = null;
        foreach (var header in httpContext.Request.Headers)
        {
            if (header.Key == "Authorization")
            {
                token = header.Value.ToString()?.Replace("Bearer ", "").Trim();
                break;
            }
        }

        if (String.IsNullOrEmpty(token))
        {
            foreach (var routeValue in httpContext.Request.Query)
            {
                if (routeValue.Key == "token" || routeValue.Key == "access_token")
                {
                    token =  routeValue.Value.ToString();
                    break;
                }
            }
        }

        if (!string.IsNullOrEmpty(token))
        {
            var webTokenTool = httpContext.RequestServices.GetRequiredService<IWebTokenTool>();
        
            await webTokenTool.SetSessionAsync(token, default);
        }

        await _next(httpContext);
    }
}