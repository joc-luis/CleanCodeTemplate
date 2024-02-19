using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using EasyCaching.Core;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Account;

public class ChangePasswordAccountService : IChangePasswordAccountInput
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMemoryCachingTool _cachingProvider;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IValidazione _validazione;
    private readonly IChangePasswordAccountOutput _output;

    public ChangePasswordAccountService(IUserRepository userRepository, IMemoryCachingTool cachingProvider,
        ICryptographyTool cryptographyTool, IValidazione validazione, IChangePasswordAccountOutput output,
        IAccountRepository accountRepository)
    {
        _userRepository = userRepository;
        _cachingProvider = cachingProvider;
        _cryptographyTool = cryptographyTool;
        _validazione = validazione;
        _output = output;
        _accountRepository = accountRepository;
    }

    public async Task HandleAsync(ChangePasswordAccountRequest request, CancellationToken ct)
    {
        _validazione.Field("Code", request.Code).Required();
        _validazione.Field("Key", request.Key).Required();
        _validazione.Field("Password", request.NewPassword).Between(10, 20);
        _validazione.PassOrException();

        if (await _cachingProvider.ExistsAsync(request.Key, ct))
        {
            var recoverAccount = await _cachingProvider.GetAsync<RecoverAccountDto>(request.Key, ct);

            if (recoverAccount.Code != request.Code)
            {
                throw new NotFoundException("The code does not exist.");
            }

            User? user = await _userRepository.FirstOrDefaultAsync<User>(recoverAccount.Id, ct);

            if (user == null)
            {
                throw new NotFoundException("The code does not exist.");
            }

            user.Password = await _cryptographyTool.HashAsync(request.NewPassword);

            await _accountRepository.UpdatePasswordAsync(user, ct);

            await _output.HandleAsync("The password was changed successfully.", ct);
        }
        else
        {
            throw new NotFoundException("The code does not exist.");
        }
    }
}