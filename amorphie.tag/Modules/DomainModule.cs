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
        routeGroupBuilder.MapPost("saveDomainWithWorkflow", saveDomainWithWorkflow);
        routeGroupBuilder.MapGet("{domainName}/{entityName}", getEntity);
        routeGroupBuilder.MapGet("/search", SearchMethod);

    }
    async ValueTask<IResult> saveDomainWithWorkflow(
     [FromBody] DtoPostDomainWorkflow data,
     [FromServices] TagDBContext context,
     IMapper mapper,
     CancellationToken cancellationToken
 )
    {
        if (context == null || context.Domains == null)
        {
            return Results.NotFound("Context or Tags is null.");
        }

        var existingRecord = await context.Domains.FirstOrDefaultAsync(t => t.Id == data.recordId, cancellationToken);


        if (existingRecord == null)
        {
            var alreadyHasRecord = await context.Domains.FirstOrDefaultAsync(t => t.Name == data.entityData!.Name, cancellationToken);
            if (alreadyHasRecord != null)
            {
                return Results.BadRequest("Already has " + data.entityData!.Name + " domain");
            }

            var domain = mapper.Map<Domain>(data.entityData!);

            domain.CreatedAt = DateTime.UtcNow;
            context.Domains.Add(domain);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Ok(domain);
        }
        else
        {
            if (SaveDomainUpdate(data.entityData!, existingRecord))
            {
                await context!.SaveChangesAsync(cancellationToken);
            }

            return Results.Ok();
        }
    }
    static bool SaveDomainUpdate(DtoDomain data, Domain existingRecord)
    {
        var hasChanges = false;
        if (data.Description != null && data.Description != existingRecord.Description)
        {
            existingRecord.Description = data.Description;
            hasChanges = true;
        }
        if (data.Name != null && data.Name != existingRecord.Name)
        {
            existingRecord.Name = data.Name;
            hasChanges = true;
        }
        if (hasChanges)
        {
            existingRecord.ModifiedAt = DateTime.UtcNow;
        }


        return hasChanges;
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