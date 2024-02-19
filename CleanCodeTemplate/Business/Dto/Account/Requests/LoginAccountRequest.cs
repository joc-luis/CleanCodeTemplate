namespace CleanCodeTemplate.Business.Dto.Account.Requests;

public struct LoginAccountRequest
{
    public string Nick { get; set; }
    public string Password { get; set; }
}