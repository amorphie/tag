
public static class DomainModule
{

    public static void MapDomainEndpoints(this WebApplication app)
    {
        app.MapGet("/domain", getAllDomains)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Returns saved domain records with entities";
            return operation;

        })
        .Produces<GetDomainResponse[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);

        app.MapGet("/domain/{domain-name}/Entity/{entity-name}", getEntity)
        .WithOpenApi(operation =>
        {
            operation.Summary = "Returns requested entity";
            return operation;

        })
        .Produces<GetEntityResponse[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }

    public async static Task<IResult> getAllDomains(
        [FromServices] TagDBContext context,
        [FromServices] DaprClient client
        )
    {
        var domains = context!.Domains!
            .Include(t => t.Entities)
            .ToList();


        //var clientx = new DaprClientBuilder().Build();

        //var result = await client.CheckHealthAsync();
        await client.SaveStateAsync("amorphie-cache", "ugur","karatas2");


        if (domains.Count() > 0)
        {
            return Results.Ok(domains.Select(domain =>
              new GetDomainResponse(
                domain.Name,
                domain.Description,
                domain.Entities.Select(i => new GetDomainEntityResponse(i.Name, i.Description)).ToArray()
                )
            ).ToArray());
        }
        else
            return Results.NoContent();
    }

    public static IResult getEntity(
        [FromRoute(Name = "domain-name")] string domainName,
        [FromRoute(Name = "entity-name")] string tagName,
        [FromServices] TagDBContext context
       
    )
    {
        var entity = context!.Entites!
            .Include(e => e.Data)
            .ThenInclude(d => d.Sources)
            .ThenInclude(s => s.Tag)
            .Where(e => e.Name == tagName)
            .FirstOrDefault();


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
        else
            return Results.NotFound();
    }
}