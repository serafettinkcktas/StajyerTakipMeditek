namespace Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; }
    public ResultCode Code { get; }
    public string? Message { get; }

    protected Result(bool isSuccess, ResultCode code, string? message)
    {
        IsSuccess = isSuccess;
        Code = code;
        Message = message;
    }

    public static Result Success(string? message = null)
        => new(true, ResultCode.Success, message);

    public static Result Failure(ResultCode code, string message)
        => new(false, code, message);
}

public class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool isSuccess, T? data, ResultCode code, string? message)
        : base(isSuccess, code, message)
    {
        Data = data;
    }

    public static Result<T> Success(T data, string? message = null)
        => new(true, data, ResultCode.Success, message);

    public new static Result<T> Failure(ResultCode code, string message)
        => new(false, default, code, message);
}