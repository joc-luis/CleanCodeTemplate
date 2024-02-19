namespace CleanCodeTemplate.Business.Ports.Locks.Output;

public interface IUpdateBlockedOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}