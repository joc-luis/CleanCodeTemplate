﻿namespace CleanCodeTemplate.Business.Dto.Locks.Requests;

public struct UpdateBlockedRequest
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime End { get; set; }
}