using CleanCodeTemplate.Business.Dto.Account.Requests;
using CleanCodeTemplate.Business.Ports.Account.Input;
using CleanCodeTemplate.Business.Ports.Account.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Account;

public class CreateAccountEndpoint : Endpoint<CreateAccountRequest, string>
{
    private readonly ICreateAccountInput _input;
    private readonly ICreateAccountOutput _output;

    public CreateAccountEndpoint(ICreateAccountInput input, ICreateAccountOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        AllowAnonymous();
        Post("/account/create");
    }

    public override async Task HandleAsync(CreateAccountRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);
        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}