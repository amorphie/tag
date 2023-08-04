
using amorphie.core.Base;

using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;

public sealed class TagFrameworkModule : BaseTagModule<DtoTag, Tag, TagValidator>
{
    public TagFrameworkModule(WebApplication app) : base(app)
    {
    }

   


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("getTag/{tagName}", getTag);
        routeGroupBuilder.MapPost("workflowStatus", saveTagWithWorkflow);

    }
    async ValueTask<IResult> getTag(
      [FromRoute(Name = "tagName")] string tagName,
      [FromServices] TagDBContext context
      )
    {

        var tag = await context!.Tags!
            .Include(st => st.TagsRelations)
            .FirstOrDefaultAsync(t => t.Name == tagName);

        var tagDto = ObjectMapper.Mapper.Map<DtoTag>(tag);

        if (tag == null)
            return Results.NotFound();
        return Results.Ok(tagDto);
    }
     async   ValueTask<IResult> saveTagWithWorkflow(
        [FromBody] DtoPostWorkflow data,
        [FromServices] TagDBContext context,
        CancellationToken cancellationToken
        )
    {

        var existingRecord = await context!.Tags!.FirstOrDefaultAsync(t => t.Id == data.recordId);


        if (existingRecord == null)
        {
            var alreadyHasRecord =await context!.Tags!.FirstOrDefaultAsync(t => t.Name == data.entityData!.Name);
            if(alreadyHasRecord!=null)
            {
                return Results.BadRequest("Already has " + data.entityData!.Name + " tag");
            }
            var tag = ObjectMapper.Mapper.Map<Tag>(data.entityData!);
            
            tag.CreatedDate = DateTime.UtcNow;
            context!.Tags!.Add(tag);
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
            existingRecord.LastModifiedDate = DateTime.Now.ToUniversalTime();
        }
        if (data.Ttl != null && data.Ttl != existingRecord.Ttl)
        {
            existingRecord.Ttl = data.Ttl.Value;
            hasChanges = true;
        }
        if (data.Name != null && data.Name != existingRecord.Name)
        {
            existingRecord.Name = data.Name;
            existingRecord.LastModifiedDate = DateTime.Now.ToUniversalTime();
            hasChanges = true;
        }

        if (hasChanges)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    
}