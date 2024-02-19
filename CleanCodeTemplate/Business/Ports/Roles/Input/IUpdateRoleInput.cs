using CleanCodeTemplate.Business.Dto.Roles.Requests;

namespace CleanCodeTemplate.Business.Ports.Roles.Input;

public interface IUpdateRoleInput
{
    Task HandleAsync(UpdateRoleRequest request, CancellationToken ct);
}