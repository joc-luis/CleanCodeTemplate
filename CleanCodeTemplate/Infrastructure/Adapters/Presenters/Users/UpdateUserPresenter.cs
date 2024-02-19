using CleanCodeTemplate.Business.Ports.Users.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Users;

public class UpdateUserPresenter : IUpdateUserOutput, IPresenter<string>
{
    public Task HandleAsync(string response, CancellationToken ct)
    {
        Response = response;

        return Task.CompletedTask;
    }

    public string Response { get; private set; }
}