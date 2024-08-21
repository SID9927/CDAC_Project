using System;

namespace DotnetBackend.ExceptionHandler;
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string message) : base(message)
    {
    }
}
