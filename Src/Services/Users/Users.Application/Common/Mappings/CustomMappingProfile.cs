using AutoMapper;

namespace Users.Application.Common.Mappings;

public sealed class CustomMappingProfile : Profile
{
    public CustomMappingProfile(IEnumerable<IMapping> mappings) {
        foreach ( var item in mappings ) {
            item.CreateMappings(this);
        }
    }
}
