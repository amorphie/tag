using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.core.Module.minimal_api;
using amorphie.tag.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using amorphie.core.Swagger;
using Microsoft.OpenApi.Models;
using amorphie.tag.data;
using amorphie.core.Extension;


namespace amorphie.tag.Module;

public class TagModule : BaseBBTRoute<DtoTag, Tag, TagDBContext>
{
    public TagModule(WebApplication app)
        : base(app) { }

    public override string[]? PropertyCheckList => new string[] { "Name", "Status", "url" };

    public override string? UrlFragment => "tag";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("/getTag/{tagName}", getTag);
        routeGroupBuilder.MapPost("/tag", saveTagWithWorkflow);

        routeGroupBuilder.MapGet("/search", SearchMethod);
    }

    async ValueTask<IResult> getTag(
         [FromRoute(Name = "tagName")] string tagName,
         [FromServices] TagDBContext context,
         IMapper mapper
         )
    {
        if (context == null || context.Tags == null)
        {
            return Results.NotFound("Context or Tags is null.");
        }
        var tag = await context.Tags
            .Include(st => st.TagsRelations)
            .FirstOrDefaultAsync(t => t.Name == tagName);

        var tagDto = mapper.Map<DtoTag>(tag);

        if (tag == null)
            return Results.NotFound();
        return Results.Ok(tagDto);
    }
    async ValueTask<IResult> saveTagWithWorkflow(
    [FromBody] DtoPostWorkflow data,
    [FromServices] TagDBContext context,
    IMapper mapper,
    CancellationToken cancellationToken
)
    {
        if (context == null || context.Tags == null)
        {
            return Results.NotFound("Context or Tags is null.");
        }

        var existingRecord = await context.Tags.FirstOrDefaultAsync(t => t.Id == data.recordId, cancellationToken);


        if (existingRecord == null)
        {
            var alreadyHasRecord = await context.Tags.FirstOrDefaultAsync(t => t.Name == data.entityData!.Name, cancellationToken);
            if (alreadyHasRecord != null)
            {
                return Results.BadRequest("Already has " + data.entityData!.Name + " tag");
            }

            var tag = mapper.Map<Tag>(data.entityData!);

            tag.CreatedDate = DateTime.UtcNow;
            context.Tags.Add(tag);
            await context.SaveChangesAsync(cancellationToken);
            return Results.Ok(tag);
        }
        else
        {
            // Apply update to only changed fields.
            if (SaveTagUpdate(data.entityData!, existingRecord))
            {
                await context!.SaveChangesAsync(cancellationToken);
            }

            return Results.Ok();
        }
    }
    static bool SaveTagUpdate(DtoSaveTagRequest data, Tag existingRecord)
    {
        var hasChanges = false;
        // Apply update to only changed fields.
        if (data.Url != null && data.Url != existingRecord.Url)
        {
            existingRecord.Url = data.Url;
            hasChanges = true;
        }
        if (data.Name != null && data.Name != existingRecord.Name)
        {
            existingRecord.Name = data.Name;
            hasChanges = true;
        }
        if (hasChanges)
        {
            existingRecord.LastModifiedDate = DateTime.Now.ToUniversalTime();
        }
        if (data.Ttl != null && data.Ttl != existingRecord.Ttl)
        {
            existingRecord.Ttl = data.Ttl.Value;
            hasChanges = true;
        }

        return hasChanges;
    }



    protected async ValueTask<IResult> SearchMethod(
        [FromServices] TagDBContext context,
        [FromServices] IMapper mapper,
        [AsParameters] TagSearch tagSearch,
        HttpContext httpContext,
        CancellationToken token
    )
    {
        IQueryable<Tag> query = context
               .Set<Tag>()
               .AsNoTracking().Where(x => EF.Functions.Like(x.Name, $"%{tagSearch.Keyword}%"));

        if (!string.IsNullOrEmpty(tagSearch.SortColumn))
        {
            query = await query.Sort(tagSearch.SortColumn, tagSearch.SortDirection);
        }
        IList<Tag> resultList = await query
            .Skip(tagSearch.Page * tagSearch.PageSize)
            .Take(tagSearch.PageSize)
            .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
            ? Results.Ok(mapper.Map<IList<DtoTag>>(resultList))
            : Results.NoContent();

    }
}