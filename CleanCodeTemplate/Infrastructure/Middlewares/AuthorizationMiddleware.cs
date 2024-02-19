using CleanCodeTemplate.Business.Dto.Authorizations;
using CleanCodeTemplate.Business.Ports.Authorizations.Input;

namespace CleanCodeTemplate.Infrastructure.Middlewares;

public class AuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {

        var haveAuthorization = httpContext.RequestServices.GetRequiredService<IHaveAuthorizationInput>();

        await haveAuthorization.HandleAsync(new AuthorizationDto()
        {
            Url = httpContext.Request.Path.ToString(),
            Method = httpContext.Request.Method
        }, default);
        
        await _next(httpContext);
    }
}