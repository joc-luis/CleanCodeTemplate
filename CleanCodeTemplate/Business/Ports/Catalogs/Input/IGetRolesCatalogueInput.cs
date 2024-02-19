namespace CleanCodeTemplate.Business.Ports.Catalogs.Input;

public interface IGetRolesCatalogueInput
{
    Task HandleAsync(CancellationToken ct);
}