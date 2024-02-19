using CleanCodeTemplate.Business.Ports.Locks.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Locks;

public class DestroyBlockedPresenter : IDestroyBlockedOutput, IPresenter<string>
{
    public Task HandleAsync(string response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public string Response { get; private set; }
}