namespace CleanCodeTemplate.Business.Modules.Tools;

public interface ICryptographyTool
{
    Task<string> HashAsync(string raw);

    Task<bool> VerifyHashAsync(string hashed, string raw);

    Task<string> RandomStringAsync(int length = 10);
}