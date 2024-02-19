namespace CleanCodeTemplate.Business.Ports.Users.Input;

public interface IDestroyUserInput
{
    Task HandleAsync(Guid id, CancellationToken ct);
}