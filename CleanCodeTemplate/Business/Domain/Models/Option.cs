namespace CleanCodeTemplate.Business.Domain.Models;

public class Option
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Icon { get; set; }
    
    public Option(string name, string url, string icon, Guid? parentId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Url = url;
        Icon = icon;
        ParentId = parentId;
    }

    public Option()
    {
        
    }
}