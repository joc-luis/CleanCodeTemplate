namespace CleanCodeTemplate.Business.Ports.Catalogs.Input;

public interface IGetPermissionsCatalogueInput
{
    Task HandleAsync(CancellationToken ct);
}