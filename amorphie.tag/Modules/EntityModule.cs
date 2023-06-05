using amorphie.core.Base;
using amorphie.core.Enums;
using amorphie.core.IBase;
public static class EntityModule
{

    public static void MapEntityEndpoints(this WebApplication app)
    {
        // app.MapPost("/saveEntityWithData", saveEntityWithData).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Saves or updates requested entity.";
        //     return operation;
        // }).Produces<SaveEntityRequest>(StatusCodes.Status200OK);

        // app.MapGet("/entity/{domainId}/{entityId}", getEntity).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Returns requested entity";
        //     return operation;

        // }).Produces<GetEntityResponse>(StatusCodes.Status200OK)
        // .Produces(StatusCodes.Status204NoContent);

        // app.MapDelete("/entity/{domainId}/{entityId}", deleteEntity).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Deletes requested entity";
        //     return operation;

        // }).Produces(StatusCodes.Status200OK);

        // // app.MapPost("/entity/{entityData}", saveEntityData).WithOpenApi(operation =>
        // // {
        // //     operation.Summary = "Saves or updates requested entity data.";
        // //     return operation;
        // // }).Produces<SaveEntityDataRequest>(StatusCodes.Status200OK);

        // app.MapGet("/getEntityData/{entityId}/{fieldName}", getEntityData).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Returns requested entity data with entity data source";
        //     operation.Parameters[0].Description = "Entity name";
        //     operation.Parameters[1].Description = "Field Name";
        //     return operation;

        // }).Produces<GetEntityDataResponse>(StatusCodes.Status200OK);

        // app.MapGet("/getAllEntityData/{entityId}", getAllEntityData).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Returns requested entity data with entity data source";
        //     operation.Parameters[0].Description = "Entity name";
        //     return operation;

        // }).Produces<GetEntityDataResponse>(StatusCodes.Status200OK);

        // app.MapDelete("/deleteEntityData/{entityId}/{fieldName}", deleteEntityData).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Deletes requested entity data";
        //     return operation;

        // }).Produces(StatusCodes.Status200OK);
        // app.MapPost("/saveEntityDataSource/{Id}", saveEntityDataSource).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Saves or updates requested entity data source.";
        //     operation.Parameters[0].Description = "Entity data Id";
        //     return operation;
        // }).Produces<SaveEntityDataSourcesRequest>(StatusCodes.Status200OK);
        // app.MapPost("/saveEntity", saveEntity).WithOpenApi(operation =>
        // {
        //     operation.Summary = "Saves or updates requested entity.";
        //     return operation;
        // }).Produces<SaveEntityRequest>(StatusCodes.Status200OK);

    }

    // #region  saveEntity
    // async static Task<IResult> saveEntity([FromServices] TagDBContext context, [FromBody] SaveEntityRequest request)
    // {
    //     // Requestten gelen Entity name'e göre Tabloda aynı Entity name ve domaine ait kayıt var mı kontrol edilir.
    //     var existingRecord = context?.Entities?.FirstOrDefault(d => d.Name == request.Name && d.DomainId == request.DomainId);
    //     // Eğer kayıt yoksa yeni kayıt oluşturulur.
    //     if (existingRecord == null)
    //     {
    //         context!.Entities!.Add(new Entity
    //         {
    //             Name = request.Name,
    //             Description = request!.Description!,
    //             DomainId = request.DomainId,
    //             CreatedAt = DateTime.Now.ToUniversalTime()
    //         });
    //         await context!.SaveChangesAsync();
    //         return Results.Ok();
    //     }
    //     // Eğer kayıt varsa ve description değişmişse description güncellenir.
    //     else
    //     {
    //         if (await context!.Entities!.Where(x => x.Description != request.Description && x.Name == request.Name && request.Description != null).ExecuteUpdateAsync
    //         (x => x.SetProperty(y => y.Description, request.Description)
    //         .SetProperty(y => y.ModifiedAt, DateTime.Now.ToUniversalTime())
    //         ) == 1)
    //         {
    //             await context!.SaveChangesAsync();
    //             return Results.Ok();
    //         }
    //         else
    //         {
    //             return Results.Problem("Not Modified.", null, 304);
    //         }

    //     }
    // }

    // #endregion

    #region saveEntityWithEntityDataAndEntitySource
    /// <summary> saveEntity Entity yoksa oluşturma, entity mevcut ve description değişmişse güncellemek için kullanılır. </summary>
    // async static Task<IResult> saveEntityWithData([FromServices] TagDBContext context, [FromBody] Test request)
    // {
    //     // Requestten gelen Entity name'e göre Tabloda aynı Entity name ve domaine ait kayıt var mı kontrol edilir.
    //     // var existingRecord = context?.Entities?.FirstOrDefault(d => d.Name == request.Name && d.domainId == request.domainId);
    //     var addedData = context!.Entities!.Include(x => x.Data)
    //     .ThenInclude(x => x.Sources)
    //     .Where(x => x.Name == request.Name && x.DomainId == request.DomainId).FirstOrDefault();
    //     // Eğer kayıt yoksa yeni kayıt oluşturulur. Kayıtla birlikte Data Response model ile Data ve DataSources kayıtları da oluşturulur.
    //     if (addedData == null && request.Data != null)
    //     {
    //         context!.Entities!.Add(new Entity
    //         {
    //             Name = request.Name,
    //             Description = request.Description,
    //             DomainId = request.DomainId,
    //             CreatedAt = DateTime.Now.ToUniversalTime(),
    //             Data = request!.Data!.Select(x => new EntityData
    //             {
    //                 Field = x.Field,
    //                 Ttl = x.Ttl,
    //                 Id = Guid.NewGuid(),
    //                 EntityId = x.EntityId,
    //                 CreatedAt = (DateTime)x.CreatedAt,
    //                 Sources = x!.Source!.Select(y => new EntityDataSource
    //                 {
    //                     Order = y.Order,
    //                     TagName = y.TagName,
    //                     DataPath = y.DataPath
    //                 }).ToList()
    //             }).ToList()
    //         });
    //         context.SaveChanges();
    //         return Results.Ok();
    //     }
    //     if (addedData == null && request.Data == null)
    //     {
    //         context!.Entities!.Add(new Entity
    //         {
    //             Name = request.Name,
    //             Description = request.Description,
    //             DomainId = request.DomainId,
    //             CreatedAt = DateTime.Now.ToUniversalTime(),
    //         });
    //         context.SaveChanges();
    //         return Results.Ok();
    //     }
    //     //Eğer entityId var ve Description değişmişse Description güncellenir.
    //     else
    //     {
    //         if (await context!.Entities!.Where(x => x.Description != request.Description && x.Name == request.Name && request.Description != null).ExecuteUpdateAsync
    //         (x => x.SetProperty(y => y.Description, request.Description)
    //         .SetProperty(y => y.ModifiedAt, DateTime.Now.ToUniversalTime())
    //         ) == 1)
    //         {
    //             context.SaveChanges();
    //             return Results.Ok();
    //         }
    //         //Eğer değişiklik yoksa 304 döner.
    //         else
    //         {
    //             return Results.Problem("Not Modified.", null, 304);
    //         }

    //     }
    // }
    #endregion

}

#region SaveEntityData
///<summary> EntityData tablosuna kayıt ekler veya günceller. </summary>
// static IResult saveEntityData([FromServices] TagDBContext context, [FromBody] SaveEntityDataRequest request)
// {
//     //EntityData tablosunda aynı entityId ve Field bilgilerine sahip kayıt var mı kontrol edilir.
//     var entityData = context!.EntityData!.Include(d => d.Sources)
//         .Where(d => d.EntityId == request.EntityId && d.Field == request.Field)
//         .FirstOrDefault();
//     //var updatedData = context!.EntityData!.Include(d => d.Sources).Where(x => x.Id == request.Id).FirstOrDefault();
//     //Sorgu sonucu kayıt yoksa yeni kayıt oluşturulur.
//     if (entityData == null)
//     {
//         context.EntityData!.Add(new EntityData
//         {
//             EntityId = request.EntityId,
//             Field = request.Field,
//             Ttl = request.Ttl,
//             CreatedAt = DateTime.Now.ToUniversalTime(),
//             Sources = request!.Source!.Select(s => new EntityDataSource
//             {
//                 Order = s.Order,
//                 TagName = s.TagName,
//                 DataPath = s.DataPath
//             }).ToList()
//         });
//         context.SaveChanges();
//         return Results.Created($"/entity/{request.Id}/{request.Field}", entityData);
//     }

//     //Sorgu sonucu kayıt varsa 409 Conflict döndürülür.
//     else
//         return Results.Problem("Field already exists.", null, 409);
// }
#endregion

// #region getEntityData
// /// <summary> EntityData tablosunda entityId ve fieldName'ine göre spesifik bir kaydı data source tablosunda ki kayıtlar ile birlikte getirir. </summary>
// static IResult getEntityData(
//     [FromRoute(Name = "entityId")] Guid entityId,
//     [FromRoute(Name = "fieldName")] string? fieldName,
//     [FromServices] TagDBContext context

// )
// {
//     //EntityData tablosunda aynı entityId ve Field bilgilerine sahip kayıt var mı kontrol edilir.
//     var entityData = context!.EntityData!
//         .Include(d => d.Sources)
//         .ThenInclude(s => s.Tag)
//         .Where(d => d.EntityId == entityId || d.Field == fieldName)
//         .FirstOrDefault();
//     //Sorgu sonucu kayıt varsa data source tablosunda ki ilişkili kayıtları ile birlikte kayıt döndürülür.
//     if (entityData != null)
//     {
//         return Results.Ok(
//             new GetEntityDataResponse(
//                 entityData.Field,
//                 entityData.Ttl,
//                 entityData.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
//             ));
//     }
//     //Sorgu sonucu kayıt yoksa 204 No Content döndürülür.
//     else
//         return Results.NoContent();
// }
// #endregion

// #region getAllEntityData
// /// <summary> EntityData tablosundaki istenen kaydı, data source tablosunda ki kayıtları ile birlikte getirir. </summary>
// static IResult getAllEntityData(
//     [FromRoute(Name = "entityId")] Guid entityId,
//     [FromServices] TagDBContext context

// )
// {
//     //Entity Data tablosundaki kayıtların data source tablosundaki kayıtları ile birlikte getirilmesi için, 
//     //Include ve ThenInclude kullanılmıştır.
//     //Entity name parametresi ile sorgu atılır ve sonuç listeye aktarılır.
//     var entityData = context!.EntityData!
//         .Include(d => d.Sources)
//         .ThenInclude(s => s.Tag)
//         .Where(d => d.EntityId == entityId)
//         .ToList();
//     //Sorgu sonucu kayıt varsa kayıtlar döndürülür.
//     if (entityData != null)
//     {
//         return Results.Ok(
//             entityData.Select(d => new GetEntityDataResponse(
//                 d.Field,
//                 d.Ttl,
//                 d.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
//             )).ToArray());
//     }
//     //Sorgu sonucu kayıt yoksa 204 No Content döndürülür.
//     else
//         return Results.NoContent();
// }
// #endregion


// #region deleteEntityData
// /// <summary> EntityData tablosundan kayıt silmek için kullanılır. </summary>
// async static Task<IResult> deleteEntityData(
//     [FromRoute(Name = "entityId")] Guid entityId,
//     [FromRoute(Name = "fieldName")] string fieldName,
//     [FromServices] TagDBContext context

// )
// {
//     //EntityData tablosunda aynı entityId ve Field bilgilerine sahip kayıt var mı kontrol edilir.
//     //Kayıt varsa silinir yoksa 204 No Content döndürülür.
//     if (await context.EntityData!.Where(d => d.EntityId == entityId && d.Field == fieldName).ExecuteDeleteAsync() == 1)
//     {
//         return Results.Ok();
//     }
//     else
//     {
//         return Results.NoContent();
//     }
// }
//     #endregion

#region saveEntityDataSource
/// <summary> EntityData tablosunda kayıtlı olan EntityData'nın Id si kullanılarak Entity Data Source Ekleme işlemi yapılır. </summary>
// static IResult saveEntityDataSource(
//     [FromServices] TagDBContext context,
//     [FromBody] SaveEntityDataSourcesRequest request,
//     [FromRoute(Name = "Id")] Guid Id
// )
// {
//     //EntityData tablosunda parametre olarak gelen Id'ye sahip kayıt var mı kontrol edilir.
//     var entityData = context!.EntityData!.Include(e => e.Sources).Where(e => e.Id == Id).FirstOrDefault();
//     //EntityData mevcutsa EntityDataSource tablosuna kayıt eklenir.
//     //EntityDataSource tablosunda aynı Order ve DataPath bilgilerine sahip kayıt varsa 409 Conflict döndürülür.
//     //EntityData tablosunda parametre olarak gelen Id'ye sahip kayıt yoksa hata döndürülür.
//     if (entityData != null)
//     {
//         if (entityData.Sources.Any(s => s.Order == request.Order && s.DataPath == request.DataPath))
//             return Results.Problem("Order and DataPath already exists.", null, 409);
//         entityData.Sources.Add(new EntityDataSource
//         {
//             Order = request.Order,
//             TagName = request.TagName,
//             DataPath = request.DataPath
//         });
//         context.SaveChanges();
//         return Results.Ok();
//     }
//     else
//         return Results.Problem("EntityData not found.", null, null, "Given Id is not found in EntityData table");
// }
#endregion
