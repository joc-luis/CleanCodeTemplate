namespace CleanCodeTemplate.Business.Domain.Models;

public class Blocked
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid UserBlockedId { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; } = DateTime.UtcNow;
    public DateTime End { get; set; }

    public Blocked(Guid userId, Guid userBlockedId, string description, DateTime end)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        UserBlockedId = userBlockedId;
        Description = description;
        End = end;
    }

    public Blocked()
    {
        
    }
}