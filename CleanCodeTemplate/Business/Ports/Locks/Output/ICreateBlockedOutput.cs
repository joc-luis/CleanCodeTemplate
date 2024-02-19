namespace CleanCodeTemplate.Business.Ports.Locks.Output;

public interface ICreateBlockedOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}