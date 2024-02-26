using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Modules.Constants;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Database.Input;
using CleanCodeTemplate.Business.Ports.Database.Output;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Database;

public class InitializeDatabaseService : IInitializeDatabaseInput
{
    private readonly IOptionRepository _optionRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyTool _cryptographyTool;
    private readonly IInitializeDatabaseOutput _output;

    public InitializeDatabaseService(IOptionRepository optionRepository, IPermissionRepository permissionRepository,
        IRoleRepository roleRepository, IUserRepository userRepository, IInitializeDatabaseOutput output,
        ICryptographyTool cryptographyTool)
    {
        _optionRepository = optionRepository;
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _output = output;
        _cryptographyTool = cryptographyTool;
    }

    public async Task HandleAsync()
    {
        IEnumerable<User> users = await _userRepository.GetAsync<User>(default);
        if (!users.Any())
        {
            await CreateSettingsOptions();
            await CreateDefaultRoles();
            await CreateDefaultUser();
            await _output.HandleAsync("Options, role and default user created successfully.");
        }
        else
        {
            await _output.HandleAsync("The default options have already been created.");
        }
    }


    private async Task CreateSettingsOptions()
    {
        #region Settings

        Option settings = new Option("Settings", "settings", "cog");
        await _optionRepository.CreateAsync(settings, default);

        Option settingsProfile = new Option("Account", "account", "account", settings.Id);
        await _optionRepository.CreateAsync(settingsProfile, default);

        Option settingsRoles = new Option("Roles", "roles", "lock", settings.Id);
        await _optionRepository.CreateAsync(settingsRoles, default);
        await _permissionRepository.CreateAsync(new Permission(settingsRoles.Id, "Create role", "/role", "POST"),
            default);
        await _permissionRepository.CreateAsync(new Permission(settingsRoles.Id, "Update role", "/role", "PUT"),
            default);
        await _permissionRepository.CreateAsync(new Permission(settingsRoles.Id, "Get roles", "/role", "GET"), default);
        await _permissionRepository.CreateAsync(
            new Permission(settingsRoles.Id, "Delete role", $"/role/{PatternConstants.Guid}", "DELETE"), default);

        Option settingsUsers = new Option("Users", "users", "people", settings.Id);
        await _optionRepository.CreateAsync(settingsUsers, default);
        await _permissionRepository.CreateAsync(new Permission(settingsUsers.Id, "Create user", "/user", "POST"),
            default);
        await _permissionRepository.CreateAsync(new Permission(settingsUsers.Id, "Update user", "/user", "PUT"),
            default);
        await _permissionRepository.CreateAsync(new Permission(settingsUsers.Id, "Get users", "/user/search", "POST"), default);
        await _permissionRepository.CreateAsync(
            new Permission(settingsUsers.Id, "Delete user", $"/user/{PatternConstants.Guid}", "DELETE"), default);

        Option blockedSettings = new Option("Blocked users", "blocked/users", "locked", settings.Id);
        await _optionRepository.CreateAsync(blockedSettings, default);
        await _permissionRepository.CreateAsync(new Permission(blockedSettings.Id, "Block user", "/blocked", "POST"),
            default);
        await _permissionRepository.CreateAsync(
            new Permission(blockedSettings.Id, "Update user lock", "/blocked", "PUT"), default);
        await _permissionRepository.CreateAsync(
            new Permission(blockedSettings.Id, "Get blocked users", "/blocked/search", "POST"), default);
        await _permissionRepository.CreateAsync(
            new Permission(blockedSettings.Id, "Remove user lock", $"/blocked/{PatternConstants.Guid}", "DELETE"),
            default);

        #endregion

        #region Catalogs

        await _permissionRepository.CreateAsync(new Permission("Catalogue of permissions",
                "/catalogue/permissions",
                "GET"),
            default);
        
        await _permissionRepository.CreateAsync(new Permission("Catalogue of roles",
                "/catalogue/roles",
                "GET"),
            default);
        
        await _permissionRepository.CreateAsync(new Permission("Catalogue of users",
                "catalogue/users",
                "GET"),
            default);

        #endregion
    }

    private async Task CreateDefaultRoles()
    {
        IEnumerable<Guid> permissions = await _permissionRepository.GetAsync<Guid>(new Query().Select("Id"), default);
        Role role = new Role("Root", permissions);
        await _roleRepository.CreateAsync(role, default);
        await _roleRepository.CreateAsync(new Role("Default", new List<Guid>()), default);
    }

    private async Task CreateDefaultUser()
    {
        byte[] image = await File.ReadAllBytesAsync(Path.Combine(DirectoryConstants.Img, "account.png"));
        Role role = await _roleRepository.FirstAsync<Role>(new Query().Where("Name", "Root"), default);
        User user = new User(role.Id, image, "4dm1n", await _cryptographyTool.HashAsync("1234567890"),
            "root@example.com");
        await _userRepository.CreateAsync(user, default);
    }
}