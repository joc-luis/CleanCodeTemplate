using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Users.Responses;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Ports.Users.Output;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Users;

public class GetUserService : IGetUserInput
{
    private readonly IUserRepository _userRepository;
    private readonly IGetUserOutput _output;

    public GetUserService(IUserRepository userRepository, IGetUserOutput output)
    {
        _userRepository = userRepository;
        _output = output;
    }

    public async Task HandleAsync(SearchPaginationRequest request, CancellationToken ct)
    {
        var query = new Query().Join("Roles", "Roles.Id", "RoleId")
            .Select("Users.Id", "Users.Nick", "Users.Email", "Roles.Name as Role", "RoleId", "TwoFactors");


        foreach (string value in request.Value.Split(" "))
        {
            query.OrWhereRaw("Users.Nick like ?", $"%{value}%");
            query.OrWhereRaw("Users.Email like ?", $"%{value}%");
            query.OrWhereRaw("Roles.Name like ?", $"%{value}%");
        }
            

        var response = await _userRepository
            .GetPaginationAsync<GetUserResponse>(query, request.Page, request.Take, ct);
        
        await _output.HandleAsync(response, ct);
    }
}