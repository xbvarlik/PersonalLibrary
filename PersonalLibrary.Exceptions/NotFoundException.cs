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