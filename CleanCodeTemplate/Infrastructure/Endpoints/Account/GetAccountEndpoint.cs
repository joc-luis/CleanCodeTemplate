using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class GetAccountEndpoint : EndpointWithoutRequest<GetAccountResponse>
{
    private readonly IGetAccountInput _input;
    private readonly IGetAccountOutput _output;

    public GetAccountEndpoint(IGetAccountInput input, IGetAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Get("account");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _input.HandleAsync(ct);
        await SendAsync(((IPresenter<GetAccountResponse>)_output).Response, cancellation: ct);
    }
}