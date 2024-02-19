using CleanCodeTemplate.Business.Dto.Users.Requests;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Ports.Users.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Users;

public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, string>
{
    private readonly IUpdateUserInput _input;
    private readonly IUpdateUserOutput _output;

    public UpdateUserEndpoint(IUpdateUserInput input, IUpdateUserOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Put("/user");
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}