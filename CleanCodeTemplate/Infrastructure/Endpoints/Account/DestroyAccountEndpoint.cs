using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class DestroyAccountEndpoint : Endpoint<DestroyAccountRequest, string>
{
    private readonly IDestroyAccountInput _input;
    private readonly IDestroyAccountOutput _output;

    public DestroyAccountEndpoint(IDestroyAccountInput input, IDestroyAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Delete("/account");
    }

    public override async Task HandleAsync(DestroyAccountRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}