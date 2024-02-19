namespace CleanCodeTemplate.Business.Dto;

public struct SearchPaginationRequest
{
    public string Value { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }
}