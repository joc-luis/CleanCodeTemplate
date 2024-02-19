namespace CleanCodeTemplate.Business.Modules.Tools;

public interface IEmailTool
{
    Task SendAsync(string to, string subject, string body, CancellationToken ct);
}