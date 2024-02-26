using System.Text.RegularExpressions;
using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Authorizations;
using CleanCodeTemplate.Business.Dto.Permissions;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Authorizations.Input;
using EasyCaching.Core;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Authorizations;

public class HaveAuthorizationService : IHaveAuthorizationInput
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IMemoryCachingTool _cachingProvider;
    private readonly IRoleCachingTool _roleCachingTool;
    private readonly IRoleRepository _roleRepository;

    public HaveAuthorizationService(IPermissionRepository permissionRepository, IWebTokenTool webTokenTool,
        IMemoryCachingTool cachingProvider, IRoleCachingTool roleCachingTool, IRoleRepository roleRepository)
    {
        _permissionRepository = permissionRepository;
        _webTokenTool = webTokenTool;
        _cachingProvider = cachingProvider;
        _roleCachingTool = roleCachingTool;
        _roleRepository = roleRepository;
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
                IEnumerable<Permission> rolePermissions;
                
                if (await _roleCachingTool.ExistAsync(_webTokenTool.SessionAccount.RoleId.ToString(), ct))
                {
                    rolePermissions =
                        await _roleCachingTool.GetAsync<IEnumerable<Permission>>(_webTokenTool.SessionAccount.RoleId.ToString(), ct);
                }
                else
                {
                    Role? role = await _roleRepository.FirstOrDefault<Role>(_webTokenTool.SessionAccount.RoleId, ct);

                    if (role == null)
                    {
                        throw new UnauthorizedException();
                    }

                    rolePermissions =
                        await _permissionRepository.GetAsync<Permission>(new Query().WhereIn("Id", role.Permissions), ct);

                    await _roleCachingTool.SetAsync(role.Id.ToString(), rolePermissions, TimeSpan.FromDays(30), ct);
                }
                
                if (!rolePermissions.Any(p =>
                        p.Url == permission.Url && p.Method == permission.Method))
                {
                    throw new ForbiddenException();
                }
            }
        }
    }
}