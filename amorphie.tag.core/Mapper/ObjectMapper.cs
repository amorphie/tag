using System;
using AutoMapper;

public class ObjectMapper
{
    private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile<TagMapper>();

    });

    return config.CreateMapper();
});

    public static IMapper Mapper => lazy.Value;
}