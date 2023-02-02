

var builder = WebApplication.CreateBuilder(args);
using var client = new DaprClientBuilder().Build();

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
Console.WriteLine("Test: " + builder.Configuration["STATE_STORE"]);

builder.Services.AddDbContext<TagDBContext>
    (options => options.UseNpgsql(builder.Configuration["PostgreDB"], b => b.MigrationsAssembly("amorphie.tag")));


var app = builder.Build();

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