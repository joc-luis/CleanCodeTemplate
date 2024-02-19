using CleanCodeTemplate.Business.Dto.Catalogs.Responses;
using CleanCodeTemplate.Business.Ports.Catalogs.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Catalogs;

public class GetCataloguePresenter : IGetCatalogueOutput, IPresenter<IEnumerable<GetCatalogueResponse>>
{
    public Task HandleAsync(IEnumerable<GetCatalogueResponse> response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public IEnumerable<GetCatalogueResponse> Response { get; private set; }
}