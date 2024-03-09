using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Roles;

public class DestroyRoleService : IDestroyRoleInput
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleCachingTool _roleCachingTool;
    private readonly IDestroyRoleOutput _output;

    public DestroyRoleService(IRoleRepository roleRepository, IDestroyRoleOutput output, IUserRepository userRepository, IRoleCachingTool roleCachingTool)
    {
        _roleRepository = roleRepository;
        _output = output;
        _userRepository = userRepository;
        _roleCachingTool = roleCachingTool;
    }

    public async Task HandleAsync(Guid id, CancellationToken ct)
    {
        User?user = await _userRepository.FirstOrDefaultAsync<User>(new Query().Where("RoleId", id), ct);

        if (user != null)
        {
            throw new ForbiddenException();
        }

        await _roleCachingTool.RemoveAsync(id.ToString(), ct);
        
        await _roleRepository.DestroyAsync(id, ct);

        await _output.HandleAsync("The role was successfully deleted.", ct);
    }
}