using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Ports.Users.Output;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Users;

public class DestroyUserService : IDestroyUserInput
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailTool _emailTool;
    private readonly IBlockedCachingTool _blockedCachingTool;
    private readonly IDestroyUserOutput _output;

    public DestroyUserService(IUserRepository userRepository, IEmailTool emailTool, IDestroyUserOutput output,
        IBlockedCachingTool blockedCachingTool)
    {
        _userRepository = userRepository;
        _emailTool = emailTool;
        _output = output;
        _blockedCachingTool = blockedCachingTool;
    }

    public async Task HandleAsync(Guid id, CancellationToken ct)
    {
        var query = new Query().Join("Roles", "RoleId", "Roles.Id")
            .Where("Users.Id", id)
            .Where("Roles.Name", "Root");

        User? isRoot = await _userRepository.FirstOrDefaultAsync<User>(query, ct);


        if (isRoot != null)
        {
            throw new ForbiddenException();
        }

        User user = await _userRepository.FirstOrDefaultAsync<User>(id, ct) ?? throw new NotFoundException();
        
        await _userRepository.DestroyAsync(user.Id, ct);

        await _blockedCachingTool.RemoveAsync(user.Id.ToString(), ct);

        await _blockedCachingTool.SetAsync(user.Id.ToString(), "removed", TimeSpan.FromDays(2), ct);
        
        await _emailTool.SendAsync(user.Email, "Account deletion", "Your account has been deleted.", ct);
        
        await _output.HandleAsync("User was successfully deleted.", ct);
    }
}