using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface IChangePasswordAccountInput
{
    Task HandleAsync(ChangePasswordAccountRequest request, CancellationToken ct);
}