using System.ComponentModel.DataAnnotations;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string TAG_STORE_NAME = "ss-tag";

var client = new DaprClientBuilder().Build();


app.MapGet("/tag", async ([FromQuery][Range(5, 100)] int? limit, [FromQuery] string? token) =>
{
    if (!limit.HasValue) limit = 100;

    var query = $"{{ \"page\": {{ \"limit\": {limit}, \"token\": \"{token}\" }} }}";
    var tags = await client.QueryStateAsync<TagInfo>(TAG_STORE_NAME, query);

    return tags;
});

app.MapGet("/tag/{tag-name}", async ([FromRoute(Name = "tag-name")] string tagName) =>
{
    var returnValue = await client.GetStateAsync<TagInfo>(TAG_STORE_NAME, tagName);
    return returnValue;
});

app.MapPost("/tag", (TagInfo tagInfo) =>
{
    client.SaveStateAsync(TAG_STORE_NAME, tagInfo.Name, tagInfo);
    return "";
});


app.Urls.Add("http://localhost:4001");
app.Run();

record TagInfo(string Name, string URL, int TTL);
