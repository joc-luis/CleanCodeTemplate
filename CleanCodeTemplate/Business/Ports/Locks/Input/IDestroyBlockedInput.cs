namespace CleanCodeTemplate.Business.Ports.Locks.Input;

public interface IDestroyBlockedInput
{
    Task HandleAsync(Guid id, CancellationToken ct);
}