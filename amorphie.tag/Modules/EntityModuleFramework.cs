
using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
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
    }


    #region  getEntity
    /// <summary> Entity tablosunda ki domainId ve entityId parametlerine sahip spesifik bir entity kaydı getirir. </summary>
    async ValueTask<IResult> getEntity(
        [FromRoute(Name = "domainName")] string domainName,
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context

    )
    {
        var entity = await context!.Entities!
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
    async ValueTask<IResult> deleteEntity(
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
    }
}