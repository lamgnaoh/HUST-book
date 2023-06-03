namespace AssignmentApp.API.Utilities.Exception;

public class CustomException : System.Exception
{
    public CustomException()
    {
    }

    public CustomException(string message)
        : base(message)
    {
    }

    public CustomException(string message, System.Exception inner)
        : base(message, inner)
    {
        
    }
}