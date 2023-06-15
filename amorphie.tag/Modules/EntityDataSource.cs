
using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;

public sealed class EntityDataSourceModuleFrameWorkModule : BaseEntityDataSourceModule<DtoEntityDataSource, EntityDataSource, EntityDataSourceValidator>
{
    public EntityDataSourceModuleFrameWorkModule(WebApplication app) : base(app)
    {
    }


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
    }

}