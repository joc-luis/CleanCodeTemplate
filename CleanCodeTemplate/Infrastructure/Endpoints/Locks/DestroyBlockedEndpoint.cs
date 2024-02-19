using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Locks;

public class DestroyBlockedEndpoint : EndpointWithoutRequest<string>
{
    private readonly IDestroyBlockedInput _input;
    private readonly IDestroyBlockedOutput _output;

    public DestroyBlockedEndpoint(IDestroyBlockedInput input, IDestroyBlockedOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Delete("/blocked/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _input.HandleAsync(Route<Guid>("id"), ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}