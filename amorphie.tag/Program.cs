
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Dapr.Client;
using amorphie.tag.data;
using Microsoft.EntityFrameworkCore;



var client = new DaprClientBuilder().Build();
var configuration = await client.GetConfiguration("configstore", new List<string>() { "config-amorphie-ss-tag", "config-amorphie-tag-db" });
string TAG_STATE_STORE_NAME = configuration.Items["config-amorphie-ss-tag"].Value;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(configuration.Items["config-amorphie-tag-db"].Value, b => b.MigrationsAssembly("amorphie.tag")));


var app = builder.Build();

app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

RegisterRoutes();

try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();

}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}


void RegisterRoutes()
{
    app.Logger.LogInformation("Registering Routes");

    app.MapGet("/tag", GetAllTags)
    .WithOpenApi(operation =>
    {
        operation.Summary = "Returns saved tag records.";
        operation.Parameters[0].Description = "Filtering parameter. Given **label** is used to filter tags.";
        operation.Parameters[1].Description = "Paging parameter. **limit** is the page size of resultset.";
        operation.Parameters[2].Description = "Paging parameter. **Token** is returned from last query.";
        return operation;
    })
    .Produces<TagInfoList>(StatusCodes.Status200OK);

    app.MapGet("/tag/{tag-name}", GetTag)
    .WithOpenApi(operation =>
    {
        operation.Summary = "Returns requested tag.";
        operation.Parameters[0].Description = "Name of the requested tag.";
        return operation;
    })
    .Produces<TagInfo>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

    app.MapPost("/tag", SaveTag)
    .WithTopic("pubsub", "SaveTag") // Default topic for bulk save requirement
    .WithOpenApi(operation =>
    {
        operation.Summary = "Saves or updates requested tag.";
        return operation;
    })
    .Produces<TagInfo>(StatusCodes.Status200OK);

    app.MapDelete("/tag/{tag-name}", DeleteTag)
    .WithOpenApi(operation =>
    {
        operation.Summary = "Deletes existing tag.";
        return operation;
    })
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status409Conflict)
    .Produces(StatusCodes.Status200OK);
}

#region Route Implementations

async Task<IResult> GetAllTags(
    [FromQuery] string? label,
    [FromQuery][Range(5, 100)] int limit = 100,
    [FromQuery] string token = "")
{

    //"filter": { "IN": { "lavels": [ "Dev Ops", "Hardware" ] }  }


    var query = $"{{ \"page\": {{ \"limit\": {limit}, \"token\": \"{token}\" }} }}";
    var tags = await client.QueryStateAsync<TagInfo>(TAG_STATE_STORE_NAME, query);

    return Results.Ok(new TagInfoList(tags.Results.Select(tag =>
        new TagInfo(tag.Data.Name, tag.Data.URL, tag.Data.TTL, tag.Data.Labels))
        .ToArray(), tags.Token));
};

async Task<IResult> GetTag([FromRoute(Name = "tag-name")] string tagName, TagDBContext context)
{

    var returnValue = context.Tags.First(t => t.Name == tagName);

    if (returnValue == null)
        return Results.NotFound();

    return Results.Ok(returnValue);
};

async Task<IResult> SaveTag([FromBody] Tag tagInfo, TagDBContext context)
{
    context.Add(tagInfo);
    context.SaveChanges();
};

async Task<IResult> DeleteTag([FromRoute(Name = "tag-name")] string tagName)
{
    var (value, etag) = await client.GetStateAndETagAsync<TagInfo>(TAG_STATE_STORE_NAME, tagName);

    if (value == null)
        return Results.NotFound();

    var returnValue = await client.TryDeleteStateAsync(TAG_STATE_STORE_NAME, tagName, etag);

    if (returnValue)
        return Results.Ok();
    else
        return Results.Conflict();
};

#endregion

#region Models

record TagInfo(string Name, string URL, int TTL, string[] Labels);
record TagInfoList(TagInfo[] items, string token);

#endregion 





