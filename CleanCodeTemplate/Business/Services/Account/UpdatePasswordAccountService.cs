using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Account;

public class UpdatePasswordAccountService : IUpdatePasswordAccountInput
{
    private readonly IAccountRepository _accountRepository;
    private readonly IValidazione _validazione;
    private readonly IWebTokenTool _webTokenTool;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IUpdatePasswordAccountOutput _output;


    public UpdatePasswordAccountService(IAccountRepository accountRepository, ICryptographyTool cryptographyTool,
        IUpdatePasswordAccountOutput output, IValidazione validazione, IWebTokenTool webTokenTool)
    {
        _accountRepository = accountRepository;
        _cryptographyTool = cryptographyTool;
        _output = output;
        _validazione = validazione;
        _webTokenTool = webTokenTool;
    }

    public async Task HandleAsync(UpdatePasswordAccountRequest request, CancellationToken ct)
    {
        _validazione.Field("Current password", request.CurrentPassword).Required();
        _validazione.Field("New password", request.NewPassword).Required()
            .Different("Current password", request.CurrentPassword);
        _validazione.PassOrException();

        User user = await _accountRepository.First<User>(_webTokenTool.SessionAccount.Id, ct);

        if (!await _cryptographyTool.VerifyHashAsync(user.Password, request.CurrentPassword))
        {
            throw new BadRequestException("Password does not match");
        }

        user.Password = await _cryptographyTool.HashAsync(request.NewPassword);

        await _accountRepository.UpdatePasswordAsync(user, ct);

        await _output.HandleAsync("The password was successfully updated.", ct);
    }
}