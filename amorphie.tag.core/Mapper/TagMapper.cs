using AutoMapper;

namespace amorphie.tag.core.Mapper;

class TagMapper : Profile
{
    public TagMapper()
    {
        CreateMap<Tag, DtoTag>().ReverseMap();
        CreateMap<TagRelation, DtoTagRelation>().ReverseMap();
        CreateMap<DtoSaveTagRequest, Tag>();
    }
}