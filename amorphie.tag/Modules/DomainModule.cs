
using amorphie.core.Module.minimal_api;
using amorphie.core.Repository;
using amorphie.tag.Modules.Base;
using amorphie.tag.Validator;
using AutoMapper;
using FluentValidation;

public sealed class DomainModule : BaseDomainModule<DtoDomain, Domain, DomainValidator>
{
    public DomainModule(WebApplication app) : base(app)
    {
    }

    public override string? UrlFragment => "domain";


    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
        base.AddRoutes(routeGroupBuilder);
        routeGroupBuilder.MapGet("{domainName}/{entityName}", getEntity);
        routeGroupBuilder.MapPost("saveDomainWithWorkflow", saveDomainWithWorkflow);

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
        // Apply update to only changed fields.
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
        // Apply update to only changed fields.
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
        .Where(e => e.Name == entityName)
        .FirstOrDefaultAsync();

    if (entity != null)
    {
        return Results.Ok(
            new GetEntityResponse(
                entity.Name,
                entity.Description!,
                entity.Data.Select(d => new GetEntityDataResponse(d.Field, d.Ttl,
                    d.Sources.Select(s => new GetEntityDataSourcesResponse(s.Order, s.Tag!.Name, s.DataPath)).ToArray()
                )).ToArray()
            )
        );
    }
    else
    {
        return Results.NotFound();
    }
}
}