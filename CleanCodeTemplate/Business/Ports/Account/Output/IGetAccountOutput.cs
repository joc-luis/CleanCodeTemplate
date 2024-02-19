using CleanCodeTemplate.Business.Dto.Account.Responses;

namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface IGetAccountOutput
{
    Task HandleAsync(GetAccountResponse response, CancellationToken ct);
}