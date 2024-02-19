using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class TwoFactorLoginEndpoint : Endpoint<TwoFactorLoginRequest, string>
{
    private readonly ITwoFactorLoginAccountInput _input;
    private readonly ITwoFactorLoginAccountOutput _output;

    public TwoFactorLoginEndpoint(ITwoFactorLoginAccountInput input, ITwoFactorLoginAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("/account/login/code");
    }

    public override async Task HandleAsync(TwoFactorLoginRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);
        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}