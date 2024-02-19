using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface IUpdatePasswordAccountInput
{
    Task HandleAsync(UpdatePasswordAccountRequest request, CancellationToken ct);
}