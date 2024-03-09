using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Locks.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Constants;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Locks;

public class UpdateBlockedService : IUpdateBlockedInput
{
    private readonly IBlockedRepository _blockedRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidazione _validazione;
    private readonly IBlockedCachingTool _blockedCachingTool;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IEmailTool _emailTool;
    private readonly IUpdateBlockedOutput _output;

    public UpdateBlockedService(IBlockedRepository blockedRepository, IUserRepository userRepository,
        IValidazione validazione, IBlockedCachingTool blockedCachingTool, IWebTokenTool webTokenTool,
        IEmailTool emailTool, IUpdateBlockedOutput output)
    {
        _blockedRepository = blockedRepository;
        _userRepository = userRepository;
        _validazione = validazione;
        _blockedCachingTool = blockedCachingTool;
        _webTokenTool = webTokenTool;
        _emailTool = emailTool;
        _output = output;
    }

    public async Task HandleAsync(UpdateBlockedRequest request, CancellationToken ct)
    {
        _validazione.Field("End", Convert.ToDateTime(request.End)).Min(DateTime.Now.AddDays(1).Date);
        _validazione.Field("Description", request.Description).Regex(PatternConstants.Text);
        _validazione.PassOrException();

        Blocked blocked = await _blockedRepository.FirstOrDefault<Blocked>(request.Id, ct) ??
                          throw new NotFoundException();

        if (blocked.UserId != _webTokenTool.SessionAccount.Id)
        {
            throw new ForbiddenException();
        }

        User user = await _userRepository.FirstOrDefaultAsync<User>(blocked.UserBlockedId, ct) ??
                    throw new NotFoundException();

        blocked.Description = request.Description;
        blocked.End = Convert.ToDateTime(request.End);

        if (await _blockedCachingTool.ExistsAsync(blocked.UserBlockedId.ToString(), ct))
        {
            await _blockedCachingTool.RemoveAsync(blocked.UserBlockedId.ToString(), ct);
        }

        var duration = (Convert.ToDateTime(request.End).Date - DateTime.Now.Date).TotalMinutes;

        await _blockedCachingTool.SetAsync(blocked.UserBlockedId.ToString(), "Blocked", TimeSpan.FromMinutes(duration),
            ct);

        await _blockedRepository.UpdateAsync(blocked, ct);

        await _emailTool.SendAsync(user.Email, "Account lockout Update",
            $"Your account block was updated, from {blocked.Start.Date} until {request.End}", ct);


        await _output.HandleAsync("Account lock updated successfully.", ct);
    }
}