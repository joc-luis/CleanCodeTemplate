namespace CleanCodeTemplate.Business.Dto.Account.Responses;

public struct LoginAccountResponse
{
    public bool IsTwoFactors { get; set; }
    public string Token { get; set; }
}