namespace CleanCodeTemplate.Business.Dto.Account;

public struct TwoFactorsDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
}