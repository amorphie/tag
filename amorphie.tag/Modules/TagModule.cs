using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;
using AutoMapper;
using Result = amorphie.core.Base.Result;

public static class TagModule
{
    public static void MapTagEndpoints(this WebApplication app)
    {
        app.MapGet("/tag", getAllTags)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Returns saved tag records.";
            operation.Parameters[0].Description = "Filtering parameter. Given **tag** is used to filter tags.";
            operation.Parameters[1].Description = "Paging parameter. **limit** is the page size of resultset.";
            operation.Parameters[2].Description = "Paging parameter. **Token** is returned from last query.";
            return operation;
        })
        .Produces<GetTagResponse[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);

        app.MapGet("/tag/{tagName}", getTag)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Returns requested tag.";
            operation.Parameters[0].Description = "Name of the requested tag.";
            return operation;
        })
        .Produces<GetTagResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        app.MapPost("/tag/{ownerName}/tags", addTagToTag)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Add tag to tag :)";
            operation.Parameters[0].Description = "Tag name";
            return operation;
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status304NotModified)
        .Produces(StatusCodes.Status404NotFound);

        app.MapDelete("/tag/{tagName}/tags/{tagNameToDelete}", deleteTagFromTag)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Delete tag from tag ";
            operation.Parameters[0].Description = "Tag name";
            operation.Parameters[1].Description = "Tag name to be deleted.";
            return operation;
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status304NotModified)
        .Produces(StatusCodes.Status404NotFound);


        app.MapPost("/tag", saveTag)
        .WithTopic("pubsub", "SaveTag") // Default topic for bulk save requirement
        .WithOpenApi(operation =>
        {
            operation.Summary = "Saves or updates requested tag.";
            return operation;
        })
        .Produces<GetTagResponse>(StatusCodes.Status200OK);

        app.MapDelete("/tag/{tagName}", deleteTag)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Deletes existing tag.";
            return operation;
        })
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status200OK);


    }

    static async ValueTask<IResult> getAllTags(
    [FromServices] TagDBContext context,
    [FromQuery] string? tagName,
    [FromQuery][Range(0, 100)] int page = 0,
    [FromQuery][Range(5, 100)] int pageSize = 100
    )
    {
        var result = new List<GetTagResponse>();
        var queryAfterWhere = new List<Tag>();
        var query = context!.Tags!
            .Include(t => t.TagsRelations)
            .Skip(page * pageSize).Take(pageSize)
            .AsQueryable();

        if (!string.IsNullOrEmpty(tagName))
        {
            queryAfterWhere = await query.Where(t => t.TagsRelations.Any(c => c.TagName == tagName)).ToListAsync();
        }
        else
        {
            queryAfterWhere = await query.ToListAsync();
        }

        foreach (var item in queryAfterWhere)
        {
            result.Add(new GetTagResponse(item.Name, item.Url, item.Ttl, item.TagsRelations.Select(i => i.TagName).ToArray()));
        }
        if (result.Count == 0)
            return Results.NotFound(new Response { Result = new Result(Status.Error, "Not Found") });

        return Results.Ok(new Response { Result = new Result(Status.Success, "Tags Listed"), Data = result });
    }

    // static IDataResult<GetTagResponse> getTag(
    //     [FromRoute(Name = "tagName")] string tagName,
    //     [FromServices] TagDBContext context
    //     )
    // {

    //     var tag = context!.Tags!
    //         .Include(st => st.TagsRelations)
    //         .FirstOrDefault(t => t.Name == tagName);

    //     if (tag == null)
    //         return new ErrorDataResult<GetTagResponse>("No Tag found");
    //     var result = new GetTagResponse(tag.Name, tag.Url, tag.Ttl, tag.TagsRelations.Select(i => i.TagName).ToArray());

    //     return new SuccessDataResult<GetTagResponse>(result, "Tag found");
    // }
    static IResult getTag(
      [FromRoute(Name = "tagName")] string tagName,
      [FromServices] TagDBContext context,
      [FromServices] IMapper mapper
      )
    {

        var tag = context!.Tags!
            .Include(st => st.TagsRelations)
            .FirstOrDefault(t => t.Name == tagName);

        //var tagDto = ObjectMapper.Mapper.Map<DtoTag>(tag);
        var tagDto = mapper.Map<DtoTag>(tag);

        if (tag == null)
            return Results.NotFound(new Response<DtoTag> { Result = new Result(Status.Error, "Tag Not Found") });
        return Results.Ok(new Response<DtoTag> { Data = tagDto, Result = new Result(Status.Success, "Record Found") });
    }

    // static IDataResult<Tag> saveTag(
    //     [FromBody] SaveTagRequest data,
    //     [FromServices] TagDBContext context
    //     )
    // {

    //     var existingRecord = context?.Tags?.FirstOrDefault(t => t.Name == data.Name);

    //     if (existingRecord == null)
    //     {
    //         var savedData = new Tag { Name = data.Name, Url = data.Url, Ttl = data.Ttl ?? 0, CreatedDate = DateTime.UtcNow };
    //         context!.Tags!.Add(savedData);
    //         context.SaveChanges();
    //         // return Results.Created($"/tag/{data.Name}", existingRecord);
    //         return new SuccessDataResult<Tag>(savedData, "Saved");
    //     }
    //     else
    //     {
    //         var hasChanges = false;
    //         // Apply update to only changed fields.
    //         if (data.Url != null && data.Url != existingRecord.Url) { existingRecord.Url = data.Url; hasChanges = true; existingRecord.LastModifiedDate = DateTime.Now.ToUniversalTime(); }
    //         if (data.Ttl != null && data.Ttl != existingRecord.Ttl) { existingRecord.Ttl = data.Ttl.Value; hasChanges = true; }

    //         if (hasChanges)
    //         {
    //             context!.SaveChanges();
    //             return new SuccessDataResult<Tag>(existingRecord, "Updated");
    //         }
    //         else
    //         {
    //             return new ErrorDataResult<Tag>(existingRecord, "No changes");
    //         }
    //     }
    // }

    static IResult saveTag(
        [FromBody] DtoSaveTagRequest data,
        [FromServices] TagDBContext context,
        [FromServices] IMapper mapper
        )
    {

        var existingRecord = context?.Tags?.FirstOrDefault(t => t.Name == data.Name);


        if (existingRecord == null)
        {
            // var tag = ObjectMapper.Mapper.Map<Tag>(data);
            var tag = mapper.Map<Tag>(data);
            tag.CreatedDate = DateTime.UtcNow;
            context!.Tags!.Add(tag);
            context.SaveChanges();
            //return new SuccessDataResult<DtoTag>(ObjectMapper.Mapper.Map<DtoTag>(tag), "Saved");
            // return new Response<DtoTag> { Data = ObjectMapper.Mapper.Map<DtoTag>(tag), Result = new amorphie.core.Base.Result(Status.Success, "Kaydedildi") };
            return Results.Ok(new Response<DtoTag> { Data = mapper.Map<DtoTag>(tag), Result = new Result(Status.Success, "Tag Saved") });
        }
        else
        {
            // Apply update to only changed fields.
            if (SaveTagUpdate(data, existingRecord))
            {
                context!.SaveChanges();
                //return new SuccessDataResult<DtoTag>(ObjectMapper.Mapper.Map<DtoTag>(existingRecord), "Updated");
                return Results.Ok(new Response<DtoTag> { Data = mapper.Map<DtoTag>(existingRecord), Result = new Result(Status.Success, "Update Başarili") });

            }
        }
        return Results.BadRequest(new Response<DtoTag> { Data = mapper.Map<DtoTag>(existingRecord), Result = new Result(Status.Error, "Değişiklik yok") });
    }

    static IResult deleteTag(
        [FromRoute(Name = "tagName")] string tagName,
        [FromServices] TagDBContext context)
    {

        var existingRecord = context?.Tags?.FirstOrDefault(t => t.Name == tagName);

        if (existingRecord == null)
        {
            //return new ErrorDataResult<Tag>(existingRecord!, "No Tag found");

            // return Results.NotFound( NoDataResponse { Result = new Result(Status.Error, "No Tag found") });
            return Results.NotFound(new Response { Result = new Result(Status.Error, "No Tag Found") });

        }

        else
        {
            context!.Remove(existingRecord);
            context.SaveChanges();
            //return new SuccessDataResult<Tag>(existingRecord, "Deleted");
            return Results.Ok(new Response { Result = new Result(Status.Success, "Tag Deleted"), Data = existingRecord });
        }
    }

    //Tag'e tag relation'ı eklemek için kullanılır. Method ismi,parametre isimleri ve açıklaması değiştirilebilir?
    static IResult addTagToTag(
        [FromRoute(Name = "ownerName")] string ownerName,
        [FromBody] string tagNameToAdd,
        [FromServices] TagDBContext context,
        IMapper mapper
        )
    {
        var tag = context!.Tags!
             .Include(t => t.TagsRelations)
             .FirstOrDefault(t => t.Name == ownerName);


        if (tag == null)
            return Results.NotFound(new Response<DtoTag> { Result = new Result(Status.Error, "Tag Not Found") });

        var tagToAdd = context!.Tags!.FirstOrDefault(t => t.Name == tagNameToAdd);
        if (tagToAdd == null)
            return Results.NotFound(new Response<DtoTag> { Result = new Result(Status.Error, "Tag To Add Is Not Found") });

        if (tag.TagsRelations.Any(t => t.TagName == tagNameToAdd))
            return Results.BadRequest(new Response<DtoTag> { Result = new Result(Status.Error, "Not Modified Tag Already Assigned") });
        tag.TagsRelations.Add(new TagRelation { TagName = tagNameToAdd, OwnerName = ownerName });
        // var result = tag.TagsRelations.FirstOrDefault(t => t.TagName == tagNameToAdd && t.OwnerName == ownerName);
        context.SaveChanges();
        var result = context!.Tags!.Include(t => t.TagsRelations).FirstOrDefault(t => t.TagsRelations.Any(x => x.TagName == tagNameToAdd));

        // return new SuccessDataResult<DtoTag>(ObjectMapper.Mapper.Map<DtoTag>(result)!, "Added");
        return Results.Ok(new Response<DtoTag> { Data = mapper.Map<DtoTag>(result), Result = new Result(Status.Success, "Tag To Add Saved") });
    }

    static IResult deleteTagFromTag(
        [FromRoute(Name = "tagName")] string tagName,
        [FromRoute(Name = "tagNameToDelete")] string tagNameToDelete,
        [FromServices] TagDBContext context
        )
    {
        var tag = context!.Tags!
            .Include(t => t.TagsRelations)
            .FirstOrDefault(t => t.Name == tagName);

        if (tag == null)
            // return new ErrorResult("Tag is not found");
            return Results.NotFound(new Response { Result = new Result(Status.Error, "Tag is not found") });

        var tagToDelete = context!.TagRelations!.FirstOrDefault(t => t.TagName == tagNameToDelete);


        if (tagToDelete == null)
            return Results.NotFound(new Response { Result = new Result(Status.Error, "Tag to delete is not found") });

        context.Remove(tagToDelete);
        context.SaveChanges();
        return Results.Ok(new Response { Result = new Result(Status.Success, "Deleted") });

    }



    static bool SaveTagUpdate(DtoSaveTagRequest data, Tag existingRecord)
    {
        var hasChanges = false;
        // Apply update to only changed fields.
        if ((data.Url != null && data.Url != existingRecord.Url) || (data.Ttl != null && data.Ttl != existingRecord.Ttl))
        {
            if (data.Url != null && data.Url != existingRecord.Url)
            {
                existingRecord.Url = data.Url;
            }
            if (data.Ttl != null && data.Ttl != existingRecord.Ttl)
            {
                existingRecord.Ttl = data.Ttl.Value;
            }
            hasChanges = true;
            existingRecord.LastModifiedDate = DateTime.Now.ToUniversalTime();
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