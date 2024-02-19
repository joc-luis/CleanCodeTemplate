using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Locks.Responses;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Locks;

public class GetBlockedEndpoint : Endpoint<SearchPaginationRequest, PaginationResponse<GetBlockedResponse>>
{
    private readonly IGetBlockedInput _input;
    private readonly IGetBlockedOutput _output;

    public GetBlockedEndpoint(IGetBlockedInput input, IGetBlockedOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Post("/blocked/search");
    }

    public override async Task HandleAsync(SearchPaginationRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<PaginationResponse<GetBlockedResponse>>)_output).Response, cancellation: ct);
    }
}