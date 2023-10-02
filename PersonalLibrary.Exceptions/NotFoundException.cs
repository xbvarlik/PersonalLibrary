namespace PersonalLibrary.Exceptions;

public class NotFoundException : NullReferenceException
{
    public NotFoundException()
    {
        
    }

    public NotFoundException(string message) : base(message)
    {
        
    }
}

public class TokenNullException : NullReferenceException
{
    public TokenNullException()
    {
        
    }

    public TokenNullException(string message) : base(message)
    {
        
    }
}