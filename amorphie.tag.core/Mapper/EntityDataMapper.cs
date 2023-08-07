using AutoMapper;

namespace amorphie.tag.core.Mapper;

public class EntityDataMapper : Profile
{
    public EntityDataMapper()
    {
        CreateMap<EntityData, DtoEntityData>().ReverseMap();
    }
}