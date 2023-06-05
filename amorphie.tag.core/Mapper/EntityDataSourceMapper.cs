using AutoMapper;


public class EntityDataSourceMapper : Profile
{
    public EntityDataSourceMapper()
    {
        CreateMap<EntityDataSource, DtoEntityDataSource>().ReverseMap();
    }
}