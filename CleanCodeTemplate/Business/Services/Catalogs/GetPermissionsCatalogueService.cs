using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Catalogs.Responses;
using CleanCodeTemplate.Business.Ports.Catalogs.Input;
using CleanCodeTemplate.Business.Ports.Catalogs.Output;

namespace CleanCodeTemplate.Business.Services.Catalogs;

public class GetPermissionsCatalogueService : IGetPermissionsCatalogueInput
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IGetCatalogueOutput _output;

    public GetPermissionsCatalogueService(IPermissionRepository permissionRepository, IGetCatalogueOutput output)
    {
        _permissionRepository = permissionRepository;
        _output = output;
    }

    public async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<GetCatalogueResponse> response = await _permissionRepository.GetAsync<GetCatalogueResponse>(ct);

        await _output.HandleAsync(response, ct);
    }
}