using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using amorphie.core.Swagger;
using Microsoft.OpenApi.Models;
using amorphie.tag.data;
using System.Text.Json;
using amorphie.tag.data;
using amorphie.core.Extension;

namespace amorphie.domain.Module;

public class EntityDataModule : BaseBBTRoute<DtoEntityData, EntityData, TagDBContext>
{
    public EntityDataModule(WebApplication app)
        : base(app) { }

    public override string[]? PropertyCheckList => new string[] { "Name" };

    public override string? UrlFragment => "entityData";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("getEntityData/{domainName}/{entityName}", getEntityData);
        routeGroupBuilder.MapGet("{entityName}/{fieldName}", getEntityDataWithFieldName);
        routeGroupBuilder.MapGet("{entityName}", getEntityDataWithEntityName);
        routeGroupBuilder.MapDelete("{entityName}/{fieldName}", deleteEntityData);
        routeGroupBuilder.MapGet("/search", SearchMethod);


    }

    async ValueTask<IResult> getEntityData(
    [FromRoute(Name = "domainName")] string domainName,
    [FromRoute(Name = "entityName")] string entityName,
    [FromServices] TagDBContext context
)
    {
        if (context == null || context.Entities == null)
        {
            return Results.NotFound("Context or Entities is null.");
        }

        var entity = await context.Entities
            .Include(e => e.EntityData)
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
                    entity.EntityData.Select(d => new GetEntityDataResponse(d.Field, d.Ttl,
                        d.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                    )).ToArray()
                ));
        }
        else
            return Results.NoContent();
    }


    protected async ValueTask<IResult> deleteEntityData(
       [FromRoute(Name = "entityName")] string entityName,
       [FromRoute(Name = "fieldName")] string fieldName,
       [FromServices] TagDBContext context
    )
    {
        if (context == null || context.EntityData == null)
        {
            return Results.NotFound("Context or EntityData is null.");
        }
        var deletedData = context.EntityData.FirstOrDefault(d => d.Entity!.Name == entityName && d.Field == fieldName);
        if (deletedData != null)
        {
            context.EntityData.Remove(deletedData);
            await context.SaveChangesAsync();
            return Results.Ok();
        }
        else
            return Results.NoContent();

    }


    protected async ValueTask<IResult> getEntityDataWithFieldName(
       [FromRoute(Name = "entityName")] string entityName,
       [FromRoute(Name = "fieldName")] string fieldName,
       [FromServices] TagDBContext context
   )
    {
        if (context == null || context.EntityData == null)
        {
            return Results.NotFound("Context or EntityData is null.");
        }
        var entityData = await context.EntityData
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

    protected async ValueTask<IResult> getEntityDataWithEntityName(
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context
    )
    {
        if (context == null || context.EntityData == null)
        {
            return Results.NotFound("Context or EntityData is null.");
        }
        var entityData = await context.EntityData
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




    protected async ValueTask<IResult> SearchMethod(
        [FromServices] TagDBContext context,
        [FromServices] IMapper mapper,
        [AsParameters] EntityDataSearch entityDataSearch,
        HttpContext httpContext,
        CancellationToken token
    )
    {
        IQueryable<EntityData> query = context
                    .Set<EntityData>()
                    .AsNoTracking().Where(x => x.Field.ToLower().Contains(entityDataSearch.Keyword.ToLower()) || x.EntityName.ToLower().Contains(entityDataSearch.Keyword.ToLower()));

        if (!string.IsNullOrEmpty(entityDataSearch.SortColumn))
        {
            query = await query.Sort(entityDataSearch.SortColumn, entityDataSearch.SortDirection);
        }
        IList<EntityData> resultList = await query
            .Skip(entityDataSearch.Page * entityDataSearch.PageSize)
            .Take(entityDataSearch.PageSize)
            .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
            ? Results.Ok(mapper.Map<IList<DtoEntityData>>(resultList))
            : Results.NoContent();

    }
}
