using CleanCodeTemplate.Business.Dto.Account.Responses;

namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface ILoginAccountOutput
{
    Task HandleAsync(LoginAccountResponse response, CancellationToken ct);
}