using amorphie.core.security.Extensions;
using Npgsql.Replication.TestDecoding;
using JsonSerializer = System.Text.Json.JsonSerializer;

using var client = new DaprClientBuilder().Build();

var builder = WebApplication.CreateBuilder(args);
// var secret = await client.GetSecretAsync("amorphie-secretstore", "amorphie-secretstore");
// builder.Configuration.AddInMemoryCollection(secret);
// var postgreSql = builder.Configuration["PostgreSql"];
await builder.Configuration.AddVaultSecrets("amorphie-secretstore", new string[] { "amorphie-secretstore" });
var postgreSql = builder.Configuration["PostgreSql"];
var amorphie_tag = "";

//var test = await client.GetConfiguration("amorphie-config", new List<string>() { "STATE_STORE" });


var STATE_STORE = builder.Configuration["STATE_STORE"];

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.tag")));

var app = builder.Build();
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

app.UseSwagger();
app.UseSwaggerUI();

//async kullan. 

app.MapGet("/tag/{domainName}/{entityName}/{tagName}/execute", ExecuteTag)
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
                $"entityData/getEntity/{domainName}/{entityName}"
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



async Task<IResult> ExecuteEntity(
    [FromRoute(Name = "domainName")] string domainName,
    [FromRoute(Name = "entityName")] string entityName,
    [FromQuery(Name = "tag")] string[] tags,
    [FromQuery(Name = "reference")] string reference,
    HttpRequest request,
    HttpContext httpContext
)
{
    if (tags == null || tags.Length == 0)
    {
        return Results.BadRequest("At least one tag must be supplied.");
    }

    var queryReference = reference?.Split('&').ToList();
    var queryTags = request.Query["tag"].ToList();
    try
    {
        var entity = await client.InvokeMethodAsync<GetEntityResponse>(
            HttpMethod.Get,
            $"{amorphie_tag}",
            $"entityData/getEntity/{domainName}/{entityName}"
        );

        var returnValue = new Dictionary<string, dynamic>();

        foreach (var field in entity.Data)
        {
            var sourceTags = field.Sources.OrderBy(f => f.Order).ToArray();

            foreach (var targetTag in sourceTags)
            {
                if (queryTags.Contains(targetTag.Tag))
                {
                    var data = await client.InvokeMethodAsync<dynamic>(
                                        HttpMethod.Get,
                                        "amorphie-tag-execute",
                                        $"tag/{domainName}/{entityName}/{targetTag.Tag}/execute?reference={queryReference?.FirstOrDefault()}");
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

    DtoTag? tag;

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

    // if (cachedResponse is not null)
    // {
    //     httpContext.Response.Headers.Add("X-Cache", "Hit");
    //     return Results.Ok(cachedResponse);
    // }
    // else
    {

        using HttpClient httpClient = new();
        var response = await httpClient.GetFromJsonAsync<dynamic>(urlToConsume);

        var metadata = new Dictionary<string, string> { { "ttlInSeconds", $"{tag.Ttl}" } };
        await client.SaveStateAsync(STATE_STORE, urlToConsume, response, metadata: metadata);

        httpContext.Response.Headers.Add("X-Cache", "Miss");
        //         var requestData = new
        //     {
        //         page = 1,
        //         size = 1,
        //         identityNumber = "44671321132"
        //     };

        //     HttpContent content = new StringContent(JsonSerializer.Serialize<dynamic>(requestData), Encoding.UTF8, "application/json");

        //     var customerApi=httpClient.PostAsync("https://test-entegrasyon-customerapi.burgan.com.tr/Customer",content);
        // var responseContent = customerApi.Result.Content.ReadAsStringAsync().Result;
        //     dynamic customerApiResponse = JsonConvert.DeserializeObject(responseContent);

        //     app.Logger.LogInformation($"ExecuteTag is responded with {customerApiResponse}");



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

        /// ----- TODO: minimize
        var json = System.Text.Json.JsonSerializer.Serialize<RenderRequestDefinition>(payload);

        HttpRequestMessage yourmsg = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://test-template-engine.burgan.com.tr/Template/Render"),
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var responses = await httpClient.SendAsync(yourmsg);

        // httpClient.BaseAddress = new Uri("https://test-template-engine.burgan.com.tr/");
        // var status = await httpClient.PostAsync("Template/Render", new StringContent(json, Encoding.UTF8, "application/json"));
        ///////---------------------------
        app.Logger.LogInformation($"ExecuteTag is responded with {responses}");
        return Results.Ok(responses.Content.ReadFromJsonAsync<dynamic>().Result);

        // post http client with body


        //12113810636



        //swagger-adresi: https://test-template-engine.burgan.com.tr/swagger/index.html
    }

}

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
            $"entityData/getEntity/{domainName}/{entityName}"
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



        // var response = await httpClient.GetFromJsonAsync<dynamic>(urlToConsume);

        // httpContext.Response.Headers.Add("X-Cache", "Miss");
        //         var requestData = new
        //     {
        //         page = 1,
        //         size = 1,
        //         identityNumber = "44671321132"
        //     };

        //     HttpContent content = new StringContent(JsonSerializer.Serialize<dynamic>(requestData), Encoding.UTF8, "application/json");

        //     var customerApi=httpClient.PostAsync("https://test-entegrasyon-customerapi.burgan.com.tr/Customer",content);
        // var responseContent = customerApi.Result.Content.ReadAsStringAsync().Result;
        //     dynamic customerApiResponse = JsonConvert.DeserializeObject(responseContent);

        //     app.Logger.LogInformation($"ExecuteTag is responded with {customerApiResponse}");


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
            RequestUri = new Uri("https://test-template-engine.burgan.com.tr/Template/Render"),
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var responses = await httpClient.SendAsync(yourmsg);

        // httpClient.BaseAddress = new Uri("https://test-template-engine.burgan.com.tr/");
        // var status = await httpClient.PostAsync("Template/Render", new StringContent(json, Encoding.UTF8, "application/json"));
        ///////---------------------------
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


};

