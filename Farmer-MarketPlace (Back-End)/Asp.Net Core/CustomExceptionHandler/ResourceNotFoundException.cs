namespace DotnetBackend.ExceptionHandler;
public class ResourceNotFoundException : Exception
{
    /// <summary>
    /// Represents an exception that is thrown when a requested resource is not found.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ResourceNotFoundException(string message) : base(message)
    {
    }
}
