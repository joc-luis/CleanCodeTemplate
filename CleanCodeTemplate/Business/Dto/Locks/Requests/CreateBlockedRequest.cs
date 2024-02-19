namespace CleanCodeTemplate.Business.Dto.Locks.Requests;

public struct CreateBlockedRequest
{
    public Guid BlockedUserId { get; set; }
    public string Description { get; set; }
    public DateTime End { get; set; }
}