namespace CleanCodeTemplate.Business.Exceptions.Http;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        
    }
}