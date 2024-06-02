namespace Domain.Shared;

public class Result
{
    public bool IsSuccess { get; }
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
}

public class Result<T> : Result
{
    public T? Value { get; }


    private Result(T? value, bool isSuccess, Error? error) : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, default);
    }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(default, false, error);
    }
}