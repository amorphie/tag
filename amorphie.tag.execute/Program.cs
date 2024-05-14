using amorphie.core.Middleware.Logging;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Xml;
using Elastic.Apm.NetCoreAll;
using MongoDB.Bson;
using Npgsql.Replication.TestDecoding;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Refit;
using Serilog;
using JsonSerializer = System.Text.Json.JsonSerializer;
using amorphie.core.Extension;
using System.Drawing.Text;
using Google.Protobuf.WellKnownTypes;
using amorphie.core.Base;
using Elastic.Apm.Api;
using System.Net;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

await builder.Configuration.AddVaultSecrets("amorphie-tag", new string[] { "amorphie-tag" });

var postgreSql = builder.Configuration["PostgreSql"];
var amorphie_tag = builder.Configuration["amorphie-tag"];
var templateEngineEndpoint = builder.Configuration["Url:TemplateEngine"];
var STATE_STORE = builder.Configuration["STATE_STORE"];

builder.AddSeriLogWithHttpLogging<AmorphieLogEnricher>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TagDBContext>(options =>
    options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.tag")));
builder.Host.UseSerilog((_, serviceProvider, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
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

if (!app.Environment.IsDevelopment())
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
app.UseAllElasticApm(app.Configuration);
app.UseHttpLogging();

var client = new DaprClientBuilder().Build();

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

app.MapGet("/htmlTemplate/{domainName}/{entityName}/{tagName}/{viewTemplateName}/execute", HtmlTemplateExecuteTag);
app.MapGet("/pdfTemplate/{domainName}/{entityName}/{tagName}/{viewTemplateName}/execute", PdfTemplateExecuteTag);

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Application terminated unexpectedly");
}

async Task<IResult> ExecuteTag(
    [FromRoute] string tagName,
    [FromRoute] string domainName,
    [FromRoute] string entityName,
    HttpRequest request,
    HttpContext httpContext)
{
    return await ExecuteTagInternal(tagName, domainName, entityName, request, httpContext);
}

async Task<IResult> TagExecute(
    [FromRoute] string tagName,
    HttpRequest request,
    HttpContext httpContext)
{
    return await ExecuteTagInternal(tagName, null, null, request, httpContext);
}

async Task<IResult> HtmlTemplateExecuteTag(
    [FromRoute] string tagName,
    [FromRoute] string domainName,
    [FromRoute] string entityName,
    [FromRoute] string viewTemplateName,
    [FromQuery] string reference,
    [FromQuery] string version,
    HttpRequest request,
    HttpContext httpContext)
{
    app.Logger.LogInformation($"HtmlTemplateExecuteTag called with domainName={domainName}, entityName={entityName}, tagName={tagName}, viewTemplateName={viewTemplateName}, reference={reference}, version={version}");
    return await TemplateExecuteTag(tagName, domainName, entityName, viewTemplateName, reference, version, request, httpContext, "html");
}

async Task<IResult> PdfTemplateExecuteTag(
    [FromRoute] string tagName,
    [FromRoute] string domainName,
    [FromRoute] string entityName,
    [FromRoute] string viewTemplateName,
    [FromQuery] string reference,
    [FromQuery] string version,
    HttpRequest request,
    HttpContext httpContext)
{
    app.Logger.LogInformation($"PdfTemplateExecuteTag called with domainName={domainName}, entityName={entityName}, tagName={tagName}, viewTemplateName={viewTemplateName}, reference={reference}, version={version}");
    return await TemplateExecuteTag(tagName, domainName, entityName, viewTemplateName, reference, version, request, httpContext, "pdf");
}

async Task<IResult> ExecuteTagInternal(string tagName, string domainName, string entityName, HttpRequest request, HttpContext httpContext)
{
    try
    {
        HttpClient httpClient = new();
        app.Logger.LogInformation("ExecuteTag is calling");

        var getTagResult = await GetTag(tagName);
        if (getTagResult.Result != Results.Ok())
        {
            app.Logger.LogInformation("");
            return getTagResult.Result;
        }

        DtoTag tag = getTagResult.Data;
        var dtoTags = await GetRelatedTags(tag);
        var entityDataResult = new Dictionary<string, dynamic>();

        foreach (var dtoTag in dtoTags)
        {
            var urlToConsumerResult = await GetConsumeUrl(dtoTag.Url, request);
            if (urlToConsumerResult.Result != Results.Ok())
            {
                app.Logger.LogInformation("");
                return urlToConsumerResult.Result;
            }

            var urlToConsume = urlToConsumerResult.Data;
            var getJsonDataResult = await GetJsonData(urlToConsume, httpClient);
            if (getJsonDataResult.Result != Results.Ok())
            {
                app.Logger.LogInformation("");
                return getJsonDataResult.Result;
            }

            var jsondata = getJsonDataResult.Data;

            if (!string.IsNullOrEmpty(domainName) && !string.IsNullOrEmpty(entityName))
            {
                var entity = await GetEntity(domainName, entityName);

                foreach (var field in entity.Data)
                {
                    var fieldSources = field.Sources.OrderBy(f => f.Order).ToArray();
                    GetFieldSource(field, dtoTag.Name, jsondata, entityDataResult, fieldSources);
                }
            }
            else
            {
                var dataAsJson = JToken.Parse(jsondata);
                foreach (var property in dataAsJson)
                {
                    entityDataResult[property.Path] = property.First.ToString();
                }
            }

            app.Logger.LogInformation($"ExecuteTag filterData responded with {entityDataResult}");
        }

        var cachedResponse = await client.GetStateAsync<dynamic>(STATE_STORE, tagName);

        if (cachedResponse is not null)
        {
            app.Logger.LogInformation("Cache hit");
            httpContext.Response.Headers.Append("X-Cache", "Hit");
            return Results.Ok(cachedResponse);
        }

        var metadata = new Dictionary<string, string> { { "ttlInSeconds", $"{tag.Ttl}" } };
        await client.SaveStateAsync(STATE_STORE, tagName, entityDataResult, metadata: metadata);

        httpContext.Response.Headers.Append("X-Cache", "Miss");

        app.Logger.LogInformation($"ExecuteTag responded with {entityDataResult}");
        return Results.Ok(entityDataResult);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Unhandled Tag query service error");
        return Results.Problem($"Unhandled Tag query service error: {ex.Message}", null, 510);
    }
}

async Task<IResult> TemplateExecuteTag(
    string tagName,
    string domainName,
    string entityName,
    string viewTemplateName,
    string reference,
    string version,
    HttpRequest request,
    HttpContext httpContext,
    string type)
{
    try
    {
        HttpClient httpClient = new();
        app.Logger.LogInformation("TemplateExecuteTag is calling");

        var getTagResult = await GetTag(tagName);
        if (getTagResult.Result != Results.Ok())
        {
            app.Logger.LogInformation("Failed to get tag");
            return getTagResult.Result;
        }

        DtoTag tag = getTagResult.Data;
        var dtoTags = await GetRelatedTags(tag);
        var entityDataResult = new Dictionary<string, dynamic>();

        foreach (var dtoTag in dtoTags)
        {
            var urlToConsumerResult = await GetConsumeUrl(dtoTag.Url, request);
            if (urlToConsumerResult.Result != Results.Ok())
            {
                app.Logger.LogInformation("Failed to get URL to consume");
                return urlToConsumerResult.Result;
            }

            var urlToConsume = urlToConsumerResult.Data;
            var getJsonDataResult = await GetJsonData(urlToConsume, httpClient);
            if (getJsonDataResult.Result != Results.Ok())
            {
                app.Logger.LogInformation("Failed to get JSON data");
                return getJsonDataResult.Result;
            }

            var jsondata = getJsonDataResult.Data;
            var entity = await GetEntity(domainName, entityName);

            if (entity.Data == null || !entity.Data.Any())
            {
                app.Logger.LogInformation("No entity data found");
                return Results.Problem("Entity query service error: NoContent", null, 510);
            }

            foreach (var field in entity.Data)
            {
                var fieldSources = field.Sources.OrderBy(f => f.Order).ToArray();
                GetFieldSource(field, dtoTag.Name, jsondata, entityDataResult, fieldSources);
            }

            app.Logger.LogInformation($"TemplateExecuteTag filterData responded with {entityDataResult}");
        }

        app.Logger.LogInformation($"Final data: {entityDataResult}");
        var deserializeResponse = await CallTemplateEngine(version, httpClient, type, entityDataResult, viewTemplateName);

        httpContext.Response.Headers.Append("X-Cache", "Miss");

        return Results.Content(deserializeResponse);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while executing the template");
        return Results.Problem(ex.Message);
    }
}

async ValueTask<ResultData> GetTag(string tagName)
{
    try
    {
        var tag = await client.InvokeMethodAsync<DtoTag>(HttpMethod.Get, $"{amorphie_tag}", $"Tag/getTag/{tagName}");
        if (string.IsNullOrEmpty(tag.Url))
        {
            return new ResultData(Results.BadRequest("This tag does not have URL"));
        }
        return new ResultData(Results.Ok(), tag);
    }
    catch (Dapr.Client.InvocationException ex)
    {
        return HandleTagInvocationException(ex);
    }
    catch (Exception ex)
    {
        return new ResultData(Results.Problem($"Unhandled Tag query service error: {ex.Message}", null, 510));
    }
}

ResultData HandleTagInvocationException(Dapr.Client.InvocationException ex)
{
    return ex.Response.StatusCode switch
    {
        HttpStatusCode.NotFound => new ResultData(Results.NotFound("Tag is not found.")),
        HttpStatusCode.InternalServerError => new ResultData(Results.Problem($"Tag query service is unavailable {ex.Message}", null, 510)),
        _ => new ResultData(Results.Problem($"Tag query service error: {ex.Response.StatusCode}", null, 510)),
    };
}

async Task<List<DtoTag>> GetRelatedTags(DtoTag tag)
{
    var dtoTags = new List<DtoTag> { tag };

    foreach (var item in tag.TagsRelations)
    {
        if (!dtoTags.Any(t => t.Name == item.TagName))
        {
            var relatedTag = await GetTag(item.TagName);
            if (relatedTag.Data != null)
            {
                dtoTags.Add(relatedTag.Data);
            }
        }
    }

    return dtoTags;
}

async ValueTask<ResultData> GetConsumeUrl(string url, HttpRequest request)
{
    var parameters = url.Split(new char[] { '/', '?', '&', '=' }, StringSplitOptions.RemoveEmptyEntries)
        .Where(x => x.StartsWith('@')).ToList();

    foreach (var p in parameters)
    {
        if (!request.Query.ContainsKey(p.TrimStart('@')))
        {
            return new ResultData(Results.BadRequest($"Required Url parameter(s) is not supplied as query parameters. Required parameters: {string.Join(",", parameters)}"));
        }

        url = url.Replace(p, request.Query[p.TrimStart('@')]);
    }

    return new ResultData(Results.Ok(), url);
}

async Task<ResultData> GetJsonData(string urlToConsume, HttpClient httpClient)
{
    var result = await httpClient.GetAsync(urlToConsume);

    if (!result.IsSuccessStatusCode)
    {
        return new ResultData(Results.BadRequest("GetData Failed"));
    }

    var content = await result.Content.ReadAsStringAsync();
    var contentType = result.Content.Headers.ContentType?.MediaType;
    string jsondata;

    try
    {
        if (contentType == "application/json")
        {
            jsondata = JToken.Parse(content).ToString();
        }
        else if (contentType == "application/xml" || contentType == "text/xml")
        {
            var doc = new XmlDocument();
            doc.LoadXml(content);
            jsondata = JsonConvert.SerializeXmlNode(doc);
        }
        else
        {
            jsondata = content;
        }
    }
    catch (Exception ex)
    {
        return new ResultData(Results.BadRequest(ex.Message));
    }

    return new ResultData(Results.Ok(), jsondata);
}

async Task<GetEntityResponse> GetEntity(string domainName, string entityName)
{
    app.Logger.LogInformation($"Fetching entity data for domainName={domainName}, entityName={entityName}");
    var response = await client.InvokeMethodAsync<GetEntityResponse>(HttpMethod.Get, $"{amorphie_tag}", $"entityData/getEntityData/{domainName}/{entityName}");
    if (response.Data == null || !response.Data.Any())
    {
        app.Logger.LogInformation("No entity data returned from service");
    }
    return response;
}

void GetFieldSource(GetEntityDataResponse field, string tagName, string jsondata, Dictionary<string, dynamic> entityDataResult, GetEntityDataSourcesResponse[] fieldSources)
{
    foreach (var fieldSource in fieldSources)
    {
        if (tagName.Contains(fieldSource.Tag))
        {
            var dataAsJson = JToken.Parse(jsondata);
            var token = dataAsJson.SelectToken(fieldSource.Path);
            if (token != null)
            {
                entityDataResult.Add(field.Field, token.Value<string>());
            }
            break;
        }
    }
}

async Task<string> CallTemplateEngine(string version, HttpClient httpClient, string type, Dictionary<string, dynamic> entityDataResult, string viewTemplateName)
{
    var data = JsonSerializer.Serialize(entityDataResult);
    app.Logger.LogInformation($"ExecuteTag renderData responded with {data}");

    var payload = new RenderRequestDefinition
    {
        Name = viewTemplateName ?? "contractTag",
        RenderData = data,
        RenderID = Guid.NewGuid(),
        Action = "amorphie-template-executer",
        Customer = "numberTemplate",
        Identity = "amorphie-tag",
        ItemId = "numberTemplate",
        ProcessName = "numberTemplate",
        RenderDataForLog = data,
        SemVer = version ?? "1.0.4"
    };

    var json = JsonSerializer.Serialize(payload);
    app.Logger.LogInformation($"ExecuteTag jsonEncode responded with {json}");

    var requestUri = type == "pdf"
        ? new Uri($"{templateEngineEndpoint}Template/Render/pdf")
        : new Uri($"{templateEngineEndpoint}Template/Render");

    var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
    {
        Content = new StringContent(json, Encoding.UTF8, "application/json")
    };

    var response = await httpClient.SendAsync(requestMessage);
    var options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    app.Logger.LogInformation($"ExecuteTag templateResponse responded with {response}");

    var deserializeData = await response.Content.ReadFromJsonAsync<dynamic>();
    return JsonSerializer.Deserialize<string>(deserializeData, options);
}
