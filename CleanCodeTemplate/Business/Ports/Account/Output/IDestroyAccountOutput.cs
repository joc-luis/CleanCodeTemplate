namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface IDestroyAccountOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}