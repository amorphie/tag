
using amorphie.core.Base;
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
        routeGroupBuilder.MapGet("getTag/{tagName}", getTag);

    }
    async ValueTask<IResult> getTag(
      [FromRoute(Name = "tagName")] string tagName,
      [FromServices] TagDBContext context
      )
    {

        var tag = await context!.Tags!
            .Include(st => st.TagsRelations)
            .FirstOrDefaultAsync(t => t.Name == tagName);

        var tagDto = ObjectMapper.Mapper.Map<DtoTag>(tag);

        if (tag == null)
            return Results.NotFound();
        return Results.Ok(tagDto);
    }
}