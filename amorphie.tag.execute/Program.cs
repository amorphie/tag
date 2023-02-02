

using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

using var client = new DaprClientBuilder().Build();

var builder = WebApplication.CreateBuilder(args);

//var test = await client.GetConfiguration("amorphie-config", new List<string>() { "STATE_STORE" });

var STATE_STORE = builder.Configuration["STATE_STORE"];
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(builder.Configuration["PostgreDB"], b => b.MigrationsAssembly("amorphie.tag")));

var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();
app.UseHttpMetrics();
app.MapMetrics();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/tag/{tagName}/execute", ExecuteTag)
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
app.MapGet("/template/{tagName}/execute", TemplateExecuter);
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

app.MapGet("/domain/{domainName}/entity/{entityName}/Execute", ExecuteEntity)
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
    [FromRoute(Name = "tagName")] string tagName,
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



async Task<IResult> ExecuteEntity(
    [FromRoute(Name = "domainName")] string domainName,
    [FromRoute(Name = "entityName")] string entityName,
    [FromQuery(Name = "tag")] string[] tag,
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

async Task<IResult> TemplateExecuter(
    [FromRoute(Name = "tagName")] string tagName,
    //Swagger'da deneme yapmak için eklendi normal requestten gelen query kullanılabilir.
    [FromQuery(Name = "reference")] string? reference,
    [FromServices] TagDBContext context,
    [FromQuery(Name = "viewTemplateName")] string? ViewTemplateName,
    HttpRequest request,
    HttpContext httpContext

)
{
    //View tablosundaki template name templateEngine tarafında template olarak oluşturulacak ???. 
    //Bu template name queryden gelen template bilgisine göre template engineden template oluşturulup response olarak return edilecek. 
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
    //Template döneceği için bu foreach döngüsüne gerek yok???
    foreach (var p in parameters)
    {
        if (!request.Query.ContainsKey(p.TrimStart('@')))
            return Results.BadRequest($"Required Url parameter(s) is not supplied as query parameters. Required parameters : {string.Join(",", parameters)}");
        // Düzeltildi
        urlToConsume = urlToConsume.Replace(p, request.QueryString.Value!.TrimStart('?').Split('&').FirstOrDefault(x => x.StartsWith(p.TrimStart('@')))!.Split('=').LastOrDefault() ?? string.Empty);
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
        var data = System.Text.Json.JsonSerializer.Serialize<dynamic>(response);
        var machineName = Environment.MachineName;
        var payload = new RenderRequestDefinition
        {
            Name = ViewTemplateName ?? "test-mehmet4",
            RenderData = data,
            RenderID = Guid.NewGuid(),
            SemVer = "1.0.0",
            Action = "amorphie-template-executer",
            Customer = "test-mehmet1",
            Identity = machineName ?? "amorphie-tag",
            ItemId = "test-mehmet1",
            ProcessName = "test-mehmet1",
            RenderDataForLog = data,
        };
        var json = System.Text.Json.JsonSerializer.Serialize<RenderRequestDefinition>(payload);

        HttpRequestMessage yourmsg = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://test-template-engine.burgan.com.tr/Template/Render"),
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var responses = await httpClient.SendAsync(yourmsg);

        // httpClient.BaseAddress = new Uri("https://test-template-engine.burgan.com.tr/");
        // var status = await httpClient.PostAsync("Template/Render", new StringContent(json, Encoding.UTF8, "application/json"));

        app.Logger.LogInformation($"ExecuteTag is responded with {responses}");
        return Results.Ok(responses.Content.ReadFromJsonAsync<dynamic>().Result);

        //swagger-adresi: https://test-template-engine.burgan.com.tr/swagger/index.html
    }
};