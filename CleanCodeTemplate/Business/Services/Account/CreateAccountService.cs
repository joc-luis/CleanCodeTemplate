using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Constants;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Account;

public class CreateAccountService : ICreateAccountInput
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailTool _emailTool;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IValidazione _validazione;
    private readonly IRoleRepository _roleRepository;
    private readonly ICreateAccountOutput _output;


    public CreateAccountService(IAccountRepository accountRepository, IUserRepository userRepository,
        IEmailTool emailTool, ICryptographyTool cryptographyTool, IValidazione validazione,
        IRoleRepository roleRepository, ICreateAccountOutput output)
    {
        _accountRepository = accountRepository;
        _userRepository = userRepository;
        _emailTool = emailTool;
        _cryptographyTool = cryptographyTool;
        _validazione = validazione;
        _roleRepository = roleRepository;
        _output = output;
    }

    public async Task HandleAsync(CreateAccountRequest request, CancellationToken ct)
    {
        Role role = await _roleRepository.First<Role>(new Query().Where("Name", "Default"), ct);
      
        var query = new Query()
            .WhereRaw("(Users.Nick = ? or Users.Email = ?)", request.Nick, request.Email);
        var exist = await _userRepository.FirstOrDefaultAsync<User>(query, ct);

        if (exist!= null)
        {
            if (exist.Nick == request.Nick)
            {
                throw new BadRequestException("The nickname is already in use");
            }
            throw new BadRequestException("The email is already in use");
        }

        _validazione.Field("Nick", request.Nick).AlphaDash();
        _validazione.Field("Email", request.Email).Email();
        _validazione.PassOrException();

        byte[] image = request.Image ??
                       await File.ReadAllBytesAsync(Path.Combine(DirectoryConstants.Img, "account.png"), ct);
        string password = await _cryptographyTool.RandomStringAsync();
        
        User user = new User(role.Id, image, 
            request.Nick, await _cryptographyTool.HashAsync(password), 
            request.Email, 
            request.TwoFactors);

        await _accountRepository.RegisterAsync(user, ct);

        await _emailTool.SendAsync(request.Email, "User profile creation",
            $"Your account was successfully created, to access use your nickname and " +
            $"password {password}, we recommend changing the password",
            ct);


        await _output.HandleAsync("Your username was created correctly, check your email to obtain the password", ct);
    }
}