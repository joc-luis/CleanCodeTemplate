namespace CleanCodeTemplate.Business.Dto.Users.Requests;

public struct CreateUserRequest
{
    public Guid RoleId { get; set; }
    public string Nick { get; set; }
    public string Email { get; set; }
    public IEnumerable<byte>? Image { get; set; }
    public bool TwoFactors { get; set; }
}