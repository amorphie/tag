using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.OpenApi.Models;
using amorphie.tag.data;
using System.Text.Json;
using System.Text.Json.Serialization;
using amorphie.core.Base;
using amorphie.core.Extension;

namespace amorphie.domain.Module;

public class DomainModule : BaseBBTRoute<DtoDomain, Domain, TagDBContext>
{
    public DomainModule(WebApplication app)
        : base(app) { }

    public override string[]? PropertyCheckList => new string[] { "Name" };

    public override string? UrlFragment => "domain";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("{domainName}/{entityName}", getEntity);
        routeGroupBuilder.MapGet("/search", SearchMethod);

    }


    async Task<IResult> getEntity(
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
            .Where(e => e.Name == entityName)
            .FirstOrDefaultAsync();

        if (entity != null)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                MaxDepth = 3

            };
            var response = mapper.Map<EntityAllDto>(entity);
            var serialezedResponse = JsonSerializer.Serialize(response, options);
            return Results.Ok(response);
        }
        else
        {
            return Results.NotFound();
        }
    }



    protected async ValueTask<IResult> SearchMethod(
        [FromServices] TagDBContext context,
        [FromServices] IMapper mapper,
        [AsParameters] DomainSearch domainSearch,
        HttpContext httpContext,
        CancellationToken token
    )
    {
        IQueryable<Domain> query = context
                  .Set<Domain>()
                  .AsNoTracking().Where(x => x.Name.ToLower().Contains(domainSearch.Keyword.ToLower()));

        if (!string.IsNullOrEmpty(domainSearch.SortColumn))
        {
            query = await query.Sort(domainSearch.SortColumn, domainSearch.SortDirection);
        }
        IList<Domain> resultList = await query
            .Skip(domainSearch.Page * domainSearch.PageSize)
            .Take(domainSearch.PageSize)
            .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
            ? Results.Ok(mapper.Map<IList<DtoDomain>>(resultList))
            : Results.NoContent();
    }

}