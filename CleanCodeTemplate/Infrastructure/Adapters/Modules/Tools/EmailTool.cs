using System.Net;
using MailKit.Net.Smtp;
using CleanCodeTemplate.Business.Modules.Tools;
using MailKit.Security;
using MimeKit;

namespace CleanCodeTemplate.Infrastructure.Adapters.Modules.Tools;

public class EmailTool : IEmailTool
{
    private readonly IDictionary<string, string> _env;
    private readonly SmtpClient _smtpClient;

    public EmailTool(IDictionary<string, string> env)
    {
        _env = env;
        _smtpClient = new SmtpClient();
    }

    public async Task SendAsync(string to, string subject, string body, CancellationToken ct)
    {
        _smtpClient.ServerCertificateValidationCallback = (s,c,h,e) => true;
        await _smtpClient.ConnectAsync(_env["SMTP_HOST"], Convert.ToInt32(_env["SMTP_PORT"]),
            SecureSocketOptions.Auto, ct);
       
        await _smtpClient.AuthenticateAsync(_env["SMTP_USER"], _env["SMTP_PASSWORD"], ct);
        
        await _smtpClient.SendAsync(new MimeMessage()
        {
            From ={ new MailboxAddress(_env["SMTP_EMAIL"], _env["SMTP_EMAIL"]) },
            To =  { new MailboxAddress(to, to) },
            Body = new TextPart("plain")
            {
                Text = body
            },
            Subject = subject
        }, ct);
    }
}