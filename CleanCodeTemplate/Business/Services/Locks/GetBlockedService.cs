using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Locks.Responses;
using CleanCodeTemplate.Business.Ports.Locks.Input;
using CleanCodeTemplate.Business.Ports.Locks.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Modules.Extensions;
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
        var query = new Query()
            .Join("Users as UsersBlock", "Blocked.UserBlockedId", "UsersBlock.Id")
            .Join("Users", "Blocked.UserId", "Users.Id")
            .Select("Blocked.*", "UsersBlock.Nick as UserBlocked", "Users.Nick as User");

        foreach (var value in request.Value.Split(" "))
        {
            query.WhereContains("Users.Nick", $"{value}");
            query.WhereContains("Users.Email", $"{value}");
            query.WhereContains("UsersBlock.Nick", $"{value}");
            query.WhereContains("UsersBlock.Email", $"{value}");
        }

        var response = await _blockedRepository
            .GetPaginationAsync<GetBlockedResponse>(query, request.Page, request.Take, ct);

        response.Items = response.Items.Select(i => i with { End = DateTime.SpecifyKind(i.End, DateTimeKind.Local), Start = DateTime.SpecifyKind(i.Start, DateTimeKind.Local) });

        await _output.HandleAsync(response, ct);
    }
}