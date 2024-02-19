namespace CleanCodeTemplate.Business.Dto.Account.Responses;

public struct GetAccountResponse
{
    public string Nick { get; set; }
    public string Email { get; set; }
    public IEnumerable<byte> Image { get; set; }
}