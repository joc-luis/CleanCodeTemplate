namespace CleanCodeTemplate.Business.Dto.Account.Requests;

public struct TwoFactorLoginRequest
{
    public string Key { get; set; }
    public string Code { get; set; }
}