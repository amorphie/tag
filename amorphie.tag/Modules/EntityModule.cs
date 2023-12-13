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
using amorphie.core.Extension;

namespace amorphie.domain.Module;

public class EntityModule : BaseBBTRoute<DtoEntity, Entity, TagDBContext>
{
    public EntityModule(WebApplication app)
        : base(app) { }

    public override string[]? PropertyCheckList => new string[] { "Name" };

    public override string? UrlFragment => "entity";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("{domainName}/{entityName}", getEntity);
        routeGroupBuilder.MapDelete("{domainName}/{entityName}", deleteEntity);
        routeGroupBuilder.MapGet("/search", SearchMethod);
    }


    protected async ValueTask<IResult> getEntity(
            [FromRoute(Name = "domainName")] string domainName,
            [FromRoute(Name = "entityName")] string entityName,
            [FromServices] TagDBContext context,
            IMapper mapper

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
        var mappedData = mapper.Map<GetEntityResponseDto>(entity);

        if (entity != null)
        {
            return Results.Ok(mappedData);
        }
        else
            return Results.NoContent();
    }


    async Task<IResult> deleteEntity(
    [FromRoute(Name = "domainName")] string domainName,
    [FromRoute(Name = "entityName")] string entityName,
    [FromServices] TagDBContext context
)
    {
        if (context == null || context.Entities == null)
        {
            return Results.NotFound("Context or Entities is null.");
        }
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var entityToDelete = await context.Entities
                .Where(e => e.Name == entityName && e.DomainName == domainName)
                .Include(e => e.EntityData)
                .FirstOrDefaultAsync();

            if (entityToDelete != null)
            {
                context.Entities.Remove(entityToDelete);
                context.EntityData!.RemoveRange(entityToDelete.EntityData);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Results.Ok();
            }
            else
            {
                await transaction.RollbackAsync();
                return Results.NoContent();
            }
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Results.BadRequest($"An error occurred: {ex.Message}");
        }
    }



    protected async ValueTask<IResult> SearchMethod(
        [FromServices] TagDBContext context,
        [FromServices] IMapper mapper,
        [AsParameters] EntitySearch entitySearch,
        HttpContext httpContext,
        CancellationToken token
    )
    {
        IQueryable<Entity> query = context
                    .Set<Entity>()
                    .AsNoTracking().Where(x=>x.Name.ToLower().Contains(entitySearch.Keyword.ToLower())||x.DomainName.ToLower().Contains(entitySearch.Keyword.ToLower()));

        if (!string.IsNullOrEmpty(entitySearch.SortColumn))
        {
            query = await query.Sort(entitySearch.SortColumn, entitySearch.SortDirection);
        }
        IList<Entity> resultList = await query
            .Skip(entitySearch.Page * entitySearch.PageSize)
            .Take(entitySearch.PageSize)
            .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
            ? Results.Ok(mapper.Map<IList<DtoEntity>>(resultList))
            : Results.NoContent();
    }

}