
var client = new DaprClientBuilder().Build();

var builder = WebApplication.CreateBuilder(args);

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
    operation.Summary = "Executes given tag with using body parameters.";
    return operation;
})
.Produces(StatusCodes.Status200OK)
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
    HttpRequest request
    )
{
    app.Logger.LogInformation("ExecuteTag is calling");

    GetTagResponse tag = null;

    try
    {
        tag = await client.InvokeMethodAsync<GetTagResponse>(HttpMethod.Get, "amorphie-tag", "tag/" + tagName);
    }
    catch (Dapr.Client.InvocationException ex)
    {
        if (ex.Response.StatusCode == HttpStatusCode.NotFound)
            return Results.NotFound("Tag is not found.");
    }

    if (string.IsNullOrEmpty(tag.Url))
    {
        return Results.Problem("This tag does not have URL",null,510);
    }


    
    var parameters = targetUrl.Split(new Char[] { '/', '?', '&', '=' },StringSplitOptions.RemoveEmptyEntries).Where(x => x.StartsWith('@')).ToArray();

    app.Logger.LogInformation($"Url parameters{  string.Join(",", parameters) }");

    app.Logger.LogInformation($"ExecuteTag is called with {request.Query["Param1"].ToString()}");

    return Results.Ok(tag);
};






