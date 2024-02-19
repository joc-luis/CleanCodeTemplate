namespace CleanCodeTemplate.Business.Dto.Account.Requests;

public struct ChangePasswordAccountRequest
{
    public string Key { get; set; }
    public string Code { get; set; }
    public string NewPassword { get; set; }
}