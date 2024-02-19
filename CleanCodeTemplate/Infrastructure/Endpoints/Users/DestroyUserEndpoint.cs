using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Ports.Users.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Users;

public class DestroyUserEndpoint : EndpointWithoutRequest<string>
{
    private readonly IDestroyUserInput _input;
    private readonly IDestroyUserOutput _output;

    public DestroyUserEndpoint(IDestroyUserInput input, IDestroyUserOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Delete("/user/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _input.HandleAsync(Route<Guid>("id"), ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}