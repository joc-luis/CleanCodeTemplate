using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using SqlKata;
using SqlKata.Execution;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly SqlKataContext _kataContext;
    private const string Table = "Permissions";

    public PermissionRepository(SqlKataContext kataContext)
    {
        _kataContext = kataContext;
    }

    public Task CreateAsync(Permission store, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table).InsertAsync(store, cancellationToken: ct);
    }

    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct)
    {
        return _kataContext.QueryFactory.FromQuery(query).From(Table).GetAsync<TEntity>(cancellationToken: ct);
    }

    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table).GetAsync<TEntity>(cancellationToken: ct);
    }
}