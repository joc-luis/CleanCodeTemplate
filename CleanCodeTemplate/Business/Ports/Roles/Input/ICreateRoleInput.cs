using CleanCodeTemplate.Business.Dto.Roles.Requests;

namespace CleanCodeTemplate.Business.Ports.Roles.Input;

public interface ICreateRoleInput
{
    Task HandleAsync(CreateRoleRequest request, CancellationToken ct);
}