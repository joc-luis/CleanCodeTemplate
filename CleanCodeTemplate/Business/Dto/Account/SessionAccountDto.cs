using CleanCodeTemplate.Business.Dto.Permissions;

namespace CleanCodeTemplate.Business.Dto.Account;

public class SessionAccountDto
{
    public Guid Id { get; init; }
    public Guid RoleId { get; init; }
}