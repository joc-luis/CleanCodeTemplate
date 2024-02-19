using CleanCodeTemplate.Business.Exceptions.Http;
using Serilog;
using ValidaZione.Exceptions;

namespace CleanCodeTemplate.Infrastructure.Middlewares;

public class CatchMiddleware
{
    private readonly RequestDelegate _next;

    public CatchMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException e)
        {
            Log.Warning(e.ToString());
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(e.Message);
        }
        catch (ForbiddenException e)
        {
            Log.Warning(e.ToString());
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsJsonAsync(e.Message);
        }
        catch (UnauthorizedException e)
        {
            Log.Warning(e.ToString());
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsJsonAsync(e.Message);
        }
        catch (BadRequestException e)
        {
            Log.Warning(e.ToString());
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(e.Message);
        }
        catch (ValidazioneException e)
        {
            Log.Warning(e.ToString());
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(e.Message);
        }
        catch (Exception e)
        {
            Log.Error(e.ToString());
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(e.Message);
        }
    }
}