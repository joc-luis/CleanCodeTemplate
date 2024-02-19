namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface ICreateAccountOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}