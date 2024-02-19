using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Roles.Responses;
using CleanCodeTemplate.Business.Dto.Users.Responses;
using CleanCodeTemplate.Business.Ports.Roles.Output;
using CleanCodeTemplate.Business.Ports.Users.Output;
using SqlKata.Execution;

namespace CleanCodeTemplate.Infrastructure.Adapters.Presenters.Users;

public class GetUserPresenter : IGetUserOutput, IPresenter<PaginationResponse<GetUserResponse>>
{
    public Task HandleAsync(PaginationResponse<GetUserResponse> response, CancellationToken ct)
    {
        Response = response;
        
        return Task.CompletedTask;
    }

    public PaginationResponse<GetUserResponse> Response { get; private set; }
}