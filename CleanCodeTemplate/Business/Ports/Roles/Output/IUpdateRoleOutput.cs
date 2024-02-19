namespace CleanCodeTemplate.Business.Ports.Roles.Output;

public interface IUpdateRoleOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}