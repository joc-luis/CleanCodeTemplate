namespace CleanCodeTemplate.Business.Dto.Options.Responses;

public struct GetParentOptionResponse
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public IEnumerable<GetChildOptionResponse> Children { get; set; }
}