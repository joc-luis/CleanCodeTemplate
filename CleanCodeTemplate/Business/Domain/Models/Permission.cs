namespace CleanCodeTemplate.Business.Domain.Models;

public class Permission
{
    public Permission(Guid optionId, string name, string url, string method)
    {
        Id = Guid.NewGuid();
        OptionId = optionId;
        Name = name;
        Url = url;
        Method = method;
    }
    
    public Permission(string name, string url, string method)
    {
        Id = Guid.NewGuid();
        OptionId = null;
        Name = name;
        Url = url;
        Method = method;
    }

    public Guid Id { get; set; }
    public Guid? OptionId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Method { get; set; }

    public Permission()
    {
        
    }
}