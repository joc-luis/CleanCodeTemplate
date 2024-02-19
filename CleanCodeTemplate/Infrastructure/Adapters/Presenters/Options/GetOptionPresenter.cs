using CleanCodeTemplate.Business.Dto.Options.Responses;
using CleanCodeTemplate.Business.Ports.Options.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Options;

public class GetOptionPresenter : IGetOptionOutput, IPresenter<IEnumerable<GetParentOptionResponse>>
{
    public Task HandleAsync(IEnumerable<GetParentOptionResponse> response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public IEnumerable<GetParentOptionResponse> Response { get; private set; }
}