using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;

namespace CleanCodeTemplate.Business.Services.Account;

public class GetAccountService : IGetAccountInput
{
    private readonly IAccountRepository _accountRepository;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IGetAccountOutput _output;

    public GetAccountService(IAccountRepository accountRepository, IWebTokenTool webTokenTool, IGetAccountOutput output)
    {
        _accountRepository = accountRepository;
        _webTokenTool = webTokenTool;
        _output = output;
    }

    public async Task HandleAsync(CancellationToken ct)
    {
        GetAccountResponse response =
            await _accountRepository.First<GetAccountResponse>(_webTokenTool.SessionAccount.Id, ct);
        await _output.HandleAsync(response, ct);
    }
}