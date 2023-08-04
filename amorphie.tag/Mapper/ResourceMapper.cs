using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace amorphie.tag.Mapper
{
    public sealed class ResourceMapper : Profile
    {
        public ResourceMapper()
        {
            CreateMap<Domain, DtoDomain>().ReverseMap();
            CreateMap<EntityData, DtoEntityData>().ReverseMap();
            CreateMap<EntityDataSource, DtoEntityDataSource>().ReverseMap();
            CreateMap<Entity, DtoEntity>().ReverseMap();
            CreateMap<Tag, DtoTag>().ReverseMap();
            CreateMap<TagRelation, DtoTagRelation>().ReverseMap();
            CreateMap<DtoSaveTagRequest, Tag>();


        }
    }
}
