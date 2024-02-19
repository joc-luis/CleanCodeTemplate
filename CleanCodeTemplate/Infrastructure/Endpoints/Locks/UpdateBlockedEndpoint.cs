using CleanCodeTemplate.Business.Dto.Locks.Requests;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Locks;

public class UpdateBlockedEndpoint : Endpoint<UpdateBlockedRequest, string>
{
    private readonly IUpdateBlockedInput _input;
    private readonly IUpdateBlockedOutput _output;

    public UpdateBlockedEndpoint(IUpdateBlockedInput input, IUpdateBlockedOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Put("/blocked");
    }

    public override async Task HandleAsync(UpdateBlockedRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}