namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface IGetAccountInput
{
    Task HandleAsync(CancellationToken ct);
}