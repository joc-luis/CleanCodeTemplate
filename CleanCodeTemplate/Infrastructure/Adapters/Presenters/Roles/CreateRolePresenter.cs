using CleanCodeTemplate.Business.Ports.Roles.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Roles;

public class CreateRolePresenter : ICreateRoleOutput, IPresenter<string>
{
    public Task HandleAsync(string response, CancellationToken ct)
    {
        Response = response;

        return Task.CompletedTask;
    }

    public string Response { get; private set; }
}