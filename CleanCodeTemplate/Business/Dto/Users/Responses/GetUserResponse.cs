namespace CleanCodeTemplate.Business.Dto.Users.Responses;

public struct GetUserResponse
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public string Nick { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public bool TwoFactors { get; set; }
}