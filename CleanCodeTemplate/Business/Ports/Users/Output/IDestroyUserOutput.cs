namespace CleanCodeTemplate.Business.Ports.Users.Output;

public interface IDestroyUserOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}