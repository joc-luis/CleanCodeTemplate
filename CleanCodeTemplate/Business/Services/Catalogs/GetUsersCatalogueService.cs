using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto.Catalogs.Responses;
using CleanCodeTemplate.Business.Ports.Catalogs.Input;
using CleanCodeTemplate.Business.Ports.Catalogs.Output;
using SqlKata;

namespace CleanCodeTemplate.Business.Services.Catalogs;

public class GetUsersCatalogueService : IGetUsersCatalogueInput
{
    private readonly IUserRepository _userRepository;
    private readonly IGetCatalogueOutput _output;


    public GetUsersCatalogueService(IUserRepository userRepository, IGetCatalogueOutput output)
    {
        _userRepository = userRepository;
        _output = output;
    }

    public async Task HandleAsync(CancellationToken ct)
    {
        IEnumerable<GetCatalogueResponse> response = await _userRepository
            .GetAsync<GetCatalogueResponse>(new Query().Select("Id", "Nick as Name"), ct);

        await _output.HandleAsync(response, ct);
    }
}