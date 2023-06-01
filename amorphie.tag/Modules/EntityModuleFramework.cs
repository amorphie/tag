
using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;

public sealed class EntityModuleFrameWorkModule : BaseEntityModule<DtoEntity, Entity, EntityValidator>
{
    public EntityModuleFrameWorkModule(WebApplication app) : base(app)
    {
    }

    public override string? UrlFragment => "entity";


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);

    }

}