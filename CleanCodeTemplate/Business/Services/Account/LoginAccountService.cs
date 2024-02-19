using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Dto.Permissions;
using CleanCodeTemplate.Business.Dto.Account;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using EasyCaching.Core;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Account;

public class LoginAccountService : ILoginAccountInput
{
    private readonly IUserRepository _userRepository;
    private readonly IValidazione _validazione;
    private readonly IMemoryCachingTool _cachingProvider;
    private readonly IWebTokenTool _webTokenTool;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IEmailTool _emailTool;
    private readonly ILoginAccountOutput _output;

    public LoginAccountService(IUserRepository userRepository, IValidazione validazione,
        IMemoryCachingTool cachingProvider, IWebTokenTool webTokenTool, ICryptographyTool cryptographyTool,
        IRoleRepository roleRepository, IPermissionRepository permissionRepository, ILoginAccountOutput output,
        IEmailTool emailTool)
    {
        _userRepository = userRepository;
        _validazione = validazione;
        _cachingProvider = cachingProvider;
        _webTokenTool = webTokenTool;
        _cryptographyTool = cryptographyTool;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _output = output;
        _emailTool = emailTool;
    }

    public async Task HandleAsync(LoginAccountRequest request, CancellationToken ct)
    {
        _validazione.Field("Nick", request.Nick).AlphaDash();
        _validazione.Field("Password", request.Password).Required();
        _validazione.PassOrException();

        User? user = await _userRepository.FirstOrDefaultAsync<User>(new Query().Where("Nick", request.Nick), ct);
        if (user == null || !await _cryptographyTool.VerifyHashAsync(user.Password, request.Password))
        {
            throw new NotFoundException("The credentials do not exist in our records.");
        }

        LoginAccountResponse response;

        if (user.TwoFactors)
        {
            string key = await _cryptographyTool.RandomStringAsync(20);
            response = new LoginAccountResponse()
            {
                IsTwoFactors = true,
                Token = key
            };

            TwoFactorsDto twoFactors = new TwoFactorsDto()
            {
                Id = user.Id,
                Code = await _cryptographyTool.RandomStringAsync()
            };

            await _cachingProvider.SetAsync(key, twoFactors, TimeSpan.FromMinutes(5), ct);

            await _emailTool.SendAsync(user.Email, "Access token", $"Your access token is {twoFactors.Code} " +
                                                                   "(do not share this code with anyone), " +
                                                                   "if you are not trying to log in we recommend " +
                                                                   "changing your password", ct);
        }
        else
        {
            Role role = await _roleRepository.FirstAsync<Role>(user.RoleId, ct);

            IEnumerable<PermissionDto> permissions = await _permissionRepository
                .GetAsync<PermissionDto>(new Query().WhereIn("Id", role.Permissions), ct);

            string token = await _webTokenTool.GenerateTokenAsync(new SessionAccountDto()
            {
                Id = user.Id,
                Permissions = permissions
            }, 1, ct);

            response = new LoginAccountResponse()
            {
                IsTwoFactors = false,
                Token = token
            };
        }

        await _output.HandleAsync(response, ct);
    }
}