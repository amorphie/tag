
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

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

var client = new DaprClientBuilder().Build();

string TAG_STORE_NAME = "ss-tag";

RegisterRoutes();

// Ensure to Dapr sidecar is activated.
using (var tokenSource = new CancellationTokenSource())
{
    await client.WaitForSidecarAsync(tokenSource.Token);
}

app.Run();


void RegisterRoutes()
{
    app.MapGet("/tag", GetAllTags)
    .WithOpenApi(operation =>
    {
        operation.Summary = "Returns saved tag records.";
        operation.Parameters[0].Description = "Paging parameter. **limit** is the page size of resultset.";
        operation.Parameters[1].Description = "Paging parameter. **Token** is returned from last query.";
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

}

#region Route Implementations

async Task<IResult> GetAllTags(
    [Range(5, 100)]
    [FromQuery] int? limit,
    [FromQuery] string? token)
{
    if (!limit.HasValue) limit = 100;

    var query = $"{{ \"page\": {{ \"limit\": {limit}, \"token\": \"{token}\" }} }}";
    var tags = await client.QueryStateAsync<TagInfo>(TAG_STORE_NAME, query);

    return Results.Ok(new TagInfoList(tags.Results.Select(tag =>
        new TagInfo(tag.Data.Name, tag.Data.URL, tag.Data.TTL))
        .ToArray(), tags.Token));
};


async Task<IResult> GetTag([FromRoute(Name = "tag-name")] string tagName)
{
    var returnValue = await client.GetStateAsync<TagInfo>(TAG_STORE_NAME, tagName);

    if (returnValue == null)
        return Results.NotFound();

    return Results.Ok(returnValue);
};



async Task<IResult> SaveTag([FromBody] TagInfo tagInfo)
{
    try
    {
        //throw new Exception("FOR TESTING PURPOSE");

        // Check is the tag exists ?
        var existingRecord = await client.GetStateAsync<TagInfo>(TAG_STORE_NAME, tagInfo.Name);

        await client.SaveStateAsync(TAG_STORE_NAME, tagInfo.Name, tagInfo);
        var returnValue = await client.GetStateAsync<TagInfo>(TAG_STORE_NAME, tagInfo.Name);

        if (existingRecord == null)
        {
            return Results.Created($"/tag/{tagInfo.Name}", returnValue);
        }
        else
        {
            return Results.Ok(returnValue);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.StackTrace);
        return Results.Problem(ex.Message, tagInfo.Name, 503, ex.Source);
    }
};

#endregion

#region Models

record TagInfo(string Name, string URL, int TTL);
record TagInfoList(TagInfo[] items, string token);

#endregion 





