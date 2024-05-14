using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using amorphie.core.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using amorphie.core.Swagger;
using Microsoft.OpenApi.Models;
using amorphie.core.Extension;
using amorphie.core.Module.minimal_api;
using amorphie.core.Identity;
using FluentValidation;
using amorphie.tag.Validator;

namespace amorphie.tag.Module;

public class TagRelationModule : BaseBBTRoute<DtoTagRelation, TagRelation, TagDBContext>
{
    public TagRelationModule(WebApplication app)
        : base(app) { }

    public override string[]? PropertyCheckList => new string[] { "Name", "Status", "url" };

    public override string? UrlFragment => "TagRelation";

    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("/getTagRelation/{TagRelationName}", getTagRelation);
        routeGroupBuilder.MapGet("/search", SearchMethod);
    }

    async ValueTask<IResult> getTagRelation(
         [FromRoute(Name = "TagRelationName")] string TagRelationName,
         [FromServices] TagDBContext context,
         IMapper mapper
         )
    {
        if (context == null || context.TagRelations == null)
        {
            return Results.NotFound("Context or TagRelations is null.");
        }
        var TagRelation = await context.TagRelations
            .Include(st => st.Tag)
            .FirstOrDefaultAsync(t => t.TagName == TagRelationName);

        var TagRelationDto = mapper.Map<DtoTagRelation>(TagRelation);

        if (TagRelation == null)
            return Results.NotFound();
        return Results.Ok(TagRelationDto);
    }

    protected async ValueTask<IResult> SearchMethod(
        [FromServices] TagDBContext context,
        [FromServices] IMapper mapper,
        [AsParameters] TagRelationSearch TagRelationSearch,
        HttpContext httpContext,
        CancellationToken token
    )
    {
        IQueryable<TagRelation> query = context
               .Set<TagRelation>()
               .AsNoTracking().Where(x => EF.Functions.Like(x.TagName, $"%{TagRelationSearch.Keyword}%"));

        if (!string.IsNullOrEmpty(TagRelationSearch.SortColumn))
        {
            query = await query.Sort(TagRelationSearch.SortColumn, TagRelationSearch.SortDirection);
        }
        IList<TagRelation> resultList = await query
            .Skip(TagRelationSearch.Page * TagRelationSearch.PageSize)
            .Take(TagRelationSearch.PageSize)
            .ToListAsync(token);

        return (resultList != null && resultList.Count > 0)
            ? Results.Ok(mapper.Map<IList<DtoTagRelation>>(resultList))
            : Results.NoContent();
    }
    protected override async ValueTask<IResult> UpsertMethod(
     [FromServices] IMapper mapper,
     [FromServices] IValidator<TagRelation> validator,
     [FromServices] TagDBContext context,
     [FromServices] IBBTIdentity bbtIdentity,
     [FromBody] DtoTagRelation data,
     HttpContext httpContext,
     CancellationToken token)
    {
        if (context == null)
        {
            return Results.NotFound("Context is null.");
        }
        var tag = await context.Tags.FindAsync(data.TagId);
        if (tag == null)
        {
            return Results.BadRequest("The specified TagId does not exist.");
        }

        var existingTagRelation = await context.TagRelations
            .FirstOrDefaultAsync(x => x.TagName == data.TagName && x.TagId == data.TagId, token);

        if (existingTagRelation != null)
        {
            // mapper.Map(data, existingTagRelation);
            existingTagRelation.ModifiedAt = DateTime.UtcNow;
            existingTagRelation.TagId = data.TagId;
            existingTagRelation.OwnerName = data.OwnerName;

            context.TagRelations.Update(existingTagRelation);
        }
        else
        {
            existingTagRelation = mapper.Map<TagRelation>(data);
            context.TagRelations.Add(existingTagRelation);
        }

        await context.SaveChangesAsync();
        var tagRelationDto = mapper.Map<DtoTagRelation>(existingTagRelation);
        return Results.Ok(tagRelationDto);
    }
}
