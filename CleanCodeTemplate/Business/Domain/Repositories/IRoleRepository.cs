using CleanCodeTemplate.Business.Domain.Models;
using SqlKata;
using SqlKata.Execution;

namespace CleanCodeTemplate.Business.Domain.Repositories;

public interface IRoleRepository
{
    Task CreateAsync(Role store, CancellationToken ct);
    Task UpdateAsync(Role update, CancellationToken ct);
    Task DestroyAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct);
    Task<PaginationResult<TEntity>> GetPaginationAsync<TEntity>(int page, int take, CancellationToken ct);
    Task<PaginationResult<TEntity>> GetPaginationAsync<TEntity>(Query query, int page, int take, CancellationToken ct);
    Task<TEntity> FirstAsync<TEntity>(Guid id, CancellationToken ct);
    Task<TEntity> FirstAsync<TEntity>(Query query, CancellationToken ct);
    Task<TEntity?> FirstOrDefault<TEntity>(Guid id, CancellationToken ct);
    Task<TEntity?> FirstOrDefault<TEntity>(Query query, CancellationToken ct);
}