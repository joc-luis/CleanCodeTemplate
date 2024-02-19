using CleanCodeTemplate.Business.Dto.Locks.Requests;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Locks;

public class CreateBlockedEndpoint : Endpoint<CreateBlockedRequest, string>
{
    private readonly ICreateBlockedInput _input;
    private readonly ICreateBlockedOutput _output;

    public CreateBlockedEndpoint(ICreateBlockedInput input, ICreateBlockedOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Post("/blocked");
    }

    public override async Task HandleAsync(CreateBlockedRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<string>)_output).Response, cancellation: ct);
    }
}