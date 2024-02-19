using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Locks.Responses;

namespace CleanCodeTemplate.Business.Ports.Locks.Output;

public interface IGetBlockedOutput
{
    Task HandleAsync(PaginationResponse<GetBlockedResponse> response, CancellationToken ct);
}