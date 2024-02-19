namespace CleanCodeTemplate.Business.Ports.Roles.Output;

public interface ICreateRoleOutput
{
    Task HandleAsync(string response, CancellationToken ct);
}