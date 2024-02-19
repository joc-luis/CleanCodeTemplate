
namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface IUpdatePasswordAccountOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}