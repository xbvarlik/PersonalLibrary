namespace PersonalLibrary.Exceptions;

public class MongoException : Exception
{
    public MongoException()
    {
        
    }
    
    public MongoException(string message) : base(message)
    {
        
    }
}