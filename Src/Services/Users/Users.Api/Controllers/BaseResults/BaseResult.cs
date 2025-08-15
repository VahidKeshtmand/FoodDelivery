namespace Users.Api.Controllers.BaseResults;

public sealed record BaseResult<T>
{
    public T Data { get; private set; }

    public BaseResult(T data) {
        Data = data;
    }
}
