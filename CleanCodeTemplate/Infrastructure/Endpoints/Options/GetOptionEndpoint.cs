using CleanCodeTemplate.Business.Dto.Options.Responses;
using CleanCodeTemplate.Business.Ports.Options.Input;
using CleanCodeTemplate.Business.Ports.Options.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Options;

public class GetOptionEndpoint: EndpointWithoutRequest<IEnumerable<GetParentOptionResponse>>
{
    private readonly IGetOptionInput _input;
    private readonly IGetOptionOutput _output;

    public GetOptionEndpoint(IGetOptionOutput output, IGetOptionInput input)
    {
        _output = output;
        _input = input;
    }

    public override void Configure()
    {
        Get("/option");
        ResponseCache(240);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _input.HandleAsync(ct);

        await SendAsync(((IPresenter<IEnumerable<GetParentOptionResponse>>)_output).Response, cancellation: ct);
    }
}