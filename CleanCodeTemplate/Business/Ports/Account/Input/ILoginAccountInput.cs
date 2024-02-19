using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface ILoginAccountInput
{
    Task HandleAsync(LoginAccountRequest request, CancellationToken ct);
}