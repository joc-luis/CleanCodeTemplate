using CleanCodeTemplate.Business.Dto.Options.Responses;

namespace CleanCodeTemplate.Business.Ports.Options.Output;

public interface IGetOptionOutput
{
    Task HandleAsync(IEnumerable<GetParentOptionResponse> response, CancellationToken ct);
}