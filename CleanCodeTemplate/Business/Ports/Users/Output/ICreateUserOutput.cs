namespace CleanCodeTemplate.Business.Ports.Users.Output;

public interface ICreateUserOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}