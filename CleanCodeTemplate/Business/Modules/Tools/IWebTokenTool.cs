using CleanCodeTemplate.Business.Dto.Account;

namespace CleanCodeTemplate.Business.Modules.Tools;

public interface IWebTokenTool
{
    SessionAccountDto SessionAccount { get; }

    Task<string> GenerateTokenAsync(SessionAccountDto sessionAccountDto, int days, CancellationToken ct);

    Task SetSessionAsync(string token, CancellationToken ct);
}