namespace CleanCodeTemplate.Business.Domain.Models;

public class Role
{
    public Role(string name, IEnumerable<Guid> permissions)
    {
        Id = Guid.NewGuid();
        Name = name;
        Permissions = permissions;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Guid> Permissions { get; set; }

    public Role()
    {
        
    }
}