using CleanCodeTemplate.Business.Dto.Account.Responses;

namespace CleanCodeTemplate.Business.Ports.Account.Output;

public interface IRecoverAccountOutput
{
    Task HandleAsync(RecoverAccountResponse response, CancellationToken ct);
}