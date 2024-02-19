using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Users.Responses;
using SqlKata.Execution;

namespace CleanCodeTemplate.Business.Ports.Users.Output;

public interface IGetUserOutput
{
    Task HandleAsync(PaginationResponse<GetUserResponse> response, CancellationToken ct);
}