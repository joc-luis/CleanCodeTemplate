using CleanCodeTemplate.Business.Dto;

namespace CleanCodeTemplate.Business.Ports.Locks.Input;

public interface IGetBlockedInput
{
    Task HandleAsync(SearchPaginationRequest request, CancellationToken ct);
}