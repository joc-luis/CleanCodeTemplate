namespace CleanCodeTemplate.Business.Modules.Tools;

public interface IRoleCachingTool
{
    Task<TValue> GetAsync<TValue>(string key, CancellationToken ct);
    Task<bool> ExistAsync(string key, CancellationToken ct);
    Task SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken ct);
    Task RemoveAsync(string key, CancellationToken ct);
}