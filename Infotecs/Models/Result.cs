namespace Infotecs.Models;

public class Result<TValue>
{
    public Result(TValue value)
    {
        IsSuccess = true;
        Value = value;
    }
    public Result(bool isSuccess, int statusCode, string? message)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
    public Result(bool isSuccess, int statusCode, TValue value, string? message)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
    public bool IsSuccess { get; set; }
    public TValue? Value { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}