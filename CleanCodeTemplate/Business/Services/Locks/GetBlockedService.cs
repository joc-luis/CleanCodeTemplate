using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Locks.Responses;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Locks;

public class GetBlockedService : IGetBlockedInput
{
    private readonly IBlockedRepository _blockedRepository;
    private readonly IGetBlockedOutput _output;

    public GetBlockedService(IBlockedRepository blockedRepository, IGetBlockedOutput output)
    {
        _blockedRepository = blockedRepository;
        _output = output;
    }

    public async Task HandleAsync(SearchPaginationRequest request, CancellationToken ct)
    {
        var query = new Query().Join("Users as UsersBlock", "Blocked.UserBlockedId", "UsersBlock.Id")
        .Join("Users", "Blocked.UserId", "Users.Id")
        .Select("Blocked.*", "UsersBlock.Nick as UserBlocked", "Users.Nick as User");

        foreach (var value in request.Value.Split(" "))
        {
            query.OrWhereRaw("Users.Nick like ?", $"%{value}%");
            query.OrWhereRaw("Users.Email like ?", $"%{value}%");
            query.OrWhereRaw("UsersBlock.Nick like ?", $"%{value}%");
            query.OrWhereRaw("UsersBlock.Email like ?", $"%{value}%");
        }

        var response = await _blockedRepository.GetPaginationAsync<GetBlockedResponse>(query, request.Page, request.Take, ct);

        await _output.HandleAsync(response, ct);
    }
}