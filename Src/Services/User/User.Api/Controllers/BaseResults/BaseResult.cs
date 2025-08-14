namespace User.Api.Controllers.BaseResults;

/// <summary>
/// BaseResult
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed record BaseResult<T>
{
    /// <summary>
    /// Data
    /// </summary>
    public T Data { get; private set; }

    /// <summary>
    /// BaseResult
    /// </summary>
    /// <param name="data"></param>
    public BaseResult(T data) {
        Data = data;
    }
}
