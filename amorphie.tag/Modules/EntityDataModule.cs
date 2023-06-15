
using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;

public sealed class EntityDataModuleFrameWorkModule : BaseEntityModule<DtoEntityData, EntityData, EntityDataValidator>
{
    public EntityDataModuleFrameWorkModule(WebApplication app) : base(app)
    {
    }

    public override string? UrlFragment => "entityData";


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapDelete("{entityName}/{fieldName}", deleteEntityData);
        routeGroupBuilder.MapGet("{entityName}/{fieldName}", getEntityDataWithFieldName);
        routeGroupBuilder.MapGet("getEntityDataWithEntityName/{entityName}", getEntityDataWithEntityName);
        routeGroupBuilder.MapGet("getEntity/{domainName}/{entityName}", getEntity); // getEntity with DomainName
    }

    //Get EntityData with DomainName and EntityName
    async ValueTask<IResult> getEntity(
        [FromRoute(Name = "domainName")] string domainName,
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context
    )
    {
        var entity = await context!.Entities!
            .Include(e => e.Data)
            .ThenInclude(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(e => e.Name == entityName && e.DomainName == domainName)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            return Results.Ok(
                new GetEntityResponse(
                    entity.Name,
                    entity.Description!,
                    entity.Data.Select(d => new GetEntityDataResponse(d.Field, d.Ttl,
                        d.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                    )).ToArray()
                ));
        }
        else
            return Results.NoContent();
    }


    //Delete EntityData with EntityName and Entity FieldName
    async ValueTask<IResult> deleteEntityData(
       [FromRoute(Name = "entityName")] string entityName,
       [FromRoute(Name = "fieldName")] string fieldName,
       [FromServices] TagDBContext context
)
    {
        var deletedData = context.EntityData!.FirstOrDefault(d => d.Entity.Name == entityName && d.Field == fieldName);
        if (deletedData != null)
        {
            context.EntityData!.Remove(deletedData);
            await context.SaveChangesAsync(); // await ile SaveChangesAsync'yi bekleyin
            return Results.Ok();
        }
        else
        {
            return Results.NoContent();
        }
    }

    async ValueTask<IResult> getEntityDataWithFieldName(
        [FromRoute(Name = "entityName")] string entityName,
        [FromRoute(Name = "fieldName")] string fieldName,
        [FromServices] TagDBContext context
    )
    {
        var entityData = await context!.EntityData!
            .Include(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(d => d.Entity!.Name == entityName && d.Field == fieldName)
            .FirstOrDefaultAsync();

        if (entityData != null)
        {
            return Results.Ok(
                new GetEntityDataResponse(
                    entityData.Field,
                    entityData.Ttl,
                    entityData.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                ));
        }
        else
            return Results.NoContent();
    }
    async ValueTask<IResult> getEntityDataWithEntityName(
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context
    )
    {
        // var entityData = await context!.EntityData!
        //     .Include(d => d.Sources)
        //     .ThenInclude(s => s.Tag)
        //     .Where(d => d.Entity!.Name == entityName && d.Field == fieldName)
        //     .FirstOrDefaultAsync();

        var entityData = await context!.EntityData!
            .Include(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(d => d.Entity!.Name == entityName)
            .FirstOrDefaultAsync();

        if (entityData != null)
        {
            return Results.Ok(
                new GetEntityDataResponse(
                    entityData.Field,
                    entityData.Ttl,
                    entityData.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                ));
        }
        else
            return Results.NoContent();
    }
}