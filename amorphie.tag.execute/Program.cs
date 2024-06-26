using amorphie.core.Middleware.Logging;
using amorphie.core.security.Extensions;
using Elastic.Apm.NetCoreAll;
using Npgsql.Replication.TestDecoding;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Refit;
using JsonSerializer = System.Text.Json.JsonSerializer;

using var client = new DaprClientBuilder().Build();

var builder = WebApplication.CreateBuilder(args);

await builder.Configuration.AddVaultSecrets("amorphie-tag", new string[] { "amorphie-tag" });
var postgreSql = builder.Configuration["PostgreSql"];
var amorphie_tag = "";
var templateEngineEndpoint = builder.Configuration["Url:TemplateEngine"];


var STATE_STORE = builder.Configuration["STATE_STORE"];

builder.AddSeriLogWithHttpLogging<AmorphieLogEnricher>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.tag")));

builder.Services.AddCors(options =>

{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseAllElasticApm(app.Configuration);
}
app.UseLoggingHandlerMiddlewares();

if (app.Environment.IsDevelopment())
{
    amorphie_tag = builder.Configuration["amorphie-tags"];
}
else
{
    amorphie_tag = "amorphie-tag.test-amorphie-tag";
}
app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

//async kullan. 

app.MapGet("/tag/{domainName}/{entityName}/{tagName}/execute", ExecuteTag)
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

app.MapGet("/tag/{tagName}/execute", TagExecute)
.WithOpenApi(operation =>
{
    operation.Summary = "Executes given tag with using query parameters and returns all entity fields.";
    return operation;
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status500InternalServerError)
.Produces(StatusCodes.Status510NotExtended)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status204NoContent);
app.MapGet("/template/{domainName}/{entityName}/{tagName}/execute", TemplateExecuteTag);
// app.MapGet("/template/{tagName}/execute", ExecuteTemplate);
app.MapGet("/tag/{tagName}/ugur", () => { })
.WithOpenApi(operation =>
{
    operation.Summary = "Ugurun methodu";
    return operation;
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status500InternalServerError)
.Produces(StatusCodes.Status510NotExtended)
.Produces(StatusCodes.Status400BadRequest)
.Produces(StatusCodes.Status204NoContent);

// app.MapGet("/domain/{domainName}/entity/{entityName}/Execute", ExecuteEntity)
// .WithOpenApi(operation =>
// {
//     operation.Summary = "Executes given entity with using tag-in query- parameters.";
//     return operation;
// })
// .Produces(StatusCodes.Status200OK)
// .Produces(StatusCodes.Status500InternalServerError)
// .Produces(StatusCodes.Status510NotExtended)
// .Produces(StatusCodes.Status400BadRequest)
// .Produces(StatusCodes.Status204NoContent);

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
    [FromRoute(Name = "tagName")] string tagName,
    [FromRoute(Name = "domainName")] string domainName,
    [FromRoute(Name = "entityName")] string entityName,
    HttpRequest request,
    HttpContext httpContext
    )
{
    app.Logger.LogInformation("ExecuteTag is calling");

    DtoTag tag;

    try
    {
        //b TODO: dapr service call to long
        //tag = await client.InvokeMethodAsync<GetTagResponse>(HttpMethod.Get, "amorphie-tag", $"tag/{tagName}");
        //var test = client.CreateInvokeMethodRequest(HttpMethod.Get, "amorphie-tag", $"tag/{tagId}");
        // var result = client.InvokeMethodWithResponseAsync(test).Result.Content.ReadAsStringAsync().Result;
        // var json = (JObject)JsonConvert.DeserializeObject(result);
        // tag = json["data"].ToObject<DtoTag>();
        tag = await client.InvokeMethodAsync<DtoTag>(HttpMethod.Get, $"{amorphie_tag}", $"Tag/getTag/{tagName}");

    }
    catch (Dapr.Client.InvocationException ex)
    {
        if (ex.Response.StatusCode == HttpStatusCode.NotFound)
            return Results.NotFound("Tag is not found.");

        if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
            return Results.Problem($"Tag query service is unavailable {ex.Message}", null, 510);

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
        // Düzeltildi
        urlToConsume = urlToConsume.Replace(p, request!.QueryString.Value!.TrimStart('?').Split('&').FirstOrDefault(x => x.StartsWith(p.TrimStart('@')))!.Split('=').LastOrDefault() ?? string.Empty);
        //urlToConsume = urlToConsume.Replace(p, request.Query.FirstOrDefault(x => x.Value != p).ToString());
    }


    var cachedResponse = await client.GetStateAsync<dynamic>(STATE_STORE, urlToConsume);

    if (cachedResponse is not null)
    {
        httpContext.Response.Headers.Add("X-Cache", "Hit");
        return Results.Ok(cachedResponse);
    }
    else
    {
        HttpClient httpClient = new();
        var result = await httpClient.GetAsync(urlToConsume);
        string test = await result.Content.ReadAsStringAsync();
        app.Logger.LogInformation($"ExecuteTag is responded with {test}");


        try
        {
            var entity = await client.InvokeMethodAsync<GetEntityResponse>(
                HttpMethod.Get,
                $"{amorphie_tag}",
                $"entityData/getEntityData/{domainName}/{entityName}"
            );

            var returnValue = new Dictionary<string, dynamic>();

            foreach (var field in entity.Data)
            {
                var sourceTags = field.Sources.OrderBy(f => f.Order).ToArray();

                foreach (var targetTag in sourceTags)
                {
                    if (tagName.Contains(targetTag.Tag))
                    {

                        JToken dataAsJson = JToken.Parse(test);

                        if (dataAsJson.SelectToken(targetTag.Path) != null)
                        {
                            returnValue.Add(field.Field, dataAsJson.SelectToken(targetTag.Path)!.Value<string>()!);
                        }

                        break;
                    }

                }
            }

            // var metadata = new Dictionary<string, string> { { "ttlInSeconds", $"{tag.Ttl}" } };
            var metadata = new Dictionary<string, string> { { "ttlInSeconds", $"{3}" } };
            await client.SaveStateAsync(STATE_STORE, urlToConsume, returnValue, metadata: metadata);

            httpContext.Response.Headers.Add("X-Cache", "Miss");


            app.Logger.LogInformation($"ExecuteTag is responded with {returnValue}");
            // return Results.Ok(test);

            return Results.Ok(returnValue);
        }
        catch (Dapr.Client.InvocationException ex)
        {
            if (ex.Response.StatusCode == HttpStatusCode.NotFound)
            {
                return Results.NotFound("Entity is not found.");
            }

            if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return Results.Problem("Entity query service is unavailable", null, 510);
            }

            return Results.Problem($"Entity query service error: {ex.Response.StatusCode}", null, 510);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Unhandled Entity query service error: {ex.Message}", null, 510);
        }

    }
};

async Task<IResult> TemplateExecuteTag(
    [FromRoute(Name = "tagName")] string tagName,
    [FromRoute(Name = "domainName")] string domainName,
    [FromRoute(Name = "entityName")] string entityName,
    [FromQuery(Name = "viewTemplateName")] string? ViewTemplateName,

    HttpRequest request,
    HttpContext httpContext
    )
{
    app.Logger.LogInformation("ExecuteTag is calling");

    DtoTag tag;

    try
    {
        //b TODO: dapr service call to long
        //tag = await client.InvokeMethodAsync<GetTagResponse>(HttpMethod.Get, "amorphie-tag", $"tag/{tagName}");
        //var test = client.CreateInvokeMethodRequest(HttpMethod.Get, "amorphie-tag", $"tag/{tagId}");
        // var result = client.InvokeMethodWithResponseAsync(test).Result.Content.ReadAsStringAsync().Result;
        // var json = (JObject)JsonConvert.DeserializeObject(result);
        // tag = json["data"].ToObject<DtoTag>();
        tag = await client.InvokeMethodAsync<DtoTag>(HttpMethod.Get, $"{amorphie_tag}", $"Tag/getTag/{tagName}");

    }
    catch (Dapr.Client.InvocationException ex)
    {
        if (ex.Response.StatusCode == HttpStatusCode.NotFound)
            return Results.NotFound("Tag is not found.");

        if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
            return Results.Problem($"Tag query service is unavailable {ex.Message}", null, 510);

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
        // Düzeltildi
        urlToConsume = urlToConsume.Replace(p, request!.QueryString.Value!.TrimStart('?').Split('&').FirstOrDefault(x => x.StartsWith(p.TrimStart('@')))!.Split('=').LastOrDefault() ?? string.Empty);
        //urlToConsume = urlToConsume.Replace(p, request.Query.FirstOrDefault(x => x.Value != p).ToString());
    }



    HttpClient httpClient = new();
    var result = await httpClient.GetAsync(urlToConsume);
    string test = await result.Content.ReadAsStringAsync();
    app.Logger.LogInformation($"ExecuteTag testData is responded with {test}");


    try
    {
        var entity = await client.InvokeMethodAsync<GetEntityResponse>(
            HttpMethod.Get,
            $"{amorphie_tag}",
            $"entityData/getEntityData/{domainName}/{entityName}"
        );

        var returnValue = new Dictionary<string, dynamic>();

        foreach (var field in entity.Data)
        {
            var sourceTags = field.Sources.OrderBy(f => f.Order).ToArray();

            foreach (var targetTag in sourceTags)
            {
                if (tagName.Contains(targetTag.Tag))
                {

                    JToken dataAsJson = JToken.Parse(test);

                    if (dataAsJson.SelectToken(targetTag.Path) != null)
                    {
                        returnValue.Add(field.Field, dataAsJson.SelectToken(targetTag.Path)!.Value<string>()!);
                    }

                    break;
                }

            }
        }

        // var metadata = new Dictionary<string, string> { { "ttlInSeconds", $"{tag.Ttl}" } };



        app.Logger.LogInformation($"ExecuteTag filterData is responded with {returnValue}");
        // return Results.Ok(test);



        var data = System.Text.Json.JsonSerializer.Serialize<dynamic>(returnValue);
        app.Logger.LogInformation($"ExecuteTag renderData is responded with {data}");
        var machineName = Environment.MachineName;
        var payload = new RenderRequestDefinition
        {
            Name = ViewTemplateName ?? "test-mehmet4",
            RenderData = data,
            RenderID = Guid.NewGuid(),
            SemVer = "1.0.0",
            Action = "amorphie-template-executer",
            Customer = "test-mehmet1",
            Identity = "amorphie-tag",
            ItemId = "test-mehmet1",
            ProcessName = "test-mehmet1",
            RenderDataForLog = data,
        };

        /// ----- TODO: minimize
        var json = System.Text.Json.JsonSerializer.Serialize<RenderRequestDefinition>(payload);
        app.Logger.LogInformation($"ExecuteTag jsonEncode is responded with {json}");
        HttpRequestMessage yourmsg = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{templateEngineEndpoint}/Template/Render"),
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var responses = await httpClient.SendAsync(yourmsg);

        app.Logger.LogInformation($"ExecuteTag templateResponse is responded with {responses}");
        return Results.Ok(responses.Content.ReadFromJsonAsync<dynamic>().Result);
    }
    catch (Dapr.Client.InvocationException ex)
    {
        if (ex.Response.StatusCode == HttpStatusCode.NotFound)
        {
            return Results.NotFound("Entity is not found.");
        }

        if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
        {
            return Results.Problem("Entity query service is unavailable", null, 510);
        }

        return Results.Problem($"Entity query service error: {ex.Response.StatusCode}", null, 510);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Unhandled Entity query service error: {ex.Message}", null, 510);
    }
}


async Task<IResult> TagExecute(
[FromRoute(Name = "tagName")] string tagName,
HttpRequest request,
HttpContext httpContext
)
{
    app.Logger.LogInformation("ExecuteTag is calling");

    DtoTag tag;

    try
    {
        tag = await client.InvokeMethodAsync<DtoTag>(HttpMethod.Get, $"{amorphie_tag}", $"Tag/getTag/{tagName}");
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
        // Düzeltildi
        urlToConsume = urlToConsume.Replace(p, request!.QueryString.Value!.TrimStart('?').Split('&').FirstOrDefault(x => x.StartsWith(p.TrimStart('@')))!.Split('=').LastOrDefault() ?? string.Empty);
        //urlToConsume = urlToConsume.Replace(p, request.Query.FirstOrDefault(x => x.Value != p).ToString());
    }


    var cachedResponse = await client.GetStateAsync<dynamic>(STATE_STORE, urlToConsume);

    if (cachedResponse is not null)
    {
        httpContext.Response.Headers.Add("X-Cache", "Hit");
        return Results.Ok(cachedResponse);
    }
    else
    {
        HttpClient httpClient = new();
        var response = await httpClient.GetFromJsonAsync<dynamic>(urlToConsume);

        var metadata = new Dictionary<string, string> { { "ttlInSeconds", $"{tag.Ttl}" } };
        await client.SaveStateAsync(STATE_STORE, urlToConsume, response, metadata: metadata);

        httpContext.Response.Headers.Add("X-Cache", "Miss");


        app.Logger.LogInformation($"ExecuteTag is responded with {response}");
        return Results.Ok(response);
    }
};


