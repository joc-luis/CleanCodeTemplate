using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class ChangePasswordAccountEndpoint : Endpoint<ChangePasswordAccountRequest, string>
{
    private readonly IChangePasswordAccountInput _input;
    private readonly IChangePasswordAccountOutput _output;

    public ChangePasswordAccountEndpoint(IChangePasswordAccountInput input, IChangePasswordAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Put("/account/change/password");
    }

    public override async Task HandleAsync(ChangePasswordAccountRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);
        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}