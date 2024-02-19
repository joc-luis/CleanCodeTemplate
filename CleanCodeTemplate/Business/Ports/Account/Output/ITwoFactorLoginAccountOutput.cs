namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface ITwoFactorLoginAccountOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}