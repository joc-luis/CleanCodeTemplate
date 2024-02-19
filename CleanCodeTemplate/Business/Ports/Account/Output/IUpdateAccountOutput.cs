namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface IUpdateAccountOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}