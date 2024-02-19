using CleanCodeTemplate.Business.Dto.Users.Requests;

namespace CleanCodeTemplate.Business.Ports.Users.Input;

public interface ICreateUserInput
{
    Task HandleAsync(CreateUserRequest request, CancellationToken ct);
}