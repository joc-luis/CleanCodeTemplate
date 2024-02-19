using System.Collections;
using CleanCodeTemplate.Business.Dto.Catalogs.Responses;
using CleanCodeTemplate.Business.Ports.Catalogs.Input;
using CleanCodeTemplate.Business.Ports.Catalogs.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Catalogs;

public class GetUsersCatalogueEndpoint : EndpointWithoutRequest<IEnumerable<GetCatalogueResponse>>
{
    private readonly IGetUsersCatalogueInput _input;
    private readonly IGetCatalogueOutput _output;

    public GetUsersCatalogueEndpoint(IGetUsersCatalogueInput input, IGetCatalogueOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Get("/catalogue/users");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _input.HandleAsync(ct);

        await SendAsync(((IPresenter<IEnumerable<GetCatalogueResponse>>)_output).Response, cancellation: ct);
    }
}