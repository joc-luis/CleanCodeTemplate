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

public class CreateUserService : ICreateUserInput
{
    private readonly IUserRepository _userRepository;
    private readonly IValidazione _validazione;
    private readonly IRoleRepository _roleRepository;
    private readonly IEmailTool _emailTool;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly ICreateUserOutput _output;

    public CreateUserService(IUserRepository userRepository, IValidazione validazione, IRoleRepository roleRepository,
        IEmailTool emailTool, ICryptographyTool cryptographyTool, ICreateUserOutput output)
    {
        _userRepository = userRepository;
        _validazione = validazione;
        _roleRepository = roleRepository;
        _emailTool = emailTool;
        _cryptographyTool = cryptographyTool;
        _output = output;
    }

    public async Task HandleAsync(CreateUserRequest request, CancellationToken ct)
    {
        IEnumerable<Role> roles = await _roleRepository.GetAsync<Role>(ct);

        _validazione.Field("Nick", request.Nick).AlphaDash();
        _validazione.Field("Email", request.Email).Email();
        _validazione.Field("Role", request.RoleId.ToString()).In(roles.Select(r => r.Id.ToString()));
        _validazione.PassOrException();

        User? exist = await _userRepository
            .FirstOrDefaultAsync<User>(new Query()
                .Where("Nick", request.Nick)
                .OrWhere("Email", request.Email), ct);

        if (exist != null)
        {
            throw new BadRequestException("The email or the nickname has taken.");
        }

        string password = await _cryptographyTool.RandomStringAsync();

        IEnumerable<byte> image = request.Image ??
                                  await File.ReadAllBytesAsync(Path.Combine(DirectoryConstants.Img, "account.png"), ct);

        User user = new User(request.RoleId, image, request.Nick, await _cryptographyTool.HashAsync(password),
            request.Email, request.TwoFactors);

        await _userRepository.CreateAsync(user, ct);

        await _emailTool.SendAsync(user.Email, "User creation", "A user has been created for you, " +
                                                                $"the nickname is {request.Nick} and the password " +
                                                                $"is {password},we recommend you update the password",
            ct);

        await _output.HandleAsync("The user created successfully.", ct);
    }
}