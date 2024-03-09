namespace CleanCodeTemplate.Business.Dto.Locks.Requests;

public struct CreateBlockedRequest
{
    public Guid UserBlockedId { get; set; }
    public string Description { get; set; }
    public string End { get; set; }
}