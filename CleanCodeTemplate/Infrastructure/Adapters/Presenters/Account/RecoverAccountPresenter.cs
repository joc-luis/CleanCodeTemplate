using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Ports.Account.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Account;

public class RecoverAccountPresenter : IRecoverAccountOutput, IPresenter<RecoverAccountResponse>
{
    public Task HandleAsync(RecoverAccountResponse response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public RecoverAccountResponse Response { get; private set; }
}