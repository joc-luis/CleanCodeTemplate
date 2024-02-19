namespace CleanCodeTemplate.Business.Dto.Account.Requests;

public struct UpdatePasswordAccountRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}