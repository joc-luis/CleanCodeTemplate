using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;

namespace CleanCodeTemplate.Business.Services.Account;

public class DestroyAccountService : IDestroyAccountInput
{
    private readonly IAccountRepository _accountRepository;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IEmailTool _emailTool;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IBlockedCachingTool _blockedCachingTool;
    private readonly IDestroyAccountOutput _output;

    public DestroyAccountService(IAccountRepository accountRepository, IEmailTool emailTool,
        IBlockedCachingTool blockedCachingTool, IDestroyAccountOutput output, IWebTokenTool webTokenTool,
        ICryptographyTool cryptographyTool)
    {
        _accountRepository = accountRepository;
        _emailTool = emailTool;
        _blockedCachingTool = blockedCachingTool;
        _output = output;
        _webTokenTool = webTokenTool;
        _cryptographyTool = cryptographyTool;
    }

    public async Task HandleAsync(DestroyAccountRequest request, CancellationToken ct)
    {
        User user = await _accountRepository.First<User>(_webTokenTool.SessionAccount.Id, ct);

        if (!await _cryptographyTool.VerifyHashAsync(user.Password, request.Password))
        {
            throw new BadRequestException("Password does not match");
        }

        await _accountRepository.DestroyAsync(user.Id, ct);

        await _blockedCachingTool.SetAsync(user.Id.ToString(), "removed", TimeSpan.FromDays(2), ct);

        await _emailTool.SendAsync(user.Email, "Account deletion", "Your account was successfully deleted.", ct);

        await _output.HandleAsync("Your account has been successfully deleted.", ct);
    }
}