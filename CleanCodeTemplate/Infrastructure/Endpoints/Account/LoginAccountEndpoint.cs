using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Dto.Account.Responses;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class LoginAccountEndpoint : Endpoint<LoginAccountRequest, LoginAccountResponse>
{
    private readonly ILoginAccountInput _input;
    private readonly ILoginAccountOutput _output;

    public LoginAccountEndpoint(ILoginAccountInput input, ILoginAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("/account/login");
    }

    public override async Task HandleAsync(LoginAccountRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<LoginAccountResponse>)_output).Response, cancellation: ct);
    }
}