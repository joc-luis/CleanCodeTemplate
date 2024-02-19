namespace CleanCodeTemplate.Business.Ports.Roles.Output;

public interface IDestroyRoleOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}