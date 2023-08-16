using AutoMapper;

namespace amorphie.tag.core.Mapper;

public class EntityDataSourceMapper : Profile
{
    public EntityDataSourceMapper()
    {
        CreateMap<EntityDataSource, DtoEntityDataSource>().ReverseMap();
    }
}