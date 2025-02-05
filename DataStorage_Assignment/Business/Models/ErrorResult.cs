namespace Business.Models;

public class ErrorResult : Result
{
    public ErrorResult(int statusCode, string? errorMessage)
    {
        Success = false;
        StatusCode = statusCode;
    }
}