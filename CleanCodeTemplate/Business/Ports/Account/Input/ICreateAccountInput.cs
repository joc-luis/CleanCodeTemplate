using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface ICreateAccountInput
{
    Task HandleAsync(CreateAccountRequest request, CancellationToken ct);
}