﻿namespace CleanCodeTemplate.Business.Dto.Account.Requests;

public struct CreateAccountRequest
{
    public string Nick { get; set; }
    public string Email { get; set; }
    public byte[]? Image { get; set; }
    public bool TwoFactors { get; set; }
}