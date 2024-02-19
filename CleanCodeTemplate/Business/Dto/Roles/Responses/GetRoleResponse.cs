namespace CleanCodeTemplate.Business.Dto.Roles.Responses;

public struct GetRoleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Guid> Permissions { get; set; }
}