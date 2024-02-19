using CleanCodeTemplate.Business.Dto.Account.Requests;

namespace CleanCodeTemplate.Business.Ports.Account.Input;

public interface IUpdateAccountInput
{
    Task HandleAsync(UpdateAccountRequest request, CancellationToken ct);
}