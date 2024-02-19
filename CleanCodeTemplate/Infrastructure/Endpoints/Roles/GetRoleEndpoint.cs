using CleanCodeTemplate.Business.Dto.Roles.Responses;
using CleanCodeTemplate.Business.Ports.Roles.Input;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Roles;

public class GetRoleEndpoint : EndpointWithoutRequest<IEnumerable<GetRoleResponse>>
{
    private readonly IGetRoleInput _input;
    private readonly IGetRoleOutput _output;

    public GetRoleEndpoint(IGetRoleInput input, IGetRoleOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Get("/role");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _input.HandleAsync(ct);

        await SendAsync(((IPresenter<IEnumerable<GetRoleResponse>>)_output).Response, cancellation: ct);
    }
}