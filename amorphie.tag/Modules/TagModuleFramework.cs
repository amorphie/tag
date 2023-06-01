
using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;

public sealed class TagFrameworkModule : BaseTagModule<DtoTag, Tag, TagValidator>
{
    public TagFrameworkModule(WebApplication app) : base(app)
    {
    }

    public override string? UrlFragment => "Tag";


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

    }

}