namespace CleanCodeTemplate.Business.Modules.Tools;

public interface IBlockedCachingTool
{
    Task<TValue> GetAsync<TValue>(string key, CancellationToken ct);
    Task<bool> ExistsAsync(string key, CancellationToken ct);
    Task SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken ct);
    Task RemoveAsync(string key, CancellationToken ct);
}