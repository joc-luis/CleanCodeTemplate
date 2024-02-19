namespace CleanCodeTemplate.Business.Ports.Locks.Output;

public interface IDestroyBlockedOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}