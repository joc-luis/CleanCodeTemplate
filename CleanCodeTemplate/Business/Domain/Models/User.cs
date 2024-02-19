namespace CleanCodeTemplate.Business.Domain.Models;

public class User
{
    public User(Guid roleId, IEnumerable<byte> image, string nick, string password, string email, bool twoFactors = false)
    {
        Id = Guid.NewGuid();
        RoleId = roleId;
        Image = image;
        Nick = nick;
        Password = password;
        Email = email;
        TwoFactors = twoFactors;
    }

    public User()
    {
        
    }

    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public IEnumerable<byte> Image { get; set; }
    public string Nick { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool TwoFactors { get; set; }
}