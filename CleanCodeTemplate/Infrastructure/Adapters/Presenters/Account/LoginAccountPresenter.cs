using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Ports.Account.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Account;

public class LoginAccountPresenter : ILoginAccountOutput, IPresenter<LoginAccountResponse>
{
    public Task HandleAsync(LoginAccountResponse response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public LoginAccountResponse Response { get; private set; }
}