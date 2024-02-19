namespace CleanCodeTemplate.Business.Ports.Users.Output;

public interface IUpdateUserOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}