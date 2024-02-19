using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Dto.Account;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using EasyCaching.Core;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Account;

public class RecoverAccountService : IRecoverAccountInput
{
    private readonly IValidazione _validazione;
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IEmailTool _emailTool;
    private readonly IMemoryCachingTool _cachingProvider;
    private readonly IRecoverAccountOutput _output;

    public RecoverAccountService(IValidazione validazione, IUserRepository userRepository,
        ICryptographyTool cryptographyTool, IEmailTool emailTool, IMemoryCachingTool cachingProvider,
        IRecoverAccountOutput output)
    {
        _validazione = validazione;
        _userRepository = userRepository;
        _cryptographyTool = cryptographyTool;
        _emailTool = emailTool;
        _cachingProvider = cachingProvider;
        _output = output;
    }

    public async Task HandleAsync(RecoverAccountRequest request, CancellationToken ct)
    {
        _validazione.Field("Email", request.Email).Email();
        _validazione.PassOrException();

        User? user = await _userRepository.FirstOrDefaultAsync<User>(new Query().Where("Email", request.Email), ct);
        string key = await _cryptographyTool.RandomStringAsync(20);

        if (user != null)
        {
            string code = await _cryptographyTool.RandomStringAsync();

            RecoverAccountDto recover = new RecoverAccountDto()
            {
                Code = code,
                Id = user.Id
            };

            await _cachingProvider.SetAsync(key, recover, TimeSpan.FromMinutes(30), ct);

            await _emailTool.SendAsync(user.Email,
                "Change of password",
                $"The code {code} (Do not share it with anyone) is " +
                "to change your password, if you are not requesting a password " +
                "change, ignore this email.", ct);
        }

        RecoverAccountResponse response = new RecoverAccountResponse()
        {
            Message = "If the email matches our records, we will send you a code to change your password.",
            Key = key
        };
        
        await _output.HandleAsync(response, ct);
    }
}