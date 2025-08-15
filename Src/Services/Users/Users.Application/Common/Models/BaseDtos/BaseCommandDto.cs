using AutoMapper;
using Users.Application.Common.Mappings;
using Users.Domain.Common;

namespace Users.Application.Common.Models.BaseDtos;

public abstract record BaseCommandDto<TDto, TEntity, TKey> : IMapping
    where TDto : class, new()
    where TEntity : class, IBaseEntity<TKey>, new()
{

    public TEntity ToEntity(IMapper mapper) {
        return mapper.Map<TEntity>(CastToDerivedClass(mapper, this));
    }

    public TEntity ToEntity(IMapper mapper, TEntity entity) {
        return mapper.Map(CastToDerivedClass(mapper, this), entity);
    }

    public static TDto FromEntity(IMapper mapper, TEntity model) {
        return mapper.Map<TDto>(model);
    }

    protected TDto CastToDerivedClass(IMapper mapper, BaseCommandDto<TDto, TEntity, TKey> baseInstance) {
        return mapper.Map<TDto>(baseInstance);
    }

    public void CreateMappings(Profile profile) {
        var mappingExpression = profile.CreateMap<TDto, TEntity>();

        var dtoType = typeof(TDto);
        var entityType = typeof(TEntity);
        //Ignore any property of source (like Post.Author) that dose not contains in destination 
        foreach ( var property in entityType.GetProperties() ) {
            if ( dtoType.GetProperty(property.Name) == null )
                mappingExpression.ForMember(property.Name, opt => opt.Ignore());
        }

        CustomMappings(mappingExpression.ReverseMap());
    }

    public virtual void CustomMappings(IMappingExpression<TEntity, TDto> mapping) { }
}

public abstract record BaseCommandDto<TDto, TEntity> : BaseCommandDto<TDto, TEntity, int>
    where TDto : class, new()
    where TEntity : class, IBaseEntity<int>, new()
{

}
