using System.Text;
using CleanCodeTemplate.Business.Domain.Models;
using CleanCodeTemplate.Business.Domain.Repositories;
using SqlKata.Execution;

namespace CleanCodeTemplate.Infrastructure.Adapters.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly SqlKataContext _kataContext;
    private const string Table = "Users";

    public AccountRepository(SqlKataContext kataContext)
    {
        _kataContext = kataContext;
    }

    public Task RegisterAsync(User store, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table)
            .InsertAsync(new
            {
                store.Id,
                store.RoleId,
                store.Email,
                store.Nick,
                store.Password,
                store.TwoFactors,
                Image = store.Image.ToArray()
            }, cancellationToken: ct);
    }

    public Task UpdateAsync(User update, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table)
            .Where("Id", update.Id)
            .UpdateAsync(new
            {
                update.RoleId,
                update.Email,
                update.Nick,
                update.TwoFactors,
                Image = update.Image.ToArray()
            }, cancellationToken: ct);
    }

    public Task UpdatePasswordAsync(User update, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table)
            .Where("Id", update.Id)
            .UpdateAsync(new { update.Password }, cancellationToken: ct);
    }

    public Task DestroyAsync(Guid id, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table)
            .Where("Id", id)
            .DeleteAsync(cancellationToken: ct);
    }

    public Task<TEntity> First<TEntity>(Guid id, CancellationToken ct)
    {
        return _kataContext.QueryFactory.Query(Table)
            .Where("Id", id)
            .FirstAsync<TEntity>(cancellationToken: ct);
    }
}