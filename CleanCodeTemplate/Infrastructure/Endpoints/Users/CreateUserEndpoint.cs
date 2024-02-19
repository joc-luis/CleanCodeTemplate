using CleanCodeTemplate.Business.Dto.Users.Requests;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Ports.Users.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Users;

public class CreateUserEndpoint : Endpoint<CreateUserRequest, string>
{
    private readonly ICreateUserInput _input;
    private readonly ICreateUserOutput _output;

    public CreateUserEndpoint(ICreateUserInput input, ICreateUserOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Post("/user");
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}