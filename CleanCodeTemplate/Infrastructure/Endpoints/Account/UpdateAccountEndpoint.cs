using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class UpdateAccountEndpoint : Endpoint<UpdateAccountRequest, string>
{
    private readonly IUpdateAccountInput _input;
    private readonly IUpdateAccountOutput _output;

    public UpdateAccountEndpoint(IUpdateAccountInput input, IUpdateAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Put("/account");
    }

    public override async Task HandleAsync(UpdateAccountRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}