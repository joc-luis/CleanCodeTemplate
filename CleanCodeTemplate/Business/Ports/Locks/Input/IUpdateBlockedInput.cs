using CleanCodeTemplate.Business.Dto.Locks.Requests;

namespace CleanCodeTemplate.Business.Ports.Locks.Input;

public interface IUpdateBlockedInput
{
    Task HandleAsync(UpdateBlockedRequest request, CancellationToken ct);
}