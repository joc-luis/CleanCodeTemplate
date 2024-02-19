using CleanCodeTemplate.Business.Dto.Users.Requests;

namespace CleanCodeTemplate.Business.Ports.Users.Input;

public interface IUpdateUserInput
{
    Task HandleAsync(UpdateUserRequest request, CancellationToken ct);
}