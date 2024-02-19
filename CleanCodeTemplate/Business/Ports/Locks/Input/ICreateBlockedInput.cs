using CleanCodeTemplate.Business.Dto.Locks.Requests;

namespace CleanCodeTemplate.Business.Ports.Locks.Input;

public interface ICreateBlockedInput
{
    Task HandleAsync(CreateBlockedRequest request, CancellationToken ct);
}