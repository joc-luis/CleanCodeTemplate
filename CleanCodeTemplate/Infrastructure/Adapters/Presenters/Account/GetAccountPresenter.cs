using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Ports.Account.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Account;

public class GetAccountPresenter : IGetAccountOutput, IPresenter<GetAccountResponse>
{
    public Task HandleAsync(GetAccountResponse response, CancellationToken ct)
    {
        Response = response;

        return Task.CompletedTask;
    }

    public GetAccountResponse Response { get; private set; }
}