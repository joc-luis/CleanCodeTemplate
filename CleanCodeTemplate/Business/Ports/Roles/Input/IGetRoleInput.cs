namespace CleanCodeTemplate.Business.Ports.Roles.Input;

public interface IGetRoleInput
{
    Task HandleAsync(CancellationToken ct);
}