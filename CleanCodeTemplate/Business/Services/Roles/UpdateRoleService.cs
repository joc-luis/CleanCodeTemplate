using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Roles.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Roles;

public class UpdateRoleService : IUpdateRoleInput
{
    private readonly IRoleRepository _roleRepository;
    private readonly IValidazione _validazione;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUpdateRoleOutput _output;

    public UpdateRoleService(IRoleRepository roleRepository, IValidazione validazione,
        IPermissionRepository permissionRepository, IUpdateRoleOutput output)
    {
        _roleRepository = roleRepository;
        _validazione = validazione;
        _permissionRepository = permissionRepository;
        _output = output;
    }

    public async Task HandleAsync(UpdateRoleRequest request, CancellationToken ct)
    {
        IEnumerable<string> names = await _roleRepository.GetAsync<string>(new Query().Select("Name").Where("Id", "<>", request.Id), ct);
        IEnumerable<Guid> permissions = await _permissionRepository.GetAsync<Guid>(new Query().Select("Id"), ct);
        
        _validazione.Field("Name", request.Name).Unique(names);
        _validazione.Field("Permissions", request.Permissions).Between(1, permissions.Count());
        foreach (Guid permission in request.Permissions)
        {
            _validazione.Field("Permissions", permission.ToString()).In(permissions.Select(p => p.ToString()));
        }
        _validazione.PassOrException();

        Role role = await _roleRepository.FirstOrDefault<Role>(request.Id, ct) ?? throw new NotFoundException();

        role.Name = request.Name;
        role.Permissions = request.Permissions;

        await _roleRepository.UpdateAsync(role, ct);

        await _output.HandleAsync("The role was successfully updated", ct);
    }
}