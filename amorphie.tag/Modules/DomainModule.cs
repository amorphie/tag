
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