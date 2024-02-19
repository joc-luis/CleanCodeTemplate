using CleanCodeTemplate.Business.Dto;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace CleanCodeTemplate.Infrastructure.Adapters.Modules.Extensions;

public static class SqlKataExtensions
{
    public static async Task<PaginationResponse<TEntity>> GetPaginationResultAsync<TEntity>(this Query query,
        int page = 1, int take = 25, CancellationToken ct = default)
    {
        PaginationResponse<TEntity> pagination = new PaginationResponse<TEntity>();
        
        var result = await query.PaginateAsync<TEntity>(page, take, cancellationToken: ct);

        pagination.Page = result.Page;
        pagination.Take = result.PerPage;
        pagination.Items = result.List;
        pagination.Pages = result.TotalPages;
        pagination.Total = result.Count;

        return pagination;
    }
}