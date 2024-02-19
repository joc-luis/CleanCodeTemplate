using CleanCodeTemplate.Business.Dto.Authorizations;

namespace CleanCodeTemplate.Business.Ports.Authorizations.Input;

public interface IHaveAuthorizationInput
{
    Task HandleAsync(AuthorizationDto auth, CancellationToken ct);
}