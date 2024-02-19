using CleanCodeTemplate.Business.Dto.Roles.Responses;
using CleanCodeTemplate.Business.Ports.Roles.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Roles;

public class GetRolePresenter : IGetRoleOutput, IPresenter<IEnumerable<GetRoleResponse>>
{
    public Task HandleAsync(IEnumerable<GetRoleResponse> response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public IEnumerable<GetRoleResponse> Response { get; private set; }
}