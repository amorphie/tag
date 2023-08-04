using AutoMapper;


namespace amorphie.tag.core.Mapper;
public class DomainMapper : Profile
{
    public DomainMapper()
    {
        CreateMap<Domain, DtoDomain>().ReverseMap();
    }
}