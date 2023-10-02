namespace PersonalLibrary.Exceptions;

public class IdentityException : Exception
{
    public IdentityException()
    {
        
    }

    public IdentityException(string message) : base(message)
    {
        
    }
}

public class InvalidCredentialsException : IdentityException
{
    public InvalidCredentialsException()
    {
        
    }

    public InvalidCredentialsException(string message) : base(message)
    {
        
    }
}
