using amorphie.core.Module.minimal_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.OpenApi.Models;
using amorphie.tag.data;
using System.Text.Json;
using System.Text.Json.Serialization;
using amorphie.core.Base;

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
            MaxDepth=3
            
        };
        var response = mapper.Map<EntityAllDto>(entity);
        var serialezedResponse = JsonSerializer.Serialize(response,options);
        return Results.Ok(response);
    }
    else
    {
        return Results.NotFound();
    }
}
}


    // protected async ValueTask<IResult> SearchMethod(
    //     [FromServices] DomainDbContext context,
    //     [FromServices] IMapper mapper,
    //     [AsParameters] DomainModuleSearch userSearch,
    //     HttpContext httpContext,
    //     CancellationToken token
    // )
    // {
    //     IList<DomainModule> resultList = await context
    //         .Set<DomainModule>()
    //         .AsNoTracking()
    //         .Where(
    //             x =>
    //                 x.FirstMidName.Contains(userSearch.Keyword!)
    //                 || x.LastName.Contains(userSearch.Keyword!)
    //         )
    //         .Skip(userSearch.Page)
    //         .Take(userSearch.PageSize)
    //         .ToListAsync(token);

    //     return (resultList != null && resultList.Count > 0)
    //         ? Results.Ok(mapper.Map<IList<DomainModuleDTO>>(resultList))
    //         : Results.NoContent();
    // }

