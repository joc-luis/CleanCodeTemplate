namespace CleanCodeTemplate.Business.Dto.Roles.Requests;

public struct CreateRoleRequest
{
    public string Name { get; set; }
    public IEnumerable<Guid> Permissions { get; set; }
}