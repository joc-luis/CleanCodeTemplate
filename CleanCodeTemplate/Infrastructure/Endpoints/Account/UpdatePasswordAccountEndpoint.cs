using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class UpdatePasswordAccountEndpoint : Endpoint<UpdatePasswordAccountRequest, string>
{
    private readonly IUpdatePasswordAccountInput _input;
    private readonly IUpdatePasswordAccountOutput _output;

    public UpdatePasswordAccountEndpoint(IUpdatePasswordAccountInput input, IUpdatePasswordAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Put("/account/update/password");
    }

    public override async Task HandleAsync(UpdatePasswordAccountRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);
        
        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}