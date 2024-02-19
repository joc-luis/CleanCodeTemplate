using CleanCodeTemplate.Business.Dto.Catalogs.Responses;

namespace CleanCodeTemplate.Business.Ports.Catalogs.Output;

public interface IGetCatalogueOutput
{
    Task HandleAsync(IEnumerable<GetCatalogueResponse> response, CancellationToken ct);
}