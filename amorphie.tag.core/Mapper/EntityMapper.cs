using AutoMapper;

namespace amorphie.tag.core.Mapper;

public class EntityMapper : Profile
{
    public EntityMapper()
    {
        CreateMap<Entity, DtoEntity>().ReverseMap();
    }
}