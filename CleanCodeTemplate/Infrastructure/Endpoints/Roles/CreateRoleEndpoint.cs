using CleanCodeTemplate.Business.Dto.Roles.Requests;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Roles;

public class CreateRoleEndpoint : Endpoint<CreateRoleRequest, string>
{
    private readonly ICreateRoleInput _input;
    private readonly ICreateRoleOutput _output;

    public CreateRoleEndpoint(ICreateRoleInput input, ICreateRoleOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Post("/role");
    }

    public override async Task HandleAsync(CreateRoleRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}