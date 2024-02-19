using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Roles.Responses;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Roles.Output;

namespace CleanCodeTemplate.Business.Services.Roles;

public class GetRoleService : IGetRoleInput
{
    private readonly IRoleRepository _roleRepository;
    private readonly IGetRoleOutput _output;

    public GetRoleService(IRoleRepository roleRepository, IGetRoleOutput output)
    {
        _roleRepository = roleRepository;
        _output = output;
    }

    public async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<GetRoleResponse> response = await _roleRepository.GetAsync<GetRoleResponse>(ct);

        await _output.HandleAsync(response, ct);
    }
}