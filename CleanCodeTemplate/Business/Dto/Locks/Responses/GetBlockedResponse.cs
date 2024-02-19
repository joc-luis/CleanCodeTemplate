namespace CleanCodeTemplate.Business.Dto.Locks.Responses;

public struct GetBlockedResponse
{
    public Guid Id { get; set; }
    public Guid UserBlockedId { get; set; }
    public string UserBlocked { get; set; }
    public Guid UserId { get; set; }
    public string User { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}