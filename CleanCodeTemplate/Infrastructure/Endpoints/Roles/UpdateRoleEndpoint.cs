using CleanCodeTemplate.Business.Dto.Roles.Requests;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Roles;

public class UpdateRoleEndpoint : Endpoint<UpdateRoleRequest, string>
{
    private readonly IUpdateRoleInput _input;
    private readonly IUpdateRoleOutput _output;

    public UpdateRoleEndpoint(IUpdateRoleInput input, IUpdateRoleOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Put("/role");
    }

    public override async Task HandleAsync(UpdateRoleRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}