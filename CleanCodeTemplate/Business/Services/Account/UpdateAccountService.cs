using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Account;

public class UpdateAccountService : IUpdateAccountInput
{
    private readonly IAccountRepository _accountRepository;
    private readonly IValidazione _validazione;
    private readonly IUserRepository _userRepository;
    private readonly IEmailTool _emailTool;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IUpdateAccountOutput _output;
    private readonly IWebTokenTool _webTokenTool;


    public UpdateAccountService(IAccountRepository accountRepository, IValidazione validazione,
        IUserRepository userRepository, IEmailTool emailTool, ICryptographyTool cryptographyTool,
        IUpdateAccountOutput output, IWebTokenTool webTokenTool)
    {
        _accountRepository = accountRepository;
        _validazione = validazione;
        _userRepository = userRepository;
        _emailTool = emailTool;
        _cryptographyTool = cryptographyTool;
        _output = output;
        _webTokenTool = webTokenTool;
    }

    public async Task HandleAsync(UpdateAccountRequest request, CancellationToken ct)
    {
        var query = new Query()
            .WhereRaw("Users.Id <> ? and (Users.Nick = ? or Users.Email = ?)",
                _webTokenTool.SessionAccount.Id,
                request.Nick,
                request.Email);
        
        var exist = await _userRepository.FirstOrDefaultAsync<User>(query, ct);

        if (exist != null)
        {
            if (exist.Nick == request.Nick)
            {
                throw new BadRequestException("The nickname is already in use");
            }

            throw new BadRequestException("The email is already in use");
        }

        _validazione.Field("Nick", request.Nick).AlphaDash();
        _validazione.Field("Email", request.Email).Email();
        _validazione.Field("Password", request.Password).Required();
        _validazione.PassOrException();

        User user = await _accountRepository.First<User>(_webTokenTool.SessionAccount.Id, ct);

        if (!await _cryptographyTool.VerifyHashAsync(user.Password, request.Password))
        {
            throw new BadRequestException("Password does not match");
        }

        user.Nick = request.Nick;
        user.Email = request.Email;
        user.Image = request.Image ?? user.Image;

        await _accountRepository.UpdateAsync(user, ct);

        await _emailTool.SendAsync(user.Email, "Profile update", "Your profile was successfully updated.", ct);

        await _output.HandleAsync("Your profile was successfully updated.", ct);
    }
}