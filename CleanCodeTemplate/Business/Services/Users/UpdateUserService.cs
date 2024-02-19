using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Users.Requests;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Constants;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Ports.Users.Output;
using SqlKata;
using ValidaZione.Interfaces;

namespace CleanCodeTemplate.Business.Services.Users;

public class UpdateUserService : IUpdateUserInput
{
    private readonly IUserRepository _userRepository;
    private readonly IValidazione _validazione;
    private readonly IRoleRepository _roleRepository;
    private readonly IEmailTool _emailTool;
    private readonly IUpdateUserOutput _output;

    public UpdateUserService(IUserRepository userRepository, IValidazione validazione, IRoleRepository roleRepository,
        IEmailTool emailTool, IUpdateUserOutput output)
    {
        _userRepository = userRepository;
        _validazione = validazione;
        _roleRepository = roleRepository;
        _emailTool = emailTool;
        _output = output;
    }

    public async Task HandleAsync(UpdateUserRequest request, CancellationToken ct)
    {
        IEnumerable<Role> roles = await _roleRepository.GetAsync<Role>(ct);

        _validazione.Field("Nick", request.Nick).AlphaDash();
        _validazione.Field("Email", request.Email).Email();
        _validazione.Field("Role", request.RoleId.ToString()).In(roles.Select(r => r.Id.ToString()));
        _validazione.PassOrException();
        
        

        User? exist = await _userRepository
            .FirstOrDefaultAsync<User>(new Query()
                .Where("Id", "<>", request.Id)
                .WhereRaw("(Nick = ? or Email = ?)", request.Nick, request.Email), ct);

        if (exist != null)
        {
            throw new BadRequestException("The email or the nickname has taken.");
        }
        
        var query = new Query().Join("Roles", "RoleId", "Roles.Id")
            .Where("Users.Id", request.Id)
            .Where("Roles.Name", "Root");

        User? isRoot = await _userRepository.FirstOrDefaultAsync<User>(query, ct);


        if (isRoot != null)
        {
            throw new ForbiddenException();
        }

        User user = new User()
        {
            Id = request.Id,
            Nick = request.Nick,
            Email = request.Email,
            RoleId = request.RoleId,
            TwoFactors = request.TwoFactors,
            Image = request.Image ??
                    await File.ReadAllBytesAsync(Path.Combine(DirectoryConstants.Img, "account.png"), ct)
        };

        await _userRepository.UpdateAsync(user, ct);

        await _emailTool.SendAsync(user.Email, "Update profile", "Your profile was updated.", ct);

        await _output.HandleAsync("User was successfully updated", ct);
    }
}