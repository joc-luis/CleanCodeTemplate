using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Locks.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Constants;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Locks;

public class CreateBlockedService : ICreateBlockedInput
{
    private readonly IBlockedRepository _blockedRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidazione _validazione;
    private readonly IBlockedCachingTool _blockedCachingTool;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IEmailTool _emailTool;
    private readonly ICreateBlockedOutput _output;


    public CreateBlockedService(IBlockedRepository blockedRepository, IUserRepository userRepository,
        IValidazione validazione, IBlockedCachingTool blockedCachingTool, IWebTokenTool webTokenTool, IEmailTool emailTool,
        ICreateBlockedOutput output)
    {
        _blockedRepository = blockedRepository;
        _userRepository = userRepository;
        _validazione = validazione;
        _blockedCachingTool = blockedCachingTool;
        _webTokenTool = webTokenTool;
        _emailTool = emailTool;
        _output = output;
    }

    public async Task HandleAsync(CreateBlockedRequest request, CancellationToken ct)
    {
        var query = new Query()
            .Join("Roles", "Users.RoleId", "Roles.Id")
            .Where("Roles.Name", "<>", "Root")
            .Where("Users.Id", request.BlockedUserId);

        User user = await _userRepository.FirstOrDefaultAsync<User>(query, ct) ?? throw new ForbiddenException();

        _validazione.Field("End", request.End).Min(DateTime.Now.AddDays(1).Date);
        _validazione.Field("Description", request.Description).Nullable().Regex(PatternConstants.Text);
        _validazione.PassOrException();

        if (_webTokenTool.SessionAccount.Id == request.BlockedUserId)
        {
            throw new ForbiddenException();
        }

        Blocked blocked = new Blocked(_webTokenTool.SessionAccount.Id, request.BlockedUserId, request.Description,
            request.End.Date);

        await _blockedRepository.CreateAsync(blocked, ct);
        
        var duration = (request.End - DateTime.Now.Date).TotalMinutes;

        await _blockedCachingTool.SetAsync(request.BlockedUserId.ToString(), "blocked", TimeSpan.FromMinutes(duration), ct);

        await _emailTool.SendAsync(user.Email, "Your user has been blocked",
            $"A block was established on your account from {DateTime.Now.Date} to {request.End.Date}", ct);

        await _output.HandleAsync("Account blocked successfully", ct);
    }
}