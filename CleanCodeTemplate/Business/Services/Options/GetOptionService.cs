using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Options.Responses;
using CleanCodeTemplate.Business.Modules.Tools;
using CleanCodeTemplate.Business.Ports.Options.Input;
using CleanCodeTemplate.Business.Ports.Options.Output;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Options;

public class GetOptionService : IGetOptionInput
{
    private readonly IOptionRepository _optionRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IWebTokenTool _webTokenTool;
    private readonly IGetOptionOutput _output;

    public GetOptionService(IOptionRepository optionRepository, IRoleRepository roleRepository,
        IPermissionRepository permissionRepository, IWebTokenTool webTokenTool, IGetOptionOutput output)
    {
        _optionRepository = optionRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _webTokenTool = webTokenTool;
        _output = output;
    }

    public async Task HandleAsync(CancellationToken ct)
    {
        List<GetParentOptionResponse> response = new List<GetParentOptionResponse>();

        var parents = await _optionRepository
            .GetAsync<Option>(new Query().WhereNull("ParentId"), ct);

        var query = new Query().Join("Users", "Roles.Id", "RoleId")
            .Where("Users.Id", _webTokenTool.SessionAccount.Id);

        Role role = await _roleRepository.FirstAsync<Role>(query, ct);

        var joinQuery = new Query().Join("Permissions", "Options.Id", "OptionId")
            .WhereIn("Permissions.Id", role.Permissions)
            .GroupBy("Options.Id", "Options.Name", "Options.Icon", "Options.Url", "Options.ParentId")
            .Select("Options.*");

        var children = await _optionRepository.GetAsync<Option>(joinQuery, ct);

        var options = await _permissionRepository
            .GetAsync<Guid>(new Query().WhereNotNull("OptionId").Select("OptionId"), ct);

        var defaultChildren = await _optionRepository
            .GetAsync<Option>(new Query().WhereNotIn("Id", options), ct);

        foreach (Option parent in parents)
        {
            List<GetChildOptionResponse> parentChildren = children.Where(c => c.ParentId == parent.Id)
                .Select(c => new GetChildOptionResponse()
                {
                    Icon = c.Icon,
                    Name = c.Name,
                    Url = $"{parent.Url}/{c.Url}"
                }).ToList();

            parentChildren.AddRange(defaultChildren.Where(d => d.ParentId == parent.Id)
                .Select(d => new GetChildOptionResponse()
                {
                    Icon = d.Icon,
                    Name = d.Name,
                    Url = $"{parent.Url}/{d.Url}"
                }));


            response.Add(new GetParentOptionResponse()
            {
                Name = parent.Name,
                Icon = parent.Icon,
                Children = parentChildren
            });
        }

        await _output.HandleAsync(response, ct);
    }
}