namespace Copepod.Sdk;

/// <summary>
/// Exception thrown when the Copepod API returns an error response.
/// </summary>
public class CopepodException : Exception
{
    public int StatusCode { get; }
    public object? ResponseData { get; }

    public CopepodException(int statusCode, string message, object? responseData = null)
        : base(message)
    {
        StatusCode = statusCode;
        ResponseData = responseData;
    }
}

/// <summary>
/// Exception thrown when an operation requires authentication but no token is set.
/// </summary>
public class NotAuthenticatedException : CopepodException
{
    public NotAuthenticatedException()
        : base(401, "Not authenticated: call LoginAsync() or SetToken() first")
    {
    }
}
