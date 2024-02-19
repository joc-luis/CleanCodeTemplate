namespace CleanCodeTemplate.Business.Exceptions.Http;

public class ForbiddenException : Exception
{
    public ForbiddenException(string message = "You do not have permissions to perform the requested action") : base(message)
    {
        
    }
}