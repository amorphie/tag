using AutoMapper;


public class DomainMapper : Profile
{
    public DomainMapper()
    {
        CreateMap<Domain, DtoDomain>().ReverseMap();
    }
}