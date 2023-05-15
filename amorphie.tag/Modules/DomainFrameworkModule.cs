
using System.Diagnostics;
using amorphie.core.Module.minimal_api;


public sealed class DomainFrameworkModule : BaseRoute
{
        public DomainFrameworkModule(WebApplication app) : base(app)
        {
        }

    public override string? UrlFragment => "domain";
    public override void AddRoutes(RouteGroupBuilder routeGroupBuilder)
    {
           routeGroupBuilder.MapGet("/", getAllDomains)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Returns saved domain records with entities";
            return operation;

        })
        .Produces<GetDomainResponse[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);

                routeGroupBuilder.MapGet("/{domainName}/Entity/{entityName}", getEntity)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Returns requested entity";
            return operation;

        })
        .Produces<GetEntityResponse[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);

        routeGroupBuilder.MapPost("/addDomain", saveDomain).WithTopic("pubsub", "SaveDomain").WithOpenApi(operation =>
        {
            operation.Summary = "Saves or updates requested domain.";
            return operation;
        }).
        Produces<GetDomainResponse>(StatusCodes.Status200OK);
    }

 async  ValueTask<IResult> getAllDomains(
        [FromServices] TagDBContext context,
        [FromServices] DaprClient client,
        HttpContext httpContext
        )
    {

        var cacheData = await client.GetStateAsync<GetDomainResponse[]>("amorphie-cache", "GetAllDomains");
        if (cacheData != null)
        {
            httpContext.Response.Headers.Add("X-Cache", "Hit");
            return Results.Ok(cacheData);
        }


        var domains = await context!.Domains!
            .Include(t => t.Entities)
            .ToListAsync();


        if (domains.Count() > 0)
        {

            var response = domains.Select(domain =>
              new GetDomainResponse(
                domain.Name,
                domain.Description,
                domain.Entities.Select(i => new GetDomainEntityResponse(i.Name, i.Description!)).ToArray()
            )).ToArray();

            var metadata = new Dictionary<string, string> { { "ttlInSeconds", "15" } };
            await client.SaveStateAsync("amorphie-cache", "GetAllDomains", response, metadata: metadata);

            httpContext.Response.Headers.Add("X-Cache", "Miss");

            return Results.Ok(response);
        }
        else
            return Results.NoContent();
    }

    async ValueTask<IResult> saveDomain([FromServices] TagDBContext context, [FromBody] SaveDomainRequest request)
    {
        //Türkçe Karakter kabul etmiyor. Swagger hatası olabilir? 
        var existingRecord = context?.Domains?.FirstOrDefault(d => d.Name == request.Name);
        if (existingRecord == null)
        {
            context!.Domains!.Add(new Domain { Name = request.Name, Description = request.Description! });
            await context.SaveChangesAsync();
            return Results.Created($"/domain/{request.Name}", existingRecord);
        }
        else
        {
            var hasChanges = false;
            if (request.Description != null && request.Description != existingRecord.Description)
            {
                existingRecord.Description = request.Description;
                hasChanges = true;
            }
            if (hasChanges)
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

    async ValueTask<IResult> getEntity(
        [FromRoute(Name = "domainName")] string domainName,
        [FromRoute(Name = "entityName")] string entityName,
        [FromServices] TagDBContext context

    )
    {
        //Entitye taşı
        var entity = await context!.Entities!
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
                ));
        }
        else
            return Results.NotFound();
    }
}