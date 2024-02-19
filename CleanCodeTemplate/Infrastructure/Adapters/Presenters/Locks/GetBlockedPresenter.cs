using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Locks.Responses;
using CleanCodeTemplate.Business.Ports.Locks.Output;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Locks;

public class GetBlockedPresenter : IGetBlockedOutput, IPresenter<PaginationResponse<GetBlockedResponse>>
{
    public Task HandleAsync(PaginationResponse<GetBlockedResponse> response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public PaginationResponse<GetBlockedResponse> Response { get; private set; }
}