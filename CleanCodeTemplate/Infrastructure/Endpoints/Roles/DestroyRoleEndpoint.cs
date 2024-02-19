using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Roles;

public class DestroyRoleEndpoint : EndpointWithoutRequest<string>
{
    private readonly IDestroyRoleInput _input;
    private readonly IDestroyRoleOutput _output;

    public DestroyRoleEndpoint(IDestroyRoleInput input, IDestroyRoleOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Delete("role/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _input.HandleAsync(Route<Guid>("id"), ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}