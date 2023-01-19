public static class EntityModule
{

    public static void MapEntityEndpoints(this WebApplication app)
    {
        app.MapPost("/saveEntityWithData", saveEntityWithData).WithOpenApi(operation =>
        {
            operation.Summary = "Saves or updates requested entity.";
            return operation;
        }).Produces<SaveEntityRequest>(StatusCodes.Status200OK);

        app.MapGet("/entity/{domainName}/{entityName}", getEntity).WithOpenApi(operation =>
        {
            operation.Summary = "Returns requested entity";
            return operation;

        }).Produces<GetEntityResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);

        app.MapDelete("/entity/{domainName}/{entityName}", deleteEntity).WithOpenApi(operation =>
        {
            operation.Summary = "Deletes requested entity";
            return operation;

        }).Produces(StatusCodes.Status200OK);

        app.MapPost("/entity/{entityData}", saveEntityData).WithOpenApi(operation =>
        {
            operation.Summary = "Saves or updates requested entity data.";
            return operation;
        }).Produces<SaveEntityDataRequest>(StatusCodes.Status200OK);

        app.MapGet("/getEntityData/{entityName}/{fieldName}", getEntityData).WithOpenApi(operation =>
        {
            operation.Summary = "Returns requested entity data with entity data source";
            operation.Parameters[0].Description = "Entity name";
            operation.Parameters[1].Description = "Field Name";
            return operation;

        }).Produces<GetEntityDataResponse>(StatusCodes.Status200OK);

        app.MapGet("/getAllEntityData/{entityName}", getAllEntityData).WithOpenApi(operation =>
        {
            operation.Summary = "Returns requested entity data with entity data source";
            operation.Parameters[0].Description = "Entity name";
            return operation;

        }).Produces<GetEntityDataResponse>(StatusCodes.Status200OK);

        app.MapDelete("/deleteEntityData/{entityName}/{fieldName}", deleteEntityData).WithOpenApi(operation =>
        {
            operation.Summary = "Deletes requested entity data";
            return operation;

        }).Produces(StatusCodes.Status200OK);
        app.MapPost("/saveEntityDataSource/{Id}", saveEntityDataSource).WithOpenApi(operation =>
        {
            operation.Summary = "Saves or updates requested entity data source.";
            operation.Parameters[0].Description = "Entity data Id";
            return operation;
        }).Produces<SaveEntityDataSourcesRequest>(StatusCodes.Status200OK);
        app.MapPost("/saveEntity", saveEntity).WithOpenApi(operation =>
        {
            operation.Summary = "Saves or updates requested entity.";
            return operation;
        }).Produces<SaveEntityRequest>(StatusCodes.Status200OK);

    }

    #region  saveEntity
    async static Task<IResult> saveEntity([FromServices] TagDBContext context, [FromBody] SaveEntityRequest request)
    {
        // Requestten gelen Entity name'e göre Tabloda aynı Entity name ve domaine ait kayıt var mı kontrol edilir.
        var existingRecord = context?.Entities?.FirstOrDefault(d => d.Name == request.Name && d.DomainName == request.DomainName);
        // Eğer kayıt yoksa yeni kayıt oluşturulur.
        if (existingRecord == null)
        {
            context!.Entities!.Add(new Entity
            {
                Name = request.Name,
                Description = request!.Description!,
                DomainName = request.DomainName,
                CreatedDate = DateTime.Now.ToUniversalTime()
            });
            await context!.SaveChangesAsync();
            return Results.Ok();
        }
        // Eğer kayıt varsa ve description değişmişse description güncellenir.
        else
        {
            if (await context!.Entities!.Where(x => x.Description != request.Description && x.Name == request.Name && request.Description != null).ExecuteUpdateAsync
            (x => x.SetProperty(y => y.Description, request.Description)
            .SetProperty(y => y.LastModifiedDate, DateTime.Now.ToUniversalTime())
            ) == 1)
            {
                await context!.SaveChangesAsync();
                return Results.Ok();
            }
            else
            {
                return Results.Problem("Not Modified.", null, 304);
            }

        }
    }

    #endregion

    #region saveEntityWithEntityDataAndEntitySource
    /// <summary> saveEntity Entity yoksa oluşturma, entity mevcut ve description değişmişse güncellemek için kullanılır. </summary>
    async static Task<IResult> saveEntityWithData([FromServices] TagDBContext context, [FromBody] SaveEntityRequest request)
    {
        // Requestten gelen Entity name'e göre Tabloda aynı Entity name ve domaine ait kayıt var mı kontrol edilir.
        // var existingRecord = context?.Entities?.FirstOrDefault(d => d.Name == request.Name && d.DomainName == request.DomainName);
        var addedData = context!.Entities!.Include(x => x.Data)
        .ThenInclude(x => x.Sources)
        .Where(x => x.Name == request.Name && x.DomainName == request.DomainName).FirstOrDefault();
        // Eğer kayıt yoksa yeni kayıt oluşturulur. Kayıtla birlikte Data Response model ile Data ve DataSources kayıtları da oluşturulur.
        if (addedData == null)
        {
            context!.Entities!.Add(new Entity
            {
                Name = request.Name,
                Description = request.Description,
                DomainName = request.DomainName,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                Data = request!.Data!.Select(x => new EntityData
                {
                    Field = x.Field,
                    Ttl = x.Ttl,
                    Id = x.Id,
                    EntityName = x.EntityName,
                    CreatedDate = x.CreatedDate,
                    Sources = x!.Source!.Select(y => new EntityDataSource
                    {
                        Order = y.Order,
                        TagName = y.TagName,
                        DataPath = y.DataPath
                    }).ToList()
                }).ToList()
            });
            context.SaveChanges();
            return Results.Ok();
        }
        //Eğer EntityName var ve Description değişmişse Description güncellenir.
        else
        {
            if (await context!.Entities!.Where(x => x.Description != request.Description && x.Name == request.Name && request.Description != null).ExecuteUpdateAsync
            (x => x.SetProperty(y => y.Description, request.Description)
            .SetProperty(y => y.LastModifiedDate, DateTime.Now.ToUniversalTime())
            ) == 1)
            {
                context.SaveChanges();
                return Results.Ok();
            }
            //Eğer değişiklik yoksa 304 döner.
            else
            {
                return Results.Problem("Not Modified.", null, 304);
            }

        }
    }
    #endregion

    #region  getEntity
    /// <summary> Entity tablosunda ki domainName ve entityName parametlerine sahip spesifik bir entity kaydı getirir. </summary>
    static IResult getEntity(
        [FromRoute(Name = "domainName")] string domainName,
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context

    )
    {
        // Entity tablosuna entityName ve domainName parametrelerine göre sorgu atılır.
        // Eğer sorgu sonucu kayıt varsa EntityDataSource tablosunda ilişkili kayıtları ile birlikte gelir.
        var entity = context!.Entities!
            .Include(e => e.Data)
            .ThenInclude(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(e => e.Name == entityName && e.DomainName == domainName)
            .FirstOrDefault();


        // Eğer sorgu sonucu kayıt varsa Response Modeller ile kayıtlar döndürülür.
        if (entity != null)
        {
            return Results.Ok(
                new GetEntityResponse(
                    entity.Name,
                    entity.Description,
                    entity.Data.Select(d => new GetEntityDataResponse(d.Field, d.Ttl,
                        d.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                    )).ToArray()
                ));
        }
        // Eğer sorgu sonucu kayıt yoksa 204 döndürülür.
        else
            return Results.NoContent();
    }
    #endregion

    #region deleteEntity
    /// <summary> Entity tablosundan entityName ve domainName göre kayıt siler. </summary>
    async static Task<IResult> deleteEntity(
        [FromRoute(Name = "domainName")] string domainName,
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context

    )
    {
        //Entity tablosundan entityName ve domainName göre kayıt siler.
        //Entity tablosunda entityName ve domainName eşleşen kayıt varsa Entity Data tablosundaki ilişkili kayıtlarınıda siler.
        //Entity tablosunda entityName ve domainName eşleşen kayıt yoksa 204 döndürür.


        //Entity tablosuna sorgu atar entityName ve domainName ile eşleşen kayı varsa siler
        if (await context.Entities!.Where(e => e.Name == entityName && e.DomainName == domainName).ExecuteDeleteAsync() == 1)
        {
            //Entity tablosunda silinen Entity'nin Entity Datada ki kayıtları silinir.
            //Entity tablosunda silinen Entity'nin EntityDataSource tablosundaki kayıtlarıda silinir.
            //Silinmiş olan Entity ile ilgili diğer tablolardaki bütün kayıtlar silinir.?
            await context.EntityData!.Where(d => d.EntityName == entityName).ExecuteDeleteAsync();
            return Results.Ok();
        }
        //Entity tablosunda entityName ve domainName ile eşleşen kayıt yoksa 204 döndürür.
        else
        {
            return Results.NoContent();
        }

        // var entity = context!.Entities!
        //     .Where(e => e.Name == entityName)
        //     .FirstOrDefault();

        // if (entity != null)
        // {
        //     context!.Entities!.Remove(entity);


        //     context.SaveChanges();
        //     return Results.Ok();
        // }
        // else
        //     return Results.NoContent();
    }

    #endregion

    #region SaveEntityData
    ///<summary> EntityData tablosuna kayıt ekler veya günceller. </summary>
    static IResult saveEntityData([FromServices] TagDBContext context, [FromBody] SaveEntityDataRequest request)
    {
        //EntityData tablosunda aynı EntityName ve Field bilgilerine sahip kayıt var mı kontrol edilir.
        var entityData = context!.EntityData!.Include(d => d.Sources)
            .Where(d => d.EntityName == request.EntityName && d.Field == request.Field)
            .FirstOrDefault();
        //var updatedData = context!.EntityData!.Include(d => d.Sources).Where(x => x.Id == request.Id).FirstOrDefault();
        //Sorgu sonucu kayıt yoksa yeni kayıt oluşturulur.
        if (entityData == null)
        {
            context.EntityData!.Add(new EntityData
            {
                EntityName = request.EntityName,
                Field = request.Field,
                Ttl = request.Ttl,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                Sources = request.Source.Select(s => new EntityDataSource
                {
                    Order = s.Order,
                    TagName = s.TagName,
                    DataPath = s.DataPath
                }).ToList()
            });
            context.SaveChanges();
            return Results.Created($"/entity/{request.EntityName}/{request.Field}", entityData);
        }
        // if (updatedData != null && updatedData.Id == request.Id)
        // {
        //     updatedData.Field = request.Field;
        //     updatedData.Ttl = request.Ttl;
        //     updatedData.EntityName = request.EntityName;
        //     updatedData.LastModifiedDate = DateTime.Now.ToUniversalTime();
        //     updatedData.Sources = request.Source.Select(s => new EntityDataSource
        //     {
        //         Order = s.Order,
        //         TagName = s.TagName,
        //         DataPath = s.DataPath
        //     }).ToList();
        //     context!.EntityData!.Update(updatedData);
        //     context.SaveChanges();
        //     return Results.Ok("Updated");
        // }


        //Sorgu sonucu kayıt varsa 409 Conflict döndürülür.
        else
            return Results.Problem("Field already exists.", null, 409);
    }
    #endregion

    #region getEntityData
    /// <summary> EntityData tablosunda entityName ve fieldName'ine göre spesifik bir kaydı data source tablosunda ki kayıtlar ile birlikte getirir. </summary>
    static IResult getEntityData(
        [FromRoute(Name = "entityName")] string entityName,
        [FromRoute(Name = "fieldName")] string? fieldName,
        [FromServices] TagDBContext context

    )
    {
        //EntityData tablosunda aynı EntityName ve Field bilgilerine sahip kayıt var mı kontrol edilir.
        var entityData = context!.EntityData!
            .Include(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(d => d.EntityName == entityName || d.Field == fieldName)
            .FirstOrDefault();
        //Sorgu sonucu kayıt varsa data source tablosunda ki ilişkili kayıtları ile birlikte kayıt döndürülür.
        if (entityData != null)
        {
            return Results.Ok(
                new GetEntityDataResponse(
                    entityData.Field,
                    entityData.Ttl,
                    entityData.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                ));
        }
        //Sorgu sonucu kayıt yoksa 204 No Content döndürülür.
        else
            return Results.NoContent();
    }
    #endregion

    #region getAllEntityData
    /// <summary> EntityData tablosundaki istenen kaydı, data source tablosunda ki kayıtları ile birlikte getirir. </summary>
    static IResult getAllEntityData(
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context

    )
    {
        //Entity Data tablosundaki kayıtların data source tablosundaki kayıtları ile birlikte getirilmesi için, 
        //Include ve ThenInclude kullanılmıştır.
        //Entity name parametresi ile sorgu atılır ve sonuç listeye aktarılır.
        var entityData = context!.EntityData!
            .Include(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(d => d.EntityName == entityName)
            .ToList();
        //Sorgu sonucu kayıt varsa kayıtlar döndürülür.
        if (entityData != null)
        {
            return Results.Ok(
                entityData.Select(d => new GetEntityDataResponse(
                    d.Field,
                    d.Ttl,
                    d.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                )).ToArray());
        }
        //Sorgu sonucu kayıt yoksa 204 No Content döndürülür.
        else
            return Results.NoContent();
    }
    #endregion


    #region deleteEntityData
    /// <summary> EntityData tablosundan kayıt silmek için kullanılır. </summary>
    async static Task<IResult> deleteEntityData(
        [FromRoute(Name = "entityName")] string entityName,
        [FromRoute(Name = "fieldName")] string fieldName,
        [FromServices] TagDBContext context

    )
    {
        //EntityData tablosunda aynı EntityName ve Field bilgilerine sahip kayıt var mı kontrol edilir.
        //Kayıt varsa silinir yoksa 204 No Content döndürülür.
        if (await context.EntityData!.Where(d => d.EntityName == entityName && d.Field == fieldName).ExecuteDeleteAsync() == 1)
        {
            return Results.Ok();
        }
        else
        {
            return Results.NoContent();
        }
    }
    #endregion

    #region saveEntityDataSource
    /// <summary> EntityData tablosunda kayıtlı olan EntityData'nın Id si kullanılarak Entity Data Source Ekleme işlemi yapılır. </summary>
    static IResult saveEntityDataSource(
        [FromServices] TagDBContext context,
        [FromBody] SaveEntityDataSourcesRequest request,
        [FromRoute(Name = "Id")] Guid Id
    )
    {
        //EntityData tablosunda parametre olarak gelen Id'ye sahip kayıt var mı kontrol edilir.
        var entityData = context!.EntityData!.Include(e => e.Sources).Where(e => e.Id == Id).FirstOrDefault();
        //EntityData mevcutsa EntityDataSource tablosuna kayıt eklenir.
        //EntityDataSource tablosunda aynı Order ve DataPath bilgilerine sahip kayıt varsa 409 Conflict döndürülür.
        //EntityData tablosunda parametre olarak gelen Id'ye sahip kayıt yoksa hata döndürülür.
        if (entityData != null)
        {
            if (entityData.Sources.Any(s => s.Order == request.Order && s.DataPath == request.DataPath))
                return Results.Problem("Order and DataPath already exists.", null, 409);
            entityData.Sources.Add(new EntityDataSource
            {
                Order = request.Order,
                TagName = request.TagName,
                DataPath = request.DataPath
            });
            context.SaveChanges();
            return Results.Ok();
        }
        else
            return Results.Problem("EntityData not found.", null, null, "Given Id is not found in EntityData table");
    }
    #endregion
}