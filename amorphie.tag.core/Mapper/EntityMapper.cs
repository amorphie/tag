using AutoMapper;


public class EntityMapper : Profile
{
    public EntityMapper()
    {
        CreateMap<Entity, DtoEntity>().ReverseMap();
    }
}