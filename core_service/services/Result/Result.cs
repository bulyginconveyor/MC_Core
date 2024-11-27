namespace core_service.services.Result;

public record Result
{
    private ResultStatus _result;
    public bool IsSuccess => _result == ResultStatus.Success;
    public bool IsError => _result == ResultStatus.Error;
    public string? ErrorMessage { get; protected set; } = null;

    protected Result()
    {
        _result = ResultStatus.Success;
    }

    protected Result(string errorMessage)
    {
        ErrorMessage = errorMessage;
        _result = ResultStatus.Error;
    }
    
    public static Result Success() => new Result();
    public static Result Error(string errorMessage) => new Result(errorMessage);
}


public record Result<T> : Result
{
    public T? Value { get; init; }

    protected Result(T value) : base()
    {
        this.Value = value;
    }

    protected Result(T value, string? errorMessage) : base(errorMessage!)
    {
        this.Value = value;
        this.ErrorMessage = errorMessage ?? $"Error in type {typeof(T).Name}. This is default error message!";
    }
    
    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Error(T value, string? errorMessage) => new Result<T>(value, errorMessage);
}