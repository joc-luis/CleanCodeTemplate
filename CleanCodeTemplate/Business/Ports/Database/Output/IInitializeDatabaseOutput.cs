namespace CleanCodeTemplate.Business.Ports.Database.Output;

public interface IInitializeDatabaseOutput
{
    Task HandleAsync(string response);
}