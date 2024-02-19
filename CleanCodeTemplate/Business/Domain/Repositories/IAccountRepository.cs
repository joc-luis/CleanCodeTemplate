using CleanCodeTemplate.Business.Domain.Models;
using SqlKata;

namespace CleanCodeTemplate.Business.Domain.Repositories;

public interface IAccountRepository
{
    Task RegisterAsync(User store, CancellationToken ct);
    Task UpdateAsync(User update, CancellationToken ct);
    Task UpdatePasswordAsync(User update, CancellationToken ct);
    Task DestroyAsync(Guid id, CancellationToken ct);
    Task<TEntity> First<TEntity>(Guid id, CancellationToken ct);
}