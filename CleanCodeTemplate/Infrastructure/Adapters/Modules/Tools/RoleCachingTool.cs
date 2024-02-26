﻿using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using EasyCaching.Core;

namespace CleanCodeTemplate.Infrastructure.Adapters.Modules.Tools;

public class RoleCachingTool : IRoleCachingTool
{
    private readonly IEasyCachingProvider _cachingProvider;

    public RoleCachingTool(IEasyCachingProviderFactory cachingFactory)
    {
        _cachingProvider = cachingFactory.GetCachingProvider("roles");
    }


    public async Task<TValue> GetAsync<TValue>(string key, CancellationToken ct)
    {
        var cache = await _cachingProvider.GetAsync<TValue>(key, cancellationToken: ct);

        if (!cache.HasValue)
        {
            throw new NotFoundException();
        }

        return cache.Value;
    }

    public Task<bool> ExistAsync(string key, CancellationToken ct)
    {
        return _cachingProvider.ExistsAsync(key, ct);
    }

    public Task SetAsync<TValue>(string key, TValue value, TimeSpan duration, CancellationToken ct)
    {
        return _cachingProvider.SetAsync(key, value, duration, ct);
    }

    public Task RemoveAsync(string key, CancellationToken ct)
    {
        return _cachingProvider.RemoveAsync(key, ct);
    }
}