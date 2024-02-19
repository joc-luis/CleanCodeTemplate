using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using CleanCodeTemplate.Business.Dto;
using CleanCodeTemplate.Infrastructure.Adapters.Modules.Extensions;
using SqlKata;
using SqlKata.Execution;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Repositories;

public class BlockedRepository : IBlockedRepository
{
    private const string Table = "Blocked";
    private readonly SqlKataContext _kataContext;

    public BlockedRepository(SqlKataContext kataContext)
    {
        _kataContext = kataContext;
    }

    public Task CreateAsync(Blocked store, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table).InsertAsync(store, cancellationToken: ct);
    }

    public Task UpdateAsync(Blocked update, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table)
            .Where("Id", update.Id)
            .UpdateAsync(update, cancellationToken: ct);
    }

    public Task DestroyAsync(Guid id, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table)
            .Where("Id", id)
            .DeleteAsync(cancellationToken: ct);
    }

    public Task DestroyAsync(Query query, CancellationToken ct)
    {
        return _kataContext.QueryFactory
            .FromQuery(query)
            .From(Table)
            .DeleteAsync(cancellationToken: ct);
    }

    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table).GetAsync<TEntity>(cancellationToken: ct);
    }

    public Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct)
    {
        return _kataContext.QueryFactory
            .FromQuery(query)
            .From(Table)
            .GetAsync<TEntity>(cancellationToken: ct);
    }

    public Task<PaginationResult<TEntity>> GetPaginationAsync<TEntity>(int page, int take, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table).PaginateAsync<TEntity>(page, take, cancellationToken: ct);
    }

    public Task<PaginationResponse<TEntity>> GetPaginationAsync<TEntity>(Query query, int page, int take, CancellationToken ct)
    {
        return _kataContext.QueryFactory
            .FromQuery(query)
            .From(Table)
            .GetPaginationResultAsync<TEntity>(page, take, ct);
    }

    public Task<TEntity> First<TEntity>(Guid id, CancellationToken ct)
    {
        return _kataContext.QueryFactory
            .Query("Table")
            .Where("Id", id)
            .FirstAsync<TEntity>(cancellationToken: ct);
    }

    public Task<TEntity> First<TEntity>(Query query, CancellationToken ct)
    {
        return _kataContext.QueryFactory
            .FromQuery(query)
            .From(Table)
            .FirstAsync<TEntity>(cancellationToken: ct);
    }

    public Task<TEntity?> FirstOrDefault<TEntity>(Guid id, CancellationToken ct)
    {
        return _kataContext.QueryFactory
            .Query(Table)
            .Where("Id", id)
            .FirstOrDefaultAsync<TEntity>(cancellationToken: ct);
    }

    public Task<TEntity?> FirstOrDefault<TEntity>(Query query, CancellationToken ct)
    {
        return _kataContext.QueryFactory
            .FromQuery(query)
            .From("Table")
            .FirstOrDefaultAsync<TEntity>(cancellationToken: ct);
    }
}