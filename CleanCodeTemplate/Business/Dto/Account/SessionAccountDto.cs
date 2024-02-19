using CleanCodeTemplate.Business.Dto.Permissions;

namespace CleanCodeTemplate.Business.Dto.Account;

public class SessionAccountDto
{
    public Guid Id { get; set; }
    public IEnumerable<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
}