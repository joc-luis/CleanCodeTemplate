namespace CleanCodeTemplate.Business.Ports.Options.Input;

public interface IGetOptionInput
{
    Task HandleAsync(CancellationToken ct);
}