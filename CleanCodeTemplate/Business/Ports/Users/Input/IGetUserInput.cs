using CleanCodeTemplate.Business.Dto;

namespace CleanCodeTemplate.Business.Ports.Users.Input;

public interface IGetUserInput
{
    Task HandleAsync(SearchPaginationRequest request, CancellationToken ct);
}