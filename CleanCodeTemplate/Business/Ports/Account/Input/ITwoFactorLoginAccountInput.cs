using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface ITwoFactorLoginAccountInput
{
    Task HandleAsync(TwoFactorLoginRequest request, CancellationToken ct);
}