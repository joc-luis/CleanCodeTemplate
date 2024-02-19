using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class RecoverAccountEndpoint : Endpoint<RecoverAccountRequest, RecoverAccountResponse>
{
    private readonly IRecoverAccountInput _input;
    private readonly IRecoverAccountOutput _output;

    public RecoverAccountEndpoint(IRecoverAccountInput input, IRecoverAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("/account/recover");
    }

    public override async Task HandleAsync(RecoverAccountRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<RecoverAccountResponse>)_output).Response, cancellation: ct);
    }
}