using CleanCodeTemplate.Business.Ports.Roles.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Roles;

public class UpdateRolePresenter : IUpdateRoleOutput, IPresenter<string>
{
    public Task HandleAsync(string response, CancellationToken ct)
    {
        Response = response;

        return Task.CompletedTask;
    }

    public string Response { get; private set; }
}