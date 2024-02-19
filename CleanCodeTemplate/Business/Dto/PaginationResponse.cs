namespace CleanCodeTemplate.Business.Dto;

public class PaginationResponse<TValue>
{
    public int Page { get; set; }
    public int Take { get; set; }
    public long Total { get; set; }
    public int Pages { get; set; }
    public IEnumerable<TValue> Items { get; set; } = new List<TValue>();
}