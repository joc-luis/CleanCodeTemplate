namespace CleanCodeTemplate.Business.Ports.Catalogs.Input;

public interface IGetUsersCatalogueInput
{
    Task HandleAsync(CancellationToken ct);
}