using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Business.Dto.Users.Responses;
using CleanCodeTemplate.Business.Ports.Users.Input;
using CleanCodeTemplate.Business.Ports.Users.Output;
using CleanCodeTemplate.Infrastructure.Adapters.Presenters;
using FastEndpoints;
using SqlKata.Execution;

namespace CleanCodeTemplate.Infrastructure.Endpoints.Users;

public class GetUserEndpoint : Endpoint<SearchPaginationRequest, PaginationResponse<GetUserResponse>>
{
    private readonly IGetUserInput _input;
    private readonly IGetUserOutput _output;

    public GetUserEndpoint(IGetUserInput input, IGetUserOutput output)
    {
        _input = input;
        _output = output;
    }

    public override void Configure()
    {
        Post("/user/search");
    }

    public override async Task HandleAsync(SearchPaginationRequest req, CancellationToken ct)
    {
        await _input.HandleAsync(req, ct);

        await SendAsync(((IPresenter<PaginationResponse<GetUserResponse>>)_output).Response, cancellation: ct);
    }
}