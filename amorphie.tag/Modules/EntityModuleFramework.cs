
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;


public sealed class EntityModuleFrameWorkModule : BaseEntityModule<DtoEntity, Entity, EntityValidator>
{
    public EntityModuleFrameWorkModule(WebApplication app) : base(app)
    {
    }


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("getEntity/{domainName}/{entityName}", getEntity); // getEntity with DomainName
        routeGroupBuilder.MapDelete("deleteEntity/{domainName}/{entityName}", deleteEntity); // Delete Entity with DomainId and EntityId
        routeGroupBuilder.MapPost("saveEntityWithWorkflow", saveEntityWithWorkflow); // Save Entity with Workflow
    }
 async ValueTask<IResult> saveEntityWithWorkflow(
    [FromBody] DtoPostEntityWorkflow data,
    [FromServices] TagDBContext context,
    IMapper mapper,
    CancellationToken cancellationToken
){
    if (context == null || context.Entities == null)
    {
        return Results.NotFound("Context or Tags is null.");
    }
    
    var existingRecord = await context.Entities.FirstOrDefaultAsync(t => t.Id == data.recordId, cancellationToken);

      if (existingRecord == null)
    {
        var alreadyHasRecord = await context.Entities.FirstOrDefaultAsync(t => t.Name == data.entityData!.Name, cancellationToken);
        if (alreadyHasRecord != null)
        {
            return Results.BadRequest("Already has " + data.entityData!.Name + " entity");
        }
        var entity = mapper.Map<Entity>(data.entityData!);

        entity.CreatedAt = DateTime.UtcNow;
        context.Entities.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return Results.Ok(entity);
    }
  else
    {
        // Apply update to only changed fields.
        if (SaveEntityUpdate(data.entityData!, existingRecord))
        {
            await context!.SaveChangesAsync(cancellationToken);
        }
        
        return Results.Ok();
    }
}
    static bool SaveEntityUpdate(DtoEntity data, Entity existingRecord)
    {
        var hasChanges = false;
        // Apply update to only changed fields.
        if (data.Name != null && data.Name != existingRecord.Name)
        {
            existingRecord.Name = data.Name;
            hasChanges = true;
        }
        if (data.Description != null && data.Description != existingRecord.Description)
        {
            existingRecord.Description = data.Description;
            hasChanges = true;
        }
        

        return hasChanges;
    }


    #region  getEntity
    /// <summary> Entity tablosunda ki domainId ve entityId parametlerine sahip spesifik bir entity kaydı getirir. </summary>
    async ValueTask<IResult> getEntity(
        [FromRoute(Name = "domainName")] string domainName,
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context

    )
    {

        if (context == null || context.Entities == null)
        {
            return Results.NotFound("Context or Entities is null.");
        }
        var entity = await context.Entities
            .Include(e => e.Data)
            .ThenInclude(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(e => e.Name == entityName && e.DomainName == domainName)
            .FirstOrDefaultAsync();


        // Eğer sorgu sonucu kayıt varsa Response Modeller ile kayıtlar döndürülür.
        if (entity != null)
        {
            return Results.Ok(
                new GetEntityResponse(
                    entity.Name,
                    entity.Description!,
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

    /// <summary> Entity tablosundan entityId ve domainId göre kayıt siler. </summary>
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
                .Include(e => e.Data)
                .FirstOrDefaultAsync();

            if (entityToDelete != null)
            {
                context.Entities.Remove(entityToDelete);
                context.EntityData!.RemoveRange(entityToDelete.Data);
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
}