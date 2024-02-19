using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Dto;
using SqlKata;
using SqlKata.Execution;

namespace CleanCodeTemplate.Business.Domain.Repositories;

public interface IBlockedRepository
{
    Task CreateAsync(Blocked store, CancellationToken ct);
    Task UpdateAsync(Blocked update, CancellationToken ct);
    Task DestroyAsync(Guid id, CancellationToken ct);
    Task DestroyAsync(Query query, CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct);
    Task<PaginationResult<TEntity>> GetPaginationAsync<TEntity>(int page, int take, CancellationToken ct);
    Task<PaginationResponse<TEntity>> GetPaginationAsync<TEntity>(Query query, int page, int take, CancellationToken ct);
    Task<TEntity> First<TEntity>(Guid id, CancellationToken ct);
    Task<TEntity> First<TEntity>(Query query, CancellationToken ct);
    Task<TEntity?> FirstOrDefault<TEntity>(Guid id, CancellationToken ct);
    Task<TEntity?> FirstOrDefault<TEntity>(Query query, CancellationToken ct);
}