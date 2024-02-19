using CleanCodeTemplate.Business.Dto.Roles.Responses;

namespace CleanCodeTemplate.Business.Ports.Roles.Output;

public interface IGetRoleOutput
{
    Task HandleAsync(IEnumerable<GetRoleResponse> response, CancellationToken ct);
}