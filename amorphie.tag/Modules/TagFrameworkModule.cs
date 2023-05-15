using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;
using amorphie.core.Module.minimal_api;
using amorphie.tag.Validator;

public sealed class TagFrameworkModule : BaseBBTRoute<DtoTag, Tag, TagValidator, DbContext>
{
        public TagFrameworkModule(WebApplication app) : base(app)
        {
        }


        public override string? UrlFragment => "tag";

        public override string[]? PropertyCheckList => new string[] { "Url","Ttl" };

        public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
        {
            base.AddRoutes(routeGroupBuilder);

            routeGroupBuilder.MapPost("custom", () => { });
        }
}

