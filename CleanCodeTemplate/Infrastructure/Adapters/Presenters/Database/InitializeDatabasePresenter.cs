using CleanCodeTemplate.Business.Ports.Database.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Database;

public class InitializeDatabasePresenter : IInitializeDatabaseOutput, IPresenter<string>
{
    public Task HandleAsync(string response)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public string Response { get; private set; }
}