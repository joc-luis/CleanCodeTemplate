namespace CleanCodeTemplate.Business.Exceptions.Http;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message = "Sign in to continue.") : base(message)
    {
        
    }
}