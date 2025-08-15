namespace Users.Application.Common.Models.BaseDtos;

public interface IBaseDto<TKey>
{
    public TKey Id { get; set; }
}

public interface IBaseDto : IBaseDto<int> { }
