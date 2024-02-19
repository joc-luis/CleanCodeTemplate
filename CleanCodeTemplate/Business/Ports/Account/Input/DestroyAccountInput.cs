using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface IDestroyAccountInput
{
    Task HandleAsync(DestroyAccountRequest request, CancellationToken ct);
}