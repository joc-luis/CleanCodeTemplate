using CleanCodeTemplate.Business.Domain.Models;
using SqlKata;

namespace CleanCodeTemplate.Business.Domain.Repositories;

public interface IOptionRepository
{
    Task CreateAsync(Option store, CancellationToken ct);
    Task<IEnumerable<TEntity>> GetAsync<TEntity>(Query query, CancellationToken ct);
}