using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Catalogs.Responses;
using CleanCodeTemplate.Business.Ports.Catalogs.Input;
using CleanCodeTemplate.Business.Ports.Catalogs.Output;

namespace CleanCodeTemplate.Business.Services.Catalogs;

public class GetRolesCatalogueService : IGetRolesCatalogueInput
{
    private readonly IRoleRepository _roleRepository;
    private readonly IGetCatalogueOutput _output;

    public GetRolesCatalogueService(IRoleRepository roleRepository, IGetCatalogueOutput output)
    {
        _roleRepository = roleRepository;
        _output = output;
    }

    public async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<GetCatalogueResponse> response = await _roleRepository.GetAsync<GetCatalogueResponse>(ct);

        await _output.HandleAsync(response, ct);
    }
}