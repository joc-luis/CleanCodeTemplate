using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Account;
using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Dto.Permissions;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using EasyCaching.Core;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Account;

public class TwoFactorLoginAccountService : ITwoFactorLoginAccountInput
{
    private readonly IUserRepository _userRepository;
    private readonly IValidazione _validazione;
    private readonly IMemoryCachingTool _cachingProvider;
    private readonly IRoleRepository _roleRepository;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ITwoFactorLoginAccountOutput _output;

    public TwoFactorLoginAccountService(IUserRepository userRepository, IMemoryCachingTool cachingProvider,
        IRoleRepository roleRepository, IWebTokenTool webTokenTool, IPermissionRepository permissionRepository,
        ITwoFactorLoginAccountOutput output, IValidazione validazione)
    {
        _userRepository = userRepository;
        _cachingProvider = cachingProvider;
        _roleRepository = roleRepository;
        _webTokenTool = webTokenTool;
        _permissionRepository = permissionRepository;
        _output = output;
        _validazione = validazione;
    }

    public async Task HandleAsync(TwoFactorLoginRequest request, CancellationToken ct)
    {
        _validazione.Field("Key", request.Key).Required();
        _validazione.Field("Code", request.Code).Required();
        _validazione.PassOrException();

        if (await _cachingProvider.ExistsAsync(request.Key, ct))
        {
            var twoFactors = await _cachingProvider.GetAsync<TwoFactorsDto>(request.Key, ct);

            if (twoFactors.Code == request.Code)
            {
                await _cachingProvider.RemoveAsync(request.Key, ct);
                
                User? user = await _userRepository.FirstOrDefaultAsync<User>(twoFactors.Id, ct);

                if (user == null)
                {
                    throw new NotFoundException("The code does not exist.");
                }

                Role role = await _roleRepository.FirstAsync<Role>(user.RoleId, ct);

                IEnumerable<PermissionDto> permissions = await _permissionRepository
                    .GetAsync<PermissionDto>(new Query()
                        .WhereIn("Id", role.Permissions), ct);

                string response = await _webTokenTool.GenerateTokenAsync(new SessionAccountDto()
                {
                    Id = user.Id,
                    Permissions = permissions
                }, 1, ct);

                
                await _output.HandleAsync(response, ct);
            }
            else
            {
                throw new NotFoundException("The code does not exist.");
            }
        }
        else
        {
            throw new NotFoundException("The code does not exist.");
        }
    }
}