namespace CleanCodeTemplate.Business.Ports.Roles.Input;

public interface IDestroyRoleInput
{
    Task HandleAsync(Guid id, CancellationToken ct);
}