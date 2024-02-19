using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;

namespace CleanCodeTemplate.Business.Services.Locks;

public class DestroyBlockedService : IDestroyBlockedInput
{
    private readonly IBlockedRepository _blockedRepository;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IBlockedCachingTool _blockedCachingTool;
    private readonly IEmailTool _emailTool;
    private readonly IUserRepository _userRepository;
    private readonly IDestroyBlockedOutput _output;

    public DestroyBlockedService(IBlockedRepository blockedRepository, IWebTokenTool webTokenTool, IBlockedCachingTool blockedCachingTool, IEmailTool emailTool, IDestroyBlockedOutput output, IUserRepository userRepository)
    {
        _blockedRepository = blockedRepository;
        _webTokenTool = webTokenTool;
        _blockedCachingTool = blockedCachingTool;
        _emailTool = emailTool;
        _output = output;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(Guid id, CancellationToken ct)
    {
        Blocked blocked = await _blockedRepository.FirstOrDefault<Blocked>(id, ct) ?? throw new NotFoundException();
        User user = await _userRepository.FirstOrDefaultAsync<User>(blocked.UserBlockedId, ct) ??
                    throw new NotFoundException();
        
        if (blocked.UserId != _webTokenTool.SessionAccount.Id)
        {
            throw new ForbiddenException();
        }

        await _blockedRepository.DestroyAsync(id, ct);

        await _blockedCachingTool.RemoveAsync(blocked.UserBlockedId.ToString(), ct);

        await _emailTool.SendAsync(user.Email, "Lock removed", "Your account is no longer blocked.", ct);

        await _output.HandleAsync("The account was successfully unlocked.", ct);
    }
}