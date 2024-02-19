namespace CleanCodeTemplate.Business.Dto.Account.Requests;

public struct UpdateAccountRequest
{
    public string Nick { get; set; }
    public string Email { get; set; }
    public IEnumerable<byte>? Image { get; set; }
    public string Password { get; set; }
}