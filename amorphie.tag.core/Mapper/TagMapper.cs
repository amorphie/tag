using AutoMapper;

class TagMapper : Profile
{
    public TagMapper()
    {
        CreateMap<Tag, DtoTag>().ReverseMap();
        CreateMap<TagRelation, DtoTagRelation>().ReverseMap();
        CreateMap<DtoSaveTagRequest, Tag>();
    }
}