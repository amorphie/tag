using AutoMapper;


public class EntityDataMapper : Profile
{
    public EntityDataMapper()
    {
        CreateMap<EntityData, DtoEntityData>().ReverseMap();
    }
}