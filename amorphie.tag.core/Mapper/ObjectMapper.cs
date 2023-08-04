using System;
using AutoMapper;
namespace amorphie.tag.core.Mapper;

public class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new(() =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<TagMapper>();

    });

    return config.CreateMapper();
});

    public static IMapper Mapper => lazy.Value;
}