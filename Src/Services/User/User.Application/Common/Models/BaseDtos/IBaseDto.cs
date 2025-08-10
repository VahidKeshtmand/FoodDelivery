namespace User.Application.Common.Models.BaseDtos;

/// <summary>
/// کلاس پایه Dto
/// </summary>
/// <typeparam name="TKey">شناسه</typeparam>
public interface IBaseDto<TKey>
{
    /// <summary>
    /// شناسه
    /// </summary>
    public TKey Id { get; set; }
}

public interface IBaseDto : IBaseDto<int> { }
