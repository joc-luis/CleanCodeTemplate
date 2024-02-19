namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface IChangePasswordAccountOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}