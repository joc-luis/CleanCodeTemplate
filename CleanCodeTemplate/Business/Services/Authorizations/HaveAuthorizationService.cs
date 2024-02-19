using System.Text.RegularExpressions;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Authorizations;
using CleanCodeTemplate.Business.Dto.Permissions;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Authorizations.Input;
using EasyCaching.Core;

namespace CleanCodeTemplate.Business.Services.Authorizations;

public class HaveAuthorizationService : IHaveAuthorizationInput
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IMemoryCachingTool _cachingProvider;

    public HaveAuthorizationService(IPermissionRepository permissionRepository, IWebTokenTool webTokenTool,
        IMemoryCachingTool cachingProvider)
    {
        _permissionRepository = permissionRepository;
        _webTokenTool = webTokenTool;
        _cachingProvider = cachingProvider;
    }

    public async Task HandleAsync(AuthorizationDto auth, CancellationToken ct)
    {
        IEnumerable<PermissionDto> permissions;

        if (await _cachingProvider.ExistsAsync("Permissions", ct))
        {
            permissions = await _cachingProvider.GetAsync<IEnumerable<PermissionDto>>("Permissions", ct);
        }
        else
        {
            permissions = await _permissionRepository.GetAsync<PermissionDto>(ct);
            await _cachingProvider.SetAsync("Permissions", permissions, TimeSpan.FromDays(30), ct);
        }

        foreach (var permission in permissions)
        {
            var exists = permission.Method == auth.Method && new Regex(permission.Url).IsMatch(auth.Url);
           
            if (exists)
            {
                if (!_webTokenTool.SessionAccount.Permissions.Any(p =>
                        p.Url == permission.Url && p.Method == permission.Method))
                {
                    throw new ForbiddenException();
                }
            }
        }
    }
}