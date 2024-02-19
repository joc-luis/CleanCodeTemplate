namespace CleanCodeTemplate.Business.Dto.Roles.Requests;

public struct UpdateRoleRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Guid> Permissions { get; set; }
}