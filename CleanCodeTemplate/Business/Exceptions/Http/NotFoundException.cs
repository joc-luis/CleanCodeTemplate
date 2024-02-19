namespace CleanCodeTemplate.Business.Exceptions.Http;

public class NotFoundException : Exception
{
    public NotFoundException(string message = "The requested resource does not exist.") : base(message)
    {
        
    }
}