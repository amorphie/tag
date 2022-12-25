var client = new DaprClientBuilder().Build();

var builder = WebApplication.CreateBuilder(args);

var STATE_STORE = "amorphie-cache";

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/tag/{tag-name}/execute", ExecuteTag)
.WithOpenApi(operation =>
{
    operation.Summary = "Executes given tag with using query parameters.";
    return operation;
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status500InternalServerError)
.Produces(StatusCodes.Status510NotExtended)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status204NoContent);



app.MapGet("/domain/{domain-name}/entity/{entity-name}/Execute", ExecuteEntity)
.WithOpenApi(operation =>
{
    operation.Summary = "Executes given entity with using tag-in query- parameters.";
    return operation;
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status500InternalServerError)
.Produces(StatusCodes.Status510NotExtended)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status204NoContent);

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();

}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}

async Task<IResult> ExecuteTag(
    [FromRoute(Name = "tag-name")] string tagName,
    HttpRequest request,
    HttpContext httpContext
    )
{
    app.Logger.LogInformation("ExecuteTag is calling");

    GetTagResponse? tag;

    try
    {
        tag = await client.InvokeMethodAsync<GetTagResponse>(HttpMethod.Get, "amorphie-tag", $"tag/{tagName}");
    }
    catch (Dapr.Client.InvocationException ex)
    {
        if (ex.Response.StatusCode == HttpStatusCode.NotFound)
            return Results.NotFound("Tag is not found.");

        if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
            return Results.Problem("Tag query service is unavailable", null, 510);

        return Results.Problem($"Tag query service error : {ex.Response.StatusCode}", null, 510);
    }
    catch (Exception ex)
    {

        return Results.Problem($"Unhandled Tag query service error : {ex.Message}", null, 510);
    }

    if (string.IsNullOrEmpty(tag.Url))
    {
        return Results.BadRequest("This tag does not have URL");
    }

    var parameters = tag.Url.Split(new Char[] { '/', '?', '&', '=' }, StringSplitOptions.RemoveEmptyEntries).Where(x => x.StartsWith('@')).ToList();
    var urlToConsume = tag.Url;

    foreach (var p in parameters)
    {
        if (!request.Query.ContainsKey(p.TrimStart('@')))
            return Results.BadRequest($"Required Url parameter(s) is not supplied as query parameters. Required parameters : {string.Join(",", parameters)}");

        urlToConsume = urlToConsume.Replace(p, request.Query[p.TrimStart('@')].ToString());
    }


    var cachedResponse = await client.GetStateAsync<dynamic>(STATE_STORE, urlToConsume);

    if (cachedResponse is not null)
    {
        httpContext.Response.Headers.Add("X-Cache", "Hit");
        return Results.Ok(cachedResponse);
    }
    else
    {
        // This process will be replaced with with dapr 1.10 version service invoke for better telemetry: https://github.com/dapr/dapr/issues/4549
        HttpClient httpClient = new();
        var response = await httpClient.GetFromJsonAsync<dynamic>(urlToConsume);

        var metadata = new Dictionary<string, string> { { "ttlInSeconds", $"{tag.Ttl}" } };
        await client.SaveStateAsync(STATE_STORE, urlToConsume, response, metadata: metadata);

        httpContext.Response.Headers.Add("X-Cache", "Miss");


        app.Logger.LogInformation($"ExecuteTag is responded with {response}");

        return Results.Ok(response);
    }
};



async Task<IResult> ExecuteEntity(
    [FromRoute(Name = "domain-name")] string domainName,
    [FromRoute(Name = "entity-name")] string entityName,
    [FromQuery(Name = "tag")] string[] tags,
    HttpRequest request,
    HttpContext httpContext
    )
{
    if (!request.Query.ContainsKey("tag"))
    {
        return Results.BadRequest("Any tag is not suplied. At least one tag must be suplied.");
    }
    var queryTags = request.Query["tag"].ToList();

    GetEntityResponse? entity;

    try
    {
        entity = await client.InvokeMethodAsync<GetEntityResponse>(HttpMethod.Get, "amorphie-tag", $"domain/{domainName}/entity/{entityName}");
    }
    catch (Dapr.Client.InvocationException ex)
    {
        if (ex.Response.StatusCode == HttpStatusCode.NotFound)
            return Results.NotFound("Entity is not found.");

        if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
            return Results.Problem("Entity query service is unavailable", null, 510);

        return Results.Problem($"Entity query service error : {ex.Response.StatusCode}", null, 510);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Unhandled Entity query service error : {ex.Message}", null, 510);
    }

    var returnValue = new Dictionary<string, dynamic>();

    foreach (var field in entity.Data)
    {
        var sourceTags = field.Sources.OrderBy(f => f.Order).ToArray();

        foreach (var targetTag in sourceTags)
        {
            if (queryTags.Contains(targetTag.Tag))
            {
                var data = await client.InvokeMethodAsync<dynamic>(HttpMethod.Get, "amorphie-tag-execute", $"tag/{targetTag.Tag}/execute{request.QueryString.Value}");
                JToken dataAsJson = JToken.Parse(data.ToString());

                if (dataAsJson.SelectToken(targetTag.Path) != null)
                {
                    returnValue.Add(field.Field, dataAsJson.SelectToken(targetTag.Path)!.Value<string>()!);
                }

                break;
            }

        }
    }

    return Results.Ok(returnValue);
}


