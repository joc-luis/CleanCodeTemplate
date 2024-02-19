using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface IRecoverAccountInput
{
    Task HandleAsync(RecoverAccountRequest request, CancellationToken ct);
}