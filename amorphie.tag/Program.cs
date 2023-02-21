using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
using var client = new DaprClientBuilder().Build();
await builder.Configuration.AddVaultSecrets("amorphie-secretstore", "amorphie-secretstore");
var postgreSql = builder.Configuration["PostgreSql"];
var postgreDb = builder.Configuration["PostgreDB"];

//var client = new DaprClientBuilder().Build();
#pragma warning disable 618
//var configurations = await client.GetConfiguration("amorphie-config", new List<string>() { "PostgreDB" });

#pragma warning restore 618

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();
builder.Services.AddDaprClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddEnvironmentVariables();

Console.WriteLine("Environment: " + builder.Environment.EnvironmentName);
Console.WriteLine("State Store: " + builder.Configuration["STATE_STORE"]);
Console.WriteLine("Vault PostgreSql: " + postgreSql);


builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(postgreSql, b => b.MigrationsAssembly("amorphie.tag")));
builder.Services.AddMvc()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();
// var db = app.Services.GetRequiredService<TagDBContext>();
// db.Database.Migrate();
app.UseCloudEvents();
app.UseRouting();
app.MapSubscribeHandler();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Logger.LogInformation("Registering Routes");

app.MapDomainEndpoints();
app.MapTagEndpoints();
app.MapEntityEndpoints();


try
{
    app.Logger.LogInformation("Starting application...");
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Aplication is terminated unexpectedly ");
}