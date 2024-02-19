using CleanCodeTemplate.Business.Domain.Models;
using SqlKata;

namespace CleanCodeTemplate.Business.Domain.Repositories;

public interface IPermissionRepository
{
        Task CreateAsync(Permission store, CancellationToken ct);
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct);
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(CancellationToken ct);
}